$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
    $compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
    $sources = @(Get-ChildItem ControlViaticos -Recurse -Filter '*.cs' |
        Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' } |
        ForEach-Object { $_.FullName })
    foreach ($suite in @('LiquidacionesRegression', 'CierreRegression', 'AccesoUsuariosRegression')) {
        $output = Join-Path $env:TEMP ("ControlViaticos-" + $suite + ".exe")
        $regressionSource = Join-Path $PSScriptRoot ($suite + '.cs')
        $dialogs = Join-Path $PSScriptRoot 'DialogosPrueba.cs'
        & $compiler /nologo /codepage:65001 /target:exe "/main:$suite" "/out:$output" /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Data.dll /r:System.Configuration.dll $sources $regressionSource $dialogs
        if ($LASTEXITCODE -ne 0) { throw "Falló la compilación de $suite." }
        & $output
        if ($LASTEXITCODE -ne 0) { throw "Fallaron las pruebas de $suite." }
    }
} finally { Pop-Location }
