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
            Equipment.Content = curRequest.Equipment.Name;
            TypeOfProblem.Content = curRequest.TypeOfProblem.Name;
            Description.Text = curRequest.Description;
            Status.Content = curRequest.Status;
        }
    }
}
