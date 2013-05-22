using System;

namespace NetConfig
{
    public enum UsedAlgorithm
    {
        Broadcast,
        Cucliced
    }

    public class StatusHandle
    {
        private bool working = false;
        private UsedAlgorithm alg = UsedAlgorithm.Cucliced;

        private object sync = null;
        private int interval = 100;

        public StatusHandle()
        {
            sync = new object();
        }

        public bool Working { get { return working; } set { working = value; } }
        public UsedAlgorithm Algorithm { get { return alg; } set { alg = value; } }

        public object Sync { get { return sync; } }
        public int Interval { get { return interval; } set { interval = value; } }
    }
}