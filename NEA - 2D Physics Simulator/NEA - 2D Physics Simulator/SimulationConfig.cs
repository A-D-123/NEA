using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NEA___2D_Physics_Simulator
{
    internal class SimulationConfig
    {
        public float PPM { get; set; } = 25;
        public float Gravity { get; set; } = 9.81f;
        public float AirDensity { get; set;} = 1.225f;
    }
}
