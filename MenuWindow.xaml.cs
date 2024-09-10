using GruzChelb.AppData;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GruzChelb
{
    /// <summary>
    /// Главное рабочее окно сотрудников, взаимодействие с системой и базой данных
    /// </summary>
    public partial class MenuWindow : Window
    {
        public static MainWindow MainOpen;
        public MenuWindow()
        {
            InitializeComponent();
            FrameSector.MenuFrame = menuFrame;
            BaseSector.TotalAmountCheck();
            fullButton.ToolTip = "Развернуть";
            FrameSector.MenuFrame.Navigate(new Pages.Statistics());

            if (LoginSector.Login == "ADMIN")
            {
                txtBlockWorkerAuto.Text = "Система авторизована под аккаунтом создателя" +
                    "\nПредоставлены полные права по управлению системой";
            }
            else
            {
                foreach (var worker in ChelGruzEntities.GetContext().Workers)
                {
                    if (worker.Id == LoginSector.IdNumber)
                    {
                        txtBlockWorkerAuto.Text = "Аккаунт " + worker.Login + "\n" +
                            worker.Role + " " + worker.Name + " " + worker.SecondName;
                    }
                }
            }
        }
        private void CloseButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CollButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void fullButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                fullButton.ToolTip = "Развернуть";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                fullButton.ToolTip = "Свернуть в окно";
            }
        }
        private void ToolBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
       
        private void OrdersClick(object sender, RoutedEventArgs e)
        {
            if(FrameSector.MenuFrame.Content == null || !(FrameSector.MenuFrame.Content is OrdersList))
            {
                FrameSector.MenuFrame.Navigate(new OrdersList());
            }
        }

        private void WorkersClick(object sender, RoutedEventArgs e)
        {
            if (FrameSector.MenuFrame.Content == null || !(FrameSector.MenuFrame.Content is Pages.WorkersList))
            {
                if (LoginSector.Role != "Владелец")
                {
                    FrameSector.MenuFrame.Navigate(new Pages.WorkersList());
                }
                else if (!(menuFrame.Content is Pages.WorkersListDev))
                {
                    FrameSector.MenuFrame.Navigate(new Pages.WorkersListDev());
                }
                
            }
        }

        private void ClientsClick(object sender, RoutedEventArgs e)
        {
            if (FrameSector.MenuFrame.Content == null || !(FrameSector.MenuFrame.Content is Pages.ClientsList))
            {
                FrameSector.MenuFrame.Navigate(new Pages.ClientsList());
            }
        }

        private void RatesClick(object sender, RoutedEventArgs e)
        {
            if (FrameSector.MenuFrame.Content == null || !(FrameSector.MenuFrame.Content is Pages.RatesList))
            {
                FrameSector.MenuFrame.Navigate(new Pages.RatesList());
            }
        }

        private void ProductsClick(object sender, RoutedEventArgs e)
        {
            if (FrameSector.MenuFrame.Content == null || !(FrameSector.MenuFrame.Content is Pages.ProductsList))
            {
                FrameSector.MenuFrame.Navigate(new Pages.ProductsList());
            }
        }

        private void UserSwapClick(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = (MenuWindow)Window.GetWindow(this);
            MainOpen = new MainWindow();
            MainOpen.Show();
            menuWindow.Close();
        }
    }
}