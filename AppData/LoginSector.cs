using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruzChelb.AppData
{
    /// <summary>
    /// Сектор, отвечающий за фиксацию текущего пользователя, прошедшего авторизацию
    /// </summary>
    public class LoginSector
    {
        public static int IdNumber { get; set; }
        public static string Role { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
    }
}