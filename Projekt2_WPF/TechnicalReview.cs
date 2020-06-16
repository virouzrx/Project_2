using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_WPF
{
    class TechnicalReview
    {
        [Key]
        public int TechnicalReviewID { get; set; }
        public DateTime TechnicalReviewDate { get; set; }
        public DateTime TechnicalReviewExpiryDate { get; set; }
        public bool TechnicalReviewStatus { get; set; }
        public int CarId { get; set; }
        public virtual Cars Car { get; set; }
    }
}
