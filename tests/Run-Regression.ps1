$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
    $compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
    $output = Join-Path $env:TEMP 'ControlViaticos-LiquidacionesRegression.exe'
    $sources = @(Get-ChildItem ControlViaticos -Recurse -Filter '*.cs' |
        Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' } |
        ForEach-Object { $_.FullName })
    & $compiler /nologo /target:exe /main:LiquidacionesRegression "/out:$output" /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Data.dll /r:System.Configuration.dll $sources tests/LiquidacionesRegression.cs
    if ($LASTEXITCODE -ne 0) { throw 'Falló la compilación de regresión.' }
    & $output
    if ($LASTEXITCODE -ne 0) { throw 'Fallaron las pruebas de regresión.' }
} finally { Pop-Location }
