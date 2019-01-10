unit ControlPMBus;

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

function PMBus_ScanDevice(NeedInit:Byte):Integer; stdcall;
function PMBus_OpenDevice(DevType,DevIndex,Reserved:Integer):Integer; stdcall;
function PMBus_CloseDevice(DevType,DevIndex:Integer):Integer; stdcall;
function PMBus_HardInit(DevType,DevIndex,PMBusIndex,ClockSpeed:Integer;OwnAddr:Byte):Integer; stdcall;
function PMBus_WriteByte(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode,Data,PEC:Byte):Integer; stdcall;
function PMBus_ReadByte(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode:Byte; pData:PByte;PEC:Byte):Integer; stdcall;
function PMBus_SendByte(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode,PEC:Byte):Integer; stdcall;
function PMBus_WriteWord(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode:Byte;Data:Word;PEC:Byte):Integer; stdcall;
function PMBus_ReadWord(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode:Word; pData:PWord;PEC:Byte):Integer; stdcall;
function PMBus_WriteByteExt(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCodeExt,CommandCode,Data,PEC:Byte):Integer; stdcall;
function PMBus_ReadByteExt(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCodeExt,CommandCode:Byte;pData:PByte;PEC:Byte):Integer; stdcall;
function PMBus_WriteWordExt(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCodeExt,CommandCode:Byte;Data:Word;PEC:Byte):Integer; stdcall;
function PMBus_ReadWordExt(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCodeExt,CommandCode:Byte;pData:PWord;PEC:Byte):Integer; stdcall;
function PMBus_BlockWrite(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode:Byte;pData:PByte;ByteCount,PEC:Byte):Integer; stdcall;
function PMBus_BlockRead(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode:Byte;pData,pByteCount:Pbyte;PEC:Byte):Integer; stdcall;
function PMBus_BlockProcessCall(DevType,DevIndex,PMBusIndex:Integer;SlaveAddr,CommandCode:Byte;pWriteData:PByte;WriteByteCount:Byte;pReadData,pReadByteCount:PByte;PEC:Byte):Integer; stdcall;
function PMBus_GroupCmd(DevType,DevIndex,PMBusIndex:Integer;pGroupCmdData:PByte;mdNum,PEC:Byte):Integer; stdcall;
function PMBus_GetAlert(DevType,DevIndex,PMBusIndex:Integer;pAlertFlag:PByte):Integer; stdcall;
function PMBus_SetControl(DevType,DevIndex,PMBusIndex:Integer;Value:Byte):Integer; stdcall;
implementation
function PMBus_ScanDevice;external 'Ginkgo_Driver.dll' name 'PMBus_ScanDevice';
function PMBus_OpenDevice;external 'Ginkgo_Driver.dll' name 'PMBus_OpenDevice';
function PMBus_CloseDevice;external 'Ginkgo_Driver.dll' name 'PMBus_CloseDevice';
function PMBus_HardInit;external 'Ginkgo_Driver.dll' name 'PMBus_HardInit';
function PMBus_WriteByte;external 'Ginkgo_Driver.dll' name 'PMBus_WriteByte';
function PMBus_ReadByte;external 'Ginkgo_Driver.dll' name 'PMBus_ReadByte';
function PMBus_SendByte;external 'Ginkgo_Driver.dll' name 'PMBus_SendByte';
function PMBus_WriteWord;external 'Ginkgo_Driver.dll' name 'PMBus_WriteWord';
function PMBus_ReadWord;external 'Ginkgo_Driver.dll' name 'PMBus_ReadWord';
function PMBus_WriteByteExt;external 'Ginkgo_Driver.dll' name 'PMBus_WriteByteExt';
function PMBus_ReadByteExt;external 'Ginkgo_Driver.dll' name 'PMBus_ReadByteExt';
function PMBus_WriteWordExt;external 'Ginkgo_Driver.dll' name 'PMBus_WriteWordExt';
function PMBus_ReadWordExt;external 'Ginkgo_Driver.dll' name 'PMBus_ReadWordExt';
function PMBus_BlockWrite;external 'Ginkgo_Driver.dll' name 'PMBus_BlockWrite';
function PMBus_BlockRead;external 'Ginkgo_Driver.dll' name 'PMBus_BlockRead';
function PMBus_BlockProcessCall;external 'Ginkgo_Driver.dll' name 'PMBus_BlockProcessCall';
function PMBus_GroupCmd;external 'Ginkgo_Driver.dll' name 'PMBus_GroupCmd';
function PMBus_GetAlert;external 'Ginkgo_Driver.dll' name 'PMBus_GetAlert';
function PMBus_SetControl;external 'Ginkgo_Driver.dll' name 'PMBus_SetControl';
end.
