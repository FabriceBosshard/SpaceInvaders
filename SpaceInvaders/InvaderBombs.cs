using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SpaceInvaders
{
    class InvaderBombs
    {
        private const int length = 20;
        private const int width = 20;

        public static Ellipse CreateLaser()
        {
            Ellipse laser = new Ellipse()
            {
                Height = length,
                Width = width,
                Fill = Brushes.DarkViolet
            };
            return laser;
        }
    }
}
