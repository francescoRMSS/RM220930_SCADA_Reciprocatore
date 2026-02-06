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
    public partial class UC_axePosition : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        private bool incremento;  // true = +, false = -
        private Timer repeatTimer;
        private Label labelAttiva;
        private int valoreAttivo;

        public UC_axePosition()
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
            SCADAManager.delay_axePosition = new UiLabel(lbl_delay);
            SCADAManager.advance_axePosition = new UiLabel(lbl_advance);
            SCADAManager.distance_axePosition = new UiLabel(lbl_distance);
        }


        /// <summary>
        /// Gestisce l'utilizzo del parametro passato durante la navigazione
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {
          
        }



        #region Delay

        private void ClickEvent_updateDelay(object sender, EventArgs e)
        {
            string newValue = VK_Manager.OpenIntVK("0");

            if (newValue.Equals(VK_Manager.CANCEL_STRING)) return;

            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Delay}",
                newValue,
                "INT32"
            );
        }

        private void btn_delayUp_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_delay;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = true;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_delayUp_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = lbl_delay;
            repeatTimer.Stop();

            int newValue = Convert.ToInt32(label.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Delay}",
                newValue,
                "INT32"
            );

            SCADAManager.isUIUpdating = false;
        }

        private void btn_delayDown_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = lbl_delay;
            SCADAManager.isUIUpdating = true;
            labelAttiva = label;
            valoreAttivo = Convert.ToInt32(label.Text);
            incremento = false;
            CambiaValore(label, ref valoreAttivo, incremento);       // primo aumento immediato
            repeatTimer.Start();  // poi continua finché premi
        }

        private void btn_delayDown_MouseUp(object sender, MouseEventArgs e)
        {
            repeatTimer.Stop();

            float newValue = Convert.ToSingle(lbl_delay.Text);
            // Scrittura verso PLC (command)
            RefresherTask.AddUpdate(
                $"PLC1_z{SCADAManager.axeOffset}_{PLCTagName.Cmd_Delay}",
                newValue,
                "INT32"
            );

            SCADAManager.isUIUpdating = false;
        }

        #endregion

        #region Advance

        private void ClickEvent_updateAdvance(object sender, EventArgs e)
        {
            // Cmd_Advance
        }

        private void btn_advanceUp_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btn_advanceUp_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btn_advanceDown_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btn_advanceDown_MouseUp(object sender, MouseEventArgs e)
        {

        }

        #endregion

        #region Distance

        private void ClickEvent_updateDistance(object sender, EventArgs e)
        {
            // Cmd_Distance
        }

        private void btn_distanceUp_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btn_distanceUp_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btn_distanceDown_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btn_distanceDown_MouseUp(object sender, MouseEventArgs e)
        {

        }

        #endregion
    }
}
