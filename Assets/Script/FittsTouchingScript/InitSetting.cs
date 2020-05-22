using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFTAICommunicationLib;
using System.Timers;

public class InitSetting : MonoBehaviour {


    private Vector2 screenSize;
    public static float[] M2_AXIS_X = new float[] { -0.022f, 0.551f };
    public static float[] M2_AXIS_Y = new float[] { -0.286f, 0.288f }; // 傅利叶公司

    //public static float[] M2_AXIS_X = new float[] { -0.009f, 0.564f };
    //public static float[] M2_AXIS_Y = new float[] { 0.0001f, 0.541f }; // 瑞金

    public static float xOffset;
    public static float yOffset;
    public Timer timerStart;

// Use this for initialization
void Start()
    {
        DynaLinkHS.CmdTransparentControl(5f, 5f, 10f, 10f, 10f, 10f, 10f, 10f);
        //DynaLinkHS.CmdServoOff();

        ///*
        // Initiate Hand Effector Position 
        ///*
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));// 尺寸的一半
        print(screenSize);// screen size
        float M2_SIZE_X = (M2_AXIS_X[1] - M2_AXIS_X[0]) / 2;
        float M2_SIZE_Y = (M2_AXIS_Y[1] - M2_AXIS_Y[0]) / 2;
        xOffset = (screenSize.x) / M2_SIZE_X;
        yOffset = (screenSize.y) / M2_SIZE_Y;
        DynaLinkHS.CmdPassiveMovementControl((M2_AXIS_X[1] + M2_AXIS_X[0]) / 2, (M2_AXIS_Y[1] + M2_AXIS_Y[0]) / 2, (float)0.05);

        ///*
        // Initiate Control Mode(mass/damping/spring)
        /// for M2, the parameters are
        ///     - origin/target position x [原始/目标位置 x 轴] (type : float, unit : m, range : )
        ///     - origin/target position y [原始/目标位置 y 轴] (type : float, unit : m, range : )
        ///     - M (mass) x [模拟质量 x 轴] (type : float, unit : kg, range : )
        ///     - M (mass) y [模拟质量 y 轴] (type : float, unit : kg, range : )
        ///     - B (damping) x [模拟阻尼 x 轴] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) y [模拟阻尼 y 轴] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) x [模拟弹簧 x 轴] (type : float, unit : N/m, range : )
        ///     - K (spring) y [模拟弹簧 y 轴] (type : float, unit : N/m, range : )
        ///*  

        ///*
        ///  Renew/Load .yaml
        ///*
        
        //SetYaml.IniTaskInfo();// initiate .yaml
        SetYaml.LoadTaskInfo();// load .yaml

        timerStart = new Timer();
        timerStart.Interval = 10000;  //Wait for 10 seconds
        timerStart.Elapsed += GameStart; //Hook up the elapsed event for the timer
        timerStart.AutoReset = false; //Have the timer fire repeated events(true is the default)
        timerStart.Enabled = false;
        timerStart.Start();
    }

    void GameStart(object source, System.Timers.ElapsedEventArgs e)
    {
        
        DynaLinkHS.CmdServoOff();
        RunFSM.currState = RunFSM.GameState_Enum.STATE_BREAK;// initial state
        StateFunc.breakTimer = SetYaml.breakTimeSet;// break timer
        print("初始化结束");
    }

}
