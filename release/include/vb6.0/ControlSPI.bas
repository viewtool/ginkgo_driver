Attribute VB_Name = "ControlSPI"
'Error Type
Public Const SUCCESS As Long = 0
Public Const PARAMETER_NULL As Long = -1
Public Const INPUT_DATA_TOO_MUCH As Long = -2
Public Const INPUT_DATA_TOO_LESS As Long = -3
Public Const INPUT_DATA_ILLEGALITY As Long = -4
Public Const USB_WRITE_DATA_ERROR As Long = -5
Public Const USB_READ_DATA_ERROR As Long = -6
Public Const READ_NO_DATA As Long = -7
Public Const OPEN_DEVICE_FAILD As Long = -8
Public Const CLOSE_DEVICE_FAILD As Long = -9
Public Const EXECUTE_CMD_FAILD As Long = -10
Public Const SELECT_DEVICE_FAILD As Long = -11
Public Const DEVICE_OPENED As Long = -12
Public Const DEVICE_NOTOPEN As Long = -13
Public Const BUFFER_OVERFLOW As Long = -14
Public Const DEVICE_NOTEXIST As Long = -15
Public Const LOAD_KERNELDLL As Long = -16
Public Const CMD_FAILED As Long = -17
Public Const BUFFER_CREATE As Long = -18

Public Const VSI_USBSPI  As Long = 2

Public Type VSI_INIT_CONFIG
    ControlMode As Byte
    TranBits As Byte
    MasterMode As Byte
    CPOL As Byte
    CPHA As Byte
    LSBFirst As Byte
    SelPolarity As Byte
    ClockSpeed As Long
End Type

Public Type VSI_BOARD_INFO
    ProductName(31) As Byte
    FirmwareVersion(3) As Byte
    HardwareVersion(3) As Byte
    SerialNumber(11) As Byte
End Type

Declare Function VSI_ScanDevice Lib "Ginkgo_Driver.dll" (ByVal NeedInit As Byte) As Long
Declare Function VSI_OpenDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal Reserved As Long) As Long
Declare Function VSI_CloseDevice Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long) As Long
Declare Function VSI_ReadBoardInfo Lib "Ginkgo_Driver.dll" (ByVal DevIndex As Long, ByRef pInfo As VSI_BOARD_INFO) As Long
Declare Function VSI_InitSPI Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByRef pInitConfig As VSI_INIT_CONFIG) As Long
Declare Function VSI_WriteBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal SPIIndex As Long, ByRef pWriteData As Byte, ByVal WriteLen As Integer) As Long
Declare Function VSI_ReadBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal SPIIndex As Long, ByRef pReadData As Byte, ByVal ReadLen As Integer) As Long
Declare Function VSI_WriteReadBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal SPIIndex As Long, ByRef pWriteData As Byte, ByVal WriteLen As Integer, ByRef pReadData As Byte, ByVal ReadLen As Integer) As Long

Declare Function VSI_WriteBits Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal SPIIndex As Long, ByRef pWriteBitStr As Byte) As Long
Declare Function VSI_ReadBits Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal SPIIndex As Long, ByRef pReadBitStr As Byte, ByVal ReadBitsNum As Long) As Long
Declare Function VSI_WriteReadBits Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByVal SPIIndex As Long, ByRef pWriteBitStr As Byte, ByRef pReadBitStr As Byte, ByVal ReadBitsNum As Long) As Long

Declare Function VSI_SlaveReadBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByRef pReadData As Byte, ByRef pBytesNum As Long, ByVal WaitTime As Long) As Long
Declare Function VSI_SlaveWriteBytes Lib "Ginkgo_Driver.dll" (ByVal DevType As Long, ByVal DevIndex As Long, ByRef pWriteData As Byte, ByVal WriteBytesNum As Long) As Long


