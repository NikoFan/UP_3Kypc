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
    /// Логика взаимодействия для MyApplicationsView.xaml
    /// </summary>
    public partial class MyApplicationsView : Window
    {
        public MyApplicationsView()
        {
            InitializeComponent();
            setApplicationInf();
        }


        private void setApplicationInf()
        {
            Instruments instruments = new Instruments();
            foreach (var ApplicationInf in new DatabaseConnection().returnApplicationInf())
            {
                Console.WriteLine(ApplicationInf);
                // Заполнение Combobox Исполнителей
                SelectApplication.Items.Add(instruments.faultText($"{ApplicationInf.Key} Статус: {ApplicationInf.Value}"));

            }
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

        private void GoAccount(object sender, RoutedEventArgs e)
        {
            ClientAccWindow clientAccountWindow = new ClientAccWindow()
            {
                Left = Left,
                Top = Top
            };

            clientAccountWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void CreateApplication(object sender, RoutedEventArgs e)
        {
            
        }

        // Обработка выхода из приложения
        private void GoOut(object sender, RoutedEventArgs e)
        {
            if (new Instruments().createQuestionMessageBox("Are you sure you want to exit the app?"))
                new Instruments().exitApp();
        }

        // Обработка изменение исполнителя
        private void ChangeContractor(object sender, RoutedEventArgs e)
        {
            if (SelectApplication.Text.ToString().Length != 0)
            {

            }
        }


        // Обработка изменение Описания
        private void ChangeDescription(object sender, RoutedEventArgs e)
        {
            if (SelectApplication.Text.ToString().Length != 0)
            {

            }
        }
    }
}
