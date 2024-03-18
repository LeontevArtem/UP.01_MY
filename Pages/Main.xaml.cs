using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UP._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        MainWindow mainWindow;
        Page parrentPage;
        public Main(MainWindow mainWindow, Page parrentPage)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.parrentPage = parrentPage;
            WpfControlLibrary2.Elements.Button1 DateSort = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Сортировать по Дате",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 60, 5, 0)
            };
            DateSort.MouseDown += delegate { DateSortClick(); };
            WpfControlLibrary2.Elements.Button1 EquipSort = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Сортировать по Оборудованию",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            EquipSort.MouseDown += delegate { EquipSortClick(); };
            WpfControlLibrary2.Elements.Button1 ProblemSort = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Сортировать по типу проблемы",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            ProblemSort.MouseDown += delegate { ProblemSortClick(); };
            WpfControlLibrary2.Elements.Button1 UserSort = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Сортировать по Пользователю",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            UserSort.MouseDown += delegate { UserSortClick(); };
            WpfControlLibrary2.Elements.Button1 StatusSort = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Сортировать по Стутусу",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            StatusSort.MouseDown += delegate { StatusSortClick(); };
            WpfControlLibrary2.Elements.TextBox1 SearchBox = new WpfControlLibrary2.Elements.TextBox1()
            {
                XAMLPlaceholder = "Поиск по номеру",
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            WpfControlLibrary2.Elements.Button1 SearchBtn = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Поиск",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            SearchBtn.MouseDown += delegate { SearchClick(SearchBox.GetText()); };
            AdminPanel.AddChildren(DateSort);
            AdminPanel.AddChildren(EquipSort);
            AdminPanel.AddChildren(ProblemSort);
            AdminPanel.AddChildren(UserSort);
            AdminPanel.AddChildren(StatusSort);
            AdminPanel.AddChildren(SearchBox);
            AdminPanel.AddChildren(SearchBtn);
            //if (MainWindow.currentUser.Role != 3) AdminPanel.Visibility = System.Windows.Visibility.Hidden;
            if (MainWindow.currentUser.Role == 1) HeaderLabel.Content += "(Manager)";
            else Statistic.Visibility = Visibility.Hidden;
            ShowItems();
        }
        public void ShowItems()
        {
            parrent.Children.Clear();
            foreach (Classes.Request curRequest in MainWindow.RequestsList)
            {
                if (MainWindow.currentUser.Role != 0) parrent.Children.Add(new UIelements.RequestItem(curRequest, mainWindow, this));
                else
                {
                    if (MainWindow.currentUser.ID == curRequest.Client.ID) parrent.Children.Add(new UIelements.RequestItem(curRequest, mainWindow, this));
                }
            }
        }

        public void SearchClick(string req)
        {
            parrent.Children.Clear();
            List<Classes.Request> SearchList;
            if (req != "") SearchList = MainWindow.RequestsList.Where(x => x.ID.ToString() == req.Trim()).ToList();
            else SearchList = MainWindow.RequestsList;
            foreach (Classes.Request curRequest in SearchList)
            {
                if (MainWindow.currentUser.Role != 0) parrent.Children.Add(new UIelements.RequestItem(curRequest, mainWindow, this));
                else
                {
                    if (MainWindow.currentUser.ID == curRequest.Client.ID) parrent.Children.Add(new UIelements.RequestItem(curRequest, mainWindow, this));
                }
            }
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.OpenPage(new Pages.Login(mainWindow, null));
        }

        private void AddRequest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.OpenPage(new Pages.AddRequest(mainWindow, this, null));
        }
        public void DateSortClick()
        {
            MainWindow.RequestsList = MainWindow.RequestsList.OrderBy(x => x.StartDate).ToList();
            ShowItems();
        }
        public void EquipSortClick()
        {
            MainWindow.RequestsList = MainWindow.RequestsList.OrderBy(x => x.Equipment.ID).ToList();
            ShowItems();
        }
        public void ProblemSortClick()
        {
            MainWindow.RequestsList = MainWindow.RequestsList.OrderBy(x => x.TypeOfProblem.ID).ToList();
            ShowItems();
        }
        public void UserSortClick()
        {
            MainWindow.RequestsList = MainWindow.RequestsList.OrderBy(x => x.Client.ID).ToList();
            ShowItems();
        }
        public void StatusSortClick()
        {
            MainWindow.RequestsList = MainWindow.RequestsList.OrderBy(x => x.Status).ToList();
            ShowItems();
        }

        private void Statistic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int AmountOfRequests = MainWindow.RequestsList.Where(x => x.Status == 2 && x.Manager.ID == MainWindow.currentUser.ID).ToList().Count();
            int AverageTime = 0;
            int Count = 0;
            foreach (Classes.Request curRequest in MainWindow.RequestsList.Where(x => x.Status == 2).ToList())
            {
                AverageTime += (curRequest.EndDate - curRequest.StartDate).Days;
                Count++;
            }
            AverageTime /= Count;
            MessageBox.Show($"Количество выполненных заявок: {AmountOfRequests} \nСреднее время выполнения: {AverageTime} дней");
        }
    }
}
