using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System;


public class StateFunc: MonoBehaviour {

    private static float taskTimer;
    public static float circleTimer;
    public static float breakTimer;
    public static int taskNum = 0;
    private static float timeLine = 0f;
    private static List<float> timeSequence = new List<float>();
    private static List<int> trialNum = new List<int>();
    private static List<string> taskState = new List<string>();
    private static List<float> postitionX = new List<float>();
    private static List<float> postitionY = new List<float>();
    public static StringBuilder csvContent;

    private void Start()
    {
        csvContent = new StringBuilder();
    }
    public static void CircleShowing()
    {
        taskTimer -= Time.deltaTime;// task timer
        // record data
        timeLine += Time.deltaTime;
        timeSequence.Add(timeLine);// time
        postitionX.Add(MoveObject.screenPos.x); // cursor X
        postitionY.Add(MoveObject.screenPos.y); // cursor Y
        //print(MoveObject.screenPos.y);
        //print(MoveObject.screenPos.y);
        trialNum.Add(taskNum);
        taskState.Add("State_CircleShowing");
        print("State_CircleShowing");
        csvContent.AppendLine((float)timeLine + "," + taskNum + "," + "State_CircleShowing" + "," + MoveObject.screenPos.x + "," + MoveObject.screenPos.y);

        if (taskTimer <= 0f)
        {
            // 【circle shows】transfer to 【trial fail】
            RunFSM.currState = RunFSM.GameState_Enum.STATE_TRIALFAIL;
        }
        else
        {
            if (GlobalVar.isCollisionOn == true)
            {
                // 【circle shows】transfer to 【in circle】
                RunFSM.currState = RunFSM.GameState_Enum.STATE_INCIRCLE;
                circleTimer = SetYaml.circleTimeSet;// 圈内定时开始
            }
        }      
    }

    public static void InCircle()
    {
        taskTimer -= Time.deltaTime; 
        circleTimer -= Time.deltaTime; 
        ShowCircle.ChangeCircleColor(taskNum);
        // record data
        timeLine += Time.deltaTime;
        timeSequence.Add(timeLine);// time
        postitionX.Add(MoveObject.screenPos.x); // cursor X
        postitionY.Add(MoveObject.screenPos.y); // cursor Y
        trialNum.Add(taskNum);
        taskState.Add("State_InCircle");
        print("State_InCircle");
        csvContent.AppendLine((float)timeLine + "," + taskNum + "," + "State_InCircle" + "," + MoveObject.screenPos.x + "," + MoveObject.screenPos.y);


        if (taskTimer <= 0f)
        {
            // 【in circle】transfer to 【trial fail】
            RunFSM.currState = RunFSM.GameState_Enum.STATE_TRIALFAIL;
        }
        else
        {
            if (GlobalVar.isCollisionOff == true)
            {
                // 【in circle】transfer to【out circle】
                RunFSM.currState = RunFSM.GameState_Enum.STATE_OUTCIRCLE;
            }
            else
            {
                if (circleTimer <= 0f)
                {
                    // 【in circle】transfer to【trial succeed】
                    RunFSM.currState = RunFSM.GameState_Enum.STATE_TRIALSUCCEED;
                }
            }
        }  
    }

    public static void OutCircle()
    {
        ShowCircle.aimObject.GetComponent<SpriteRenderer>().color = Color.grey;
        taskTimer -= Time.deltaTime;
        // record data
        timeLine += Time.deltaTime;
        timeSequence.Add(timeLine);// time
        postitionX.Add(MoveObject.screenPos.x); // cursor X
        postitionY.Add(MoveObject.screenPos.y); // cursor Y
        trialNum.Add(taskNum);
        taskState.Add("State_OutCircle");
        print("State_OutCircle");
        csvContent.AppendLine((float)timeLine + "," + taskNum + "," + "State_OutCircle" + "," + MoveObject.screenPos.x + "," + MoveObject.screenPos.y);


        if (taskTimer <= 0f)
        {
            // 【out circle】transfer to【trial fail】
            RunFSM.currState = RunFSM.GameState_Enum.STATE_TRIALFAIL;
        }
        else
        {
            if (GlobalVar.isCollisionOn == true)
            {
                // 【out circle】transfer to【in circle】
                RunFSM.currState = RunFSM.GameState_Enum.STATE_INCIRCLE;
                circleTimer = SetYaml.circleTimeSet;// trial timer start
            }
        }
    }

