using RM.src.RM220930.Classes;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Forms.Plant.Axis;
using RMLib.PLC;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    /// <summary>
    /// Gestisce la seleziona degli assi e delle relative proprietà. Ogni asse ha un indice (offset) che servirà
    /// da dscriminante durante la lettura e scrittura della proprietà relative.
    /// </summary>
    public partial class UC_axis : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        #region Proprietà di UC_axis

        /// <summary>
        /// Gestisce switch tra le varie userControl degli assi
        /// </summary>
        private Navigator _navigator;

        /// <summary>
        /// Asse selezionato
        /// </summary>
        public static int axeOffset = 1;

        #endregion

        /// <summary>
        /// Costruttura user control axis
        /// </summary>
        public UC_axis()
        {
            InitializeComponent();

            // Istanzio il navigator assegnandogli il panel contenitore
            _navigator = new Navigator(pnl_container);

            // Registro le pagine che dovrà genire il panel contenitore
            RegisterPages();
        }

        #region Metodi di UC_axis

        /// <summary>
        /// Gestisce l'utilizzo del parametro passato durante la navigazione
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {
            // Se il parametro è il numero di asse
            if (parameter is int offset)
            {
                axeOffset = offset;   
            }
            else // se non c'è imposto 1 di default
            {
                axeOffset = 1;
            }

            SelectAxe(); // Seleziono graficamente l'asse

            if (!_navigator.HasCurrentPage)
            {
                _navigator.Navigate("Work Params", axeOffset);
            }
        }

        /// <summary>
        /// Ripristina colore dei button degli assi
        /// </summary>
        private void RestoreButtonColor()
        {
            btn_rec.BackColor = SystemColors.ActiveBorder;
            btn_axe1.BackColor = SystemColors.ActiveBorder;
            btn_axe2.BackColor = SystemColors.ActiveBorder;
            btn_axe3.BackColor = SystemColors.ActiveBorder;
            btn_axe4.BackColor = SystemColors.ActiveBorder;
            btn_axe5.BackColor = SystemColors.ActiveBorder;
            btn_axe6.BackColor = SystemColors.ActiveBorder;
            btn_axe7.BackColor = SystemColors.ActiveBorder;
            btn_axe8.BackColor = SystemColors.ActiveBorder;
        }

        /// <summary>
        /// Seleziona graficamente l'asse
        /// </summary>
        private void SelectAxe()
        {
            switch (axeOffset)
            {
                case 0:
                    RestoreButtonColor();
                    //pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
                    btn_rec.BackColor = Color.DimGray;
                    break;

                case 1:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
                    btn_axe1.BackColor = Color.DimGray;
                    break;

                case 2:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
                    btn_axe2.BackColor = Color.DimGray;
                    break;

                case 3:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
                    btn_axe3.BackColor = Color.DimGray;
                    break;

                case 4:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
                    btn_axe4.BackColor = Color.DimGray;
                    break;

                case 5:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
                    btn_axe5.BackColor = Color.DimGray;
                    break;

                case 6:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
                    btn_axe6.BackColor = Color.DimGray;
                    break;

                case 7:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
                    btn_axe7.BackColor = Color.DimGray;
                    break;

                case 8:
                    RestoreButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
                    btn_axe8.BackColor = Color.DimGray;
                    break;
            }
        }

        /// <summary>
        /// Registra le pagine che verranno switchate all'interno del panel contenitore
        /// </summary>
        private void RegisterPages()
        {
            // Registrazione delle pagine
            _navigator.RegisterPage("Work Params", typeof(UC_workParams));
            _navigator.RegisterPage("Axe Position", typeof(UC_axePosition));
            _navigator.RegisterPage("Axe Configuration", typeof(UC_axeConfiguration));

            UserControl page;
            page = (UserControl)Activator.CreateInstance(typeof(UC_workParams));
            page.Dock = DockStyle.Fill;
            _navigator._cache["Work Params"] = page;

        }

        #endregion

        #region Eventi di UC_axis

        #region Selezione assi

        /// <summary>
        /// Seleziona rec
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectRec(object sender, EventArgs e)
        {
            axeOffset = 0;
            RestoreButtonColor();
            //pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
            btn_rec.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe1(object sender, EventArgs e)
        {
            axeOffset = 1;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
            btn_axe1.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe2(object sender, EventArgs e)
        {
            axeOffset = 2;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
            btn_axe2.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe3(object sender, EventArgs e)
        {
            axeOffset = 3;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
            btn_axe3.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe4(object sender, EventArgs e)
        {
            axeOffset = 4;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
            btn_axe4.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe5(object sender, EventArgs e)
        {
            axeOffset = 5;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
            btn_axe5.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe6(object sender, EventArgs e)
        {
            axeOffset = 6;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
            btn_axe6.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe7(object sender, EventArgs e)
        {
            axeOffset = 7;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
            btn_axe7.BackColor = Color.DimGray;
        }

        /// <summary>
        /// Seleziona l'asse 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe8(object sender, EventArgs e)
        {
            axeOffset = 8;
            RestoreButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
            btn_axe8.BackColor = Color.DimGray;
        }

        #endregion

        #region selezione impostazione

        /// <summary>
        /// Apre la pagina dei parametri di lavoro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToWorkParams(object sender, EventArgs e)
        {
            _navigator.Navigate("Work Params", axeOffset);
        }

        /// <summary>
        /// Apre la pagina delle posizione asse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToAxePosition(object sender, EventArgs e)
        {
            _navigator.Navigate("Axe Position", axeOffset);
        }

        /// <summary>
        /// Apre la pagina della configurazione asse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToAxeConfiguration(object sender, EventArgs e)
        {
            _navigator.Navigate("Axe Configuration", axeOffset);
        }

        #endregion

        #endregion
    }
}
