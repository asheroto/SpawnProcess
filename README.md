# SpawnProcess
Quick and simple application that spawns a new process without blocking execution.  A new process is started using shell execute.

## Installation
1. Download the latest EXE from the releases.
2. Move the EXE into C:\Windows
3. Launch from any directory by typing `SpawnProcess.exe`
4. **Note**: the process you spawn needs to end itself... if it doesn't end itself and you launch the program hidden, you will have to kill it with Task Manager to end it!

## Usage
### Executable Name
`SpawnProcess.exe`
### Command Line Arguments

|Argument|Required/Optional|Description|
|--|--|--|
|-path|Required|Path to Process|
|-args|Optional|Arguments for Process (args must be in double-quotes)|
|-windowstyle|Optional|Hidden/Normal/Minimized/Maximized|
|-workingdirectory|Optional|Working Directory of Process|

### Example #1
`SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-arg1 goesHere -arg2 goesHere" -windowstyle normal -workingdirectory "C:\Path\To"`

### Example #2
`SpawnProcess.exe -path "C:\Path\To\Process.exe"`

### Example #3
`SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-data -more" -windowstyle hidden`

## PHP Usage

### Explanation
You can use shell_exec (returns output to you) or shell (does not return output) to spawn a new process without it blocking the script. Meaning once you launch a new process the script will continue running and NOT wait on the result/output of the EXE you run. The only output you will receive is the process ID that is spawned.

### Example
`echo shell_exec("spawnprocess.exe -path php.exe -args \"phpScript.php\" -windowstyle hidden");`
