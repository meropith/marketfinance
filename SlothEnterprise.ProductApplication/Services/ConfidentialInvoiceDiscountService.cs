using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ConfidentialInvoiceDiscountService : IApplicationService<ConfidentialInvoiceDiscount>
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountService(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
        }

        public IApplicationResult Process(ISellerApplication application, ConfidentialInvoiceDiscount product)
        {
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                StaticHelpers.Convert_ISellerCompanyData_to_CompanyDataRequest(application.CompanyData),
                product.TotalLedgerNetworth,
                product.AdvancePercentage,
                product.VatRate);

            return result;
        }
    }
}