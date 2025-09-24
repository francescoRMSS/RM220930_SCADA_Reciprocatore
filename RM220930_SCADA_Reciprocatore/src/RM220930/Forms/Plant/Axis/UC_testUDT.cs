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
    public partial class UC_testUDT : UserControl
    {
        public UC_testUDT()
        {
            InitializeComponent();
        }

        private void btn_Cmd_On_Axe_Click(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_On_Axe", true, "BOOL");
        }

        private void btn_Cmd_En_Axe_Click(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_En_Axe", true, "BOOL");
        }
    }
}
