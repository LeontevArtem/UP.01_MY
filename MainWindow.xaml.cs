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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UP._01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void OpenPage(Page ToPage)
        {
            DoubleAnimation opgrid = new DoubleAnimation();
            opgrid.From = 1;
            opgrid.To = 0;
            opgrid.Duration = TimeSpan.FromSeconds(0.1);
            opgrid.Completed += delegate
            {
                frame.Navigate(ToPage);
                DoubleAnimation opgrid2 = new DoubleAnimation();
                opgrid2.From = 0;
                opgrid2.To = 1;
                opgrid2.Duration = TimeSpan.FromSeconds(0.1);
                frame.BeginAnimation(Frame.OpacityProperty, opgrid2);

            };
            frame.BeginAnimation(Frame.OpacityProperty, opgrid);
        }
    }
}
