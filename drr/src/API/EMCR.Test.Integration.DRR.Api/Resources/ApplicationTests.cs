﻿using EMCR.DRR.Managers.Intake;
using EMCR.DRR.Resources.Applications;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace EMCR.Tests.Integration.DRR.Resources
{
    public class ApplicationTests
    {
        private string TestPrefix = "autotest-dev";

        [Test]
        public async Task CanCreateEOIApplication()
        {
            var host = EMBC.Tests.Integration.DRR.Application.Host;
            var applicationRepository = host.Services.GetRequiredService<IApplicationRepository>();

            var originalApplication = CreateTestEOIApplication();
            var id = (await applicationRepository.Manage(new SubmitApplication { Application = originalApplication })).Id;
            id.ShouldNotBeEmpty();

            var newApplication = (await applicationRepository.Query(new ApplicationsQuery { ApplicationName = id })).Items.ShouldHaveSingleItem();
            newApplication.ProjectTitle.ShouldNotBeEmpty();
            //newApplication.Submitter.FirstName.ShouldBe(originalApplication.Submitter.FirstName);
            //newApplication.AdditionalContacts.Count().ShouldBe(originalApplication.AdditionalContacts.Count());
        }

        [Test]
        public async Task CanQueryApplications()
        {
            var host = EMBC.Tests.Integration.DRR.Application.Host;
            var applicationRepository = host.Services.GetRequiredService<IApplicationRepository>();

            var applications = (await applicationRepository.Query(new ApplicationsQuery { })).Items;
            applications.ShouldNotBeEmpty();
            //newApplication.Submitter.FirstName.ShouldBe(originalApplication.Submitter.FirstName);
            //newApplication.AdditionalContacts.Count().ShouldBe(originalApplication.AdditionalContacts.Count());
        }

        private Application CreateTestEOIApplication()
        {
            var uniqueSignature = TestPrefix + "-" + Guid.NewGuid().ToString().Substring(0, 4);
            return new Application
            {
                //Proponent Information
                ProponentType = ProponentType.LocalGovernment,
                ProponentName = $"{uniqueSignature}_applicant_name",
                Submitter = CreateTestContact(uniqueSignature),
                ProjectContact = CreateTestContact(uniqueSignature),
                AdditionalContact1 = CreateTestContact(uniqueSignature),
                //AdditionalContact2 = CreateTestContact(uniqueSignature),
                PartneringProponents = new[]
                {
                   new PartneringProponent { Name = "partner1" },
                   new PartneringProponent { Name = "partner2" },
                },

                //Project Information
                FundingStream = FundingStream.Stream1,
                ProjectTitle = $"{uniqueSignature}_projectTitle",
                ProjectType = ProjectType.New,
                ScopeStatement = "scope",
                RelatedHazards = new[]
                {
                    Hazards.Flood,
                    Hazards.Tsunami,
                    Hazards.Other
                },
                OtherHazardsDescription = "Other Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),

                //Funding Information
                EstimatedTotal = 1000,
                FundingRequest = 100,
                OtherFunding = new[]
                {
                    new FundingInformation
                    {
                        Name = "my $$$",
                        Amount = 100,
                        Type = FundingType.SelfFunding,
                    },
                    new FundingInformation
                    {
                        Name = "prov $$$",
                        Amount = 200,
                        Type = FundingType.Prov,
                    },
                },
                RemainingAmount = 600,
                IntendToSecureFunding = "Funding Reasons",

                //Location Information
                OwnershipDeclaration = true,
                OwnershipDescription = "owned",
                LocationDescription = "location description",

                //Project Detail
                RationaleForFunding = "rationale for funding",
                EstimatedPeopleImpacted = 12,
                CommunityImpact = "community impact",
                InfrastructureImpacted = new[] { "test1" },
                DisasterRiskUnderstanding = "helps many people",
                AdditionalBackgroundInformation = "additional background info",
                AddressRisksAndHazards = "fix risks",
                DRIFProgramGoalAlignment = "aligns with goals",
                AdditionalSolutionInformation = "additional solution info",
                RationaleForSolution = "rational for solution",

                //Engagement Plan
                FirstNationsEngagement = "Engagement Proposal",
                NeighbourEngagement = "engage with neighbours",
                AdditionalEngagementInformation = "additional engagement info",

                //Other Supporting Information
                ClimateAdaptation = "Climate Adaptation",
                OtherInformation = "Other Info",

                //Declaration
                FinancialAwarenessConfirmation = true,
                FOIPPAConfirmation = true,
                IdentityConfirmation = true
            };
        }

        private ContactDetails CreateTestContact(string uniqueSignature)
        {
            return new ContactDetails
            {
                FirstName = $"{uniqueSignature}_first",
                LastName = $"{uniqueSignature}_last",
                Email = "test@test.com",
                Phone = "604-123-4567",
                Department = "Position",
                Title = "Title"
            };
        }
    }


}
