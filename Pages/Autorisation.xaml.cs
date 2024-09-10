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

namespace GruzChelb
{
    /// <summary>
    /// Страница с элементами заполнения данных пользователя, ведущая при нажатии на основное рабочее окно
    /// </summary>
    public partial class Autorisation : Page
    {
        public static MenuWindow MenuOpen;
        public Autorisation()
        {
            InitializeComponent();

            if (LoginSector.Login != null)
            {
                loginInput.Text = LoginSector.Login;
            }
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            bool loginVerification = false;
            if (String.IsNullOrWhiteSpace(loginInput.Text) || String.IsNullOrWhiteSpace(passwordInput.Password))
            {
                MessageBox.Show("Поля не должны быть пустыми");
                return;
            }
            if (loginInput.Text=="ADMIN" && passwordInput.Password == "ADMIN")
            {
                LoginSector.IdNumber = 0;
                LoginSector.Role = "Владелец";
                LoginSector.Login = "ADMIN";
                LoginSector.Password = "ADMIN";
                loginVerification = true;
            }
            else
            {
                foreach (var worker in ChelGruzEntities.GetContext().Workers)
                {
                    if (loginInput.Text == worker.Login && passwordInput.Password == worker.Password)
                    {
                        LoginSector.IdNumber = worker.Id;
                        LoginSector.Role = worker.Role;
                        LoginSector.Login = worker.Login;
                        LoginSector.Password = worker.Password;
                        loginVerification = true;
                        break;
                    }
                }
            }
            
            if (loginVerification is true)
            {
                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                MenuOpen = new MenuWindow();
                MenuOpen.Show();
                mainWindow.Close();
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль");
            }
        }
    }
}