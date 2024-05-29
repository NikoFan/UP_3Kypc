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
    /// Логика взаимодействия для NewApplicationWindow.xaml
    /// </summary>
    public partial class NewApplicationWindow : Window
    {
        public NewApplicationWindow()
        {
            InitializeComponent();


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

        private void RegRepair(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> ApplicationRegInformation = new Dictionary<string, string>()
            {
                {"Дата добавления", ""},
                {"Тип техники", TechTypeInput.Text.ToString()},
                {"Модель техники", TechModelInput.Text.ToString()},
                {"Описание проблемы", ProblemDiscription.Text.ToString()},
                {"Статус", "Не выполнено" },
            };
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
