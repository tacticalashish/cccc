using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChemAnalyst.DAL
{
    public class ProductdescriptionData
    {
        private ChemAnalystContext _context = new ChemAnalystContext();
        

        public bool Addproductdescription(Productdescription model)
        {
            model.CreatedDate = DateTime.Now;
            //model.ModifiedDate = DateTime.Now;
            
            _context.Productdescriptions.Add(model);
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
        public bool Editproductdescription(Productdescription model)
            {
          
            Productdescription pd = new Productdescription();
            pd.Product = model.Product;
            pd.Iupac = model.Iupac;
            pd.Cas = model.Cas;
            pd.ProductionProcess = model.ProductionProcess;
            pd.Feedstock = model.Feedstock;
            pd.Grades = model.Grades;
            pd.Enduse = model.Enduse;
            pd.Referallinks = model.Referallinks;
            model.ModifiedDate = DateTime.Now;

            _context.Entry(model).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }


        public Productdescription GetDescriptionByid(int Id)
        {
            var data = _context.Productdescriptions.Where(x => x.Id == Id).SingleOrDefault();
            if (data != null)
            {
                return data;
                // data.CreatedDate.Format("yyyy-mm-dd h:mm tt");
            }
            else
            {
                return null;
            }
        }

       

        public bool DeleteProductdescription(int Id)
        {
            Productdescription pd = _context.Productdescriptions.Where(model => model.Id == Id).SingleOrDefault();
            _context.Entry(pd).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
    }
}