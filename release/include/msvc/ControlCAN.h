/**
  ******************************************************************************
  * @file    ControlCAN.h
  * $Author: wdluo $
  * $Revision: 447 $
  * $Date:: 2013-06-29 18:24:57 +0800 #$
  * @brief   Ginkgo USB-CAN�������ײ�������API��������.
  ******************************************************************************
  * @attention
  *
  *<h3><center>&copy; Copyright 2009-2012, ViewTool</center>
  *<center><a href="http:\\www.viewtool.com">http://www.viewtool.com</a></center>
  *<center>All Rights Reserved</center></h3>
  *
  ******************************************************************************
  */
#ifndef _CONTROLCAN_H_
#define _CONTROLCAN_H_

#include <stdint.h>
#include "ErrorType.h"
#ifndef OS_UNIX
#include <Windows.h>
#else
#include <unistd.h>
#ifndef WINAPI
#define WINAPI
#endif
#endif

//���������Ͷ���
#define VCI_USBCAN1		3
#define VCI_USBCAN2		4


//CAN������
#define	ERR_CAN_OVERFLOW			0x0001	//CAN �������ڲ�FIFO���
#define	ERR_CAN_ERRALARM			0x0002	//CAN ���������󱨾�
#define	ERR_CAN_PASSIVE				0x0004	//CAN ��������������
#define	ERR_CAN_LOSE				0x0008	//CAN �������ٲö�ʧ
#define	ERR_CAN_BUSERR				0x0010	//CAN ���������ߴ���
#define	ERR_CAN_BUSOFF				0x0020	//CAN ���������߹ر�

//ͨ�ô�����
#define	ERR_DEVICEOPENED			0x0100	//�豸�Ѿ���
#define	ERR_DEVICEOPEN				0x0200	//���豸����
#define	ERR_DEVICENOTOPEN			0x0400	//�豸û�д�
#define	ERR_BUFFEROVERFLOW			0x0800	//���������
#define	ERR_DEVICENOTEXIST			0x1000	//���豸������
#define	ERR_LOADKERNELDLL			0x2000	//װ�ض�̬��ʧ��
#define ERR_CMDFAILED				0x4000	//ִ������ʧ�ܴ�����
#define	ERR_BUFFERCREATE			0x8000	//�ڴ治��


//�������÷���״ֵ̬
#define	STATUS_OK					1
#define STATUS_ERR					0


//1.����ZLGCANϵ�нӿڿ���Ϣ���������͡�
typedef  struct  _VCI_BOARD_INFO{
	uint16_t	hw_Version;			//Ӳ���汾�ţ���16 ���Ʊ�ʾ������0x0100 ��ʾV1.00��
	uint16_t	fw_Version;			//�̼��汾�ţ���16 ���Ʊ�ʾ��
	uint16_t	dr_Version;			//��������汾�ţ���16 ���Ʊ�ʾ��
	uint16_t	in_Version;			//�ӿڿ�汾�ţ���16 ���Ʊ�ʾ��
	uint16_t	irq_Num;			//�忨��ʹ�õ��жϺš�
	uint8_t		can_Num;			//��ʾ�м�·CAN ͨ����
	int8_t		str_Serial_Num[20];	//�˰忨�����кš�
	int8_t		str_hw_Type[40];	//Ӳ�����ͣ����硰USBCAN V1.00����ע�⣺�����ַ�����������\0������
	uint16_t	Reserved[4];		//ϵͳ������
} VCI_BOARD_INFO,*PVCI_BOARD_INFO;

//1.Ginkgoϵ�нӿڿ���Ϣ���������͡�
typedef  struct  _VCI_BOARD_INFO_EX{
	uint8_t		ProductName[32];	//Ӳ�����ƣ����硰Ginkgo-CAN-Adaptor����ע�⣺�����ַ�����������\0����
	uint8_t		FirmwareVersion[4];	//�̼��汾
	uint8_t		HardwareVersion[4];	//Ӳ���汾
	uint8_t		SerialNumber[12];	//���������к�
} VCI_BOARD_INFO_EX,*PVCI_BOARD_INFO_EX;

