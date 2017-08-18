using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// Interaktionslogik für ShopView.xaml
    /// </summary>
    public partial class ShopView : Window
    {
        private const int vbombCost = 500;
        private const int supernvaCost = 1500;
        private const int apokalypseCost = 3000;
        private const int heartCost = 9999;
        MainWindowViewModel _mh = MainWindowViewModel.InstanceCreation();
        public ShopView()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            ShopValues.LoadFromJson();
            DataContext = _mh;
            this.Closing += OnClosing;
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            ShopValues.saveToJson();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ShopValues.saveToJson();
            MainWindow a = new MainWindow();
            a.Show();
            Close();
        }

        private void vbomb_Click(object sender, RoutedEventArgs e)
        {
            if (_mh.NotifyHandler.GoldAmount >= vbombCost)
            {
                _mh.NotifyHandler.GoldAmount -= vbombCost;
                _mh.NotifyHandler.VBombCount += 1;
                ShopValues.saveToJson();
            }

        }

        private void supernova_Click(object sender, RoutedEventArgs e)
        {
            if (_mh.NotifyHandler.GoldAmount >= supernvaCost)
            {
                _mh.NotifyHandler.GoldAmount -= supernvaCost;
                _mh.NotifyHandler.SupernovaCount += 1;
                ShopValues.saveToJson();
            }
        }

        private void apokalypse_Click(object sender, RoutedEventArgs e)
        {
            if (_mh.NotifyHandler.GoldAmount >= apokalypseCost)
            {
                _mh.NotifyHandler.GoldAmount -= apokalypseCost;
                _mh.NotifyHandler.Apokalypsecount += 1;
                ShopValues.saveToJson();
            }
            
        }

        private void HeartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mh.NotifyHandler.GoldAmount >= heartCost)
            {
                _mh.NotifyHandler.GoldAmount -= heartCost;
                _mh.NotifyHandler.LivesExceed += 1;
                ShopValues.saveToJson();
            }

        }

        private void vbomb_Copy_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                vbomb_Click(sender,e);
            }
        }

        private void vbomb_Copy2_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                apokalypse_Click(sender,e);
            }
        }

        private void vbomb_Copy3_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                supernova_Click(sender, e);
            }
        }

        private void vbomb_Copy1_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                HeartButton_Click(sender, e);
            }
        }
    }
}
