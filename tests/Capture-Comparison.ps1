$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
Set-Location $root
New-Item -ItemType Directory -Force screenshots | Out-Null
$harness = @'
using System;
using System.Drawing;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;
using ControlViaticos.Presentacion;
class Capture {
 static T C<T>(Form f,string n) where T:Control {return (T)f.Controls.Find(n,true)[0];}
 [STAThread] static void Main(string[] args) {
 Application.EnableVisualStyles();
 Usuario u=new Usuario {id=1,nombreCompleto="Administrador de ejemplo",nombreUsuario="demo",clave="demo",tipo="Administrador",activo=true};
 Estructuras.listaUsuarios.Add(u); SesionActual.usuarioActual=u;
 Estructuras.listaEmpleados.Add(new Empleado {id=42,nombre="Ana",apellido1="Mora",apellido2="Solis",activo=true});
 Estructuras.listaTiposViatico.Add(new TipoViatico {id=83,nombre="Desayuno",monto=3000,activo=true});
 Estructuras.listaLiquidaciones.Add(new Liquidacion {id=101,idEmpleado=42,inicio=new DateTime(2026,10,25,6,0,0),fin=new DateTime(2026,10,25,15,0,0),justificacion="Visita a oficina regional"});
 Estructuras.listaDetalles.Add(new DetalleLiquidacion {idLiquidacion=101,idViatico=83,monto=3000});
 using(Form f=new frmLiquidaciones()) {
 f.FormBorderStyle=FormBorderStyle.FixedSingle; f.StartPosition=FormStartPosition.Manual; f.Location=new Point(10,10); f.ClientSize=new Size(900,700); f.Show(); Application.DoEvents();
 C<TextBox>(f,"txtConsecutivo").Text="101";
 C<TextBox>(f,"txtJustificacion").Text="Visita a oficina regional";
 C<DateTimePicker>(f,"dtpInicio").Value=new DateTime(2026,10,25,6,0,0);
 C<DateTimePicker>(f,"dtpFin").Value=new DateTime(2026,10,25,15,0,0);
 if(args[0]=="despues") { f.GetType().GetField("preparacionModificada",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).SetValue(f,false); f.GetType().GetMethod("btnBuscar_Click",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(f,new object[]{f,EventArgs.Empty}); }
 else { C<ComboBox>(f,"cmbEmpleado").SelectedIndex=0; C<DataGridView>(f,"grdDetalle").DataSource=new System.Collections.Generic.List<DetalleLiquidacion>(Estructuras.listaDetalles); C<DataGridView>(f,"grdLiquidaciones").DataSource=new System.Collections.Generic.List<Liquidacion>(Estructuras.listaLiquidaciones); C<Label>(f,"lblMontoTotal").Text="₡ 3000"; }
 Application.DoEvents(); Save(f,args[1]+"/liquidaciones-"+args[0]+".png");
 f.ClientSize=new Size(680,400); Application.DoEvents(); Save(f,args[1]+"/ventana-pequena-"+args[0]+".png");
 if(args[0]=="despues") {
   f.ClientSize=new Size(900,700);
   f.Scale(new SizeF(1.25f,1.25f)); f.ClientSize=new Size(900,700);
   Application.DoEvents(); Save(f,args[1]+"/liquidaciones-escala-simulada-125.png");
   f.Scale(new SizeF(1.2f,1.2f)); f.ClientSize=new Size(900,700);
   Application.DoEvents(); Save(f,args[1]+"/liquidaciones-escala-simulada-150.png");
 }
 }
 using(Form f=new frmUsuarios()) {f.FormBorderStyle=FormBorderStyle.FixedSingle;f.StartPosition=FormStartPosition.Manual;f.Location=new Point(10,10);f.ClientSize=new Size(700,430);f.Show();Application.DoEvents();C<ComboBox>(f,"cmbTipo").Text="Administrador";Application.DoEvents();Save(f,args[1]+"/usuarios-"+args[0]+".png");}
 }
 static void Save(Form f,string path) {using(Bitmap b=new Bitmap(f.Width,f.Height)){ f.DrawToBitmap(b,new Rectangle(0,0,b.Width,b.Height));b.Save(path,System.Drawing.Imaging.ImageFormat.Png);}}
}
'@
$harnessPath = Join-Path $env:TEMP 'Capture.cs'
[IO.File]::WriteAllText($harnessPath,$harness,[Text.Encoding]::UTF8)
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
foreach ($version in @('despues','antes')) {
    $sourceRoot = Join-Path $env:TEMP "capture-$version"
    New-Item -ItemType Directory -Force $sourceRoot | Out-Null
    if ($version -eq 'antes') {
        git archive 78fdfaa67a70059c5e8870555cb9adae0e16bbdf -o "$sourceRoot/source.zip"
        Expand-Archive "$sourceRoot/source.zip" "$sourceRoot/source" -Force
        $sources = @(Get-ChildItem "$sourceRoot/source/ControlViaticos" -Recurse -Filter '*.cs' | ForEach-Object {$_.FullName})
    } else {
        $sources = @(Get-ChildItem ControlViaticos -Recurse -Filter '*.cs' | Where-Object {$_.FullName -notmatch '[\\/](obj|bin)[\\/]'} | ForEach-Object {$_.FullName})
    }
    $executable = Join-Path $sourceRoot 'Capture.exe'
    & $compiler /nologo /codepage:65001 /target:exe /main:Capture "/out:$executable" /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Data.dll /r:System.Configuration.dll $sources $harnessPath
    if ($LASTEXITCODE -ne 0) {throw 'Compilation failed'}
    & $executable $version "$root/screenshots"
    if ($LASTEXITCODE -ne 0) {throw 'Capture failed'}
}

Get-ChildItem screenshots -Filter '*.png' | ForEach-Object { Write-Output ('CAPTURE:' + $_.Name + ':' + [Convert]::ToBase64String([IO.File]::ReadAllBytes($_.FullName))) }
