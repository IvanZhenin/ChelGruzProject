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
    ///  Страница добавления новых тарифов в базу данных
    /// </summary>
    public partial class RatesAdd : Page
    {
        private Rates currentRate = new Rates();
        public RatesAdd(Rates selectedRate)
        {
            InitializeComponent();

            blockNamePage.Text = "Добавление тарифа";
            if (selectedRate != null)
            {
                blockNamePage.Text = "Редактирование данных тарифа";
                currentRate = selectedRate;
            }

            List<string> typeCarList = new List<string>()
            {
                "Фургон",
                "Грузовик",
                "Пикап",
                "Тягач"
            };

            choseTypeCar.ItemsSource = typeCarList;

            DataContext = currentRate;
        }

        private int CheckErrors()
        {
            StringBuilder errors = new StringBuilder();
            bool uniqName = false;
            bool symbolNonLetter = false;
            int numLetter = 0;
            int numSpace = 0;
            int numDash = 0;

            if (string.IsNullOrWhiteSpace(inputRateName.Text))
            {
                errors.AppendLine("Укажите название тарифа");
            }
            else
            {
                if (blockNamePage.Text == "Добавление тарифа")
                {
                    foreach (var rate in ChelGruzEntities.GetContext().Rates)
                    {
                        if (rate.Name == inputRateName.Text)
                        {
                            uniqName = true;
                            break;
                        }
                    }
                }
                else if (blockNamePage.Text == "Редактирование данных тарифа")
                {
                    foreach (var rate in ChelGruzEntities.GetContext().Rates)
                    {
                        if (rate.Name == inputRateName.Text && currentRate.Id != rate.Id)
                        {
                            uniqName = true;
                            break;
                        }
                    }
                }

                foreach (char symb in inputRateName.Text)
                {
                    if (Char.IsLetter(symb) && (symb < 'A' || (symb > 'Z' && symb < 'a') || symb > 'z'))
                        numLetter++;
                    if (symb == '-')
                        numDash++;
                    if (symb == ' ')
                        numSpace++;
                }

                foreach (char symb in inputRateName.Text)
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
                    errors.AppendLine("Название тарифа должно быть уникальным, такой вариант уже занят");
                }
                else if (symbolNonLetter is true)
                {
                    errors.AppendLine("Название не может содержать символы кроме русских букв, пробела и тире");
                }
                else if ((numDash > 1 || numSpace > 1) || (numSpace == 1 && numDash == 1))
                {
                    errors.AppendLine("Символ пробела или тире должен быть только один в названии");
                }
                else if (numLetter < 5)
                {
                    errors.AppendLine("Название тарифа должно содержать минимум 5 букв");
                }
                else if (!char.IsUpper(inputRateName.Text[0]))
                {
                    errors.AppendLine("Название тарифа должно начинаться с заглавной буквы");
                }
            }

            if (choseTypeCar.SelectedIndex == -1)
                errors.AppendLine("Выберите тип автомобиля");

            if (string.IsNullOrWhiteSpace(inputPriceKm.Text))
            {
                errors.AppendLine("Укажите цену тарифа, поле не должно быть пустым");
            }
            else if (inputPriceKm.Text != null)
            {
                bool symbolCheck = false;
                foreach (char symb in inputPriceKm.Text)
                {
                    if (!Char.IsDigit(symb))
                    {
                        symbolCheck = true;
                        break;
                    }
                }
                if (symbolCheck is true)
                    errors.AppendLine("Цена должна содержать только цифры");
                else if (currentRate.PriceKm < 250 || currentRate.PriceKm > 5000)
                    errors.AppendLine("Укажите цену тарифа от 250 до 5000");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return 0;
            }

            currentRate.Name = inputRateName.Text;
            currentRate.TypeCar = Convert.ToString(choseTypeCar.SelectedItem);
            currentRate.PriceKm = Convert.ToInt32(inputPriceKm.Text);

            return 1;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (CheckErrors() == 0)
                return;

            if (currentRate.Id == 0)
                ChelGruzEntities.GetContext().Rates.Add(currentRate);

            try
            {
                ChelGruzEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
                if (blockNamePage.Text == "Редактирование данных тарифа")
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