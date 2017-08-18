using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window
    {
        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;


        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            ShopValues.LoadFromJson();
            this.Closing += OnClosing;
            DataContext = MainWindowViewModel.InstanceCreation();
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            ShopValues.saveToJson();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Board b = new Board();
            b.Show();
            this.Close();
        }

        private void shop_Click(object sender, RoutedEventArgs e)
        {
            ShopView s = new ShopView();
            s.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ShopValues.saveToJson();
            Close();
        }
    }

}
