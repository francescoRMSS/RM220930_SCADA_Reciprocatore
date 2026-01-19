using RM.src.RM220930.Classes.Navigator;
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
        public UC_axeConfiguration()
        {
            InitializeComponent();
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int offset)
            {
                // lbl_num.Text = offset.ToString();
            }
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            // Cmd_En_Axe
        }

        private void label28_Click(object sender, EventArgs e)
        {
            // Cmd_Time_Home
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Cmd_Min_Speed
        }

        private void label26_Click(object sender, EventArgs e)
        {
            // Cmd_Max_Speed
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Cmd_Acc
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Cmd_Dec
        }

        private void label16_Click(object sender, EventArgs e)
        {
            // Cmd_Min_Pos
        }

        private void label8_Click(object sender, EventArgs e)
        {
            // Cmd_Max_Pos
        }

        private void label12_Click(object sender, EventArgs e)
        {
            // Cmd_Stop_Pos
        }

        private void label14_Click(object sender, EventArgs e)
        {
            // Cmd_Jog_Speed
        }

        private void label24_Click(object sender, EventArgs e)
        {
            // Cmd_Offset
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // Cmd_Distance_From_Center
        }

        private void label20_Click(object sender, EventArgs e)
        {
            // Cmd_Wash_Pos
        }

        private void label22_Click(object sender, EventArgs e)
        {
            // Cmd_Jog_Speed
        }
    }
}
