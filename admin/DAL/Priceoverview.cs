using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChemAnalyst.DAL
{
    public class OverviewDataStore
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_priceoverview> GetOverviewList()
        {
            return _context.SA_priceoverview.ToList();

        }

        public List<SA_priceoverview> GetCustoverviewList(List<string> lst)
        {
            var query = (from SA_priceoverview in _context.SA_priceoverview
                         where lst.Contains(SA_priceoverview.Product.ToString())
                         select new { SA_priceoverview });

            List<SA_priceoverview> returnpro = new List<SA_priceoverview>();
            foreach (var item in query)
            {
                returnpro.Add(item.SA_priceoverview);
            }
            return returnpro;

        }

        public bool Editoverview(SA_priceoverview News)
        {

            SA_priceoverview EditNews = _context.SA_priceoverview.Where(Cat => Cat.id == News.id).FirstOrDefault();
            EditNews.Title = News.Title;
            EditNews.Description = News.Description;
            EditNews.Meta = News.Meta;
            EditNews.URL = News.URL;
            EditNews.MetaTitle = News.MetaTitle;
            EditNews.MetaDescription = News.MetaDescription;
            EditNews.Figot = News.Figot;
            EditNews.Industry = News.Industry;
            EditNews.format = News.format;
            EditNews.Pages = News.Pages;
            EditNews.RelatedRep = News.RelatedRep;
            EditNews.Tableoc = News.Tableoc;
            EditNews.Product = News.Product;
            EditNews.CategoryID = News.CategoryID;
            EditNews.CountryID = News.CountryID;
            EditNews.CreatedTime = News.CreatedTime;
            EditNews.CreatedDateFirst = News.CreatedDateFirst;
            EditNews.Language = News.Language;
            EditNews.AlternateURL = News.AlternateURL;
            EditNews.Q1 = News.Q1;
            EditNews.Q2 = News.Q2;
            EditNews.Q3 = News.Q3;
            EditNews.Q4 = News.Q4;
            EditNews.Ans1 = News.Ans1;
            EditNews.Ans2 = News.Ans2;
            EditNews.Ans3 = News.Ans3;
            EditNews.Ans4 = News.Ans4;
            EditNews.AnalystjapanURL = News.AnalystjapanURL;
            EditNews.AnalystkoreaURL = News.AnalystkoreaURL;
            _context.Entry(EditNews).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
        public bool Deleteoverview(int NewsId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_priceoverview EditNews = _context.SA_priceoverview.Where(News => News.id == NewsId).FirstOrDefault();
            _context.Entry(EditNews).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
        public async Task<bool> Addoverview(SA_priceoverview News)
        {
            News.CreatedTime = News.CreatedTime != null ? News.CreatedTime.Value : DateTime.Now;

            _context.SA_priceoverview.Add(News);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Updateoverview(SA_priceoverview News)
        {
            _context.Entry(News).State = EntityState.Modified;
            //  News.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        internal SA_priceoverview GetoverviewByid(int id)
        {
            var data = _context.SA_priceoverview.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public List<SA_priceoverview> GetCustoverviewList(int id)
        {
            return _context.SA_priceoverview.Where(w => w.id == id).ToList();

        }

        public List<SA_IndustryLanding> GetIndustrylanding()
        {
            return _context.SA_IndustryLanding.ToList();

        }

        internal SA_IndustryLanding GetIndustrylandingByid(int id)
        {
            var data = _context.SA_IndustryLanding.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        //public bool EditIndustryLanding(SA_IndustryLanding News)
        //{

        //    SA_IndustryLanding EditNews = _context.SA_IndustryLanding.Where(Cat => Cat.id == News.id).FirstOrDefault();
        //    EditNews.Title = News.Title;
        //    EditNews.Description = News.Description;
        //    EditNews.Meta = News.Meta;
        //    EditNews.URL = News.URL;
        //    EditNews.MetaTitle = News.MetaTitle;
        //    EditNews.MetaDescription = News.MetaDescription;
        //    EditNews.Figot = News.Figot;
        //    EditNews.Industry = News.Industry;
        //    EditNews.format = News.format;
        //    EditNews.Pages = News.Pages;
        //    EditNews.RelatedRep = News.RelatedRep;
        //    EditNews.Tableoc = News.Tableoc;

        //    EditNews.CountryID = News.CountryID;
        //    EditNews.CreatedTime = News.CreatedTime;
        //    EditNews.CreatedDateFirst = News.CreatedDateFirst;
        //    _context.Entry(EditNews).State = EntityState.Modified;
        //    int x = _context.SaveChanges();
        //    return x == 0 ? false : true;
        //}



        public async Task<bool> AddIndustryLanding(SA_IndustryLanding News)
        {
            News.CreatedTime = News.CreatedTime != null ? News.CreatedTime.Value : DateTime.Now;

            _context.SA_IndustryLanding.Add(News);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public bool DeleteIndustryLanding(int NewsId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_IndustryLanding EditNews = _context.SA_IndustryLanding.Where(News => News.id == NewsId).FirstOrDefault();
            _context.Entry(EditNews).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }



        public async Task<bool> AddIndustrylandingProduct(SA_IndustrylandingAndProductRelation np)
        {
            //  News.CreatedDate = DateTime.Now;
            _context.SA_IndustrylandingAndProductRelation.Add(np);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }


        public bool DeleteIndustrylandingProduct(int DealsId)
        {
            //  Deals.CreatedDate = DateTime.Now;

            _context.Database.ExecuteSqlCommand("delete from SA_IndustrylandingAndProductRelation where SA_IndustrylandingId = " + DealsId);

            return true;
        }

        internal List<SA_IndustrylandingAndProductRelation> GetIndustrylandingProduct(int id)
        {
            return _context.SA_IndustrylandingAndProductRelation.Where(x => x.SA_IndustrylandingId == id).ToList();
        }


        public bool EditIndustryLanding(SA_IndustryLanding obj)
        {

            //SA_IndustryLanding Edit_SA_IndustryLanding = _context.SA_IndustryLanding.Where(Cat => Cat.id == News.id).FirstOrDefault();
            SA_IndustryLanding Edit_SA_IndustryLanding = _context.SA_IndustryLanding.Where(X => X.id == obj.id).FirstOrDefault();
            Edit_SA_IndustryLanding.Title = obj.Title;
            Edit_SA_IndustryLanding.Description = obj.Description;
            Edit_SA_IndustryLanding.Meta = obj.Meta;
            Edit_SA_IndustryLanding.URL = obj.URL;
            Edit_SA_IndustryLanding.MetaTitle = obj.MetaTitle;
            Edit_SA_IndustryLanding.MetaDescription = obj.MetaDescription;
            Edit_SA_IndustryLanding.Figot = obj.Figot;
            Edit_SA_IndustryLanding.Industry = obj.Industry;
            Edit_SA_IndustryLanding.format = obj.format;
            Edit_SA_IndustryLanding.Pages = obj.Pages;
            Edit_SA_IndustryLanding.RelatedRep = obj.RelatedRep;
            Edit_SA_IndustryLanding.Tableoc = obj.Tableoc;

            Edit_SA_IndustryLanding.CountryID = obj.CountryID;
            Edit_SA_IndustryLanding.CreatedTime = obj.CreatedTime;
            Edit_SA_IndustryLanding.CreatedDateFirst = obj.CreatedDateFirst;
            _context.Entry(Edit_SA_IndustryLanding).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }






    }
}


