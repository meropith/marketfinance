using System;
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public interface IApplicationService<in T> where T : IProduct
    {
        public IApplicationResult Process(ISellerApplication application, T product)
        {
            throw new NotImplementedException();
        }

        public int CheckResult(IApplicationResult ApplicationResult)
        {
            return (ApplicationResult.Success) ? ApplicationResult.ApplicationId ?? -1 : -1;
        }
        
    }
}