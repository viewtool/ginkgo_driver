using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ginkgo
{
    class ControlSPI
    {
        //初始化设备类型定义
        public struct VSI_INIT_CONFIG
        {
            public Byte ControlMode;	//SPI控制方式:0-硬件控制（全双工模式）,1-硬件控制（半双工模式），2-软件控制（半双工模式）,3-单总线模式，数据线输入输出都为MOSI
            public Byte TranBits;		//数据传输字节宽度，在8~16之间取值
            public Byte MasterMode;		//主从选择控制:0-从机，1-主机
            public Byte CPOL;			//时钟极性控制:0-SCK高有效，1-SCK低有效
            public Byte CPHA;			//时钟相位控制:0-第一个SCK时钟采样，1-第二个SCK时钟采样
            public Byte LSBFirst;		//数据移位方式:0-MSB在前，1-LSB在前
            public Byte SelPolarity;	//片选信号极性:0-低电平选中，1-高电平选中
            public UInt32 ClockSpeed;	    //SPI时钟频率:单位为HZ
        }
        //适配器信息的数据类型
        public struct VSI_BOARD_INFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public Byte[] ProductName;      //适配器名称
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Byte[] FirmwareVersion;  //固件版本号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Byte[] HardwareVersion;  //硬件版本号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public Byte[] SerialNumber;     //适配器唯一序列号
        }
        //错误类型定义
        public struct ERROR
        {
            public const Int32 SUCCESS = 0;		            //没有错误
            public const Int32 PARAMETER_NULL = -1;		    //传入的指针为空指针
            public const Int32 INPUT_DATA_TOO_MUCH = -2;	//参数输入个数多余规定个数
            public const Int32 INPUT_DATA_TOO_LESS = -3;	//参数输入个数少余规定个数
            public const Int32 INPUT_DATA_ILLEGALITY = -4;	//参数传入格式和规定的不符合
            public const Int32 USB_WRITE_DATA_ERROR = -5;	//USB写数据错误
            public const Int32 USB_READ_DATA_ERROR = -6;	//USB读数据错误
            public const Int32 READ_NO_DATA = -7;		    //请求读数据时返回没有数据
            public const Int32 OPEN_DEVICE_FAILD = -8;		//打开设备失败
            public const Int32 CLOSE_DEVICE_FAILD = -9;		//关闭设备失败
            public const Int32 EXECUTE_CMD_FAILD = -10;		//设备执行命令失败
            public const Int32 SELECT_DEVICE_FAILD = -11;	//选择设备失败
            public const Int32 DEVICE_OPENED = -12;		    //设备已经打开
            public const Int32 DEVICE_NOTOPEN = -13;		//设备没有打开
            public const Int32 BUFFER_OVERFLOW = -14;		//缓冲区溢出
            public const Int32 DEVICE_NOTEXIST = -15;		//此设备不存在
            public const Int32 LOAD_KERNELDLL = -16;		//装载动态库失败
            public const Int32 CMD_FAILED = -17;		    //执行命令失败错误码
            public const Int32 BUFFER_CREATE = -18;		    //内存不足
        }
        //设备类型定义
        public const Int32 VSI_USBSPI = 2;
        public static VSI_INIT_CONFIG SPI_Config;
        //设备支持的方法
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_ScanDevice(Byte NeedInit = 1);//获取dll中的函数 
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_OpenDevice(Int32 DevType, Int32 DevIndex, Int32 Reserved);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_CloseDevice(Int32 DevType, Int32 DevIndex);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_ReadBoardInfo(Int32 DevIndex, ref VSI_BOARD_INFO pInfo);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_InitSPI(Int32 DevType, Int32 DevIndex, ref VSI_INIT_CONFIG pInitConfig);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_WriteBytes(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, Byte[] pData, UInt16 Len);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_ReadBytes(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, Byte[] pData, UInt16 Len);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_WriteReadBytes(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, Byte[] pWriteData, UInt16 WriteLen, Byte[] pReadData, UInt16 ReadLen);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_WriteBits(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, StringBuilder pWriteBitStr);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_ReadBits(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, StringBuilder pReadBitStr, Int32 ReadBitsNum);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_WriteReadBits(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, StringBuilder pWriteBitStr, StringBuilder pReadBitStr, Int32 ReadBitsNum);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_SlaveReadBytes(Int32 DevType, Int32 DevIndex, Byte[] pReadData, ref Int32 pBytesNum, Int32 WaitTime);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_SlaveWriteBytes(Int32 DevType, Int32 DevIndex, Byte[] pWriteData, Int32 WriteBytesNum);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_BlockWriteBytes(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, Byte[] pWriteData, UInt16 BlockSize, UInt16 BlockNum, UInt32 IntervalTime);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_BlockReadBytes(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, Byte[] pReadData, UInt16 BlockSize, UInt16 BlockNum, UInt32 IntervalTime);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VSI_BlockWriteReadBytes(Int32 DevType, Int32 DevIndex, Int32 SPIIndex, Byte[] pWriteData, UInt16 WriteBlockSize, Byte[] pReadData, UInt16 ReadBlockSize, UInt16 BlockNum, UInt32 IntervalTime);
    }
}
