using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EducationalPractice
{
    internal class Instruments : Window
    {
        // Вывод информационного сообщения
        public object createInformationMessageBox(string message) => MessageBox.Show(message, "Tech Support", MessageBoxButton.OK, MessageBoxImage.Information);


        // Вывод уточняющих сообщений
        public bool createQuestionMessageBox(string message) => (MessageBox.Show(message, "Tech Support", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);


        // Выходит из приложения + чистит КЕШ программы
        public void exitApp() => Environment.Exit(0);

        // Создание текста для ComboBox
        public TextBlock faultText(string text) => new TextBlock()
                                                    {
                                                        Text = text,
                                                        Foreground = new SolidColorBrush(Colors.Black),
                                                        FontSize = 15,
                                                        FontWeight = FontWeights.Bold
                                                    };


        // Возврат даты ГГГГ-ММ-ДД
        public string takeTime() => DateTime.Today.ToString().Split(' ')[0].Split('.')[2] + "-" +
                                    DateTime.Today.ToString().Split(' ')[0].Split('.')[1] + "-" +
                                    DateTime.Today.ToString().Split(' ')[0].Split('.')[0];


        public Dictionary<string, string> userAccountInformation()
        {
            return new DatabaseConnection().returnuserAccountInformation();
        }

        // Открытие аккаунтов пользователей
        public void openUserAccounts(string userRole)
        {
            switch (userRole)
            {
                case ("Оператор"):
                    break;
                case ("Мастер"):
                    break;
                case ("Менеджер"):
                    break;
                case ("Заказчик"):
                    ClientAccWindow clientAccountWindow = new ClientAccWindow()
                    {
                        Top = Top,
                        Left = Left
                    };

                    clientAccountWindow.Show();
                    break;

            }
        }


        public Border createAppBorder(string name)
        {
            Border border = new Border
            {
                Name = "name_" + name,
                Height = 100,
                Width = 400,
                Background = new SolidColorBrush(Colors.MediumPurple),
                BorderBrush = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(1),

                HorizontalAlignment = HorizontalAlignment.Left
            };

            border.MouseEnter += (sender, args) =>
            {
                border.Background = new SolidColorBrush(Colors.DarkCyan);
            };

            border.MouseLeave += (sender, args) =>
            {
                border.Background = new SolidColorBrush(Colors.MediumPurple);
            };


            return border;
        }


        public TextBlock createAppInfTextBlock(string text) => new TextBlock
        {
            Text = text,
            Foreground = new SolidColorBrush(Colors.White),
            FontSize = 16,
            HorizontalAlignment = HorizontalAlignment.Left,
        };
    }
}
