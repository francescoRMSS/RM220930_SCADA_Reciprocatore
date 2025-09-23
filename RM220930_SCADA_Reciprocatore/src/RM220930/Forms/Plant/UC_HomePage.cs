using RM.src.RM220930.Classes.Navigator;
using RMLib.Alarms;
using RMLib.MessageBox;
using RMLib.Security;
using RMLib.VATView;
using System;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    /// <summary>
    /// Pagina di home
    /// </summary>
    public partial class UC_HomePage : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        /// <summary>
        /// Costruttore
        /// </summary>
        public UC_HomePage()
        {
            InitializeComponent();
        }

        #region Metodi di UC_HomePage

        /// <summary>
        /// Metodo eseguito dopo la richiesta di navigazione in home page
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {
            // Se il parametro è una stringa con il titolo
            if (parameter is string pageTitle)
            {
                lbl_title.Text = pageTitle;
            }

        }

        #endregion

        #region Eventi di UC_HomePage

        /// <summary>
        /// Apre la gestione dell'asse 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ1(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 1);
        }

        /// <summary>
        /// Apre la gestione dell'asse 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ2(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 2);
        }

        /// <summary>
        /// Apre la gestione dell'asse 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ3(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 3);
        }

        /// <summary>
        /// Apre la gestione dell'asse 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ4(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 4);
        }

        /// <summary>
        /// Apre la gestione dell'asse 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ5(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 5);
        }

        /// <summary>
        /// Apre la gestione dell'asse 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ6(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 6);
        }

        /// <summary>
        /// Apre la gestione dell'asse 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ7(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 7);
        }

        /// <summary>
        /// Apre la gestione dell'asse 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ8(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 8);
        }

        #endregion
    }
}
