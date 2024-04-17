﻿using EMCR.DRR.Managers.Intake;

namespace EMCR.DRR.Resources.Applications
{
    public interface IApplicationRepository
    {
        Task<ManageApplicationCommandResult> Manage(ManageApplicationCommand cmd);

        Task<ApplicationQueryResult> Query(ApplicationsQuery query);
    }

    public abstract class ManageApplicationCommand
    { }

    public class ManageApplicationCommandResult
    {
        public required string Id { get; set; }
    }

    public abstract class ApplicationQuery
    { }

    public class ApplicationQueryResult
    {
        public IEnumerable<Application> Items { get; set; } = Array.Empty<Application>();
    }

    public class ApplicationsQuery : ApplicationQuery
    {
        public string? ApplicationId { get; set; }
    }

    public class SubmitApplication : ManageApplicationCommand
    {
        public required Application Application { get; set; }
    }

    public enum ApplicantTypeOptionSet
    {
        FirstNation = 172580000,
        LocalGovernment = 172580001,
        RegionalDistrict = 172580002
    }

    public enum ProjectTypeOptionSet
    {
        New = 172580000,
        Existing = 172580001,
    }


    public enum FundingTypeOptionSet
    {
        Fed = 172580000,
        FedProv = 172580001,
        Prov = 172580002,
        SelfFunding = 172580003,
        Other = 172580004,
    }

    public enum HazardsOptionSet
    {
        Drought = 172580000,
        Erosion = 172580001,
        ExtremeTemperature = 172580002,
        Flood = 172580003,
        Geohazards = 172580004,
        SeaLevelRise = 172580005,
        Seismic = 172580006,
        Storm = 172580007,
        Tsunami = 172580008,
        Other = 172580999,
    }

    public enum DRRTwoOptions
    {
        Yes = 172580000,
        No = 172580001
    }
}
