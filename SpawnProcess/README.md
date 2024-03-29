# SpawnProcess
Quick and simple application that spawns a new process without blocking execution. A new process is started using shell execute.

## Installation
1. Download the latest EXEs from the releases.
2. Move both of the EXEs into C:\Windows
3. Launch from any directory by typing `SpawnProcess.exe` or `SpawnProcessHidden.exe`
4. **Note**: the process you spawn needs to end itself... if it doesn't end itself and you launch the program hidden, you will have to kill it with Task Manager to end it!

## Usage
### Executable Name
`SpawnProcess.exe`

and

`SpawnProcessHidden.exe`

SpawnProcessHidden is the same as SpawnProcess, except when launched, such as from Task Scheduler, no window appears, and the launched process is also hidden.

### Command Line Arguments

|Argument|Required/Optional|Description|
|--|--|--|
|-path|Required|Path to Process|
|-args|Optional|Arguments for Process|
|-windowstyle|Optional|Hidden/Normal/Minimized/Maximized|
|-workingdirectory|Optional|Working Directory of Process|

### Example #1
`SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-arg1 goesHere -arg2 goesHere" -windowstyle normal -workingdirectory "C:\Path\To"`

### Example #2
`SpawnProcess.exe -path "C:\Path\To\Process.exe"`

### Example #3
`SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-data -more" -windowstyle hidden`

### SpawnProcessHidden example, such as from Task Scheduler Example
Paste this in the Program/Script field:

`SpawnProcessHidden.exe -path "powershell" -args "-executionpolicy bypass -command ""V:\Path\To\YourScript.ps1"""`

and then click "OK", and then click "Yes" for the args to move into the "Add arguments" field.

Notice the pair of double quotes before and after the Command argument. A pair of double quotes escapes the args argument double quotes at the beginning and end (so double quotes will work inside a set of another double quotes). 