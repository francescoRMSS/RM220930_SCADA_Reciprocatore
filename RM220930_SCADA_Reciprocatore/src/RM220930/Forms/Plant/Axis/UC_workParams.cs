using RM.src.RM220930.Classes;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.UiBinder;
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
    public partial class UC_workParams : UserControl, INavigable, INavigationRequester
    {
        public UC_workParams()
        {
            InitializeComponent();

            SCADAManager.Z_ONOFF_workParams = new BiStateButton(btn_z_onoff, Color.ForestGreen, "ON", Color.Firebrick, "OFF");
            SCADAManager.Z_Home = new BiStateButton(btn_home, Color.ForestGreen, Color.Firebrick);
            SCADAManager.Z_Auto = new BiStateButton(btn_autoONOFF, Color.ForestGreen, Color.Firebrick);
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int offset)
            {
               lbl_num.Text = offset.ToString();

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

            int index = UC_axis.axeOffset;

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

        private void label1_Click(object sender, EventArgs e)
        {
            // Cmd_Speed_Pos
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
        }

        private void btn_home_MouseDown(object sender, MouseEventArgs e)
        {
            int index = UC_axis.axeOffset;

            RefresherTask.AddUpdate($"PLC1_z{index}_{PLCTagName.Cmd_Go_home}", true, "BOOL");
        }
    }
}
