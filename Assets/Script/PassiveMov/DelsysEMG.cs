using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;


public class DelsysEMG {
    
    private byte[] ReadAllBytes(BinaryReader reader)
    {
        const int bufferSize = 4096;
        using (var ms = new MemoryStream())
        {
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                ms.Write(buffer, 0, count);
            return ms.ToArray();
        }
    }
    private float[] ConvertByteToFloat(byte[] array)
    {
        float[] floatArr = new float[array.Length / 4];
        for (int i = 0; i < floatArr.Length; i++)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array, i * 4, 4);
            }
            floatArr[i] = BitConverter.ToSingle(array, i * 4);
        }
        return floatArr;
    }

    //example of creating a list of sensor types to keep track of various TCP streams...
    enum SensorTypes { SensorTrigno, SensorTrignoImu, SensorTrignoMiniHead, NoSensor };

    private List<SensorTypes> _sensors = new List<SensorTypes>();
    private Dictionary<string, SensorTypes> sensorList = new Dictionary<string, SensorTypes>();

    //The following are used for TCP/IP connections
    private TcpClient commandSocket;
    private TcpClient imuEmgSocket;
    private const int commandPort = 50040;  //server command port
    private const int ImuEmgDataPort = 50041;

    //The following are streams and readers/writers for communication
    private NetworkStream commandStream;
    private NetworkStream imuEmgStream;
    private StreamReader commandReader;
    private StreamWriter commandWriter;

    //private List<float>[] emgDataList = new List<float>[16];
    private List<float>[] imuEmgDataList = new List<float>[16];
    private List<float> TimestampList = new List<float>();


    //The following are storage for acquired data
    private float[] imuEmgData = new float[16];
    private bool connected = false; //true if connected to server
    private bool running = false;   //true when acquiring data
    private bool Recording; //for EMG data recording

    //Server commands
    private const string COMMAND_QUIT = "QUIT";
    private const string COMMAND_GETTRIGGERS = "TRIGGER?";
    private const string COMMAND_SETSTARTTRIGGER = "TRIGGER START";
    private const string COMMAND_SETSTOPTRIGGER = "TRIGGER STOP";
    private const string COMMAND_START = "START";
    private const string COMMAND_STOP = "STOP";
    private const string COMMAND_SENSOR_TYPE = "TYPE?";

    //Threads for acquiring emg and acc data
    private Thread imuEmgThread;
    private long t0_in_ticks;


    //Before the loop
    StringBuilder csvIMSensors = new StringBuilder();
    String Filename;

    void Start()
    {
        Connect();
    }


        //Use this for initialization
        public void Init (long t0) {

        t0_in_ticks = t0;

        sensorList.Add("A", SensorTypes.SensorTrigno);
        sensorList.Add("D", SensorTypes.SensorTrigno);
        sensorList.Add("L", SensorTypes.SensorTrignoImu);
        sensorList.Add("J", SensorTypes.SensorTrignoMiniHead);
        sensorList.Add("O", SensorTypes.SensorTrignoImu);
    }

    // Update is called once per frame
    public void Update () {
 
        //look for STOP from server
        if (commandStream.DataAvailable)
        {
            if ((commandReader.ReadLine()).StartsWith("STOPPED"))
            {
                //A stop was received from te server.
                commandReader.ReadLine();   //read extra line terminator

                //Stop threads and wait for termination
                running = false;
                //emgThread.Join();
                imuEmgThread.Join();
                Debug.Log("EMG: Server stopped");

            }
        }

        if(Recording == true)
        {
            csvIMSensors.Append(Environment.NewLine);
        }
    }

    //Establish sensors connnection
    public bool Connect()
    {
        try
        {
            //Establish TCP/IP connection to server using URL entered
            commandSocket = new TcpClient("localhost", commandPort);

            //Set up communication streams
            commandStream = commandSocket.GetStream();
            commandReader = new StreamReader(commandStream, Encoding.ASCII);
            commandWriter = new StreamWriter(commandStream, Encoding.ASCII);

            //Get initial response from server and display
            commandReader.ReadLine();
            commandReader.ReadLine();   //get extra line terminator
            connected = true;   //iindicate that we are connected
        }
        catch (Exception connectException)
        {
            //connection failed, display error message
            Debug.Log("Could not connect.\n" + connectException.Message);
            connected = false;
            return connected;
        }

        //build a list of connected sensor types
        _sensors = new List<SensorTypes>();
        for (int i = 1; i <= 16; i++)
        {
            string query = "SENSOR " + i + " " + COMMAND_SENSOR_TYPE;
            string response = SendCommand(query);
            _sensors.Add(response.Contains("INVALID") ? SensorTypes.NoSensor : sensorList[response]);
        }

        SendCommand("UPSAMPLE OFF");

        return connected;
    }


    //Close connection
    public void Close()
    {
        //Check if running and display error message if not
        if (running)
        {
            Debug.Log("Can't quit while acquiring data!");
            return;
        }

        //send QUIT command
        SendCommand(COMMAND_QUIT);
        connected = false;  //no longer connected

        //Close all streams and connections
        commandReader.Close();
        commandWriter.Close();
        commandStream.Close();
        commandSocket.Close();
        imuEmgStream.Close();
        imuEmgSocket.Close();
    }

    //Send a command to the server and get the response
    private string SendCommand(string command)
    {
        string response = "";

        //Check if connected
        if (connected)
        {
            //Send the command
            commandWriter.WriteLine(command);
            commandWriter.WriteLine();  //terminate command
            commandWriter.Flush();  //make sure command is sent immediately

            //Read the response line and display    
            response = commandReader.ReadLine();
            commandReader.ReadLine();   //get extra line terminator
        }
        else
            Debug.Log("Not connected.");
        return response;    //return the response we got
    }

    public bool IsConnected() { return connected; }
    public bool IsRunning() { return running; }
    public int GetNbSensors() { return _sensors.Count; }


    //Start acquisition
    public void StartAcquisition()
    {
        if (!connected)
        {
            Debug.Log("Not connected.");
            return;
        }

        //write data to csv
        csvIMSensors = new StringBuilder();

        /*for (int i = 0; i < _sensors.Count; i++)
        {
            //Get First Data
            SensorTypes sensor = _sensors[i];
            switch (sensor)
            {
                case SensorTypes.SensorTrignoImu:
                    csvIMSensors.Append(sensor + "EMG" + ",");

                    break;
                case SensorTypes.NoSensor:
                    //append nothing
                    break;
                default:
                    //csvIMSensors.Append(sensor + "EMG" + ",");
                    break;
            }

        }

        if (csvIMSensors.Length > 1)
            csvIMSensors.Remove(csvIMSensors.Length - 1, 1);
            csvIMSensors.Append(Environment.NewLine);*/

        for (int i = 0; i < 16; i++)
        {
            imuEmgDataList[i] = new List<float>();
        }

        //Clear stale data
        imuEmgData = new float[16];

        //Establish data connections and creat streams
        imuEmgSocket = new TcpClient("localhost", ImuEmgDataPort);

        //emgStream = emgSocket.GetStream();
        imuEmgStream = imuEmgSocket.GetStream();

        //Create data acquisition threads
        imuEmgThread = new Thread(ImuEmgWorker);
        imuEmgThread.IsBackground = true;

        //Indicate we are running and start up the acquisition threads
        running = true;
        imuEmgThread.Start();

        //set emg sample rate to 1926 Hz
        string a = SendCommand("RATE?");

        //Send start command to server to stream data
        string response = SendCommand(COMMAND_START);

        //check response
        if (response.StartsWith("OK"))
        {
            Debug.Log("EMG: Ok");
        }
        else
        {
            running = false;    //stop threads
            Debug.Log("EMG: ERROR");
        }
    }

    //Stop acquisition and write to file
    public void StopAcquisition()
    {
        running = false;    //no longer running
                            //Wait for threads to terminate
        imuEmgThread.Join();

        //Send stop command to server
        string response = SendCommand(COMMAND_STOP);
        if (!response.StartsWith("OK"))
            Debug.Log("Server failed to stop. Further actions may fail.");
    }


    public void StartRecording()
    {

        Recording = true;
        //Clear lists imuEmgDataList[sn] emgDataList[sn] x16
        for (int i = 0; i < 16; i++)
        {
            imuEmgDataList[i].Clear();
        }
        TimestampList.Clear();
        csvIMSensors.Remove(0, csvIMSensors.Length);
        csvIMSensors.Length = 0;
        csvIMSensors.Capacity = 0;
        Debug.Log("EMG: Recording...");
    }

    public void StopRecording(String filename)
    {
        Filename = filename;
        Recording = false;

        Debug.Log("EMG: "+ imuEmgDataList[0].Count + " samples recorded.");
        for(int k =1;k< imuEmgDataList[0].Count;k++)
        {
            Debug.Log(imuEmgDataList[0][k]);
        }
        

        //Write to file
        int imEmgIndex = 0;
        while (imEmgIndex < imuEmgDataList[0].Count - 16)
        {
            //Write Time
            csvIMSensors.Append(TimestampList[imEmgIndex] + ",");

            for (int i = 0; i < 16; i++) //emgDataList will contain the most samples
            {
                //Write sensors values
                if (_sensors[i] == SensorTypes.SensorTrignoImu)
                {
                    csvIMSensors.Append(imuEmgDataList[i][imEmgIndex] + ",");
                }
            }
            imEmgIndex++;
            //csvIMSensors.Remove(csvIMSensors.Length - 1, 1);
            //csvIMSensors.Append(Environment.NewLine);
        }
        File.WriteAllText(Filename, "Time, 1,2,3\n");
        File.AppendAllText(Filename, csvIMSensors.ToString());
    }



    // Thread for imu emg acquisition
    private void ImuEmgWorker()
    {
        imuEmgStream.ReadTimeout = 1000;    //set timeout

        BinaryReader reader = new BinaryReader(imuEmgStream);
        while (running)
        {
            try
            {
                if (Recording)
                { 
                    TimestampList.Add((float)((DateTime.Now.Ticks- t0_in_ticks)/ (float)10000000));
                    for (int sn = 0; sn < 16; ++sn)
                        imuEmgDataList[sn].Add(reader.ReadSingle());
                }
                else
                {
                    for (int sn = 0; sn < 16; ++sn)
                        reader.ReadSingle();
                }
            }
            catch (IOException e)
            {

            }
        }

    }

}
