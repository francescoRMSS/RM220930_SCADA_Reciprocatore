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
    public partial class UC_MotorPanel : UserControl
    {
        public UC_MotorPanel()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // Metodo per configurare il titolo e altri dati specifici del motore
        public void ConfigurePanel(string motorTitle, int initialValue /*, altri parametri... */)
        {
            // Usa il nome della label del titolo che hai nel designer del pannello
            lb_motorTitle.Text = motorTitle;

            // Configura altri controlli in base ai parametri
            // ...
        }
    }
}
