using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301004212_Suh__ASS4.entity
{
    public class UserLogin
    {
        [Key] public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string PhoneNr { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
