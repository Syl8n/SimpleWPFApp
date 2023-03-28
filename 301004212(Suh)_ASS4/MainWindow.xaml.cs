using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
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
using _301004212_Suh__ASS4.entity;
using _301004212_Suh__ASS4.form;
using _301004212_Suh__ASS4.view;

namespace _301004212_Suh__ASS4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomerContext CustomerContext { get; set; }
        public bool SortedByFirstName { get; set; } = false;
        public bool SortedByCompanyName { get; set; } = false;
        public bool FilteredByCountry { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDB();

            DataContext = this;
        }

        private void InitializeDB()
        {
            CustomerContext = new CustomerContext();
            CustomerContext.Database.EnsureCreated();

            generateData();
        }

        private void refreshListView()
        {
            if(SortedByFirstName)
            {
                SortByFirstName_Click(this, null);
            }
            else if(SortedByCompanyName)
            {
                SortByCompany_Click(this, null);
            }
            else if (FilteredByCountry)
            {
                FromCanada_Click(this, null);
            }
            else
            {
                Generate_Click(this, null);
            }
        }

        private void AddCustomer(CustomerInputForm form)
        {
            updateDb(form);
            CustomerContext.SaveChanges();
            refreshListView();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            InputWindow window = new InputWindow();
            window.AddEvent += new InputWindow.AddEventHandler(AddCustomer);
            window.ShowDialog();
        }

        private void EditCustomer(int id, CustomerInputForm form)
        {
            CustomerAddress customerAddress = CustomerContext.CustomerAddress
                .SingleOrDefault(x => x.CustomerId == id);

            Customer customer = CustomerContext.Customer
                .SingleOrDefault(x => x.CustomerID == customerAddress.CustomerId);
            customer.Title = form.Title;
            customer.FirstName= form.FirstName;
            customer.MiddleName= form.MiddleName;
            customer.LastName= form.LastName;
            customer.CompanyName = form.CompanyName;
            customer.SalesPerson = form.SalesPerson;
            customer.Phone = form.Phone;
            customer.EmailAddress = form.EmailAddress;
            customer.Password= form.Password;

            Address address = CustomerContext.Address
                .SingleOrDefault(x => x.AddressID == customerAddress.AddressId);
            address.AddressLine1 = form.AddressLine1;
            address.AddressLine2 = form.AddressLine2;
            address.City = form.City;
            address.StateProvince = form.StateProvince;
            address.CountryRegion = form.CountryRegion;
            address.PostalCode = form.PostalCode;
            
            CustomerContext.SaveChanges();
            refreshListView();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerList.SelectedItem == null)
            {
                MessageBox.Show("Select a record first");
                return;
            }
            CustomerView view = CustomerList.SelectedItem as CustomerView;
            CustomerInputForm form = CustomerContext.CustomerAddress
                .Join(CustomerContext.Customer,
                ca => ca.CustomerId,
                c => c.CustomerID,
                (ca, c) => new { ca, c })
                .Where(cac => cac.c.CustomerID == view.Id)
                .Join(CustomerContext.Address,
                prev => prev.ca.AddressId,
                addr => addr.AddressID,
                (prev, addr) => new CustomerInputForm
                {
                    Title = prev.c.Title,
                    FirstName = prev.c.FirstName,
                    MiddleName = prev.c.MiddleName,
                    LastName = prev.c.LastName,
                    CompanyName = prev.c.CompanyName,
                    SalesPerson = prev.c.SalesPerson,
                    EmailAddress = prev.c.EmailAddress,
                    Phone = prev.c.Phone,
                    Password= prev.c.Password,
                    AddressLine1 = addr.AddressLine1,
                    AddressLine2 = addr.AddressLine2,
                    City = addr.City,
                    StateProvince = addr.StateProvince,
                    CountryRegion = addr.CountryRegion,
                    PostalCode = addr.PostalCode
                })
                .First();
            InputWindow window = new InputWindow(view.Id, form);
            window.EditEvent += new InputWindow.EditEventHandler(EditCustomer);
            window.ShowDialog();
        }

        private void FindCustomer(int id)
        {
            var records = CustomerContext.CustomerAddress
                .Join(CustomerContext.Customer,
                ca => ca.CustomerId,
                c => c.CustomerID,
                (ca, c) => new { ca, c })
                .Join(CustomerContext.Address,
                prev => prev.ca.AddressId,
                addr => addr.AddressID,
                (prev, addr) => new CustomerView
                {
                    Id = prev.c.CustomerID,
                    Title = prev.c.Title,
                    FirstName = prev.c.FirstName,
                    LastName = prev.c.LastName,
                    CompanyName = prev.c.CompanyName,
                    Address = addr.AddressLine1 + (prev.ca.AddressType.Equals("1") ? ", " + addr.AddressLine2 : ""),
                    Country = addr.CountryRegion
                })
                .Where(cv => cv.Id == id);
            CustomerList.ItemsSource = records.ToList();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            FindWindow window= new FindWindow();
            window.SearchEvent += new FindWindow.SearchEventHandler(FindCustomer);
            window.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(CustomerList.SelectedItem == null)
            {
                MessageBox.Show("Select a record first");
                return;
            }
            CustomerView view = CustomerList.SelectedItem as CustomerView;
            int idx = view.Id;
            Customer customer = CustomerContext.Customer.Find(idx);
            Address address= CustomerContext.Address.Find(idx);
            CustomerContext.Customer.Remove(customer);
            CustomerContext.Address.Remove(address);
            CustomerContext.SaveChanges();
            refreshListView();

            MessageBox.Show("Customer " + customer.LastName + " has been deleted");
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            SortedByFirstName = false;
            SortedByCompanyName = false;
            FilteredByCountry = false;
            var records = CustomerContext.CustomerAddress
                .Join(CustomerContext.Customer,
                ca => ca.CustomerId,
                c => c.CustomerID,
                (ca, c) => new { ca, c })
                .Join(CustomerContext.Address,
                prev => prev.ca.Address.AddressID,
                addr => addr.AddressID,
                (prev, addr) => new CustomerView
                {
                    Id = prev.c.CustomerID,
                    Title = prev.c.Title,
                    FirstName = prev.c.FirstName,
                    LastName = prev.c.LastName,
                    CompanyName = prev.c.CompanyName,
                    Address = addr.AddressLine1 + (prev.ca.AddressType.Equals("1") ? ", " + addr.AddressLine2 : ""),
                    Country = addr.CountryRegion
                });
            CustomerList.ItemsSource = records.ToList();
            CustomerList.Items.SortDescriptions.Clear();
        }

        private void SortByFirstName_Click(object sender, RoutedEventArgs e)
        {
            SortedByFirstName = true;
            SortedByCompanyName = false;

            CustomerList.Items.SortDescriptions.Clear();
            CustomerList.Items.SortDescriptions.Add(
                new SortDescription("FirstName", ListSortDirection.Ascending));
        }

        private void SortByCompany_Click(object sender, RoutedEventArgs e)
        {
            SortedByFirstName = false;
            SortedByCompanyName = true;

            CustomerList.Items.SortDescriptions.Clear();
            CustomerList.Items.SortDescriptions.Add(
                new SortDescription("CompanyName", ListSortDirection.Ascending));
        }

        private bool CountryFilter(object o)
        {
            CustomerView cv = o as CustomerView;
            return cv.Country.Equals("CA");
        }

        private void FromCanada_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(CustomerList.ItemsSource);
            if(collectionView.Filter == null)
            {
                FilteredByCountry = true;
                collectionView.Filter = new Predicate<object>(CountryFilter);
            }
            else
            {
                FilteredByCountry = false;
                collectionView.Filter = null;
            }
        }

        private void generateData()
        {
            if (CustomerContext.Customer.Count() > 0)
            {
                return;
            }

            CustomerInputForm customerInputForm = new CustomerInputForm
            {
                Title = "Mr",
                FirstName = "Jay",
                MiddleName = "Marco",
                LastName = "Smith",
                CompanyName = "Google",
                SalesPerson = "No",
                EmailAddress = "jsmith@gmail.com",
                Phone = "4161234567",
                Password = "1234",
                AddressLine1 = "2383 Yonge St",
                AddressLine2 = "Suite 200",
                City = "Toronto",
                StateProvince = "ON",
                CountryRegion = "CA",
                PostalCode = "M4A3Z1"
            };
            updateDb(customerInputForm);


            customerInputForm = new CustomerInputForm
            {
                Title = "Mrs",
                FirstName = "Eva",
                MiddleName = "Jasmin",
                LastName = "Potter",
                CompanyName = "Microsoft",
                SalesPerson = "No",
                EmailAddress = "esmith@hotmail.com",
                Phone = "4169876543",
                Password = "1234",
                AddressLine1 = "96 W 55th St",
                AddressLine2 = "Suite 100",
                City = "New York",
                StateProvince = "NY",
                CountryRegion = "US",
                PostalCode = "10128"
            };
            updateDb(customerInputForm);


            customerInputForm = new CustomerInputForm
            {
                Title = "Ms",
                FirstName = "Amelia",
                MiddleName = "Teresa",
                LastName = "Sunshine",
                CompanyName = "Walmart",
                SalesPerson = "Yes",
                EmailAddress = "astar@gmail.com",
                Phone = "6473248093",
                Password = "1234",
                AddressLine1 = "1345 Kingsway",
                AddressLine2 = "",
                City = "Vancouver",
                StateProvince = "BC",
                CountryRegion = "CA",
                PostalCode = "V5V3E3"
            };
            updateDb(customerInputForm);


            customerInputForm = new CustomerInputForm
            {
                Title = "Dr",
                FirstName = "Leslie",
                MiddleName = "Tommy",
                LastName = "Wingman",
                CompanyName = "Amazon",
                SalesPerson = "No",
                EmailAddress = "lwingman@gmail.com",
                Phone = "4167908234",
                Password = "1234",
                AddressLine1 = "1160 W 18th St",
                AddressLine2 = "",
                City = "Chicago",
                StateProvince = "IL",
                CountryRegion = "US",
                PostalCode = "60608"
            };
            updateDb(customerInputForm);


            CustomerContext.UserLogin.Add(new UserLogin
            {
                FirstName = "usr1",
                LastName = "usr1",
                Department = "IT",
                PhoneNr = "4163458905",
                UserName = "usr1",
                Password = "1234"
            });


            CustomerContext.UserLogin.Add(new UserLogin
            {
                FirstName = "usr2",
                LastName = "usr2",
                Department = "Finance",
                PhoneNr = "2344094834",
                UserName = "usr2",
                Password = "1234"
            });


            CustomerContext.UserLogin.Add(new UserLogin
            {
                FirstName = "usr3",
                LastName = "usr3",
                Department = "Sales",
                PhoneNr = "4325749793",
                UserName = "usr3",
                Password = "1234"
            });


            CustomerContext.UserLogin.Add(new UserLogin
            {
                FirstName = "usr4",
                LastName = "usr4",
                Department = "Marketing",
                PhoneNr = "6851639484",
                UserName = "usr4",
                Password = "1234"
            });

            CustomerContext.SaveChanges();
        }

        private void updateDb(CustomerInputForm customerInputForm)
        {
            Customer customer = customerInputForm.toCustomer();
            Address address = customerInputForm.toAddress();
            CustomerAddress customerAddress = customerInputForm.toCustomerAddress();
            customerAddress.Customer = customer;
            customerAddress.Address = address;
            customer.CustomerAddresses.Add(customerAddress);
            address.CustomerAddresses.Add(customerAddress);

            CustomerContext.Customer.Add(customer);
            CustomerContext.Address.Add(address);
            CustomerContext.CustomerAddress.Add(customerAddress);
        }
    }
}
