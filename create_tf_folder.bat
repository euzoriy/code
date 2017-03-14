@echo off
@break off
@title Create folder with batch but only if it doesn't already exist - D3F4ULT
@color 0a
@cls

setlocal EnableDelayedExpansion

:: Only one single command line is needed to receive user input
::FOR /F "tokens=*" %%A IN ('TYPE CON') DO SET INPUT=%%A
:: Use quotes if you want to display redirection characters as well
set /p INPUT="Enter TF - folder name: "


SET WORK_FOLDER=L:\Tooling Fixtures\
::SET WORK_FOLDER=L:\Tooling Fixtures\TF3118\

ECHO folder name "%WORK_FOLDER%%INPUT%" will be created.

if not exist "%WORK_FOLDER%%INPUT%" (
  mkdir "%WORK_FOLDER%%INPUT%"
  xcopy /s/e "%WORK_FOLDER%TEMPLATE" "%WORK_FOLDER%%INPUT%"  

  if "!errorlevel!" EQU "0" (
    echo Folder created successfully
  ) else (
    echo Error while creating folder
  )
) else (
  echo Folder already exists
)

pause
start explorer "%WORK_FOLDER%%INPUT%"
exit