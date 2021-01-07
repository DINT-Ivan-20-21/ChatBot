using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Mensaje> mensajes;
        public MainWindow()
        {
            mensajes = new ObservableCollection<Mensaje>();
            InitializeComponent();
            MensajesItemsControl.ItemsSource = mensajes;
        }

        private void Salir_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ComprobarConexion_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Conexión correcta con el servidor del bot",
                            "Comprobar conexión",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void Configuracion_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Configuracion_CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        private void NuevaConversacion_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mensajes.Clear();
        }
        private void GuardarConversacion_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog guardarDialogo = new SaveFileDialog();
                guardarDialogo.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (guardarDialogo.ShowDialog() == true)
                {
                    using (StreamWriter sw = new StreamWriter(guardarDialogo.OpenFile()))
                    {
                        foreach (Mensaje mensaje in mensajes)
                        {
                            sw.WriteLine($"{(mensaje.EsBot ? "Robot" : "Usuario")}-> {mensaje.Texto}");
                        }
                    }
                }
            }catch(IOException ioe)
            {
                MessageBox.Show("Se produjo un error al guardar la conversacion",
                           "Guardar conversación",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,
                           "Guardar conversación",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);
            }

        }

        private void Conversacion_CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mensajes.Count > 0;
        }

        private void MandarMensaje_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mensajes.Add(new Mensaje(false, ChatTextBox.Text));
            mensajes.Add(new Mensaje(true, "Lo siento, estoy un poco cansado para hablar."));
            ChatTextBox.Text = "";
        }

        private void MandarMensaje_CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ChatTextBox != null && ChatTextBox.Text.Length > 0;
        }
    }
}
