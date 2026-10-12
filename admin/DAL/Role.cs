using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChemAnalyst.DAL
{

    public class RoleDataStore
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_RoleViewModel> GetRoleList()
        {
            //return  _context.SA_Role.ToList();
            return _context.SA_Role.ToList().Select(item => new SA_RoleViewModel
            {
                id = item.id,
                Role = item.Role,
                RoleDiscription = item.RoleDiscription,
                CreatedTime = item.CreatedTime != null ? item.CreatedTime.Value.ToString("dd/MM/yyy") : DateTime.Now.ToString("dd/MM/yyyy")
            }
        ).ToList();

        }

        public bool EditRole(SA_RoleViewModel RoleViewModel)
        {
            //  Role.CreatedDate = DateTime.Now;
            SA_Role Role = _context.SA_Role.Where(role => role.id == RoleViewModel.id).FirstOrDefault();
            Role.RoleDiscription = RoleViewModel.RoleDiscription;
            Role.ForwardingEmail = RoleViewModel.ForwardingEmail;
            Role.Role = RoleViewModel.Role;
            _context.Entry(Role).State = EntityState.Modified;

            int x = _context.SaveChanges();
            List<SA_RoleWiseAccess> Editrole = _context.SA_RoleWiseAccess.Where(role => role.RoleId == RoleViewModel.id).ToList();
            foreach (var item in Editrole)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            if (RoleViewModel.ChemicalPricing == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 1;
                access.access = true;
                access.PageDiscription = "Chemical Pricing";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();
            }
            if (RoleViewModel.MarketAnalysis == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 2;
                access.access = true;
                access.PageDiscription = "Market Analysis";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();
            }
            if (RoleViewModel.CompanyProfile == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 3;
                access.access = true;
                access.PageDiscription = "Company Profile";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();
            }
            if (RoleViewModel.IndustryReports == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 4;
                access.access = true;
                access.PageDiscription = "Industry Reports";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();
            }
            if (RoleViewModel.News == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 5;
                access.access = true;
                access.PageDiscription = "News";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();
            }
            if (RoleViewModel.Deals == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 6;
                access.access = true;
                access.PageDiscription = "Deals";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();
            }
            if (RoleViewModel.SubscriptionManagement == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 7;
                access.access = true;
                access.PageDiscription = "Subscription Management";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            //new Added Ashish
            if (RoleViewModel.SectoralReport == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 8;
                access.access = true;
                access.PageDiscription = "Sectoral Report";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.CommentaryManagement == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 9;
                access.access = true;
                access.PageDiscription = "Commentary Management";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.DisruptionTracker == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 10;
                access.access = true;
                access.PageDiscription = "Disruption Tracker";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.AssociateApproval == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 11;
                access.access = true;
                access.PageDiscription = "Associate Approval";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ConsultantApproval == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 12;
                access.access = true;
                access.PageDiscription = "Consultant Approval";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.PricingOverview == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 13;
                access.access = true;
                access.PageDiscription = "Pricing Overview";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.TicketManagement == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 14;
                access.access = true;
                access.PageDiscription = "Ticket Management";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ReportTool == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 15;
                access.access = true;
                access.PageDiscription = "Report Tool";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.CustomerManagement == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 16;
                access.access = true;
                access.PageDiscription = "Customer Management";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.InternalCommentaryAssociate == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 17;
                access.access = true;
                access.PageDiscription = "Internal Commentary Associate";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.InternalCommentaryConsulatant == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 18;
                access.access = true;
                access.PageDiscription = "Internal Commentary Consulatant";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ExternalCommentaryAssociate == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 19;
                access.access = true;
                access.PageDiscription = "External Commentary Associate";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.SalesLeadManagement == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 20;
                access.access = true;
                access.PageDiscription = "Sales Lead Management";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.OperationLeadApproval == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 21;
                access.access = true;
                access.PageDiscription = "Operation Lead Approval";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.AccountsLeadApproval == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 22;
                access.access = true;
                access.PageDiscription = "Accounts Lead Approval";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.SalesManagerLeadApproval == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 23;
                access.access = true;
                access.PageDiscription = "Sales Manager Lead Approval";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.CRMDashboard == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 24;
                access.access = true;
                access.PageDiscription = "CRM Dash board";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.AssociateDisruptionTracker == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 25;
                access.access = true;
                access.PageDiscription = "Associate Disruption Tracker";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ConsultantDisruptionTracker == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 26;
                access.access = true;
                access.PageDiscription = "Consultant Disruption Tracker";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.AssociateMarketAnalysis == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 27;
                access.access = true;
                access.PageDiscription = "Associate Market Analysis";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ConsultantMarketAnalysis == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 28;
                access.access = true;
                access.PageDiscription = "Consultant Market Analysis";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ChemicalPricingDemo == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 29;
                access.access = true;
                access.PageDiscription = "Chemical Pricing Demo";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.MarketAnalysisDemo == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 30;
                access.access = true;
                access.PageDiscription = "Market Analysis Demo";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ManagerPricingandCommentary == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 31;
                access.access = true;
                access.PageDiscription = "Manager Pricing and Commentary";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.QCPricing == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 32;
                access.access = true;
                access.PageDiscription = "QC Pricing";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.QCCommentary == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 33;
                access.access = true;
                access.PageDiscription = "QC Commentary";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.FreelPriceAssociate == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 34;
                access.access = true;
                access.PageDiscription = "Freel Price Associate";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
          
            if (RoleViewModel.ConsultantSales == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 35;
                access.access = true;
                access.PageDiscription = "Consultant Sales";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.FreightIndex == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 36;
                access.access = true;
                access.PageDiscription = "Freight Index";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.AssociatePreSales == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 37;
                access.access = true;
                access.PageDiscription = "Associate Pre Sales";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }
            if (RoleViewModel.ConsultantPreSales == true)
            {
                SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                access.RoleId = Role.id;
                access.Pageid = 38;
                access.access = true;
                access.PageDiscription = "Consultant Pre Sales";
                access.CreatedTime = DateTime.Now;
                _context.SA_RoleWiseAccess.Add(access);
                _context.SaveChanges();

            }


            return x == 0 ? false : true;
        }
        public bool DeleteRole(int RoleId)
        {
            //  Role.CreatedDate = DateTime.Now;
            SA_Role Editrole = _context.SA_Role.Where(role => role.id == RoleId).FirstOrDefault();
            _context.Entry(Editrole).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            List<SA_RoleWiseAccess> deleteroleaceess = _context.SA_RoleWiseAccess.Where(role => role.RoleId == RoleId).ToList();
            foreach (var item in deleteroleaceess)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return x == 0 ? false : true;
        }
        public async Task<bool> AddRole(SA_RoleViewModel RoleViewModel)
        {
            try
            {
                int x = 0;
                SA_Role Role = new Models.SA_Role();
                Role.Role = RoleViewModel.Role;
                Role.ForwardingEmail = RoleViewModel.ForwardingEmail;
                Role.CreatedTime = DateTime.Now;
                Role.RoleDiscription = RoleViewModel.RoleDiscription;
                _context.SA_Role.Add(Role);
                _context.SaveChanges();



                if (RoleViewModel.ChemicalPricing == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 1;
                    access.access = true;
                    access.PageDiscription = "Chemical Pricing";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                }
                if (RoleViewModel.MarketAnalysis == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 2;
                    access.access = true;
                    access.PageDiscription = "Market Analysis";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                }
                if (RoleViewModel.CompanyProfile == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 3;
                    access.access = true;
                    access.PageDiscription = "Company Profile";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                }
                if (RoleViewModel.IndustryReports == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 4;
                    access.access = true;
                    access.PageDiscription = "Industry Reports";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                }
                if (RoleViewModel.News == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 5;
                    access.access = true;
                    access.PageDiscription = "News";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                }
                if (RoleViewModel.Deals == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 6;
                    access.access = true;
                    access.PageDiscription = "Deals";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                }
                if (RoleViewModel.SubscriptionManagement == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 7;
                    access.access = true;
                    access.PageDiscription = "Subscription Management";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                //new Added Ashish
                if (RoleViewModel.SectoralReport == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 8;
                    access.access = true;
                    access.PageDiscription = "Sectoral Report";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.CommentaryManagement == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 9;
                    access.access = true;
                    access.PageDiscription = "Commentary Management";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.DisruptionTracker == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 10;
                    access.access = true;
                    access.PageDiscription = "Disruption Tracker";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.AssociateApproval == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 11;
                    access.access = true;
                    access.PageDiscription = "Associate Approval";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ConsultantApproval == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 12;
                    access.access = true;
                    access.PageDiscription = "Consultant Approval";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                    

                }
                if (RoleViewModel.PricingOverview == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 13;
                    access.access = true;
                    access.PageDiscription = "Pricing Overview";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();
                   

                }
                if (RoleViewModel.TicketManagement == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 14;
                    access.access = true;
                    access.PageDiscription = "Ticket Management";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ReportTool == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 15;
                    access.access = true;
                    access.PageDiscription = "Report Tool";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.CustomerManagement == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 16;
                    access.access = true;
                    access.PageDiscription = "Customer Management";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    x = await _context.SaveChangesAsync();

                }
                if (RoleViewModel.InternalCommentaryAssociate == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 17;
                    access.access = true;
                    access.PageDiscription = "Internal Commentary Associate";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    x = await _context.SaveChangesAsync();

                }
                if (RoleViewModel.InternalCommentaryConsulatant == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 18;
                    access.access = true;
                    access.PageDiscription = "Internal Commentary Consulatant";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    x = await _context.SaveChangesAsync();

                }
                if (RoleViewModel.ExternalCommentaryAssociate == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 19;
                    access.access = true;
                    access.PageDiscription = "External Commentary Associate";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    x = await _context.SaveChangesAsync();

                }
                if (RoleViewModel.SalesLeadManagement == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 20;
                    access.access = true;
                    access.PageDiscription = "Sales Lead Management";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.OperationLeadApproval == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 21;
                    access.access = true;
                    access.PageDiscription = "Operation Lead Approval";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.AccountsLeadApproval == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 22;
                    access.access = true;
                    access.PageDiscription = "Accounts Lead Approval";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.SalesManagerLeadApproval == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 23;
                    access.access = true;
                    access.PageDiscription = "Sales Manager Lead Approval";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.CRMDashboard == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 24;
                    access.access = true;
                    access.PageDiscription = "CRM Dash board";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.AssociateDisruptionTracker == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 25;
                    access.access = true;
                    access.PageDiscription = "Associate Disruption Tracker";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ConsultantDisruptionTracker == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 26;
                    access.access = true;
                    access.PageDiscription = "Consultant Disruption Tracker";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.AssociateMarketAnalysis == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 27;
                    access.access = true;
                    access.PageDiscription = "Associate Market Analysis";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ConsultantMarketAnalysis == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 28;
                    access.access = true;
                    access.PageDiscription = "Consultant Market Analysis";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ChemicalPricingDemo == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 29;
                    access.access = true;
                    access.PageDiscription = "Chemical Pricing Demo";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.MarketAnalysisDemo == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 30;
                    access.access = true;
                    access.PageDiscription = "Market Analysis Demo";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ManagerPricingandCommentary == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 31;
                    access.access = true;
                    access.PageDiscription = "Manager Pricing and Commentary";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.QCPricing == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 32;
                    access.access = true;
                    access.PageDiscription = "QC Pricing";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.QCCommentary == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 33;
                    access.access = true;
                    access.PageDiscription = "QC Commentary";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.FreelPriceAssociate == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 34;
                    access.access = true;
                    access.PageDiscription = "Freel Price Associate";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                
               
                if (RoleViewModel.ConsultantSales == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 35;
                    access.access = true;
                    access.PageDiscription = "Consultant Sales";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.FreightIndex == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 36;
                    access.access = true;
                    access.PageDiscription = "Freight Index";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.AssociatePreSales == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 37;
                    access.access = true;
                    access.PageDiscription = "Associate Pre Sales";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }
                if (RoleViewModel.ConsultantPreSales == true)
                {
                    SA_RoleWiseAccess access = new Models.SA_RoleWiseAccess();
                    access.RoleId = Role.id;
                    access.Pageid = 36;
                    access.access = true;
                    access.PageDiscription = "Consultant Pre Sales";
                    access.CreatedTime = DateTime.Now;
                    _context.SA_RoleWiseAccess.Add(access);
                    _context.SaveChanges();

                }

                return x == 0 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> UpdateRole(SA_Role Role)
        {
            _context.Entry(Role).State = EntityState.Modified;
            //  Role.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        internal SA_RoleViewModel GetRoleByid(int id)
        {
            SA_RoleViewModel model = new Models.SA_RoleViewModel();
            SA_Role editrole = _context.SA_Role.Where(x => x.id == id).SingleOrDefault();
            model.id = editrole.id;
            model.CreatedTime = editrole.CreatedTime!=null? editrole.CreatedTime.Value.ToString("dd/MM/yyyy"):DateTime.Now.ToString("dd/MM/yyyy");
            model.Role = editrole.Role;
            model.RoleDiscription = editrole.RoleDiscription;
            model.ForwardingEmail = editrole.ForwardingEmail;
            var editaccess = _context.SA_RoleWiseAccess.Where(x => x.RoleId == id).ToList();
            foreach (var item in editaccess)
            {
                if (item.Pageid == 1)
                {
                    model.ChemicalPricing = true;
                }
                if (item.Pageid == 2)
                {
                    model.MarketAnalysis = true;
                }
                if (item.Pageid == 3)
                {
                    model.CompanyProfile = true;
                }
                if (item.Pageid == 4)
                {
                    model.IndustryReports = true;
                }
                if (item.Pageid == 5)
                {
                    model.News = true;
                }
                if (item.Pageid == 6)
                {
                    model.Deals = true;
                }
                if (item.Pageid == 7)
                {
                    model.SubscriptionManagement = true;
                }

                //New Added AShish
                if (item.Pageid == 8)
                {
                    model.SectoralReport = true;
                }
                if (item.Pageid == 9)
                {
                    model.CommentaryManagement = true;
                }
                if (item.Pageid == 10)
                {
                    model.DisruptionTracker = true;
                }
                if (item.Pageid == 11)
                {
                    model.AssociateApproval = true;
                }
                if (item.Pageid == 12)
                {
                    model.ConsultantApproval = true;
                }
                if (item.Pageid == 13)
                {
                    model.PricingOverview = true;
                }
                if (item.Pageid == 14)
                {
                    model.TicketManagement = true;
                }
                if (item.Pageid == 15)
                {
                    model.ReportTool = true;
                }
                if (item.Pageid == 16)
                {
                    model.CustomerManagement = true;
                }
                if (item.Pageid == 17)
                {
                    model.InternalCommentaryAssociate = true;
                }
                if (item.Pageid == 18)
                {
                    model.InternalCommentaryConsulatant = true;
                }
                if (item.Pageid == 19)
                {
                    model.ExternalCommentaryAssociate = true;
                }
                if (item.Pageid == 20)
                {
                    model.SalesLeadManagement = true;
                }
                if (item.Pageid == 21)
                {
                    model.OperationLeadApproval = true;
                }
                if (item.Pageid == 22)
                {
                    model.AccountsLeadApproval = true;
                }
                if (item.Pageid == 23)
                {
                    model.SalesManagerLeadApproval = true;
                }
                if (item.Pageid == 24)
                {
                    model.CRMDashboard = true;
                }
                if (item.Pageid == 25)
                {
                    model.AssociateDisruptionTracker = true;
                }
                if (item.Pageid == 26)
                {
                    model.ConsultantDisruptionTracker = true;
                }
                if (item.Pageid == 27)
                {
                    model.AssociateMarketAnalysis = true;
                }
                if (item.Pageid == 28)
                {
                    model.ConsultantMarketAnalysis = true;
                }
                if (item.Pageid == 29)
                {
                    model.ChemicalPricingDemo = true;
                }
                if (item.Pageid == 30)
                {
                    model.MarketAnalysisDemo = true;
                }
                if (item.Pageid == 31)
                {
                    model.ManagerPricingandCommentary = true;
                }
                if (item.Pageid == 32)
                {
                    model.QCPricing = true;
                }
                if (item.Pageid == 33)
                {
                    model.QCCommentary = true;
                }
                if (item.Pageid == 34)
                {
                    model.FreelPriceAssociate = true;
                }
               
                if (item.Pageid == 35)
                {
                    model.ConsultantSales = true;
                }
                if (item.Pageid == 36)
                {
                    model.FreightIndex = true;
                }



            }
            return model;

        }
    }
}