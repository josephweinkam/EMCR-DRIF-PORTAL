﻿using AutoMapper;
using EMCR.DRR.Managers.Intake;
using Microsoft.Dynamics.CRM;

namespace EMCR.DRR.Resources.Applications
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<Application, drr_application>(MemberList.None)
                .ForMember(dest => dest.drr_primaryapplicanttype, opt => opt.MapFrom(src => (int?)Enum.Parse<ApplicantTypeOptionSet>(src.ApplicantType.ToString())))
                .ForMember(dest => dest.drr_name, opt => opt.MapFrom(src => src.ApplicantName))
                .ForMember(dest => dest.drr_PrimaryApplicant, opt => opt.MapFrom(src => new account { name = src.ApplicantName }))
                .ForMember(dest => dest.drr_SubmitterContactInformation, opt => opt.MapFrom(src => src.Submitter))
                .ForMember(dest => dest.drr_application_contact_Application, opt => opt.MapFrom(src => src.ProjectContacts))
                .ForMember(dest => dest.drr_projecttitle, opt => opt.MapFrom(src => src.ProjectTitle))
                .ForMember(dest => dest.drr_projecttype, opt => opt.MapFrom(src => (int?)Enum.Parse<ProjectTypeOptionSet>(src.ProjectType.ToString())))
                .ForMember(dest => dest.drr_hazards, opt => opt.MapFrom(src => string.Join(",", src.RelatedHazards.Select(h => (int?)Enum.Parse<HazardsOptionSet>(h.ToString())))))
                .ForMember(dest => dest.drr_reasonswhyotherselectedforhazards, opt => opt.MapFrom(src => src.OtherHazardsDescription))
                .ForMember(dest => dest.drr_anticipatedprojectstartdate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.drr_anticipatedprojectenddate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.drr_driffundingrequest, opt => opt.MapFrom(src => src.FundingRequest))
                .ForMember(dest => dest.drr_application_fundingsource_Application, opt => opt.MapFrom(src => src.OtherFunding))
                .ForMember(dest => dest.drr_unfundedamount, opt => opt.MapFrom(src => src.UnfundedAmount))
                .ForMember(dest => dest.drr_reasonstosecurefunding, opt => opt.MapFrom(src => src.ReasonsToSecureFunding))
                .ForMember(dest => dest.drr_totalfundingsources, opt => opt.MapFrom(src => src.TotalFunding))
                .ForMember(dest => dest.drr_ownershipdeclaration, opt => opt.MapFrom(src => src.OwnershipDeclaration ? DRRTwoOptions.Yes : DRRTwoOptions.No))
                .ForMember(dest => dest.drr_locationdescription, opt => opt.MapFrom(src => src.LocationInformation.Description))
                .ForMember(dest => dest.drr_sizeofprojectarea, opt => opt.MapFrom(src => src.LocationInformation.Area))
                .ForMember(dest => dest.drr_landuseorownership, opt => opt.MapFrom(src => src.LocationInformation.Ownership))
                .ForMember(dest => dest.drr_backgroundforfundingrequest, opt => opt.MapFrom(src => src.BackgroundDescription))
                .ForMember(dest => dest.drr_rationaleforfundingrequest, opt => opt.MapFrom(src => src.RationaleForFunding))
                .ForMember(dest => dest.drr_proposedsolution, opt => opt.MapFrom(src => src.ProposedSolution))
                .ForMember(dest => dest.drr_rationalforproposedsolution, opt => opt.MapFrom(src => src.RationaleForSolution))
                .ForMember(dest => dest.drr_engagementwithfirstnationsorindigenousorg, opt => opt.MapFrom(src => src.EngagementProposal))
                .ForMember(dest => dest.drr_climateadaptation, opt => opt.MapFrom(src => src.ClimateAdaptation))
                .ForMember(dest => dest.drr_otherrelevantinformation, opt => opt.MapFrom(src => src.OtherInformation))
                .ForMember(dest => dest.drr_identityconfirmation, opt => opt.MapFrom(src => src.IdentityConfirmation ? DRRTwoOptions.Yes : DRRTwoOptions.No))
                .ForMember(dest => dest.drr_foippaconfirmation, opt => opt.MapFrom(src => src.FOIPPAConfirmation ? DRRTwoOptions.Yes : DRRTwoOptions.No))
                .ForMember(dest => dest.drr_cfoconfirmationoffundsstatement, opt => opt.MapFrom(src => src.CFOConfirmation ? DRRTwoOptions.Yes : DRRTwoOptions.No))
                .ReverseMap()
                .ValidateMemberList(MemberList.Destination)
                .ForMember(dest => dest.ApplicantName, opt => opt.MapFrom(src => src.drr_name))
                .ForMember(dest => dest.Submitter, opt => opt.MapFrom(src => src.drr_SubmitterContactInformation))
                .ForMember(dest => dest.ProjectContacts, opt => opt.MapFrom(src => src.drr_application_contact_Application))
                .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.drr_projecttitle))
                .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.drr_projecttype))
                .ForMember(dest => dest.RelatedHazards, opt => opt.MapFrom(src => src.drr_hazards.Split(',', StringSplitOptions.None).Select(h => Enum.Parse<HazardsOptionSet>(h).ToString())))
                .ForMember(dest => dest.OtherHazardsDescription, opt => opt.MapFrom(src => src.drr_reasonswhyotherselectedforhazards))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.drr_anticipatedprojectstartdate.HasValue ? src.drr_anticipatedprojectstartdate.Value.UtcDateTime : new DateTime()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.drr_anticipatedprojectenddate.HasValue ? src.drr_anticipatedprojectenddate.Value.UtcDateTime : new DateTime()))
                .ForMember(dest => dest.FundingRequest, opt => opt.MapFrom(src => src.drr_driffundingrequest))
                .ForMember(dest => dest.OtherFunding, opt => opt.MapFrom(src => src.drr_application_fundingsource_Application))
                .ForMember(dest => dest.UnfundedAmount, opt => opt.MapFrom(src => src.drr_unfundedamount))
                .ForMember(dest => dest.ReasonsToSecureFunding, opt => opt.MapFrom(src => src.drr_reasonstosecurefunding))
                .ForMember(dest => dest.TotalFunding, opt => opt.MapFrom(src => src.drr_totalfundingsources))
                .ForMember(dest => dest.OwnershipDeclaration, opt => opt.MapFrom(src => src.drr_ownershipdeclaration == (int)DRRTwoOptions.Yes))
                .ForPath(dest => dest.LocationInformation.Description, opt => opt.MapFrom(src => src.drr_locationdescription))
                .ForPath(dest => dest.LocationInformation.Area, opt => opt.MapFrom(src => src.drr_sizeofprojectarea))
                .ForPath(dest => dest.LocationInformation.Ownership, opt => opt.MapFrom(src => src.drr_landuseorownership))
                .ForMember(dest => dest.BackgroundDescription, opt => opt.MapFrom(src => src.drr_backgroundforfundingrequest))
                .ForMember(dest => dest.RationaleForFunding, opt => opt.MapFrom(src => src.drr_rationaleforfundingrequest))
                .ForMember(dest => dest.ProposedSolution, opt => opt.MapFrom(src => src.drr_proposedsolution))
                .ForMember(dest => dest.RationaleForSolution, opt => opt.MapFrom(src => src.drr_rationalforproposedsolution))
                .ForMember(dest => dest.EngagementProposal, opt => opt.MapFrom(src => src.drr_engagementwithfirstnationsorindigenousorg))
                .ForMember(dest => dest.ClimateAdaptation, opt => opt.MapFrom(src => src.drr_climateadaptation))
                .ForMember(dest => dest.OtherInformation, opt => opt.MapFrom(src => src.drr_otherrelevantinformation))
                .ForMember(dest => dest.IdentityConfirmation, opt => opt.MapFrom(src => src.drr_identityconfirmation == (int)DRRTwoOptions.Yes))
                .ForMember(dest => dest.FOIPPAConfirmation, opt => opt.MapFrom(src => src.drr_foippaconfirmation == (int)DRRTwoOptions.Yes))
                .ForMember(dest => dest.CFOConfirmation, opt => opt.MapFrom(src => src.drr_cfoconfirmationoffundsstatement == (int)DRRTwoOptions.Yes))

            ;

            CreateMap<FundingInformation, drr_fundingsource>()
                .ForMember(dest => dest.drr_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.drr_typeoffunding, opt => opt.MapFrom(src => (int?)Enum.Parse<FundingTypeOptionSet>(src.Type.ToString())))
                .ForMember(dest => dest.drr_amount, opt => opt.MapFrom(src => src.Amount))
                .ReverseMap()
                .ValidateMemberList(MemberList.Destination)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.drr_name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.drr_typeoffunding))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.drr_amount))
            ;

            CreateMap<ContactDetails, contact>()
                .ForMember(dest => dest.firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.jobtitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.drr_phonenumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.emailaddress1, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ValidateMemberList(MemberList.Destination)
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.lastname))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.jobtitle))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.department))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.drr_phonenumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.emailaddress1))
            ;
        }
    }
}
