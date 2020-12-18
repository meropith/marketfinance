using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public static class StaticHelpers
    {
        public static CompanyDataRequest Convert_ISellerCompanyData_to_CompanyDataRequest(ISellerCompanyData applicationCompanyData)
        {
            return new CompanyDataRequest
            {
                CompanyFounded = applicationCompanyData.Founded,
                CompanyNumber = applicationCompanyData.Number,
                CompanyName = applicationCompanyData.Name,
                DirectorName = applicationCompanyData.DirectorName
            };
        }
    }
}