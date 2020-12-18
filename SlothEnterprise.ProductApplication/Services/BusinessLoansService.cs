using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class BusinessLoansService : IApplicationService<BusinessLoans>
    {
        private readonly IBusinessLoansService _service;

        public BusinessLoansService(IBusinessLoansService service)
        {
            _service = service;
        }
        

        public IApplicationResult Process(ISellerApplication application, BusinessLoans product)
        {
            var result = _service.SubmitApplicationFor(
                StaticHelpers.Convert_ISellerCompanyData_to_CompanyDataRequest(application.CompanyData),
                new LoansRequest
                {
                    InterestRatePerAnnum = product.InterestRatePerAnnum,
                    LoanAmount = product.LoanAmount
                });

            return result;
        }
    }
}