using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class CustomerDocumentBL
    {
        private readonly CustomerDocumentDAL _dal = new CustomerDocumentDAL();

        public List<CustomerDocument> GetCustomerDocumentsByCompany(int companyId)
        {
            return _dal.GetCustomerDocumentsByCompany(companyId);
        }

        public void InsertCustomerDocument(CustomerDocument model)
        {
            _dal.InsertCustomerDocument(model);
        }
    }
}
