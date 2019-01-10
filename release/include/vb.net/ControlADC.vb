Option Strict Off
Option Explicit On

Imports System.Runtime.InteropServices
Module ControlADC
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
    Public Const VAI_USBADC As Int32 = 1
    'ADC通道定义
    Public Const VAI_ADC_CH0 As Byte = 1 << 0       'ADC_CH0
    Public Const VAI_ADC_CH1 As Byte = 1 << 1       'ADC_CH1
    Public Const VAI_ADC_CH2 As Byte = 1 << 2       'ADC_CH2
    Public Const VAI_ADC_CH3 As Byte = 1 << 3       'ADC_CH3
    Public Const VAI_ADC_CH4 As Byte = 1 << 4       'ADC_CH4
    Public Const VAI_ADC_CH5 As Byte = 1 << 5       'ADC_CH5
    Public Const VAI_ADC_CH6 As Byte = 1 << 6       'ADC_CH6
    Public Const VAI_ADC_CH7 As Byte = 1 << 7       'ADC_CH7
    Public Const VAI_ADC_ALL As Byte = &HFF         'ADC_ALL
    '设备操作相关函数
    Declare Function VAI_ScanDevice Lib "Ginkgo_Driver.dll" (ByVal NeedInit As Byte) As Int32
    Declare Function VAI_OpenDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Reserved As Int32) As Int32
    Declare Function VAI_CloseDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32) As Int32
    '初始化ADC模块
    Declare Function VAI_InitADC Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Channel As Byte, ByVal Period As UInt16) As Int32
    '获取ADC数据
    Declare Function VAI_ReadDatas Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal DataNum As UInt16, ByVal pData() As UInt16) As Int32

End Module
