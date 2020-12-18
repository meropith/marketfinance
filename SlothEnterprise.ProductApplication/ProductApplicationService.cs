using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ApplicationRouter _router;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _router = new ApplicationRouter(selectInvoiceService, confidentialInvoiceWebService,
                businessLoansService);
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            return _router.Call(application);
        }
    }
}
