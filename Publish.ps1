<#
.SYNOPSIS
    Publishes dotnet packages for supported .NET 5 compatible operating systems
.DESCRIPTION
    Publishes dotnet packages for supported .NET 5 compatible operating systems
.NOTES
    Created by   : asheroto
    Date Coded   : 01/26/2020
    More info:   : https://gist.github.com/asheroto/b8c82ea515e8baa569807108d1d9ed0a
#>

# Change the variables below, then run the script
$BinaryName = "SpawnProcess"                                              # Binary name from build, without the .exe extension
$SolutionOrProjectPath = "."    # Solution or Project file you want to publish
$PublishPath = "./dist"      # The directory you want the published files to go in
$DebugOrRelease = "Release"                                                 # Debug or Release
$PublishSingleFile = "true"                                                 # If true, produces a single file
$SelfContained = "true"                                                     # If true, the binary will run without installing .NET runtime
$RemoveExtraFiles = "true"                                                  # Remove DLL, dylib, and config files
$ReadyToRun = "true"
$Trimmed = "true"
# Change the variables above, then run the script

function Publish {
    param (
        [String] $ArchID
    )

    # Publish
    dotnet publish -r $ArchID -p:PublishSingleFile=$PublishSingleFile --self-contained $SelfContained -c $DebugOrRelease -p:PublishTrimmed=$Trimmed -p:PublishReadyToRun=$ReadyToRun --nologo --output $PublishPath $SolutionOrProjectPath

    Write-Host $ArchID

    if ($ArchID.Contains("win-")) {
        # If architecture is Windows
        $OriginalBinaryName = $BinaryName + ".exe"
        $TargetBinaryName = $BinaryName + "_" + $ArchID + ".exe"
    } else {
        # If architecture not Windows
        $OriginalBinaryName = $BinaryName
        $TargetBinaryName = $BinaryName + "_" + $ArchID
    }

    # Rename original build name to build name + architecture
    Rename-Item ($PublishPath + "/" + $OriginalBinaryName) $TargetBinaryName
}

# Delete existing files in with binary name in publish path
if(Test-Path ($PublishPath)) {
    try {
        Remove-Item ($PublishPath + "/" + $BinaryName + "*")
    } catch {
        
    }
}

# Remove DLL, dylib, and config files if enabled
if($RemoveExtraFiles -eq "true") {
    # Remove DLL, dylib, config files
    Remove-Item ($PublishPath + "/*.dll")
    Remove-Item ($PublishPath + "/*.dylib")
    Remove-Item ($PublishPath + "/*.config")
}

# Publish binaries
# See runtime identifiers here: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
Publish -ArchID win-x64
Publish -ArchID win-x86
Publish -ArchID win-arm
Publish -ArchID win-arm64

# Open folder to publish path
Start-Process explorer.exe -ArgumentList $PublishPath
