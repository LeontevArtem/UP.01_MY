using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
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
using UP._01.Classes;

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
            LoadData();
            OpenPage(new Pages.Login(this,null));
        }
        public static List<User> UsersList = new List<User>();
        public static List<Classes.Request> RequestsList = new List<Classes.Request>();
        public static List<Equipment> EquipmentsList = new List<Equipment>();
        public static List<TypeOfProblem> ProblemTypes = new List<TypeOfProblem>();
        public static User currentUser;
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
        public void LoadData()
        {
            UsersList.Clear();
            RequestsList.Clear();
            EquipmentsList.Clear();
            ProblemTypes.Clear();
            //Users
            MySqlDataReader UsersQuery = MySQL.Query("Select * from Users");
            while (UsersQuery.Read())
            {
                User new_users = new User();
                new_users.ID = UsersQuery.GetInt32(0);
                new_users.Role = UsersQuery.GetInt32(1);
                new_users.Login = UsersQuery.GetString(2);
                new_users.Password = UsersQuery.GetString(3);
                new_users.Post = UsersQuery.GetString(4);
                new_users.PhoneNumber = UsersQuery.GetString(5);
                new_users.Name = UsersQuery.GetString(6);
                new_users.Surname = UsersQuery.GetString(7);
                new_users.LastName = UsersQuery.GetString(8);
                UsersList.Add(new_users);
            }
            //Equipment
            MySqlDataReader EquipmentQuery = MySQL.Query("Select * from Equipment");
            while (EquipmentQuery.Read())
            {
                Equipment new_equipment = new Equipment();
                new_equipment.ID = EquipmentQuery.GetInt32(0);
                new_equipment.Name = EquipmentQuery.GetString(1);
                EquipmentsList.Add(new_equipment);
            }
            //Types of problems
            MySqlDataReader ProblemsQuery = MySQL.Query("Select * from ProblemType");
            while (ProblemsQuery.Read())
            {
                TypeOfProblem new_problem = new TypeOfProblem();
                new_problem.ID = ProblemsQuery.GetInt32(0);
                new_problem.Name = ProblemsQuery.GetString(1);
                ProblemTypes.Add(new_problem);
            }
            //Requests
            MySqlDataReader db = MySQL.Query("Select * from Requests");
            while (db.Read())
            {
                Classes.Request new_items = new Classes.Request();
                new_items.ID = db.GetInt32(0);
                new_items.StartDate = DateTime.Parse(db.GetString(1));
                try
                {
                    new_items.EndDate = DateTime.Parse(db.GetString(2));
                }
                catch { }
                new_items.Equipment = EquipmentsList.Find(x => x.ID == db.GetInt32(3));
                new_items.TypeOfProblem = ProblemTypes.Find(x => x.ID == db.GetInt32(4));
                new_items.Description = db.GetString(5);
                new_items.Client = UsersList.Find(x => x.ID == db.GetInt32(6));
                try
                {
                    new_items.Performer = UsersList.Find(x => x.ID == db.GetInt32(7));
                }
                catch { }
                try
                {
                    new_items.Manager = UsersList.Find(x => x.ID == db.GetInt32(8));
                }
                catch { }
                new_items.Status = db.GetInt32(9);
                try
                {
                    new_items.Comment = db.GetString(10);
                }
                catch { }
                
                RequestsList.Add(new_items);
            }
        }
    }
}
