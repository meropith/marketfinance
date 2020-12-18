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
    public class BusinessLoansProductTests : IBusinessLoansService
    {
        private readonly List<Application> _applications = new List<Application>();
        private readonly List<IApplicationResult> _results = new List<IApplicationResult>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Should_SubmitApplication_SuccessFuly()
        {
            var product = _fixture.Create<BusinessLoans>();
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
                    LoansRequest = new
                    {
                        product.InterestRatePerAnnum,
                        product.LoanAmount
                    }
                });
        }

        [Fact]
        public void Should_ReturnApplicationId_IfApplication_IsSuccessful()
        {
            var expected = _fixture.Create<int>();

            _results.Add(new TestApplicationResult
            {
                ApplicationId = expected,
                Success = true
            });

            var applicationId = SubmitApplication();

            applicationId.Should().Be(expected);
        }

        [Fact]
        public void Should_Return_MinusOne_IfApplication_IsUnsuccessful()
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

        private int SubmitApplication(ISellerApplication application)
        {
            var productApplicationService = new ProductApplicationService(null, null, this);

            return productApplicationService.SubmitApplicationFor(application);
        }

        private int SubmitApplication()
        {
            var application = new SellerApplication
            {
                Product = _fixture.Create<BusinessLoans>(),
                CompanyData = _fixture.Create<SellerCompanyData>()
            };

            return SubmitApplication(application);
        }

        private class Application
        {
            public CompanyDataRequest CompanyDataRequest { get; }
            public LoansRequest LoansRequest { get; }

            public Application(CompanyDataRequest companyDataRequest, LoansRequest loansRequest)
            {
                CompanyDataRequest = companyDataRequest;
                LoansRequest = loansRequest;
            }           
        }

        public IApplicationResult SubmitApplicationFor(CompanyDataRequest applicantData, LoansRequest businessLoans)
        {
            _applications.Add(new Application(applicantData, businessLoans));

            return _results.Count > 0 ? _results[0] : _fixture.Create<TestApplicationResult>();
        }

        #endregion
    }
}