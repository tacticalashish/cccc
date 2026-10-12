using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlPriceOverView;

namespace BusinessLayer 
{
    public class blPriceOverView
    {
        private enPriceOverView _enPriceOverView = null;
        private DAL _objDAL = null;

        public enPriceOverView GetEntityReference
        {
            get { return this._enPriceOverView; }
        }

        public blPriceOverView(enPriceOverView enPriceOverView_)
        {
            this._enPriceOverView = enPriceOverView_;
        }

        public void Read()
        {
            GetDALReference().Read();
        }
        public List<enPriceOverView> GetPricingList(enPriceOverView objen)
        {
            return GetDALReference().GetPricingList(objen);
        }
        public List<enPriceOverView> GetPricingdataByProduct(enPriceOverView objen)
        {
            return GetDALReference().GetPricingdataByProduct(objen);
        }

        public List<enPriceOverView> GetIndustryLandingList(enPriceOverView objen)
        {
            return GetDALReference().GetIndustryLandingList(objen);
        }
        public List<enPriceOverView> BindFeedStockBasedonPriceoverview(enPriceOverView objen)
        {
            return GetDALReference().BindFeedStockBasedonPriceoverview(objen);
        }

        public List<enPriceOverView> GetProductStatus(enPriceOverView objen)
        {
            return GetDALReference().GetProductStatus(objen);
        }

        public List<enPriceOverView> BindParentproductBasedonChild(enPriceOverView objen)
        {
            return GetDALReference().BindParentproductBasedonChild(objen);
        }

        public List<enPriceOverView> GetAILandingPageBasedOnProduct(enPriceOverView objen)
        {
            return GetDALReference().GetAILandingPageBasedOnProduct(objen);
        }

        public List<enPriceOverView> GetAILandingPageBasedOnProductForUpload(enPriceOverView objen)
        {
            return GetDALReference().GetAILandingPageBasedOnProductForUpload(objen);
        }

        public List<enPriceOverView> UpdateStatusForAILandingPage(enPriceOverView objen)
        {
            return GetDALReference().UpdateStatusForAILandingPage(objen);
        }

        public List<enPriceOverView> UpdateAssociateStatusForAILandingPage(enPriceOverView objen)
        {
            return GetDALReference().UpdateAssociateStatusForAILandingPage(objen);
        }

        public List<enPriceOverView> UpdateConsultantStatusForAILandingPage(enPriceOverView objen)
        {
            return GetDALReference().UpdateConsultantStatusForAILandingPage(objen);
        }


        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enPriceOverView);
            }
            return _objDAL;
        }
    }
}
