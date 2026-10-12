using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChemAnalyst.DAL
{
    public class CustomerDataStore
    {

        private ChemAnalystContext _context = new ChemAnalystContext();
        public SA_Customer CheckCustomer(SA_Customer Login)
        {
            return _context.SA_Customers.Where(x => x.Email == Login.Email && x.UserPassword == Login.UserPassword).SingleOrDefault();
        }

        public bool CheckCustomerPackage(SA_Customer Login)
        {
            var query = from sales in _context.SalesPackageSubscription
                        join sub in _context.SubscriptionType on sales.SubscriptionType equals sub.id
                        //join mod in _context.Module on sales.MenuId equals mod.id
                        join lead in _context.Lead_Master on sales.LeadId equals lead.Id
                        where lead.CorpEmail.Equals(Login.Email)
                        select new SubscriptionViewModel
                        {
                            SId = sales.Id,
                            Status = sales.Status,
                            Name = lead.Name,
                            Organisation = lead.Organisation,
                            Phone = lead.Phone,
                            CorpEmail = lead.CorpEmail,
                            Designation = lead.Designation,
                            Subscribe = sales.MenuId,
                            ExpiryDate = sales.ToDate,
                            Package = sub.Name,
                            LeadId = lead.Id,

                        };

           
            if (query.Count() > 0)
            {
             var data = query.ToList().Where(x => x.ExpiryDate.Value >= DateTime.Now).FirstOrDefault();

                if (data != null)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }
        public int CheckCustomerCount(string Email)
        {
            return _context.SA_Customers.Where(x => x.Email == Email).Count();
        }
        public List<SA_Customer> GetCustomerList()
        {
            return _context.SA_Customers.Where(id => id.Status == true).ToList();
        }

        public async Task<bool> AddCustomer(SA_Customer User)
        {
            //  User.CreatedDate = DateTime.Now;
            _context.SA_Customers.Add(User);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateCustomer(SA_Customer User)
        {

            //  Category.CreatedDate = DateTime.Now;
            SA_Customer Objuser = _context.SA_Customers.Where(user => user.id == User.id).FirstOrDefault();
            Objuser.Fname = User.Fname;
            Objuser.Lname = User.Lname;
            Objuser.Phone = User.Phone;
            Objuser.Role = User.Role;
            Objuser.Email = User.Email;
            Objuser.Gender = User.Gender;
            Objuser.UserPassword = User.UserPassword;
            if (Objuser.ProfileImage != null)
                Objuser.ProfileImage = User.ProfileImage;
            _context.Entry(Objuser).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateCustomerStatus(int customerId)
        {

            //  Category.CreatedDate = DateTime.Now;
            SA_Customer Objuser = _context.SA_Customers.Where(user => user.id == customerId).FirstOrDefault();
            if (Objuser.Status)
                Objuser.Status = false;
            else
                Objuser.Status = true;


            _context.Entry(Objuser).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        internal int UpdatePassword(string newPassword)
        {
            throw new NotImplementedException();
        }

        internal bool DeleteCustomer(int id)
        {
            SA_Customer User = _context.SA_Customers.Where(user => user.id == id).FirstOrDefault();
            _context.Entry(User).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        internal SA_Role UpdateCustomer(string id)
        {
            throw new NotImplementedException();
        }


        internal SA_Customer GetCustomerByid(int id)
        {
            return _context.SA_Customers.Where(user => user.id == id).FirstOrDefault();
        }

        internal SA_Customer GetCustomerByEmail(string email)
        {
            return _context.SA_Customers.Where(user => user.Email == email).FirstOrDefault();
        }
        internal int UpdatePassword(SA_Customer ObjPassChange)
        {
            SA_Customer UpdatedPass = _context.SA_Customers.Where(user => user.id == ObjPassChange.id).FirstOrDefault();
            UpdatedPass.UserPassword = ObjPassChange.UserPassword;
            UpdatedPass.IsInitialPasswordChanged = true;
            _context.Entry(UpdatedPass).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x;
        }

        public bool AddLoginDetail(int UserId, out int loginId)
        {
            loginId = 0;
            var LoginDetail = new SA_Customer_LoginDetail();
            LoginDetail.CustomerId = UserId;
            LoginDetail.Login = DateTime.Now;
            LoginDetail.CreatedTime = DateTime.Now;
            _context.SA_Customer_LoginDetails.Add(LoginDetail);
            int x = _context.SaveChanges();
            loginId = _context.SA_Customer_LoginDetails.Max(l => l.Id);
            return x == 0 ? false : true;
        }

        public bool AddLogoutDetail(int loginId)
        {
            var loginDetail = _context.SA_Customer_LoginDetails.FirstOrDefault(l => l.Id == loginId);
            if (loginDetail != null)
            {
                loginDetail.Logout = DateTime.Now;
            }
            _context.Entry(loginDetail).State = System.Data.Entity.EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true; ;
        }

        public List<CustomerLoginViewModel> GetCustoemrLoginDetails()
        {
            return (from 
                    cld in _context.SA_Customer_LoginDetails
                    join 
                    c in _context.SA_Customers 
                    on cld.CustomerId equals c.id
                    orderby cld.CreatedTime descending
                    select new CustomerLoginViewModel()
                    {
                        Name = c.Fname + " " + c.Lname,
                        LoginTime = cld.Login,
                        LogoutTime = cld.Logout
                    }).ToList();
        }
    }
}