//2.����CAN��Ϣ֡���������͡�
typedef  struct  _VCI_CAN_OBJ{
	uint32_t	ID;			//����ID��
	uint32_t	TimeStamp;	//���յ���Ϣ֡ʱ��ʱ���ʶ����CAN ��������ʼ����ʼ��ʱ��
	uint8_t		TimeFlag;	//�Ƿ�ʹ��ʱ���ʶ��Ϊ1 ʱTimeStamp ��Ч��TimeFlag ��TimeStamp ֻ�ڴ�֡Ϊ����֡ʱ�����塣
	uint8_t		SendType;	//����֡���ͣ�=0 ʱΪ�������ͣ�=1 ʱΪ���η��ͣ�=2 ʱΪ�Է����գ�=3 ʱΪ�����Է����գ�ֻ�ڴ�
						//֡Ϊ����֡ʱ�����塣�����豸����ΪEG20T-CAN ʱ�����ͷ�ʽ��VCI_InitCan ��ͨ�����ã��˴���
						//������Ч������Ϊ�Է�����ģʽ��EG20T-CAN ���ܴ������Ͻ������ݣ�ֻ���յ��Լ����������ݣ�
	uint8_t		RemoteFlag;	//�Ƿ���Զ��֡
	uint8_t		ExternFlag;	//�Ƿ�����չ֡
	uint8_t		DataLen;	//���ݳ���(<=8)����Data �ĳ��ȡ�
	uint8_t		Data[8];	//���ĵ����ݡ�
	uint8_t		Reserved[3];//ϵͳ������
}VCI_CAN_OBJ,*PVCI_CAN_OBJ;

//3.����CAN������״̬���������͡�
typedef struct _VCI_CAN_STATUS{
	uint8_t		ErrInterrupt;	//�жϼ�¼���������������
	uint8_t		regMode;		//CAN ������ģʽ�Ĵ�����
	uint8_t		regStatus;		//CAN ������״̬�Ĵ�����
	uint8_t		regALCapture;	//CAN �������ٲö�ʧ�Ĵ�����
	uint8_t		regECCapture;	//CAN ����������Ĵ�����
	uint8_t		regEWLimit;		//CAN ���������󾯸����ƼĴ�����
	uint8_t		regRECounter;	//CAN ���������մ���Ĵ�����
	uint8_t		regTECounter;	//CAN ���������ʹ���Ĵ�����
	uint32_t	regESR;			//CAN ����������״̬�Ĵ�����
	uint32_t	regTSR;			//CAN ����������״̬�Ĵ���
	uint32_t	BufferSize;		//CAN ���������ջ�������С
	uint32_t	Reserved;
}VCI_CAN_STATUS,*PVCI_CAN_STATUS;

//4.���������Ϣ���������͡�
typedef struct _ERR_INFO{
	uint32_t	ErrCode;			//������
	uint8_t		Passive_ErrData[3];	//�������Ĵ���������������ʱ��ʾΪ��������Ĵ����ʶ���ݡ�
	uint8_t		ArLost_ErrData;		//�������Ĵ��������ٲö�ʧ����ʱ��ʾΪ�ٲö�ʧ����Ĵ����ʶ���ݡ�
} VCI_ERR_INFO,*PVCI_ERR_INFO;

//5.�����ʼ��CAN����������
typedef struct _INIT_CONFIG{
	uint32_t	AccCode;	//������
	uint32_t	AccMask;	//������
	uint32_t	Reserved;	//����
	uint8_t		Filter;		//�˲���ʽ,0-˫�˲���1-���˲�
	uint8_t		Timing0;	//��ʱ��0��BTR0����
	uint8_t		Timing1;	//��ʱ��1��BTR1����
	uint8_t		Mode;		//ģʽ��
}VCI_INIT_CONFIG,*PVCI_INIT_CONFIG;

//�����ʼ��CAN����������
typedef struct _INIT_CONFIG_EX
{
	//CAN������ = 36MHz/(CAN_BRP)/(CAN_SJW+CAN_BS1+CAN_BS2)
	uint32_t	CAN_BRP;	//ȡֵ��Χ1~1024
	uint8_t		CAN_SJW;	//ȡֵ��Χ1~4
	uint8_t		CAN_BS1;	//ȡֵ��Χ1~16
	uint8_t		CAN_BS2;	//ȡֵ��Χ1~8
	uint8_t		CAN_Mode;	//CAN����ģʽ��0-����ģʽ��1-����ģʽ��2-��Ĭģʽ��3-��Ĭ����ģʽ
	uint8_t		CAN_ABOM;	//�Զ����߹���0-��ֹ��1-ʹ��
	uint8_t		CAN_NART;	//�����ط�����0-ʹ�ܱ����ش���1-��ֹ�����ش�
	uint8_t		CAN_RFLM;	//FIFO��������0-�±��ĸ��Ǿɱ��ģ�1-�����±���
	uint8_t		CAN_TXFP;	//�������ȼ�����0-��ʶ��������1-��������˳�����
	uint8_t		CAN_RELAY;	//�Ƿ����м̹��ܣ�0x00-�ر��м̹��ܣ�0x10-CAN1��CAN2�м̣�0x01-CAN2��CAN1�м̣�0x11-˫���м�
	uint32_t	Reserved;	//ϵͳ����
}VCI_INIT_CONFIG_EX,*PVCI_INIT_CONFIG_EX;


