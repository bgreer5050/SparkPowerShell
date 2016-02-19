using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ObservableTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Person> person;

        public MainWindow()
        {
            InitializeComponent();
            SolidColorBrush brush = new SolidColorBrush { Color = Color.FromRgb(100, 100, 100) };
            SolidColorBrush brush2 = new SolidColorBrush { Color = Color.FromRgb(200, 200, 200) };


            person = new ObservableCollection<Person>()
            {


                new Person() {Name="Bill", Address="USA", Brush=brush },
                new Person() {Name="John", Address="MEXICO", Brush=brush2 },

            };
            lstNames.ItemsSource = person;
        }

        private void btnNames_Click(object sender, RoutedEventArgs e)
        {
            person.Add(new Person() { Name = txtName.Text, Address = txtAddress.Text });

            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
    }
}
