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


namespace SpaceInvaders
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();                        
            StartGame();
            Playground.Focus();
            DataContext = new MainWindowViewModel();

        }
        public MainWindowViewModel MainWindowViewModel => DataContext as MainWindowViewModel;

        private void StartGame()
        {       
            player = new Player();
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
                        if (!player.isShooting)
                        {
                            player.ConfigureShoot(Playground, Player);
                        }
                        break;
                    case Key.Left:
                    case Key.A:
                    case Key.NumPad4:
                        MainWindowViewModel.Move(DirectionPlayer.Left);
                        break;
                    case Key.Right:
                    case Key.D:
                    case Key.NumPad6:
                        MainWindowViewModel.Move(DirectionPlayer.Right);
                        break;
                    case Key.Escape:
                        Environment.Exit(0);
                        break;
                }

            }
        }
        private Player player;
    }
}
