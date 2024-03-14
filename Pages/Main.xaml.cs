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
            ShowItems();
        }
        public void ShowItems()
        {
            parrent.Children.Clear();
            foreach (Classes.Request curRequest in MainWindow.RequestsList)
            {
                if (MainWindow.currentUser.Role == 1 || MainWindow.currentUser.Role == 3) parrent.Children.Add(new UIelements.RequestItem(curRequest,mainWindow,this));
            }
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.OpenPage(new Pages.Login(mainWindow,null));
        }
    }
}
