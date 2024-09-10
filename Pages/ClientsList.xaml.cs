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
    /// Страница списка клиентов компании
    /// </summary>
    public partial class ClientsList : Page
    {
        public ClientsList()
        {
            InitializeComponent();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на добавление клиента");
                return;
            }

            FrameSector.MenuFrame.Navigate(new ClientsAdd(null));
        }

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на удаление клиента");
                return;
            }

            var clientsForRemoving = dataGridClients.SelectedItems.Cast<Clients>().ToList();
            int quantOrders = 0;

            if (clientsForRemoving.Count > 0)
            {
                foreach (var order in ChelGruzEntities.GetContext().Orders)
                {
                    if (order.NumClient == clientsForRemoving[0].Id)
                        quantOrders++;
                }
            }
                

            if (clientsForRemoving.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите данные для удаления!");
            }
            else if (MessageBox.Show($"Вы точно хотите удалить клиента {clientsForRemoving[0].SecondName} {clientsForRemoving[0].Name}?",
                "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (quantOrders > 0 && MessageBox.Show($"При удалении данного клиента будет удалено {quantOrders} " +
                    $"заказов! Вы уверены?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var prodsInOrderForRemoving = ChelGruzEntities.GetContext().ProdInOrders.ToList();
                        var ordersForRemoving = ChelGruzEntities.GetContext().Orders.ToList();
                        foreach (var client in clientsForRemoving)
                        {
                            ordersForRemoving = ordersForRemoving.Where(p => p.NumClient == client.Id).ToList();
                        }
                        foreach (var order in ordersForRemoving)
                        {
                            prodsInOrderForRemoving = prodsInOrderForRemoving.Where(p => p.NumOrder == order.Id).ToList();
                        }

                        ChelGruzEntities.GetContext().ProdInOrders.RemoveRange(prodsInOrderForRemoving);
                        ChelGruzEntities.GetContext().Orders.RemoveRange(ordersForRemoving);
                        ChelGruzEntities.GetContext().Clients.RemoveRange(clientsForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridClients.ItemsSource = ChelGruzEntities.GetContext().Clients.ToList();
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Не удалось удалить запись, были нарушены связи в базе данных");
                    }
                }
                else if (quantOrders == 0)
                {
                    try
                    {
                        ChelGruzEntities.GetContext().Clients.RemoveRange(clientsForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridClients.ItemsSource = ChelGruzEntities.GetContext().Clients.ToList();
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Не удалось удалить запись, были нарушены связи в базе данных");
                    }
                }
            }
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на изменение данных клиента");
                return;
            }

            FrameSector.MenuFrame.Navigate(new ClientsAdd((sender as Button).DataContext as Clients));
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
                dataGridClients.ItemsSource = ChelGruzEntities.GetContext().Clients.ToList();
            }
        }
    }
}