    public static void TaskSucceed()
    {
        // record data
        timeLine += Time.deltaTime;
        timeSequence.Add(timeLine);// time
        postitionX.Add(MoveObject.screenPos.x); // cursor X
        postitionY.Add(MoveObject.screenPos.y); // cursor Y
        trialNum.Add(taskNum);
        taskState.Add("State_TaskSucceed");
        csvContent.AppendLine((float)timeLine + "," + taskNum + "," + "State_TaskSucceed" + "," + MoveObject.screenPos.x + "," + MoveObject.screenPos.y);


        taskNum += 1;
        breakTimer = SetYaml.breakTimeSet;// break计时
        // 【trial succeed】transfer to【break】
        RunFSM.currState = RunFSM.GameState_Enum.STATE_BREAK;
    }

    public static void TaskFail()
    {
        // record data
        timeLine += Time.deltaTime;
        timeSequence.Add(timeLine);// time
        postitionX.Add(MoveObject.screenPos.x); // cursor X
        postitionY.Add(MoveObject.screenPos.y); // cursor Y
        trialNum.Add(taskNum);
        taskState.Add("State_TaskFail");
        csvContent.AppendLine((float)timeLine + "," + taskNum + "," + "State_TaskFail" + "," + MoveObject.screenPos.x + "," + MoveObject.screenPos.y);


        taskNum += 1;
        breakTimer = SetYaml.breakTimeSet;// break timer
        // 【trial fail】transfer to【break】
        RunFSM.currState = RunFSM.GameState_Enum.STATE_BREAK;
    }
    public static void Break()
    {

        if (taskNum >= SetYaml.trialNum)
        {
            ShowCircle.aimObject.SetActive(false);
            // save data
            //SaveCsv.SaveData(System.Environment.CurrentDirectory + "\\FittsTouchingEXP\\" + GlobalVar.FILENAME + "1.csv",
            //timeSequence, trialNum, taskState, postitionX, postitionY);

            string csvfullfilename = System.Environment.CurrentDirectory + "\\FittsTouchingEXP\\" + GlobalVar.FILENAME + "1.csv";

            File.WriteAllText(csvfullfilename, "Time,Trial,State,PositionX,PositionY\n");
            File.AppendAllText(csvfullfilename, csvContent.ToString());
            Application.Quit();
        }
        else
        {
            ShowCircle.aimObject.GetComponent<Renderer>().enabled = false; // circle vanish
            breakTimer -= Time.deltaTime; // break timer -- 
            timeLine += Time.deltaTime;
            timeSequence.Add(timeLine);// time
            postitionX.Add(MoveObject.screenPos.x); // cursor X
            postitionY.Add(MoveObject.screenPos.y); // cursor Y
            trialNum.Add(taskNum);
            taskState.Add("State_Break");
            print("State_Break");
            csvContent.AppendLine((float)timeLine + "," + taskNum + "," + "State_Break" + "," + MoveObject.screenPos.x + "," + MoveObject.screenPos.y);

            if (breakTimer <= 0f)
            {
                // 【break】transfer to【circle shows, next trial starts】
                RunFSM.currState = RunFSM.GameState_Enum.STATE_CIRCLESHOWING;
                taskTimer = SetYaml.taskTimeSet; // next trial starts
                ShowCircle.RandomCircle(taskNum); //new circle
                GlobalVar.isCollisionOn = false;
                GlobalVar.isCollisionOff = false;
            }
        }
    }

    /// <summary>
    /// Collision Detect
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        GlobalVar.isCollisionOn = true;
        GlobalVar.isCollisionOff = false;
        TimerFlash.FlashSet();

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GlobalVar.isCollisionOff = true;
        GlobalVar.isCollisionOn = false;
    }
}
