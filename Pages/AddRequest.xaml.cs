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
            if (curRequest != null)
            {
                try
                {
                    MySQL.Query($"UPDATE `UP.01`.`Requests` SET `Equipment` = '{EquipmentID}', `TypeOfProblem` = '{ProblemID}', `Description` = '{Descriptiontext}' WHERE (`ID` = '{curRequest.ID}');");
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
                    MySQL.Query($"INSERT INTO `UP.01`.`Requests` (`StartDate`, `Equipment`, `TypeOfProblem`, `Description`, `Client`, `Status`) VALUES ('{StartDate}', '{EquipmentID}', '{ProblemID}', '{Descriptiontext}', '{ClientID}', '{Status}');");
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
