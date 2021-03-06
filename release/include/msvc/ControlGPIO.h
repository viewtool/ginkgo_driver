/**
  ******************************************************************************
  * @file    ControlGPIO.h
  * $Author: wdluo $
  * $Revision: 447 $
  * $Date:: 2013-06-29 18:24:57 +0800 #$
  * @brief   Ginkgo USB-GPIO适配器底层控制相关API函数定义.
  ******************************************************************************
  * @attention
  *
  *<h3><center>&copy; Copyright 2009-2012, ViewTool</center>
  *<center><a href="http:\\www.viewtool.com">http://www.viewtool.com</a></center>
  *<center>All Rights Reserved</center></h3>
  * 
  ******************************************************************************
  */
#ifndef _CONTROLGPIO_H_
#define _CONTROLGPIO_H_

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

//适配器类型定义
#define VGI_USBGPIO			(1)		//设备类型

//定义GPIO的引脚
#define	VGI_GPIO_PIN0		(1<<0)	//GPIO_0
#define	VGI_GPIO_PIN1		(1<<1)	//GPIO_1
#define	VGI_GPIO_PIN2		(1<<2)	//GPIO_2
#define	VGI_GPIO_PIN3		(1<<3)	//GPIO_3
#define	VGI_GPIO_PIN4		(1<<4)	//GPIO_4
#define	VGI_GPIO_PIN5		(1<<5)	//GPIO_5
#define	VGI_GPIO_PIN6		(1<<6)	//GPIO_6
#define	VGI_GPIO_PIN7		(1<<7)	//GPIO_7
#define	VGI_GPIO_PIN8		(1<<8)	//GPIO_8
#define	VGI_GPIO_PIN9		(1<<9)	//GPIO_9
#define	VGI_GPIO_PIN10		(1<<10)	//GPIO_10
#define	VGI_GPIO_PIN11		(1<<11)	//GPIO_11
#define	VGI_GPIO_PIN12		(1<<12)	//GPIO_12
#define	VGI_GPIO_PIN13		(1<<13)	//GPIO_13
#define	VGI_GPIO_PIN14		(1<<14)	//GPIO_14
#define	VGI_GPIO_PIN15		(1<<15)	//GPIO_15
#define	VGI_GPIO_PIN_ALL	(0xFFFF)//ALL PIN

//GPIO functional description
#define INPUT_PULL_UP		0	//Input pull-up
#define OUTPUT_PUSH_PULL	1	//Output push-pull
#define OUTPUT_OPEN_DRAIN	2	//Output open-drain
#define INPUT_FLOATING		3	//Input floating

//1.Ginkgo系列接口卡信息的数据类型。
typedef  struct  _VGI_BOARD_INFO{
	uint8_t		ProductName[32];	//硬件名称，比如“Ginkgo-I2C-Adaptor”（注意：包括字符串结束符‘\0’）
	uint8_t		FirmwareVersion[4];	//固件版本
	uint8_t		HardwareVersion[4];	//硬件版本
	uint8_t		SerialNumber[12];	//适配器序列号
} VGI_BOARD_INFO,*PVGI_BOARD_INFO; 



#ifdef __cplusplus
extern "C"
{
#endif

int32_t WINAPI VGI_ScanDevice(uint8_t NeedInit=1);
int32_t WINAPI VGI_OpenDevice(int32_t DevType,int32_t DevIndex,int32_t Reserved);
int32_t WINAPI VGI_CloseDevice(int32_t DevType,int32_t DevIndex);
int32_t WINAPI VGI_ReadBoardInfo(int32_t DevIndex,PVGI_BOARD_INFO pInfo);
int32_t WINAPI VGI_InitGPIO(int32_t DevType, int32_t DevIndex, uint16_t PinMask, uint32_t PinMode);
int32_t WINAPI VGI_WriteDatas(int32_t DevType,int32_t DevIndex, uint16_t PinMask,uint16_t Data);
int32_t WINAPI VGI_ReadDatas(int32_t DevType,int32_t DevIndex,uint16_t PinMask,uint16_t *pData);
int32_t	WINAPI VGI_SetPins(int32_t DevType,int32_t DevIndex,uint16_t PinMask);
int32_t	WINAPI VGI_ResetPins(int32_t DevType,int32_t DevIndex,uint16_t PinMask);
int32_t	WINAPI VGI_SetInput(int32_t DevType,int32_t DevIndex,uint16_t PinMask);
int32_t	WINAPI VGI_SetOutput(int32_t DevType,int32_t DevIndex,uint16_t PinMask);
int32_t	WINAPI VGI_SetOpenDrain(int32_t DevType,int32_t DevIndex,uint16_t PinMask);

#ifdef __cplusplus
}
#endif


#endif
