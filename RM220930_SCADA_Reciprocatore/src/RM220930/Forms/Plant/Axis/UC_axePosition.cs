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

namespace RM.src.RM220930.Forms.Plant.Axis
{
    public partial class UC_axePosition : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        public UC_axePosition()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gestisce l'utilizzo del parametro passato durante la navigazione
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {
          
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Cmd_Distance
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Cmd_Advance
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Cmd_Delay
        }
    }
}
