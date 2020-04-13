/********************************************************************************************
* File: Demo Application for DynaLink Cmd application
* File Task: Manager all the Data Display informtaion control
* Release data: 2017-June-15
* Release Version: V3.0.0
* Communition Protol: Ethernet, UDP/IP
* Communition Application define: DynaLink V6.2
********************************************************************************************/
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using FFTAICommunicationLib;
public class DataDispManager : MonoBehaviour
{
    public Text DispCurrentPosX;
    public Text DispCurrentPosY;
    public Text DispCurrentSpdX;
    public Text DispCurrentSpdY;
    public Text DispCurrentTorX;
    public Text DispCurrentTorY;
    public Text DispValADC1;
    public Text DispValADC2;
    public Text DispEthCounts;
    public Text DispFeedBackCounts;
    public Text BuffUse;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        DispCurrentPosX.text = DynaLinkHS.StatusRobot.PositionDataJoint1.ToString();//DiagnosticStatus.MotRTdata.LowPassPosJ1.ToString();
        DispCurrentPosY.text = DynaLinkHS.StatusRobot.PositionDataJoint2.ToString();//DiagnosticStatus.MotRTdata.LowPassPosJ2.ToString();
        DispCurrentSpdX.text = DynaLinkHS.StatusRobot.VelocityDataJoint1.ToString();
        DispCurrentSpdY.text = DynaLinkHS.StatusRobot.VelocityDataJoint2.ToString();
        DispValADC1.text = DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue.ToString();
        DispValADC2.text = DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue.ToString();
        //DispEthCounts.text = DynaLinkHS.EthCounts.ToString();
        //DispFeedBackCounts.text = DynaLinkHS.DynaLinkAckCnt.ToString();
        //BuffUse.text = DynaLinkHS.Ringbuff.BuffPoint.ToString();

    }

}
