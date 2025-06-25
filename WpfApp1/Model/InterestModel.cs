using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class InterestModel
    {

        public double Principal { get; set; }
        public double Rate { get; set; }
        public double Time { get; set; }

        public double CalculateInterest()
        {
            return (Principal * Rate * Time) / 100;
        }
        public double CalculateTotal()
        {
            return Principal + CalculateInterest();
        }
    }
}
