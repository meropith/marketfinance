using System;
using System.Collections.Generic;
using System.Linq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Services.Base;

namespace SlothEnterprise.ProductApplication
{
    public class ApplicationRouter
    {
        private readonly List<object> _supportedServices;
        readonly IApplicationService<Product> DefaultIApplicationServiceFunctions = new BaseService();

        public ApplicationRouter(ISelectInvoiceService selectInvoiceService,
            IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _supportedServices = new List<object>
            {
                new SelectiveInvoiceDiscountService(selectInvoiceService),
                new ConfidentialInvoiceDiscountService(confidentialInvoiceWebService),
                new BusinessLoansService(businessLoansService)
            };           
        }

        public int Call(ISellerApplication application)
        {
            
            var supportedService = _supportedServices
                .FirstOrDefault(x => x.GetType().Name.Contains(application.Product.GetType().Name));

            if (supportedService == null)
            {
                throw new InvalidOperationException();
            }

             IApplicationResult appResult = (((dynamic)supportedService).Process(application, (dynamic)application.Product));
          
            return DefaultIApplicationServiceFunctions.CheckResult(appResult);
        }
    }
}