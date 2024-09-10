using GruzChelb.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace GruzChelb.Pages
{
    /// <summary>
    /// Страница добавления новых сотрудников в базу данных
    /// </summary>
    public partial class WorkersAdd : Page
    {
        private Workers currentWorker = new Workers();
        public WorkersAdd(Workers selectedWorker)
        {
            InitializeComponent();

            List<string> workerRoles = new List<string>()
            {
                "Менеджер"
            };
            if (LoginSector.Login == "ADMIN")
                workerRoles.Add("Администратор");
            choseWorkerRole.ItemsSource = workerRoles;

            blockNamePage.Text = "Добавление сотрудника";
            if (selectedWorker != null)
            {
                blockNamePage.Text = "Редактирование данных сотрудника";
                currentWorker = selectedWorker;
            }

            DataContext = currentWorker;
        }

        private int CheckErrors()
        {
            StringBuilder errors = new StringBuilder();
            bool symbolNonLetter = false;
            bool smallLetter = false;

            if (string.IsNullOrWhiteSpace(inputWorkerName.Text))
            {
                errors.AppendLine("Укажите имя сотрудника");
            }
            else
            {
                foreach (char symb in inputWorkerName.Text)
                {
                    if (symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я')
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                for (int i = 1; i < inputWorkerName.Text.Length; i++)
                {
                    if (Char.IsUpper(inputWorkerName.Text[i]))
                    {
                        smallLetter = true;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Имя сотрудника должно содержать только русские буквы");
                    symbolNonLetter = false;
                }
                else if (inputWorkerName.Text.Length < 2)
                {
                    errors.AppendLine("Имя сотрудника должно содержать минимум 2 буквы");
                }
                else if (!char.IsUpper(inputWorkerName.Text[0]) || smallLetter == true)
                {
                    errors.AppendLine("Имя сотрудника должно начинаться с заглавной буквы, остальные с маленькой");
                    smallLetter = false;
                }
            }

            if (string.IsNullOrWhiteSpace(inputWorkerSecondName.Text))
            {
                errors.AppendLine("Укажите фамилию сотрудника");
            }
            else
            {
                foreach (char symb in inputWorkerSecondName.Text)
                {
                    if (symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я')
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                for (int i = 1; i < inputWorkerSecondName.Text.Length; i++)
                {
                    if (Char.IsUpper(inputWorkerSecondName.Text[i]))
                    {
                        smallLetter = true;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Фамилия сотрудника должна содержать только русские буквы");
                    symbolNonLetter = false;
                }
                else if (inputWorkerSecondName.Text.Length < 2)
                {
                    errors.AppendLine("Фамилия сотрудника должна содержать минимум 2 буквы");
                }
                else if (!char.IsUpper(inputWorkerSecondName.Text[0]) || smallLetter == true)
                {
                    errors.AppendLine("Фамилия сотрудника должна начинаться с заглавной буквы, остальные с маленькой");
                    smallLetter = false;
                }
            }

            if (string.IsNullOrWhiteSpace(inputWorkerThirdName.Text))
            {
                errors.AppendLine("Укажите отчество сотрудника");
            }
            else
            {
                foreach (char symb in inputWorkerThirdName.Text)
                {
                    if ((symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                for (int i = 1; i < inputWorkerThirdName.Text.Length; i++)
                {
                    if (Char.IsUpper(inputWorkerThirdName.Text[i]))
                    {
                        smallLetter = true;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Отчество сотрудника должно содержать только русские буквы");
                    symbolNonLetter = false;
                }
                else if (inputWorkerThirdName.Text.Length < 5)
                {
                    errors.AppendLine("Отчество сотрудника должно содержать минимум 5 букв");
                }
                else if (!char.IsUpper(inputWorkerThirdName.Text[0]) || smallLetter == true)
                {
                    errors.AppendLine("Отчество сотрудника должно начинаться с заглавной буквы, остальные с маленькой");
                }
            }

            if (choseWorkerRole.SelectedIndex == -1)
                errors.AppendLine("Выберите должность сотрудника");

            bool uniqLogin = false;

            if (string.IsNullOrWhiteSpace(inputLogin.Text))
            {
                errors.AppendLine("Укажите логин сотрудника");
            }
            else
            {
                if (blockNamePage.Text == "Добавление сотрудника")
                {
                    foreach (var worker in ChelGruzEntities.GetContext().Workers)
                    {
                        if (worker.Login == inputLogin.Text)
                        {
                            uniqLogin = true;
                            break;
                        }
                    }
                }
                else if (blockNamePage.Text == "Редактирование данных сотрудника")
                {
                    foreach (var worker in ChelGruzEntities.GetContext().Workers)
                    {
                        if (worker.Login == inputLogin.Text && currentWorker.Id != worker.Id)
                        {
                            uniqLogin = true;
                            break;
                        }
                    }
                }

                foreach (char symb in inputLogin.Text)
                {
                    if (!Char.IsDigit(symb) && (symb < 'A' || (symb > 'Z' && symb < 'a') || symb > 'z'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                if (inputLogin.Text == "ADMIN")
                {
                    errors.AppendLine("Данное имя не может быть использовано в качестве логина");
                }
                else if (uniqLogin is true)
                {
                    errors.AppendLine("Логин должен быть уникальным, такой вариант уже занят");
                }
                else if (symbolNonLetter is true)
                {
                    errors.AppendLine("Логин должен содержать только английские буквы или цифры");
                    symbolNonLetter = false;
                }
                else if (inputLogin.Text.Length < 5)
                {
                    errors.AppendLine("Логин должен содержать минимум 5 символов");
                }
            }

            if (string.IsNullOrWhiteSpace(inputPassword.Text))
            {
                errors.AppendLine("Укажите пароль сотрудника");
            }
            else
            {
                foreach (char symb in inputPassword.Text)
                {
                    if (Char.IsLetter(symb) && !(symb < 'А' || (symb > 'Я' && symb < 'а') || symb > 'я'))
                    {
                        symbolNonLetter = true;
                        break;
                    }
                }

                if (symbolNonLetter is true)
                {
                    errors.AppendLine("Пароль не должен содержать русские буквы");
                }
                else if (inputPassword.Text.Length < 5)
                {
                    errors.AppendLine("Пароль должен содержать минимум 5 символов");
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return 0;
            }

            currentWorker.Name = inputWorkerName.Text;
            currentWorker.SecondName = inputWorkerSecondName.Text;
            currentWorker.ThirdName = inputWorkerThirdName.Text;
            currentWorker.Role = choseWorkerRole.SelectedItem.ToString();
            currentWorker.Login = inputLogin.Text;
            currentWorker.Password = inputPassword.Text;

            return 1;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (CheckErrors() == 0)
                return;
            
            if (currentWorker.Id == 0)
                ChelGruzEntities.GetContext().Workers.Add(currentWorker);

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
                if (symbol < 'А' || (symbol  > 'Я' && symbol < 'а') || symbol > 'я')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void InputLoginPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var symbol in e.Text)
            {
                if (!Char.IsDigit(symbol) && (symbol < 'A' || (symbol > 'Z' && symbol < 'a') || symbol > 'z'))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void InputPassPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var symbol in e.Text)
            {
                if (!(symbol < 'А' || (symbol > 'Я' && symbol < 'а') || symbol > 'я'))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}