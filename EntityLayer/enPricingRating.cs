using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enPricingRating
    {
        public int AssociateID { get; set; }
        public int Id { get; set; }
        public string AssociateName { get; set; }
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string AssociateEmail { get; set; }
        public string AssociateRole { get; set; }
        public string ValidationMessage { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string AssociateRating { get; set; }
        public string Productivity { get; set; }
        public string CreatedBy { get; set; }
        public string Details { get; set; }
        public string searchKeyword { get; set; }
        public int StartRowNumber { get; set; }
        public int EndRowNumber { get; set; }
        public int PageNumber { get; set; }
        public int UserId { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
        public decimal? ProductQualityQuery { get; set; }
        public string ProductQualityQueryFeedback { get; set; }
        public decimal? Discipline { get; set; }
        public string DisciplineFeedback { get; set; }
        public decimal? ProductAddon { get; set; }
        public string ProductAddonFeedback { get; set; }
        public string TotalRating { get; set; }
        public string AVGRating { get; set; }
        public int TimelineRating { get; set; }
        public int QualityRating { get; set; }
        public string Type { get; set; }
        public string Type2 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Amount { get; set; }
        public string Percent { get; set; }
        public string TotalAmount { get; set; }
        public string Incentive { get; set; }
        public string IncentiveAmount { get; set; }


    }
}
