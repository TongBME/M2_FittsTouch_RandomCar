using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    enum FFTAICommunicationV2X1RobotInterfaceOperationMode
    {
        // flag
        FlagCalibration = 0x31010101,
        FlagEmergentStop = 0x31010102,
        FlagFault = 0x31010103,
        FlagServoOn = 0x31010104,

        FlagInformation = 0x31011101,

        // sensor : button sensor
        RedButtonValue = 0x0A010101,
        YellowButtonValue = 0x0A010102,
        GreenButtonValue = 0x0A010103,
        BlueButtonValue = 0x0A010104,

        ButtonSensorInformation = 0x0A011101,

        // sensor : force sensor
        LeftLegThighForceSensorValue = 0x0A010201,
        LeftLegCalfForceSensorValue = 0x0A010202,
        RightLegThighForceSensorValue = 0x0A010203,
        RightLegCalfForceSensorValue = 0x0A010204,

        ForceSensorInformation = 0x0A011201,

        // sensor : pressure sensor
        LeftLegFootPressureSensorValue = 0x0A010301,
        RightLegFootPressureSensorValue = 0x0A010302,

        FootPressureInformation = 0x0A011301,

        // basic motor configuration
        ModeOfOperation = 0x01010101,
        ControlWord = 0x01010102,
        StatusWord = 0x01010103,
        FaultClear = 0x01010104,
        MotorEnable = 0x01010105,
        MotorDisable = 0x01010106,

        // motor information
        MotorCurrent = 0x02010101,

        MotorInformation = 0x02011101,

        // joint information
        JointTorque = 0x02010201,
        JointVelocity = 0x02010202,
        JointPosition = 0x02010203,

        JointInformation = 0x02011201,

        // end effector information
        EndEffectorTorque = 0x02010301,
        EndEffectorVelocity = 0x02010302,
        EndEffectorPosition = 0x02010303,

        EndEffectorInformation = 0x02011301,

        // protection
        JointLimitKinetic = 0x03010101,
        JointLimitAcceleration = 0x03010102,
        JointLimitVelocity = 0x03010103,
        JointLimitPosition = 0x03010104,

        CurrentValueAsJointLimitKinetic = 0x03010201,
        CurrentValueAsJointLimitAcceleration = 0x03010202,
        CurrentValueAsJointLimitVelocity = 0x03010203,
        CurrentValueAsJointLimitPosition = 0x03010204,

        EndEffectorLimitKinetic = 0x03020101,
        EndEffectorLimitAcceleration = 0x03020102,
        EndEffectorLimitVelocity = 0x03020103,
        EndEffectorLimitPosition = 0x03020104,

        CurrentValueAsEndEffectorLimitKinetic = 0x03020201,
        CurrentValueAsEndEffectorLimitAcceleration = 0x03020202,
        CurrentValueAsEndEffectorLimitVelocity = 0x03020203,
        CurrentValueAsEndEffectorLimitPosition = 0x03020204,

        // home control
        HomeControlZeroHomePosition = 0x10010101,

        // joint torque control
        JointTorqueControlTorque    = 0x11010101,

        // joint velocity control
        JointVelocityControlAcceleration    = 0x12010101,
        JointVelocityControlDeceleration    = 0x12010201,
        JointVelocityControlVelocity        = 0x12010301,

        // joint position control
        JointPositionControlAcceleration    = 0x13010101,
        JointPositionControlDeceleration    = 0x13010201,
        JointPositionControlVelocity        = 0x13010301,
        JointPositionControlPosition        = 0x13010401,
        JointPositionControlAddNewPoint     = 0x13010501,

        // end effector torque control
        EndEffectorTorqueControlTorque      = 0x21010101,

        // end effector velocity control
        EndEffectorVelocityControlAcceleration  = 0x22010101,
        EndEffectorVelocityControlDeceleration  = 0x22010201,
        EndEffectorVelocityControlVelocity      = 0x22010301,

        // end effector position control
        EndEffectorPositionControlAcceleration  = 0x23010101,
        EndEffectorPositionControlDeceleration  = 0x23010201,
        EndEffectorPositionControlVelocity      = 0x23010301,
        EndEffectorPositionControlPosition      = 0x23010401,
        EndEffectorPositionControlAddNewPoint   = 0x23010501,

    };
}
