#!/usr/bin/python

import sys, os.path
import shutil

from mod_pbxproj import XcodeProject
projectPath = sys.argv[1]
projectPath = projectPath.replace('_SOOM@#',' ')

project = XcodeProject.Load(projectPath + '/Unity-iPhone.xcodeproj/project.pbxproj')

project.add_file('System/Library/Frameworks/Security.framework', tree='SDKROOT')
project.add_file('usr/lib/libsqlite3.0.dylib', tree='SDKROOT')
project.add_file('System/Library/Frameworks/StoreKit.framework', tree='SDKROOT')
project.add_other_ldflags('-ObjC')

project.saveFormat3_2()
