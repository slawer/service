using System;
using System.Threading;

namespace Calibration
{
    class Sync
    {
        private long blocked = 0;

        public Sync()
        {
        }

        public bool Block()
        {
            if (!Blocked)
            {
                return (Interlocked.Exchange(ref blocked, 1) == 1);
            }
            return false;
        }

        public void Relese()
        {
            if (Blocked)
            {
                Interlocked.Exchange(ref blocked, 0);
            }
        }

        public bool Blocked
        {
            get { return (Interlocked.Read(ref blocked) == 1); }
        }
    }
}