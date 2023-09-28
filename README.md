# SpawnProcess
Quick and simple application that spawns a new process without blocking execution. A new process is started using shell execute. Ability to launch hidden window.

## Installation
1. Download the latest EXEs from the releases.
2. Move both of the EXEs into `C:\Windows` (not required but is the easiest installation)
3. Launch from any directory by typing `SpawnProcess.exe` or `SpawnProcessHidden.exe`
4. **Note**: the process you spawn needs to end itself... if it doesn't end itself and you launch the program hidden, you will have to kill it with Task Manager to end it!

## Usage

### Executable Name
|Program|Description|
|--|--|
|`SpawnProcess.exe`|Control over window style|
|`SpawnProcessHidden.exe`|Same as SpawnProcess, except when launched, such as from Task Scheduler, no window appears by default, and the launched process is also hidden.|

### Command Line Arguments

|Argument|Required/Optional|Description|
|--|--|--|
|-path|Required|Path to Process|
|-args|Optional|Arguments for Process|
|-windowstyle|Optional|Hidden/Normal/Minimized/Maximized|
|-workingdirectory|Optional|Working Directory of Process|

### Example #1
```powershell
SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-arg1 goesHere -arg2 goesHere" -windowstyle normal -workingdirectory "C:\Path\To"
```

### Example #2
```powershell
SpawnProcess.exe -path "C:\Path\To\Process.exe"
```

### Example #3
```powershell
SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-data -more" -windowstyle minimized
```

### Example #4
```powershell
SpawnProcess.exe -path "C:\Path\To\Process.exe" -args "-data -more" -windowstyle hidden
```

### SpawnProcessHidden example, such as from Task Scheduler Example
Paste this in the Program/Script field (change path as needed):

```powershell
SpawnProcessHidden.exe -path "powershell" -args "-executionpolicy bypass -command ""V:\Path\To\YourScript.ps1"""
```

and then click "OK", and then click "Yes" for the args to move into the "Add arguments" field.

Another example:
```powershell
SpawnProcessHidden.exe -Path "pwsh" -Args "-Command & 'C:\Projects\ChocolateyPackages\fxsound\update.ps1'" -WorkingDirectory "C:\Projects\ChocolateyPackages\ClickUp-Official"
```
