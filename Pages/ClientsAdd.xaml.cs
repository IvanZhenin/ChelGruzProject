using GruzChelb.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Страница добавления новых клиентов в базу данных
    /// </summary>
    public partial class ClientsAdd : Page
    {
        private Clients currentClient = new Clients();
        public ClientsAdd(Clients selectedClient)
        {
            InitializeComponent();

            blockNamePage.Text = "Добавление клиента";
            if (selectedClient != null)
            {
                blockNamePage.Text = "Редактирование данных клиента";
                currentClient = selectedClient;
            }

            DataContext = currentClient;
        }

        private int CheckErrors()
        {
            StringBuilder errors = new StringBuilder();
            bool symbolNonLetter = false;
            bool numberFormatError = false;
            bool smallLetter = false;

            if (inputTelephone.Text.Length != 15)
            {
                numberFormatError = true;
            }
            else
            {
                for (int i = 0; i < inputTelephone.Text.Length; i++)
                {
                    if (i == 0 && inputTelephone.Text[i] != '7')
                    {
                        numberFormatError = true;
                        break;
                    }
                    else if (i == 1 || i == 5 || i == 9 || i == 12)
                    {
                        if (inputTelephone.Text[i] != '-')
                        {
                            numberFormatError = true;
                            break;
                        }
                    }
                    else
                    {
                        if (!Char.IsDigit(inputTelephone.Text[i]))
                        {
                            numberFormatError = true;
                            break;
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(inputClientName.Text))
            {
                errors.AppendLine("Укажите имя клиента");
            }
            else
            {
                foreach (char symb in inputClientName.Text)
                {
                    if ((symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                for (int i = 1; i < inputClientName.Text.Length; i++)
                {
                    if (Char.IsUpper(inputClientName.Text[i]))
                    {
                        smallLetter = true;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Имя клиента должно содержать только русские буквы");
                    symbolNonLetter = false;
                }
                else if (inputClientName.Text.Length < 2)
                {
                    errors.AppendLine("Имя клиента должно содержать минимум 2 буквы");
                }
                else if (!char.IsUpper(inputClientName.Text[0]) || smallLetter == true)
                {
                    errors.AppendLine("Имя клиента должно начинаться с заглавной буквы, остальные с маленькой");
                    smallLetter = false;
                }
            }

            if (string.IsNullOrWhiteSpace(inputClientSecondName.Text))
            {
                errors.AppendLine("Укажите фамилию клиента");
            }
            else
            {
                foreach (char symb in inputClientSecondName.Text)
                {
                    if ((symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                for (int i = 1; i < inputClientSecondName.Text.Length; i++)
                {
                    if (Char.IsUpper(inputClientSecondName.Text[i]))
                    {
                        smallLetter = true;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Фамилия клиента должна содержать только русские буквы");
                    symbolNonLetter = false;
                }
                else if (inputClientSecondName.Text.Length < 2)
                {
                    errors.AppendLine("Фамилия клиента должна содержать минимум 2 буквы");
                }
                else if (!char.IsUpper(inputClientSecondName.Text[0]) || smallLetter == true)
                {
                    errors.AppendLine("Фамилия клиента должна начинаться с заглавной буквы, остальные с маленькой");
                    symbolNonLetter = false;
                }
            }

            if (string.IsNullOrWhiteSpace(inputClientThirdName.Text))
            {
                errors.AppendLine("Укажите отчество клиента");
            }
            else
            {
                foreach (char symb in inputClientThirdName.Text)
                {
                    if ((symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                for (int i = 1; i < inputClientThirdName.Text.Length; i++)
                {
                    if (Char.IsUpper(inputClientThirdName.Text[i]))
                    {
                        smallLetter = true;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Отчество клиента должно содержать только русские буквы");
                    symbolNonLetter = false;
                }
                else if (inputClientThirdName.Text.Length < 5)
                {
                    errors.AppendLine("Отчество клиента должно содержать минимум 5 букв");
                }
                else if (!char.IsUpper(inputClientThirdName.Text[0]))
                {
                    errors.AppendLine("Отчество клиента должно начинаться с заглавной буквы, остальные с маленькой");
                }
            }

            if (string.IsNullOrWhiteSpace(inputTelephone.Text))
            {
                errors.AppendLine("Укажите номер телефона клиента");
            }
            else if (numberFormatError is true)
            {
                errors.AppendLine("Телефон должен быть строго оформлен по образцу: 7-000-000-00-00, первая цифра обязательно 7");
            }

            if (string.IsNullOrWhiteSpace(inputAddress.Text))
            {
                errors.AppendLine("Укажите адресс клиента");
            }
            else
            {
                if (!inputAddress.Text.Any(char.IsLetter))
                {
                    errors.AppendLine("Адрес клиента не может содержать только цифры");
                }
                else if (inputAddress.Text.Length <= 1)
                {
                    errors.AppendLine("Адрес клиента не может содержать только 1 символ, укажите больше");
                }
                else if (inputAddress.Text.Length > 1)
                {
                    int numChar = 0;
                    for (int i = 0; i < inputAddress.Text.Length; i++)
                    {
                        if (char.IsLetter(inputAddress.Text[i]))
                        {
                            numChar++;
                            if (numChar >= 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            numChar = 0;
                        }
                    }

                    foreach (char symb in inputAddress.Text)
                    {
                        if (Char.IsLetter(symb))
                        {
                            if (symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я')
                            {
                                symbolNonLetter = true;
                            }
                        }
                    }

                    if (numChar <= 1)
                    {
                        errors.AppendLine("Адрес клиента должен содержать минимум 2 буквы подряд");
                    }
                    else if (symbolNonLetter == true)
                    {
                        errors.AppendLine("Адрес не должен содержать английских букв");
                    }
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return 0;
            }

            currentClient.Name = inputClientName.Text;
            currentClient.SecondName = inputClientSecondName.Text;
            currentClient.ThirdName = inputClientThirdName.Text;
            currentClient.Telephone = inputTelephone.Text;
            currentClient.Address = inputAddress.Text;

            return 1;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (CheckErrors() == 0)
                return;

            if (currentClient.Id == 0)
                ChelGruzEntities.GetContext().Clients.Add(currentClient);

            try
            {
                ChelGruzEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
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
                if (symbol < 'А' || (symbol > 'Я' && symbol < 'а') || symbol > 'я')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void InputMobilePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var symbol in e.Text)
            {
                if (!Char.IsDigit(symbol) && symbol != '-')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void InputAddressPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var symbol in e.Text)
            {
                if (!Char.IsDigit(symbol) && (symbol < 'А' || (symbol > 'Я' && symbol < 'а') || symbol > 'я')
                    && symbol != ' ' && symbol != '-' && symbol != '.' && symbol != ',')
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}