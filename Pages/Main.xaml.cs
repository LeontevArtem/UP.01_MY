using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfControlLibrary2.Elements;

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
            WpfControlLibrary2.Elements.Button1 AddEquipment = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Добавить оборудование",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5,60,5,0)
            };
            WpfControlLibrary2.Elements.Button1 AddProblem = new WpfControlLibrary2.Elements.Button1()
            {
                XAMLText = "Добавить тип проблемы",
                XAMLTextColor = Color.FromRgb(255, 255, 255),
                Margin = new System.Windows.Thickness(5, 5, 5, 0)
            };
            AdminPanel.AddChildren(AddEquipment);
            AdminPanel.AddChildren(AddProblem);
            if (MainWindow.currentUser.Role != 3) AdminPanel.Visibility = System.Windows.Visibility.Hidden;
            if (MainWindow.currentUser.Role == 1) HeaderLabel.Content += "(Manager)";
            ShowItems();
        }
        public void ShowItems()
        {
            parrent.Children.Clear();
            foreach (Classes.Request curRequest in MainWindow.RequestsList)
            {
                if (MainWindow.currentUser.Role == 1 || MainWindow.currentUser.Role == 3) parrent.Children.Add(new UIelements.RequestItem(curRequest, mainWindow, this));
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
            mainWindow.OpenPage(new Pages.AddRequest(mainWindow,this,null));
        }
    }
}
