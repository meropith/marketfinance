using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.ProductApplicationServiceTests
{
    public class ConfidentialInvoiceDiscountProductTests : IConfidentialInvoiceService
    {
        private readonly List<Application> _applications = new List<Application>();
        private readonly List<IApplicationResult> _results = new List<IApplicationResult>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Should_SubmitApplication_Successfully()
        {
            var product = _fixture.Create<ConfidentialInvoiceDiscount>();
            var application = new SellerApplication
            {
                Product = product,
                CompanyData = _fixture.Create<SellerCompanyData>()
            };

            SubmitApplication(application);

            _applications.Should().BeEquivalentTo(new 
            {
                    CompanyDataRequest = new
                    {
                        CompanyName = application.CompanyData.Name,
                        CompanyNumber = application.CompanyData.Number,
                        application.CompanyData.DirectorName,
                        CompanyFounded = application.CompanyData.Founded
                    },
                    product.TotalLedgerNetworth,
                    product.AdvancePercentage,
                    product.VatRate
                });
        }

        [Fact]
        public void Should_ReturnApplicationId_IfApplication_IsSuccessful()
        {
            var expectedApplicationId = _fixture.Create<int>();
            
            _results.Add(new TestApplicationResult
            {
                ApplicationId = expectedApplicationId,
                Success = true
            });

            var applicationId = SubmitApplication();

            applicationId.Should().Be(expectedApplicationId);
        }
               
        [Fact]
        public void Should_ReturnMinusOne_IfApplication_IsUnsuccessful()
        {
            _results.Add(new TestApplicationResult
            {
                ApplicationId = _fixture.Create<int>(),
                Success = false
            });

            var applicationId = SubmitApplication();

            applicationId.Should().Be(-1);
        }

        #region Private Helper functions

        #endregion

        private int SubmitApplication(SellerApplication application)
        {
            var productApplicationService = new ProductApplicationService(null, this, null);

            return productApplicationService.SubmitApplicationFor(application);
        }
        
        private int SubmitApplication()
        {
            var application = new SellerApplication
            {
                Product = _fixture.Create<ConfidentialInvoiceDiscount>(),
                CompanyData = _fixture.Create<SellerCompanyData>()
            };

            return SubmitApplication(application);
        }


        public IApplicationResult SubmitApplicationFor(CompanyDataRequest applicantData,
            decimal invoiceLedgerTotalValue,
            decimal advantagePercentage, decimal vatRate)
        {
            _applications.Add(new Application(applicantData, invoiceLedgerTotalValue, advantagePercentage,
                vatRate));

            return _results.Count > 0 ? _results[0] : _fixture.Create<TestApplicationResult>();
        }

        private class Application
        {
            public CompanyDataRequest CompanyDataRequest { get; }
            public decimal TotalLedgerNetworth { get; }
            public decimal AdvancePercentage { get; }
            public decimal VatRate { get; }

            public Application(CompanyDataRequest companyDataRequest, decimal totalLedgerNetworth,
                decimal advancePercentage, decimal vatRate)
            {
                CompanyDataRequest = companyDataRequest;
                TotalLedgerNetworth = totalLedgerNetworth;
                AdvancePercentage = advancePercentage;
                VatRate = vatRate;
            }
        }
        
    }
}