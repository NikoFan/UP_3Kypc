using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducationalPractice
{
    /// <summary>
    /// Логика взаимодействия для NewApplicationWindow.xaml
    /// </summary>
    public partial class NewApplicationWindow : Window
    {
        public NewApplicationWindow()
        {
            InitializeComponent();
            setDate();

        }

        // Установка даты
        private void setDate() => RegDate.Text = new Instruments().takeTime();


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
            if (new Instruments().createQuestionMessageBox("Are you sure you want to exit the app?"))
                new Instruments().exitApp();
        }

        private void GoAccount(object sender, RoutedEventArgs e)
        {
            ClientAccWindow clientAccWindow = new ClientAccWindow()
            {
                Top = Top,
                Left = Left,
            };

            clientAccWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        // Открыть созданные заявки
        private void OpenMyApplications(object sender, RoutedEventArgs e)
        {


            if (new Instruments().createQuestionMessageBox("Процесс создания заявки завершится! Продолжить?"))
            {
                MyApplicationsView myApplicationsView = new MyApplicationsView()
                {
                    Left = Left,
                    Top = Top
                };

                myApplicationsView.Show();
                this.Visibility = Visibility.Hidden;
            }

        }

        // Создание заявки
        private void addNewRepair(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> applicationCreateInfo = new Dictionary<string, string>()
            {
                {"Дата добавления", RegDate.Text.ToString()},
                {"Тип техники", TechTypeInput.Text.ToString()},
                {"Модель техники", TechModelInput.Text.ToString()},
                {"Описание", ProblemDiscription.Text.ToString()},
                {"Статус", "Новая заявка"},
                {"ID Заказчика", new CacheWork().ReturnUserId().ToString()}
            };
            bool checkResult = true;
            foreach (var item in applicationCreateInfo)
            {
                if ((checkResult = checkInputInformation(item.Value)) == false)
                    break;
            }
            // Проверка корректности данных
            if (!checkResult)
            {
                new Instruments().createInformationMessageBox("Ошибка при проверке данных. проверьте чтобы каждое поле было заполнено!");
                return;
            }
            // Регистрация заявки
            if (new DatabaseConnection().RepairReg(applicationCreateInfo))
            {
                new Instruments().createInformationMessageBox("Заявка зарегистрирована!");
                // Возврат на окно аккаунта
                new Instruments().openUserAccounts("Заказчик");
                this.Visibility = Visibility.Hidden;
            }



        }

        // Проверка информации на корректность
        private bool checkInputInformation(string example)
        {
            if (example.Length < 1)
            {
                return false;
            }
            return true;
        }
    }

    
}
