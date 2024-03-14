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
using System.Windows.Shapes;
using UP._01.Classes;

namespace UP._01.Windows
{
    /// <summary>
    /// Логика взаимодействия для Comments.xaml
    /// </summary>
    public partial class Comments : Window
    {
        public Comments(Classes.Request curRequest)
        {
            InitializeComponent();
            foreach (RequestHistory curRequestHistories in MainWindow.RequestHistories.Where(x => x.Request.ID == curRequest.ID))
            {
                parrent.Children.Add(new UIelements.CommentItem(curRequestHistories));
            }
        }
    }
}
