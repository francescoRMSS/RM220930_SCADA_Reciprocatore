using RM.src.RM220930.Classes.Navigator;
using System;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    public partial class UserControlImpostazioni : UserControl, INavigable, INavigationRequester
    {
        // Evento per richiedere navigazione
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public UserControlImpostazioni()
        {
            InitializeComponent();
        }

        // Implementazione del contratto INavigable
        public void OnNavigatedTo(object parameter)
        {
            if (parameter is string messaggio)
            {
                lblInfo.Text = $"Sei arrivato con messaggio: {messaggio}";
            }
            else
            {
                lblInfo.Text = "Sezione impostazioni";
            }
        }

        // Esempio: pulsante per tornare alla dashboard
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            var args = new NavigateEventArgs("Dashboard", "Tornato dalle impostazioni");
            NavigateRequested?.Invoke(this, args);
        }
    }
}
