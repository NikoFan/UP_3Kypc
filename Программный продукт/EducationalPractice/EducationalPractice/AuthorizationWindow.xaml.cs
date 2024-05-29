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
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace EducationalPractice
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        // Переход к окну регистрации
        private void GoRegistrationWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow()
            {
                Left = Left,
                Top = Top
            };

            mainWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        // Передвижение окна
        private void DragWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            {
            }

        }

        // Обработка выхода из приложения
        private void GoOut(object sender, RoutedEventArgs e)
        {
            if (new Instruments().createQuestionMessageBox("Вы точно хотите закрыть приложение?"))
                new Instruments().exitApp();
        }

        // Передача данных на авторизации
        private void SignInUser(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(RoleInput.Text.ToString());
            string[] registrationArray = new string[3]
            {
                LoginInput.Text.ToString(),
                PasswordInput.Password.ToString(),
                RoleInput.Text.ToString()
            };
            if (checkData(registrationArray))
            {
                // Если при регистрации данные совпали - регистрация прекращена
                if (!new DatabaseConnection().signInAccount(registrationArray, RoleInput.Text.ToString(), $"ID {RoleInput.Text}а"))
                    new Instruments().createInformationMessageBox("Аккаунт не найден!");
                else
                {
                    new Instruments().createInformationMessageBox($"{RoleInput.Text} авторизован!");
                    openUserAccountWindow(RoleInput.Text.ToString());
                }


            }
            else
                new Instruments().createInformationMessageBox("Авторизация прервана.\nПроверьте информацию!");

        }

        // Проверка данных перед регистрацией
        private bool checkData(string[] inputArray)
        {
            CheckInputData checkInputData = new CheckInputData();
            if (checkInputData.checkSQLI(inputArray[0]) &&
                checkInputData.checkSQLI(inputArray[1]) &&
                inputArray[2].Length != 0)
                return true;
            return false;
        }

        // Открытие окна пользователя
        private void openUserAccountWindow(string userRole)
        {

            if (userRole == "Мастер")
            {
                AuthorizationWindow userAccount = new AuthorizationWindow()
                {
                    Left = Left,
                    Top = Top
                };
                userAccount.Show();
            }
            else if (userRole == "Заказчик")
            {
                ClientAccWindow userAccount = new ClientAccWindow()
                {
                    Left = Left,
                    Top = Top
                };
                userAccount.Show();
            }
            this.Visibility = Visibility.Hidden;
        }
    }
}
