using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SpaceInvaders
{
    class Timer{
        public DispatcherTimer timer = new DispatcherTimer();

        public Timer()
        {
            timer.Interval=new TimeSpan(10000);
        }

    }
}
