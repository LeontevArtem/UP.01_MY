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
using UP._01.Classes;

namespace UP._01.UIelements
{
    /// <summary>
    /// Логика взаимодействия для CommentItem.xaml
    /// </summary>
    public partial class CommentItem : UserControl
    {
        RequestHistory RequestHistory;
        public CommentItem(RequestHistory requestHistory)
        {
            InitializeComponent();
            this.RequestHistory = requestHistory;
            ID.Content += $"{requestHistory.ID.ToString()} от {requestHistory.Date.ToString()}";
            Request.Content += requestHistory.Request.ID.ToString();
            if (requestHistory.Performer != null) Performer.Content += requestHistory.Performer.Name;
            Comment.Text = requestHistory.Comment.Replace("CHAR(10)","\n");
            Status.Content += requestHistory.Status;
        }
    }
}
