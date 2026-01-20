using CookComputing.XmlRpc;
using RM.Properties;
using RM.src.RM220930.Classes;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.UiBinder;
using RMLib.Alarms;
using RMLib.MessageBox;
using RMLib.PLC;
using RMLib.Security;
using RMLib.VATView;
using RMLib.View;
using S7.Net.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static RMLib.PLC.AppVariable;

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
            InitZONOFFButtonList();
            InitializeZButtons();
            InitializeGoToZButtons();
        }

        #region Metodi di UC_HomePage

        /// <summary>
        /// Inizializzazione della lista di button z ON-OFF
        /// </summary>
        private void InitZONOFFButtonList()
        {
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z1ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z2ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z3ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z4ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z5ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z6ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z7ONOFF));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z8ONOFF));
        }

        /// <summary>
        /// Assegna tag ai button degli Z asse e collega l'evento click
        /// </summary>
        private void InitializeZButtons()
        {
            for (int i = 0; i < SCADAManager.Z_ONOFF.Count; i++)
            {
                SCADAManager.Z_ONOFF[i]._button.Tag = i;         // Tag = indice 0..7
                SCADAManager.Z_ONOFF[i]._button.Click += ClickEvent_EnableDisableZ;
            }
        }

        /// <summary>
        /// Assegna l'evento ad ogni tasto Z asse
        /// </summary>
        private void InitializeGoToZButtons()
        {
            Button[] goToZButtons = { btn_z1, btn_z2, btn_z3, btn_z4, btn_z5, btn_z6, btn_z7, btn_z8 };

            for (int i = 0; i < goToZButtons.Length; i++)
            {
                goToZButtons[i].Tag = i;                      // indice 0..7
                goToZButtons[i].Click += ClickEvent_goToZ;    // collega l'evento generico
            }
        }

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
        /// Apre la gestione dell'asse selezionato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;        // Sicurezza
            if (!(btn.Tag is int index)) return;       // Recupera l'indice dal Tag

            UC_axis.axeOffset = index + 1;             // Mantieni axeOffset coerente
            FormHomePage._navigator.Navigate("Axis", index + 1);
        }

        /// <summary>
        /// Gestisce abilitazione/disabilitazione Z assi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_EnableDisableZ(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;        // Sicurezza: verifica che sia un Button
            if (!(btn.Tag is int index)) return;       // Recupera l'indice dell'asse dal Tag

            UC_axis.axeOffset = index + 1;             // Se vuoi mantenere axeOffset

            if (SCADAManager.Z_State[index])
            {
                SCADAManager.Z_StateToSend[index] = false;

                RefresherTask.AddUpdate(
                    $"PLC1_z{UC_axis.axeOffset}_{PLCTagName.Cmd_On_Axe}",
                    SCADAManager.Z_StateToSend[index],
                    "BOOL");
            }
            else
            {
                SCADAManager.Z_StateToSend[index] = true;
                RefresherTask.AddUpdate(
                    $"PLC1_z{UC_axis.axeOffset}_{PLCTagName.Cmd_On_Axe}",
                    SCADAManager.Z_StateToSend[index],
                    "BOOL");
            }

        }

        #endregion

        #region TODO

        private void btn_recOFF_Click(object sender, EventArgs e)
        {
            // Cmd_On_Axe
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            // Cmd_Jog_Neg
        }

      

        private void lbl_valueHub_Click(object sender, EventArgs e)
        {
            // Read_Act_Pos
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            // Cmd_Jog_Pos
        }

        #endregion
    }
}
