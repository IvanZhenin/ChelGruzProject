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
    /// Страница добавления новых товаров в выбранный заказ
    /// </summary>
    public partial class ProdsAddToOrder : Page
    {
        private ProdInOrders currentPos = new ProdInOrders();
        private ProdInOrders actualPos = new ProdInOrders();
        public ProdsAddToOrder(ProdInOrders selectedProdList, Orders order)
        {
            InitializeComponent();
            blockNamePage.Text = "Добавление позиции";
            if (selectedProdList != null)
            {
                blockNamePage.Text = "Редактирование позиции";
                currentPos = selectedProdList;
                actualPos = selectedProdList;
            }
            
            foreach (var prod in ChelGruzEntities.GetContext().Products)
            {
                if (selectedProdList != null && currentPos.NumProd == prod.Id)
                {
                    inputProdNum.Text = prod.Name;
                }
            }

            inputOrderNum.Text = order.Id.ToString();
            var actualProdList = ChelGruzEntities.GetContext().Products.ToList();
            var actualProdData = ChelGruzEntities.GetContext().ProdInOrders.Where(p => p.NumOrder == order.Id).Select(p => p.NumProd);
            actualProdList = actualProdList.Where(p => !actualProdData.Contains(p.Id)).ToList();

            choseProdId.ItemsSource = actualProdList;

            DataContext = currentPos;
        }

        private int CheckErrors()
        {
            StringBuilder errors = new StringBuilder();
            int numProdActual = 0;

            foreach (var prod in ChelGruzEntities.GetContext().Products)
            {
                if (prod.Name == choseProdId.Text)
                    numProdActual = prod.Id;
            }


            if (choseProdId.Visibility == Visibility.Visible && choseProdId.SelectedIndex == -1)
                errors.AppendLine("Выберите товар");

            if (currentPos.Quantity < 1 || currentPos.Quantity > 100)
                errors.AppendLine("Укажите количество товара от 1 до 100");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return 0;
            }
            
            currentPos.NumOrder = Convert.ToInt32(inputOrderNum.Text);
            foreach (var prod in ChelGruzEntities.GetContext().Products)
            {
                if (choseProdId.Text == prod.Name)
                    currentPos.NumProd = Convert.ToInt32(prod.Id);
            }
            currentPos.Quantity = Convert.ToInt32(inputQuantity.Text);

            return 1;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (CheckErrors() == 0)
                return;

            if (inputProdNum.Visibility == Visibility.Collapsed)
                ChelGruzEntities.GetContext().ProdInOrders.Add(currentPos);

            try
            {
                ChelGruzEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
                FrameSector.ProdListFrame.GoBack();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Не удалось сохранить запись, были нарушены связи в базе данных");
            }
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void choseProdIdIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (actualPos.NumOrder != 0 && actualPos.NumProd != 0)
                choseProdId.Visibility = Visibility.Collapsed;
        }

        private void inputProdNumIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (actualPos.NumProd == 0)
            {
                borderInputProdPos.Visibility = Visibility.Collapsed;
                inputProdNum.Visibility = Visibility.Collapsed;
            }
        }

        private void InputQuantityPreviewTextInput(object sender, TextCompositionEventArgs e)
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
    }
}