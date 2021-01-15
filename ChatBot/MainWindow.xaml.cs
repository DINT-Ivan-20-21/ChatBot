using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker;
using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatBot
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Mensaje> mensajes;

        public MainWindow()
        {
            mensajes = new ObservableCollection<Mensaje>();
            InitializeComponent();
            ChatItemsControl.ItemsSource = mensajes;
        }

        private void Salir_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void ComprobarConexion_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string estado = "";
            try
            {
                var cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Properties.Settings.Default.EndPointKey)) { RuntimeEndpoint = Properties.Settings.Default.EndPoint };
                QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(Properties.Settings.Default.KnowledgeBaseId, new QueryDTO { Question = "Hola" });
                estado = "Conexión correcta con el servidor del bot";
            }
            catch (IOException ioex)
            {
                estado = "No se puedo establecer la conexión con el bot";
            }
            MessageBox.Show(estado,
                            "Comprobar conexión",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

        }

        private void Configuracion_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Configuracion configuracion = new Configuracion();
            configuracion.Owner = this;

            configuracion.ColorFondo = typeof(Colors).GetProperty("LightYellow").Name;
            configuracion.ColorMensajeUsuario = typeof(Colors).GetProperty(Properties.Settings.Default.colorMensajeUsuario).Name;
            configuracion.ColorMensajeBot = typeof(Colors).GetProperty(Properties.Settings.Default.colorMensajeBot).Name;
            configuracion.Sexo = Properties.Settings.Default.sexo;
            
            if(configuracion.ShowDialog() == true)
            {
                Properties.Settings.Default.colorFondo = configuracion.ColorFondo;
                Properties.Settings.Default.colorMensajeUsuario = configuracion.ColorMensajeUsuario;
                Properties.Settings.Default.colorMensajeBot = configuracion.ColorMensajeBot;
                Properties.Settings.Default.sexo = configuracion.Sexo;
                Properties.Settings.Default.Save();
            }
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
            }
            catch (IOException ioe)
            {
                MessageBox.Show("Se produjo un error al guardar la conversacion",
                           "Guardar conversación",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error);
            }
            catch (Exception ex)
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

        private async void MandarMensaje_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mensajes.Add(new Mensaje(false, ChatTextBox.Text));
            Mensaje mensajeBot = new Mensaje(true, "Pensando...");
            mensajes.Add(mensajeBot);

            string respuesta = "";
            try
            {
                var cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Properties.Settings.Default.EndPointKey)) { RuntimeEndpoint = Properties.Settings.Default.EndPoint };
                QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(Properties.Settings.Default.KnowledgeBaseId, new QueryDTO { Question = ChatTextBox.Text });
                respuesta = response.Answers[0].Answer;
            }
            catch (IOException ioex)
            {
                respuesta = "Estoy muy cansado para hablar";
            }

            mensajeBot.Texto = respuesta;
            ChatTextBox.Text = "";
            ChatScrollViewer.ScrollToVerticalOffset(ChatScrollViewer.ExtentHeight);
        }

        private void MandarMensaje_CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ChatTextBox != null && ChatTextBox.Text.Length > 0;
        }
    }
}
