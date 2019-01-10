using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ginkgo
{
    class ControlADC
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
        //ADC通道定义
        public struct ADC_MASK
        {
            public const Byte VAI_ADC_CH0 = 1 << 0;     //ADC_CH0
            public const Byte VAI_ADC_CH1 = 1 << 1;     //ADC_CH1
            public const Byte VAI_ADC_CH2 = 1 << 2;     //ADC_CH2
            public const Byte VAI_ADC_CH3 = 1 << 3;     //ADC_CH3
            public const Byte VAI_ADC_CH4 = 1 << 4;     //ADC_CH4
            public const Byte VAI_ADC_CH5 = 1 << 5;     //ADC_CH5
            public const Byte VAI_ADC_CH6 = 1 << 6;     //ADC_CH6
            public const Byte VAI_ADC_CH7 = 1 << 7;     //ADC_CH7
            public const Byte VAI_ADC_ALL = 0XFF;       //ADC_ALL
        }
        //设备类型
        public const Int32 VAI_USBADC = 1;
        //设备支持的方法
        [DllImport("Ginkgo_Driver.dll")]
        //扫描设备（必须调用）
        public static extern int VAI_ScanDevice(Byte NeedInit = 1);
        [DllImport("Ginkgo_Driver.dll")]
        //打开设备（必须调用）
        public static extern int VAI_OpenDevice(Int32 DevType, Int32 DevIndex, Int32 Reserved);
        [DllImport("Ginkgo_Driver.dll")]
        //关闭设备
        public static extern int VAI_CloseDevice(Int32 DevType, Int32 DevIndex);
        [DllImport("Ginkgo_Driver.dll")]
        //初始化ADC模块
        public static extern int VAI_InitADC(Int32 DevType, Int32 DevIndex, Byte Channel, UInt16 Period);
        [DllImport("Ginkgo_Driver.dll")]
        //获取ADC数据
        public static extern int VAI_ReadDatas(Int32 DevType, Int32 DevIndex, UInt16 DataNum, UInt16[] pData);
    }  
}
