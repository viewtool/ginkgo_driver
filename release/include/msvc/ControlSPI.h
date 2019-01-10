/**
  ******************************************************************************
  * @file    ControlSPI.h
  * $Author: wdluo $
  * $Revision: 447 $
  * $Date:: 2013-06-29 18:24:57 +0800 #$
  * @brief   Ginkgo USB-SPI�������ײ�������API��������.
  ******************************************************************************
  * @attention
  *
  *<h3><center>&copy; Copyright 2009-2012, ViewTool</center>
  *<center><a href="http:\\www.viewtool.com">http://www.viewtool.com</a></center>
  *<center>All Rights Reserved</center></h3>
  * 
  ******************************************************************************
  */
#ifndef _CONTROLSPI_H_
#define _CONTROLSPI_H_

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
#define VSI_USBSPI		(2)

//1.Ginkgoϵ�нӿڿ���Ϣ���������͡�
typedef  struct  _VSI_BOARD_INFO{
	uint8_t		ProductName[32];	//Ӳ�����ƣ����硰Ginkgo-SPI-Adaptor����ע�⣺�����ַ�����������\0����
	uint8_t		FirmwareVersion[4];	//�̼��汾
	uint8_t		HardwareVersion[4];	//Ӳ���汾
	uint8_t		SerialNumber[12];	//���������к�
} VSI_BOARD_INFO,*PVSI_BOARD_INFO; 


//2.�����ʼ��SPI����������
typedef struct _VSI_INIT_CONFIG{
    uint8_t     ControlMode;	//SPI���Ʒ�ʽ:0-Ӳ�����ƣ�ȫ˫��ģʽ��,1-Ӳ�����ƣ���˫��ģʽ����2-������ƣ���˫��ģʽ��,3-������ģʽ�����������������ΪMOSI
    uint8_t     TranBits;		//���ݴ����ֽڿ�ȣ���8��16֮��ȡֵ
    uint8_t     MasterMode;		//����ѡ�����:0-�ӻ���1-����
    uint8_t     CPOL;			//ʱ�Ӽ��Կ���:0-SCK����Ч��1-SCK����Ч
    uint8_t     CPHA;			//ʱ����λ����:0-��һ��SCKʱ�Ӳ�����1-�ڶ���SCKʱ�Ӳ���
    uint8_t     LSBFirst;		//������λ��ʽ:0-MSB��ǰ��1-LSB��ǰ
    uint8_t     SelPolarity;	//Ƭѡ�źż���:0-�͵�ƽѡ�У�1-�ߵ�ƽѡ��
	uint32_t	ClockSpeed;		//SPIʱ��Ƶ��:��λΪHZ
}VSI_INIT_CONFIG,*PVSI_INIT_CONFIG;

//3.SPI Flash������ʼ�����ݽṹ
typedef struct _VSI_FLASH_INIT_CONFIG
{
	uint32_t	page_size;
	uint32_t	page_num;
	uint8_t		write_enable[8];
	uint8_t		read_status[8];
	uint8_t		chip_erase[8];
	uint8_t		write_data[8];
	uint8_t		read_data[8];
	uint8_t		first_cmd[8];
	uint8_t		busy_bit;
	uint8_t		busy_mask;
	uint8_t		addr_bytes;
	uint8_t		addr_shift;
	uint8_t		init_flag;
}VSI_FLASH_INIT_CONFIG,*PVSI_FLASH_INIT_CONFIG;

#ifdef __cplusplus
extern "C"
{
#endif

int32_t WINAPI VSI_ScanDevice(uint8_t NeedInit=1);
int32_t WINAPI VSI_OpenDevice(int32_t DevType,int32_t DevIndex,int32_t Reserved);
int32_t WINAPI VSI_CloseDevice(int32_t DevType,int32_t DevIndex);
int32_t WINAPI VSI_InitSPI(int32_t DevType, int32_t DevIndex, PVSI_INIT_CONFIG pInitConfig);
int32_t WINAPI VSI_ReadBoardInfo(int32_t DevIndex,PVSI_BOARD_INFO pInfo);
int32_t WINAPI VSI_WriteBytes(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pWriteData,uint16_t Len);
int32_t WINAPI VSI_ReadBytes(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pReadData,uint16_t Len);
int32_t WINAPI VSI_WriteReadBytes(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t* pWriteData,uint16_t WriteLen,uint8_t * pReadData,uint16_t ReadLen);
int32_t WINAPI VSI_WriteBits(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pWriteBitStr);
int32_t WINAPI VSI_ReadBits(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pReadBitStr,int32_t ReadBitsNum);
int32_t WINAPI VSI_WriteReadBits(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pWriteBitStr,uint8_t *pReadBitStr,int32_t ReadBitsNum);

int32_t WINAPI VSI_SlaveModeSet(int32_t DevType,int32_t DevIndex,uint8_t SlaveMode,uint16_t CmdBytes,uint16_t DataBytes);
int32_t WINAPI VSI_SlaveReadBytes(int32_t DevType,int32_t DevIndex,uint8_t *pReadData,int32_t *pBytesNum,int32_t WaitTime);
int32_t WINAPI VSI_SlaveWriteBytes(int32_t DevType,int32_t DevIndex,uint8_t *pWriteData,int32_t WriteBytesNum);

int32_t WINAPI VSI_FlashInit(int32_t DevType,int32_t DevIndex, PVSI_FLASH_INIT_CONFIG pFlashInitConfig);
int32_t WINAPI VSI_FlashWriteBytes(int32_t DevType,int32_t DevIndex,int32_t PageAddr,uint8_t *pWriteData,uint16_t WriteLen);
int32_t WINAPI VSI_FlashReadBytes(int32_t DevType,int32_t DevIndex,int32_t PageAddr,uint8_t *pReadData,uint16_t ReadLen);

int32_t WINAPI VSI_SetUserKey(int32_t DevType,int32_t DevIndex,uint8_t* pUserKey);
int32_t WINAPI VSI_CheckUserKey(int32_t DevType,int32_t DevIndex,uint8_t* pUserKey);

int32_t WINAPI VSI_BlockWriteBytes(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pWriteData,uint16_t BlockSize,uint16_t BlockNum,uint32_t IntervalTime);
int32_t WINAPI VSI_BlockReadBytes(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pReadData,uint16_t BlockSize,uint16_t BlockNum,uint32_t IntervalTime);
int32_t WINAPI VSI_BlockWriteReadBytes(int32_t DevType,int32_t DevIndex,int32_t SPIIndex,uint8_t *pWriteData,uint16_t WriteBlockSize,uint8_t *pReadData,uint16_t ReadBlockSize,uint16_t BlockNum,uint32_t IntervalTime);

#ifdef __cplusplus
}
#endif

#endif

