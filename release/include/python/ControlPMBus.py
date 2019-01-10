################################################################################################
# Program test environment
# Pyhone version:3.4.1
# Firmware version:2.8.28
# Dependent files(MacOSX):libGinkgo_Driver.dylib,libusb-0.1.4.dylib,libusb-1.0.0.dylib
# Dependent files(Windows):Ginkgo_Driver.dll
# Dependent files(Linux):libGinkgo_Driver.so,libusb-1.0.so
################################################################################################
from ctypes import *
import platform
# 设备类型定义
VAI_USBADC      =       1
VCI_USBCAN1     =       3
VCI_USBCAN2     =       4
VGI_USBGPIO     =       1
VII_USBI2C      =       1
VSI_USBSPI      =       2


#错误码定义
ERR_SUCCESS               =  (0)    # 没有错误
ERR_PARAMETER_NULL        =  (-1)   # 传入的指针为空指针
ERR_INPUT_DATA_TOO_MUCH   =  (-2)   # 参数输入个数多余规定个数
ERR_INPUT_DATA_TOO_LESS   =  (-3)   # 参数输入个数少余规定个数
ERR_INPUT_DATA_ILLEGALITY =  (-4)   # 参数传入格式和规定的不符合
ERR_USB_WRITE_DATA        =  (-5)   # USB写数据错误
ERR_USB_READ_DATA         =  (-6)   # USB读数据错误
ERR_READ_NO_DATA          =  (-7)   # 请求读数据时返回没有数据
ERR_OPEN_DEVICE           =  (-8)   # 打开设备失败
ERR_CLOSE_DEVICE          =  (-9)   # 关闭设备失败
ERR_EXECUTE_CMD           =  (-10)  # 设备执行命令失败
ERR_SELECT_DEVICE         =  (-11)  # 选择设备失败
ERR_DEVICE_OPENED         =  (-12)  # 设备已经打开
ERR_DEVICE_NOTOPEN        =  (-13)  # 设备没有打开
ERR_BUFFER_OVERFLOW       =  (-14)  # 缓冲区溢出
ERR_DEVICE_NOTEXIST       =  (-15)  # 此设备不存在
ERR_LOAD_KERNELDLL        =  (-16)  # 装载动态库失败
ERR_CMD_FAILED            =  (-17)  # 执行命令失败错误码
ERR_BUFFER_CREATE         =  (-18)  # 内存不足

# Import library
if(platform.system()=="Windows"):
	if "64bit" in platform.architecture():
		GinkgoLib = windll.LoadLibrary( ".\\lib\\windows\\64bit\\Ginkgo_Driver.dll" )
	else:
		GinkgoLib = windll.LoadLibrary( ".\\lib\\windows\\32bit\\Ginkgo_Driver.dll" )
elif(platform.system()=="Darwin"):
	GinkgoLib = cdll.LoadLibrary( "./lib/macos/libGinkgo_Driver.dylib" )
elif(platform.system()=="Linux"):
	if "64bit" in platform.architecture():
		GinkgoLib = cdll.LoadLibrary( "./lib/linux/64bit/libGinkgo_Driver.so" )
	else:
		GinkgoLib = cdll.LoadLibrary( "./lib/linux/32bit/libGinkgo_Driver.so" )
else:
        print("Unknown system")

# 查看和电脑连接的设备数
def PMBus_ScanDevice(NeedInit):
	NeedInit = c_ubyte(NeedInit)
	return GinkgoLib.PMBus_ScanDevice(NeedInit)
# 打开设备
def PMBus_OpenDevice(DevType, DevIndex, Reserved):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	Reserved = c_int(Reserved)
	return GinkgoLib.PMBus_OpenDevice(DevType, DevIndex, Reserved)
# 关闭设备
def PMBus_CloseDevice( DevType, DevIndex):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	return GinkgoLib.PMBus_CloseDevice( DevType, DevIndex)
# 初始化SMBUS模块
def PMBus_HardInit( DevType,  DevIndex,  PMBusIndex, ClockSpeed, OwnAddr):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	ClockSpeed = c_int(ClockSpeed)
	OwnAddr = c_ubyte(OwnAddr)
	return GinkgoLib.PMBus_HardInit(DevType,  DevIndex,  PMBusIndex, ClockSpeed, OwnAddr)
# PMBus_WriteByte
def PMBus_WriteByte(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	CommandCode = c_ubyte(CommandCode)
	Data = c_ubyte(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_WriteByte(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,Data,PEC)
# SMBUS_ReadByte
def PMBus_ReadByte(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_ReadByte(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pData,PEC)
# PMBus_SendByte
def PMBus_SendByte(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_SendByte(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,PEC)
# PMBus_WriteWord
def PMBus_WriteWord(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	Data = c_ushort(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_WriteWord(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,Data,PEC)
# PMBus_ReadWord
def PMBus_ReadWord(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode, pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)	
	return GinkgoLib.PMBus_ReadWord(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode, pData,PEC)
# PMBus_WriteByteExt
def PMBus_WriteByteExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	CommandCodeExt = c_ubyte(CommandCodeExt)
	CommandCode = c_ubyte(CommandCode)
	Data = c_ubyte(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_WriteByteExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode,Data,PEC)
# PMBus_ReadByteExt
def PMBus_ReadByteExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode,pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	CommandCodeExt = c_ubyte(CommandCodeExt)
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_ReadByteExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode,pData,PEC)
# PMBus_WriteWordExt
def PMBus_WriteWordExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCodeExt = c_ubyte(CommandCodeExt)
	CommandCode = c_ubyte(CommandCode)
	Data = c_ushort(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_WriteWordExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode,Data,PEC)
# PMBus_ReadWordExt
def PMBus_ReadWordExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode, pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCodeExt = c_ubyte(CommandCodeExt)
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)	
	return GinkgoLib.PMBus_ReadWordExt(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCodeExt,CommandCode, pData,PEC)

# PMBus_BlockWrite
def PMBus_BlockWrite(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pData,ByteCount,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)	
	ByteCount = c_ubyte(ByteCount)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_BlockWrite(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pData,ByteCount,PEC)
# PMBus_BlockRead
def PMBus_BlockRead(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pData,pByteCount,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)	
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_BlockRead(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pData,pByteCount,PEC)
# PMBus_BlockProcessCall
def PMBus_BlockProcessCall(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pWriteData,WriteByteCount,pReadData,pReadByteCount,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)	
	WriteByteCount = c_ubyte(WriteByteCount)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_BlockProcessCall(DevType,DevIndex,PMBusIndex,SlaveAddr,CommandCode,pWriteData,WriteByteCount,pReadData,pReadByteCount,PEC)
# PMBus_GroupCmd
def PMBus_GroupCmd(DevType,DevIndex,PMBusIndex,pGroupCmdData,CmdNum,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)	
	CmdNum = c_ubyte(CmdNum)
	PEC = c_ubyte(PEC)
	return GinkgoLib.PMBus_GroupCmd(DevType,DevIndex,PMBusIndex,pGroupCmdData,CmdNum,PEC)
# PMBus_GetAlert
def PMBus_GetAlert(DevType,DevIndex,PMBusIndex,pAlertFlag):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	return GinkgoLib.PMBus_GetAlert(DevType,DevIndex,PMBusIndex,pAlertFlag);
# PMBus_SetControl
def PMBus_SetControl(DevType,DevIndex,PMBusIndex,Value):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	PMBusIndex = c_int(PMBusIndex)
	Value = c_ubyte(Value)
	return GinkgoLib.PMBus_SetControl(DevType,DevIndex,PMBusIndex,Value)
	
	


