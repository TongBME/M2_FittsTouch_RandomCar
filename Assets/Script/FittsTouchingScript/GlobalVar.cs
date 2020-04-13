using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar : MonoBehaviour {

    //collision state 
    public static bool isCollisionOn; // collide from ourside
    public static bool isCollisionOff; // exit from inside
    public static bool isInit; 
    //file setting
    public static string FILENAME = "Trajectory"; // save .CSV file
    public static string YAMLNAME = "FittsTouchingTask";// load .yaml file


    // Use this for initialization
    void Start()
    {
        isCollisionOn = false;
        isCollisionOff = false;
        isInit = false;
    }
}
