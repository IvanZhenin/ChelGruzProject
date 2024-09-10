using GruzChelb.AppData;
using GruzChelb.Pages;
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
    /// Окно для добавления товаров в указанный ранее заказ
    /// </summary>
    public partial class ListProdsWindow : Window
    {
        public ListProdsWindow(Orders order)
        {
            InitializeComponent();
            FrameSector.ProdListFrame = prodListFrame;
            txtNumOrder.Text = "Заказ номер " + order.Id.ToString();
            prodListFrame.Navigate(new ProdsListInOrder(order));
        }

        private void CollButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ToolBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}