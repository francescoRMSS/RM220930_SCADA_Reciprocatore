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
        public UC_workParams()
        {
            InitializeComponent();

            SCADAManager.Z_ONOFF_workParams = new BiStateButton(btn_z_onoff, Color.ForestGreen, "ON", Color.Firebrick, "OFF");
            SCADAManager.Z_Home_workParams = new BiStateButton(btn_home, Color.ForestGreen, Color.Firebrick);
            SCADAManager.Z_Auto_workParams = new BiStateButton(btn_autoONOFF, Color.ForestGreen, Color.Firebrick);
            SCADAManager.numAxe_workParams = new UiLabel(lbl_numAxe);
            SCADAManager.speed_workParams = new UiLabel(lbl_speed);
            SCADAManager.speed_workParams = new UiLabel(lbl_speed);
            SCADAManager.posRange_workParams = new UiLabel(lbl_posRange);
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

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

        private void label9_Click(object sender, EventArgs e)
        {
            // Cmd_Min_Pos
        }

        private void label11_Click(object sender, EventArgs e)
        {
            // Cmd_Max_Pos
        }

        private void label5_Click(object sender, EventArgs e)
        {
           // Cmd_Offset_From_Piece
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Cmd_Pos_Range
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

        private void MouseDownEvent_SpeedUp(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            int index = Convert.ToInt32(btn.Tag);

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Jog_Pos}", true, "BOOL");
        }

        private void btn_home_MouseUp(object sender, MouseEventArgs e)
        {
            int index = SCADAManager.axeOffset;

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Go_home}", false, "BOOL");
        }
    }
}
