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
    /// Логика взаимодействия для MyApplicationsView.xaml
    /// </summary>
    public partial class MyApplicationsView : Window
    {
        public MyApplicationsView()
        {
            InitializeComponent();
            setApplicationsInf();
        }


        private void setApplicationsInf()
        {
            foreach (var item in new DatabaseConnection().returnApplicationsInfo())
            {
                StackPanel grid = new StackPanel();
                Border border = new Instruments().createAppBorder(item.Key);
                foreach (var info in item.Value)
                {
                    grid.Children.Add(new Instruments().createAppInfTextBlock(info.Value));
                }

                border.Child = grid;
                AppShower.Children.Add(border);
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




    }
}
