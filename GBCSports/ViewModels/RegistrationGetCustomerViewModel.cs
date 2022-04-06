using GBCSports.Models;

namespace GBCSports.ViewModels
{
    public class RegistrationGetCustomerViewModel
    {

        public List<string> CustomerList { get; set; }

        public List<int> CustomerIdList { get; set; }

        public List<Product> RegisteredProductList { get; set; }
        public int CustomerId { get; set; }

        public List<Product> ProductList { get; set; }

        public int RegisteredProductId { get; set; }

        //public Customer Customer { get; set; }



    }
}
