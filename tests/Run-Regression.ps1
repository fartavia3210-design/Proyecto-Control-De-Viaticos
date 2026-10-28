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
        $log = Join-Path $env:TEMP ($suite + '.stdout.log')
        $errors = Join-Path $env:TEMP ($suite + '.stderr.log')
        Write-Output "INICIO: $suite"
        $process = Start-Process -FilePath $output -PassThru -NoNewWindow -RedirectStandardOutput $log -RedirectStandardError $errors
        $finished = $process.WaitForExit(120000)
        if (!$finished) { $process.Kill(); $process.WaitForExit() }
        Get-Content $log
        Get-Content $errors
        if (!$finished) { throw "$suite excedió dos minutos; revisar la última comprobación del registro." }
        if ($process.ExitCode -ne 0) { throw "Fallaron las pruebas de $suite." }
    }
} finally { Pop-Location }
