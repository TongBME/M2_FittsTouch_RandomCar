using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    enum FFTAICommunicationV2M2RobotInterfaceOperationMode
    {
        // flag
        FlagCalibration = 0x31010101,
        FlagEmergentStop = 0x31010102,
        FlagFault = 0x31010103,
        FlagServoOn = 0x31010104,

        FlagInformation = 0x31011101,

        FlagOutOfJointKineticLimit = 0x31010201,
        FlagOutOfJointAccelerationLimit = 0x31010202,
        FlagOutOfJointVelocityLimit = 0x31010203,
        FlagOutOfJointPositionLimit = 0x31010204,

        FlagOutOfEndEffectorKineticLimit = 0x31010211,
        FlagOutOfEndEffectorAccelerationLimit = 0x31010212,
        FlagOutOfEndEffectorVelocityLimit = 0x31010213,
        FlagOutOfEndEffectorPositionLimit = 0x31010214,

        FlagOutOfJointLimitInformation = 0x31011201,
        FlagOutOfEndEffectorLimitInformation = 0x31011202,

        // sensor : button
        EmergentStopButton  = 0x0A010101,

        ButtonSensorInformation = 0x0A011101,
        ButtonSensorInstalled = 0x0A011102,
        ButtonSensorAccessible = 0x0A011103,
        ButtonSensorCalibrate = 0x0A011104,

        // sensor : force sensor
        HorizontalForceSensorValue  = 0x0A010201,
        VerticalForceSensorValue    = 0x0A010202,

        ForceSensorInformation = 0x0A011201,
        ForceSensorInstalled = 0x0A011202,
        ForceSensorAccessible = 0x0A011203,
        ForceSensorCalibrate = 0x0A011204,

        // driver
        DriverInformation = 0x0B011101,
        DriverInstalled = 0x0B011102,
        DriverAccessible = 0x0B011103,

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
        JointLimitKinetic       = 0x03010101,
        JointLimitAcceleration  = 0x03010102,
        JointLimitVelocity      = 0x03010103,
        JointLimitPosition      = 0x03010104,

        CurrentValueAsJointLimitKinetic         = 0x03010201,
        CurrentValueAsJointLimitAcceleration    = 0x03010202,
        CurrentValueAsJointLimitVelocity        = 0x03010203,
        CurrentValueAsJointLimitPosition        = 0x03010204,

        EndEffectorLimitKinetic         = 0x03020101,
        EndEffectorLimitAcceleration    = 0x03020102,
        EndEffectorLimitVelocity        = 0x03020103,
        EndEffectorLimitPosition        = 0x03020104,

        CurrentValueAsEndEffectorLimitKinetic       = 0x03020201,
        CurrentValueAsEndEffectorLimitAcceleration  = 0x03020202,
        CurrentValueAsEndEffectorLimitVelocity      = 0x03020203,
        CurrentValueAsEndEffectorLimitPosition      = 0x03020204,

        // home control
        HomeControlZeroHomePosition     = 0x10010101,

        // joint control
        // joint control : torque control
        JointTorqueControlTorque        = 0x11010101,

        // joint control : velocity control
        JointVelocityControlAcceleration    = 0x12010101,
        JointVelocityControlDeceleration    = 0x12010201,
        JointVelocityControlVelocity        = 0x12010301,

        // joint control : position control
        JointPositionControlAcceleration    = 0x13010101,
        JointPositionControlDeceleration    = 0x13010201,
        JointPositionControlVelocity        = 0x13010301,
        JointPositionControlPosition        = 0x13010401,
        JointPositionControlAddNewPoint     = 0x13010501,

        // end effector control
        // end effector control : torque control
        EndEffectorTorqueControlTorque      = 0x21010101,

        // end effector control : velocity control
        EndEffectorVelocityControlAcceleration  = 0x22010101,
        EndEffectorVelocityControlDeceleration  = 0x22010201,
        EndEffectorVelocityControlVelocity      = 0x22010301,

        // end effector control : position control
        EndEffectorPositionControlAcceleration  = 0x23010101,
        EndEffectorPositionControlDeceleration  = 0x23010201,
        EndEffectorPositionControlVelocity      = 0x23010301,
        EndEffectorPositionControlPosition      = 0x23010401,
        EndEffectorPositionControlAddNewPoint   = 0x23010501,

    };
}
