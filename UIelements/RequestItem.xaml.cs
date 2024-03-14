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
            Equipment.Content += curRequest.Equipment.Name;
            TypeOfProblem.Content += curRequest.TypeOfProblem.Name;
            Description.Text += curRequest.Description;
            Status.Content += curRequest.Status.ToString();
            StartDate.Content += curRequest.StartDate.ToString();
            if (curRequest.EndDate.ToString() != "01.01.0001 0:00:00") EndDate.Content += curRequest.EndDate.ToString();
            else EndDate.Content += "Не завершена";
            Body.MouseDown += delegate { Border_MouseDown(); };
        }
        bool flag = true;
        private void Border_MouseDown()
        {
            if(flag) mainWindow.OpenPage(new Pages.AddRequest(mainWindow, parrentPage, curRequest));
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;
            try
            {
                if(MessageBox.Show("Вы уверены?","Предупреждение",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MySQL.Query($"DELETE FROM `UP.01`.`Requests` WHERE (`ID` = '{curRequest.ID}');");
                    mainWindow.LoadData();
                    (parrentPage as Pages.Main).ShowItems();
                    mainWindow.OpenPage(parrentPage);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void Delete_MouseEnter(object sender, RoutedEventArgs e)
        {
            flag = false;

        }

        private void Delete_MouseLeave(object sender, RoutedEventArgs e)
        {
            flag = true;

        }
    }
}
