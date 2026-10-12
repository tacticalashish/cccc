using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChemAnalyst.DAL
{
    public class StrategicDataStore
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_Mastrategic> GetStrategicList()
        {
            return _context.SA_Mastrategic.ToList();

        }

        public List<SA_Mastrategic> GetStrategicList(List<string> lst)
        {
            var query = (from SA_Mastrategic in _context.SA_Mastrategic
                         where lst.Contains(SA_Mastrategic.Product.ToString())
                         select new { SA_Mastrategic });

            List<SA_Mastrategic> returnpro = new List<SA_Mastrategic>();
            foreach (var item in query)
            {
                returnpro.Add(item.SA_Mastrategic);
            }
            return returnpro;

        }

        public bool Editstrategic(SA_Mastrategic News)
        {

            SA_Mastrategic EditNews = _context.SA_Mastrategic.Where(Cat => Cat.id == News.id).FirstOrDefault();
            EditNews.Title = News.Title;
            EditNews.Description = News.Description;
            EditNews.Meta = News.Meta;
            
            EditNews.MetaTitle = News.MetaTitle;
            EditNews.MetaDescription = News.MetaDescription;
           
            EditNews.Product = News.Product;
            EditNews.CategoryID = News.CategoryID;
            EditNews.CountryID = News.CountryID;
            EditNews.CreatedTime = News.CreatedTime;
            _context.Entry(EditNews).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
        public bool Deletestrategic(int NewsId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_Mastrategic EditNews = _context.SA_Mastrategic.Where(News => News.id == NewsId).FirstOrDefault();
            _context.Entry(EditNews).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
        public async Task<bool> AddMastrategic(SA_Mastrategic News)
        {
            News.CreatedTime = News.CreatedTime != null ? News.CreatedTime.Value : DateTime.Now;

            _context.SA_Mastrategic.Add(News);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Updateoverview(SA_Mastrategic News)
        {
            _context.Entry(News).State = EntityState.Modified;
            //  News.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        internal SA_Mastrategic GetstrategicByid(int id)
        {
            var data = _context.SA_Mastrategic.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public List<SA_Mastrategic> GetStragegicviewList(int id)
        {
            return _context.SA_Mastrategic.Where(w => w.id == id).ToList();

        }

    }
}