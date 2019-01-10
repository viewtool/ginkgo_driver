using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ginkgo
{
    class ControlGPIO
    {
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
        //GPIO的引脚定义
        public struct GPIO_MASK
        {
            public const UInt16 VGI_GPIO_PIN0 = 1 << 0;     //GPIO_0
            public const UInt16 VGI_GPIO_PIN1 = 1 << 1;     //GPIO_1
            public const UInt16 VGI_GPIO_PIN2 = 1 << 2;     //GPIO_2
            public const UInt16 VGI_GPIO_PIN3 = 1 << 3;     //GPIO_3
            public const UInt16 VGI_GPIO_PIN4 = 1 << 4;     //GPIO_4
            public const UInt16 VGI_GPIO_PIN5 = 1 << 5;     //GPIO_5
            public const UInt16 VGI_GPIO_PIN6 = 1 << 6;     //GPIO_6
            public const UInt16 VGI_GPIO_PIN7 = 1 << 7;     //GPIO_7
            public const UInt16 VGI_GPIO_PIN8 = 1 << 8;     //GPIO_8
            public const UInt16 VGI_GPIO_PIN9 = 1 << 9;     //GPIO_9
            public const UInt16 VGI_GPIO_PIN10 = 1 << 10;   //GPIO_10
            public const UInt16 VGI_GPIO_PIN11 = 1 << 11;   //GPIO_11
            public const UInt16 VGI_GPIO_PIN12 = 1 << 12;   //GPIO_12
            public const UInt16 VGI_GPIO_PIN13 = 1 << 13;   //GPIO_13
            public const UInt16 VGI_GPIO_PIN14 = 1 << 14;   //GPIO_14
            public const UInt16 VGI_GPIO_PIN15 = 1 << 15;   //GPIO_15
            public const UInt16 VGI_GPIO_PIN_ALL = 0xFFFF;  //GPIO_ALL
        }
        //设备类型
        public const Int32 VGI_USBGPIO = 1;
        //设备支持的方法
        [DllImport("Ginkgo_Driver.dll")]
        //扫描设备（必须调用）
        public static extern int VGI_ScanDevice(Byte NeedInit = 1);
        [DllImport("Ginkgo_Driver.dll")]
        //打开设备（必须调用）
        public static extern int VGI_OpenDevice(Int32 DevType, Int32 DevIndex, Int32 Reserved);
        [DllImport("Ginkgo_Driver.dll")]
        //关闭设备
        public static extern int VGI_CloseDevice(Int32 DevType, Int32 DevIndex);
        [DllImport("Ginkgo_Driver.dll")]
        //将指定引脚设置为输入
        public static extern int VGI_SetInput(Int32 DevType, Int32 DevIndex, UInt16 Pins);
        [DllImport("Ginkgo_Driver.dll")]
        //将指定引脚设置为输出
        public static extern int VGI_SetOutput(Int32 DevType, Int32 DevIndex, UInt16 Pins);
        [DllImport("Ginkgo_Driver.dll")]
        //将引脚设置为开漏模式（可作双向口，需加上拉电阻）
        public static extern int VGI_SetOpenDrain(Int32 DevType, Int32 DevIndex, UInt16 Pins);
        [DllImport("Ginkgo_Driver.dll")]
        //将引脚输出高电平
        public static extern int VGI_SetPins(Int32 DevType, Int32 DevIndex, UInt16 Pins);
        [DllImport("Ginkgo_Driver.dll")]
        //将引脚输出低电平
        public static extern int VGI_ResetPins(Int32 DevType, Int32 DevIndex, UInt16 Pins);
        [DllImport("Ginkgo_Driver.dll")]
        //获取引脚电平状态
        public static extern int VGI_ReadDatas(Int32 DevType, Int32 DevIndex, UInt16 PinMask, ref UInt16 pData);
    }
}
