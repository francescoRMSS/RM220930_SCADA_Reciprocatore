using RM.src.RM220930.Classes;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.UiBinder;
using RMLib.Keyboards;
using RMLib.PLC;
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
    public partial class UC_axeConfiguration : UserControl, INavigable, INavigationRequester
    {
        private bool incremento;  // true = +, false = -
        private Timer repeatTimer;
        private Label labelAttiva;
        private int valoreAttivo;

        public UC_axeConfiguration()
        {
            InitializeComponent();
            InitControls();
            InitRepeatTimer();
        }

        /// <summary>
        /// Init del timer per +/- dei valori
        /// </summary>
        private void InitRepeatTimer()
        {
            repeatTimer = new Timer();
            repeatTimer.Interval = 150; // velocità di ripetizione in ms
            repeatTimer.Tick += RepeatTimer_Tick;
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            CambiaValore(labelAttiva, ref valoreAttivo, incremento);
        }


        private void CambiaValore(Label lbl, ref int valore, bool incremento)
        {
            valore += incremento ? 1 : -1;
            lbl.Text = valore.ToString();
        }

        /// <summary>
        /// Creazione degli oggetti in SCADAManager
        /// </summary>
        private void InitControls()
        {
            SCADAManager.Z_ONOFF_axeConfiguration = new BiStateButton(btn_ONOFF, Color.ForestGreen, "Abilitato", Color.Firebrick, "Disabilitato");
            SCADAManager.homeTimeout_axeConfiguration = new UiLabel(lbl_homeTimeout);
            SCADAManager.velMin_axeConfiguration = new UiLabel(lbl_velMin);
            SCADAManager.velMax_axeConfiguration = new UiLabel(lbl_velMax);
            SCADAManager.acceleration_axeConfiguration = new UiLabel(lbl_acceleration);
            SCADAManager.deceleration_axeConfiguration = new UiLabel(lbl_deceleration);
            SCADAManager.posMin_axeConfiguration = new UiLabel(lbl_posMin);
            SCADAManager.posMax_axeConfiguration = new UiLabel(lbl_posMax);
            SCADAManager.posStop_axeConfiguration = new UiLabel(lbl_posStop);
            SCADAManager.velStop_axeConfiguration = new UiLabel(lbl_velStop);
            SCADAManager.offsetBase_axeConfiguration = new UiLabel(lbl_offsetBase);
            SCADAManager.disPistole_axeConfiguration = new UiLabel(lbl_disPistole);
            SCADAManager.posLavaggio_axeConfiguration = new UiLabel(lbl_posLavaggio);
            SCADAManager.velLavaggio_axeConfiguration = new UiLabel(lbl_velLavaggio);
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int offset)
            {
                // lbl_num.Text = offset.ToString();
            }
        }

        private void ClickEvent_enableDisableAxe(object sender, EventArgs e)
        {
            int index = SCADAManager.axeOffset;

            // Stato attuale letto dal PLC
            bool currentCmdEn = SCADAManager._zState[index].CmdEnAxe;

            // Toggle logico
            bool newCmdEn = !currentCmdEn;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{index}_{PLCTagName.Cmd_En_Axe}",
                newCmdEn,
                "BOOL"
            );
        }

        

       

       

      

    
       

       

     

    

       

        private void label22_Click(object sender, EventArgs e)
        {
            // Cmd_Jog_Speed
        }

        #region Home timeout

        private void ClickEvent_updateHomeTimeout(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Time_Home}",
                newValue,
                "INT32"
            );
        }

        private void btn_homeTimeoutUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_homeTimeout;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_homeTimeoutUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_homeTimeout;
            repeatTimer.Stop();

            int newValue = Convert.ToInt32(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Time_Home}",
                newValue,
                "INT32"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_homeTimeoutDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_homeTimeout;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_homeTimeoutDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_homeTimeout.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Time_Home}",
                newValue,
                "INT32"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Vel min

        private void ClickEvent_updateVelMin(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Speed}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_velMinUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_velMin;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_velMinUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_velMin;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Speed}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_velMinDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_velMin;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_velMinDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_velMin.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Speed}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Vel max

        private void ClickEvent_updateVelMax(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Speed}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_velMaxUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_velMax;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_velMaxUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_velMax;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Speed}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_velMaxDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_velMax;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_velMaxDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_velMax.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Speed}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Accelerazione

        private void ClickEvent_updateAcceleration(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Acc}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_accUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_acceleration;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_accUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_acceleration;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Acc}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_accDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_acceleration;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_accDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_acceleration.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Acc}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Decelerazione

        private void ClickEvent_updateDeceleration(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Dec}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_decUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_deceleration;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_decUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_deceleration;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Dec}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_decDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_deceleration;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_decDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_deceleration.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Dec}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Min pos

        private void ClickEvent_udpateMinPos(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Pos}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_posMinUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posMin;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posMinUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_posMin;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_posMinDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posMin;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posMinDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_posMin.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Max pos

        private void ClickEvent_updateMaxPos(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Pos}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_posMaxUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posMax;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posMaxUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_posMax;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_posMaxDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posMax;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posMaxDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_posMax.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Pos stop

        private void ClickEvent_updateStopPos(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Stop_Pos}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_posStopUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posStop;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posStopUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_posStop;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Stop_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_posStopDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posStop;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posStopDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_posStop.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Stop_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Vel stop

        private void ClickEvent_updateJogSpeed(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Jog_Speed}",
                newValue,
                "FLOAT"
            );
            
        }

        private void btn_velStopUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_velStop;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_velStopUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_velStop;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Jog_Speed}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_velStopDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_velStop;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_velStopDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_velStop.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Jog_Speed}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Offset base

        private void ClickEvent_updateOffset(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Offset}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_offsetBaseUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_offsetBase;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_offsetBaseUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_offsetBase;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Offset}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_offsetBaseDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_offsetBase;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_offsetBaseDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_offsetBase.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Offset}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Distanza pistole

        private void ClickEvent_updateDistance(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Distance_From_Center}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_disPistoleUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_disPistole;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_disPistoleUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_disPistole;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Distance_From_Center}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_disPistoleDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_disPistole;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_disPistoleDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_disPistole.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Distance_From_Center}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Pos lavaggio

        private void ClickEvent_updatePosLavaggio(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenFloatVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Wash_Pos}",
                newValue,
                "FLOAT"
            );
        }

        private void btn_posLavaggioUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posLavaggio;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posLavaggioUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_posLavaggio;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Wash_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_posLavaggioDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_posLavaggio;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_posLavaggioDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_posLavaggio.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Wash_Pos}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion
    }

}
