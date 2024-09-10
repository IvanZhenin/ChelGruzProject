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
    /// Страница списка товаров компании
    /// </summary>
    public partial class ProductsList : Page
    {
        public ProductsList()
        {
            InitializeComponent();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на добавление товара");
                return;
            }

            FrameSector.MenuFrame.Navigate(new ProductsAdd(null));
        }

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на удаление товара");
                return;
            }

            var prodsForRemoving = dataGridProds.SelectedItems.Cast<Products>().ToList();
            var prodsInOrderForRemoving = ChelGruzEntities.GetContext().ProdInOrders.ToList();
            int quantProds = 0;

            if (prodsForRemoving.Count > 0)
            {
                foreach (var position in prodsForRemoving)
                {
                    prodsInOrderForRemoving = prodsInOrderForRemoving.Where(p => p.NumProd == position.Id).ToList();
                }
            }
                

            foreach (var pos in prodsInOrderForRemoving)
            {
                quantProds++;
            }

            if (prodsForRemoving.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите данные для удаления!");
            }
            else if (MessageBox.Show($"Вы точно хотите удалить товар {prodsForRemoving[0].Name}?",
                "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (quantProds > 0 && MessageBox.Show($"При удалении данного товара будет удалено {quantProds} записей" +
                    $" из заказов! Вы уверены?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        ChelGruzEntities.GetContext().ProdInOrders.RemoveRange(prodsInOrderForRemoving);
                        ChelGruzEntities.GetContext().Products.RemoveRange(prodsForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridProds.ItemsSource = ChelGruzEntities.GetContext().Products.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else if (quantProds == 0)
                {
                    try
                    {
                        ChelGruzEntities.GetContext().Products.RemoveRange(prodsForRemoving);
                        ChelGruzEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        dataGridProds.ItemsSource = ChelGruzEntities.GetContext().Products.ToList();
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Не удалось удалить записи, были нарушены связи в базе данных");
                    }
                }
            }
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            if (LoginSector.Role == "Менеджер")
            {
                MessageBox.Show("У вас нет прав на изменение данных товара");
                return;
            }

            FrameSector.MenuFrame.Navigate(new ProductsAdd((sender as Button).DataContext as Products));
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
                dataGridProds.ItemsSource = ChelGruzEntities.GetContext().Products.ToList();
            }
        }
    }
}