using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using FFTAICommunicationLib;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Timers;

public class MainSysPanel : MonoBehaviour {

    public MainSysPanel MainPanel;
    public SpasticityTestPanelManager SpasticityTestPanel;  // For spasticity test

    public UpgradePanelManager UpgradePanelManager;

    public GameObject Player;
    public GameObject Beacon;

    public Button ConnectNetBtn;
    public Button DisConnectNetBtn;

    public Button SpasticityTestBtn;
    public Button CarGameBtn;
    public Button TouchingGameBtn;

    private System.Timers.Timer timerInit;
    private static bool isCarGame;

    // Use this for initialization
    void Start()
    {
        ConnectNetBtn.onClick.AddListener(ConnectNetBtnClick);
        DisConnectNetBtn.onClick.AddListener(DisConnectNetBtnClick);
        SpasticityTestBtn.onClick.AddListener(OnSpasticityTestBtnClick);
        CarGameBtn.onClick.AddListener(OnCarGameBtnClick);
        TouchingGameBtn.onClick.AddListener(OnFittsTouchingBtnClick);
    }

    void FixedUpdate()
    {
        //TextRobotType.text = DynaLinkHS.StatusApp.RobotType.ToString ();
        //TextMechanismType.text = DynaLinkHS.MechanismType.ToString ();
    }

    void ConnectNetBtnClick()
    {
        DynaLinkCore.ConnectClick();
    }

    void DisConnectNetBtnClick()
    {
        DynaLinkCore.StopSocket();
    }


    //Wenhao: for spasticity test
    void OnSpasticityTestBtnClick()
    {
        SpasticityTestPanel.gameObject.SetActive(true);
        MainPanel.gameObject.SetActive(false);
    }


    private void OnCarGameBtnClick()
    {
        isCarGame = false;
        timerInit = new System.Timers.Timer();
        timerInit.Interval = 10000;  //Wait for 10 seconds
        timerInit.Elapsed += CarGameStart; //Hook up the elapsed event for the timer
        timerInit.AutoReset = false; //Have the timer fire repeated events(true is the default)
        timerInit.Enabled = false;
        timerInit.Start();

        float M2_SIZE_X = (float)(0.551 + 0.022) / 2;
        DynaLinkHS.CmdPassiveMovementControl((-0.022f + 0.551f) / 2, (0.02f + 0.2417f) / 2, (float)0.05);
    }
    private void Update()
    {
        if(isCarGame == true)
        {
            isCarGame = false;
            SceneManager.LoadScene("StochasticSteering", LoadSceneMode.Additive);  // 登陆成功则切换到stochastic steering
        }
    }
    private void CarGameStart(object source, System.Timers.ElapsedEventArgs e)
    {
        DynaLinkHS.CmdServoOff();
        isCarGame = true;


    }

    private void OnFittsTouchingBtnClick()
    {
        SceneManager.LoadScene("FittsTouching", LoadSceneMode.Additive);  // 登陆成功则切换fitts touching
    }

    private void onClickSetButton()
    {
		/*string robotTypeString = InputFieldRobotType.text;
		string mechanismTypeString = InputFieldMechanismType.text;

		DynaLinkHSPara.RobotType robotType = DynaLinkHSPara.RobotType.None;
		DynaLinkHSPara.MechanismType mechanismType = DynaLinkHSPara.MechanismType.None;

		switch (robotTypeString) {
		case "M1":
			robotType = DynaLinkHSPara.RobotType.M1;
			break;
		case "M2": 
			robotType = DynaLinkHSPara.RobotType.M2;
			break;
		default:
			break;
		}

		switch (mechanismTypeString) {
		case "V1":
			mechanismType = DynaLinkHSPara.MechanismType.V1;
			break;
		case "V2": 
			mechanismType = DynaLinkHSPara.MechanismType.V2;
			break;
		case "V3": 
			mechanismType = DynaLinkHSPara.MechanismType.V3;
			break;
		case "V4": 
			mechanismType = DynaLinkHSPara.MechanismType.V4;
			break;
		case "V5": 
			mechanismType = DynaLinkHSPara.MechanismType.V5;
			break;
		case "MiniV1": 
			mechanismType = DynaLinkHSPara.MechanismType.MiniV1;
			break;
		case "PlusV1": 
			mechanismType = DynaLinkHSPara.MechanismType.PlusV1;
			break;
		case "PlusV2": 
			mechanismType = DynaLinkHSPara.MechanismType.PlusV2;
			break;
		case "PlusV3": 
			mechanismType = DynaLinkHSPara.MechanismType.PlusV3;
			break;
		default:
			break;
		}
		
		DynaLinkHS.CmdSetRobotType (robotType);
		DynaLinkHS.CmdSetMechanismType (mechanismType);*/
    }


	void OnClickUpgrade()
	{
		UpgradePanelManager.gameObject.SetActive(true);
		MainPanel.gameObject.SetActive(false);
	}

    private void OnDestroy()
    {
        DynaLinkCore.StopSocket();
        Thread.Sleep(100);
    }

}
