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
    public partial class UC_service : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        public UC_service()
        {
            InitializeComponent();
            // Abilita il double buffering per questo UserControl
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Gestisce l'utilizzo del parametro passato durante la navigazione
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {

        }
    }
}
