using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_WPF
{
    class Users
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

    }
}
