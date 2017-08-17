using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Prism.Commands;
using SpaceInvaders.Shop;


namespace SpaceInvaders
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Board : Window
    {
        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;


        public Board()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            StartGame();
            Playground.Focus();
            DataContext = new MainWindowViewModel();
            MainWindowViewModel.Left = 397;
            MainWindowViewModel.Top = 581;          
        }
        public MainWindowViewModel MainWindowViewModel => DataContext as MainWindowViewModel;

        private void StartGame()
        {
            player = new Player();
            UIObjects.PlayerList.Add(Player);
            new InvaderRow(Playground);

        }

        private void NewGameButton(object sender, RoutedEventArgs e)
        {
            StartGame();

            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            if (!MainWindowViewModel.IsPaused)
            {
                switch (e.Key)
                {
                    case Key.Space:
                        if (checkShootings())
                        {
                            player.ConfigureShoot(Playground, Player);
                        }
                        break;
                    case Key.V:
                        if (checkShootings())
                        {
                            player.ConfigureSuperLaser(Playground, Player);
                        }
                        break;
                    case Key.B:
                        if (checkShootings())
                        {
                            player.ConfigureVBomb(Playground, Player);
                        }
                        break;
                    case Key.N:
                        if (checkShootings())
                        {
                            player.ConfigureSuperNova(Playground, Player);
                        }
                        break;
                    case Key.M:
                        if (checkShootings())
                        {
                            player.ConfigureApokalypse(Playground, Player);
                        }
                        break;
                    case Key.Left:
                    case Key.A:
                    case Key.NumPad4:
                        //MainWindowViewModel.Move(DirectionPlayer.Left);
                        if (MainWindowViewModel.previousDirection != (int)DirectionPlayer.Right)
                            MainWindowViewModel.direction = (int)DirectionPlayer.Left;
                        break;
                    case Key.Right:
                    case Key.D:
                    case Key.NumPad6:
                        //MainWindowViewModel.Move(DirectionPlayer.Right);
                        if (MainWindowViewModel.previousDirection != (int)DirectionPlayer.Left)
                            MainWindowViewModel.direction = (int)DirectionPlayer.Right;
                        break;
                    case Key.Up:
                    case Key.W:
                    case Key.NumPad8:
                        //MainWindowViewModel.Move(DirectionPlayer.Right);
                        if (MainWindowViewModel.previousDirection != (int)DirectionPlayer.Down)
                            MainWindowViewModel.direction = (int)DirectionPlayer.Up;
                        break;
                    case Key.Down:
                    case Key.S:
                    case Key.NumPad5:
                        //MainWindowViewModel.Move(DirectionPlayer.Right);
                        if (MainWindowViewModel.previousDirection != (int)DirectionPlayer.Up)
                            MainWindowViewModel.direction = (int)DirectionPlayer.Down;
                        break;
                    case Key.Escape:
                        MainWindow a = new MainWindow();
                        a.Show();
                        this.Close();
                        break;
                    case Key.Enter:
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                        break;
                }

            }
        }

        private bool checkBombs()
        {
            if (!player.isShootingVBomb && !player.isShootingBomb)
            {
                return true;
            }
            return false;
        }

        private bool checkSpecials()
        {
            if (!player.isShootingSuperNova && !player.isShootingApokalypse)
            {
                return true;
            }
            return false;
        }

        private bool checkShootings()
        {
            if (!player.isShooting && !player.isShootingSuperNova && !player.isShootingApokalypse && !player.isShootingVBomb && !player.isShootingBomb)
            {
                return true;
            }
            return false;
        }

        private Player player;

    }
}
