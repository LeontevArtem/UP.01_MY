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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        MainWindow mainWindow;
        Page parrentPage;
        public Login(MainWindow mainWindow, Page parrentPage)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.parrentPage = parrentPage;
        }
        public void LoginClick(object sender, MouseButtonEventArgs e)
        {
            string login = LoginBox.GetText();
            string password = PasswordBox.GetText();
            MainWindow.currentUser = MainWindow.UsersList.Find(x => x.Login == login && x.Password == password);
            if (MainWindow.currentUser != null) mainWindow.OpenPage(new Pages.Main(mainWindow,this));
            else MessageBox.Show("Неверный логин или пароль");
        }

       
    }
}
