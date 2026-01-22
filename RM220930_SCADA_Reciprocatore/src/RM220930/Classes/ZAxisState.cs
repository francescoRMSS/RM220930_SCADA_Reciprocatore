using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.src.RM220930.Classes
{
    public class ZAxisState
    {
        // BOOL
        public bool CmdOnAxe { get; set; }

        // REAL
        public float ActPosition { get; set; }

        // INT
        public short ErrorCode { get; set; }

        // altri tag futuri...
    }

}
