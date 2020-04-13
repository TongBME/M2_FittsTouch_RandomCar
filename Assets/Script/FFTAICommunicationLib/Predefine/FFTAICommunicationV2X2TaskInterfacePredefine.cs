using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2X2TaskInterfaceOperationMode : uint
    {
        // basic
        // basic : set work mode
        BasicWorkMode = 0x01010101,

        BasicFlagTaskInProcess = 0x01010201,
        BasicFlagInformation = 0x01011001,

        // debug
        // debug : basic : set work mode
        DebugSetWorkMode = 0x02010101,

        // relay
        // relay : basic : set work mode
        RelayControlSetWorkMode = 0x03010101,

        // master control
        // master control : basic : set work mode
        MasterControlSetWorkMode = 0x04010101,

        MasterControlSetWalkPassive1Command = 0x04110101,
        MasterControlSetWalkPassive2Command = 0x04110201,
        MasterControlSetWalkPassive3Command = 0x04110301,
        MasterControlSetWalkPassive4Command = 0x04110401,

        MasterControlSetWalkActive1Command = 0x04120101,
        MasterControlSetWalkActive2Command = 0x04120201,

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

        // subtask : basic : passive move
        SubtaskBasicSetPassiveMoveAcceleration = 0x10010201,
        SubtaskBasicSetPassiveMoveVelocity = 0x10010202,
        SubtaskBasicSetPassiveMovePosition = 0x10010203,

        // subtask : basic : assist move
        SubtaskBasicSetAssistMoveKinetic = 0x10010301,
        SubtaskBasicSetAssistMovePosition = 0x10010302,

        // subtask : basic : active move
        SubtaskBasicSetActiveMoveKinetic = 0x10010401,

        // subtask : basic : transparent control
        SubtaskBasicSetTransparentControlOriginPoint = 0x10010501,
        SubtaskBasicSetTransparentControlM = 0x10010502,
        SubtaskBasicSetTransparentControlB = 0x10010503,
        SubtaskBasicSetTransparentControlK = 0x10010504,
    };

    public enum FFTAICommunicationV2X2TaskInterfaceWorkMode
    {
        None = 0x00,
        Debug = 0x01,
        Relay = 0x02,
        MasterControl = 0x03,
    };

    public enum FFTAICommunicationV2X2TaskInterfaceDebugWorkMode
    {
        None = 0x00,

        JointKineticControl = 0x0101,
        JointVelocityControl = 0x0102,
        JointPositionControl = 0x0103,

        EndEffectorKineticControl = 0x0201,
        EndEffectorVelocityControl = 0x0202,
        EndEffectorPositionControl = 0x0203,
    }

    public enum FFTAICommunicationV2X2TaskInterfaceRelayWorkMode
    {
        None = 0x000000,

        ClearAlarm = 0x000102,
        ClearFault = 0x000103,
        ServoOn = 0x000104,
        ServoOff = 0x000105,
        PauseMotion = 0x000106,

        PassiveMove = 0x010101,

        AssistMove = 0x020101,

        ActiveMove = 0x030101,

        TransparentControlMove = 0x040101,


    }

    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWorkMode
    {
        None = 0x00000000,

        WalkPassive1 = 0x01010101,
        WalkPassive2 = 0x01010201,

        WalkAssist1 = 0x01020101,
        WalkAssist2 = 0x01020201,

        WalkActive1 = 0x01030101,
        WalkActive2 = 0x01030201,
    }

    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWalkPassive1Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Sit = 0x02,
        Stand = 0x03,
        Walk = 0x04,
    }

    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWalkPassive2Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Sit = 0x02,
        Stand = 0x03,
        LLF = 0x04,
        RLF = 0x05,
    }

    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWalkPassive3Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Sit = 0x02,
        Stand = 0x03,
        LLF = 0x04,
        RLF = 0x05,
    }

    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWalkPassive4Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Sit = 0x02,
        Stand = 0x03,
        Walk = 0x04,
    }
    
    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWalkActive1Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Walk = 0x04,
    }

    public enum FFTAICommunicationV2X2TaskInterfaceMasterControlWalkActive2Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Walk = 0x04,
    }
}
