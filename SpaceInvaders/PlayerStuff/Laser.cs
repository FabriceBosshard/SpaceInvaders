using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaceInvaders
{
    static class Laser
    {

        public static Ellipse CreateLaser()
        {
            Ellipse laser = new Ellipse()
            {
                Height = 30,
                Width = 2,
                Fill = Brushes.AntiqueWhite
            };
            return laser;
        }

        public static Ellipse CreateBomb()
        {
            Ellipse superLaser = new Ellipse()
            {
                Height = 30,
                Width = 2,
                Fill = Brushes.Red
            };
            return superLaser;
        }

        public static UIElement CreateVBomb()
        {
            UIElement Vbomb = new Image
            {
                Height = 200,
                Width = 200,
                Source = new BitmapImage(new Uri("PlayerStuff/blackhole.png", UriKind.Relative))
            };

            return Vbomb;
        }

        public static UIElement CreateSupernova()
        {
            UIElement nova = new Image
            {
                Width = 250,
                Height = 125,
                Source = new BitmapImage(new Uri("PlayerStuff/supernova.png", UriKind.Relative))
            };

            return nova;
        }

        public static UIElement CreateApokalypse()
        {
            UIElement apokalypse = new Image
            {
                Width = 100,
                Height = 100,
                Source = new BitmapImage(new Uri("PlayerStuff/apokalypse.png", UriKind.Relative))
            };

            return apokalypse;
        }
    }
}
