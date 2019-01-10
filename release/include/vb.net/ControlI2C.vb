Option Strict Off
Option Explicit On

Imports System.Runtime.InteropServices
Module ControlI2C
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
    Public Const VII_USBI2C As Int32 = 1
    '适配器初始化数据定义
    Public Const VII_ADDR_7BIT As Byte = 7
    Public Const VII_ADDR_10BIT As Byte = 10
    Public Const VII_HCTL_MODE As Byte = 1
    Public Const VII_SCTL_MODE As Byte = 2
    Public Const VII_MASTER As Byte = 1
    Public Const VII_SLAVE As Byte = 0
    Public Const VII_SUB_ADDR_NONE As Byte = 0
    Public Const VII_SUB_ADDR_1BYTE As Byte = 1
    Public Const VII_SUB_ADDR_2BYTE As Byte = 2
    Public Const VII_SUB_ADDR_3BYTE As Byte = 3
    Public Const VII_SUB_ADDR_4BYTE As Byte = 4
    '定义初始化I2C的数据类型
    Public Structure VII_INIT_CONFIG
        Dim MasterMode As Byte      '主从选择控制:0-从机，1-主机
        Dim ControlMode As Byte     '控制方式:1-硬件控制，2-软件控制
        Dim AddrType As Byte        '7-7bit模式，10-10bit模式
        Dim SubAddrWidth As Byte    '子地址宽度，0到4取值，0时表示无子地址模式
        Dim Addr As UInt16          '从机模式时候的设备地址,最低位为读写位，设置为0
        Dim ClockSpeed As UInt32    '时钟频率:单位为HZ
    End Structure
    'Ginkgo系列适配器信息的数据类型
    Public Structure VII_BOARD_INFO
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Dim ProductName As Byte()        '硬件名称
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim FirmwareVersion As Byte()    '固件版本
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim HardwareVersion As Byte()    '硬件版本
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=12)> _
        Dim SerialNumber As Byte()       '适配器序列号
    End Structure
    '软件I2C时间参数定义，时间单位为微秒
    Public Structure VII_TIME_CONFIG
        Dim tHD_STA As UInt16   '起始信号保持时间
        Dim tSU_STA As UInt16   '起始信号建立时间
        Dim tLOW As UInt16      '时钟低电平时间
        Dim tHIGH As UInt16     '时钟高电平时间
        Dim tSU_DAT As UInt16   '数据输入建立时间
        Dim tSU_STO As UInt16   '停止信号建立时间
        Dim tDH As UInt16       '数据输出保持时间
        Dim tDH_DAT As UInt16   '数据输入保持时间
        Dim tAA As UInt16       'SCL变低至SDA数据输出及应答信号
        Dim tR As UInt16        'SDA及SCL上升时间
        Dim tF As UInt16        'SDA及SCL下降时间
        Dim tBuf As UInt16      '新的发送开始前总线空闲时间
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Dim tACK As Byte()
        Dim tStart As UInt16
        Dim tStop As UInt16
    End Structure
    '函数声明
    Declare Function VII_ScanDevice Lib "Ginkgo_Driver.dll" (ByVal NeedInit As Byte) As Int32
    Declare Function VII_OpenDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Reserved As Int32) As Int32
    Declare Function VII_CloseDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32) As Int32
    Declare Function VII_ReadBoardInfo Lib "Ginkgo_Driver.dll" (ByVal DevIndex As Int32, ByRef pInfo As VII_BOARD_INFO) As Int32
    Declare Function VII_TimeConfig Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal I2CIndex As Int32, ByRef pTimeConfig As VII_TIME_CONFIG) As Int32
    Declare Function VII_InitI2C Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal I2CIndex As Int32, ByRef pInitConfig As VII_INIT_CONFIG) As Int32
    Declare Function VII_WriteBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal I2CIndex As Int32, ByVal Addr As UInt16, ByVal SubAddr As UInt32, ByRef pWriteData As Byte, ByVal Len As UInt16) As Int32
    Declare Function VII_ReadBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal I2CIndex As Int32, ByVal Addr As UInt16, ByVal SubAddr As UInt32, ByRef pReadData As Byte, ByVal Len As UInt16) As Int32
    Declare Function VII_SlaveReadBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal I2CIndex As Int32, ByRef pReadData As Byte, ByRef pLen As UInt16) As Int32
    Declare Function VII_SlaveWriteBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal I2CIndex As Int32, ByRef pWriteData As Byte, ByVal Len As UInt16) As Int32
    Declare Function VII_SetUserKey Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByRef pUserKey As Byte) As Int32
    Declare Function VII_CheckUserKey Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByRef pUserKey As Byte) As Int32

End Module
