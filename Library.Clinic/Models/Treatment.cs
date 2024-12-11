using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Treatment
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        // Insurance-related properties
        public decimal InsuranceDiscountPercentage { get; set; } // Discount in %
        public decimal InsuranceCoverageAmount { get; set; } // Flat coverage
    }
}
