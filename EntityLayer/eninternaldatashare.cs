using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class eninternaldatashare
    {
    }
    public class eninternaldatashareProdut
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class enMonthly
    {
        public int? Product { get; set; }
        public string ProductVariant { get; set; }
        public string Year { get; set; }

        public string Month { get; set; }
        public string ProductName { get; set; }
        
        public decimal? Count { get; set; }
        public string Country { get; set; }

        public string Region { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? MinUSD { get; set; }
        public decimal? MaxUSD { get; set; }
        public float? EURO { get; set; }
        public float? YEN { get; set; }
        public float? WON { get; set; }
        public decimal? MinYEN { get; set; }
        public decimal? MinWON { get; set; }
        public decimal? MinEURO { get; set; }
        public decimal? MaxYEN { get; set; }
        public decimal? MaxWON { get; set; }
        public decimal? MaxEURO { get; set; }
        public float? USD { get; set; }
    }
    public class enweekly
    {
        public string ProductName { get; set; }
        public int? Product { get; set; }
        public string ProductVariant { get; set; }
        public string Year { get; set; }

        public string Week { get; set; }

        public string Date { get; set; }

        public decimal? Count { get; set; }
        public string Country { get; set; }

        public string Region { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? MinUSD { get; set; }
        public decimal? MaxUSD { get; set; }
        public float? EURO { get; set; }
        public float? YEN { get; set; }
        public float? WON { get; set; }
        public decimal? MinYEN { get; set; }
        public decimal? MinWON { get; set; }
        public decimal? MinEURO { get; set; }
        public decimal? MaxYEN { get; set; }
        public decimal? MaxWON { get; set; }
        public decimal? MaxEURO { get; set; }
        public float? USD { get; set; }
    }
}
