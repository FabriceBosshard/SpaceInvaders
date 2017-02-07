using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SpaceInvaders
{
    static class Laser
    {
        private const int length = 30;
        private const int width = 2;

        public static Ellipse CreateLaser()
        {
            Ellipse laser = new Ellipse()
            {
                Height = length,
                Width = width,
                Fill = Brushes.Red
            };
            return laser;
        }

        public static Ellipse CreateBomb()
        {
            Ellipse bomb = new Ellipse()
            {
                Height = 15,
                Width = 15,
                Fill = Brushes.Yellow
            };
            return bomb;
        }
    }
}
