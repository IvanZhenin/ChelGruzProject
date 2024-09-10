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
    /// Страница списка сотрудников для владельца системы, включающая в себя полный доступ
    /// </summary>
    public partial class WorkersListDev : Page
    {
        public WorkersListDev()
        {
            InitializeComponent();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
           FrameSector.MenuFrame.Navigate(new WorkersAdd(null));
        }

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            var workersForRemoving = dataGridWorkers.SelectedItems.Cast<Workers>().ToList();
            int quantOrders = 0;

            if (workersForRemoving.Count > 0)
            {
                foreach (var order in ChelGruzEntities.GetContext().Orders)
                {
                    if (order.NumWorker == workersForRemoving[0].Id)
                        quantOrders++;
                }
            }

            if (workersForRemoving.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите данные для удаления!");
            }
            else if (MessageBox.Show($"Вы точно хотите удалить {workersForRemoving[0].Role}а " +
                $"{workersForRemoving[0].SecondName} {workersForRemoving[0].Name}?",
                "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (quantOrders > 0 && MessageBox.Show($"При удалении данного сотрудника будет удалено {quantOrders} " +
                    $"заказов! Вы уверены?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var prodsInOrderForRemoving = ChelGruzEntities.GetContext().ProdInOrders.ToList();
                        var ordersForRemoving = ChelGruzEntities.GetContext().Orders.ToList();
                        foreach (var worker in workersForRemoving)
                        {
                            ordersForRemoving = ordersForRemoving.Where(p => p.NumWorker == worker.Id).ToList();
                        }
                        foreach (var order in ordersForRemoving)
                        {
                            prodsInOrderForRemoving = prodsInOrderForRemoving.Where(p => p.NumOrder == order.Id).ToList();
                        }

                        ChelGruzEntities.GetContext().ProdInOrders.RemoveRange(prodsInOrderForRemoving);
                        ChelGruzEntities.GetContext().Orders.RemoveRange(ordersForRemoving);
                        ChelGruzEntities.GetContext().Workers.RemoveRange(workersForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridWorkers.ItemsSource = ChelGruzEntities.GetContext().Workers.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else if (quantOrders == 0)
                {
                    try
                    {
                        ChelGruzEntities.GetContext().Workers.RemoveRange(workersForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridWorkers.ItemsSource = ChelGruzEntities.GetContext().Workers.ToList();
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
            FrameSector.MenuFrame.Navigate(new WorkersAdd((sender as Button).DataContext as Workers));
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
                dataGridWorkers.ItemsSource = ChelGruzEntities.GetContext().Workers.ToList();
            }
        }
    }
}