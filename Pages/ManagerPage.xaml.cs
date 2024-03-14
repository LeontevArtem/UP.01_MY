using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UP._01.Classes;

namespace UP._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    /// 
    public partial class ManagerPage : Page
    {
        MainWindow mainWindow;
        Page parrentPage;
        Classes.Request curRequest;
        public ManagerPage(MainWindow mainWindow, Page parrentPage, Classes.Request curRequest)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.parrentPage = parrentPage;
            this.curRequest = curRequest;
            EquipmentCB.ItemsSource = MainWindow.EquipmentsList;
            ProblemCB.ItemsSource = MainWindow.ProblemTypes;
            EquipmentCB.SelectedItem = MainWindow.EquipmentsList.Find(x => x.ID == curRequest.Equipment.ID);
            ProblemCB.SelectedItem = MainWindow.ProblemTypes.Find(x => x.ID == curRequest.TypeOfProblem.ID);
            PerformerCB.ItemsSource = MainWindow.UsersList;
            if (curRequest.Performer != null) PerformerCB.SelectedItem = MainWindow.UsersList.Find(x => x.ID == curRequest.Performer.ID);
            if(curRequest.EndDate != null) EndDate.SelectedDate = curRequest.EndDate;
            Description.Text = curRequest.Description;
            StatusCB.SelectedIndex = curRequest.Status;
            Comment.Text = curRequest.Comment;
            HeaderLabel.Content += $" пользователя {curRequest.Client.Name} № {curRequest.ID}";
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
            int Status = StatusCB.SelectedIndex;
            string EndDatestr = EndDate.SelectedDate.ToString();
            string Commenttext = Comment.Text;
            int PerformerID = (PerformerCB.SelectedItem as User).ID;
            try
            {
                MySQL.Query($"UPDATE `UP.01`.`Requests` SET `EndDate` = '{EndDatestr}', `Equipment` = '{EquipmentID}', `TypeOfProblem` = '{ProblemID}', `Description` = '{Descriptiontext}', `Performer` = '{PerformerID}', `Manager` = '{MainWindow.currentUser.ID}', `Status` = '{Status}', `Comment` = '{Commenttext}' WHERE (`ID` = '{curRequest.ID}');");
                mainWindow.LoadData();
                (parrentPage as Pages.Main).ShowItems();
                mainWindow.OpenPage(parrentPage);
                MessageBox.Show("Успешное изменение");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }
}
