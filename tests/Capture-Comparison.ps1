$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
    $compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
    $sources = @(Get-ChildItem ControlViaticos -Recurse -Filter '*.cs' |
        Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' } | ForEach-Object { $_.FullName })
    $output = Join-Path $env:TEMP 'ControlViaticos-Capturas.exe'
    & $compiler /nologo /codepage:65001 /target:exe /main:CapturasFinales "/out:$output" /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Data.dll /r:System.Configuration.dll $sources (Join-Path $PSScriptRoot 'CapturasFinales.cs')
    if ($LASTEXITCODE -ne 0) { throw 'Falló la compilación de capturas.' }
    & $output (Join-Path (Get-Location) 'screenshots')
    if ($LASTEXITCODE -ne 0) { throw 'Falló la captura de formularios.' }
} finally { Pop-Location }
