Option Strict Off
Option Explicit On

Imports System.Runtime.InteropServices
Module ControlPWM
    '错误类型定义
    Public Const SUCCESS As Int32 = 0                   '没有错误
    Public Const PARAMETER_NULL As Int32 = -1           '传入的指针为空指针
    Public Const INPUT_DATA_TOO_MUCH As Int32 = -2      '参数输入个数多余规定个数
    Public Const INPUT_DATA_TOO_LESS As Int32 = -3      '参数输入个数少余规定个数
    Public Const INPUT_DATA_ILLEGALITY As Int32 = -4    '参数传入格式和规定的不符合
    Public Const USB_WRITE_DATA_ERROR As Int32 = -5     'USB写数据错误
    Public Const USB_READ_DATA_ERROR As Int32 = -6      'USB读数据错误
    Public Const READ_NO_DATA As Int32 = -7             '请求读数据时返回没有数据
    Public Const OPEN_DEVICE_FAILD As Int32 = -8        '打开设备失败
    Public Const CLOSE_DEVICE_FAILD As Int32 = -9       '关闭设备失败
    Public Const EXECUTE_CMD_FAILD As Int32 = -10       '设备执行命令失败
    Public Const SELECT_DEVICE_FAILD As Int32 = -11     '选择设备失败
    Public Const DEVICE_OPENED As Int32 = -12           '设备已经打开
    Public Const DEVICE_NOTOPEN As Int32 = -13          '设备没有打开
    Public Const BUFFER_OVERFLOW As Int32 = -14         '缓冲区溢出
    Public Const DEVICE_NOTEXIST As Int32 = -15         '此设备不存在
    Public Const LOAD_KERNELDLL As Int32 = -16          '装载动态库失败
    Public Const CMD_FAILED As Int32 = -17              '执行命令失败错误码
    Public Const BUFFER_CREATE As Int32 = -18           '内存不足
    'PWM通道定义
    Public Const VPI_PWM_CH0 As Byte = 1 << 0       'PWM_CH0
    Public Const VPI_PWM_CH1 As Byte = 1 << 1       'PWM_CH1
    Public Const VPI_PWM_CH2 As Byte = 1 << 2       'PWM_CH2
    Public Const VPI_PWM_CH3 As Byte = 1 << 3       'PWM_CH3
    Public Const VPI_PWM_CH4 As Byte = 1 << 4       'PWM_CH4
    Public Const VPI_PWM_CH5 As Byte = 1 << 5       'PWM_CH5
    Public Const VPI_PWM_CH6 As Byte = 1 << 6       'PWM_CH6
    Public Const VPI_PWM_CH7 As Byte = 1 << 7       'PWM_CH7
    Public Const VPI_PWM_ALL As Byte = &HFF         'ADC_ALL
    '定义适配器类型
    Public Const VPI_USBPWM As Int32 = 2
    '定义初始化PWM的数据类型
    Public Structure VII_INIT_CONFIG
        Dim PWM_ChannelMask As Byte 'PWM索引号,bit0对应通道0，bit7对应通道7，为1时有效
        Dim PWM_Mode As Byte        'PWM模式，0-模式0,1-模式1
        Dim PWM_Pulse As Byte       'PWM占空比,0到100
        Dim PWM_Polarity As Byte    'PWM输出极性，0-输出低脉冲，1-输出高脉冲
        Dim PWM_Frequency As UInt32 'PWM频率，1Hz到20000000Hz
    End Structure
    '函数声明
    Declare Function VPI_ScanDevice Lib "Ginkgo_Driver.dll" (ByVal NeedInit As Byte) As Int32
    Declare Function VPI_OpenDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Reserved As Int32) As Int32
    Declare Function VPI_CloseDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32) As Int32
    Declare Function VPI_InitPWM Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByRef pInitConfig As VII_INIT_CONFIG) As Int32
    Declare Function VPI_StartPWM Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal ChannelMask As Byte) As Int32
    Declare Function VPI_StopPWM Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal ChannelMask As Byte) As Int32
    Declare Function VPI_SetPWMPulse Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal ChannelMask As Byte, ByVal pPulse() As Byte) As Int32
    Declare Function VPI_SetPWMPeriod Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal ChannelMask As Byte, ByVal Frequency() As Int32) As Int32
End Module
