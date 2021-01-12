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
        public Configuracion()
        {
            InitializeComponent();
            FondoComboBox.ItemsSource = typeof(Colors).GetProperties();
            FondoComboBox.SelectedItem = typeof(Colors).GetProperty(Properties.Settings.Default.colorFondo);
            UsuarioComboBox.ItemsSource = typeof(Colors).GetProperties();
            UsuarioComboBox.SelectedItem = typeof(Colors).GetProperty(Properties.Settings.Default.colorMensajeUsuario);
            RobotComboBox.ItemsSource = typeof(Colors).GetProperties();
            RobotComboBox.SelectedItem = typeof(Colors).GetProperty(Properties.Settings.Default.colorMensajeBot);
            SexoComboBox.ItemsSource = new string[] { "Hombre", "Mujer" };
        }

        private void GuardarConfiguracionCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Properties.Settings.Default.colorFondo = (FondoComboBox.SelectedItem as PropertyInfo).Name;
            Properties.Settings.Default.colorMensajeUsuario = (UsuarioComboBox.SelectedItem as PropertyInfo).Name;
            Properties.Settings.Default.colorMensajeBot = (RobotComboBox.SelectedItem as PropertyInfo).Name;
            Properties.Settings.Default.sexo = SexoComboBox.SelectedItem.ToString();
            Properties.Settings.Default.Save();

            DialogResult = true;
        }
    }
}
