using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM220930.Classes
{
    public class BufferedPanel : Panel
    {
        // Questo override applica lo stile WS_EX_COMPOSITED solo a questo pannello
        // e ai controlli che contiene, isolando l'effetto.
       /* protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Attiva WS_EX_COMPOSITED
                return cp;
            }
        }*/
    }
}
