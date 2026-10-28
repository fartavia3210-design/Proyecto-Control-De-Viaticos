$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
    $compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
    $sources = @(Get-ChildItem ControlViaticos -Recurse -Filter '*.cs' |
        Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' } |
        ForEach-Object { $_.FullName })
    foreach ($suite in @('LiquidacionesRegression', 'CierreRegression', 'AccesoUsuariosRegression', 'EstadosLiquidacionesRegression')) {
        $output = Join-Path $env:TEMP ("ControlViaticos-" + $suite + ".exe")
        $regressionSource = Join-Path $PSScriptRoot ($suite + '.cs')
        $dialogs = Join-Path $PSScriptRoot 'DialogosPrueba.cs'
        & $compiler /nologo /codepage:65001 /target:exe "/main:$suite" "/out:$output" /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Data.dll /r:System.Configuration.dll $sources $regressionSource $dialogs
        if ($LASTEXITCODE -ne 0) { throw "Falló la compilación de $suite." }
        Write-Output "INICIO: $suite"
        $process = New-Object System.Diagnostics.Process
        $process.StartInfo.FileName = $output
        $process.StartInfo.UseShellExecute = $false
        $process.StartInfo.RedirectStandardOutput = $true
        $process.StartInfo.RedirectStandardError = $true
        $process.StartInfo.StandardOutputEncoding = [Text.Encoding]::UTF8
        $process.StartInfo.StandardErrorEncoding = [Text.Encoding]::UTF8
        [void]$process.Start()
        $stdout = $process.StandardOutput.ReadToEndAsync()
        $stderr = $process.StandardError.ReadToEndAsync()
        $finished = $process.WaitForExit(120000)
        if (!$finished) { $process.Kill(); $process.WaitForExit() }
        Write-Output $stdout.Result
        Write-Output $stderr.Result
        $exitCode = $process.ExitCode
        $process.Dispose()
        if (!$finished) { throw "$suite excedió dos minutos; revisar la última comprobación del registro." }
        if ($exitCode -ne 0) { throw "Fallaron las pruebas de $suite (código $exitCode)." }
    }
} finally { Pop-Location }
