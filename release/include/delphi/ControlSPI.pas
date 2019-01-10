unit ControlSPI;

interface
const
//Device type definition
VAI_USBADC	=	1;
VCI_USBCAN1	=	3;
VCI_USBCAN2	=	4;
VGI_USBGPIO	=	1;
VII_USBI2C	=	1;
VSI_USBSPI	=	2;
//�����붨��
ERR_SUCCESS					      =	(0);		// û�д���
ERR_PARAMETER_NULL			  =	(-1);	  // �����ָ��Ϊ��ָ��
ERR_INPUT_DATA_TOO_MUCH		=	(-2);	  // ���������������涨����
ERR_INPUT_DATA_TOO_LESS		=	(-3);	  // ���������������涨����
ERR_INPUT_DATA_ILLEGALITY	=	(-4);	  // ���������ʽ�͹涨�Ĳ�����
ERR_USB_WRITE_DATA			  =	(-5);	  // USBд���ݴ���
ERR_USB_READ_DATA			    =	(-6);	  // USB�����ݴ���
ERR_READ_NO_DATA			    =	(-7);	  // ���������ʱ����û������
ERR_OPEN_DEVICE				    =	(-8);	  // ���豸ʧ��
ERR_CLOSE_DEVICE			    =	(-9);	  // �ر��豸ʧ��
ERR_EXECUTE_CMD				    =	(-10);	// �豸ִ������ʧ��
ERR_SELECT_DEVICE			    =	(-11);	// ѡ���豸ʧ��
ERR_DEVICE_OPENED			    =	(-12);	// �豸�Ѿ���
ERR_DEVICE_NOTOPEN			  =	(-13);	// �豸û�д�
ERR_BUFFER_OVERFLOW			  =	(-14);	// ���������
ERR_DEVICE_NOTEXIST			  =	(-15);	// ���豸������
ERR_LOAD_KERNELDLL			  =	(-16);	// װ�ض�̬��ʧ��
ERR_CMD_FAILED				    =	(-17);	// ִ������ʧ�ܴ�����
ERR_BUFFER_CREATE			    =	(-18);	// �ڴ治��
//Ginkgoϵ����������Ϣ���������͡�
type
PVSI_BOARD_INFO = ^VSI_BOARD_INFO;
VSI_BOARD_INFO = record
	ProductName:Array[0..31] Of Byte;	    //Ӳ�����ƣ����硰Ginkgo-SPI-Adaptor����ע�⣺�����ַ�����������\0����
	FirmwareVersion:Array[0..3] Of Byte;	//�̼��汾
	HardwareVersion:Array[0..3] Of Byte;	//Ӳ���汾
	SerialNumber:Array[0..11] Of Byte;	  //���������к�
end;
//USB-SPI��ʼ���ṹ��
type
PVSI_INIT_CONFIG = ^VSI_INIT_CONFIG;
VSI_INIT_CONFIG = record
  ControlMode:Byte;     //SPI���Ʒ�ʽ:0-Ӳ�����ƣ�ȫ˫��ģʽ��,1-Ӳ�����ƣ���˫��ģʽ����2-������ƣ���˫��ģʽ��,3-������ģʽ�����������������ΪMOSI
  TranBits:Byte;        //���ݴ����ֽڿ�ȣ���8��16֮��ȡֵ
  MasterMode:Byte;      //����ѡ�����:0-�ӻ���1-����
  CPOL:Byte;            //ʱ�Ӽ��Կ���:0-SCK����Ч��1-SCK����Ч
  CPHA:Byte;            //ʱ����λ����:0-��һ��SCKʱ�Ӳ�����1-�ڶ���SCKʱ�Ӳ���
  LSBFirst:Byte;        //������λ��ʽ:0-MSB��ǰ��1-LSB��ǰ
  SelPolarity:Byte;     //Ƭѡ�źż���:0-�͵�ƽѡ�У�1-�ߵ�ƽѡ��
  ClockSpeed:Integer;   //SPIʱ��Ƶ��:��λΪHZ
