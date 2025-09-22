using RM.src.RM220930.Classes.Navigator;
using RMLib.Alarms;
using RMLib.MessageBox;
using RMLib.Security;
using RMLib.VATView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    public partial class UC_HomePage : UserControl, INavigable, INavigationRequester
    {
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public UC_HomePage()
        {
            InitializeComponent();
        }

        public void OnNavigatedTo(object parameter)
        {
            // Esempio: se il parametro è una stringa con il nome utente
            if (parameter is string pageTitle)
            {
                lbl_title.Text = pageTitle;
            }

        }

        private void ClickEvent_exit(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("exit")) return;

            if (CustomMessageBox.ShowTranslated(MessageBoxTypeEnum.WARNING, "MSG_CLOSING_APP") == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        private void ClickEvent_openVAT(object sender, EventArgs e)
        {
            VATViewManager.ShowVAT();
        }

        private void ClickEvent_openAlarms(object sender, EventArgs e)
        {
            AlarmManager.OpenAlarmFormPage(RobotManager.formAlarmPage);
        }
    }
}
