using FFTAICommunicationLib;
using UnityEngine;
using UnityEngine.UI;

public class FFTAICommunicationSettingPanel : MonoBehaviour
{
    public MainSysPanel MainPanel;
    public FFTAICommunicationSettingPanel fFTAICommunicationSettingPanel;

    public Button ReturnButton;

    public Text PositionDataJoint1;
    public Text PositionDataJoint2;
    public Text PositionDataJoint3;
    public Text PositionDataJoint4;
    public Text PositionDataJoint5;
    public Text PositionDataJoint6;

    public Text VelocityDataJoint1;
    public Text VelocityDataJoint2;
    public Text VelocityDataJoint3;
    public Text VelocityDataJoint4;
    public Text VelocityDataJoint5;
    public Text VelocityDataJoint6;

    public  Text AccelerationDataJoint1;
    public  Text AccelerationDataJoint2;
    public  Text AccelerationDataJoint3;
    public  Text AccelerationDataJoint4;
    public  Text AccelerationDataJoint5;
    public  Text AccelerationDataJoint6;

    public  Text KineticDataJoint1;
    public  Text KineticDataJoint2;
    public  Text KineticDataJoint3;
    public  Text KineticDataJoint4;
    public  Text KineticDataJoint5;
    public  Text KineticDataJoint6;

    public  Text PositionDataEndEffectorX1;
    public  Text PositionDataEndEffectorY1;
    public  Text PositionDataEndEffectorZ1;
    public  Text PositionDataEndEffectorAlpha1;
    public  Text PositionDataEndEffectorBeta1;
    public  Text PositionDataEndEffectorGamma1;

    public  Text VelocityDataEndEffectorX1;
    public  Text VelocityDataEndEffectorY1;
    public  Text VelocityDataEndEffectorZ1;
    public  Text VelocityDataEndEffectorAlpha1;
    public  Text VelocityDataEndEffectorBeta1;
    public  Text VelocityDataEndEffectorGamma1;

    public  Text AccelerationDataEndEffectorX1;
    public  Text AccelerationDataEndEffectorY1;
    public  Text AccelerationDataEndEffectorZ1;
    public  Text AccelerationDataEndEffectorAlpha1;
    public  Text AccelerationDataEndEffectorBeta1;
    public  Text AccelerationDataEndEffectorGamma1;

    public  Text KineticDataEndEffectorX1;
    public  Text KineticDataEndEffectorY1;
    public  Text KineticDataEndEffectorZ1;
    public  Text KineticDataEndEffectorAlpha1;
    public  Text KineticDataEndEffectorBeta1;
    public  Text KineticDataEndEffectorGamma1;

    public  Text PositionDataEndEffectorX2;
    public  Text PositionDataEndEffectorY2;
    public  Text PositionDataEndEffectorZ2;
    public  Text PositionDataEndEffectorAlpha2;
    public  Text PositionDataEndEffectorBeta2;
    public  Text PositionDataEndEffectorGamma2;

    public  Text VelocityDataEndEffectorX2;
    public  Text VelocityDataEndEffectorY2;
    public  Text VelocityDataEndEffectorZ2;
    public  Text VelocityDataEndEffectorAlpha2;
    public  Text VelocityDataEndEffectorBeta2;
    public  Text VelocityDataEndEffectorGamma2;

    public  Text AccelerationDataEndEffectorX2;
    public  Text AccelerationDataEndEffectorY2;
    public  Text AccelerationDataEndEffectorZ2;
    public  Text AccelerationDataEndEffectorAlpha2;
    public  Text AccelerationDataEndEffectorBeta2;
    public  Text AccelerationDataEndEffectorGamma2;

    public  Text KineticDataEndEffectorX2;
    public  Text KineticDataEndEffectorY2;
    public  Text KineticDataEndEffectorZ2;
    public  Text KineticDataEndEffectorAlpha2;
    public  Text KineticDataEndEffectorBeta2;
    public  Text KineticDataEndEffectorGamma2;

