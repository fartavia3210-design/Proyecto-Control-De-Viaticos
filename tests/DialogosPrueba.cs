using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

// Automatización exclusiva de pruebas: nunca se compila dentro de la aplicación.
internal static class DialogosPrueba
{
    public static IDisposable Iniciar(Func<DialogResult> respuesta)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            // La automatización no depende del bucle modal de la interfaz que prueba.
            Console.Error.WriteLine("Automatizador nativo: proceso " + Process.GetCurrentProcess().Id);
            return new System.Threading.Timer(delegate { Responder(respuesta()); }, null, 30, 30);
        }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 30 };
        timer.Tick += delegate { Responder(respuesta()); };
        timer.Start();
        return timer;
    }

    private delegate bool Enumerar(IntPtr ventana, IntPtr dato);
    [DllImport("user32.dll")] private static extern bool EnumWindows(Enumerar callback, IntPtr dato);
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr ventana, out uint proceso);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetClassName(IntPtr ventana, StringBuilder nombre, int largo);
    [DllImport("user32.dll")] private static extern IntPtr GetDlgItem(IntPtr ventana, int id);
    [DllImport("user32.dll")] private static extern bool PostMessage(IntPtr ventana, uint mensaje, IntPtr wparam, IntPtr lparam);

    public static void Responder(DialogResult respuesta)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            EnumWindows(delegate(IntPtr ventana, IntPtr dato)
            {
                uint proceso;
                GetWindowThreadProcessId(ventana, out proceso);
                if (proceso != (uint)Process.GetCurrentProcess().Id) return true;
                StringBuilder clase = new StringBuilder(100);
                GetClassName(ventana, clase, clase.Capacity);
                if (clase.ToString() == "#32770")
                {
                    int boton = (int)respuesta;
                    if (GetDlgItem(ventana, boton) == IntPtr.Zero) boton = 1;
                    IntPtr control = GetDlgItem(ventana, boton);
                    if (control != IntPtr.Zero)
                    {
                        // BM_CLICK reproduce la acción del botón, incluida su notificación
                        // al diálogo, y se procesa después de terminar la enumeración.
                        PostMessage(control, 0x00F5, IntPtr.Zero, IntPtr.Zero);
                    }
                }
                return true;
            }, IntPtr.Zero);
        }
        else
        {
            List<Form> mensajes = new List<Form>();
            foreach (Form f in Application.OpenForms)
                if (f.GetType().Name.Contains("MessageBox")) mensajes.Add(f);
            foreach (Form f in mensajes) f.DialogResult = respuesta;
        }
    }
}
