using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FFTAICommunicationLib;
//using DelsysEMG;


// This code is for the purpose of spasticity identification//


public class SpasticityTestPanelManager : MonoBehaviour {

    public SpasticityTestPanelManager SelfPanel;
    public MainSysPanel BckMainPanel;


    // Standard buttons 
    public Button ReturnMain;
    public Button StopMotionBtn;
    public Button StartMotionBtn;

    // Buttons for spasticity test
    public Button RecordP1Btn;
    public Button RecordP2Btn;
    public Button TwentySpdBtn;
    public Button FourtySpdBtn;
    public Button SixtySpdBtn;
    public Button EightySpdBtn;
    public Button HundredSpdBtn;
    public Button TenSpdBtn;
    public Button ThirtySpdBtn;
    public Button FiftySpdBtn;
    public Button SeventySpdBtn;
    public Button NinetySpdBtn;
    public Button MotionFreeBtn;
    
    public InputField MaxVelocity;  //Clinician to put the max velocity to be tested, suggested to be below 1.0 m/s
    float MaxVelocity_float;
    public Text InstructionsTxt;
    float TestingPercentSpeed;
    bool TestCompleted;
    bool TestRunning;
    public float DeltaForX;
    public float DeltaForY;
    public float PrevForceX;
    public float PrevForceY;
    public float TestVelocity;

    public float[] P1, P2;
    public Timer timerBack, timerRecordBack, timerCountDown;
    public long t0;

    public StringBuilder csvcontent;
    public String CurrentCSVFilename = "";
    //string csvpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+"\\SpasticityStudyData\\"+DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss");
    string csvpath = "E:\\Fourier\\Data";

    public DelsysEMG EMG;
    public Text EMGStateTxt;
    public Text CountDownDisplayTxt;
    public int CountDown;

