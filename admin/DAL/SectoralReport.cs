using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChemAnalyst.DAL
{
    public class SectoralReport
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_Sectoral> GetSectoralList()
        {
            return _context.SA_Sectoral.ToList();

        }

        public List<SA_Sectoral> GetSectoralList(List<string> lst)
        {
            var query = (from SA_Sectoral in _context.SA_Sectoral
                         where lst.Contains(SA_Sectoral.Product.ToString())
                         select new { SA_Sectoral });

            List<SA_Sectoral> returnpro = new List<SA_Sectoral>();
            foreach (var item in query)
            {
                returnpro.Add(item.SA_Sectoral);
            }
            return returnpro;

        }

        public bool EditSectoral(SA_Sectoral News)
        {

            SA_Sectoral EditNews = _context.SA_Sectoral.Where(Cat => Cat.id == News.id).FirstOrDefault();
            EditNews.Title = News.Title;
            EditNews.Description = News.Description;
            EditNews.Meta = News.Meta;
            EditNews.MetaDescription = News.MetaDescription;
            EditNews.IsActive = News.IsActive;
            EditNews.IsDelete = News.IsDelete;
            EditNews.Product = News.Product;
            EditNews.RegionFID = News.RegionFID;
            EditNews.CountryFID = News.CountryFID;
            EditNews.ImgPath = News.ImgPath;
            EditNews.CreatedTime = News.CreatedTime;
            EditNews.Producers = News.Producers;

            _context.Entry(EditNews).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool DeleteSectoral(int NewsId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_Sectoral EditNews = _context.SA_Sectoral.Where(News => News.id == NewsId).FirstOrDefault();
            _context.Entry(EditNews).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public async Task<bool> AddSectoral(SA_Sectoral News)
        {
            News.IsActive = true;
            News.IsDelete = false;
            News.CreatedTime = News.CreatedTime > DateTime.MinValue ? News.CreatedTime : DateTime.Now;

            _context.SA_Sectoral.Add(News);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateSectoral(SA_Sectoral News)
        {
            _context.Entry(News).State = EntityState.Modified;
            //  News.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        internal SA_Sectoral GetSectroalByid(int id)
        {
            var data = _context.SA_Sectoral.Where(x => x.id == id).SingleOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public List<SA_Sectoral> GetSectoralList(int id)
        {
            return _context.SA_Sectoral.Where(w => w.id == id).ToList();

        }
    }
}