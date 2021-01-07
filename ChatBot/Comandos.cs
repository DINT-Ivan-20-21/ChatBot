using System.Windows.Input;

namespace ChatBot
{
    internal static class Comandos
    {
        public static readonly RoutedUICommand Salir = new RoutedUICommand(
            "_Salir",
            "Salir",
            typeof(Comandos),
            new InputGestureCollection
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand ComprobarConexion = new RoutedUICommand(
            "Comprobar conexión",
            "ComprobarConexion",
            typeof(Comandos),
            new InputGestureCollection
            {
                new KeyGesture(Key.O, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand Configuracion = new RoutedUICommand(
            "Configuración",
            "Configuracion",
            typeof(Comandos),
            new InputGestureCollection
            {
                new KeyGesture(Key.C, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand NuevaConversacion = new RoutedUICommand(
            "_Nueva conversación",
            "NuevaConversacion",
            typeof(Comandos),
            new InputGestureCollection
            {
                new KeyGesture(Key.N, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand GuardarConversacion = new RoutedUICommand(
            "_Guardar Conversación...",
            "GuardarConversacion",
            typeof(Comandos),
            new InputGestureCollection
            {
                new KeyGesture(Key.G, ModifierKeys.Control)
            });
    }
}