    // Use this for initialization
    void Start () {
        DynaLinkHS.CmdServoOff();

        Directory.CreateDirectory(csvpath);
        csvcontent = new StringBuilder();

        timerBack = new Timer();
        timerBack.Interval = 2000;  //Wait for 2 seconds
        timerBack.Elapsed += BackToP1; //Hook up the elapsed event for the timer
        timerBack.AutoReset = false; //Have the timer fire repeated events(true is the default)
        timerBack.Enabled = false;

        timerRecordBack = new Timer();
        timerRecordBack.Interval = 4000;  //Wait for 4 seconds
        timerRecordBack.Elapsed += WriteToFile; //Hook up the elapsed event for the timer
        timerRecordBack.AutoReset = false; //Have the timer fire repeated events(true is the default)
        timerRecordBack.Enabled = false;


        //Add a count down timer for better UI experience
        timerCountDown = new Timer();
        timerCountDown.Interval = 1000;  //Wait for 3 seconds
        timerCountDown.Elapsed += CountDownDisplay; //Hook up the elapsed event for the timer
        timerCountDown.AutoReset = false; //Have the timer fire repeated events(true is the default)
        timerCountDown.Enabled = false;
        CountDown = 0;

        TestCompleted = false;
        TestingPercentSpeed = 0;

        P1 = new float[2];
        P2 = new float[2];

        ReturnMain.onClick.AddListener(ReturnMainBtnClick);
        StartMotionBtn.onClick.AddListener(StartTestBtnClick);
        StopMotionBtn.onClick.AddListener(StopMotionBtnClick);
        RecordP1Btn.onClick.AddListener(RecordP1BtnClick);
        RecordP2Btn.onClick.AddListener(RecordP2BtnClick);
        MotionFreeBtn.onClick.AddListener(MotionFreeBtnClick);
        

        TwentySpdBtn.onClick.AddListener(TwentySpdBtnClick);
        FourtySpdBtn.onClick.AddListener(FourtySpdBtnClick);
        SixtySpdBtn.onClick.AddListener(SixtySpdBtnClick);
        EightySpdBtn.onClick.AddListener(EightySpdBtnClick);
        HundredSpdBtn.onClick.AddListener(HundredSpdBtnClick);

        TenSpdBtn.onClick.AddListener(TenSpdBtnClick);
        ThirtySpdBtn.onClick.AddListener(ThirtySpdBtnClick);
        FiftySpdBtn.onClick.AddListener(FiftySpdBtnClick);
        SeventySpdBtn.onClick.AddListener(SeventySpdBtnClick);
        NinetySpdBtn.onClick.AddListener(NinetySpdBtnClick);

        RecordP1Btn.enabled = true;
        RecordP2Btn.enabled = false;
        InstructionsTxt.text = "Bring arm to initial position: P1";
        t0 = DateTime.Now.Ticks;

        EMG = new DelsysEMG();
        EMG.Init(t0);
        EMG.Connect();
        if(EMG.IsConnected())
        {
            EMG.StartAcquisition();
            EMGStateTxt.text = EMG.GetNbSensors() + "EMGs connected.";
        }
        else
        {
            EMGStateTxt.text = "EMGs NOT connected.";
        }
    }

    
    // Update is called once per frame
    void Update ()
    {
        //Add code to record force(Fx and Fy), position(X and Y), velocity(Vx and Vy)

        if(EMG.IsConnected())
            EMG.Update();

        //Add force protection for 40N
        if (DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue > 40 || DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue < -40 || DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue > 40 || DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue < -40) //Need to define the max force that will never be reached
        {
            DynaLinkHS.CmdServoOff();
        }


        //Define catch as a sudden increase of force.
        DeltaForX = DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue - PrevForceX;
        DeltaForY = DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue - PrevForceY;
        if (DeltaForX > 15 || DeltaForY > 15)
        {
            DynaLinkHS.CmdServoOff();
        }
        PrevForceX = DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue;
        PrevForceY = DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue;

        //Save data
        if (TestRunning)
        {
            csvcontent.AppendLine((float)((DateTime.Now.Ticks - t0) / (float)10000000) + "," + DynaLinkHS.StatusRobot.PositionDataJoint1.ToString() + "," + DynaLinkHS.StatusRobot.PositionDataJoint2 + "," + DynaLinkHS.StatusRobot.VelocityDataJoint1 + "," + DynaLinkHS.StatusRobot.VelocityDataJoint2 + "," + DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue + "," + DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue);
        }

    
        //Mange countdown display
        if(CountDown>0)
        {
            CountDownDisplayTxt.text = CountDown.ToString();
            CountDownDisplayTxt.enabled = true;
        }
        else
        {
            CountDownDisplayTxt.enabled = false;
        }

    }

    void RecordP1BtnClick()
    {
        //Record the position of P1 (Free x, fix y(y=0) and find the position for P1)
        P1[0] = DynaLinkHS.StatusRobot.PositionDataJoint1;
        P1[1] = DynaLinkHS.StatusRobot.PositionDataJoint2;
        RecordP2Btn.enabled = true;
        //Provide user info
        InstructionsTxt.text = "Clinicians bring arm to final position: P2";
    }

    void RecordP2BtnClick()
    {
        //Record the position of P2 (Fix x(from P1), free y and find the position for P2)
        P2[0] = DynaLinkHS.StatusRobot.PositionDataJoint1;
        P2[1] = DynaLinkHS.StatusRobot.PositionDataJoint2;
        //InstructionsTxt.text = "Press start after P2 is recorded.";
    }

    void MotionFreeBtnClick()
    {
        //Allow free motion in both x and y directions(need to apply control to offset the frictions)
        //Transparent controll
        InstructionsTxt.text = "Robot is in transparent mode.";
        DynaLinkHS.CmdServoOff();

    }

    void StartTestBtnClick()
    {
        //Function: Bring the arm back to P1 after P2 has been recorded.
        MaxVelocity_float = float.Parse(MaxVelocity.text);
        timerBack.Start();
        InstructionsTxt.text = "Relax: robot will bring arm back slowly.";
    }

