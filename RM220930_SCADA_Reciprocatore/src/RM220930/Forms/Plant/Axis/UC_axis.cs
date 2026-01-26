using RM.src.RM220930.Classes;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.UiBinder;
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

        #endregion

        /// <summary>
        /// Costruttura user control axis
        /// </summary>
        public UC_axis()
        {
            InitializeComponent();

            // Istanzio il navigator assegnandogli il panel contenitore
            _navigator = new Navigator(pnl_container);

            InitSelectedAxisList();
            InitSelectAxisEvents();

            // Registro le pagine che dovrà genire il panel contenitore
            RegisterPages();
        }

        #region Metodi di UC_axis

        /// <summary>
        /// Inizializzazione della lista di button z ON-OFF
        /// </summary>
        private void InitSelectedAxisList()
        {
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_rec, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe1, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe2, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe3, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe4, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe5, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe6, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe7, Color.DimGray, SystemColors.ActiveBorder));
            SCADAManager.selectedAxe_axis.Add(new BiStateButton(btn_axe8, Color.DimGray, SystemColors.ActiveBorder));
        }

        /// <summary>
        /// Collega i button di selezione asse al relativo evento
        /// </summary>
        private void InitSelectAxisEvents()
        {
            btn_rec.Tag = 0;
            btn_axe1.Tag = 1;
            btn_axe2.Tag = 2;
            btn_axe3.Tag = 3;
            btn_axe4.Tag = 4;
            btn_axe5.Tag = 5;
            btn_axe6.Tag = 6;
            btn_axe7.Tag = 7;
            btn_axe8.Tag = 8;

            // Collega tutti allo stesso handler
            btn_rec.Click += ClickEvent_selectAxe_Generic;
            btn_axe1.Click += ClickEvent_selectAxe_Generic;
            btn_axe2.Click += ClickEvent_selectAxe_Generic;
            btn_axe3.Click += ClickEvent_selectAxe_Generic;
            btn_axe4.Click += ClickEvent_selectAxe_Generic;
            btn_axe5.Click += ClickEvent_selectAxe_Generic;
            btn_axe6.Click += ClickEvent_selectAxe_Generic;
            btn_axe7.Click += ClickEvent_selectAxe_Generic;
            btn_axe8.Click += ClickEvent_selectAxe_Generic;
        }

        /// <summary>
        /// Gestisce l'utilizzo del parametro passato durante la navigazione
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {

            if (!_navigator.HasCurrentPage)
            {
                _navigator.Navigate("Work Params", SCADAManager.axeOffset);
            }
        }

        /// <summary>
        /// Ripristina colore dei button degli assi
        /// </summary>
        public void RestoreButtonColor()
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

        /// <summary>
        /// Seleziona l'asse relativo
        /// </summary>
        /// <param name="axeIndex"></param>
        /// <param name="btn"></param>
        private void SelectAxe(int axeIndex, Button btn)
        {
            // Aggiorna l'asse selezionato
            SCADAManager.axeOffset = axeIndex;

            // Ripristina colore dei pulsanti precedenti
            RestoreButtonColor();

            Image[] axeImages = new Image[]
                 {
                    Properties.Resources.axe_1st_gun, // 0
                    Properties.Resources.axe_1st_gun, // 1
                    Properties.Resources.axe_2nd_gun, // 2
                    Properties.Resources.axe_3rd_gun, // 3
                    Properties.Resources.axe_4th_gun, // 4
                    Properties.Resources.axe_1st_gun, // 5
                    Properties.Resources.axe_2nd_gun, // 6
                    Properties.Resources.axe_3rd_gun, // 7
                    Properties.Resources.axe_4th_gun, // 8
                 };

            if (axeIndex >= 0 && axeIndex < axeImages.Length)
                pnl_axeImage.BackgroundImage = axeImages[axeIndex];


            // Colora il pulsante selezionato
            btn.BackColor = Color.DimGray;
        }

        #endregion

        #region Eventi di UC_axis

        /// <summary>
        /// Seleziona l'asse relativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectAxe_Generic(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;

            int axeIndex = (int)btn.Tag;
            SelectAxe(axeIndex, btn);
        }

        #region selezione impostazione

        /// <summary>
        /// Apre la pagina dei parametri di lavoro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToWorkParams(object sender, EventArgs e)
        {
            _navigator.Navigate("Work Params", SCADAManager.axeOffset);
        }

        /// <summary>
        /// Apre la pagina delle posizione asse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToAxePosition(object sender, EventArgs e)
        {
            _navigator.Navigate("Axe Position", SCADAManager.axeOffset);
        }

        /// <summary>
        /// Apre la pagina della configurazione asse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToAxeConfiguration(object sender, EventArgs e)
        {
            _navigator.Navigate("Axe Configuration", SCADAManager.axeOffset);
        }

        #endregion

        #endregion
    }
}
