param(
    [int]
    $PicsNum = 10,

    [string]
    $TargetDirectory,

    [int]
    $ThrottleLimit = 10
)

if($TargetDirectory){
    $TargetDirectory = Resolve-Path $TargetDirectory
} else {
    $TargetDirectory = Resolve-Path $PSScriptRoot
}

$picEndpointUriTemplate = "https://picsum.photos/{Width}/{Height}"

1..$PicsNum | ForEach-Object -ThrottleLimit $ThrottleLimit -Parallel {
    $TargetDirectory = $using:TargetDirectory
    $picEndpointUriTemplate = $using:picEndpointUriTemplate

    $picWidth = Get-Random -Minimum 100 -Maximum 1000
    $picHeight = Get-Random -Minimum 100 -Maximum 1000
    $picEndpointUri = $picEndpointUriTemplate `
        -replace "{Width}", $picWidth `
        -replace "{Height}", $picHeight

    $filename = (New-Guid).ToString() + ".jpg"
    $filePath = "$TargetDirectory/$filename"

    Invoke-WebRequest -Uri $picEndpointUri -OutFile $filePath

    Write-Host "[INFO] Fetched $filename"
}