    void BackToP1(object source, System.Timers.ElapsedEventArgs e)
    {
        //InstructionsTxt.text = "Warning: Robot moving to initial position.";
        DynaLinkHS.CmdPassiveMovementControl(P1[0], P1[1], (float)0.02);
    }

    //Write the data to a csv file and store them at C:\\Fourier\\M2Code\\SpasticityData
    void WriteToFile(object source, System.Timers.ElapsedEventArgs e)
    {
        string csvfullfilename = csvpath + "\\" + CurrentCSVFilename + ".csv";
        string filename = csvpath + "\\" + CurrentCSVFilename + "EMG.csv";

        if (EMG.IsConnected())
            EMG.StopRecording(filename);
        File.WriteAllText(csvfullfilename, "Time, Xpos, Ypos, Xspd, Ysped, Xfor, Yfor\n");
        File.AppendAllText(csvfullfilename, csvcontent.ToString());
        BackToP1(source, e);
    }

    void CountDownDisplay(object source, System.Timers.ElapsedEventArgs e)
    {
        //Manage Countdown
        CountDown = CountDown-1;

        if (CountDown > 0)
        {
           timerCountDown.Start();
        }
        else
        {
            //Trapezoid velocity profile
            //DynaLinkHS.CmdPassiveMovementControl(P2[0], P2[1], MaxVelocity_float * (float)TestVelocity);

            //Mim jerk velocity profile
            DynaLinkHS.CmdMinimumJerkTrajectoryControl(P1[0], P1[1], P2[0], P2[1], ((float)1.875)*(P2[1]- P1[1]) /(MaxVelocity_float * (float)TestVelocity));
            timerRecordBack.Start();//Start the timer
        }
    }

    void TenSpdBtnClick()  //10% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "10Spd";

        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }
        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.1;
        timerCountDown.Start(); //Start the timer 
    }

    void TwentySpdBtnClick()  //20% of max velocity with EMG recording
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "20Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.2;
        timerCountDown.Start(); //Start the timer 
    }

    void ThirtySpdBtnClick()  //30% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "30Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.3;
        timerCountDown.Start(); //Start the timer 
    }

    void FourtySpdBtnClick()  //40% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "40Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.4;
        timerCountDown.Start(); //Start the timer 
    }

    void FiftySpdBtnClick()  //50% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "50Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.5;
        timerCountDown.Start(); //Start the timer 
    }

    void SixtySpdBtnClick()   //60% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "60Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.6;
        timerCountDown.Start(); //Start the timer 
    }

    void SeventySpdBtnClick()  //70% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "70Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.7;
        timerCountDown.Start(); //Start the timer 
    }

    void EightySpdBtnClick()  //80% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "80Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.8;
        timerCountDown.Start(); //Start the timer 
    }

    void NinetySpdBtnClick()  //90% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "90Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)0.9;
        timerCountDown.Start(); //Start the timer 
    }

    void HundredSpdBtnClick()  //100% of max velocity
    {
        TestRunning = true;
        csvcontent.Remove(0, csvcontent.Length);
        CurrentCSVFilename = "100Spd";
        if (EMG.IsConnected())
        {
            if (EMG.IsRunning())
                EMGStateTxt.text = "EMG recording";
            else
                EMGStateTxt.text = "EMG ERROR !";
            EMG.StartRecording();
        }

        CountDown = 3;
        //Show countdown
        CountDownDisplayTxt.text = CountDown.ToString();
        CountDownDisplayTxt.enabled = true;

        TestVelocity = (float)1.0;
        timerCountDown.Start(); //Start the timer 
    }


    void ReturnMainBtnClick()
    {

        //Stop EMG Acuisition
        if (EMG.IsConnected())
        {
            EMG.StopAcquisition();
            EMG.Close();
        }
        
        //Return to the main panel
        BckMainPanel.gameObject.SetActive(true);
        SelfPanel.gameObject.SetActive(false);
    }

    void StopMotionBtnClick()
    {
        DynaLinkHS.CmdServoOff();   //For safety purpose 
        // Clinicians can click this for safety purpose
    }

    
}
