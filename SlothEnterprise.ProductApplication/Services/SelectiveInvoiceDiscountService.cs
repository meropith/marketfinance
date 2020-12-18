using System.Globalization;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class SelectiveInvoiceDiscountService : IApplicationService<SelectiveInvoiceDiscount>
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountService(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }
        
        public IApplicationResult Process(ISellerApplication application, SelectiveInvoiceDiscount product)
        {
            int result = _selectInvoiceService.SubmitApplicationFor(
                application.CompanyData.Number.ToString(CultureInfo.InvariantCulture),
                product.InvoiceAmount,
                product.AdvancePercentage);

            var appResult = new ApplicationResult
            {
                ApplicationId = result,
                Errors = null,
                Success = true
            };


            return appResult;
        }
    }
}