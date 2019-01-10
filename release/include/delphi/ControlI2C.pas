unit ControlI2C;

interface

const
//Device type definition
VAI_USBADC	=	1;
VCI_USBCAN1	=	3;
VCI_USBCAN2	=	4;
VGI_USBGPIO	=	1;
VII_USBI2C	=	1;
VSI_USBSPI	=	2;
//USB-I2C��������ʼ�����ݶ���
VII_ADDR_7BIT		=	7;	    // 7bit��ַģʽ
VII_ADDR_10BIT	=	10;	    // 10bit��ַģʽ
VII_HCTL_MODE		=	1;	    // Ӳ������
VII_SCTL_MODE		=	2;	    // ��������
VII_MASTER			=	1;	    // ����
VII_SLAVE			  =	0;	    // �ӻ�
VII_SUB_ADDR_NONE	  =	0;	// ���ӵ�ַ
VII_SUB_ADDR_1BYTE	=	1;	// 1Byte�ӵ�ַ
VII_SUB_ADDR_2BYTE	=	2;	// 2Byte�ӵ�ַ
VII_SUB_ADDR_3BYTE	=	3;	// 3Byte�ӵ�ַ
VII_SUB_ADDR_4BYTE	=	4;	// 4Byte�ӵ�ַ
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
//USB-I2C��ʼ���ṹ��
type
PVII_INIT_CONFIG = ^VII_INIT_CONFIG;
VII_INIT_CONFIG = record
  MasterMode:Byte;    //����ѡ�����:0-�ӻ���1-����
  ControlMode:Byte;   //���Ʒ�ʽ:1-Ӳ�����ƣ�2-��������
  AddrType:Byte;      //7-7bitģʽ��10-10bitģʽ
  SubAddrWidth:Byte;  //�ӵ�ַ���ȣ�0��4ȡֵ��0ʱ��ʾ���ӵ�ַģʽ
  Addr:Word;          //�ӻ�ģʽʱ����豸��ַ
  ClockSpeed:Longword;//ʱ��Ƶ��:��λΪHZ
end;
//Ginkgoϵ����������Ϣ���������͡�
type
PVII_BOARD_INFO = ^VII_BOARD_INFO;
VII_BOARD_INFO = record
	ProductName:Array[0..31] Of Byte;	    //Ӳ�����ƣ����硰Ginkgo-I2C-Adaptor����ע�⣺�����ַ�����������\0����
	FirmwareVersion:Array[0..3] Of Byte;	//�̼��汾
	HardwareVersion:Array[0..3] Of Byte;	//Ӳ���汾
	SerialNumber:Array[0..11] Of Byte;	  //���������к�
end;
//����I2Cʱ��������壬ʱ�䵥λΪ΢��
type
PVII_TIME_CONFIG = ^VII_TIME_CONFIG;
VII_TIME_CONFIG = record
  tHD_STA:Word;             //��ʼ�źű���ʱ��
  tSU_STA:Word;             //��ʼ�źŽ���ʱ��
  tLOW:Word;                //ʱ�ӵ͵�ƽʱ��
  tHIGH:Word;               //ʱ�Ӹߵ�ƽʱ��
  tSU_DAT:Word;             //�������뽨��ʱ��
  tSU_STO:Word;             //ֹͣ�źŽ���ʱ��
  tDH:Word;                 //�����������ʱ��
  tDH_DAT:Word;             //�������뱣��ʱ��
  tAA:Word;                 //SCL�����SDA���������Ӧ���ź�
  tR:Word;                  //SDA��SCL����ʱ��
  tF:Word;                  //SDA��SCL�½�ʱ��
  tBuf:Word;                //�µķ��Ϳ�ʼǰ���߿���ʱ��
  tACK:Array[0..3] Of Byte;
  tStart:Word;
  tStop:Word;
end;
//��������
function VII_ScanDevice(NeedInit:Byte):Integer; stdcall;
function VII_OpenDevice(DevType,DevIndex,Reserved:Integer):Integer; stdcall;
function VII_CloseDevice(DevType,DevIndex:Integer):Integer; stdcall;
function VII_ReadBoardInfo(DevIndex:Integer;pInfo:PVII_BOARD_INFO):Integer; stdcall;
function VII_InitI2C(DevType, DevIndex, I2CIndex:Integer; pInitConfig:PVII_INIT_CONFIG):Integer; stdcall;
function VII_WriteBytes(DevType, DevIndex, I2CIndex:Integer;Addr:Word;SubAddr:Integer;pWriteData:PByte;Len:Word):Integer; stdcall;
function VII_ReadBytes(DevType, DevIndex, I2CIndex:Integer;Addr:Word;SubAddr:Integer;pReadData:PByte;Len:Word):Integer; stdcall;
function VII_TimeConfig(DevType, DevIndex, I2CIndex:Integer;pTimeConfig:PVII_TIME_CONFIG):Integer; stdcall;
function VII_SetUserKey(DevType,DevIndex:Integer;pUserKey:PByte):Integer; stdcall;
function VII_CheckUserKey(DevType,DevIndex:Integer;pUserKey:PByte):Integer; stdcall;
function VII_SlaveReadBytes(DevType,DevIndex,I2CIndex:Integer;pReadData:PByte;pLen:PWord):Integer; stdcall;
function VII_SlaveWriteBytes(DevType,DevIndex,I2CIndex:Integer;pWriteData:PByte;Len:Word):Integer; stdcall;

implementation

function VII_ScanDevice;external 'Ginkgo_Driver.dll' name 'VII_ScanDevice';
function VII_OpenDevice;external 'Ginkgo_Driver.dll' name 'VII_OpenDevice';
function VII_CloseDevice;external 'Ginkgo_Driver.dll' name 'VII_CloseDevice';
function VII_InitI2C;external 'Ginkgo_Driver.dll' name 'VII_InitI2C';
function VII_WriteBytes;external 'Ginkgo_Driver.dll' name 'VII_WriteBytes';
function VII_ReadBytes;external 'Ginkgo_Driver.dll' name 'VII_ReadBytes';
function VII_TimeConfig;external 'Ginkgo_Driver.dll' name 'VII_TimeConfig';
function VII_ReadBoardInfo;external 'Ginkgo_Driver.dll' name 'VII_ReadBoardInfo';
function VII_SetUserKey;external 'Ginkgo_Driver.dll' name 'VII_SetUserKey';
function VII_CheckUserKey;external 'Ginkgo_Driver.dll' name 'VII_CheckUserKey';
function VII_SlaveReadBytes;external 'Ginkgo_Driver.dll' name 'VII_SlaveReadBytes';
function VII_SlaveWriteBytes;external 'Ginkgo_Driver.dll' name 'VII_SlaveWriteBytes';
end.