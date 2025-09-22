using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.src.RM220930.Classes.Navigator
{
    public interface INavigationRequester
    {
        event EventHandler<NavigateEventArgs> NavigateRequested;
    }
}
