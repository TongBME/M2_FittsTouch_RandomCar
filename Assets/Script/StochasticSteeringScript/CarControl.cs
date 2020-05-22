using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using FFTAICommunicationLib;

public class CarControl : MonoBehaviour
{
    static List<string> mWriteTxt = new List<string>();
    private static string csvFileName = "car_controlling";
    public StringBuilder csvContent;

    public Text timeText;
    private int timer = 0;
    private int time = 0;
    private int pixelCount = 25;//(car.y-startpostion.y)/0.2
    private float carMoveForward = 0;
    private float randNormal = 0f;
    private float mean = 1f;
    private float stdDev = 0.8f;
    private float carSpeedY = 0.5f;
    private float joyStickX = 0;
    private double startPosition = 0;
    private double x = 0f;
    private double v = 0f;
    private double tt = 1 / 60f;  //不加f这个值就是0

    public void Awake()
    {
        //定时器
        InvokeRepeating("LaunchProjectile", 0, 0.02F);  //0秒后，每0.1f调用一次

    }
    void LaunchProjectile()
    {
        //产生高斯扰动

        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
        double u2 = rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                     Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        randNormal = (float)
                     (mean + stdDev * randStdNormal); //random normal(mean,stdDev^2)
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(5);    //注意等待时间的写法
    }
    void Start()
    {

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
      
        Cursor.visible = false;
        //每次启动客户端删除之前保存的Log  

        /*
        if (File.Exists(outPath))
        {
            File.Delete(outPath);
        }
        */

        csvContent = new StringBuilder();
        //startposition = DynaLinkHS.StatusRobot.PositionDataJoint1;
        startPosition = DynaLinkHS.StatusRobot.PositionDataEndEffectorX1;
    }

    void Update()
    {

        if (carMoveForward <= 950)
        {
            // M2 X axis = [-0.022,0.551]; 
            // [ -(0.551 + 0.023)/2, +(0.551 + 0.023)/2 ] = [-0.278,+0.278];
            // enlarge to grassland[-10,10], gain = 10/0.278 = 35.97;

            joyStickX = (float)((DynaLinkHS.StatusRobot.PositionDataEndEffectorX1 - startPosition) * 35.97);
            x = joyStickX + randNormal;
            //print(JoystickX);


            /* by Huaqing
            v = 20 * (JoystickX + randNormal - 0.1*v) * tt + v;
            x = v * tt + 0.5 * 20 * (JoystickX + randNormal) * tt * tt + x;
            */

            // white = [-1,1]  black road = [-5,-1]&[1,5], grass = [-10,-5]&[5,10].

            Vector3 move = new Vector3((float)x, carMoveForward, 0);
            transform.position = move;

            if (move.x <= (Bezier.pixel[pixelCount].x + 5) && move.x >= (Bezier.pixel[pixelCount].x + 1))
            {
                carMoveForward += carSpeedY;
                pixelCount += 4;
            }
            else if (move.x <= (Bezier.pixel[pixelCount].x - 1) && move.x >= (Bezier.pixel[pixelCount].x - 5))
            {
                carMoveForward += carSpeedY;
                pixelCount += 4;

            }
            else if (move.x < (Bezier.pixel[pixelCount].x + 1) && move.x > (Bezier.pixel[pixelCount].x - 1))
            {
                carMoveForward += carSpeedY / 2;
                pixelCount += 2;
            }
            else
            {
                carMoveForward += carSpeedY / 4;
                pixelCount++;
            }

            time = (int)Time.time;

            csvContent.AppendLine((float)joyStickX + "," + transform.position.x + "," + Bezier.pixel[pixelCount - 1].x);

        }

        if (carMoveForward > 950)
        {
            Debug.Log(time);
            timeText.text = ("所用时间为" + time + "秒");
            timer++;
            WriteToFile();
          
            if (timer > 300)
            {
                Application.Quit();
            }

        }

    }

    void WriteToFile()
    {
        string csvfullfilename = System.Environment.CurrentDirectory + "\\StotisticSteeringEXP\\" + csvFileName + ".csv";
        File.WriteAllText(csvfullfilename, "JoystickX,PositionX, Besier\n");
        File.AppendAllText(csvfullfilename, csvContent.ToString());
    }
   
}
