using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw2
{
    public class InterlockedInt
    {
        private int cnt;

        public int Cnt
        {
            get
            {
                return cnt;
            }
        }

        public void Increment()
        {
            Interlocked.Increment(ref cnt);
        }
    }
}
