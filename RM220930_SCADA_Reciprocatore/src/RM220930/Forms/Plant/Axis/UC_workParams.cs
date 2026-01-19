using RM.src.RM220930.Classes.Navigator;
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
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int offset)
            {
               lbl_num.Text = offset.ToString();
            }
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
    }
}
