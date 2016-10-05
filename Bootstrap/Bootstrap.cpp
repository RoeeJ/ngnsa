// Bootstrap.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <stdio.h>
#include <string>
#include <cstring>
#include "MetaHost.h"
#include "MSCorEE.h"
#include <Windows.h>
#include <tchar.h>

#ifndef UNICODE  
typedef std::string String;
#else
typedef std::wstring String;
#endif

std::string getRegKey(const std::string& location, const std::string& name);
String getNGNSAPath();

DWORD WINAPI loadNGNSA(LPVOID lpParam) {
	//MessageBox(0, L"Bootstrap dll loaded!", NULL, MB_OK);
	ICLRMetaHost *pMetaHost = NULL;
	CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&pMetaHost);

	ICLRRuntimeInfo *pRuntimeInfo = NULL;
	pMetaHost->GetRuntime(L"v4.0.30319", IID_ICLRRuntimeInfo, (LPVOID*)&pRuntimeInfo);

	ICLRRuntimeHost *pClrRuntimeHost = NULL;
	pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pClrRuntimeHost);

	pClrRuntimeHost->Start();
	//MessageBox(0, L"CLR Loaded!", NULL, MB_OK);

	// Okay, the CLR is up and running in this (previously native) process.
	// Now call a method on our managed C# class library.
	DWORD dwRet = 0;
	String dllPath = getNGNSAPath();
	String stemp = String(dllPath.begin(), dllPath.end());
	LPCWSTR sw = stemp.c_str();
	if (!dllPath.empty()) {
		//MessageBox(0,sw, L"NGNSA Found!", MB_OK);
		pClrRuntimeHost->ExecuteInDefaultAppDomain(sw, L"NGNSA.Startup", L"EntryPoint", L"", &dwRet);
	}
	return 0;
}
String getNGNSAPath() {
	const int cBuffSize = 255;//Size of buffer
	TCHAR szBuff[cBuffSize] = { 0 },//Actual buffer
		*szPath = //Path of key
		_T("Software\\NoGameNoLife\\NoGameNoStory");
	HKEY hKey = NULL;//Key used to hget value
	DWORD dwSize = cBuffSize; //To tell buff size and recieve actual size
							  //First open a key to allow you to read the registry
	if (RegOpenKeyEx(HKEY_LOCAL_MACHINE,//Main key to browse
		szPath,//sub key
		0,
		KEY_READ,//access rights - we want to read
		&hKey)//Recieve the key we want to use
		!= ERROR_SUCCESS) {
		MessageBox(HWND_DESKTOP, _T("Could not open key"), NULL, MB_OK);
		return std::wstring();
	}

	if (RegQueryValueEx(hKey,//From previous call
		_T("anticheat"),//value we want to look at
		0,
		NULL,//not needed,we know its a string
		(UCHAR*)szBuff,//Put info here
		&dwSize)//How big is the buffer?
		!= ERROR_SUCCESS) {
		MessageBox(HWND_DESKTOP, _T("Could not get value"), NULL, MB_OK);
		return std::wstring();
	}

	RegCloseKey(hKey);//Dont forget to cleanup!!!!
	return String(szBuff);
}
/**
* @param location The location of the registry key. For example "Software\\Bethesda Softworks\\Morrowind"
* @param name the name of the registry key, for example "Installed Path"
* @return the value of the key or an empty string if an error occured.
*/
std::string getRegKey(const std::string& location, const std::string& name){
	HKEY key;
	TCHAR value[1024];
	DWORD bufLen = 1024 * sizeof(TCHAR);
	long ret;
	ret = RegOpenKeyExA(HKEY_LOCAL_MACHINE, location.c_str(), 0, KEY_QUERY_VALUE, &key);
	if (ret != ERROR_SUCCESS){
		return std::string();
	}
	ret = RegQueryValueExA(key, name.c_str(), 0, 0, (LPBYTE)value, &bufLen);
	RegCloseKey(key);
	if ((ret != ERROR_SUCCESS) || (bufLen > 1024 * sizeof(TCHAR))){
		return std::string();
	}
	std::wstring arr_w(value);
	std::string stringValue(arr_w.begin(), arr_w.end());
	size_t i = stringValue.length();
	while (i > 0 && stringValue[i - 1] == '\0'){
		--i;
	}
	return stringValue.substr(0, i);
}