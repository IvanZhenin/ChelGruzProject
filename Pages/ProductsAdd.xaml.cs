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
    /// Страница добавления новых товаров в базу данных
    /// </summary>
    public partial class ProductsAdd : Page
    {
        private Products currentProduct = new Products();
        public ProductsAdd(Products selectedProduct)
        {
            InitializeComponent();

            blockNamePage.Text = "Добавление товара";
            if (selectedProduct != null)
            {
                blockNamePage.Text = "Редактирование данных товара";
                currentProduct = selectedProduct;
            }

            DataContext = currentProduct;
        }

        private int CheckErrors()
        {
            StringBuilder errors = new StringBuilder();
            bool uniqName = false;
            bool symbolNonLetter = false;
            int numLetter = 0;
            int numSpace = 0;
            int numDash = 0;

            if (string.IsNullOrWhiteSpace(inputProdName.Text))
            {
                errors.AppendLine("Укажите название товара");
            }
            else
            {
                if (blockNamePage.Text == "Добавление товара")
                {
                    foreach (var prod in ChelGruzEntities.GetContext().Products)
                    {
                        if (prod.Name == inputProdName.Text)
                        {
                            uniqName = true;
                            break;
                        }
                    }
                }
                else if (blockNamePage.Text == "Редактирование данных товара")
                {
                    foreach (var prod in ChelGruzEntities.GetContext().Products)
                    {
                        if (prod.Name == inputProdName.Text && currentProduct.Id != prod.Id)
                        {
                            uniqName = true;
                            break;
                        }
                    }
                }

                foreach (char symb in inputProdName.Text)
                {
                    if (Char.IsLetter(symb))
                        numLetter++;
                    if (symb == '-')
                        numDash++;
                    if (symb == ' ')
                        numSpace++;
                }

                foreach (char symb in inputProdName.Text)
                {
                    if (Char.IsLetter(symb) && (symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                    else if (!Char.IsLetter(symb) && symb != ' ' && symb != '-')
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                if (uniqName is true)
                {
                    errors.AppendLine("Название товара должно быть уникальным, такой вариант уже занят");
                }
                else if (symbolNonLetter is true)
                {
                    errors.AppendLine("Название не может содержать символы кроме русских букв, пробела и тире");
                }
                else if ((numDash > 1 || numSpace > 1) || (numSpace == 1 && numDash == 1))
                {
                    errors.AppendLine("Символ пробела или тире должен быть только один в названии");
                }
                else if (numLetter < 3)
                {
                    errors.AppendLine("Название товара должно содержать минимум 3 буквы");
                }
                else if (!char.IsUpper(inputProdName.Text[0]))
                {
                    errors.AppendLine("Название товара должно начинаться с заглавной буквы");
                }
            }

            if (string.IsNullOrWhiteSpace(inputPriceOne.Text))
            {
                errors.AppendLine("Укажите цену товара, поле не должно быть пустым");
            }
            else if (inputPriceOne.Text != null)
            {
                bool symbolCheck = false;
                foreach (char symb in inputPriceOne.Text)
                {
                    if (!Char.IsDigit(symb))
                    {
                        symbolCheck = true;
                        break;
                    }
                }
                if (symbolCheck is true)
                    errors.AppendLine("Цена должна содержать только цифры");
                else if (currentProduct.PriceOne < 50 || currentProduct.PriceOne > 1500)
                    errors.AppendLine("Укажите цену товара от 50 до 1500");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return 0;
            }
           
            currentProduct.Name = inputProdName.Text;
            currentProduct.PriceOne = Convert.ToInt32(inputPriceOne.Text);

            return 1;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (CheckErrors() == 0)
                return;

            if (currentProduct.Id == 0)
                ChelGruzEntities.GetContext().Products.Add(currentProduct);

            try
            {
                ChelGruzEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
                if (blockNamePage.Text == "Редактирование данных товара")
                    BaseSector.TotalAmountCheck();
                FrameSector.MenuFrame.GoBack();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Не удалось сохранить запись, были нарушены связи в базе данных");
            }
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            FrameSector.MenuFrame.GoBack();
        }

        private void InputNamePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var symbol in e.Text)
            {
                if ((Char.IsLetter(symbol) && (symbol < 'А' ||
                    (symbol > 'Я' && symbol < 'а') || symbol > 'я')) && symbol != '-')
                {
                    e.Handled = true;
                    return;
                }
                else if (!Char.IsLetter(symbol) && symbol != '-')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void InputPricePreviewTextInput(object sender, TextCompositionEventArgs e)
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