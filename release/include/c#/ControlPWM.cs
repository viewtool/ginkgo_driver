using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ginkgo
{
    class ControlPWM
    {
        //初始化设备类型定义
        public struct VPI_INIT_CONFIG
        {
            public Byte PWM_ChannelMask;    //PWM索引号,bit0对应通道0，bit7对应通道7，为1时有效
            public Byte PWM_Mode;		    //PWM模式，0-模式0,1-模式1
            public Byte PWM_Pulse;		    //PWM占空比,0到100
            public Byte PWM_Polarity;	    //PWM输出极性，0-输出低脉冲，1-输出高脉冲
            public UInt32 PWM_Frequency;	//PWM频率，1Hz到20000000Hz
        }
        //PWM通道定义
        public struct PWM_MASK
        {
            public const Byte VPI_PWM_CH0 = 1 << 0;     //PWM_CH0
            public const Byte VPI_PWM_CH1 = 1 << 1;     //PWM_CH1
            public const Byte VPI_PWM_CH2 = 1 << 2;     //PWM_CH2
            public const Byte VPI_PWM_CH3 = 1 << 3;     //PWM_CH3
            public const Byte VPI_PWM_CH4 = 1 << 4;     //PWM_CH4
            public const Byte VPI_PWM_CH5 = 1 << 5;     //PWM_CH5
            public const Byte VPI_PWM_CH6 = 1 << 6;     //PWM_CH6
            public const Byte VPI_PWM_CH7 = 1 << 7;     //PWM_CH7
            public const Byte VPI_PWM_ALL = 0xFF;       //PWM_ALL
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
        public const Int32 VPI_USBPWM = 2;          //设备类型
        public static VPI_INIT_CONFIG PWM_Config;
        //设备支持的方法
        [DllImport("Ginkgo_Driver.dll")]
        //扫描设备
        public static extern int VPI_ScanDevice(Byte NeedInit = 1);
        [DllImport("Ginkgo_Driver.dll")]
        //打开设备
        public static extern int VPI_OpenDevice(Int32 DevType, Int32 DevIndex, Int32 Reserved);
        [DllImport("Ginkgo_Driver.dll")]
        //关闭设备
        public static extern int VPI_CloseDevice(Int32 DevType, Int32 DevIndex);
        [DllImport("Ginkgo_Driver.dll")]
        //初始化PWM模块
        public static extern int VPI_InitPWM(Int32 DevType, Int32 DevIndex, ref VPI_INIT_CONFIG pInitConfig);
        [DllImport("Ginkgo_Driver.dll")]
        //启动PWM
        public static extern int VPI_StartPWM(Int32 DevType, Int32 DevIndex, Byte ChannelMask);
        [DllImport("Ginkgo_Driver.dll")]
        //停止PWM
        public static extern int VPI_StopPWM(Int32 DevType, Int32 DevIndex, Byte ChannelMask);
        [DllImport("Ginkgo_Driver.dll")]
        //设置PWM占空比（在启动PWM后设置）
        public static extern int VPI_SetPWMPulse(Int32 DevType, Int32 DevIndex, Byte ChannelMask, Byte[] pPulse);
        [DllImport("Ginkgo_Driver.dll")]
        //设置PWM时钟频率（在启动PWM后设置）
        public static extern int VPI_SetPWMPeriod(Int32 DevType, Int32 DevIndex, Byte ChannelMask, Int32[] Frequency);
    }
}
