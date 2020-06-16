using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_WPF
{
    class Drivers
    {
        [Key]
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Region { get; set; } 
        public string City { get; set; }
        public string PESEL { get; set; }
        public DateTime BirthDate { get; set; } //coś nie działa przy wpisywaniu danych, wywala wyjątek
        public virtual ICollection<Cars> Cars { get; set; } = new List<Cars>();
        public virtual ICollection<DrivingLicense> DrivingLicenses { get; set; } = new List<DrivingLicense>();
    }
}
