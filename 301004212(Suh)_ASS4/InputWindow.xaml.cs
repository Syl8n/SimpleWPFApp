using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using _301004212_Suh__ASS4.form;

namespace _301004212_Suh__ASS4
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public int Id { get; set; }
        public bool EditMode { get; set; } = false;
        public CustomerInputForm InputForm { get; set; }
        public List<string> TitleList { get; set; } = new List<string>
        {
            "Mr", "Mrs", "Ms", "Dr"
        };
        public List<string> SalesList { get; set; } = new List<string>
        {
            "Yes", "No"
        };
        public string CurrentCountry { get; set; }
        public List<string> CountryList { get; set; } = new List<string>
        {
            "CA", "US"
        };
        public List<string> ProvinceList { get; set; } = new List<string>
        {
            "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT"
        };
        public List<string> StateList { get; set; } = new List<string>
        {
            "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL"
            , "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE"
            , "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "PR", "RI", "SC"
            , "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY"
        };
        public InputWindow()
        {
            InitializeComponent();
            InputForm = new CustomerInputForm();
            DataContext = this;
        }

        public InputWindow(int id, CustomerInputForm form)
        {
            InitializeComponent();
            Id = id;
            InputForm = form;
            DataContext= this;
            EditMode = true;
        }

        public delegate void AddEventHandler(CustomerInputForm form);
        public event AddEventHandler AddEvent;

        public delegate void EditEventHandler(int id, CustomerInputForm form);
        public event EditEventHandler EditEvent;

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(EditMode)
            {
                EditEvent(Id, InputForm);
            }
            else
            {
                AddEvent(InputForm);
            }
            Window.GetWindow(this).Close();
        }

        private void Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ("CA".Equals(InputForm.CountryRegion))
            {
                StateProvince.ItemsSource = ProvinceList;
            }
            else if ("US".Equals(InputForm.CountryRegion))
            {
                StateProvince.ItemsSource = StateList;
            }
        }
    }
}
