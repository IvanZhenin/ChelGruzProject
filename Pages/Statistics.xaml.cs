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
    /// Страница общей статистики компании: различные данные по заказам, сотрудникам, клиентам, тарифам и товарам
    /// </summary>
    public partial class Statistics : Page
    {
        public Statistics()
        {
            InitializeComponent();

            txtBlockDataStateWorkers.Text += "Текущее количество сотрудников: " 
                + ChelGruzEntities.GetContext().Workers.Count();
            txtBlockDataStateClients.Text += "Текущее количество клиентов: " 
                + ChelGruzEntities.GetContext().Clients.Count();
            txtBlockDataStateRates.Text += "Текущее количество тарифов: " 
                + ChelGruzEntities.GetContext().Rates.Count();
            txtBlockDataStateProds.Text += "Текущее количество товаров: " 
                + ChelGruzEntities.GetContext().Products.Count();

            txtBlockDataOrdersExpect.Text += "Количество заказов в ожидании: " 
                + ChelGruzEntities.GetContext().Orders.Where(p => p.Status == "В ожидании").Count();
            txtBlockDataOrdersPerform.Text += "Количество выполняющихся заказов: "
                + ChelGruzEntities.GetContext().Orders.Where(p => p.Status == "Выполняется").Count();
            txtBlockDataOrdersMade.Text += "Количество выполненных заказов: "
                + ChelGruzEntities.GetContext().Orders.Where(p => p.Status == "Выполнен").Count();
            txtBlockDataOrdersRefused.Text += "Количество отказанных заказов: "
                + ChelGruzEntities.GetContext().Orders.Where(p => p.Status == "Отказан").Count();

            decimal maxAmount = 0;
            foreach(var profMax in ChelGruzEntities.GetContext().Orders)
            {
                if (profMax.TotalAmount > maxAmount && profMax.Status == "Выполнен")
                {
                    maxAmount = (decimal)profMax.TotalAmount;
                }
            }
            txtBlockDataMaximumProfit.Text += "Максимальная прибыль за заказ: " + (int)maxAmount + " руб.";

            decimal monthAmount = 0;
            int currentMonth = DateTime.Today.Month;
            foreach (var profDate in ChelGruzEntities.GetContext().Orders)
            {
                if ((!(profDate.DateEnd is null) && profDate.DateEnd.Value.Month == currentMonth)
                    && profDate.Status == "Выполнен")
                {
                    monthAmount += (decimal)profDate.TotalAmount;
                }
            }
            txtBlockDataProfitMonth.Text += "Прибыль за текущий месяц: " + (int)monthAmount + " руб.";

            decimal potAmount = 0;
            foreach (var profPot in ChelGruzEntities.GetContext().Orders)
            {
                if (profPot.Status == "Выполняется" || profPot.Status == "В ожидании")
                {
                    potAmount += (decimal)profPot.TotalAmount;
                }
            }
            txtBlockDataPotProfit.Text += "Потенциальная прибыль: " + (int)potAmount + " руб.";

            decimal totalAmount = 0;
            foreach (var profTotal in ChelGruzEntities.GetContext().Orders)
            {
                if (profTotal.Status == "Выполнен")
                {
                    totalAmount += (decimal)profTotal.TotalAmount;
                }
            }
            txtBlockDatatTotalProfit.Text += "Итоговая прибыль: " + (int)totalAmount + " руб.";
        }
    }
}