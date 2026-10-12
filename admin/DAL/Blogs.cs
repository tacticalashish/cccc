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
    public class BlogsDataStore
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_Blogs> GetBlogsListAdmin()
        {
            return _context.SA_Blogs.ToList();
        }

        public List<SA_Blogs> GetBlogsList()
        {

            return _context.SA_Blogs.Where(x => x.status == 1).ToList();

        }

        public bool EditBlogs(SA_Blogs Blogs)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_Blogs EditBlogs = _context.SA_Blogs.Where(Cat => Cat.id == Blogs.id).FirstOrDefault();
            EditBlogs.Name = Blogs.Name;
            EditBlogs.Discription = Blogs.Discription;
            EditBlogs.URL = Blogs.URL;
            EditBlogs.MetaDiscription = Blogs.MetaDiscription;
            EditBlogs.Keywords = Blogs.Keywords;
            EditBlogs.Author = Blogs.Author;
            EditBlogs.CategoryID = Blogs.CategoryID;
            EditBlogs.CountryID = Blogs.CountryID;
            EditBlogs.MetaTitle = Blogs.MetaTitle;
            EditBlogs.Product = Blogs.Product;
            EditBlogs.CreatedTime = Blogs.CreatedTime;
            EditBlogs.Paid = Blogs.Paid;
            EditBlogs.Tags = Blogs.Tags;
            EditBlogs.Summary = Blogs.Summary;
            EditBlogs.Exclusive = Blogs.Exclusive;
            EditBlogs.Priority = Blogs.Priority;
            if (Blogs.Img != null)
            {
                EditBlogs.Img = Blogs.Img;
            }
            if (Blogs.Videos != null)
            {
                EditBlogs.Videos = Blogs.Videos;
            }
            _context.Entry(EditBlogs).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool UpdateBlogsStatus(int BlogsId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_Blogs Edit = _context.SA_Blogs.Where(Cat => Cat.id == BlogsId).FirstOrDefault();
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

        internal List<SA_BlogsAndProductRelation> GetBlogsProduct(int id)
        {
            return _context.SA_BlogsAndProductRelation.Where(x => x.SA_BlogsId == id).ToList();
        }

        public bool DeleteBlogs(int BlogId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_Blogs EditBlogs = _context.SA_Blogs.Where(Blogs => Blogs.id == BlogId).FirstOrDefault();
            _context.Entry(EditBlogs).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool AddBlogs(SA_Blogs Blog)
        {
            //  News.CreatedDate = DateTime.Now;
            _context.SA_Blogs.Add(Blog);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool AddBlogsProduct(SA_BlogsAndProductRelation np)
        {
            //  News.CreatedDate = DateTime.Now;
            _context.SA_BlogsAndProductRelation.Add(np);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateBlogs(SA_Blogs Blogs)
        {
            _context.Entry(Blogs).State = EntityState.Modified;
            //  News.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        internal SA_Blogs GetBlogsByid(int id)
        {
            return _context.SA_Blogs.Where(x => x.id == id && x.status == 1).SingleOrDefault();
        }

        public bool DeleteBlogsProduct(int DealsId)
        {
            //  Deals.CreatedDate = DateTime.Now;

            _context.Database.ExecuteSqlCommand("delete from SA_BlogsAndProductRelation where SA_BlogsId = " + DealsId);

            return true;
        }

        internal List<SelectListItem> GetProductList()
        {
            return (from product in _context.SA_Product
                    where product.status == 1
                    //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                    select new SelectListItem { Text = product.ProductName, Value = product.id.ToString() }).OrderBy(w => w.Text).ToList();
        }

        internal async System.Threading.Tasks.Task<SA_Blogs> GetBlogsByidUI(int id)
        {
            try
            {
                return _context.SA_Blogs.Where(x => x.id == id && x.status == 1).SingleOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<List<SA_Blogs>> GetBlogsListNewsUI()
        {
            return _context.SA_Blogs.Where(x => x.status == 1 && x.CreatedTime <= DateTime.Now).ToList();
        }


        public bool AddInterview(SA_InterviewVideo InterviewVideo)
        {

            _context.SA_InterviewVideo.Add(InterviewVideo);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }


        public bool EditInterviewVideo(SA_InterviewVideo InterviewVideo)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_InterviewVideo EditInterview = _context.SA_InterviewVideo.Where(Cat => Cat.id == InterviewVideo.id).FirstOrDefault();
            EditInterview.Name = InterviewVideo.Name;
            EditInterview.CreatedTime = InterviewVideo.CreatedTime;
            if (InterviewVideo.Videos != null)
            {
                EditInterview.Videos = InterviewVideo.Videos;
            }
            _context.Entry(EditInterview).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        internal SA_InterviewVideo GetInterviewVideoByid(int id)
        {
            return _context.SA_InterviewVideo.Where(x => x.id == id && x.status == 1).SingleOrDefault();
        }

        public bool UpdateInterviewStatus(int InterviewId)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_InterviewVideo Edit = _context.SA_InterviewVideo.Where(Cat => Cat.id == InterviewId).FirstOrDefault();
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

        public bool DeleteInterview(int Id)
        {
            //  News.CreatedDate = DateTime.Now;
            SA_InterviewVideo EditBlogs = _context.SA_InterviewVideo.Where(Blogs => Blogs.id == Id).FirstOrDefault();
            _context.Entry(EditBlogs).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }






    }
}