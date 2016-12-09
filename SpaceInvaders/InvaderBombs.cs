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
        static class InvaderBomb
        {            
            private static int length = 20;
            private static int width = 20;

            public static Ellipse CreateBomb()
            {
                Ellipse bomb = new Ellipse();
                bomb.Height = length;
                bomb.Width = width;
                bomb.Fill = Brushes.DarkViolet;
                return bomb;
            }
        }
    }
}
