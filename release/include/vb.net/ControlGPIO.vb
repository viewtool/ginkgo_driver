Option Strict Off
Option Explicit On

Imports System.Runtime.InteropServices
Module ControlGPIO
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
    '定义适配器类型
    Public Const VGI_USBGPIO As Int32 = 1
    'GPIO引脚定义
    Public Const VGI_GPIO_PIN0 As UInt16 = 1 << 0       'GPIO_0
    Public Const VGI_GPIO_PIN1 As UInt16 = 1 << 1       'GPIO_1
    Public Const VGI_GPIO_PIN2 As UInt16 = 1 << 2       'GPIO_2
    Public Const VGI_GPIO_PIN3 As UInt16 = 1 << 3       'GPIO_3
    Public Const VGI_GPIO_PIN4 As UInt16 = 1 << 4       'GPIO_4
    Public Const VGI_GPIO_PIN5 As UInt16 = 1 << 5       'GPIO_5
    Public Const VGI_GPIO_PIN6 As UInt16 = 1 << 6       'GPIO_6
    Public Const VGI_GPIO_PIN7 As UInt16 = 1 << 7       'GPIO_7
    Public Const VGI_GPIO_PIN8 As UInt16 = 1 << 8       'GPIO_8
    Public Const VGI_GPIO_PIN9 As UInt16 = 1 << 9       'GPIO_9
    Public Const VGI_GPIO_PIN10 As UInt16 = 1 << 10     'GPIO_10
    Public Const VGI_GPIO_PIN11 As UInt16 = 1 << 11     'GPIO_11
    Public Const VGI_GPIO_PIN12 As UInt16 = 1 << 12     'GPIO_12
    Public Const VGI_GPIO_PIN13 As UInt16 = 1 << 13     'GPIO_13
    Public Const VGI_GPIO_PIN14 As UInt16 = 1 << 14     'GPIO_14
    Public Const VGI_GPIO_PIN15 As UInt16 = 1 << 15     'GPIO_15
    Public Const VGI_GPIO_PIN_ALL As UInt16 = &HFFFF    'GPIO_ALL
    '设备操作相关函数
    Declare Function VGI_ScanDevice Lib "Ginkgo_Driver.dll" (ByVal NeedInit As Byte) As Int32
    Declare Function VGI_OpenDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Reserved As Int32) As Int32
    Declare Function VGI_CloseDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32) As Int32
    '获取引脚输入值
    Declare Function VGI_ReadDatas Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PinMask As UInt16, ByRef pData As UInt16) As Int32
    '引脚输出控制
    Declare Function VGI_SetPins Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Pins As UInt16) As Int32
    Declare Function VGI_ResetPins Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Pins As UInt16) As Int32
    '管脚方向和模式设置
    Declare Function VGI_SetInput Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Pins As UInt16) As Int32
    Declare Function VGI_SetOutput Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Pins As UInt16) As Int32
    Declare Function VGI_SetOpenDrain Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Pins As UInt16) As Int32

End Module
