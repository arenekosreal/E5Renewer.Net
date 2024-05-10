using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.Graph.Models.IdentityGovernance;
using Microsoft.Graph.Models.Search;
using Microsoft.Graph.Models.Security;
using Microsoft.Graph.Models;
using Microsoft.Graph;

namespace E5Renewer.Processor.GraphAPIs
{
    /// <summary>Functions container for calling msgraph apis.</summary>
    [APIContainer]
    public static class Get
    {
        #region API
        /// <summary>msgraph api implemention.</summary>
        [API("AgreementAcceptances.Get")]
        public static async Task<AgreementAcceptanceCollectionResponse?> GetAgreementAcceptance(GraphServiceClient client) => await client.AgreementAcceptances.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Admin.Get")]
        public static async Task<Admin?> GetAdmin(GraphServiceClient client) => await client.Admin.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Agreements.Get")]
        public static async Task<AgreementCollectionResponse?> GetAgreements(GraphServiceClient client) => await client.Agreements.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("AppCatalogs.Get")]
        public static async Task<AppCatalogs?> GetAppCatalogs(GraphServiceClient client) => await client.AppCatalogs.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("ApplicationTemplates.Get")]
        public static async Task<ApplicationTemplateCollectionResponse?> GetApplicationTemplates(GraphServiceClient client) => await client.ApplicationTemplates.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Applications.Get")]
        public static async Task<ApplicationCollectionResponse?> GetApplications(GraphServiceClient client) => await client.Applications.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("AuditLogs.Get")]
        public static async Task<AuditLogRoot?> GetAuditLogs(GraphServiceClient client) => await client.AuditLogs.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("AuthenticationMethodConfigurations.Get")]
        public static async Task<AuthenticationMethodConfigurationCollectionResponse?> GetAuthenticationMethodConfigurations(GraphServiceClient client) => await client.AuthenticationMethodConfigurations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("AuthenticationMethodsPolicy.Get")]
        public static async Task<AuthenticationMethodsPolicy?> GetAuthenticationMethodsPolicy(GraphServiceClient client) => await client.AuthenticationMethodsPolicy.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("CertificateBasedAuthConfiguration.Get")]
        public static async Task<CertificateBasedAuthConfigurationCollectionResponse?> GetCertificateBasedAuthConfiguration(GraphServiceClient client) => await client.CertificateBasedAuthConfiguration.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Chats.Get")]
        public static async Task<ChatCollectionResponse?> GetChats(GraphServiceClient client) => await client.Chats.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Communications.Get")]
        public static async Task<CloudCommunications?> GetCommunications(GraphServiceClient client) => await client.Communications.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Compliance.Get")]
        public static async Task<Compliance?> GetCompliance(GraphServiceClient client) => await client.Compliance.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Connections.Get")]
        public static async Task<ExternalConnectionCollectionResponse?> GetConnections(GraphServiceClient client) => await client.Connections.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Contacts.Get")]
        public static async Task<OrgContactCollectionResponse?> GetContacts(GraphServiceClient client) => await client.Contacts.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DataPolicyOperations.Get")]
        public static async Task<DataPolicyOperationCollectionResponse?> GetDataPolicyOperations(GraphServiceClient client) => await client.DataPolicyOperations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DeviceAppManagement.Get")]
        public static async Task<DeviceAppManagement?> GetDeviceAppManagement(GraphServiceClient client) => await client.DeviceAppManagement.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DeviceManagement.Get")]
        public static async Task<DeviceManagement?> GetDeviceManagement(GraphServiceClient client) => await client.DeviceManagement.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Devices.Get")]
        public static async Task<DeviceCollectionResponse?> GetDevies(GraphServiceClient client) => await client.Devices.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Direcory.Get")]
        public static async Task<DirectoryObject1?> GetDirecory(GraphServiceClient client) => await client.Directory.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DirectoryObjects.Get")]
        public static async Task<DirectoryObjectCollectionResponse?> GetDirectoryObjects(GraphServiceClient client) => await client.DirectoryObjects.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DirectoryRoleTemplates.Get")]
        public static async Task<DirectoryRoleTemplateCollectionResponse?> GetDirectoryRoleTemplates(GraphServiceClient client) => await client.DirectoryRoleTemplates.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DirectoryRoles.Get")]
        public static async Task<DirectoryRoleCollectionResponse?> GetDirectoryRoles(GraphServiceClient client) => await client.DirectoryRoles.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("DomainDnsRecords.Get")]
        public static async Task<DomainDnsRecordCollectionResponse?> GetDomainDnsRecords(GraphServiceClient client) => await client.DomainDnsRecords.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Domains.Get")]
        public static async Task<DomainCollectionResponse?> GetDomains(GraphServiceClient client) => await client.Domains.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Drives.Get")]
        public static async Task<DriveCollectionResponse?> GetDrives(GraphServiceClient client) => await client.Drives.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Education.Get")]
        public static async Task<EducationRoot?> GetEducation(GraphServiceClient client) => await client.Education.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("EmployeeExperience.Get")]
        public static async Task<EmployeeExperience?> GetEmployeeExperience(GraphServiceClient client) => await client.EmployeeExperience.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("External.Get")]
        public static async Task<External?> GetExternal(GraphServiceClient client) => await client.External.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("FilterOperators.Get")]
        public static async Task<FilterOperatorSchemaCollectionResponse?> GetFilterOperators(GraphServiceClient client) => await client.FilterOperators.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Functions.Get")]
        public static async Task<AttributeMappingFunctionSchemaCollectionResponse?> GetFunctions(GraphServiceClient client) => await client.Functions.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("GroupLifecyclePolicies.Get")]
        public static async Task<GroupLifecyclePolicyCollectionResponse?> GetGroupLifecyclePolicies(GraphServiceClient client) => await client.GroupLifecyclePolicies.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("GroupSettingTemplates.Get")]
        public static async Task<GroupSettingTemplateCollectionResponse?> GetGroupSettingTemplates(GraphServiceClient client) => await client.GroupSettingTemplates.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("GroupSetings.Get")]
        public static async Task<GroupSettingCollectionResponse?> GetGroupSettings(GraphServiceClient client) => await client.GroupSettings.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Groups.Get")]
        public static async Task<GroupCollectionResponse?> GetGroups(GraphServiceClient client) => await client.Groups.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Identity.Get")]
        public static async Task<IdentityContainer?> GetIdentity(GraphServiceClient client) => await client.Identity.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("IdentityGovernance.Get")]
        public static async Task<IdentityGovernance?> GetIdentityGovernance(GraphServiceClient client) => await client.IdentityGovernance.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("IdentityProtection.Get")]
        public static async Task<IdentityProtectionRoot?> GetIdentityProtection(GraphServiceClient client) => await client.IdentityProtection.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [Obsolete("GraphServiceClient.IdentityProviders.GetAsync is obsolete.")]
        [API("IdentityProviders.Get")]
        public static async Task<IdentityProviderCollectionResponse?> GetIdentityProviders(GraphServiceClient client) => await client.IdentityProviders.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("InformationProtecion.Get")]
        public static async Task<InformationProtection?> GetInfomationProtecion(GraphServiceClient client) => await client.InformationProtection.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Invitations.Get")]
        public static async Task<InvitationCollectionResponse?> GetInvitations(GraphServiceClient client) => await client.Invitations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("OAuth2PermissionGrants.Get")]
        public static async Task<OAuth2PermissionGrantCollectionResponse?> GetOAuth2PermissionGrants(GraphServiceClient client) => await client.Oauth2PermissionGrants.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Organization.Get")]
        public static async Task<OrganizationCollectionResponse?> GetOrganization(GraphServiceClient client) => await client.Organization.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("PermissionGrants.Get")]
        public static async Task<ResourceSpecificPermissionGrantCollectionResponse?> GetPermissionGrants(GraphServiceClient client) => await client.PermissionGrants.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Places.Count.Get")]
        public static async Task<int?> GetPlacesCount(GraphServiceClient client) => await client.Places.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Places.GraphRoom.Get")]
        public static async Task<RoomCollectionResponse?> GetPlacesGraphRoom(GraphServiceClient client) => await client.Places.GraphRoom.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Places.GraphRoom.Count.Get")]
        public static async Task<int?> GetPlacesGraphRoomCount(GraphServiceClient client) => await client.Places.GraphRoom.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Places.GraphRoomList.Get")]
        public static async Task<RoomListCollectionResponse?> GetPlacesGraphRoomList(GraphServiceClient client) => await client.Places.GraphRoomList.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Places.GraphRoomList.Count.Get")]
        public static async Task<int?> GetPlacesGraphRoomListCount(GraphServiceClient client) => await client.Places.GraphRoomList.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Get")]
        public static async Task<Planner?> GetPlanner(GraphServiceClient client) => await client.Planner.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Buckets.Get")]
        public static async Task<PlannerBucketCollectionResponse?> GetPlannerBuckets(GraphServiceClient client) => await client.Planner.Buckets.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Buckets.Count.Get")]
        public static async Task<int?> GetPlannerBucketsC(GraphServiceClient client) => await client.Planner.Buckets.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Plans.Get")]
        public static async Task<PlannerPlanCollectionResponse?> GetPlannerPlans(GraphServiceClient client) => await client.Planner.Plans.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Plans.Count.Get")]
        public static async Task<int?> GetPlannerPlansCount(GraphServiceClient client) => await client.Planner.Plans.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Tasks.Get")]
        public static async Task<PlannerTaskCollectionResponse?> GetPlannerTasks(GraphServiceClient client) => await client.Planner.Tasks.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Planner.Tasks.Count.Get")]
        public static async Task<int?> GetPlannerTasksCount(GraphServiceClient client) => await client.Planner.Tasks.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Policies.Get")]
        public static async Task<PolicyRoot?> GetPolicies(GraphServiceClient client) => await client.Policies.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Print.Get")]
        public static async Task<Print?> GetPrint(GraphServiceClient client) => await client.Print.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Privacy.Get")]
        public static async Task<Privacy?> GetPrivacy(GraphServiceClient client) => await client.Privacy.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Reports.Get")]
        public static async Task<ReportRoot?> GetReports(GraphServiceClient client) => await client.Reports.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("RoleManagement.Get")]
        public static async Task<RoleManagement?> GetRoleManagement(GraphServiceClient client) => await client.RoleManagement.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("SchemaExtensions.Get")]
        public static async Task<SchemaExtensionCollectionResponse?> GetSchemaExtensions(GraphServiceClient client) => await client.SchemaExtensions.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("ScopedRoleMemberships.Get")]
        public static async Task<ScopedRoleMembershipCollectionResponse?> GetScopedRoleMemberships(GraphServiceClient client) => await client.ScopedRoleMemberships.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Get")]
        public static async Task<SearchEntity?> GetSearch(GraphServiceClient client) => await client.Search.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Acronyms.Get")]
        public static async Task<AcronymCollectionResponse?> GetSearchAcronyms(GraphServiceClient client) => await client.Search.Acronyms.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Acronyms.Count.Get")]
        public static async Task<int?> GetSearchAcronymsCount(GraphServiceClient client) => await client.Search.Acronyms.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Bookmarks.Get")]
        public static async Task<BookmarkCollectionResponse?> GetSearchBookmarks(GraphServiceClient client) => await client.Search.Bookmarks.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Bookmarks.Count.Get")]
        public static async Task<int?> GetSearchBookmarksCount(GraphServiceClient client) => await client.Search.Bookmarks.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Qnas.Get")]
        public static async Task<QnaCollectionResponse?> GetSearchQnas(GraphServiceClient client) => await client.Search.Qnas.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Search.Qnas.Count.Get")]
        public static async Task<int?> GetSearhQnasCount(GraphServiceClient client) => await client.Search.Qnas.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Get")]
        public static async Task<Security?> GetSecurity(GraphServiceClient client) => await client.Security.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Alerts.Get")]
        public static async Task<Microsoft.Graph.Models.AlertCollectionResponse?> GetSecurityCount(GraphServiceClient client) => await client.Security.Alerts.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Alerts.Count.Get")]
        public static async Task<int?> GetSecurityAlertsCount(GraphServiceClient client) => await client.Security.Alerts.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Alerts_v2.Get")]
        public static async Task<Microsoft.Graph.Models.Security.AlertCollectionResponse?> GetSecurityAlertsV2(GraphServiceClient client) => await client.Security.Alerts_v2.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Alerts_v2.Count.Get")]
        public static async Task<int?> GetSecurityAlertsV2Count(GraphServiceClient client) => await client.Security.Alerts_v2.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Get")]
        public static async Task<AttackSimulationRoot?> GetSecurityAttackSimulation(GraphServiceClient client) => await client.Security.AttackSimulation.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.EndUserNotifications.Get")]
        public static async Task<EndUserNotificationCollectionResponse?> GetSecurityAttackSimulationEndUserNotifications(GraphServiceClient client) => await client.Security.AttackSimulation.EndUserNotifications.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.EndUserNotifications.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationEndUserNotificationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.EndUserNotifications.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.LandingPages.Get")]
        public static async Task<LandingPageCollectionResponse?> GetSecurityAttackSimulationLandingPages(GraphServiceClient client) => await client.Security.AttackSimulation.LandingPages.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.LandingPages.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationLandingPagesCount(GraphServiceClient client) => await client.Security.AttackSimulation.LandingPages.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.LoginPages.Get")]
        public static async Task<LoginPageCollectionResponse?> GetSecurityAttackSimulationLoginPages(GraphServiceClient client) => await client.Security.AttackSimulation.LoginPages.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.LoginPages.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationLoginPagesCount(GraphServiceClient client) => await client.Security.AttackSimulation.LoginPages.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Operations.Get")]
        public static async Task<AttackSimulationOperationCollectionResponse?> GetSecurityAttackSimulationOperations(GraphServiceClient client) => await client.Security.AttackSimulation.Operations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Operations.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationOperationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Operations.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Payloads.Get")]
        public static async Task<PayloadCollectionResponse?> GetSecurityAttackSimulationPayloads(GraphServiceClient client) => await client.Security.AttackSimulation.Payloads.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Payloads.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationPayloadsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Payloads.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.SimulationAutomations.Get")]
        public static async Task<SimulationAutomationCollectionResponse?> GetSecurityAttackSimulationSimulationAutomations(GraphServiceClient client) => await client.Security.AttackSimulation.SimulationAutomations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.SimulationAutomations.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationSimulationAutomationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.SimulationAutomations.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Simulations.Get")]
        public static async Task<SimulationCollectionResponse?> GetSecurityAttackSimulationSimulations(GraphServiceClient client) => await client.Security.AttackSimulation.Simulations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Simulations.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationSimulationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Simulations.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Trainings.Get")]
        public static async Task<TrainingCollectionResponse?> GetSecurityAttackSimulationTrendings(GraphServiceClient client) => await client.Security.AttackSimulation.Trainings.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.AttackSimulation.Trainings.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationTrendingsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Trainings.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Cases.Get")]
        public static async Task<CasesRoot?> GetSecurityCases(GraphServiceClient client) => await client.Security.Cases.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Cases.EdiscoveryCases.Get")]
        public static async Task<EdiscoveryCaseCollectionResponse?> GetSecurityCasesEdiscoveryCases(GraphServiceClient client) => await client.Security.Cases.EdiscoveryCases.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Cases.EdiscoveryCases.Count.Get")]
        public static async Task<int?> GetSecurityCasesEdiscoveryCasesCount(GraphServiceClient client) => await client.Security.Cases.EdiscoveryCases.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Incidents.Get")]
        public static async Task<IncidentCollectionResponse?> GetSecurityIncidents(GraphServiceClient client) => await client.Security.Incidents.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Incidents.Count.Get")]
        public static async Task<int?> GetSecurityIncidentsCount(GraphServiceClient client) => await client.Security.Incidents.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.SecureScoreControlProfiles.Get")]
        public static async Task<SecureScoreControlProfileCollectionResponse?> GetSecuritySecureScoreControlProfiles(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.SecureScoreControlProfiles.Count.Get")]
        public static async Task<int?> GetSecuritySecureScoreControlProfilesCount(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.SecureScores.Get")]
        public static async Task<SecureScoreCollectionResponse?> GetSecurityScores(GraphServiceClient client) => await client.Security.SecureScores.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.SecureScores.Count.Get")]
        public static async Task<int?> GetSecuritySecureScoresCount(GraphServiceClient client) => await client.Security.SecureScores.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.SubjectRightsRequests.Get")]
        public static async Task<SubjectRightsRequestCollectionResponse?> GetSecuritySubjectRightsRequests(GraphServiceClient client) => await client.Security.SubjectRightsRequests.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.SubjectRightsRequests.Count.Get")]
        public static async Task<int?> GetSecuritySubjectRightsRequestsCount(GraphServiceClient client) => await client.Security.SubjectRightsRequests.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Get")]
        public static async Task<ThreatIntelligence?> GetSecurityThreatIntelligence(GraphServiceClient client) => await client.Security.ThreatIntelligence.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.ArticleIndicators.Get")]
        public static async Task<ArticleIndicatorCollectionResponse?> GetSecurityThreatIntelligenceArticleIndicators(GraphServiceClient client) => await client.Security.ThreatIntelligence.ArticleIndicators.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.ArticleIndicators.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceArticleIndicatorsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.ArticleIndicators.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Articles.Get")]
        public static async Task<ArticleCollectionResponse?> GetSecurityThreatIntelligenceArticles(GraphServiceClient client) => await client.Security.ThreatIntelligence.Articles.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Articles.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceArticlesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Articles.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostComponents.Get")]
        public static async Task<HostComponentCollectionResponse?> GetSecurityThreatIntelligenceHostComponents(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostComponents.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostComponents.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostComponentsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostComponents.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostCookies.Get")]
        public static async Task<HostCookieCollectionResponse?> GetSecurityThreatIntelligenceHostCookies(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostCookies.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostCookies.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostCookiesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostCookies.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostPairs.Get")]
        public static async Task<HostPairCollectionResponse?> GetSecurityThreatIntelligenceHostPairs(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPairs.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostPairs.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostPairsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPairs.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostPorts.Get")]
        public static async Task<HostPortCollectionResponse?> GetSecurityThreatIntelligenceHostPorts(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPorts.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostPorts.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostPortsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPorts.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostSslCertificates.Get")]
        public static async Task<HostSslCertificateCollectionResponse?> GetSecurityThreatIntelligenceHostSslCertificates(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostSslCertificates.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostSslCertificates.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostSslCertificatesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostSslCertificates.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostTrackers.Get")]
        public static async Task<HostTrackerCollectionResponse?> GetSecurityThreatIntelligenceHostTrackers(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostTrackers.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.HostTrackers.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostTrackersCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostTrackers.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Hosts.Get")]
        public static async Task<HostCollectionResponse?> GetSecurityThreatIntelligenceHosts(GraphServiceClient client) => await client.Security.ThreatIntelligence.Hosts.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Hosts.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Hosts.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.IntelligenceProfiles.Get")]
        public static async Task<IntelligenceProfileCollectionResponse?> GetSecurityThreatIntelligenceIntelProfiles(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelProfiles.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.IntelligenceProfiles.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceIntelProfilesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelProfiles.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.IntelligenceProfileIndicators.Get")]
        public static async Task<IntelligenceProfileIndicatorCollectionResponse?> GetSecurityThreatIntelligenceIntelligenceProfileIndicators(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelligenceProfileIndicators.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.IntelligenceProfileIndicators.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceIntelligenceProfileIndicatorsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelligenceProfileIndicators.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.PassiveDnsRecords.Get")]
        public static async Task<PassiveDnsRecordCollectionResponse?> GetSecurityThreatIntelligencePassiveDnsRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.PassiveDnsRecords.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.PassiveDnsRecords.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligencePassiveDnsRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.PassiveDnsRecords.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.SslCertificates.Get")]
        public static async Task<SslCertificateCollectionResponse?> GetSecurityThreatIntelligenceSslCertificates(GraphServiceClient client) => await client.Security.ThreatIntelligence.SslCertificates.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.SslCertificates.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceSslCertificatesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.SslCertificates.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Subdomains.Get")]
        public static async Task<SubdomainCollectionResponse?> GetSecurityThreatIntelligenceSubdomains(GraphServiceClient client) => await client.Security.ThreatIntelligence.Subdomains.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Subdomains.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceSubdomainsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Subdomains.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Vulnerabilities.Get")]
        public static async Task<VulnerabilityCollectionResponse?> GetSecurityThreatIntelligenceVulnerabilities(GraphServiceClient client) => await client.Security.ThreatIntelligence.Vulnerabilities.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.Vulnerabilities.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceVulnerabilitiesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Vulnerabilities.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.WhoisHistoryRecords.Get")]
        public static async Task<WhoisHistoryRecordCollectionResponse?> GetSecurityThreatIntelligenceWhoisHistoryRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisHistoryRecords.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.WhoisHistoryRecords.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceWhoisHistoryRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisHistoryRecords.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.WhoisRecords.Get")]
        public static async Task<WhoisRecordCollectionResponse?> GetSecurityThreatIntelligenceWhoisRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisRecords.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.ThreatIntelligence.WhoisRecords.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceWhoisRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisRecords.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.TriggerTypes.Get")]
        public static async Task<TriggerTypesRoot?> GetSecurityTriggerTypes(GraphServiceClient client) => await client.Security.TriggerTypes.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.TriggerTypes.RetentionEventTypes.Get")]
        public static async Task<RetentionEventTypeCollectionResponse?> GetSecurityTriggerTypesRetentionEventTypes(GraphServiceClient client) => await client.Security.TriggerTypes.RetentionEventTypes.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.TriggerTypes.RetentionEventTypes.Count.Get")]
        public static async Task<int?> GetSecurityTriggerTypesRetentionEventTypesCount(GraphServiceClient client) => await client.Security.TriggerTypes.RetentionEventTypes.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Triggers.Get")]
        public static async Task<TriggersRoot?> GetSecurityTriggers(GraphServiceClient client) => await client.Security.Triggers.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Triggers.RetentionEvents.Get")]
        public static async Task<RetentionEventCollectionResponse?> GetSecurityTriggersRetensionEvents(GraphServiceClient client) => await client.Security.Triggers.RetentionEvents.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Security.Triggers.RetentionEvents.Count.Get")]
        public static async Task<int?> GetSecurityTriggersRetensionEventsCount(GraphServiceClient client) => await client.Security.Triggers.RetentionEvents.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("ServicePrincipals.Get")]
        public static async Task<ServicePrincipalCollectionResponse?> GetServicePrincipals(GraphServiceClient client) => await client.ServicePrincipals.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("ServicePrincipals.Count.Get")]
        public static async Task<int?> GetServicePrincipalsCount(GraphServiceClient client) => await client.ServicePrincipals.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("ServicePrincipals.Delta.Get")]
        public static async Task<Microsoft.Graph.ServicePrincipals.Delta.DeltaGetResponse?> GetServicePrincipalsDelta(GraphServiceClient client) => await client.ServicePrincipals.Delta.GetAsDeltaGetResponseAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Shares.Get")]
        public static async Task<SharedDriveItemCollectionResponse?> GetShares(GraphServiceClient client) => await client.Shares.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Shares.Count.Get")]
        public static async Task<int?> GetSharesCount(GraphServiceClient client) => await client.Shares.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Sites.Get")]
        public static async Task<SiteCollectionResponse?> GetSites(GraphServiceClient client) => await client.Sites.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Sites.Count.Get")]
        public static async Task<int?> GetSitesCount(GraphServiceClient client) => await client.Sites.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Sites.Add.Get")]
        public static async Task<Microsoft.Graph.Sites.Delta.DeltaGetResponse?> GetSitesDelta(GraphServiceClient client) => await client.Sites.Delta.GetAsDeltaGetResponseAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Sites.GetAllSites.Get")]
        public static async Task<Microsoft.Graph.Sites.GetAllSites.GetAllSitesGetResponse?> GetSitesGetAllSites(GraphServiceClient client) => await client.Sites.GetAllSites.GetAsGetAllSitesGetResponseAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.Get")]
        public static async Task<SolutionsRoot?> GetSolutions(GraphServiceClient client) => await client.Solutions.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.BookingBusinesses.Get")]
        public static async Task<BookingBusinessCollectionResponse?> GetSolutionsBookingBusinesses(GraphServiceClient client) => await client.Solutions.BookingBusinesses.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.BookingBusinesses.Count.Get")]
        public static async Task<int?> GetSolutionsBookingBusinessesCount(GraphServiceClient client) => await client.Solutions.BookingBusinesses.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.BookingCurrencies.Get")]
        public static async Task<BookingCurrencyCollectionResponse?> GetSolutionsBookingCurrencies(GraphServiceClient client) => await client.Solutions.BookingCurrencies.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.BookingCurrencies.Count.Get")]
        public static async Task<int?> GetSolutionsBookingCurrenciesCount(GraphServiceClient client) => await client.Solutions.BookingCurrencies.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.VirtualEvents.Get")]
        public static async Task<VirtualEventsRoot?> GetSolutionsVirtualEvents(GraphServiceClient client) => await client.Solutions.VirtualEvents.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.VirtualEvents.Events.Get")]
        public static async Task<VirtualEventCollectionResponse?> GetSolutionsVirtualEventsEvents(GraphServiceClient client) => await client.Solutions.VirtualEvents.Events.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.VirtualEvents.Events.Count.Get")]
        public static async Task<int?> GetSolutionsVirtualEventsEventsCount(GraphServiceClient client) => await client.Solutions.VirtualEvents.Events.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.VirtualEvents.Webinars.Get")]
        public static async Task<VirtualEventWebinarCollectionResponse?> GetSolutionsVirtualEventsWebinars(GraphServiceClient client) => await client.Solutions.VirtualEvents.Webinars.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Solutions.VirtualEvents.Webinars.Count.Get")]
        public static async Task<int?> GetSolutionsVirtualEventsWebinarsCount(GraphServiceClient client) => await client.Solutions.VirtualEvents.Webinars.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("SubscribedSkus.Get")]
        public static async Task<SubscribedSkuCollectionResponse?> GetSubscribedSkus(GraphServiceClient client) => await client.SubscribedSkus.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Subscriptions.Get")]
        public static async Task<SubscriptionCollectionResponse?> GetSubscriptions(GraphServiceClient client) => await client.Subscriptions.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teams.Get")]
        public static async Task<TeamCollectionResponse?> GetTeams(GraphServiceClient client) => await client.Teams.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teams.Count.Get")]
        public static async Task<int?> GetTeamsCount(GraphServiceClient client) => await client.Teams.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teams.GetAllMessages.Get")]
        public static async Task<Microsoft.Graph.Teams.GetAllMessages.GetAllMessagesGetResponse?> GetTeamsGetAllMessages(GraphServiceClient client) => await client.Teams.GetAllMessages.GetAsGetAllMessagesGetResponseAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("TeamsTemplates.Get")]
        public static async Task<TeamsTemplateCollectionResponse?> GetTeamsTemplates(GraphServiceClient client) => await client.TeamsTemplates.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("TeamsTemplates.Count.Get")]
        public static async Task<int?> GetTeamsTemplatesCount(GraphServiceClient client) => await client.TeamsTemplates.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.Get")]
        public static async Task<Teamwork?> GetTeamwork(GraphServiceClient client) => await client.Teamwork.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.DeletedChats.Get")]
        public static async Task<DeletedChatCollectionResponse?> GetTeamworkDeletedChats(GraphServiceClient client) => await client.Teamwork.DeletedChats.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.DeletedChats.Count.Get")]
        public static async Task<int?> GetTeamworkDeletedChatsCount(GraphServiceClient client) => await client.Teamwork.DeletedChats.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.DeletedTeams.Get")]
        public static async Task<DeletedTeamCollectionResponse?> GetTeamworkDeletedTeams(GraphServiceClient client) => await client.Teamwork.DeletedTeams.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.DeletedTeams.Count.Get")]
        public static async Task<int?> GetTeamworkDeletedTeamsCount(GraphServiceClient client) => await client.Teamwork.DeletedTeams.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.DeletedTeams.GetAllMessages.Get")]
        public static async Task<Microsoft.Graph.Teamwork.DeletedTeams.GetAllMessages.GetAllMessagesGetResponse?> GetTeamworkDeletedTeamsGetAllMessages(GraphServiceClient client) => await client.Teamwork.DeletedTeams.GetAllMessages.GetAsGetAllMessagesGetResponseAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.TeamsAppSettings.Get")]
        public static async Task<TeamsAppSettings?> GetTeamworkTeamsAppSettings(GraphServiceClient client) => await client.Teamwork.TeamsAppSettings.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.WorkforceIntegrations.Get")]
        public static async Task<WorkforceIntegrationCollectionResponse?> GetTeamworkWorkforceIntegrations(GraphServiceClient client) => await client.Teamwork.WorkforceIntegrations.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Teamwork.WorkforceIntegrations.Count.Get")]
        public static async Task<int?> GetTeamworkWorkforceIntegrationsCount(GraphServiceClient client) => await client.Teamwork.WorkforceIntegrations.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("TenantRelationships.Get")]
        public static async Task<TenantRelationship?> GetTenantRelationships(GraphServiceClient client) => await client.TenantRelationships.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("TenantRelationships.DelegatedAdminCustomers.Get")]
        public static async Task<DelegatedAdminCustomerCollectionResponse?> GetTenantRelationshipsDelegatedAdminCustomers(GraphServiceClient client) => await client.TenantRelationships.DelegatedAdminCustomers.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("TenantRelationships.DelegatedAdminCustomers.Count.Get")]
        public static async Task<int?> GetTenantRelationshipsDelegatedAdminCustomersCount(GraphServiceClient client) => await client.TenantRelationships.DelegatedAdminCustomers.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Users.Get")]
        public static async Task<UserCollectionResponse?> GetUsers(GraphServiceClient client) => await client.Users.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Users.Count.Get")]
        public static async Task<int?> GetUsersCount(GraphServiceClient client) => await client.Users.Count.GetAsync();
        /// <summary>msgraph api implemention.</summary>
        [API("Users.Delta.Get")]
        public static async Task<Microsoft.Graph.Users.Delta.DeltaGetResponse?> GetUsersDelta(GraphServiceClient client) => await client.Users.Delta.GetAsDeltaGetResponseAsync();

        #endregion // API
    }
}
