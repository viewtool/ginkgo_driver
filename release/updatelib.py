import os 
import os.path 
import shutil 
import time,  datetime

dllsrcPathwin32 = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\vs2010\\Ginkgo_Driver\\Release\\Ginkgo_Driver.dll'
dllsrcPathwin64 = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\vs2010\\Ginkgo_Driver\\x64\\Release\\Ginkgo_Driver.dll'
libsrcPathwin32 = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\vs2010\\Ginkgo_Driver\\Release\\Ginkgo_Driver.lib'
libsrcPathwin64 = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\vs2010\\Ginkgo_Driver\\x64\\Release\\Ginkgo_Driver.lib'
cblibsrcPathwin = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\vs2010\\Ginkgo_Driver\\x64\\Release\\Ginkgo_Driver_CB.lib'
dllsrcPathgccwin = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\CodeBlocks\\windows\\Ginkgo_Driver\\bin\\Release\\libGinkgo_Driver.dll'
libsrcPathgccwin = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\CodeBlocks\\windows\\Ginkgo_Driver\\bin\\Release\\libGinkgo_Driver.a'
dllsrcPathlinux32 = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\CodeBlocks\\linux_32bit\\Ginkgo_Driver\\bin\\Release\\libGinkgo_Driver.so'
dllsrcPathlinux64 = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\CodeBlocks\\linux_64bit\\Ginkgo_Driver\\bin\\Release\\libGinkgo_Driver.so'
dllsrcPathRPi = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\CodeBlocks\\linux_arm_RaspberryPi\\Ginkgo_Driver\\bin\\Release\\libGinkgo_Driver.so'
dllsrcPathmacos = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\CodeBlocks\\macos\\Ginkgo_Driver\\bin\\Release\\libGinkgo_Driver.dylib'
dllsrcPathAndroid = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Android Driver\\bin\\ginkgo_driver.jar'
headerFilePath = 'E:\\Project\\vt_ginkgo\\truck\\Ginkgo Driver\\Source'
# c++ builder
print('copy c++ builder')
shutil.copy(dllsrcPathwin32,os.getcwd()+'\\lib\\c++ builder') 
shutil.copy(dllsrcPathwin32,os.getcwd()+'\\lib\\c++ builder\\Ginkgo_Driver.lib') 

# gcc_windows
print('copy gcc windows')
shutil.copy(dllsrcPathgccwin,os.getcwd()+'\\lib\\gcc_windows') 
shutil.copy(libsrcPathgccwin,os.getcwd()+'\\lib\\gcc_windows')

# labview_32bit
print('copy labview 32bit')
for root, dirs, files in os.walk(os.getcwd()+'\\lib\\labview_32bit', topdown=False):
    for name in dirs:# Remove buil directory
        if 'Control' in name or 'Bootloader' in name:
            shutil.copy(dllsrcPathwin32,os.getcwd()+'\\lib\\labview_32bit\\'+name)

# labview_64bit
print('copy labview 64bit')
for root, dirs, files in os.walk(os.getcwd()+'\\lib\\labview_64bit', topdown=False):
    for name in dirs:# Remove buil directory
        if 'Control' in name or 'Bootloader' in name:
            shutil.copy(dllsrcPathwin64,os.getcwd()+'\\lib\\labview_64bit\\'+name)

# linux_32bit
print('copy linux 32bit')
shutil.copy(dllsrcPathlinux32,os.getcwd()+'\\lib\\linux_32bit') 
# linux_64bit
print('copy linux 64bit')
shutil.copy(dllsrcPathlinux64,os.getcwd()+'\\lib\\linux_64bit') 
# linux_rpi
print('copy linux Raspberry Pi')
shutil.copy(dllsrcPathRPi,os.getcwd()+'\\lib\\Raspberry Pi') 

# Android
print('copy Android')
shutil.copy(dllsrcPathAndroid,os.getcwd()+'\\lib\\Android\\Ginkgo_Driver.jar') 

# macos
print('copy macos')
shutil.copy(dllsrcPathmacos,os.getcwd()+'\\lib\\macos') 

# msvc_32bit
print('copy msvc 32bit')
shutil.copy(dllsrcPathwin32,os.getcwd()+'\\lib\\msvc_32bit') 
shutil.copy(libsrcPathwin32,os.getcwd()+'\\lib\\msvc_32bit') 

# msvc_64bit
print('copy msvc 64bit')
shutil.copy(dllsrcPathwin64,os.getcwd()+'\\lib\\msvc_64bit') 
shutil.copy(libsrcPathwin64,os.getcwd()+'\\lib\\msvc_64bit') 

# Copy header file
print('Copy header file')
for root, dirs, files in os.walk(headerFilePath, topdown=False):
    for name in files:# Remove buil directory
            if ('Control' in name or 'EasyScale' in name or 'ErrorType' in name)and ('.h' in name):
                shutil.copy(headerFilePath+'\\'+name,os.getcwd()+'\\include\\msvc\\'+name)
