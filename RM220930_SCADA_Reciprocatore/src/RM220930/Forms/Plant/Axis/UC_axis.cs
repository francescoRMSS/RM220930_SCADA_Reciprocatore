using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Forms.Plant.Axis;
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
        private int axeOffset = 1;

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
            // Se il parametro è una stringa lo utilizzo come titolo della pagina
            if (parameter is int offset)
            {
                axeOffset = offset;
                SelectAxe();
                _navigator.Navigate("Work Params", axeOffset);
            }
            else
            {
                _navigator.Navigate("Work Params", 1);
            }
        }

        private void RestorButtonColor()
        {
            btn_axe1.BackColor = SystemColors.ControlDarkDark;
            btn_axe2.BackColor = SystemColors.ControlDarkDark;
            btn_axe3.BackColor = SystemColors.ControlDarkDark;
            btn_axe4.BackColor = SystemColors.ControlDarkDark;
            btn_axe5.BackColor = SystemColors.ControlDarkDark;
            btn_axe6.BackColor = SystemColors.ControlDarkDark;
            btn_axe7.BackColor = SystemColors.ControlDarkDark;
            btn_axe8.BackColor = SystemColors.ControlDarkDark;
        }

        private void SelectAxe()
        {
            switch (axeOffset)
            {
                case 1:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
                    btn_axe1.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 2:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
                    btn_axe2.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 3:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
                    btn_axe3.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 4:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
                    btn_axe4.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 5:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
                    btn_axe5.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 6:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
                    btn_axe6.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 7:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
                    btn_axe7.BackColor = SystemColors.ButtonHighlight;
                    break;

                case 8:
                    RestorButtonColor();
                    pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
                    btn_axe8.BackColor = SystemColors.ButtonHighlight;
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
        }

        #endregion

        #region Eventi di UC_axis

        #region Selezione assi

        /// <summary>
        /// Seleziona l'asse 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe1(object sender, EventArgs e)
        {
            axeOffset = 1;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
            btn_axe1.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe2(object sender, EventArgs e)
        {
            axeOffset = 2;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
            btn_axe2.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe3(object sender, EventArgs e)
        {
            axeOffset = 3;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
            btn_axe3.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe4(object sender, EventArgs e)
        {
            axeOffset = 4;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
            btn_axe4.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe5(object sender, EventArgs e)
        {
            axeOffset = 5;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_1st_gun;
            btn_axe5.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe6(object sender, EventArgs e)
        {
            axeOffset = 6;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_2nd_gun;
            btn_axe6.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe7(object sender, EventArgs e)
        {
            axeOffset = 7;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_3rd_gun;
            btn_axe7.BackColor = SystemColors.ButtonHighlight;
        }

        /// <summary>
        /// Seleziona l'asse 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe8(object sender, EventArgs e)
        {
            axeOffset = 8;
            RestorButtonColor();
            pnl_axeImage.BackgroundImage = Properties.Resources.axe_4th_gun;
            btn_axe8.BackColor = SystemColors.ButtonHighlight;
        }

        #endregion

        /// <summary>
        /// Apre la pagina dei parametri di lavoro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToWorkParams(object sender, EventArgs e)
        {
            _navigator.Navigate("Work Params", axeOffset);
        }

        #endregion
    }
}
