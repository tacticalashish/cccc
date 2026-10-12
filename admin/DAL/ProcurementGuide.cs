using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.DAL
{
    public class ProcurementGuideStore
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_ProcurementGuide> GetProcurementGuideListAdmin()
        {
            return _context.SA_ProcurementGuide.ToList();
        }

        public List<SA_ProcurementGuide> GetProcurementGuideList()
        {

            return _context.SA_ProcurementGuide.Where(x => x.status == 1).ToList();

        }

        public bool EditProcurementGuide(SA_ProcurementGuide ProcurementGuide)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_ProcurementGuide Edit = _context.SA_ProcurementGuide.Where(Cat => Cat.id == ProcurementGuide.id).FirstOrDefault();
            Edit.Name = ProcurementGuide.Name;
            Edit.Discription = ProcurementGuide.Discription;
            Edit.URL = ProcurementGuide.URL;
            Edit.MetaDiscription = ProcurementGuide.MetaDiscription;
            Edit.Keywords = ProcurementGuide.Keywords;
            Edit.Author = ProcurementGuide.Author;
            Edit.CategoryID = ProcurementGuide.CategoryID;
            Edit.CountryID = ProcurementGuide.CountryID;
            Edit.MetaTitle = ProcurementGuide.MetaTitle;
            Edit.Product = ProcurementGuide.Product;
            Edit.CreatedTime = ProcurementGuide.CreatedTime;
            Edit.Paid = ProcurementGuide.Paid;
            Edit.Tags = ProcurementGuide.Tags;
            Edit.Summary = ProcurementGuide.Summary;
            Edit.Exclusive = ProcurementGuide.Exclusive;
            Edit.Priority = ProcurementGuide.Priority;
            if (ProcurementGuide.Img != null)
            {
                Edit.Img = ProcurementGuide.Img;
            }
            if (ProcurementGuide.Videos != null)
            {
                Edit.Videos = ProcurementGuide.Videos;
            }
            _context.Entry(Edit).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool UpdateProcurementGuideStatus(int ProcurementGuideId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_ProcurementGuide Edit = _context.SA_ProcurementGuide.Where(Cat => Cat.id == ProcurementGuideId).FirstOrDefault();
            if (Edit.status.HasValue)
            {
                if (Edit.status.Value == 1)
                {
                    Edit.status = 2;
                }
                else
                {
                    Edit.status = 1;
                }
            }
            else
            {
                Edit.status = 1;
            }


            _context.Entry(Edit).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        internal List<SA_ProcurementGuideAndProductRelation> GetProcurementGuideProduct(int id)
        {
            return _context.SA_ProcurementGuideAndProductRelation.Where(x => x.SA_ProcurementGuideId == id).ToList();
        }

        public bool DeleteProcurementGuide(int ProcurementGuideId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_ProcurementGuide EditProcurementGuide = _context.SA_ProcurementGuide.Where(ProcurementGuide => ProcurementGuide.id == ProcurementGuideId).FirstOrDefault();
            _context.Entry(EditProcurementGuide).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool AddProcurementGuide(SA_ProcurementGuide ProcurementGuide)
        {
            //  News.CreatedDate = DateTime.Now;
            _context.SA_ProcurementGuide.Add(ProcurementGuide);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool AddProcurementGuideProduct(SA_ProcurementGuideAndProductRelation np)
        {
            //  News.CreatedDate = DateTime.Now;
            _context.SA_ProcurementGuideAndProductRelation.Add(np);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateProcurementGuide(SA_ProcurementGuide ProcurementGuide)
        {
            _context.Entry(ProcurementGuide).State = EntityState.Modified;
            //  News.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        internal SA_ProcurementGuide GetProcurementGuideByid(int id)
        {
            return _context.SA_ProcurementGuide.Where(x => x.id == id && x.status == 1).SingleOrDefault();
        }

        public bool DeleteProcurementGuideProduct(int DealsId)
        {
            //  Deals.CreatedDate = DateTime.Now;

            _context.Database.ExecuteSqlCommand("delete from SA_ProcurementGuideAndProductRelation where SA_ProcurementGuideId = " + DealsId);

            return true;
        }

        internal List<SelectListItem> GetProductList()
        {
            return (from product in _context.SA_Product
                    where product.status == 1
                    //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                    select new SelectListItem { Text = product.ProductName, Value = product.id.ToString() }).OrderBy(w => w.Text).ToList();
        }

        internal async System.Threading.Tasks.Task<SA_ProcurementGuide> GetProcurementGuideByidUI(int id)
        {
            try
            {
                return _context.SA_ProcurementGuide.Where(x => x.id == id && x.status == 1).SingleOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<List<SA_ProcurementGuide>> GetProcurementGuideListNewsUI()
        {
            return _context.SA_ProcurementGuide.Where(x => x.status == 1 && x.CreatedTime <= DateTime.Now).ToList();
        }







    }
}