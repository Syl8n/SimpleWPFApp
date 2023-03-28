using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _301004212_Suh__ASS4.entity;

namespace _301004212_Suh__ASS4.form
{
    public class CustomerInputForm
    {
        public string Title { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string SalesPerson { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Password { get; set; } = "";
        public string AddressLine1 { get; set; } = "";
        public string AddressLine2 { get; set; } = "";
        public string City { get; set; } = "";
        public string StateProvince { get; set; } = "";
        public string CountryRegion { get; set; } = "";
        public string PostalCode { get; set; } = "";

        public Customer toCustomer()
        {
            return new Customer
            {
                Title = this.Title,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                CompanyName = this.CompanyName,
                SalesPerson = this.SalesPerson,
                EmailAddress = this.EmailAddress,
                Phone = this.Phone,
                Password = this.Password,
            };
        }

        public Address toAddress()
        {
            return new Address
            {
                AddressLine1 = this.AddressLine1,
                AddressLine2 = this.AddressLine2,
                City = this.City,
                StateProvince = this.StateProvince,
                CountryRegion = this.CountryRegion,
                PostalCode = this.PostalCode,
            };
        }

        public CustomerAddress toCustomerAddress()
        {
            return new CustomerAddress
            {
                AddressType = this.AddressLine2.Equals(string.Empty) ? "2" : "1",
                ModifiedDate = DateTime.Now
            };
        }
    }
}
