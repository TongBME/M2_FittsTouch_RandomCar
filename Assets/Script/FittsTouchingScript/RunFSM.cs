using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FFTAICommunicationLib;

public class RunFSM : MonoBehaviour {
    
    // all states 
    public enum GameState_Enum
    {
        STATE_CIRCLESHOWING,
        STATE_INCIRCLE,
        STATE_OUTCIRCLE,
        STATE_TRIALSUCCEED,
        STATE_TRIALFAIL,
        STATE_BREAK,
        STATE_NULL
    }; 

    public static GameState_Enum currState; // current state

    // Use this for initialization
    void Start () {
        currState = GameState_Enum.STATE_NULL;
    }
	
	// Update is called once per frame
	void Update () {

        switch (currState)
        {
            case GameState_Enum.STATE_CIRCLESHOWING:
                StateFunc.CircleShowing();
                break;

            case GameState_Enum.STATE_INCIRCLE:
                StateFunc.InCircle();
                break;

            case GameState_Enum.STATE_OUTCIRCLE:
                StateFunc.OutCircle();
                break;

            case GameState_Enum.STATE_TRIALSUCCEED:
                StateFunc.TaskSucceed();
                break;

            case GameState_Enum.STATE_TRIALFAIL:
                StateFunc.TaskFail();
                break;

            case GameState_Enum.STATE_BREAK:
                StateFunc.Break();
                break;

            case GameState_Enum.STATE_NULL:
                break;
        }
    }
}
