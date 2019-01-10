using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ginkgo
{
    class ControlI2C
    {
        //初始化设备类型定义
        public struct VII_INIT_CONFIG
        {
            public Byte MasterMode;		//主从选择控制:0-从机，1-主机
            public Byte ControlMode;	//控制方式:0-硬件从机模式，1-硬件控制，2-软件控制
            public Byte AddrType;		//7-7bit模式，10-10bit模式
            public Byte SubAddrWidth;	//子地址宽度，0到4取值，0时表示无子地址模式
            public UInt16 Addr;			//从机模式时候的设备地址
            public UInt32 ClockSpeed;	//时钟频率:单位为HZ
        }
        //适配器信息的数据类型
        public struct VII_BOARD_INFO
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
        //软件I2C时间配置类型定义
        public struct VII_TIME_CONFIG
        {
            public UInt16 tHD_STA;   //起始信号保持时间
            public UInt16 tSU_STA;   //起始信号建立时间
            public UInt16 tLOW;      //时钟低电平时间
            public UInt16 tHIGH;     //时钟高电平时间
            public UInt16 tSU_DAT;   //数据输入建立时间
            public UInt16 tSU_STO;   //停止信号建立时间
            public UInt16 tDH;       //数据输出保持时间
            public UInt16 tDH_DAT;   //数据输入保持时间
            public UInt16 tAA;       //SCL变低至SDA数据输出及应答信号
            public UInt16 tR;        //SDA及SCL上升时间
            public UInt16 tF;        //SDA及SCL下降时间
            public UInt16 tBuf;      //新的发送开始前总线空闲时间
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Byte[] tACK;
            public UInt16 tStart;
            public UInt16 tStop;
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
        //设备类型及初始化数据定义
        public struct INIT_CONFIG
        {
            public const Int32 VII_USBI2C = 1;          //设备类型
            public const Byte VII_ADDR_7BIT = 7;        //7bit地址模式
            public const Byte VII_ADDR_10BIT = 10;      //10bit地址模式
            public const Byte VII_HCTL_MODE = 1;        //硬件控制
            public const Byte VII_SCTL_MODE = 2;        //软件控制
            public const Byte VII_MASTER = 1;           //主机
            public const Byte VII_SLAVE = 0;            //从机
            public const Byte VII_SUB_ADDR_NONE = 0;    //无子地址
            public const Byte VII_SUB_ADDR_1BYTE = 1;   //1Byte子地址
            public const Byte VII_SUB_ADDR_2BYTE = 2;   //2Byte子地址
            public const Byte VII_SUB_ADDR_3BYTE = 3;   //3Byte子地址
            public const Byte VII_SUB_ADDR_4BYTE = 4;   //4Byte子地址
        }

        public static VII_INIT_CONFIG I2C_Config;
        public static VII_TIME_CONFIG I2C_TimeConfig;
        public static VII_BOARD_INFO I2C_Info; 
        //设备支持的方法
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_ScanDevice(Byte NeedInit = 1);//获取dll中的函数 
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_OpenDevice(Int32 DevType, Int32 DevIndex, Int32 Reserved);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_CloseDevice(Int32 DevType, Int32 DevIndex);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_ReadBoardInfo(Int32 DevIndex, ref VII_BOARD_INFO pInfo);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_InitI2C(Int32 DevType, Int32 DevIndex, Int32 I2CIndex, ref VII_INIT_CONFIG pInitConfig);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_WriteBytes(Int32 DevType, Int32 DevIndex, Int32 I2CIndex, UInt16 Addr, UInt32 SubAddr, Byte[] pWriteData, UInt16 Len);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_ReadBytes(Int32 DevType, Int32 DevIndex, Int32 I2CIndex, UInt16 Addr, UInt32 SubAddr, Byte[] pReadData, UInt16 Len);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_TimeConfig(Int32 DevType, Int32 DevIndex, Int32 I2CIndex, ref VII_TIME_CONFIG pTimeConfig);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_SetUserKey(Int32 DevType, Int32 DevIndex, Byte[] pUserKey);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_CheckUserKey(Int32 DevType, Int32 DevIndex, Byte[] pUserKey);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_SlaveReadBytes(Int32 DevType, Int32 DevIndex, Int32 I2CIndex, Byte[] pReadData, ref UInt16 pLen);
        [DllImport("Ginkgo_Driver.dll")]
        public static extern int VII_SlaveWriteBytes(Int32 DevType, Int32 DevIndex, Int32 I2CIndex, Byte[] pWriteData, UInt16 Len);
    }
}
