using GruzChelb.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GruzChelb
{
    /// <summary>
    /// Сектор, отвечающий за позиционирование страниц в окнах приложения
    /// </summary>
    public class FrameSector
    {
        public static Frame MainFrame { get; set; }
        public static Frame MenuFrame { get; set; }
        public static Frame ProdListFrame { get; set; }
    }
}