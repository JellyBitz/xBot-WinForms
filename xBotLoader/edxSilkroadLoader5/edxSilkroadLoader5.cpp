#include <windows.h>
#include <windowsx.h>
#include "../Common/FileChooser.h"
#include "../Common/Silkroad.h"
#include "../Common/ConfigFile.h"
#include "../Common/GetCommonDirectory.h"
#include "../Common/common.h"
#include <string>
#include <sstream>
#include <map>
#include <algorithm>
#include "resource.h"

//-------------------------------------------------------------------------
char* getCmdOption(char ** begin, char ** end, const std::string & option)
{
	char ** itr = std::find(begin, end, option);
	if (itr != end && ++itr != end)
	{
		return *itr;
	}
	return 0;
}

bool cmdOptionExists(char** begin, char** end, const std::string& option)
{
	return std::find(begin, end, option) != end;
}
int main(int argc, char** argv) {
	
	// Reading arguments required
	std::string pathToClient = getCmdOption(argv, argv + argc, "-path");
	if (pathToClient.empty()) {
		MessageBoxA(0, "Error: Path to the client required.", "Arguments Error", MB_ICONERROR);
		MessageBoxA(0, "Arguments used: -path [value required]\n-locale [22]\n-division [0]\nhost [0]\n--userandom", "Arguments Information", MB_ICONERROR);
		return 0;
	}
	std::string locale = getCmdOption(argv, argv + argc, "-locale");
	if (locale.empty()) {
		locale = "22"; // vsro 1.188
	}
	std::string divisionIndex = getCmdOption(argv, argv + argc, "-division");
	if (divisionIndex.empty()) {
		divisionIndex = "0";
	}
	std::string hostIndex = getCmdOption(argv, argv + argc, "-host");
	if (hostIndex.empty()) {
		hostIndex = "0";
	}
	int random = 0;
	if (cmdOptionExists(argv, argv + argc, "--userandom"))
		random = rand() % (999999 + 1);

	// Get directory
	char cd[MAX_PATH + 1] = { 0 };
	GetCurrentDirectoryA(MAX_PATH, cd);
	std::string pathToDll = cd;
	pathToDll += "\\xBotLoader.dll";
	
	// Create process & inject dll
	STARTUPINFOA si = { 0 };
	PROCESS_INFORMATION pi = { 0 };
	si.cb = sizeof(STARTUPINFOA);

	std::stringstream cmdLine;
	std::stringstream args;
	args << random << " /" << locale << " " << divisionIndex << " " << hostIndex;

	cmdLine << "\"" << pathToClient.c_str() << "\" " << args.str().c_str();

	bool result = (0 != CreateProcessA(0, (LPSTR)cmdLine.str().c_str(), 0, NULL, FALSE, CREATE_SUSPENDED, NULL, NULL, &si, &pi));
	if (result == false)
	{
		MessageBoxA(0, "Could not start \"sro_client.exe\".", "Fatal Error", MB_ICONERROR);
		return 0;
	}

	InjectDLL(pi.hProcess, GetFileEntryPoint(pathToClient.c_str()), pathToDll.c_str(), "Initialize");

	ResumeThread(pi.hThread);
	ResumeThread(pi.hProcess);

	// Return the process ID injected, otherwise 0.
	return pi.dwProcessId;
}
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	main(__argc, __argv);
}