end;
//��������
function VSI_ScanDevice(NeedInit:Byte):Integer; stdcall;
function VSI_OpenDevice(DevType,DevIndex,Reserved:Integer):Integer; stdcall;
function VSI_CloseDevice(DevType,DevIndex:Integer):Integer; stdcall;
function VSI_ReadBoardInfo(DevIndex:Integer;pInfo:PVSI_BOARD_INFO):Integer; stdcall;
function VSI_InitSPI(DevType, DevIndex:Integer; pInitConfig:PVSI_INIT_CONFIG):Integer; stdcall;
function VSI_WriteBytes(DevType, DevIndex, SPIIndex:Integer;pWriteData:PByte;Len:Word):Integer; stdcall;
function VSI_ReadBytes(DevType, DevIndex, SPIIndex:Integer;pReadData:PByte;Len:Word):Integer; stdcall;
function VSI_WriteReadBytes(DevType, DevIndex, SPIIndex:Integer;pWriteData:PByte;WriteLen:Word;pReadData:PByte;ReadLen:Word):Integer; stdcall;
function VSI_WriteBits(DevType, DevIndex, SPIIndex:Integer;pWriteBitStr:PChar):Integer; stdcall;
function VSI_ReadBits(DevType, DevIndex, SPIIndex:Integer;pReadBitStr:PChar;ReadBitsNum:Integer):Integer; stdcall;
function VSI_WriteReadBits(DevType, DevIndex, SPIIndex:Integer;pWriteBitStr,pReadBitStr:PChar;ReadBitsNum:Integer):Integer; stdcall;
function VSI_SlaveReadBytes(DevType,DevIndex:Integer;pReadData:PByte;pBytesNum:PInteger;WaitTime:Integer):Integer; stdcall;
function VSI_SlaveWriteBytes(DevType,DevIndex:Integer;pWriteData:PByte;WriteBytesNum:Integer):Integer; stdcall;
function VSI_SetUserKey(DevType,DevIndex:Integer;pUserKey:PByte):Integer; stdcall;
function VSI_CheckUserKey(DevType,DevIndex:Integer;pUserKey:PByte):Integer; stdcall;

implementation

function VSI_ScanDevice;external 'Ginkgo_Driver.dll' name 'VSI_ScanDevice';
function VSI_OpenDevice;external 'Ginkgo_Driver.dll' name 'VSI_OpenDevice';
function VSI_CloseDevice;external 'Ginkgo_Driver.dll' name 'VSI_CloseDevice';
function VSI_ReadBoardInfo;external 'Ginkgo_Driver.dll' name 'VSI_ReadBoardInfo';
function VSI_InitSPI;external 'Ginkgo_Driver.dll' name 'VSI_InitSPI';
function VSI_WriteBytes;external 'Ginkgo_Driver.dll' name 'VSI_WriteBytes';
function VSI_ReadBytes;external 'Ginkgo_Driver.dll' name 'VSI_ReadBytes';
function VSI_WriteReadBytes;external 'Ginkgo_Driver.dll' name 'VSI_WriteReadBytes';
function VSI_WriteBits;external 'Ginkgo_Driver.dll' name 'VSI_WriteBits';
function VSI_ReadBits;external 'Ginkgo_Driver.dll' name 'VSI_ReadBits';
function VSI_WriteReadBits;external 'Ginkgo_Driver.dll' name 'VSI_WriteReadBits';
function VSI_SlaveReadBytes;external 'Ginkgo_Driver.dll' name 'VSI_SlaveReadBytes';
function VSI_SlaveWriteBytes;external 'Ginkgo_Driver.dll' name 'VSI_SlaveWriteBytes';
function VSI_SetUserKey;external 'Ginkgo_Driver.dll' name 'VSI_SlaveWriteBytes';
function VSI_CheckUserKey;external 'Ginkgo_Driver.dll' name 'VSI_CheckUserKey';
end.
