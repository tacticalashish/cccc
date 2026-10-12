using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChemAnalyst.DAL
{
    public class ServicesIndexDAL
    {
        private ChemAnalystContext _context = new ChemAnalystContext();


        internal int DeleteRecordBasedOnFileName(string Filename)
        {
            int numberOfDeletedRecords = 0;

            List<SA_ServicesIndex> obj = null;// _context.SA_ServicesIndex.Where(x => x.Filename == Filename).ToList();
            foreach (SA_ServicesIndex index in obj)
            {
                _context.Entry(index).State = EntityState.Deleted;
                numberOfDeletedRecords = _context.SaveChanges();
            }


            return numberOfDeletedRecords;
        }


        internal bool IsRecordExistBasedOnFileName(string Filename)
        {
            bool isFileExist = false;
            List<SA_ServicesIndex> obj = null;//_context.SA_ServicesIndex.Where(x => x.Filename == Filename).ToList();
            if (obj.Count() > 0)
                isFileExist = true;
            return isFileExist;
        }

        public string ToValidateData(DataTable dataTable)
        {
            string result = "please check excel, did not found any records";
            int count = dataTable.AsEnumerable().Select(item => item.ItemArray).FirstOrDefault().Count();
            var listOfObject = dataTable.AsEnumerable().SelectMany(x => x.ItemArray).Select((v, i) => new { Value = v, Index = i % count }).ToList();

            bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 8).Select(it => it.Value).ToList());
            if (!isCountNumber)
                throw new Exception("count values are not number");
            result = "success";

            return result;
        }

        private bool IsListTypeOfDecimal(List<object> listOfObject)
        {
            bool result = true;

            try
            {
                List<decimal> castListToDecimal = listOfObject.ConvertAll<decimal>(Convert.ToDecimal).ToList();

                if (listOfObject.Count() != castListToDecimal.Count())
                    throw new Exception("mismatch count list");

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;

        }

    }
}