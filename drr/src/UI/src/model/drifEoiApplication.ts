/**
 * Generated by orval v6.27.1 🍺
 * Do not edit manually.
 * DRR API
 * OpenAPI spec version: 1.0.0
 */
import type { ContactDetails } from './contactDetails';
import type { FundingStream } from './fundingStream';
import type { FundingInformation } from './fundingInformation';
import type { ProjectType } from './projectType';
import type { ProponentType } from './proponentType';
import type { Hazards } from './hazards';

export interface DrifEoiApplication {
  /** @nullable */
  additionalBackgroundInformation?: string;
  additionalContacts?: ContactDetails[];
  /** @nullable */
  additionalEngagementInformation?: string;
  /** @nullable */
  additionalSolutionInformation?: string;
  addressRisksAndHazards?: string;
  cfoConfirmation?: boolean;
  climateAdaptation?: string;
  disasterRiskUnderstanding?: string;
  drifProgramGoalAlignment?: string;
  endDate?: string;
  estimatedPeopleImpacted?: string;
  estimatedTotal?: number;
  firstNationsEngagement?: string;
  foippaConfirmation?: boolean;
  fundingRequest?: number;
  fundingStream?: FundingStream;
  identityConfirmation?: boolean;
  infrastructureImpacted?: string;
  /** @nullable */
  intendToSecureFunding?: string;
  locationDescription?: string;
  neighbourEngagement?: string;
  otherFunding?: FundingInformation[];
  /** @nullable */
  otherHazardsDescription?: string;
  /** @nullable */
  otherInformation?: string;
  ownershipDeclaration?: boolean;
  ownershipDescription?: string;
  partneringProponents?: string[];
  projectContact?: ContactDetails;
  projectTitle?: string;
  projectType?: ProjectType;
  proponentName?: string;
  proponentType?: ProponentType;
  rationaleForFunding?: string;
  rationaleForSolution?: string;
  relatedHazards?: Hazards[];
  remainingAmount?: number;
  startDate?: string;
  submitter?: ContactDetails;
  scopeStatement?: string;
  communityImpact?: string;
}
