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
    /// Страница отображения списка товаров в выбранном заказе
    /// </summary>
    public partial class ProdsListInOrder : Page
    {
        private Orders orderPos = new Orders();
        public ProdsListInOrder(Orders order)
        {
            InitializeComponent();
            orderPos = order;
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProdsAddToOrder((sender as Button).DataContext as ProdInOrders, orderPos));
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProdsAddToOrder(null, orderPos));
        }

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            var prodOrdersForRemoving = dataGridProds.SelectedItems.Cast<ProdInOrders>().ToList();

            if (prodOrdersForRemoving.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите данные для удаления!");
            }
            else if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы в количестве {prodOrdersForRemoving.Count()} единиц?", 
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ChelGruzEntities.GetContext().ProdInOrders.RemoveRange(prodOrdersForRemoving);
                    ChelGruzEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    dataGridProds.ItemsSource = ChelGruzEntities.GetContext().ProdInOrders.Where(p => p.NumOrder == orderPos.Id).ToList();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Не удалось удалить записи, были нарушены связи в базе данных");
                }
            }
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ChelGruzEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dataGridProds.ItemsSource = ChelGruzEntities.GetContext().ProdInOrders.Where(p => p.NumOrder == orderPos.Id).ToList();
            }
        }
    }
}