    public  Text AdcDataSensorensor1;
    public  Text AdcDataSensorensor2;
    public  Text AdcDataSensorensor3;
    public  Text AdcDataSensorensor4;
    public  Text AdcDataSensorensor5;
    public  Text AdcDataSensorensor6;

    private void Start()
    {
        ReturnButton.onClick.AddListener(OnClickReturnButton);
    }

    void OnClickReturnButton()
    {
        MainPanel.gameObject.SetActive(true);
        fFTAICommunicationSettingPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        PositionDataJoint1.text = DynaLinkHS.StatusRobot.PositionDataJoint1.ToString();
        PositionDataJoint2.text = DynaLinkHS.StatusRobot.PositionDataJoint2.ToString();
        PositionDataJoint3.text = DynaLinkHS.StatusRobot.PositionDataJoint3.ToString();
        PositionDataJoint4.text = DynaLinkHS.StatusRobot.PositionDataJoint4.ToString();
        PositionDataJoint5.text = DynaLinkHS.StatusRobot.PositionDataJoint5.ToString();
        PositionDataJoint6.text = DynaLinkHS.StatusRobot.PositionDataJoint6.ToString();

        VelocityDataJoint1.text = DynaLinkHS.StatusRobot.VelocityDataJoint1.ToString();
        VelocityDataJoint2.text = DynaLinkHS.StatusRobot.VelocityDataJoint2.ToString();
        VelocityDataJoint3.text = DynaLinkHS.StatusRobot.VelocityDataJoint3.ToString();
        VelocityDataJoint4.text = DynaLinkHS.StatusRobot.VelocityDataJoint4.ToString();
        VelocityDataJoint5.text = DynaLinkHS.StatusRobot.VelocityDataJoint5.ToString();
        VelocityDataJoint6.text = DynaLinkHS.StatusRobot.VelocityDataJoint6.ToString();

        AccelerationDataJoint1.text = DynaLinkHS.StatusRobot.AccelerationDataJoint1.ToString();
        AccelerationDataJoint2.text = DynaLinkHS.StatusRobot.AccelerationDataJoint2.ToString();
        AccelerationDataJoint3.text = DynaLinkHS.StatusRobot.AccelerationDataJoint3.ToString();
        AccelerationDataJoint4.text = DynaLinkHS.StatusRobot.AccelerationDataJoint4.ToString();
        AccelerationDataJoint5.text = DynaLinkHS.StatusRobot.AccelerationDataJoint5.ToString();
        AccelerationDataJoint6.text = DynaLinkHS.StatusRobot.AccelerationDataJoint6.ToString();

        KineticDataJoint1.text = DynaLinkHS.StatusRobot.KineticDataJoint1.ToString();
        KineticDataJoint2.text = DynaLinkHS.StatusRobot.KineticDataJoint2.ToString();
        KineticDataJoint3.text = DynaLinkHS.StatusRobot.KineticDataJoint3.ToString();
        KineticDataJoint4.text = DynaLinkHS.StatusRobot.KineticDataJoint4.ToString();
        KineticDataJoint5.text = DynaLinkHS.StatusRobot.KineticDataJoint5.ToString();
        KineticDataJoint6.text = DynaLinkHS.StatusRobot.KineticDataJoint6.ToString();

        PositionDataEndEffectorX1.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorX1.ToString();
        PositionDataEndEffectorY1.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorY1.ToString();
        PositionDataEndEffectorZ1.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorZ1.ToString();
        PositionDataEndEffectorAlpha1.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorAlpha1.ToString();
        PositionDataEndEffectorBeta1.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorBeta1.ToString();
        PositionDataEndEffectorGamma1.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorGamma1.ToString();

		VelocityDataEndEffectorX1.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorX1.ToString();
        VelocityDataEndEffectorY1.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorY1.ToString();
        VelocityDataEndEffectorZ1.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorZ1.ToString();
        VelocityDataEndEffectorAlpha1.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorAlpha1.ToString();
        VelocityDataEndEffectorBeta1.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorBeta1.ToString();
        VelocityDataEndEffectorGamma1.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorGamma1.ToString();

		AccelerationDataEndEffectorX1.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorX1.ToString();
        AccelerationDataEndEffectorY1.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorY1.ToString();
        AccelerationDataEndEffectorZ1.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorZ1.ToString();
        AccelerationDataEndEffectorAlpha1.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorAlpha1.ToString();
        AccelerationDataEndEffectorBeta1.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorBeta1.ToString();
        AccelerationDataEndEffectorGamma1.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorGamma1.ToString();

		KineticDataEndEffectorX1.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorX1.ToString();
        KineticDataEndEffectorY1.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorY1.ToString();
        KineticDataEndEffectorZ1.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorZ1.ToString();
        KineticDataEndEffectorAlpha1.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorAlpha1.ToString();
        KineticDataEndEffectorBeta1.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorBeta1.ToString();
        KineticDataEndEffectorGamma1.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorGamma1.ToString();

        PositionDataEndEffectorX2.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorX2.ToString();
        PositionDataEndEffectorY2.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorY2.ToString();
        PositionDataEndEffectorZ2.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorZ2.ToString();
        PositionDataEndEffectorAlpha2.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorAlpha2.ToString();
        PositionDataEndEffectorBeta2.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorBeta2.ToString();
        PositionDataEndEffectorGamma2.text = DynaLinkHS.StatusRobot.PositionDataEndEffectorGamma2.ToString();

        VelocityDataEndEffectorX2.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorX2.ToString();
        VelocityDataEndEffectorY2.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorY2.ToString();
        VelocityDataEndEffectorZ2.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorZ2.ToString();
        VelocityDataEndEffectorAlpha2.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorAlpha2.ToString();
        VelocityDataEndEffectorBeta2.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorBeta2.ToString();
        VelocityDataEndEffectorGamma2.text = DynaLinkHS.StatusRobot.VelocityDataEndEffectorGamma2.ToString();

        AccelerationDataEndEffectorX2.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorX2.ToString();
        AccelerationDataEndEffectorY2.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorY2.ToString();
        AccelerationDataEndEffectorZ2.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorZ2.ToString();
        AccelerationDataEndEffectorAlpha2.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorAlpha2.ToString();
        AccelerationDataEndEffectorBeta2.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorBeta2.ToString();
        AccelerationDataEndEffectorGamma2.text = DynaLinkHS.StatusRobot.AccelerationDataEndEffectorGamma2.ToString();

        KineticDataEndEffectorX2.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorX2.ToString();
        KineticDataEndEffectorY2.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorY2.ToString();
        KineticDataEndEffectorZ2.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorZ2.ToString();
        KineticDataEndEffectorAlpha2.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorAlpha2.ToString();
        KineticDataEndEffectorBeta2.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorBeta2.ToString();
        KineticDataEndEffectorGamma2.text = DynaLinkHS.StatusRobot.KineticDataEndEffectorGamma2.ToString();

		AdcDataSensorensor1.text = DynaLinkHS.StatusSensor.ADCSensor1.CalculateValue.ToString();
		AdcDataSensorensor2.text = DynaLinkHS.StatusSensor.ADCSensor2.CalculateValue.ToString();
		AdcDataSensorensor3.text = DynaLinkHS.StatusSensor.ADCSensor3.CalculateValue.ToString();
		AdcDataSensorensor4.text = DynaLinkHS.StatusSensor.ADCSensor4.CalculateValue.ToString();
		AdcDataSensorensor5.text = DynaLinkHS.StatusSensor.ADCSensor5.CalculateValue.ToString();
		AdcDataSensorensor6.text = DynaLinkHS.StatusSensor.ADCSensor6.CalculateValue.ToString();
    }


}
