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
       private static int length = 30;
       private static int width = 2;

       public static Ellipse CreateLaser()
        {
            Ellipse laser = new Ellipse();
            laser.Height = length;
            laser.Width = width;
            laser.Fill = Brushes.Red;
            return laser;
        }
    }
}
