using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2M1TaskInterfaceOperationMode : uint
    {
        // Basic
        BasicWorkMode = 0x01010101,

        BasicFlagTaskInProcess = 0x01010201,
        BasicFlagInformation = 0x01011001,

        // Debug
        DebugSetWorkMode = 0x02010101,

        // relay
        // relay : basic : set work mode
        RelaySetWorkMode = 0x03010101,

        // master Control
        // master Control : basic : set work mode
        MasterControlSetWorkMode = 0x04010101,

        // subtask
        // subtask : basic
        // subtask : basic : joint kinetic control
        SubtaskBasicSetJointKineticControlKinetic = 0x10000101,

        // subtask : basic : joint velocity control
        SubtaskBasicSetJointVelocityControlAcceleration = 0x10000201,
        SubtaskBasicSetJointVelocityControlVelocity = 0x10000202,

        // subtask : basic : joint position control
        SubtaskBasicSetJointPositionControlAcceleration = 0x10000301,
        SubtaskBasicSetJointPositionControlVelocity = 0x10000302,
        SubtaskBasicSetJointPositionControlPosition = 0x10000303,

        // subtask : basic : end effector kinetic control
        SubtaskBasicSetEndEffectorKineticControlKinetic = 0x10000401,

        // subtask : basic : end effector velocity control
        SubtaskBasicSetEndEffectorVelocityControlAcceleration = 0x10000501,
        SubtaskBasicSetEndEffectorVelocityControlVelocity = 0x10000502,

        // subtask : basic : end effector position control
        SubtaskBasicSetEndEffectorPositionControlAcceleration = 0x10000601,
        SubtaskBasicSetEndEffectorPositionControlVelocity = 0x10000602,
        SubtaskBasicSetEndEffectorPositionControlPosition = 0x10000603,

        // subtask : basic : find home
        SubtaskBasicSetFindHomeRequestKinetic = 0x10010101,
        SubtaskBasicSetFindHomeAcceleration = 0x10010102,
        SubtaskBasicSetFindHomeVelocity = 0x10010103,

        // subtask : basic : passive linear motion
        SubtaskBasicSetPassiveLinearMotionAcceleration = 0x10010201,
        SubtaskBasicSetPassiveLinearMotionVelocity = 0x10010202,
        SubtaskBasicSetPassiveLinearMotionPosition = 0x10010203,

        // subtask : basic : mass simulation
        SubtaskBasicSetMassSimulationMass = 0x10010301,
        SubtaskBasicSetMassSimulationFrictionFactor = 0x10010302,

		// subtask : basic : force assist without sensor
		SubtaskBasicSetForceAssistWithoutSensorForce = 0x10010401,

		// subtask : basic : force assist with sensor
		SubtaskBasicSetForceAssistWithSensorForce = 0x10010402,

        // subtask : basic : force assist with target position
        SubtaskBasicSetForceAssistWithTargetPositionForce = 0x10010501,
        SubtaskBasicSetForceAssistWithTargetPositionPosition = 0x10010502,

        // subtask : basic : force resist without sensor
        SubtaskBasicSetForceResistWithoutSensorForce = 0x10010601,

		// subtask : basic : force resist with sensor
		SubtaskBasicSetForceResistWithSensorForce = 0x10010602,

        // subtask : basic : transparent control
        SubtaskBasicSetTransparentControlOriginPosition = 0x10010701,
        SubtaskBasicSetTransparentControlM = 0x10010702,
        SubtaskBasicSetTransparentControlB = 0x10010703,
        SubtaskBasicSetTransparentControlK = 0x10010704,

        // subtask : basic : kinetic control with sensor
        SubtaskBasicSetKineticControlWithSensorForce = 0x10010802,

        // subtask : basic : transparent control with limit spring force
        SubtaskBasicSetTransparentControlWithLimitSpringForceOriginPoint = 0x10010901,
        SubtaskBasicSetTransparentControlWithLimitSpringForceM = 0x10010902,
        SubtaskBasicSetTransparentControlWithLimitSpringForceB = 0x10010903,
        SubtaskBasicSetTransparentControlWithLimitSpringForceK = 0x10010904,
        SubtaskBasicSetTransparentControlWithLimitSpringForceLimitSpringForce = 0x10010905,
    };

    public enum FFTAICommunicationV2M1TaskInterfaceWorkMode
    {
        Debug = 0x01,
        Relay = 0x02,
        MasterControl = 0x03,
    };

    public enum FFTAICommunicationV2M1TaskInterfaceDebugWorkMode
    {
        None = 0x00,

        JointKineticControl = 0x0101,
        JointVelocityControl = 0x0102,
        JointPositionControl = 0x0103,

        EndEffectorKineticControl = 0x0201,
        EndEffectorVelocityControl = 0x0202,
        EndEffectorPositionControl = 0x0203,
    }

    public enum FFTAICommunicationV2M1TaskInterfaceRelayWorkMode
    {
        None = 0x00,

		FindHome = 0x0101,
		ClearAlarm = 0x0102,
		ClearFault = 0x0103,
		ServoOn = 0x0104,
		ServoOff = 0x0105,
		PauseMotion = 0x0106,

		PassiveLinearMotion = 0x0201,

		MassSimulation = 0x0301,

		ForceAssistWithoutSensor = 0x0401,
		ForceAssistWithSensor = 0x0402,

		ForceAssistWithTargetPosition = 0x0501,

		ForceResistWithoutSensor = 0x0601,
		ForceResistWithSensor = 0x0602,

		TransparentControl = 0x0701,
        TunnelGuidanceControl = 0x0702,
        TransparentControlWithLimitSpringForce = 0x0703,

		DrawInfinityCurve = 0x0801,
		DrawCircleCurve = 0x0802,

        KineticControlWithSensor = 0x0901,
    }

    public enum FFTAICommunicationV2M1TaskInterfaceMasterControlWorkMode
    {
        None = 0x00,
    }

}
