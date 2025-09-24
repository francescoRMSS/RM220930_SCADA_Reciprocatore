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

    }
}
