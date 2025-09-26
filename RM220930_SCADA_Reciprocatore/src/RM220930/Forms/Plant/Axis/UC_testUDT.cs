using RM.Properties;
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
        bool Cmd_On_Axe = false;

        public UC_testUDT()
        {
            InitializeComponent();
            InitVar();
            // Collegamento evento ValueChanged del dizionario al metodo HandleDictionaryChange
            PLCConfig.appVariables.ValueChanged += RefreshVariables;
        }

        private void InitVar()
        {
            Cmd_On_Axe = Convert.ToBoolean(PLCConfig.appVariables.getValue
                (("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_On_Axe")));

            if (Cmd_On_Axe)
            {
                btn_Cmd_On_Axe1.BackColor = Color.Green;
                btn_Cmd_On_Axe1.Text = "TRUE";
            }
            else
            {
                btn_Cmd_On_Axe1.BackColor = Color.Red;
                btn_Cmd_On_Axe1.Text = "FALSE";
            }
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

            string key = e.Key;
            string expectedKey = $"PLC1_axe{UC_axis.axeOffset}_Cmd_On_Axe";

            if (key == expectedKey)
            {
                if (e.NewValue.ToString() == "True")
                {
                    btn_Cmd_On_Axe1.BackColor = Color.Green;
                    btn_Cmd_On_Axe1.Text = "TRUE";
                    Cmd_On_Axe = true;
                }
                else
                {
                    btn_Cmd_On_Axe1.BackColor = Color.Red;
                    btn_Cmd_On_Axe1.Text = "FALSE";
                    Cmd_On_Axe = false;
                }
            }



        }
    

        private void ClickEvent_modifyCmd_On_axe(object sender, EventArgs e)
        {
            if (!Cmd_On_Axe)
                RefresherTask.AddUpdate("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_On_Axe", true, "BOOL");
            else
                RefresherTask.AddUpdate("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_On_Axe", false, "BOOL"); 
        }

        private void btn_Cmd_En_Axe_Click(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_En_Axe", true, "BOOL");
        }
    }
}
