using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruzChelb.AppData
{
    /// <summary>
    /// Сектор, отвечающий за подсчет суммы заказов, в случае отказанного статуса у заказа значение его суммы приравнивается нулю
    /// </summary>
    public class BaseSector
    {
        public static void TotalAmountCheck()
        {
            foreach (var order in ChelGruzEntities.GetContext().Orders)
            {
                foreach (var rate in ChelGruzEntities.GetContext().Rates)
                {
                    if (order.NumRate == rate.Id)
                        order.TotalAmount = (int)order.DistanceKm * rate.PriceKm;
                }
            }

            foreach (var orderTag in ChelGruzEntities.GetContext().Orders)
            {
                foreach (var prodTag in ChelGruzEntities.GetContext().Products)
                {
                    foreach (var posTag in ChelGruzEntities.GetContext().ProdInOrders)
                    {
                        if (orderTag.Id == posTag.NumOrder && prodTag.Id == posTag.NumProd)
                            orderTag.TotalAmount = orderTag.TotalAmount + (posTag.Quantity * prodTag.PriceOne);

                        if (orderTag.Status == "Отказан")
                            orderTag.TotalAmount = 0;
                    }
                }
            }
            ChelGruzEntities.GetContext().SaveChanges();
        }
    }
}