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

namespace EducationalPractice
{
    /// <summary>
    /// Логика взаимодействия для ClientAccWindow.xaml
    /// </summary>
    public partial class ClientAccWindow : Window
    {
        public ClientAccWindow()
        {
            InitializeComponent();
            setUserInformation();
        }

        // Установка информации про аккаунт
        private void setUserInformation()
        {
            string[] inf = new DatabaseConnection().returnUserAccountInformation();
            fio.Text = inf[0];
            phone.Text = inf[0];
            role.Text = inf[0];
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


        private void CreateApplication(object sender, RoutedEventArgs e)
        {

        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (new Instruments().createQuestionMessageBox("Вы точно хотите выйти из аккаунта?"))
            {
                MainWindow mainWindow = new MainWindow()
                {
                    Left = Left,
                    Top = Top
                };

                mainWindow.Show();
                this.Visibility = Visibility.Hidden;
            }
                
        }



        // Обработка выхода из приложения
        private void GoOut(object sender, RoutedEventArgs e)
        {
            if (new Instruments().createQuestionMessageBox("Вы точно хотите закрыть приложение?"))
                new Instruments().exitApp();
        }


        // Открыть созданные заявки
        private void OpenMyApplications(object sender, RoutedEventArgs e)
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
}
