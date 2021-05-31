using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    public class Config
    {
        public int Port { get; set; } = 7500;
        public bool IdleMode { get; set; } = true;
        public int IdleModeTimer { get; set; } = 15;

    }
}
