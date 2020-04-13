using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFTAICommunicationLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MoveObject : MonoBehaviour
{
    public GameObject objPoint;
    public static Vector2 screenPos;
    private Vector2 screenSize;

    void Start()
    {    
        objPoint = GameObject.Find("handpoint");
        objPoint.transform.position = new Vector2(0.0f,0.0f);
        objPoint.GetComponent<SpriteRenderer>().color = Color.red;
        objPoint.SetActive(true);

        //screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));// 屏幕尺寸
        
    }

    void Update()
    {
        float xMid = (InitSetting.M2_AXIS_X[1] + InitSetting.M2_AXIS_X[0]) / 2f;
        float yMid = (InitSetting.M2_AXIS_Y[1] + InitSetting.M2_AXIS_Y[0]) / 2f;
        float xM2 = (float)(DynaLinkHS.StatusRobot.PositionDataJoint1 - xMid);
        float yM2 = (float)(DynaLinkHS.StatusRobot.PositionDataJoint2 - yMid);
        float objectX = xM2 * InitSetting.xOffset;
        float objectY = yM2 * InitSetting.yOffset;
        screenPos = new Vector2(objectX, objectY);
        objPoint.transform.position = screenPos;
        //print(DynaLinkHS.StatusRobot.PositionDataJoint1);
        print(DynaLinkHS.StatusRobot.PositionDataJoint2);

        //Vector2 mousePos = Input.mousePosition; //mouse X and Y
        //screenPos = Camera.main.WorldToScreenPoint(objPoint.transform.position);
        //Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = worldPos;
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenSize.x, screenSize.x), Mathf.Clamp(transform.position.y, -screenSize.y, screenSize.y));
        ////record object trajectory in the screen
    }
}
