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
using System.Reflection;
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
            InitZActualPoslabelList();
            InitGeneralButtons();
        }

        #region Metodi di UC_HomePage

        /// <summary>
        /// Inizializzazione della lista di button z ON-OFF
        /// </summary>
        private void InitZONOFFButtonList()
        {
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z0ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z1ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z2ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z3ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z4ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z5ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
            SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z6ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
           // SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z7ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
           // SCADAManager.Z_ONOFF.Add(new BiStateButton(btn_z8ONOFF, Color.ForestGreen, "ON", Color.Firebrick, "OFF"));
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
            Button[] goToZButtons = { btn_rec, btn_z1, btn_z2, btn_z3, btn_z4, btn_z5, btn_z6, btn_z7, btn_z8 };

            for (int i = 0; i < goToZButtons.Length; i++)
            {
                goToZButtons[i].Tag = i;                      // indice 0..7
                goToZButtons[i].Click += ClickEvent_goToZ;    // collega l'evento generico
            }
        }

        /// <summary>
        /// Inizializzazione della lista di label z pos
        /// </summary>
        private void InitZActualPoslabelList()
        {
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueRec));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ1));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ2));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ3));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ4));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ5));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ6));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ7));
            SCADAManager.z_actualPos.Add(new UiLabel(lbl_valueZ8));
        }

        /// <summary>
        /// Metodo che interecetta evento di navigazione sulla pagina
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

        /// <summary>
        /// Inizializzazione dei buttons general
        /// </summary>
        private void InitGeneralButtons()
        {
            SCADAManager.manMode = new BiStateButton(btn_manMode, Color.ForestGreen, Color.Firebrick);
            SCADAManager.autoMode = new BiStateButton(btn_autoMode, Color.ForestGreen, Color.Firebrick);
            SCADAManager.pos0Mode = new BiStateButton(btn_pos0, Color.ForestGreen, Color.Firebrick);
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

            SCADAManager.axeOffset = index;             // Mantieni axeOffset coerente
            FormHomePage._navigator.Navigate("Axis", index);
        }

        /// <summary>
        /// Gestisce abilitazione/disabilitazione Z assi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_EnableDisableZ(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;
            if (!(btn.Tag is int index)) return;

            SCADAManager.axeOffset = index;

            // Stato attuale letto dal PLC
            bool currentCmdOn = SCADAManager._zState[index].CmdOnAxe;

            // Toggle logico
            bool newCmdOn = !currentCmdOn;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{index}_{PLCTagName.Cmd_On_Axe}",
                newCmdOn,
                "BOOL"
            );
        }

        /// <summary>
        /// Gestisce evento mouse down movimento jog pos dell'asse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownEvent_JogAxePos(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            int index = Convert.ToInt32(btn.Tag);

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Jog_Pos}", true, "BOOL");
        }

        /// <summary>
        /// Gestisce evento mouse up movimento jog pos dell'asse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpEvent_JogAxePos(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            int index = Convert.ToInt32(btn.Tag);

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Jog_Pos}", false, "BOOL");
        }

        /// <summary>
        /// Applica il jog negativo all'asse relativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownEvent_JogAxeNeg(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            int index = Convert.ToInt32(btn.Tag);

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Jog_neg}", true, "BOOL");
        }

        /// <summary>
        /// Applica il jog negativo all'asse relativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpEvent_JogAxeNeg(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            int index = Convert.ToInt32(btn.Tag);

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Jog_neg}", false, "BOOL");
        }

        /// <summary>
        /// Apre pagina laser (non implementata)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openLaser(object sender, EventArgs e)
        {
            CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Funzione non implementata");
        }

        /// <summary>
        /// Apre pagina 3D Live (non implementata)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_open3DLive(object sender, EventArgs e)
        {
            CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Funzione non implementata");
        }

        /// <summary>
        /// Apre pagina luci cabina (non implementata)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_cabinLight(object sender, EventArgs e)
        {
            CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Funzione non implementata");
        }

        /// <summary>
        /// Imposta l'impianto in modalità auto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_setAutoMode(object sender, EventArgs e)
        {
            // Set automatic mode
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Select_Automatic}", true, "BOOL");
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Select_Manual}", false, "BOOL");
        }

        /// <summary>
        /// Imposta l'impianto in modalità manuale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_setManMode(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Select_Manual}", true, "BOOL");
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Select_Automatic}", false, "BOOL");
        }

        /// <summary>
        /// Reset delle modalitù auto/man
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_pos0_Click(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Select_Automatic}", false, "BOOL");
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Select_Manual}", false, "BOOL");
        }

        /// <summary>
        /// Richiesta reset allarmi generale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_resetAllarmi_MouseDown(object sender, MouseEventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Reset}", true, "BOOL");
        }

        /// <summary>
        /// Termine richiesta reset allarmi generale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_resetAllarmi_MouseUp(object sender, MouseEventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Reset}", false, "BOOL");
        }

        /// <summary>
        /// Richiesta tutti gli assi in home position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_allHome_MouseDown(object sender, MouseEventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Cmd_Go_Home_All_Axis}", true, "BOOL");
        }

        /// <summary>
        /// Termine richiesta tutti gli assi in home position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_allHome_MouseUp(object sender, MouseEventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Cmd_Go_Home_All_Axis}", false, "BOOL");
        }

        /// <summary>
        /// Richiesta tutti gli assi ON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_allAxisON_MouseDown(object sender, MouseEventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Cmd_All_Axis_In_Power}", true, "BOOL");
        }

        /// <summary>
        /// Termine richiesta tutti gli assi ON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_allAxisON_MouseUp(object sender, MouseEventArgs e)
        {
            RefresherTask.AddUpdate($"PLC1_{PLCTagName.Hmi_Cmd_All_Axis_In_Power}", false, "BOOL");
        }

        #endregion
    }
}
