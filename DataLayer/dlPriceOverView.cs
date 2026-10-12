using EntityLayer;
using EntityLayer.CustomEntity;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataLayer
{
    public class dlPriceOverView
    {
        private enPriceOverView _enPriceOverView = null;

        public dlPriceOverView(enPriceOverView enPriceOverView_)
        {
            this._enPriceOverView = enPriceOverView_;
        }

        public void Read()
        {
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_PriceOverView_READ]", new object[] {
                                                            _enPriceOverView.Product
                                                            }))
            {
                if (idr.Read())
                {
                    ConstructObject(idr, _enPriceOverView);
                }
            }
        }
        public List<enPriceOverView> GetPricingList(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPriceOverViewDetailsById]", new object[] {
                                                             en.ID,//PricingOvervoiewid
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForPricingList(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }
        public List<enPriceOverView> GetPricingdataByProduct(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetGetPricingdataByProduct]", new object[] {
                                                             en.ID,//PricingOvervoiewid
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForPriceOverView(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        public List<enPriceOverView> GetIndustryLandingList(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetIndustryLandingDetailsById]", new object[] {
                                                             en.ID,//PricingOvervoiewid
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForIndustrydataView(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        public List<enPriceOverView> BindFeedStockBasedonPriceoverview(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetFeedStockProductBasedonPriceOverview]", new object[] {
                                                             en.ProductName,
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForFeedStockBasedonPriceoverview(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }


        public List<enPriceOverView> GetProductStatus(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GETProductStatusForPriceoverview]", new object[] {
                                                             en.ProductName,
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForFeedStockBasedonPriceoverview(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        public List<enPriceOverView> BindParentproductBasedonChild(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetParentProductBasedOnChild]", new object[] {
                                                             en.ProductName,
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForFeedStockBasedonPriceoverview(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        public List<enPriceOverView> GetAILandingPageBasedOnProduct(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAILandingPageBasedOnProduct]", new object[] {
                                                             en.Product,
                                                             en.Quaterly,
                                                             en.Year,
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForAILandingPageBasedOnProduct(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }
            return ObjList;
        }

        public List<enPriceOverView> GetAILandingPageBasedOnProductForUpload(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAILandingPageBasedOnProductUpload]", new object[] {
                                                             en.Product,
                                                           }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForAILandingPageBasedOnProduct(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }
            return ObjList;
        }

        public List<enPriceOverView> UpdateStatusForAILandingPage(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateStatusForAILandingPage]", new object[] {
                                                            en.Flag,
                                                            en.Product,
                                                            en.ApprovedBy,
                                                            en.Quaterly ,
                                                            en.Year ,
                                                            en.Description ,
                                                            en.Q1 ,
                                                            en.Ans1 ,
                                                            en.Q2 ,
                                                            en.Ans2 ,
                                                            en.Q3,
                                                            en.Ans3 ,
                                                            en.Q4 ,
                                                            en.Ans4
        }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForUpdateStatusForAILandingPage(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        public List<enPriceOverView> UpdateAssociateStatusForAILandingPage(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateAssociateStatusForAILandingPage]", new object[] {
                                                            en.ID,
                                                            en.Flag,
                                                            en.Product,
                                                            en.Quaterly ,
                                                            en.Year ,
                                                            en.Description ,
                                                            en.Q1 ,
                                                            en.Ans1 ,
                                                            en.Q2 ,
                                                            en.Ans2 ,
                                                            en.Q3,
                                                            en.Ans3 ,
                                                            en.Q4 ,
                                                            en.Ans4,
                                                            en.CreatedBy
        }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForUpdateStatusForAILandingPage(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        public List<enPriceOverView> UpdateConsultantStatusForAILandingPage(enPriceOverView en)
        {
            enPriceOverView objen = new enPriceOverView();
            List<enPriceOverView> ObjList = new List<enPriceOverView>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateConsultantStatusForAILandingPage]", new object[] {
                                                            en.ID,
                                                            en.Flag,
                                                            en.Product,
                                                            en.Quaterly ,
                                                            en.Year ,
                                                            en.Description ,
                                                            en.Q1 ,
                                                            en.Ans1 ,
                                                            en.Q2 ,
                                                            en.Ans2 ,
                                                            en.Q3,
                                                            en.Ans3 ,
                                                            en.Q4 ,
                                                            en.Ans4,
                                                            en.CreatedBy
        }))
            {
                while (idr.Read())
                {
                    enPriceOverView lstdata = new enPriceOverView();
                    ConstructObjectForUpdateStatusForAILandingPage(idr, lstdata);
                    ObjList.Add(lstdata);
                }
            }

            return ObjList;
        }

        private void ConstructObjectForPriceOverView(IDataReader idr, enPriceOverView enChemanalystApp_)
        {
            enChemanalystApp_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enChemanalystApp_.Title = DBNull.Value == idr["Title"] ? null : Convert.ToString(idr["Title"]);
            enChemanalystApp_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enChemanalystApp_.Tableoc = DBNull.Value == idr["Tableoc"] ? null : Convert.ToString(idr["Tableoc"]);
            enChemanalystApp_.Figot = DBNull.Value == idr["Figot"] ? null : Convert.ToString(idr["Figot"]);
            enChemanalystApp_.RelatedRep = DBNull.Value == idr["RelatedRep"] ? null : Convert.ToString(idr["RelatedRep"]);
            enChemanalystApp_.Pages = DBNull.Value == idr["Pages"] ? null : Convert.ToString(idr["Pages"]);
            enChemanalystApp_.format = DBNull.Value == idr["format"] ? null : Convert.ToString(idr["format"]);
            enChemanalystApp_.Industry = DBNull.Value == idr["Industry"] ? null : Convert.ToString(idr["Industry"]);
            enChemanalystApp_.Meta = DBNull.Value == idr["Meta"] ? null : Convert.ToString(idr["Meta"]);
            enChemanalystApp_.MetaTitle = DBNull.Value == idr["MetaTitle"] ? null : Convert.ToString(idr["MetaTitle"]);
            enChemanalystApp_.MetaDescription = DBNull.Value == idr["MetaDescription"] ? null : Convert.ToString(idr["MetaDescription"]);
            enChemanalystApp_.CreatedTime = idr["CreatedTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);
            enChemanalystApp_.URL = DBNull.Value == idr["URL"] ? null : Convert.ToString(idr["URL"]);
            enChemanalystApp_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enChemanalystApp_.CountryID = DBNull.Value == idr["CountryID"] ? 0 : Convert.ToInt32(idr["CountryID"]);
            enChemanalystApp_.CategoryID = DBNull.Value == idr["CategoryID"] ? 0 : Convert.ToInt32(idr["CategoryID"]);
            enChemanalystApp_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);

            enChemanalystApp_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);

            enChemanalystApp_.CategoryName = DBNull.Value == idr["CategoryName"] ? null : Convert.ToString(idr["CategoryName"]);
            enChemanalystApp_.AlternateURL = DBNull.Value == idr["AlternateURL"] ? null : Convert.ToString(idr["AlternateURL"]);


        }


        private void ConstructObject(IDataReader idr, enPriceOverView enPriceOverView_)
        {
            enPriceOverView_.ID = Convert.ToInt32(idr["ID"]);
            enPriceOverView_.Title = DBNull.Value == idr["Title"] ? null : Convert.ToString(idr["Title"]);
            enPriceOverView_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enPriceOverView_.URL = DBNull.Value == idr["URL"] ? null : Convert.ToString(idr["URL"]);
            enPriceOverView_.Meta = DBNull.Value == idr["Meta"] ? null : Convert.ToString(idr["Meta"]);
            enPriceOverView_.MetaTitle = DBNull.Value == idr["MetaTitle"] ? null : Convert.ToString(idr["MetaTitle"]);
            enPriceOverView_.MetaDescription = DBNull.Value == idr["MetaDescription"] ? null : Convert.ToString(idr["MetaDescription"]);
            enPriceOverView_.Tableoc = DBNull.Value == idr["Tableoc"] ? null : Convert.ToString(idr["Tableoc"]);
            enPriceOverView_.Figot = DBNull.Value == idr["Figot"] ? null : Convert.ToString(idr["Figot"]);
            enPriceOverView_.RelatedRep = DBNull.Value == idr["RelatedRep"] ? null : Convert.ToString(idr["RelatedRep"]);
            enPriceOverView_.Pages = DBNull.Value == idr["Pages"] ? null : Convert.ToString(idr["Pages"]);
            enPriceOverView_.format = DBNull.Value == idr["format"] ? null : Convert.ToString(idr["format"]);
            enPriceOverView_.Industry = DBNull.Value == idr["Industry"] ? null : Convert.ToString(idr["Industry"]);
            enPriceOverView_.CreatedTime = DBNull.Value == idr["CreatedTime"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);
            enPriceOverView_.Product = DBNull.Value == idr["Product"] ? (int?)null : Convert.ToInt32(idr["Product"]);
            enPriceOverView_.CountryID = DBNull.Value == idr["CountryID"] ? (int?)null : Convert.ToInt32(idr["CountryID"]);
            enPriceOverView_.CategoryID = DBNull.Value == idr["CategoryID"] ? (int?)null : Convert.ToInt32(idr["CategoryID"]);
        }

        private void ConstructObjectForIndustrydataView(IDataReader idr, enPriceOverView enChemanalystApp_)
        {
            enChemanalystApp_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enChemanalystApp_.Title = DBNull.Value == idr["Title"] ? null : Convert.ToString(idr["Title"]);
            enChemanalystApp_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enChemanalystApp_.Tableoc = DBNull.Value == idr["Tableoc"] ? null : Convert.ToString(idr["Tableoc"]);
            enChemanalystApp_.Figot = DBNull.Value == idr["Figot"] ? null : Convert.ToString(idr["Figot"]);
            enChemanalystApp_.RelatedRep = DBNull.Value == idr["RelatedRep"] ? null : Convert.ToString(idr["RelatedRep"]);
            enChemanalystApp_.Pages = DBNull.Value == idr["Pages"] ? null : Convert.ToString(idr["Pages"]);
            enChemanalystApp_.format = DBNull.Value == idr["format"] ? null : Convert.ToString(idr["format"]);
            enChemanalystApp_.Industry = DBNull.Value == idr["Industry"] ? null : Convert.ToString(idr["Industry"]);
            enChemanalystApp_.Meta = DBNull.Value == idr["Meta"] ? null : Convert.ToString(idr["Meta"]);
            enChemanalystApp_.MetaTitle = DBNull.Value == idr["MetaTitle"] ? null : Convert.ToString(idr["MetaTitle"]);
            enChemanalystApp_.MetaDescription = DBNull.Value == idr["MetaDescription"] ? null : Convert.ToString(idr["MetaDescription"]);
            enChemanalystApp_.CreatedTime = idr["CreatedTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);
            enChemanalystApp_.URL = DBNull.Value == idr["URL"] ? null : Convert.ToString(idr["URL"]);
            enChemanalystApp_.CountryID = DBNull.Value == idr["CountryID"] ? 0 : Convert.ToInt32(idr["CountryID"]);
            enChemanalystApp_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);



        }

        private void ConstructObjectForPricingList(IDataReader idr, enPriceOverView enChemanalystApp_)
        {
            enChemanalystApp_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enChemanalystApp_.Title = DBNull.Value == idr["Title"] ? null : Convert.ToString(idr["Title"]);
            enChemanalystApp_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enChemanalystApp_.Tableoc = DBNull.Value == idr["Tableoc"] ? null : Convert.ToString(idr["Tableoc"]);
            enChemanalystApp_.Figot = DBNull.Value == idr["Figot"] ? null : Convert.ToString(idr["Figot"]);
            enChemanalystApp_.RelatedRep = DBNull.Value == idr["RelatedRep"] ? null : Convert.ToString(idr["RelatedRep"]);
            enChemanalystApp_.Pages = DBNull.Value == idr["Pages"] ? null : Convert.ToString(idr["Pages"]);
            enChemanalystApp_.format = DBNull.Value == idr["format"] ? null : Convert.ToString(idr["format"]);
            enChemanalystApp_.Industry = DBNull.Value == idr["Industry"] ? null : Convert.ToString(idr["Industry"]);
            enChemanalystApp_.Meta = DBNull.Value == idr["Meta"] ? null : Convert.ToString(idr["Meta"]);
            enChemanalystApp_.MetaTitle = DBNull.Value == idr["MetaTitle"] ? null : Convert.ToString(idr["MetaTitle"]);
            enChemanalystApp_.MetaDescription = DBNull.Value == idr["MetaDescription"] ? null : Convert.ToString(idr["MetaDescription"]);
            enChemanalystApp_.CreatedTime = idr["CreatedTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);
            enChemanalystApp_.URL = DBNull.Value == idr["URL"] ? null : Convert.ToString(idr["URL"]);
            enChemanalystApp_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enChemanalystApp_.CountryID = DBNull.Value == idr["CountryID"] ? 0 : Convert.ToInt32(idr["CountryID"]);
            enChemanalystApp_.CategoryID = DBNull.Value == idr["CategoryID"] ? 0 : Convert.ToInt32(idr["CategoryID"]);
            enChemanalystApp_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enChemanalystApp_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enChemanalystApp_.CategoryName = DBNull.Value == idr["CategoryName"] ? null : Convert.ToString(idr["CategoryName"]);
            enChemanalystApp_.AlternateURL = DBNull.Value == idr["AlternateURL"] ? null : Convert.ToString(idr["AlternateURL"]);
            enChemanalystApp_.Q1 = DBNull.Value == idr["Q1"] ? null : Convert.ToString(idr["Q1"]);
            enChemanalystApp_.Q2 = DBNull.Value == idr["Q2"] ? null : Convert.ToString(idr["Q2"]);
            enChemanalystApp_.Q3 = DBNull.Value == idr["Q3"] ? null : Convert.ToString(idr["Q3"]);
            enChemanalystApp_.Q4 = DBNull.Value == idr["Q4"] ? null : Convert.ToString(idr["Q4"]);
            enChemanalystApp_.Ans1 = DBNull.Value == idr["Ans1"] ? null : Convert.ToString(idr["Ans1"]);
            enChemanalystApp_.Ans2 = DBNull.Value == idr["Ans2"] ? null : Convert.ToString(idr["Ans2"]);
            enChemanalystApp_.Ans3 = DBNull.Value == idr["Ans3"] ? null : Convert.ToString(idr["Ans3"]);
            enChemanalystApp_.Ans4 = DBNull.Value == idr["Ans4"] ? null : Convert.ToString(idr["Ans4"]);
            enChemanalystApp_.AnalystjapanURL = DBNull.Value == idr["AnalystjapanURL"] ? null : Convert.ToString(idr["AnalystjapanURL"]);
            enChemanalystApp_.AnalystkoreaURL = DBNull.Value == idr["AnalystkoreaURL"] ? null : Convert.ToString(idr["AnalystkoreaURL"]);
        }

        private void ConstructObjectForFeedStockBasedonPriceoverview(IDataReader idr, enPriceOverView enChemanalystApp_)
        {
            enChemanalystApp_.ID = DBNull.Value == idr["ChildProductId"] ? 0 : Convert.ToInt32(idr["ChildProductId"]);
            enChemanalystApp_.ProductName = DBNull.Value == idr["ChildProductName"] ? null : Convert.ToString(idr["ChildProductName"]);
            enChemanalystApp_.URL = DBNull.Value == idr["ShareURL"] ? null : Convert.ToString(idr["ShareURL"]);
            enChemanalystApp_.Status = DBNull.Value == idr["Status"] ? 0 : Convert.ToInt32(idr["Status"]);
        }

        private void ConstructObjectForAILandingPageBasedOnProduct(IDataReader idr, enPriceOverView enChemanalystApp_)
        {
            enChemanalystApp_.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            enChemanalystApp_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enChemanalystApp_.Quaterly = DBNull.Value == idr["Quaterly"] ? null : Convert.ToString(idr["Quaterly"]);
            enChemanalystApp_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enChemanalystApp_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enChemanalystApp_.Q1 = DBNull.Value == idr["Ques1"] ? null : Convert.ToString(idr["Ques1"]);
            enChemanalystApp_.Q2 = DBNull.Value == idr["Ques2"] ? null : Convert.ToString(idr["Ques2"]);
            enChemanalystApp_.Q3 = DBNull.Value == idr["Ques3"] ? null : Convert.ToString(idr["Ques3"]);
            enChemanalystApp_.Q4 = DBNull.Value == idr["Ques4"] ? null : Convert.ToString(idr["Ques4"]);
            enChemanalystApp_.Ans1 = DBNull.Value == idr["Ans1"] ? null : Convert.ToString(idr["Ans1"]);
            enChemanalystApp_.Ans2 = DBNull.Value == idr["Ans2"] ? null : Convert.ToString(idr["Ans2"]);
            enChemanalystApp_.Ans3 = DBNull.Value == idr["Ans3"] ? null : Convert.ToString(idr["Ans3"]);
            enChemanalystApp_.Ans4 = DBNull.Value == idr["Ans4"] ? null : Convert.ToString(idr["Ans4"]);
        }

        private void ConstructObjectForUpdateStatusForAILandingPage(IDataReader idr, enPriceOverView enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }

       

        

    }
}
