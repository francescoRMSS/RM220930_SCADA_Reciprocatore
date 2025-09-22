using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NavigateEventArgs : EventArgs
{
    public string PageKey { get; }
    public object Parameter { get; }

    public NavigateEventArgs(string pageKey, object parameter = null)
    {
        PageKey = pageKey;
        Parameter = parameter;
    }
}
