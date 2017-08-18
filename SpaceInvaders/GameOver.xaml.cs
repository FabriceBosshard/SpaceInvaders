using System;
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
using System.Windows.Shapes;
using SpaceInvaders.Shop;

namespace SpaceInvaders
{
    /// <summary>
    /// Interaktionslogik für GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        public GameOver()
        {            
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.KeyUp += NewGame;
            DataContext = MainWindowViewModel.InstanceCreation();

        }

        public MainWindowViewModel MainWindowViewModel => DataContext as MainWindowViewModel;

        private void NewGame(object sender, KeyEventArgs e)
        {
            Thread.Sleep(3000);
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
            UIObjects.GameOver = false;
            ShopValues.saveToJson();
        }
    }
}
