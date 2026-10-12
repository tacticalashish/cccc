using ChemAnalyst.SQLHelpers;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ChemAnalyst.DAL
{
    public class MasterImplementation
    {

        //public EmployeeVM GetEmployeeDetails(EmployeeDetails empDetails)
        //{

        //    EmployeeVM vmGetGrid = new EmployeeVM();
        //    SqlParameter[] prms = new SqlParameter[2];
        //    prms[0] = new SqlParameter("@Row_id", "");
        //    prms[1] = new SqlParameter("@MODE", "GET");

        //    DataSet ds = new DataSet();
        //    ds = (new DBHelper().GetDatasetFromSP)("dbo.PC_EmpMaster", prms);
        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            vmGetGrid.loadEmployeeList = ds.Tables[0].ToList<EmployeeDetails>().ToList();
        //        }

        //    }
        //    return vmGetGrid;
        //}

        //public int SaveUOMClassDetail(vmClass ObjUOM)
        //{
        //    try
        //    {
        //        SqlParameter[] prms = new SqlParameter[9];
        //        if (ObjUOM.UOM_Class_ID == 0)
        //        {
        //            prms[0] = new SqlParameter("@Mode", "SAVE");
        //        }
        //        else
        //        {
        //            prms[0] = new SqlParameter("@Mode", "UPDATE");
        //        }
        //        prms[1] = new SqlParameter("@UOM_Class_ID", ObjUOM.UOM_Class_ID);
        //        prms[2] = new SqlParameter("@UOM_Class_Name", ObjUOM.UOM_Class_Name);
        //        prms[3] = new SqlParameter("@UOM_Class_Desc", ObjUOM.UOM_Class_Desc);
        //        prms[4] = new SqlParameter("@InActive", ObjUOM.InActive);
        //        prms[5] = new SqlParameter("@Modified_By", "");
        //        prms[6] = new SqlParameter("@Created_By", "");
        //        prms[7] = new SqlParameter("@Modified_Date", DateTime.Now);
        //        prms[8] = new SqlParameter("@Created_Date", DateTime.Now);
        //        DataSet ds = new DataSet();
        //        ds = (new DBHelper().GetDatasetFromSP)("dbo.Proc_GetUOMClass", prms);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                ObjUOM.Result = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //            }
        //        }
        //        return ObjUOM.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public int DeltUOMClassDetails(vmClass ObjUOM)
        //{

        //    SqlParameter[] prms = new SqlParameter[2];
        //    prms[0] = new SqlParameter("@UOM_Class_ID", ObjUOM.UOM_Class_ID);
        //    prms[1] = new SqlParameter("@Mode", "DELT");
        //    DBHelper o = new DBHelper();
        //    o.ExecuteNonQuery("dbo.Proc_GetUOMClass", prms);
        //    var Result = 1;
        //    return Result;
        //}
        
    }
}