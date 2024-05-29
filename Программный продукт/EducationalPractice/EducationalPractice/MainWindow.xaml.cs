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

namespace EducationalPractice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Переход к окну авторизации
        private void GoAuthorizationWindow(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow()
            {
                Left = Left,
                Top = Top
            };

            authorizationWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        // Передвижение окна
        private void DragWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DragMove();
            } catch (Exception)
            {
            }
            
        }
        
        // Обработка выхода из приложения
        private void GoOut(object sender, RoutedEventArgs e)
        {
            if (new Instruments().createQuestionMessageBox("Are you sure you want to exit the app?"))
                new Instruments().exitApp();
        }

        // Передача данных на регистрацию
        private void SignUpUser(object sender, RoutedEventArgs e)
        {
            string[] registrationArray = new string[5]
            {
                PhoneInput.Text.ToString(),
                FIOInput.Text.ToString(),
                LoginInput.Text.ToString(),
                PasswordInput.Password.ToString(),
                RoleInput.Text.ToString()
            };
            if (checkData(registrationArray))
            {
                if (!new DatabaseConnection().addNewAccount(registrationArray, RoleInput.Text.ToString()))
                    new Instruments().createInformationMessageBox("Регистрация прервана.\nДанные совпадают с другим аккаунтом!");
                else
                {
                    // Открытие окна аккаунта администратора
                    new Instruments().createInformationMessageBox($"{RoleInput.Text} Зарегистрирован!");
                    openUserAccountWindow(RoleInput.Text.ToString());
                }




            }
            else
                new Instruments().createInformationMessageBox("Регистрация была прервана.\nПроверьте веденную информацию!");

        }

        // Проверка данных перед регистрацией
        private bool checkData(string[] inputArray)
        {
            CheckInputData checkInputData = new CheckInputData();
            if (checkInputData.checkSQLI(inputArray[0]) &&
                checkInputData.controlTelephoneNumbers(inputArray[0]) &&
                checkInputData.checkSQLI(inputArray[1]) &&
                checkInputData.checkSQLI(inputArray[2]) &&
                checkInputData.checkSQLI(inputArray[3]) &&
                
                inputArray[4].Length != 0)
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
