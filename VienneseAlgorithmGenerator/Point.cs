using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VienneseAlgorithmGenerator
{
    class Point
    {
        public bool WasZero { get; set; }
        public int Val { get; set; }

        public Point(int val)
        {
            Val = val;
            WasZero = false;
        }

        public Point(int val, bool zero)
        {
            Val = val;
            WasZero = zero;
        }

        public override string ToString()
        {
            return Val.ToString();
        }
    }
}
