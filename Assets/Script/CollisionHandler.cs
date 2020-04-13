using System;
using UnityEngine;
using System.Collections;
using FFTAICommunicationLib;

public class CollisionHandler : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        //CollYN = GameObject.Find("/Sphere-Player/Cube-Collision-Y");
        //Debug.Log(CollYN);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //not use this funtion in this project
    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log("Collision object name：" + collider.gameObject.name);

    }
    
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Triggle Active Collider：" + collider.gameObject.name);
        if (collider.gameObject.name == "CollisionYP-CCW")
        {
            //Debug.Log("Trig：CollisionYP_CCW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x01);
        
        }
        else if (collider.gameObject.name == "CollisionYN-CW")
        {
            //Debug.Log("Trig：CollisionYN_CW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x01);
       
        }
        else if (collider.gameObject.name == "CollisionXP-CCW")
        {
            //Debug.Log("Trig：CollisionXP_CCW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x01);
     
        }
        else if (collider.gameObject.name == "CollisionXN-CW")
        {
            //Debug.Log("Trig：CollisionXN_CW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x01);

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("Triggle Exit Collider：" + collider.gameObject.name);
        
        if (collider.gameObject.name == "CollisionYP-CCW")
        {
            //Debug.Log("Exit：CollisionYP_CCW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
        }
        else if (collider.gameObject.name == "CollisionYN-CW")
        {
            //Debug.Log("Exit：CollisionYN_CW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
        }
        else if (collider.gameObject.name == "CollisionXP-CCW")
        {
            //Debug.Log("Exit：CollisionXP_CCW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
        }
        else if (collider.gameObject.name == "CollisionXN-CW")
        {
            //Debug.Log("Exit：CollisionXN_CW");
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
            DynaLinkHS.CmdSetCurrentPositionAsEndEffectorLimitPosition(0x00);
        }        
    }
 }
