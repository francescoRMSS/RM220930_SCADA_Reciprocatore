using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Forms.Plant.Axis;
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
    public partial class UC_axis : UserControl, INavigable, INavigationRequester
    {
        /// <summary>
        /// Gestisce switch tra le varie userControl
        /// </summary>
        private Navigator _navigator;

        /// <summary>
        /// Asse selezionato
        /// </summary>
        private int axeOffset = 0;

        public UC_axis()
        {
            InitializeComponent();
            _navigator = new Navigator(pnl_container);
            RegisterPages();
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public void OnNavigatedTo(object parameter)
        {
            // Esempio: se il parametro è una stringa con il nome utente
            if (parameter is string pageTitle)
            {
                lbl_title.Text = pageTitle;
            }
            else
            if (parameter is int offset)
            {

            }
        }

        private void RegisterPages()
        {
            // Registrazione delle pagine
            _navigator.RegisterPage("Parametri lavoro", typeof(UC_parametriLavoro));
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void ClickEvent_goToParametriLavoro(object sender, EventArgs e)
        {
            _navigator.Navigate("Parametri lavoro", axeOffset);
        }

        private void ClickEvent_selectAxe1(object sender, EventArgs e)
        {
            axeOffset = 1;
        }

        private void ClickEvent_selectAxe2(object sender, EventArgs e)
        {
            axeOffset = 2;
        }
    }
}
