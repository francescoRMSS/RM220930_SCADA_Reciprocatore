using CookComputing.XmlRpc;
using RM.Properties;
using RM.src.RM220930.Classes.Navigator;
using RM.src.RM220930.Classes.PLC;
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
    public partial class UC_testUDT : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        #region Proprietà di UC_axis

        /// <summary>
        /// Gestisce switch tra le varie userControl degli assi
        /// </summary>
        private Navigator _navigator;

        /// <summary>
        /// Asse selezionato
        /// </summary>
        public static int axeOffset = 1;

        #endregion
        bool Cmd_On_Axe = false;

        string varToUpdate = string.Empty;

        PLCTagName plcTagName = new PLCTagName();

        public UC_testUDT()
        {
            InitializeComponent();
            // Collegamento evento ValueChanged del dizionario al metodo HandleDictionaryChange
            PLCConfig.appVariables.ValueChanged += RefreshVariables;
        }

        /// <summary>
        /// Metodo richiamato dall'evento ValueChanged del dizionario delle variabili PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshVariables(object sender, DictionaryChangedEventArgs e)
        {
            if (varToUpdate != string.Empty)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object, DictionaryChangedEventArgs>(RefreshVariables), sender, e);
                    return;
                }

                var field = typeof(PLCTagName).GetField(varToUpdate);

                string valore = field.GetValue(plcTagName)?.ToString();

                string expectedKey = $"PLC1_z{UC_axis.axeOffset}_{valore}";

                string key = e.Key;

                if (key == expectedKey)
                {
                    switch (e.NewValue)
                    {
                        case bool b:
                            btn_boolValue.BackColor = b ? Color.Green : Color.Red;
                            btn_boolValue.Text = b ? "TRUE" : "FALSE";
                            Cmd_On_Axe = b;
                            break;

                        case short i16:
                            tb_intValue.Text = i16.ToString();
                            break;

                        case float f:
                            tb_floatValue.Text = f.ToString();
                            break;
                    }

                }
            }


        }

        /// <summary>
        /// Gestisce l'utilizzo del parametro passato durante la navigazione
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {
            // Se il parametro è il numero di asse
            if (parameter is int offset)
            {
                axeOffset = offset;
            }
            else // se non c'è imposto 1 di default
            {
                axeOffset = 1;
            }

        }

      
    

        private void ClickEvent_modifyCmd_On_axe(object sender, EventArgs e)
        {
            var field = typeof(PLCTagName).GetField(varToUpdate);

            string valore = field.GetValue(plcTagName)?.ToString();

            bool boolValue = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_z{UC_axis.axeOffset}_{valore}"));

            if (boolValue)
                RefresherTask.AddUpdate($"PLC1_z{UC_axis.axeOffset}_{valore}", false, "BOOL");
            else
                RefresherTask.AddUpdate($"PLC1_z{UC_axis.axeOffset}_{valore}", true, "BOOL");

        }

        private void btn_Cmd_En_Axe_Click(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate("PLC1_" + "axe" + UC_axis.axeOffset.ToString() + "_" + "Cmd_En_Axe", true, "BOOL");
        }

        private void btn_Cmd_On_Axe8_Click(object sender, EventArgs e)
        {

        }

        private void cb_boolList_SelectedIndexChanged(object sender, EventArgs e)
        {
            varToUpdate = cb_boolList.SelectedItem.ToString();
            lbl_selectedBool.Text = cb_boolList.SelectedItem.ToString();

            var field = typeof(PLCTagName).GetField(varToUpdate);

            string valore = field.GetValue(plcTagName)?.ToString();

            bool boolValue = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_z{UC_axis.axeOffset}_{valore}"));

            if (boolValue)
            {
                btn_boolValue.BackColor = Color.Green;
                btn_boolValue.Text = "TRUE";
                Cmd_On_Axe = true;
            }
            else
            {
                btn_boolValue.BackColor = Color.Red;
                btn_boolValue.Text = "FALSE";
                Cmd_On_Axe = false;
            }           
        }

        private void cb_axis_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_selectedAxe.Text = cb_axis.SelectedItem.ToString();
            UC_axis.axeOffset = Convert.ToInt16(cb_axis.SelectedItem.ToString());
        }

        private void cb_intList_SelectedIndexChanged(object sender, EventArgs e)
        {
            varToUpdate = cb_intList.SelectedItem.ToString();
            lbl_selectedInt.Text = cb_intList.SelectedItem.ToString();

            var field = typeof(PLCTagName).GetField(varToUpdate);

            string valore = field.GetValue(plcTagName)?.ToString();

            int intValue = Convert.ToInt16(PLCConfig.appVariables.getValue($"PLC1_z{UC_axis.axeOffset}_{valore}"));

            tb_intValue.Text = intValue.ToString();

        }

        private void btn_sendIntValue_Click(object sender, EventArgs e)
        {
            var field = typeof(PLCTagName).GetField(varToUpdate);

            string valore = field.GetValue(plcTagName)?.ToString();

            int valToSend = Convert.ToInt16(tb_intValue.Text);

            RefresherTask.AddUpdate($"PLC1_z{UC_axis.axeOffset}_{valore}", valToSend, "INT16");
        }

        private void cb_floatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            varToUpdate = cb_floatList.SelectedItem.ToString();
            lbl_selectedFloat.Text = cb_floatList.SelectedItem.ToString();

            var field = typeof(PLCTagName).GetField(varToUpdate);

            string valore = field.GetValue(plcTagName)?.ToString();

            float floatValue = Convert.ToInt16(PLCConfig.appVariables.getValue($"PLC1_z{UC_axis.axeOffset}_{valore}"));

            tb_floatValue.Text = floatValue.ToString();
        }

        private void btn_sendFloatValue_Click(object sender, EventArgs e)
        {
            var field = typeof(PLCTagName).GetField(varToUpdate);

            string valore = field.GetValue(plcTagName)?.ToString();

            float valToSend = float.Parse(tb_floatValue.Text);

            RefresherTask.AddUpdate($"PLC1_z{UC_axis.axeOffset}_{valore}", valToSend, "FLOAT");
        }
    }
}
