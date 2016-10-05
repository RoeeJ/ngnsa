#include "stdafx.h"
#include <stdio.h>
#include <string>
#include "MetaHost.h"
#include "MSCorEE.h"

#ifndef UNICODE  
typedef std::string String;
#else
typedef std::wstring String;
#endif

std::string getRegKey(const std::string& location, const std::string& name);
DWORD WINAPI loadNGNSA(LPVOID lpParam);
String getNGNSAPath();