using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ginkgo
{
    class ControlSMBus
    {
        //错误类型定义
        public struct ERROR
        {
            public const Int32 PMBUS_OK = 0x00;    // Packet was sent correctly
            public const Int32 PMBUS_ERROR_SLAVE_NOT_SUPPORTED = 0x01;    // Slave mode is not supported 
            public const Int32 PMBUS_ERROR_BUSOFF = 0x02;
            public const Int32 PMBUS_ERROR_TXFULL = 0x03;
            public const Int32 PMBUS_ERROR_BUSY = 0x04;
            public const Int32 PMBUS_ERROR_RXEMPTY = 0x05;
            public const Int32 PMBUS_ERROR_OVERRUN = 0x06;
            public const Int32 PMBUS_ERROR_TIMEOUT = 0x07;    // Timeout occured during sending the packet 
            public const Int32 PMBUS_ERROR_INVALID_SIZE = 0x08;    // Invalid size of received packet
            public const Int32 PMBUS_ERROR_PACKET_TOO_LONG = 0x09;    // Packet to sent does not fit into internal buffer
            public const Int32 PMBUS_ERROR_PARAMETER = 0x0A;    // Invalid parameter
            public const Int32 PMBUS_ERROR_PEC = 0x0B;    // PEC error
            public const Int32 PMBUS_ERROR_NACK = 0x0C;	  // NACK error
            public const Int32 PMBUS_ERROR_ARLO = 0x0D;	  // Arbitration lost (master mode)
        }

        //设备支持的方法
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 SMBus_ScanDevice(Byte NeedInit = 1);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_ScanDevice(Byte NeedInit);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_OpenDevice(Int32 DevType, Int32 DevIndex, Int32 Reserved);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_CloseDevice(Int32 DevType, Int32 DevIndex);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_HardInit(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, UInt32 ClockSpeed, Byte OwnAddr);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_WriteByte(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, Byte Data, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_ReadByte(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, Byte[] pData, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_SendByte(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_WriteWord(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, UInt16 Data, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_ReadWord(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, UInt16[] pData, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_WriteByteExt(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCodeExt, Byte CommandCode, Byte Data, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_ReadByteExt(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCodeExt, Byte CommandCode, Byte[] pData, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_WriteWordExt(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCodeExt, Byte CommandCode, UInt16 Data, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_ReadWordExt(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCodeExt, Byte CommandCode, UInt16[] pData, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_BlockWrite(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, Byte[] pData, Byte ByteCount, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_BlockRead(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, Byte[] pData, ref Byte pByteCount, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_BlockProcessCall(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte SlaveAddr, Byte CommandCode, Byte[] pWriteData, Byte WriteByteCount, Byte[] pReadData, ref Byte pReadByteCount, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_GroupCmd(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte[] pGroupCmdData, Byte CmdNum, Byte PEC);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_GetAlert(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte[] pAlertFlag);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern Int32 PMBus_SetControl(Int32 DevType, Int32 DevIndex, Int32 PMBusIndex, Byte Value);

    }
}
