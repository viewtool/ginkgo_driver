Option Strict Off
Option Explicit On

Imports System.Runtime.InteropServices
Module ControlPMBus
    '错误类型定义
    Public Const PMBUS_OK As Int32 = 0                          'Packet was sent correctly
    Public Const PMBUS_ERROR_SLAVE_NOT_SUPPORTED As Int32 = 1   'Slave mode is not supported 
    Public Const PMBUS_ERROR_BUSOFF As Int32 = 2
    Public Const PMBUS_ERROR_TXFULL As Int32 = 3
    Public Const PMBUS_ERROR_BUSY As Int32 = 4
    Public Const PMBUS_ERROR_RXEMPTY As Int32 = 5
    Public Const PMBUS_ERROR_OVERRUN As Int32 = 6
    Public Const PMBUS_ERROR_TIMEOUT As Int32 = 7               'Timeout occured during sending the packet 
    Public Const PMBUS_ERROR_INVALID_SIZE As Int32 = 8          'Invalid size of received packet
    Public Const PMBUS_ERROR_PACKET_TOO_LONG As Int32 = 9       'Packet to sent does not fit into internal buffer
    Public Const PMBUS_ERROR_PARAMETER As Int32 = 10            'Invalid parameter
    Public Const PMBUS_ERROR_PEC As Int32 = 11                  'PEC error
    Public Const PMBUS_ERROR_NACK As Int32 = 12                 'NACK error
    Public Const PMBUS_ERROR_ARLO As Int32 = 13                 'Arbitration lost (master mode)
    '函数定义
    Declare Function PMBus_ScanDevice Lib "Ginkgo_Driver.dll" (ByVal NeedInit As Byte) As Int32
    Declare Function PMBus_OpenDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal Reserved As Int32) As Int32
    Declare Function PMBus_CloseDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32) As Int32
    Declare Function PMBus_HardInit Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal ClockSpeed As Int32, ByVal OwnAddr As Byte) As Int32
    Declare Function PMBus_WriteByte Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal Data As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_ReadByte Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal pData() As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_SendByte Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_WriteWord Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal Data As UInt16, ByVal PEC As Byte) As Int32
    Declare Function PMBus_ReadWord Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal pData() As UInt16, ByVal PEC As Byte) As Int32
    Declare Function PMBus_WriteByteExt Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCodeExt As Byte, ByVal CommandCode As Byte, ByVal Data As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_ReadByteExt Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCodeExt As Byte, ByVal CommandCode As Byte, ByVal pData() As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_WriteWordExt Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCodeExt As Byte, ByVal CommandCode As Byte, ByVal Data As UInt16, ByVal PEC As Byte) As Int32
    Declare Function PMBus_ReadWordExt Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCodeExt As Byte, ByVal CommandCode As Byte, ByVal pData() As UInt16, ByVal PEC As Byte) As Int32
    Declare Function PMBus_BlockWrite Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal pData() As Byte, ByVal ByteCount As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_BlockRead Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal pData() As Byte, ByRef pByteCount As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_BlockProcessCall Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal SlaveAddr As Byte, ByVal CommandCode As Byte, ByVal pWriteData() As Byte, ByVal WriteByteCount As Byte, ByVal pReadData() As Byte, ByRef pReadByteCount As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_GroupCmd Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal pGroupCmdData() As Byte, ByVal CmdNum As Byte, ByVal PEC As Byte) As Int32
    Declare Function PMBus_GetAlert Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByRef pAlertFlag As Byte) As Int32
    Declare Function PMBus_SetControl Lib "Ginkgo_Driver.dll" (ByVal DevType As Int32, ByVal DevIndex As Int32, ByVal PMBusIndex As Int32, ByVal Value As Byte) As Int32


End Module
