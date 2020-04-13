using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2MotorInterfaceOperationMode
    {
        //-------------------------------- Motor Configuration (0x00) --------------------------------------

        SetMotorSelection = 0x70010101,

        //-------------------------------- Motor Configuration (0x00) --------------------------------------

        // basic motor configuration objects
        GetModeOfOperation = 0x01010101,
        SetModeOfOperation = 0x01010101,
        GetControlWord = 0x01010201,
        SetControlWord = 0x01010201,
        GetStatusWord = 0x01010301,
        SetFaultClear = 0x01010401,
        SetMotorEnable = 0x01010501,
        SetMotorDisable = 0x01010601,

        // basic motor information
        GetCurrent = 0x02010101,
        GetTorque = 0x02020101,
        GetVelocity = 0x02020201,
        GetPosition = 0x02020301,

        // home control
        SetHomeControlZeroHomePosition = 0x10010101,

        // torque control
        GetTorqueControlTorque = 0x11010101,
        SetTorqueControlTorque = 0x11010101,

        // velocity control
        GetVelocityControlAcceleration = 0x12010101,
        SetVelocityControlAcceleration = 0x12010101,
        GetVelocityControlDeceleration = 0x12010201,
        SetVelocityControlDeceleration = 0x12010201,
        GetVelocityControlVelocity = 0x12010301,
        SetVelocityControlVelocity = 0x12010301,

        // position control
        GetPositionControlAcceleration = 0x13010101,
        SetPositionControlAcceleration = 0x13010101,
        GetPositionControlDeceleration = 0x13010201,
        SetPositionControlDeceleration = 0x13010201,
        GetPositionControlVelocity = 0x13010301,
        SetPositionControlVelocity = 0x13010301,
        GetPositionControlPosition = 0x13010401,
        SetPositionControlPosition = 0x13010401,
        SetPositionControlAddNewAbsPoint = 0x13010501,

    }
}