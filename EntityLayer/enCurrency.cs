using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enCurrency
    {
        public int ID { get; set; }

        public string Base { get; set; }

        public DateTime Date { get; set; }

        public int Year { get { return Date.Year; } }

        public string Month { get { return Date.ToString("MMMM"); } }

        public int Day { get { return Date.Day; } }

        public string TimeStamp { get; set; }

        public string INR { get; set; }

        public string Euro { get; set; }

        //KRW for Korea
        public string KRW { get; set; }

        public string YEN { get; set; }

        public DateTime CreatedDate { get; set; }

        public Rates rates { get; set; }
        public string FromDate { get; set; }

        public string ToDate { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
        public string Createddate { get; set; }
        public string INREURO { get; set; }
        public string INRWON { get; set; }
        public string INRYEN { get; set; }

        public string INREuro { get; set; }
        public string INRKRW { get; set; }
    



    }

    public class Rates
    {
        public double INR { get; set; }
        public double EUR { get; set; }

        public double KRW { get; set; }

        public double JPY { get; set; }

    }
}
