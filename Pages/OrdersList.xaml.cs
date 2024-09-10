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
    /// Страница списка заказов компании
    /// </summary>
    public partial class OrdersList : Page
    {
        public OrdersList()
        {
            InitializeComponent();
            
            List<string> ratesList = new List<string>();
            ratesList.Add("Все тарифы");
            foreach (var rate in ChelGruzEntities.GetContext().Rates)
            {
                ratesList.Add(rate.Name);
            }
            cmbBoxSrchRates.ItemsSource = ratesList;
            cmbBoxSrchRates.SelectedIndex = 0;

            List<string> choseList = new List<string>() 
            {
                "Все статусы",
                "В ожидании",
                "Выполнен",
                "Выполняется",
                "Отказан"
            };

            cmbBoxSrchStatus.ItemsSource = choseList;
            cmbBoxSrchStatus.SelectedIndex = 0;

            UpdateOrders();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            FrameSector.MenuFrame.Navigate(new OrdersAdd(null));
        }

        private void UpdateOrders()
        {
            var currentOrders = ChelGruzEntities.GetContext().Orders.ToList();

            if (cmbBoxSrchRates.SelectedIndex > 0)
                currentOrders = currentOrders.Where(p => p.Rates.Name == Convert.ToString(cmbBoxSrchRates.SelectedItem)).ToList();

            if (cmbBoxSrchStatus.SelectedIndex > 0)
                currentOrders = currentOrders.Where(p => p.Status == Convert.ToString(cmbBoxSrchStatus.SelectedItem)).ToList();

            currentOrders = currentOrders.Where(p => p.Id.ToString().ToLower().Contains(txtBoxSrchOrders.Text.ToLower())).ToList();

            listViewOrders.ItemsSource = currentOrders.OrderBy(p => p.Id).ToList();
        }
        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            var ordersForRemoving = listViewOrders.SelectedItems.Cast<Orders>().ToList();
            var prodsInOrderForRemoving = ChelGruzEntities.GetContext().ProdInOrders.ToList();
            foreach (var order in ordersForRemoving)
            {
                prodsInOrderForRemoving = prodsInOrderForRemoving.Where(p => p.NumOrder == order.Id).ToList();
            }

            if (ordersForRemoving.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите данные для удаления!");
            }
            else if (MessageBox.Show($"Вы точно хотите удалить выбранные заказы в количестве {ordersForRemoving.Count()} единиц?", 
                "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ChelGruzEntities.GetContext().ProdInOrders.RemoveRange(prodsInOrderForRemoving);
                    ChelGruzEntities.GetContext().Orders.RemoveRange(ordersForRemoving);
                    ChelGruzEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    UpdateOrders();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Не удалось удалить записи, были нарушены связи в базе данных");
                }
            }
        }

        private void BtnRedClick(object sender, RoutedEventArgs e)
        {
            FrameSector.MenuFrame.Navigate(new OrdersAdd((sender as Button).DataContext as Orders));
        }

        private void BtnGoMainPageClick(object sender, RoutedEventArgs e)
        {
            FrameSector.MenuFrame.Content = new Pages.Statistics();
        }

        private void BtnProdListClick(object sender, RoutedEventArgs e)
        {
            ListProdsWindow listProdsWindow = new ListProdsWindow((sender as Button).DataContext as Orders);
            listProdsWindow.Closed += ListProdsWindowСlosed;
            listProdsWindow.ShowDialog();
        }
        private void ListProdsWindowСlosed(object sender, EventArgs e)
        {
            BaseSector.TotalAmountCheck();
            UpdateOrders();
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ChelGruzEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UpdateOrders();
            }
        }

        private void TxtBoxSrchOrdersPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var symbol in e.Text)
            {
                if (!Char.IsDigit(symbol))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void TxtBoxTargetTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrders();
        }

        private void CmbBoxSrchRatesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOrders();
        }

        private void CmbBoxSrchStatusSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOrders();
        }
    }
}