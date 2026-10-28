using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

// Automatización exclusiva de pruebas: nunca se compila dentro de la aplicación.
internal static class DialogosPrueba
{
    private delegate bool Enumerar(IntPtr ventana, IntPtr dato);
    [DllImport("kernel32.dll")] private static extern uint GetCurrentThreadId();
    [DllImport("user32.dll")] private static extern bool EnumThreadWindows(uint hilo, Enumerar callback, IntPtr dato);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetClassName(IntPtr ventana, StringBuilder nombre, int largo);
    [DllImport("user32.dll")] private static extern IntPtr GetDlgItem(IntPtr ventana, int id);
    [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr ventana, uint mensaje, IntPtr wparam, IntPtr lparam);

    public static void Responder(DialogResult respuesta)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            EnumThreadWindows(GetCurrentThreadId(), delegate(IntPtr ventana, IntPtr dato)
            {
                StringBuilder clase = new StringBuilder(100);
                GetClassName(ventana, clase, clase.Capacity);
                if (clase.ToString() == "#32770")
                {
                    int boton = (int)respuesta;
                    if (GetDlgItem(ventana, boton) == IntPtr.Zero) boton = 1;
                    SendMessage(ventana, 0x0111, new IntPtr(boton), IntPtr.Zero);
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
