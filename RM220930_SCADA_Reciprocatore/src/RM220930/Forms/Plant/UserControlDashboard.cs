using RM.src.RM220930.Classes.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    public partial class UserControlDashboard : UserControl, INavigable, INavigationRequester
    {
        public event EventHandler<NavigateEventArgs> NavigateRequested;
        public UserControlDashboard()
        {
            InitializeComponent();
        }

        // Implementazione del contratto INavigable
        public void OnNavigatedTo(object parameter)
        {
            // Esempio: se il parametro è una stringa con il nome utente
            if (parameter is string nomeUtente)
            {
                lblWelcome.Text = $"Benvenuto, {nomeUtente}!";
            }
            else
            {
                lblWelcome.Text = "Benvenuto!";
            }
        }

        private void btn_impostazioni_Click(object sender, EventArgs e)
        {
            // Qui dico esplicitamente di andare a "Impostazioni"
            var args = new NavigateEventArgs("Impostazioni", "Sono arrivato dal Dashboard");
            NavigateRequested?.Invoke(this, args);
        }

    }
}
