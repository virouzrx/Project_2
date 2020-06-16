using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_WPF
{
    class CarDocsAndInfo
    {
        [Key]
        public string VinNumber { get; set; }
        public string CarCardNumber { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public int CarId { get; set; }
        public virtual Cars Car { get; set; }
    }
}
