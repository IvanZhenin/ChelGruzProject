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

namespace GruzChelb
{
    /// <summary>
    /// Страница добавления новых заказов в базу данных
    /// </summary>
    public partial class OrdersAdd : Page
    {
        private Orders currentOrder = new Orders();
        public OrdersAdd(Orders selectedOrder)
        {
            InitializeComponent();

            List<String> workersList = new List<string>(); 
            List<int> workersPos = new List<int>();
            foreach (var worker in ChelGruzEntities.GetContext().Workers)
            {
                workersList.Add(worker.Id + ". " + worker.SecondName + " " + worker.Name + " " + worker.ThirdName);
                workersPos.Add(worker.Id);
            }
            choseWorkerId.ItemsSource = workersList;

            List<String> clientsList = new List<string>();
            List<int> clientsPos = new List<int>();
            foreach (var client in ChelGruzEntities.GetContext().Clients)
            {
                clientsList.Add(client.Id + ". " + client.SecondName + " " + client.Name + " " + client.ThirdName);
                clientsPos.Add(client.Id);
            }
            choseClientId.ItemsSource = clientsList;

            List<string> statusList = new List<string>()
            {
                "В ожидании",
                "Выполняется",
                "Выполнен"
            };
            
            blockNamePage.Text = "Добавление заказа";
            
            if (selectedOrder != null)
            {
                blockNamePage.Text = "Редактирование заказа";
                currentOrder = selectedOrder;
                statusList.Add("Отказан");

                int posNumber = 0;
                foreach (var pos in workersPos)
                {
                    posNumber++;
                    if (pos == selectedOrder.NumWorker)
                        choseWorkerId.SelectedIndex = posNumber - 1;
                }

                posNumber = 0;
                foreach (var pos in clientsPos)
                {
                    posNumber++;
                    if (pos == selectedOrder.NumClient)
                        choseClientId.SelectedIndex = posNumber - 1;
                }
            }

            choseStatusOrder.ItemsSource = statusList;
            choseRateId.ItemsSource = ChelGruzEntities.GetContext().Rates.ToList();

            DataContext = currentOrder;
        }

        private int CheckErrors()
        {
            StringBuilder errors = new StringBuilder();
            bool symbolNonLetter = false;

            if (choseWorkerId.SelectedIndex == -1)
                errors.AppendLine("Выберите сотрудника");

            if (choseClientId.SelectedIndex == -1)
                errors.AppendLine("Выберите клиента");

            if (choseRateId.SelectedIndex == -1)
                errors.AppendLine("Укажите тариф");

            if (string.IsNullOrWhiteSpace(inputAddressDelivery.Text))
            {
                errors.AppendLine("Укажите адрес доставки");
            }
            else
            {
                if (!inputAddressDelivery.Text.Any(char.IsLetter))
                {
                    errors.AppendLine("Адрес доставки не может содержать только цифры");
                }
                else if (inputAddressDelivery.Text.Length <= 1)
                {
                    errors.AppendLine("Адрес доставки не может содержать только 1 символ, укажите больше");
                }
                else if (inputAddressDelivery.Text.Length > 1)
                {
                    int numChar = 0;
                    for (int i = 0; i < inputAddressDelivery.Text.Length; i++)
                    {
                        if (char.IsLetter(inputAddressDelivery.Text[i]))
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

                    foreach (char symb in inputAddressDelivery.Text)
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
                        errors.AppendLine("Адрес доставки должен содержать минимум 2 буквы подряд");
                    }
                    else if (symbolNonLetter == true)
                    {
                        errors.AppendLine("Адрес не должен содержать английских букв");
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(inputDistance.Text))
            {
                errors.AppendLine("Укажите расстояние доставки, поле не должно быть пустым");
            }
            else if (inputDistance.Text != null)
            {
                bool symbolCheck = false;
                foreach (char symb in inputDistance.Text)
                {
                    if (!Char.IsDigit(symb))
                    {
                        symbolCheck = true;
                        break;
                    }
                }
                if (symbolCheck is true)
                    errors.AppendLine("Расстояние должно содержать только цифры");
                else if (currentOrder.DistanceKm < 1 || currentOrder.DistanceKm > 150)
                    errors.AppendLine("Укажите расстояние доставки от 1 до 150");
            }

            if (currentOrder.DateStart is null)
            {
                errors.AppendLine("Укажите дату начала заказа");
            }
            else if (currentOrder.DateStart > DateTime.Today)
            {
                errors.AppendLine("Дата начала заказа не может быть позже сегодняшней даты");
            }

            if (currentOrder.DateEnd is null && choseStatusOrder.SelectedIndex == 2)
            {
                errors.AppendLine("Дата конца не может не существовать при выполненном статусе");
            }
            else if (currentOrder.DateEnd != null)
            {
                if (currentOrder.DateEnd > DateTime.Today)
                {
                    errors.AppendLine("Дата конца заказа не может быть позже сегодняшней даты");
                }
                else if (currentOrder.DateEnd < currentOrder.DateStart)
                {
                    errors.AppendLine("Дата конца не может быть раньше даты начала");
                }
                else if (choseStatusOrder.SelectedIndex == 0 || choseStatusOrder.SelectedIndex == 1)
                {
                    errors.AppendLine("Дата конца не может существовать при статусе ожидания или выполнения");
                }
            }

            if (choseStatusOrder.SelectedIndex == -1)
                errors.AppendLine("Укажите статус заказа");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return 0;
            }

            string clientPos = "";
            foreach (char symbol in choseClientId.SelectedItem.ToString())
            {
                if (symbol == '.')
                    break;
                clientPos += symbol;
            }
            currentOrder.NumClient = Convert.ToInt32(clientPos);

            string workPos = "";
            foreach (char symbol in choseWorkerId.SelectedItem.ToString())
            {
                if (symbol == '.')
                    break;
                workPos += symbol;
            }
            currentOrder.NumWorker = Convert.ToInt32(workPos);

            foreach (var rate in ChelGruzEntities.GetContext().Rates)
            {
                if (rate.Name == choseRateId.Text)
                {
                    currentOrder.NumRate = rate.Id;
                    break;
                }
            }

            currentOrder.AddressDelivery = inputAddressDelivery.Text;
            currentOrder.DistanceKm = Convert.ToInt32(inputDistance.Text);
            currentOrder.DateStart = Convert.ToDateTime(inputDataStart.SelectedDate);
            if (currentOrder.DateEnd != null)
                currentOrder.DateEnd = Convert.ToDateTime(inputDataEnd.SelectedDate);
            currentOrder.Status = choseStatusOrder.Text;

            return 1;
        }
        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (CheckErrors() == 0)
                return;

            if (currentOrder.Id == 0)
                ChelGruzEntities.GetContext().Orders.Add(currentOrder);

            try
            {
                ChelGruzEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
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

        private void InputDistancePreviewTextInput(object sender, TextCompositionEventArgs e)
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