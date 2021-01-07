using System.ComponentModel;

namespace ChatBot
{
    class Mensaje : INotifyPropertyChanged
    {
        private bool _esBot;

        public bool EsBot
        {
            get { return _esBot; }
            set
            {
                if (this._esBot != value)
                {
                    this._esBot = value;
                    this.NotifyPropertyChanged("EsBot");
                }
            }
        }


        private string _texto;

        public string Texto
        {
            get { return _texto; }
            set
            {
                if (this._texto != value)
                {
                    _texto = value;
                    this.NotifyPropertyChanged("Texto");
                }
            }
        }

        public Mensaje(bool esBot, string texto)
        {
            EsBot = esBot;
            Texto = texto;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
