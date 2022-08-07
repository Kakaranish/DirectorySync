param (
    [string]
    $SourceDirectory,

    [string]
    $TargetDirectory
)

$ErrorActionPreference = 'Stop'

$files = Get-ChildItem -Path $SourceDirectory -File -Recurse | Sort-Object {Get-Random}
if(-not $files.Length) {
    Write-Error "No files found in src directory"
}

$minDirCount = [Math]::Floor($files.Length / 4)
if($minDirCount -lt 1) {
    $minDirCount = 1
}
$maxDirCount = [Math]::Floor($files.Length / 3 * 2)
if($maxDirCount -lt 1) {
    $maxDirCount = 1
}

$dirCount = [int] (Get-Random -Minimum $minDirCount -Maximum $maxDirCount)
Write-Host "[INFO] Directories count to be created $dirCount"

New-Item -ItemType Directory $TargetDirectory -Force | Out-Null

function RandomlyGenerateDirectory {
    param(
        [int] $index,
        [string] $rootDirectory
    )

    $dirName = "$index`_" + (Get-Date).Ticks.ToString()
    $dirPath = (Join-Path $rootDirectory $dirName).ToString()
    New-Item -ItemType Directory $dirPath -Force | Out-Null

    $shouldGenerateSubDir = (Get-Random -Minimum 1 -Maximum 3) -eq 2
    if($shouldGenerateSubDir) {
        RandomlyGenerateDirectory -index $index -rootDirectory $dirPath
    }
}

foreach ($dirIndex in 1..$dirCount) {
    RandomlyGenerateDirectory -index $dirIndex -rootDirectory $TargetDirectory
}

$createdDirectories = Get-ChildItem -Directory -Recurse -Path $TargetDirectory | Select-Object -ExpandProperty FullName

foreach($file in $files) {
    $randomDirIndex = Get-Random -Minimum 0 -Maximum $createdDirectories.Length
    $randomDir = $createdDirectories[$randomDirIndex]
        
    Copy-Item -Path $file -Destination (Join-Path $randomDir $file.Name)
}
