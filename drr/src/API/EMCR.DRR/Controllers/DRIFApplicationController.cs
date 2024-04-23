﻿using System.ComponentModel;
using System.Net.Mime;
using System.Text.Json.Serialization;
using EMCR.DRR.Managers.Intake;
using EMCR.DRR.Resources.Applications;
using Microsoft.AspNetCore.Mvc;

namespace EMCR.DRR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class DRIFApplicationController : ControllerBase
    {
        private readonly ILogger<DRIFApplicationController> logger;
        private readonly IIntakeManager intakeManager;
        private readonly IApplicationRepository applicationRepository;


        public DRIFApplicationController(ILogger<DRIFApplicationController> logger, IIntakeManager intakeManager, IApplicationRepository applicationRepository)
        {
            this.logger = logger;
            this.intakeManager = intakeManager;
            this.applicationRepository = applicationRepository;
        }


        [HttpPost("EOI")]
        public async Task<ActionResult<ApplicationResult>> CreateEOIApplication(DrifEoiApplication application)
        {
            var id = await intakeManager.Handle(new DrifEoiApplicationCommand { application = application });
            return Ok(new ApplicationResult { Id = id });
        }
    }

    public class ApplicationResult
    {
        public required string Id { get; set; }
    }

    public class DrifEoiApplication
    {
        //Proponent Information
        public ProponentType ProponentType { get; set; }
        public required string ProponentName { get; set; }
        public required ContactDetails Submitter { get; set; }
        public required ContactDetails ProjectContact { get; set; }
        public required IEnumerable<ContactDetails> AdditionalContacts { get; set; }
        public required IEnumerable<string> PartneringProponents { get; set; }

        //Project Information
        public required FundingStream FundingStream { get; set; }
        public required string ProjectTitle { get; set; }
        public ProjectType ProjectType { get; set; }
        public required string ScopeStatement { get; set; }
        public required IEnumerable<Hazards> RelatedHazards { get; set; }
        public string? OtherHazardsDescription { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        //Funding Information
        public required decimal EstimatedTotal { get; set; }
        public required decimal FundingRequest { get; set; }
        public required IEnumerable<FundingInformation> OtherFunding { get; set; }
        public required decimal RemainingAmount { get; set; }
        public string? IntendToSecureFunding { get; set; }

        //Location Information
        public required bool OwnershipDeclaration { get; set; }
        public required string OwnershipDescription { get; set; }
        public required string LocationDescription { get; set; }

        //Project Detail
        public required string RationaleForFunding { get; set; }
        public required int EstimatedPeopleImpacted { get; set; }
        public required string CommunityImpact { get; set; }
        public required IEnumerable<string> InfrastructureImpacted { get; set; }
        public required string DisasterRiskUnderstanding { get; set; }
        public string? AdditionalBackgroundInformation { get; set; }
        public required string AddressRisksAndHazards { get; set; }
        public required string DRIFProgramGoalAlignment { get; set; }
        public string? AdditionalSolutionInformation { get; set; }
        public required string RationaleForSolution { get; set; }

        //Engagement Plan
        public required string FirstNationsEngagement { get; set; }
        public required string NeighbourEngagement { get; set; }
        public string? AdditionalEngagementInformation { get; set; }

        //Other Supporting Information
        public required string ClimateAdaptation { get; set; }
        public string? OtherInformation { get; set; }


        //Declaration
        public required bool IdentityConfirmation { get; set; }
        public required bool FOIPPAConfirmation { get; set; }
        public required bool FinancialAwarenessConfirmation { get; set; }
    }

    public class FundingInformation
    {
        public required string Name { get; set; }
        public required FundingType Type { get; set; }
        public required decimal Amount { get; set; }
        public string? OtherDescription { get; set; }

    }

    public class ContactDetails
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Title { get; set; }
        public required string Department { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProponentType
    {
        [Description("First Nation")]
        FirstNation,

        [Description("Local Government")]
        LocalGovernment,

        [Description("Regional District")]
        RegionalDistrict
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FundingStream
    {
        [Description("Foundational and Non-Structural")]
        Stream1,

        [Description("Structural")]
        Stream2
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProjectType
    {
        [Description("New Project")]
        New,

        [Description("New Phase of Existing Project")]
        Existing
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FundingType
    {
        [Description("Federal")]
        Fed,

        [Description("Federal/Provincial")]
        FedProv,

        [Description("Provincial")]
        Prov,

        [Description("Self-funded")]
        SelfFunding,

        [Description("Other Grants")]
        OtherGrants,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Hazards
    {
        [Description("Drought and water scarcity")]
        Drought,

        [Description("Extreme Temperature")]
        ExtremeTemperature,

        [Description("Flood")]
        Flood,

        [Description("Geohazards (e.g., avalanche, landslide)")]
        Geohazards,

        [Description("Sea Level Rise")]
        SeaLevelRise,

        [Description("Seismic")]
        Seismic,

        [Description("Tsunami")]
        Tsunami,

        [Description("Other")]
        Other,
    }
}
