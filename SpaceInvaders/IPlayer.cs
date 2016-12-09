using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpaceInvaders
{
    interface IPlayer
    {
        void Shoot(object sender, EventArgs e);
        void Die();
        void DecrementLives();
        void ConfigureShoot(Canvas canvas, Point currentPosition);
        void DeleteBullet();
        void movePlayer(Point currentposition);

    }
}
