using RMLib.Keyboards;
using RMLib.PLC;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RM.src.RM220930
{
    /// <summary>
    /// Pagina di configurazione del modulo us3
    /// </summary>
    public partial class UC_us3Configuration : UserControl
    {
        /// <summary>
        /// True quando tasto STANDARD viene premuto
        /// </summary>
        private bool isStandard = false;

        /// <summary>
        /// True quando tasto PULSE viene premuto
        /// </summary>
        private bool isPulse = false;

        /// <summary>
        /// True quando tasto SWIPE viene premuto
        /// </summary>
        private bool isSwipe = false;

        /// <summary>
        /// True quando tasto START viene premuto
        /// </summary>
        private bool isStarted = false;

        /// <summary>
        /// True quando tasto AUTO viene premuto
        /// </summary>
        private bool isAuto = false;

        /// <summary>
        /// True quando tasto MAN viene premuto
        /// </summary>
        private bool isManual = false;

        private static bool pulseMode_default = false, swipeMode_default = false, 
             start_default = false, auto_man_default = false;

        private static float frequency_default = 0, power_default = 0, feedbackDetect_default = 0, feedbackPower_default = 0;

        /// <summary>
        /// Costruttore vuoto per creare la pagina di configurazione del modulo us3
        /// </summary>
        public UC_us3Configuration()
        {
            InitOnDemandVariables();
            InitializeComponent();
           // InitDefaultValues();
            InitComponents();

           
            // Collegamento evento ValueChanged del dizionario al metodo HandleDictionaryChange
            PLCConfig.appVariables.ValueChanged += RefreshVariables;
            /*
            if (Global.fieraScreenSaver)
            {
                foreach (Control control in Controls)
                {
                    control.MouseLeave += ScreenSaverManager.MouseMoveEvent_resetTimerScreenSaver;
                    //TODO: bisogna scorrere i controlli anche dentro i panel
                }
            }*/

            // Avvia il caricamento asincrono dei parametri
           // Task.Run(() => InitParameters());

        }

        #region Metodi di UC_us3Configuration

        private void InitOnDemandVariables()
        {/*
            RefresherTask.AddOnDemand("PLC1_" + "CMD_US_DX_Pulse_Mode",
            "{\"register_type\":\"Holding Register\", \"address\":1, \"bit_position\":14}",
           AppVariableTypeEnum.BOOL,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "CMD_US_DX_Swipe_Mode",
            "{\"register_type\":\"Holding Register\", \"address\":1, \"bit_position\":15}",
           AppVariableTypeEnum.BOOL,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "US_Dx_Freq_Write",
            "{\"register_type\":\"Holding Register\", \"address\":31}",
           AppVariableTypeEnum.INT16,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "US_Dx_Volt_Write",
            "{\"register_type\":\"Holding Register\", \"address\":32}",
           AppVariableTypeEnum.INT16,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "US_Dx_ACT frequency",
            "{\"register_type\":\"Holding Register\", \"address\":40}",
           AppVariableTypeEnum.INT16,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "US_Dx_ACT Voltage",
            "{\"register_type\":\"Holding Register\", \"address\":41}",
           AppVariableTypeEnum.INT16,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "CMD Start US_Dx",
            "{\"register_type\":\"Holding Register\", \"address\":1, \"bit_position\":3}",
           AppVariableTypeEnum.BOOL,
           0);

            RefresherTask.AddOnDemand("PLC1_" + "Man-Auto US",
            "{\"register_type\":\"Holding Register\", \"address\":1, \"bit_position\":5}",
           AppVariableTypeEnum.BOOL,
           0);
           */

        }

        private void InitDefaultValues()
        {
            // Inizializzo i button 'STANDARD', 'PULSE' e 'SWIPE'
            if (pulseMode_default.ToString() == "False" && swipeMode_default.ToString() == "False")
            {
                btn_standard.BackColor = Color.FromArgb(40, 175, 75);
                btn_pulse.BackColor = SystemColors.ActiveBorder;
                btn_swipe.BackColor = SystemColors.ActiveBorder;
                isStandard = true;
                isPulse = false;
                isSwipe = false;
            }
            else
            {
                btn_standard.BackColor = SystemColors.ActiveBorder;
                isStandard = false;
            }

            if (pulseMode_default.ToString() == "True")
            {
                btn_pulse.BackColor = Color.FromArgb(40, 175, 75);
                btn_standard.BackColor = SystemColors.ActiveBorder;
                btn_swipe.BackColor = SystemColors.ActiveBorder;
                isPulse = true;
                isStandard = false;
                isSwipe = false;
            }
            else
            {
                btn_pulse.BackColor = SystemColors.ActiveBorder;
                isPulse = false;
            }

            if (swipeMode_default.ToString() == "True")
            {
                btn_swipe.BackColor = Color.FromArgb(40, 175, 75);
                btn_pulse.BackColor = SystemColors.ActiveBorder;
                btn_standard.BackColor = SystemColors.ActiveBorder; // Corretto da btn_swipe a btn_standard
                isSwipe = true;
                isStandard = false;
                isPulse = false;
            }
            else
            {
                btn_swipe.BackColor = SystemColors.ActiveBorder;
                isSwipe = false;
            }

            // Inizializzo la label 'Frequenza'
            lbl_frequency.Text = frequency_default.ToString();

            // Inizializzo la label 'Potenza'
            lbl_power.Text = power_default.ToString();

            // Inizializzo la label 'feedback detect'
            lbl_feedBackDetect.Text = feedbackDetect_default.ToString();

            // Inizializzo la label 'feedback power'
            lbl_feedBackPower.Text = feedbackPower_default.ToString();

            // Inizializzo il button 'START'
            if (start_default.ToString() == "True")
            {
                btn_start.BackColor = Color.FromArgb(40, 175, 75);
                isStarted = true;
            }
            else
            {
                btn_start.BackColor = SystemColors.ActiveBorder;
                isStarted = false;
            }

            // Inizializzo i button AUTO e MAN
            if (auto_man_default.ToString() == "True")
            {
                btn_auto.BackColor = Color.FromArgb(40, 175, 75);
                btn_man.BackColor = SystemColors.ActiveBorder;
                isAuto = true;
                isManual = false;
            }
            else
            {
                btn_auto.BackColor = SystemColors.ActiveBorder;
                btn_man.BackColor = Color.FromArgb(40, 175, 75);
                isAuto = false;
                isManual = true;
            }
        }

        /// <summary>
        /// Inizializza i valori dei controlli presenti nell'interfaccia
        /// </summary>
        private async Task InitParameters()
        {
          //  await Task.Delay(2000); // Simula un piccolo ritardo per evitare blocchi della UI

            object pulseMode, swipeMode, frequency, power, feedbackDetect, feedbackPower, start, auto_man;

            lock (PLCConfig.appVariables)
            {
                pulseMode = PLCConfig.appVariables.getValue("PLC1_" + "CMD_US_DX_Pulse_Mode");
                swipeMode = PLCConfig.appVariables.getValue("PLC1_" + "CMD_US_DX_Swipe_Mode");
                frequency = PLCConfig.appVariables.getValue("PLC1_" + "US_Dx_Freq_Write");
                power = PLCConfig.appVariables.getValue("PLC1_" + "US_Dx_Volt_Write");
                feedbackDetect = PLCConfig.appVariables.getValue("PLC1_" + "US_Dx_ACT frequency");
                feedbackPower = PLCConfig.appVariables.getValue("PLC1_" + "US_Dx_ACT Voltage");
                start = PLCConfig.appVariables.getValue("PLC1_" + "CMD Start US_Dx");
                auto_man = PLCConfig.appVariables.getValue("PLC1_" + "Man-Auto US");
            }

            // Aggiorna la UI nel thread della UI
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateUI(pulseMode, swipeMode, frequency, power, feedbackDetect, feedbackPower, start, auto_man);
                }));
            }
            else
            {
                UpdateUI(pulseMode, swipeMode, frequency, power, feedbackDetect, feedbackPower, start, auto_man);
            }
        }

        private void UpdateUI(object pulseMode, object swipeMode, object frequency, object power, object feedbackDetect, object feedbackPower, object start, object auto_man)
        {
            // Inizializzo i button 'STANDARD', 'PULSE' e 'SWIPE'
            if (pulseMode != null && swipeMode != null && pulseMode.ToString() == "False" && swipeMode.ToString() == "False")
            {
                btn_standard.BackColor = Color.FromArgb(40, 175, 75);
                btn_pulse.BackColor = SystemColors.ActiveBorder;
                btn_swipe.BackColor = SystemColors.ActiveBorder;
                isStandard = true;
                isPulse = false;
                isSwipe = false;
            }
            else
            {
                btn_standard.BackColor = SystemColors.ActiveBorder;
                isStandard = false;
            }

            if (pulseMode != null && pulseMode.ToString() == "True")
            {
                btn_pulse.BackColor = Color.FromArgb(40, 175, 75);
                btn_standard.BackColor = SystemColors.ActiveBorder;
                btn_swipe.BackColor = SystemColors.ActiveBorder;
                isPulse = true;
                isStandard = false;
                isSwipe = false;
            }
            else
            {
                btn_pulse.BackColor = SystemColors.ActiveBorder;
                isPulse = false;
            }

            if (swipeMode != null && swipeMode.ToString() == "True")
            {
                btn_swipe.BackColor = Color.FromArgb(40, 175, 75);
                btn_pulse.BackColor = SystemColors.ActiveBorder;
                btn_standard.BackColor = SystemColors.ActiveBorder; // Corretto da btn_swipe a btn_standard
                isSwipe = true;
                isStandard = false;
                isPulse = false;
            }
            else
            {
                btn_swipe.BackColor = SystemColors.ActiveBorder;
                isSwipe = false;
            }

            // Inizializzo la label 'Frequenza'
            if(frequency != null) lbl_frequency.Text = frequency.ToString();

            // Inizializzo la label 'Potenza'
            if (power != null) lbl_power.Text = power.ToString();

            // Inizializzo la label 'feedback detect'
            if (feedbackDetect != null) lbl_feedBackDetect.Text = feedbackDetect.ToString();

            // Inizializzo la label 'feedback power'
            if (feedbackPower != null) lbl_feedBackPower.Text = feedbackPower.ToString();

            // Inizializzo il button 'START'
            if (start != null && start.ToString() == "True")
            {
                btn_start.BackColor = Color.FromArgb(40, 175, 75);
                isStarted = true;
            }
            else
            {
                btn_start.BackColor = SystemColors.ActiveBorder;
                isStarted = false;
            }

            // Inizializzo i button AUTO e MAN
            if (auto_man != null && auto_man.ToString() == "True")
            {
                btn_auto.BackColor = Color.FromArgb(40, 175, 75);
                btn_man.BackColor = SystemColors.ActiveBorder;
                isAuto = true;
                isManual = false;
            }
            else
            {
                btn_auto.BackColor = SystemColors.ActiveBorder;
                btn_man.BackColor = Color.FromArgb(40, 175, 75);
                isAuto = false;
                isManual = true;
            }

            if (pulseMode != null) pulseMode_default = pulseMode.ToString() == "True";
            if (swipeMode != null) swipeMode_default = swipeMode.ToString() == "True";
            if (frequency != null) frequency_default = float.Parse(frequency.ToString());
            if (power != null) power_default = float.Parse(power.ToString());
            if (feedbackDetect != null) feedbackDetect_default = float.Parse(feedbackDetect.ToString());
            if (feedbackPower != null) feedbackPower_default = float.Parse(feedbackPower.ToString());
            if (start != null) start_default = start.ToString() == "True";
            if (auto_man != null) auto_man_default = auto_man.ToString() == "True";
        }

        /// <summary>
        /// Inizializza i controlli di UC_us3Configuration
        /// </summary>
        private void InitComponents()
        {
            FormHomePage.Instance.LabelHeader = "CONFIGURAZIONE US";
            FormHomePage.Instance.ChangeBackColor = BackColor;
            //TODO: lettura dei valori da assegnare ai pulsanti e label in base ai valori del plc nel dizionario
            //TODO: fare un timer che ogni tot tempo refresha i valori rileggendoli dal dizionario
        }

        /// <summary>
        /// Metodo richiamato dall'evento ValueChanged del dizionario delle variabili PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshVariables(object sender, DictionaryChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, DictionaryChangedEventArgs>(RefreshVariables), sender, e);
                return;
            }

            switch (e.Key)
            {
                case "PLC1_" + "US_Dx_ACT frequency":
                    lbl_feedBackDetect.Text = e.NewValue.ToString();
                    break;

                case "PLC1_" + "US_Dx_ACT Voltage":
                    lbl_feedBackPower.Text = e.NewValue.ToString();
                    break;

                case "PLC1_" + "US_Dx_Freq_Write":
                    lbl_frequency.Text = e.NewValue.ToString();
                    break;

                case "PLC1_" + "US_Dx_Volt_Write":
                    lbl_power.Text = e.NewValue.ToString();
                    break;

                case "PLC1_" + "CMD_US_DX_Pulse_Mode":
                    if (e.NewValue.ToString() == "True")
                    {
                        btn_pulse.BackColor = Color.FromArgb(40, 175, 75);
                        btn_standard.BackColor = SystemColors.ActiveBorder;
                        isPulse = true;
                    }
                    else
                    {
                        btn_pulse.BackColor = SystemColors.ActiveBorder;
                        isPulse = false;
                        if (!isPulse && !isSwipe)
                        {
                            btn_standard.BackColor = Color.FromArgb(40, 175, 75);
                        }
                    }
                    break;

                case "PLC1_" + "CMD_US_DX_Swipe_Mode":
                    if (e.NewValue.ToString() == "True")
                    {
                        btn_swipe.BackColor = Color.FromArgb(40, 175, 75);
                        btn_standard.BackColor = SystemColors.ActiveBorder;
                        isSwipe = true;
                    }
                    else
                    {
                        btn_swipe.BackColor = SystemColors.ActiveBorder;
                        isSwipe = false;
                        if (!isSwipe && !isPulse)
                        {
                            btn_standard.BackColor = Color.FromArgb(40, 175, 75);
                        }
                    }
                    break;

                case "PLC1_" + "CMD Start US_Dx":
                    if (e.NewValue.ToString() == "True")
                    {
                        btn_start.BackColor = Color.FromArgb(40, 175, 75);
                        isStarted = true;
                    }
                    else
                    {
                        btn_start.BackColor = SystemColors.ActiveBorder;
                        isStarted = false;
                    }
                    break;

                case "PLC1_" + "Man-Auto US":
                    if (e.NewValue.ToString() == "True")
                    {
                        btn_auto.BackColor = Color.FromArgb(40, 175, 75);
                        btn_man.BackColor = SystemColors.ActiveBorder;
                        isAuto = true;
                        isManual = false;
                    }
                    else
                    {
                        btn_man.BackColor = Color.FromArgb(40, 175, 75);
                        btn_auto.BackColor = SystemColors.ActiveBorder;
                        isAuto = false;
                        isManual = true;
                    }
                    break;
            }
        }

        #endregion

        #region Eventi di UC_us3Configuration
        /// <summary>
        /// Ritorno a pagina di home
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            FormHomePage.Instance.LabelHeader = "HOME PAGE";
            FormHomePage.Instance.ChangeBackColor = SystemColors.Control;
            FormHomePage.Instance.PnlContainer.Controls["UC_HomePage"].BringToFront();
            FormHomePage.Instance.PnlContainer.Controls.Remove(Controls["UC_permissions"]);

            List<string> keys = new List<string>();
            keys.Add("PLC1_" + "CMD_US_DX_Pulse_Mode");
            keys.Add("PLC1_" + "CMD_US_DX_Swipe_Mode");
            keys.Add("PLC1_" + "US_Dx_Freq_Write");
            keys.Add("PLC1_" + "US_Dx_Freq_Write");
            keys.Add("PLC1_" + "US_Dx_ACT frequency");
            keys.Add("PLC1_" + "US_Dx_ACT Voltage");
            keys.Add("PLC1_" + "CMD Start US_Dx");
            keys.Add("PLC1_" + "Man-Auto US");
            RefresherTask.RemoveOnDemand(keys);


            Dispose();
        }

        /// <summary>
        /// Inserimento frequenza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblFrequency_Click(object sender, EventArgs e)
        {
            string newFrequency = VK_Manager.OpenFloatVK("0");
            if (newFrequency.Equals(VK_Manager.CANCEL_STRING)) return;
            lbl_frequency.Text = newFrequency;
            RefresherTask.AddUpdate("PLC1_" + "US_Dx_Freq_Write", Convert.ToInt16(newFrequency), "INT16");

            frequency_default = Convert.ToInt16(newFrequency);
        }

        /// <summary>
        /// Inserimento potenza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblPower_Click(object sender, EventArgs e)
        {
            string newPower = VK_Manager.OpenFloatVK("0");
            if (newPower.Equals(VK_Manager.CANCEL_STRING)) return;
            lbl_power.Text = newPower;
            RefresherTask.AddUpdate("PLC1_" + "US_Dx_Volt_Write", Convert.ToInt16(newPower), "INT16");

            power_default = Convert.ToInt16(newPower);
        }

        #endregion

        /// <summary>
        /// Gestisce pressione tasto STANDARD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStandard_Click(object sender, EventArgs e)
        {
            if (isStandard)
            {
                btn_standard.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                btn_standard.BackColor = Color.FromArgb(40, 175, 75);

                btn_pulse.BackColor = SystemColors.ActiveBorder;
                RefresherTask.AddUpdate("PLC1_" + "CMD_US_DX_Pulse_Mode", false, "BOOL");

                btn_swipe.BackColor = SystemColors.ActiveBorder;
                RefresherTask.AddUpdate("PLC1_" + "CMD_US_DX_Swipe_Mode", false, "BOOL");
            }

            isStandard = !isStandard;
            isPulse = false;
            isSwipe = false;

            start_default = isStandard;
        }


        /// <summary>
        /// Gestisce pressione tasto PULSE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPulse_Click(object sender, EventArgs e)
        {
            if (isPulse)
            {
                btn_pulse.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                btn_pulse.BackColor = Color.FromArgb(40, 175, 75);
                RefresherTask.AddUpdate("PLC1_" + "CMD_US_DX_Pulse_Mode", true, "BOOL");

                btn_standard.BackColor = SystemColors.ActiveBorder;

                btn_swipe.BackColor = SystemColors.ActiveBorder;
                RefresherTask.AddUpdate("PLC1_" + "CMD_US_DX_Swipe_Mode", false, "BOOL");
            }

            isPulse = !isPulse;
            isStandard = false;
            isSwipe = false;

            pulseMode_default = isPulse;
        }

        /// <summary>
        /// Gestisce pressione tasto SWIPE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSwipe_Click(object sender, EventArgs e)
        {
            if (isSwipe)
            {
                btn_swipe.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                btn_swipe.BackColor = Color.FromArgb(40, 175, 75);
                RefresherTask.AddUpdate("PLC1_" + "CMD_US_DX_Swipe_Mode", true, "BOOL");

                btn_pulse.BackColor = SystemColors.ActiveBorder;
                RefresherTask.AddUpdate("PLC1_" + "CMD_US_DX_Pulse_Mode", false, "BOOL");

                btn_standard.BackColor = SystemColors.ActiveBorder;
            }

            isSwipe = !isSwipe;
            isPulse = false;
            isStandard = false;

            swipeMode_default = isSwipe;
        }

        /// <summary>
        /// Gestisce pressione tasto START
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                btn_start.BackColor = SystemColors.ActiveBorder;
                RefresherTask.AddUpdate("PLC1_" + "CMD Start US_Dx", false, "BOOL");
            }
            else
            {
                btn_start.BackColor = Color.FromArgb(40, 175, 75);
                RefresherTask.AddUpdate("PLC1_" + "CMD Start US_Dx", true, "BOOL");
            }

            isStarted = !isStarted;

            start_default = isStarted;
        }

        /// <summary>
        /// Gestisce pressione tasto AUTO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAuto_Click(object sender, EventArgs e)
        {
            if (isAuto)
            {
                btn_auto.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                btn_auto.BackColor = Color.FromArgb(40, 175, 75);
                RefresherTask.AddUpdate("PLC1_" + "Man-Auto US", true, "BOOL");

                btn_man.BackColor = SystemColors.ActiveBorder;
            }

            isAuto = !isAuto;
            isManual = false;

            auto_man_default = isAuto;
        }

        /// <summary>
        /// Gestisce pressione tasto MAN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMan_Click(object sender, EventArgs e)
        {
            if (isManual)
            {
                btn_man.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                btn_man.BackColor = Color.FromArgb(40, 175, 75);
                RefresherTask.AddUpdate("PLC1_" + "Man-Auto US", false, "BOOL");

                btn_auto.BackColor = SystemColors.ActiveBorder;
            }

            isManual = !isAuto;
            isAuto = false;

            auto_man_default = !isAuto;
        }

        private void btn_standard_MouseDown(object sender, MouseEventArgs e)
        {
            if (isStandard)
            {
                btn_standard.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                btn_standard.BackColor = Color.FromArgb(40, 175, 75);

                btn_pulse.BackColor = SystemColors.ActiveBorder;
                PLCConfig.appVariables.updateValue("PLC1_" + "CMD_US_DX_Pulse_Mode", false, "BOOL");

                btn_swipe.BackColor = SystemColors.ActiveBorder;
                PLCConfig.appVariables.updateValue("PLC1_" + "CMD_US_DX_Swipe_Mode", false, "BOOL");
            }

        }

        private void btn_standard_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
