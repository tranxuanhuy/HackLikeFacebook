#include <FileConstants.au3>
#include <MsgBoxConstants.au3>
#include <WinAPIFiles.au3>
#include <File.au3>


$folder = ""
Global $list
Local $timeoutLoad = 5000
Opt("SendKeyDelay", 50)

clickFirefox()

Func clickFirefox()

      ;~ doc line socks thu $i de nap vao firefox
	   ; Create a constant variable in Local scope of the filepath that will be read/written to.
	   Local $sFilePath = $folder & "toAutoitData.txt"

	   ; Open the file for reading and store the handle to a variable.
	   Local $hFileOpen = FileOpen($sFilePath, $FO_READ)
	   If $hFileOpen = -1 Then
		   MsgBox($MB_SYSTEMMODAL, "", "An error occurred when reading the file.")
		   Return False
	   EndIf

	   ; Read the fist line of the file using the handle returned by FileOpen.
	   Local $sFileRead = FileReadLine($hFileOpen, 1)
;~ 	   		   MsgBox($MB_SYSTEMMODAL, "", $sFileRead)

;	   Local $aSocks = StringSplit($sFileRead,":")

	   ; Close the handle returned by FileOpen.
	   FileClose($hFileOpen)

	   ;~    click flag
	  Opt("WinTitleMatchMode",2)
	   WinActivate("Astrill")
	  WinWaitActive("Astrill")
	  MouseClick("left",1268, 600)

	  For $i = $sFileRead To 1 Step -1
    Send("{DOWN}")
	Sleep(100)
Next
 Send("{enter}")

EndFunc   ;==>Example