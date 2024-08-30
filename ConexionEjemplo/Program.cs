using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionEjemplo
{
    internal static class Program //Declara una clase Program que es static (no puede instanciarse) y internal (solo es accesible dentro del mismo ensamblado). Esta clase contiene el punto de entrada de la aplicación.
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread] //Es un atributo que indica que el modelo de subprocesos para la aplicación es de un único subproceso (Single Thread Apartment), que es necesario para ejecutar Windows Forms.

        static void Main() //Define el método Main, que es el punto de entrada de la aplicación. Es donde comienza la ejecución del programa.
        {
            Application.EnableVisualStyles(); //Activa estilos visuales para la aplicación, lo que le da un aspecto moderno y acorde con el sistema operativo.
            Application.SetCompatibleTextRenderingDefault(false); //Establece si la aplicación debe usar la representación de texto compatible con versiones anteriores o la predeterminada. false indica que se usará la representación de texto predeterminada.
            Application.Run(new Form1()); //Inicia la aplicación y muestra el formulario principal (Form1). Este método bloquea el hilo hasta que se cierre el formulario.
        }
    }
}
