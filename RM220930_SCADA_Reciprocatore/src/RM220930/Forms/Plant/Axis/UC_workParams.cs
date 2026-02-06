using RM.src.RM220930.Classes;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.UiBinder;
using RMLib.Keyboards;
using RMLib.MessageBox;
using RMLib.PLC;
using RMLib.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant.Axis
{
    public partial class UC_workParams : UserControl, INavigable, INavigationRequester
    {
        private bool incremento;  // true = +, false = -
        private Timer repeatTimer;
        private Label labelAttiva;
        private int valoreAttivo;


        public UC_workParams()
        {
            InitializeComponent();
            InitControls();
            InitRepeatTimer();
        }

        /// <summary>
        /// Creazione degli oggetti in SCADAManager
        /// </summary>
        private void InitControls()
        {
            SCADAManager.Z_ONOFF_workParams = new BiStateButton(btn_z_onoff, Color.ForestGreen, "ON", Color.Firebrick, "OFF");
            SCADAManager.Z_Home_workParams = new BiStateButton(btn_home, Color.ForestGreen, Color.Firebrick);
            SCADAManager.Z_Auto_workParams = new BiStateButton(btn_autoONOFF, Color.ForestGreen, Color.Firebrick);
            SCADAManager.numAxe_workParams = new UiLabel(lbl_numAxe);
            SCADAManager.speed_workParams = new UiLabel(lbl_speed);
            SCADAManager.speed_workParams = new UiLabel(lbl_speed);
            SCADAManager.posRange_workParams = new UiLabel(lbl_posRange);
            SCADAManager.offsetFromPiece_workParams = new UiLabel(lbl_offsetFromPiece);
            SCADAManager.posAlta_workParams = new UiLabel(lbl_posAlta);
            SCADAManager.posBassa_workParams = new UiLabel(lbl_posBassa);
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

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            CambiaValore(labelAttiva, ref valoreAttivo, incremento);
        }


        private void CambiaValore(Label lbl, ref int valore, bool incremento)
        {
            valore += incremento ? 1 : -1;
            lbl.Text = valore.ToString();
        }



        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int offset)
            {
               //lbl_numAxe.Text = offset.ToString();

            }
        }


        /// <summary>
        /// Gestisce abilitazione/disabilitazione Z assi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_EnableDisableZ(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;
            //if (!(btn.Tag is int index)) return;

            int index = SCADAManager.axeOffset;

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


        private void customButton1_Click(object sender, EventArgs e)
        {
            
            // Cmd_On_Axe
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            // Cmd_Go_home
            // feedback Read_Home_Ok
        }

        
       

        

        

        

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void customButton14_Click(object sender, EventArgs e)
        {
            // Cmd_AutoFrom_Pc
            int index = SCADAManager.axeOffset;

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_AutoFrom_Pc}", true, "BOOL");
        }

        /// <summary>
        /// Gestisce abilitazione/disabilitazione Z assi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_EnableDisableAUTO(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;
            int index = SCADAManager.axeOffset;

            // Stato attuale letto dal PLC
            bool currentAutoOn = SCADAManager._zState[index].CmdAutoFromPc;

            // Toggle logico
            bool newAutoOn = !currentAutoOn;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{index}_{PLCTagName.Cmd_AutoFrom_Pc}",
                newAutoOn,
                "BOOL"
            );
        }


        private void btn_home_MouseDown(object sender, MouseEventArgs e)
        {
            int index = SCADAManager.axeOffset;

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Go_home}", true, "BOOL");
        }

        private void btn_numAxeUp_Click(object sender, EventArgs e)
        {
            int numAxe = Convert.ToInt16(lbl_numAxe.Text);
            if (numAxe >= SCADAManager.numZ - 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Indice non consentito");
                return;
            }
            numAxe++;
            SCADAManager.axeOffset = numAxe;
            // lbl_numAxe.Text = numAxe.ToString();
        }

        private void btn_numAxeDown_Click(object sender, EventArgs e)
        {
            int numAxe = Convert.ToInt16(lbl_numAxe.Text);
            if (numAxe < 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Indice non consentito");
                return;
            }
            numAxe--;
            SCADAManager.axeOffset = numAxe;
            // lbl_numAxe.Text = numAxe.ToString();
        }

        

        private void btn_home_MouseUp(object sender, MouseEventArgs e)
        {
            int index = SCADAManager.axeOffset;

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Go_home}", false, "BOOL");
        }

        #region Speed

        /// <summary>
        /// Update speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_updateSpeed(object sender, EventArgs e)
        {
            //if (!SecurityManager.ActionRequestCheck("modifyRobotSpeed")) return;

            string newVelocity = VK_Manager.OpenIntVK("0");

            if (newVelocity.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Speed_Pos}",
                newVelocity,
                "FLOAT"
            );

        }

        /// <summary>
        /// Aumento della velocità
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_speedUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_speed;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop aumento della velocità
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_speedUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_speed;
            repeatTimer.Stop();

            float newVelocity = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Speed_Pos}",
                newVelocity,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        /// <summary>
        /// Decremento della velocità
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_speedDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_speed;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi

        }

        /// <summary>
        /// Stop decremento della velocità
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_speedDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newVelocity = Convert.ToSingle(lbl_speed.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Speed_Pos}",
                newVelocity,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region PosRange

        /// <summary>
        /// Modifica posRange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_updatePosRange(object sender, EventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Pos_Range}",
                newValue,
                "FLOAT"
            );

        }

        /// <summary>
        /// Aumento pos range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posRangeUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_posRange;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop aumento pos range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posRangeUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_posRange;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Pos_Range}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        /// <summary>
        /// Decremento pos range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posRangeDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_posRange;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop decremento pos range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posRangeDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_posRange;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Pos_Range}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Offset from piece

        /// <summary>
        /// Update offset from piece
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_updateOffsetFromPiece(object sender, EventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Offset_From_Piece}",
                newValue,
                "FLOAT"
            );

        }

        /// <summary>
        /// Aumento offset dal pezzo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_offsetFromPieceUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_offsetFromPiece;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop aumento offset dal pezzo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_offsetFromPieceUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_offsetFromPiece;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Offset_From_Piece}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        /// <summary>
        /// Decremento offset da pezzo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_offsetFromPieceDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_offsetFromPiece;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop decremento offset da pezzo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_offsetFromPieceDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset == 0)
                return;

            Label label = lbl_offsetFromPiece;
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Offset_From_Piece}",
                newValue,
                "FLOAT"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Pos alta

        /// <summary>
        /// Update posizione alta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_updatePosAlta(object sender, EventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Max_Pos}",
                newValue,
                "FLOAT"
            );
        }

        /// <summary>
        /// Aumento pos alta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posAltaUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posAlta;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop aumento pos alta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posAltaUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posAlta;
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

        /// <summary>
        /// Decremento pos alta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PosAltaDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posAlta;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop decremento pos alta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PosAltaDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posAlta;
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

        #endregion

        #region Pos bassa

        /// <summary>
        /// Update posizione bassa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_updatePosBassa(object sender, EventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Min_Pos}",
                newValue,
                "FLOAT"
            );
        }

        /// <summary>
        /// Aumento posizione bassa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posBassaUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posBassa;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop aumento posizione bassa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posBassaUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posBassa;
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

        /// <summary>
        /// Decremento posizione bassa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posBassaDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posBassa;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        /// <summary>
        /// Stop decremento posizione bassa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_posBassaDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (SCADAManager.axeOffset != 0)
                return;

            Label label = lbl_posBassa;
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

        #endregion
    }
}