//6.������CAN �˲���������
typedef struct _VCI_FILTER_CONFIG{
	uint8_t		Enable;			//ʹ�ܸù�������1-ʹ�ܣ�0-��ֹ
	uint8_t		FilterIndex;	//�����������ţ�ȡֵ��ΧΪ0��13
	uint8_t		FilterMode;		//������ģʽ��0-����λģʽ��1-��ʶ���б�ģʽ
	uint8_t		ExtFrame;		//���˵�֡���ͱ�־��Ϊ1 ����Ҫ���˵�Ϊ��չ֡��Ϊ0 ����Ҫ���˵�Ϊ��׼֡��
	uint32_t	ID_Std_Ext;		//������ID
	uint32_t	ID_IDE;			//������IDE
	uint32_t	ID_RTR;			//������RTR
	uint32_t	MASK_Std_Ext;	//������ID������ֻ���ڹ�����ģʽΪ����λģʽʱ����
	uint32_t	MASK_IDE;		//������IDE������ֻ���ڹ�����ģʽΪ����λģʽʱ����
	uint32_t	MASK_RTR;		//������RTR������ֻ���ڹ�����ģʽΪ����λģʽʱ����
	uint32_t	Reserved;		//ϵͳ����
} VCI_FILTER_CONFIG,*PVCI_FILTER_CONFIG;

typedef void(WINAPI *PVCI_RECEIVE_CALLBACK)(uint32_t DevIndex,uint32_t CANIndex,uint32_t Len);

#ifdef __cplusplus
extern "C"
{
#endif

uint32_t WINAPI VCI_ScanDevice(uint8_t NeedInit);
uint32_t WINAPI VCI_OpenDevice(uint32_t DevType,uint32_t DevIndex,uint32_t Reserved);
uint32_t WINAPI VCI_CloseDevice(uint32_t DevType,uint32_t DevIndex);
uint32_t WINAPI VCI_InitCAN(uint32_t DevType, uint32_t DevIndex, uint32_t CANIndex, PVCI_INIT_CONFIG pInitConfig);
uint32_t WINAPI VCI_InitCANEx(uint32_t DevType, uint32_t DevIndex, uint32_t CANIndex, PVCI_INIT_CONFIG_EX pInitConfig);

uint32_t WINAPI VCI_ReadBoardInfo(uint32_t DevType,uint32_t DevIndex,PVCI_BOARD_INFO pInfo);
uint32_t WINAPI VCI_ReadBoardInfoEx(uint32_t DevIndex, PVCI_BOARD_INFO_EX pInfo);
uint32_t WINAPI VCI_ReadErrInfo(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,PVCI_ERR_INFO pErrInfo);
uint32_t WINAPI VCI_ReadCANStatus(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,PVCI_CAN_STATUS pCANStatus);

uint32_t WINAPI VCI_GetReference(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,uint32_t RefType,void *pData);
uint32_t WINAPI VCI_SetReference(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,uint32_t RefType,void *pData);
uint32_t WINAPI VCI_SetFilter(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,PVCI_FILTER_CONFIG pFilter);

uint32_t WINAPI VCI_GetReceiveNum(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex);
uint32_t WINAPI VCI_ClearBuffer(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex);

uint32_t WINAPI VCI_StartCAN(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex);
uint32_t WINAPI VCI_ResetCAN(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex);

uint32_t WINAPI VCI_Transmit(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,PVCI_CAN_OBJ pSend,uint32_t Len);
uint32_t WINAPI VCI_Receive(uint32_t DevType,uint32_t DevIndex,uint32_t CANIndex,PVCI_CAN_OBJ pReceive,uint32_t Len,int32_t WaitTime=-1);

uint32_t WINAPI VCI_RegisterReceiveCallback(uint32_t DevIndex,PVCI_RECEIVE_CALLBACK pReceiveCallBack);
uint32_t WINAPI VCI_LogoutReceiveCallback(uint32_t DevIndex);

int32_t WINAPI VCI_SetUserKey(int32_t DevType,int32_t DevIndex,uint8_t* pUserKey);
int32_t WINAPI VCI_CheckUserKey(int32_t DevType,int32_t DevIndex,uint8_t* pUserKey);
#ifdef __cplusplus
}
#endif

#endif

