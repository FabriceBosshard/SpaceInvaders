using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SpaceInvaders
{
    class Invaders
    {
        private int lives = 1;
        private int hitPoints = 10;
        private int count = 10;
        private Point OldPosition;
        bool isBorder = false;
        private bool hasTurned;

        public void Die()
        {
            //sc.score += hitPoints;
        }

        public void DecrementLives()
        {
            lives--;
            if (lives == 0)
            {
                Die();
            }
        }

        public bool CheckCollisionWithBorderRight(UIElement inv)
        {
            if (Canvas.GetLeft(inv) > 730)
            {
                return true;
            }
            return false;
        }

        public bool CheckCollisionWithBorderLeft(UIElement inv)
        {
            if (Canvas.GetLeft(inv) < 40)
            {              
                return true;
            }
            return false;
        }

        bool direction = false;

        public void moveInvader(List<UIElement> invaders)
        {
            foreach (UIElement inv in invaders)
            {
                OldPosition.X = Canvas.GetLeft(inv);
                OldPosition.Y = Canvas.GetTop(inv);

                if (direction)
                {
                    Canvas.SetLeft(inv, OldPosition.X - 35);
                    
                }
                else
                {
                    Canvas.SetLeft(inv, OldPosition.X + 35);
                    
                }
            }
            hasTurned = false;
            isBorder = false;
        }

        public bool CheckCollisionWithLaser()
        {

            return false;
        }

        public void CheckCollision(List<UIElement> invaders)
        {
            foreach (UIElement inv in invaders)
            {

                if (CheckCollisionWithBorderLeft(inv))
                {
                    isBorder = true;
                    direction = false;
                }
                else if (CheckCollisionWithBorderRight(inv))
                {
                    isBorder = true;
                    direction = true;
                }
            }
            if (isBorder && !hasTurned)
            {
                oneDown(invaders);

            }
            else
            {
                moveInvader(invaders);
            }
        }

        public void oneDown(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
            {
                OldPosition.X = Canvas.GetLeft(inv);
                OldPosition.Y = Canvas.GetTop(inv);

                Canvas.SetTop(inv, OldPosition.Y + 50);
                Canvas.SetLeft(inv, OldPosition.X);
            }
            hasTurned = true;
        }           
    }
}

