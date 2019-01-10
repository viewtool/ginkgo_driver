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
def SMBus_ScanDevice(NeedInit):
	NeedInit = c_ubyte(NeedInit)
	return GinkgoLib.SMBus_ScanDevice(NeedInit)
# 打开设备
def SMBus_OpenDevice(DevType, DevIndex, Reserved):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	Reserved = c_int(Reserved)
	return GinkgoLib.SMBus_OpenDevice(DevType, DevIndex, Reserved)
# 关闭设备
def SMBus_CloseDevice( DevType, DevIndex):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	return GinkgoLib.SMBus_CloseDevice( DevType, DevIndex)
# 初始化SMBus模块
def SMBus_HardInit( DevType,  DevIndex,  SMBusIndex, ClockSpeed, OwnAddr):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	ClockSpeed = c_int(ClockSpeed)
	OwnAddr = c_ubyte(OwnAddr)
	return GinkgoLib.SMBus_HardInit(DevType,  DevIndex,  SMBusIndex, ClockSpeed, OwnAddr)
# SMBus_QuickCommand
def SMBus_QuickCommand(DevType, DevIndex, SMBusIndex, SlaveAddr):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	return GinkgoLib.SMBus_QuickCommand(DevType, DevIndex, SMBusIndex, SlaveAddr)
# SMBus_WriteByte
def SMBus_WriteByte(DevType,DevIndex,SMBusIndex,SlaveAddr,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	Data = c_ubyte(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_WriteByte(DevType,DevIndex,SMBusIndex,SlaveAddr,Data,PEC)
# SMBus_ReadByte
def SMBus_ReadByte(DevType,DevIndex,SMBusIndex,SlaveAddr,pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_ReadByte(DevType,DevIndex,SMBusIndex,SlaveAddr,pData,PEC)
# SMBus_WriteByteProtocol
def SMBus_WriteByteProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	Data = c_ubyte(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_WriteByteProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,Data,PEC)
# SMBus_WriteWordProtocol
def SMBus_WriteWordProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,Data,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	Data = c_ushort(Data)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_WriteWordProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,Data,PEC)
# SMBus_ReadByteProtocol
def SMBus_ReadByteProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode, pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)	
	return GinkgoLib.SMBus_ReadByteProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode, pData,PEC)
# SMBus_ReadWordProtocol
def SMBus_ReadWordProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode, pData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	PEC = c_ubyte(PEC)	
	return GinkgoLib.SMBus_ReadWordProtocol(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode, pData,PEC)	
# SMBus_ProcessCall
def SMBus_ProcessCall(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,WriteData, pReadData,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)
	WriteData = c_ushort(WriteData)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_ProcessCall(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,WriteData, pReadData,PEC)
# SMBus_BlockWrite
def SMBus_BlockWrite(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,pData,ByteCount,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)	
	ByteCount = c_ubyte(ByteCount)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_BlockWrite(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,pData,ByteCount,PEC)
# SMBus_BlockRead
def SMBus_BlockRead(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,pData,pByteCount,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)	
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_BlockRead(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,pData,pByteCount,PEC)
# SMBus_BlockProcessCall
def SMBus_BlockProcessCall(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,pWriteData,WriteByteCount,pReadData,pReadByteCount,PEC):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	SlaveAddr = c_ubyte(SlaveAddr)	
	CommandCode = c_ubyte(CommandCode)	
	WriteByteCount = c_ubyte(WriteByteCount)
	PEC = c_ubyte(PEC)
	return GinkgoLib.SMBus_BlockProcessCall(DevType,DevIndex,SMBusIndex,SlaveAddr,CommandCode,pWriteData,WriteByteCount,pReadData,pReadByteCount,PEC)
def SMBus_GetAlert(DevType,DevIndex,SMBusIndex,pAlertFlag):
	DevType = c_int(DevType)
	DevIndex = c_int(DevIndex)
	SMBusIndex = c_int(SMBusIndex)
	return GinkgoLib.SMBus_GetAlert(DevType,DevIndex,SMBusIndex,pAlertFlag);
	


