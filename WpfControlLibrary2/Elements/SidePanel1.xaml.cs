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

namespace WpfControlLibrary2.Elements
{
    /// <summary>
    /// Логика взаимодействия для SidePanel1.xaml
    /// </summary>
    public partial class SidePanel1 : UserControl
    {
        public enum PanelOrientation
        {
            left,
            right
        }
        public enum PanelMode
        {
            open,
            close
        }
        public Color BackgroundColor = Color.FromRgb(255, 255, 255);
        public PanelOrientation orientation;
        PanelMode mode = PanelMode.close;
        int ControlPanelCount = 0;
        public SidePanel1(PanelOrientation orientation)
        {
            InitializeComponent();
            if (orientation == PanelOrientation.right)
            {
                menuicon.HorizontalAlignment = HorizontalAlignment.Right;
                ControlPanelBorder.HorizontalAlignment = HorizontalAlignment.Right;
                ControlPanelBorder.Margin = new Thickness(5, 5, -200, 5);
            }
            this.orientation = orientation;
        }
        public SidePanel1()
        {
            InitializeComponent();
            this.orientation = PanelOrientation.left;
        }


        public void SetBackground(Color BackgroundColor)
        {
            SolidColorBrush BackgroundBrush = new SolidColorBrush(BackgroundColor);
            ControlPanelBorder.Background = BackgroundBrush;
            menuicon.Background = BackgroundBrush;
        }
        public void AddChildren(System.Windows.UIElement uIElement)
        {
            Children.Children.Add(uIElement);
        }
        private void menuicon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (mode == PanelMode.close)
            //{
            //    mode = PanelMode.open;
            //    SwitchControlPanel();
            //}
            //else if (mode == PanelMode.open) 
            //{
            //    mode = PanelMode.close;
            //    SwitchControlPanel();
            //}
            SwitchControlPanel();
            //ControlPanelCount++;
        }
        public void SwitchImage(PanelMode mode)
        {
            if (mode == PanelMode.open)
            {
                IconImg.Source = new BitmapImage(new Uri(""));
            }
            else
            {
                IconImg.Source = new BitmapImage(new Uri(""));
            }
        }
        public void SwitchControlPanel()
        {
            ThicknessAnimation OpenAnimationLeft = new ThicknessAnimation();
            OpenAnimationLeft.From = new Thickness(ControlPanelBorder.Margin.Left, ControlPanelBorder.Margin.Top, ControlPanelBorder.Margin.Right, ControlPanelBorder.Margin.Bottom);
            OpenAnimationLeft.To = new Thickness(5, ControlPanelBorder.Margin.Top, ControlPanelBorder.Margin.Right, ControlPanelBorder.Margin.Bottom);
            OpenAnimationLeft.Duration = TimeSpan.FromSeconds(0.3);

            ThicknessAnimation CloseAnimationLeft = new ThicknessAnimation();
            CloseAnimationLeft.From = new Thickness(ControlPanelBorder.Margin.Left, ControlPanelBorder.Margin.Top, ControlPanelBorder.Margin.Right, ControlPanelBorder.Margin.Bottom);
            CloseAnimationLeft.To = new Thickness(-200, ControlPanelBorder.Margin.Top, ControlPanelBorder.Margin.Right, ControlPanelBorder.Margin.Bottom);
            CloseAnimationLeft.Duration = TimeSpan.FromSeconds(0.3);
            //OpenAnimationLeft.BeginAnimation(MarginProperty, OpenAnimationLeft);

            if (mode == PanelMode.open)
            {
                if (orientation == PanelOrientation.right)
                {
                    
                }
                else
                {
                    //OpenAnimationLeft.BeginAnimation();
                    ControlPanelBorder.BeginAnimation(MarginProperty, OpenAnimationLeft);
                    mode = PanelMode.close;
                }
            }
            else if (mode == PanelMode.close)
            {
                if (orientation == PanelOrientation.right)
                {

                }
                else
                {
                    //CloseAnimationLeft.BeginAnimation(MarginProperty, OpenAnimationLeft);
                    ControlPanelBorder.BeginAnimation(MarginProperty, CloseAnimationLeft);
                    mode = PanelMode.open;
                }
            }
        }
    }
}
