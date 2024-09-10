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

namespace GruzChelb.Pages
{
    /// <summary>
    ///  Страница списка тарифов компании
    /// </summary>
    public partial class RatesList : Page
    {
        public RatesList()
        {
            InitializeComponent();
        }
        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на добавление тарифа");
                return;
            }

            FrameSector.MenuFrame.Navigate(new RatesAdd(null));
        }

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на удаление тарифа");
                return;
            }

            var ratesForRemoving = dataGridRates.SelectedItems.Cast<Rates>().ToList();
            int quantOrders = 0;

            if (ratesForRemoving.Count > 0)
            {
                foreach (var order in ChelGruzEntities.GetContext().Orders)
                {
                    if (order.NumRate == ratesForRemoving[0].Id)
                        quantOrders++;
                }
            }
                

            if (ratesForRemoving.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите данные для удаления!");
            }
            else if (MessageBox.Show($"Вы точно хотите удалить тариф {ratesForRemoving[0].Name}?",
                "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (quantOrders > 0 && MessageBox.Show($"При удалении данного тарифа будет удалено {quantOrders} " +
                    $"заказов! Вы уверены?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var prodsInOrderForRemoving = ChelGruzEntities.GetContext().ProdInOrders.ToList();
                        var ordersForRemoving = ChelGruzEntities.GetContext().Orders.ToList();
                        foreach (var rate in ratesForRemoving)
                        {
                            ordersForRemoving = ordersForRemoving.Where(p => p.NumRate == rate.Id).ToList();
                        }
                        foreach (var order in ordersForRemoving)
                        {
                            prodsInOrderForRemoving = prodsInOrderForRemoving.Where(p => p.NumOrder == order.Id).ToList();
                        }

                        ChelGruzEntities.GetContext().ProdInOrders.RemoveRange(prodsInOrderForRemoving);
                        ChelGruzEntities.GetContext().Orders.RemoveRange(ordersForRemoving);
                        ChelGruzEntities.GetContext().Rates.RemoveRange(ratesForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridRates.ItemsSource = ChelGruzEntities.GetContext().Rates.ToList();
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Не удалось удалить записи, были нарушены связи в базе данных");
                    }
                }
                else if (quantOrders == 0)
                {
                    try
                    {
                        ChelGruzEntities.GetContext().Rates.RemoveRange(ratesForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridRates.ItemsSource = ChelGruzEntities.GetContext().Rates.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на изменение данных тарифа");
                return;
            }

            FrameSector.MenuFrame.Navigate(new RatesAdd((sender as Button).DataContext as Rates));
        }

        private void BtnGoMainPageClick(object sender, RoutedEventArgs e)
        {
            FrameSector.MenuFrame.Content = new Pages.Statistics();
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ChelGruzEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dataGridRates.ItemsSource = ChelGruzEntities.GetContext().Rates.ToList();
            }
        }
    }
}