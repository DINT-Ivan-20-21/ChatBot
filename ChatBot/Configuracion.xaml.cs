using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        public PropertyInfo ColorFondo { get; set; }
        public PropertyInfo ColorMensajeUsuario { get; set; }
        public PropertyInfo ColorMensajeBot { get; set; }
        public string Sexo { get; set; }

        public Configuracion()
        {
            InitializeComponent();

            DataContext = this;
            FondoComboBox.ItemsSource = typeof(Colors).GetProperties();
            UsuarioComboBox.ItemsSource = typeof(Colors).GetProperties();
            RobotComboBox.ItemsSource = typeof(Colors).GetProperties();
            SexoComboBox.ItemsSource = new string[] { "Hombre", "Mujer" };
        }

        private void GuardarConfiguracionCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
