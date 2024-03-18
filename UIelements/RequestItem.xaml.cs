using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UP._01.UIelements
{
    /// <summary>
    /// Логика взаимодействия для RequestItem.xaml
    /// </summary>
    public partial class RequestItem : UserControl
    {
        public Classes.Request curRequest;
        MainWindow mainWindow;
        Page parrentPage;
        public RequestItem(Classes.Request curRequest, MainWindow mainWindow, Page parrentPage)
        {
            InitializeComponent();
            this.curRequest = curRequest;
            this.mainWindow = mainWindow;
            this.parrentPage = parrentPage;
            if (MainWindow.currentUser.Role != 2 || curRequest.Status != 0) Accept.Visibility = Visibility.Hidden;
            ID.Content += curRequest.ID.ToString();
            Equipment.Content += curRequest.Equipment.Name;
            TypeOfProblem.Content += curRequest.TypeOfProblem.Name;
            Description.Text += curRequest.Description;
            switch (curRequest.Status)
            {
                case 0:
                    {
                        Status.Content += "В ожидании";
                        break;
                    }
                case 1:
                    {
                        Status.Content += "В работе";
                        break;
                    }
                case 2:
                    {
                        Status.Content += "Выполнено";
                        break;
                    }
            }
            StartDate.Content += curRequest.StartDate.ToString();
            if (curRequest.EndDate.ToString() != "01.01.0001 0:00:00") EndDate.Content += curRequest.EndDate.ToString();
            else EndDate.Content += "Дата не указана";
            Body.MouseDown += delegate { Border_MouseDown(); };
            if (curRequest.Status == 2 && MainWindow.currentUser.Role == 0)
            {
                Data.IsEnabled = false;
                Data.Opacity = 0.5;
                Body.IsEnabled = false;
                QRCodeImg.Source = Classes.QRCode.CreateBitmapCode("https://forms.gle/a3tZf9fCMtEh57Lu7");
                ImgGrid.IsEnabled = true;
                
            }
        }
        bool flag = true;
        private void Border_MouseDown()
        {
            if (flag)
            {
                if (MainWindow.currentUser.Role == 1)
                {
                    mainWindow.OpenPage(new Pages.ManagerPage(mainWindow, parrentPage, curRequest));
                }
                else mainWindow.OpenPage(new Pages.AddRequest(mainWindow, parrentPage, curRequest));
            }
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;
            try
            {
                if (MessageBox.Show("Вы уверены?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MySQL.Query($"DELETE FROM `UP.01`.`Requests` WHERE (`ID` = '{curRequest.ID}');");
                    mainWindow.LoadData();
                    (parrentPage as Pages.Main).ShowItems();
                    mainWindow.OpenPage(parrentPage);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Delete_MouseEnter(object sender, RoutedEventArgs e)
        {
            flag = false;

        }

        private void Delete_MouseLeave(object sender, RoutedEventArgs e)
        {
            flag = true;

        }

        private void Accept_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите принять заявку?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MySQL.Query($"UPDATE `UP.01`.`Requests` SET `Performer` = '{MainWindow.currentUser.ID}', `Status` = '{1}' WHERE (`ID` = '{curRequest.ID}');");
                    mainWindow.LoadData();
                    (parrentPage as Pages.Main).ShowItems();
                    mainWindow.OpenPage(parrentPage);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
