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

namespace RM.src.RM220930.Forms.Plant
{
    public partial class UC_axis : UserControl, INavigable, INavigationRequester
    {
        public UC_axis()
        {
            InitializeComponent();
        }

        public event EventHandler<NavigateEventArgs> NavigateRequested;

        public void OnNavigatedTo(object parameter)
        {
            // Esempio: se il parametro è una stringa con il nome utente
            if (parameter is string pageTitle)
            {
                lbl_title.Text = pageTitle;
            }
        }
    }
}
