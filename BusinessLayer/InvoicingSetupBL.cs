using DataLayer;
using EntityLayer;

namespace BusinessLayer
{
    // Business layer sitting between the controller and InvoicingSetupDAL.
    // (The site's older BusinessLayer/EntityLayer assemblies are precompiled
    // DLLs with no source in this project, so this new feature gets its own
    // small BL/DAL pair here instead of extending those.)
    public class InvoicingSetupBL
    {
        private readonly InvoicingSetupDAL _dal = new InvoicingSetupDAL();

        public InvoicingSetup GetInvoicingSetupByCompany(int companyId)
        {
            return _dal.GetInvoicingSetupByCompany(companyId);
        }

        public void SaveInvoicingSetup(InvoicingSetup model, string userName)
        {
            _dal.SaveInvoicingSetup(model, userName);
        }
    }
}
