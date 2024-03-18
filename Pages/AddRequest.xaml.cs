using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UP._01.Classes;

namespace UP._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Page
    {
        MainWindow mainWindow;
        Page parrentPage;
        Classes.Request curRequest;
        public AddRequest(MainWindow mainWindow, Page parrentPage, Classes.Request curRequest)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.parrentPage = parrentPage;
            this.curRequest = curRequest;
            EquipmentCB.ItemsSource = MainWindow.EquipmentsList;
            ProblemCB.ItemsSource = MainWindow.ProblemTypes;
            if (curRequest != null)
            {
                EquipmentCB.SelectedItem = MainWindow.EquipmentsList.Find(x => x.ID == curRequest.Equipment.ID);
                ProblemCB.SelectedItem = MainWindow.ProblemTypes.Find(x => x.ID == curRequest.TypeOfProblem.ID);
                Description.Text = curRequest.Description;
                Add.SetText("Сохранить");
            }
            
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.OpenPage(parrentPage);
        }

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int EquipmentID = (EquipmentCB.SelectedItem as Equipment).ID;
            int ProblemID = (ProblemCB.SelectedItem as TypeOfProblem).ID;
            string StartDate = DateTime.Now.ToString();
            string Descriptiontext = Description.Text;
            int ClientID = MainWindow.currentUser.ID;
            int Status = 0;
            string StatusT = "";
            if (curRequest != null)
            {
                switch (curRequest.Status)
                {
                    case 0:
                        {
                            StatusT += "В ожидании";
                            break;
                        }
                    case 1:
                        {
                            StatusT += "В работе";
                            break;
                        }
                    case 2:
                        {
                            StatusT += "Выполнено";
                            break;
                        }
                }
                try
                {
                    MySQL.Query($"UPDATE `UP.01`.`Requests` SET `Equipment` = '{EquipmentID}', `TypeOfProblem` = '{ProblemID}', `Description` = '{Descriptiontext}' WHERE (`ID` = '{curRequest.ID}');");
                    MySQL.Query($"INSERT INTO `UP.01`.`RequestHistory` (`RequestID`,`Comment`,`Status`,`Date`) VALUES ('{curRequest.ID}', 'Заявка изменена пользователем №{curRequest.Client.ID}'.'{StatusT}','{ DateTime.Now.ToString()}');");
                    mainWindow.LoadData();
                    (parrentPage as Pages.Main).ShowItems();
                    mainWindow.OpenPage(parrentPage);
                    MessageBox.Show("Успешное изменение");
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }

            }
            else
            {
                
                try
                {
                    MySqlDataReader db = MySQL.Query($"INSERT INTO `UP.01`.`Requests` (`StartDate`, `Equipment`, `TypeOfProblem`, `Description`, `Client`, `Status`) VALUES ('{StartDate}', '{EquipmentID}', '{ProblemID}', '{Descriptiontext}', '{ClientID}', '{Status}'); SELECT LAST_INSERT_ID()");
                    int ID = 0;
                    while (db.Read()) ID = db.GetInt32("LAST_INSERT_ID()");
                    MySQL.Query($"INSERT INTO `UP.01`.`RequestHistory` (`RequestID`,`Comment`,`Status`,`Date`) VALUES ('{ID}', 'Создана заявка №{ID} Клиентом №{ClientID} CHAR(10)','Создана','{DateTime.Now.ToString()}');");
                    mainWindow.LoadData();
                    (parrentPage as Pages.Main).ShowItems();
                    mainWindow.OpenPage(parrentPage);
                    MessageBox.Show("Успешное добавление");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            
        }
    }
}
