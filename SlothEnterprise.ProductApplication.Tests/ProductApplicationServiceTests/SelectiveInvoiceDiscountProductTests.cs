using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AutoFixture;
using FluentAssertions;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.ProductApplicationServiceTests
{
    public class SelectiveInvoiceDiscountProductTests : ISelectInvoiceService
    {
        private readonly List<Application> _applications = new List<Application>();
        private readonly List<int> _returnApplicationIds = new List<int>();
        
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Should_SubmitApplication_SuccessFuly()
        {
            var product = _fixture.Create<SelectiveInvoiceDiscount>();
            var application = new SellerApplication
            {
                Product = product,
                CompanyData = _fixture.Create<SellerCompanyData>()
            };
            
            SubmitApplication(application);

            _applications.Should().BeEquivalentTo(new Application(
                    application.CompanyData.Number.ToString(CultureInfo.InvariantCulture),
                    product.InvoiceAmount,
                    product.AdvancePercentage
                ));
        }
        
        [Fact]
        public void Should_ReturnApplicationId_IfApplication_IsSuccessful()
        {
            var application = new SellerApplication
            {
                Product = _fixture.Create<SelectiveInvoiceDiscount>(),
                CompanyData = _fixture.Create<SellerCompanyData>()
            };
            var expectedApplicationId = _fixture.Create<int>();
            _returnApplicationIds.Add(expectedApplicationId);
            
            var applicationId = SubmitApplication(application);
            
            applicationId.Should().Be(expectedApplicationId);
        }                     

        private int SubmitApplication(SellerApplication application)
        {
            var productApplicationService = new ProductApplicationService(this, null, null);

            var applicationId = productApplicationService.SubmitApplicationFor(application);
            return applicationId;
        }


        public int SubmitApplicationFor(string companyNumber, decimal invoiceAmount, decimal advancePercentage)
        {
            _applications.Add(new Application(companyNumber, invoiceAmount, advancePercentage));

            return _returnApplicationIds.Count > 0
                ? _returnApplicationIds.FirstOrDefault() : _fixture.Create<int>();
        }

        private class Application
        {
            public string CompanyNumber { get; }
            public decimal InvoiceAmount { get; }
            public decimal AdvancePercentage { get; }

            public Application(string companyNumber, decimal invoiceAmount, decimal advancePercentage)
            {
                CompanyNumber = companyNumber;
                InvoiceAmount = invoiceAmount;
                AdvancePercentage = advancePercentage;
            }
        }
    }
}