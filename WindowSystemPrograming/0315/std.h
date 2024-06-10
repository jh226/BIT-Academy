//std.h
#pragma once

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include "resource.h"

#define WM_CONNECTHANDLE	WM_USER+100

#include "data.h"
#include "handler.h"
#include "ipc.h"
#include "ui.h"
#include "control.h"