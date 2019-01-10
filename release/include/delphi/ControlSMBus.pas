unit ControlSMBus;

interface
const
//Device type definition
VAI_USBADC	=	1;
VCI_USBCAN1	=	3;
VCI_USBCAN2	=	4;
VGI_USBGPIO	=	1;
VII_USBI2C	=	1;
VSI_USBSPI	=	2;
//Error type
SMBUS_OK = 0;
SMBUS_ERROR_SLAVE_NOT_SUPPORTED = 1;//Packet was sent correctly
SMBUS_ERROR_BUSOFF = 2;             //Slave mode is not supported
SMBUS_ERROR_TXFULL = 3;
SMBUS_ERROR_BUSY = 4;
SMBUS_ERROR_RXEMPTY = 5;
SMBUS_ERROR_OVERRUN = 6;
SMBUS_ERROR_TIMEOUT = 7;            //Timeout occured during sending the packet
SMBUS_ERROR_INVALID_SIZE = 8;       //Invalid size of received packet
SMBUS_ERROR_PACKET_TOO_LONG = 9;    //Packet to sent does not fit into internal buffer
SMBUS_ERROR_PARAMETER = 10;         //Invalid parameter
SMBUS_ERROR_PEC = 11;               //PEC error
SMBUS_ERROR_NACK = 12;              //NACK error
SMBUS_ERROR_ARLO = 13;              //Arbitration lost (master mode)

function SMBus_ScanDevice(NeedInit:Byte):Integer; stdcall;
function SMBus_OpenDevice(DevType,DevIndex,Reserved:Integer):Integer; stdcall;
function SMBus_CloseDevice(DevType,DevIndex:Integer):Integer; stdcall;
function SMBus_HardInit(DevType,DevIndex,SMBUSIndex,ClockSpeed:Integer; OwnAddr:Byte):Integer; stdcall;
function SMBus_QuickCommand(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr:Byte):Integer; stdcall;
function SMBus_WriteByte(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,Data,PEC:Byte):Integer; stdcall;
function SMBus_ReadByte(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr:Byte;pData:PByte;PEC:Byte):Integer; stdcall;
function SMBus_WriteByteProtocol(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode,Data,PEC:Byte):Integer; stdcall;
function SMBus_WriteWordProtocol(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte;Data:Word;PEC:Byte):Integer; stdcall;
function SMBus_ReadByteProtocol(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte; pData:PByte;PEC:Byte):Integer; stdcall;
function SMBus_ReadWordProtocol(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte; pData:PWord;PEC:Byte):Integer; stdcall;
function SMBus_ProcessCall(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte;WriteData:Word; pReadData:PWord;PEC:Byte):Integer; stdcall;
function SMBus_BlockWrite(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte;pData:PByte;ByteCount,PEC:Byte):Integer; stdcall;
function SMBus_BlockRead(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte;pData,pByteCount:PByte;PEC:Byte):Integer; stdcall;
function SMBus_BlockProcessCall(DevType,DevIndex,SMBUSIndex:Integer;SlaveAddr,CommandCode:Byte;pWriteData:PByte;WriteByteCount:Byte;pReadData:PByte;pReadByteCount:PByte;PEC:Byte):Integer; stdcall;
function SMBus_GetAlert(DevType,DevIndex,SMBUSIndex:Integer;pAlertFlag:PByte):Integer; stdcall;

implementation
function SMBus_ScanDevice;external 'Ginkgo_Driver.dll' name 'SMBus_ScanDevice';
function SMBus_OpenDevice;external 'Ginkgo_Driver.dll' name 'SMBus_OpenDevice';
function SMBus_CloseDevice;external 'Ginkgo_Driver.dll' name 'SMBus_CloseDevice';
function SMBus_HardInit;external 'Ginkgo_Driver.dll' name 'SMBus_HardInit';
function SMBus_QuickCommand;external 'Ginkgo_Driver.dll' name 'SMBus_QuickCommand';
function SMBus_WriteByte;external 'Ginkgo_Driver.dll' name 'SMBus_WriteByte';
function SMBus_ReadByte;external 'Ginkgo_Driver.dll' name 'SMBus_ReadByte';
function SMBus_WriteByteProtocol;external 'Ginkgo_Driver.dll' name 'SMBus_WriteByteProtocol';
function SMBus_WriteWordProtocol;external 'Ginkgo_Driver.dll' name 'SMBus_WriteWordProtocol';
function SMBus_ReadByteProtocol;external 'Ginkgo_Driver.dll' name 'SMBus_ReadByteProtocol';
function SMBus_ReadWordProtocol;external 'Ginkgo_Driver.dll' name 'SMBus_ReadWordProtocol';
function SMBus_ProcessCall;external 'Ginkgo_Driver.dll' name 'SMBus_ProcessCall';
function SMBus_BlockWrite;external 'Ginkgo_Driver.dll' name 'SMBus_BlockWrite';
function SMBus_BlockRead;external 'Ginkgo_Driver.dll' name 'SMBus_BlockRead';
function SMBus_BlockProcessCall;external 'Ginkgo_Driver.dll' name 'SMBus_BlockProcessCall';
function SMBus_GetAlert;external 'Ginkgo_Driver.dll' name 'SMBus_GetAlert';
end.
