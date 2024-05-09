using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.Graph.Models.IdentityGovernance;
using Microsoft.Graph.Models.Search;
using Microsoft.Graph.Models.Security;
using Microsoft.Graph.Models;
using Microsoft.Graph;

namespace E5Renewer.Processor.GraphAPIs
{
    [APIContainer]
    public static class Get
    {
        #region API
        [API("AgreementAcceptances.Get")]
        public static async Task<AgreementAcceptanceCollectionResponse?> GetAgreementAcceptance(GraphServiceClient client) => await client.AgreementAcceptances.GetAsync();

        [API("Admin.Get")]
        public static async Task<Admin?> GetAdmin(GraphServiceClient client) => await client.Admin.GetAsync();

        [API("Agreements.Get")]
        public static async Task<AgreementCollectionResponse?> GetAgreements(GraphServiceClient client) => await client.Agreements.GetAsync();

        [API("AppCatalogs.Get")]
        public static async Task<AppCatalogs?> GetAppCatalogs(GraphServiceClient client) => await client.AppCatalogs.GetAsync();

        [API("ApplicationTemplates.Get")]
        public static async Task<ApplicationTemplateCollectionResponse?> GetApplicationTemplates(GraphServiceClient client) => await client.ApplicationTemplates.GetAsync();

        [API("Applications.Get")]
        public static async Task<ApplicationCollectionResponse?> GetApplications(GraphServiceClient client) => await client.Applications.GetAsync();

        [API("AuditLogs.Get")]
        public static async Task<AuditLogRoot?> GetAuditLogs(GraphServiceClient client) => await client.AuditLogs.GetAsync();

        [API("AuthenticationMethodConfigurations.Get")]
        public static async Task<AuthenticationMethodConfigurationCollectionResponse?> GetAuthenticationMethodConfigurations(GraphServiceClient client) => await client.AuthenticationMethodConfigurations.GetAsync();

        [API("AuthenticationMethodsPolicy.Get")]
        public static async Task<AuthenticationMethodsPolicy?> GetAuthenticationMethodsPolicy(GraphServiceClient client) => await client.AuthenticationMethodsPolicy.GetAsync();

        [API("CertificateBasedAuthConfiguration.Get")]
        public static async Task<CertificateBasedAuthConfigurationCollectionResponse?> GetCertificateBasedAuthConfiguration(GraphServiceClient client) => await client.CertificateBasedAuthConfiguration.GetAsync();

        [API("Chats.Get")]
        public static async Task<ChatCollectionResponse?> GetChats(GraphServiceClient client) => await client.Chats.GetAsync();

        [API("Communications.Get")]
        public static async Task<CloudCommunications?> GetCommunications(GraphServiceClient client) => await client.Communications.GetAsync();

        [API("Compliance.Get")]
        public static async Task<Compliance?> GetCompliance(GraphServiceClient client) => await client.Compliance.GetAsync();

        [API("Connections.Get")]
        public static async Task<ExternalConnectionCollectionResponse?> GetConnections(GraphServiceClient client) => await client.Connections.GetAsync();

        [API("Contacts.Get")]
        public static async Task<OrgContactCollectionResponse?> GetContacts(GraphServiceClient client) => await client.Contacts.GetAsync();

        [API("DataPolicyOperations.Get")]
        public static async Task<DataPolicyOperationCollectionResponse?> GetDataPolicyOperations(GraphServiceClient client) => await client.DataPolicyOperations.GetAsync();

        [API("DeviceAppManagement.Get")]
        public static async Task<DeviceAppManagement?> GetDeviceAppManagement(GraphServiceClient client) => await client.DeviceAppManagement.GetAsync();

        [API("DeviceManagement.Get")]
        public static async Task<DeviceManagement?> GetDeviceManagement(GraphServiceClient client) => await client.DeviceManagement.GetAsync();

        [API("Devices.Get")]
        public static async Task<DeviceCollectionResponse?> GetDevies(GraphServiceClient client) => await client.Devices.GetAsync();

        [API("Direcory.Get")]
        public static async Task<DirectoryObject1?> GetDirecory(GraphServiceClient client) => await client.Directory.GetAsync();

        [API("DirectoryObjects.Get")]
        public static async Task<DirectoryObjectCollectionResponse?> GetDirectoryObjects(GraphServiceClient client) => await client.DirectoryObjects.GetAsync();

        [API("DirectoryRoleTemplates.Get")]
        public static async Task<DirectoryRoleTemplateCollectionResponse?> GetDirectoryRoleTemplates(GraphServiceClient client) => await client.DirectoryRoleTemplates.GetAsync();

        [API("DirectoryRoles.Get")]
        public static async Task<DirectoryRoleCollectionResponse?> GetDirectoryRoles(GraphServiceClient client) => await client.DirectoryRoles.GetAsync();

        [API("DomainDnsRecords.Get")]
        public static async Task<DomainDnsRecordCollectionResponse?> GetDomainDnsRecords(GraphServiceClient client) => await client.DomainDnsRecords.GetAsync();

        [API("Domains.Get")]
        public static async Task<DomainCollectionResponse?> GetDomains(GraphServiceClient client) => await client.Domains.GetAsync();

        [API("Drives.Get")]
        public static async Task<DriveCollectionResponse?> GetDrives(GraphServiceClient client) => await client.Drives.GetAsync();

        [API("Education.Get")]
        public static async Task<EducationRoot?> GetEducation(GraphServiceClient client) => await client.Education.GetAsync();

        [API("EmployeeExperience.Get")]
        public static async Task<EmployeeExperience?> GetEmployeeExperience(GraphServiceClient client) => await client.EmployeeExperience.GetAsync();

        [API("External.Get")]
        public static async Task<External?> GetExternal(GraphServiceClient client) => await client.External.GetAsync();

        [API("FilterOperators.Get")]
        public static async Task<FilterOperatorSchemaCollectionResponse?> GetFilterOperators(GraphServiceClient client) => await client.FilterOperators.GetAsync();

        [API("Functions.Get")]
        public static async Task<AttributeMappingFunctionSchemaCollectionResponse?> GetFunctions(GraphServiceClient client) => await client.Functions.GetAsync();

        [API("GroupLifecyclePolicies.Get")]
        public static async Task<GroupLifecyclePolicyCollectionResponse?> GetGroupLifecyclePolicies(GraphServiceClient client) => await client.GroupLifecyclePolicies.GetAsync();

        [API("GroupSettingTemplates.Get")]
        public static async Task<GroupSettingTemplateCollectionResponse?> GetGroupSettingTemplates(GraphServiceClient client) => await client.GroupSettingTemplates.GetAsync();

        [API("GroupSetings.Get")]
        public static async Task<GroupSettingCollectionResponse?> GetGroupSettings(GraphServiceClient client) => await client.GroupSettings.GetAsync();

        [API("Groups.Get")]
        public static async Task<GroupCollectionResponse?> GetGroups(GraphServiceClient client) => await client.Groups.GetAsync();

        [API("Identity.Get")]
        public static async Task<IdentityContainer?> GetIdentity(GraphServiceClient client) => await client.Identity.GetAsync();

        [API("IdentityGovernance.Get")]
        public static async Task<IdentityGovernance?> GetIdentityGovernance(GraphServiceClient client) => await client.IdentityGovernance.GetAsync();

        [API("IdentityProtection.Get")]
        public static async Task<IdentityProtectionRoot?> GetIdentityProtection(GraphServiceClient client) => await client.IdentityProtection.GetAsync();

        [Obsolete("GraphServiceClient.IdentityProviders.GetAsync is obsolete.")]
        [API("IdentityProviders.Get")]
        public static async Task<IdentityProviderCollectionResponse?> GetIdentityProviders(GraphServiceClient client) => await client.IdentityProviders.GetAsync();

        [API("InformationProtecion.Get")]
        public static async Task<InformationProtection?> GetInfomationProtecion(GraphServiceClient client) => await client.InformationProtection.GetAsync();

        [API("Invitations.Get")]
        public static async Task<InvitationCollectionResponse?> GetInvitations(GraphServiceClient client) => await client.Invitations.GetAsync();

        [API("OAuth2PermissionGrants.Get")]
        public static async Task<OAuth2PermissionGrantCollectionResponse?> GetOAuth2PermissionGrants(GraphServiceClient client) => await client.Oauth2PermissionGrants.GetAsync();

        [API("Organization.Get")]
        public static async Task<OrganizationCollectionResponse?> GetOrganization(GraphServiceClient client) => await client.Organization.GetAsync();

        [API("PermissionGrants.Get")]
        public static async Task<ResourceSpecificPermissionGrantCollectionResponse?> GetPermissionGrants(GraphServiceClient client) => await client.PermissionGrants.GetAsync();

        [API("Places.Count.Get")]
        public static async Task<int?> GetPlacesCount(GraphServiceClient client) => await client.Places.Count.GetAsync();

        [API("Places.GraphRoom.Get")]
        public static async Task<RoomCollectionResponse?> GetPlacesGraphRoom(GraphServiceClient client) => await client.Places.GraphRoom.GetAsync();

        [API("Places.GraphRoom.Count.Get")]
        public static async Task<int?> GetPlacesGraphRoomCount(GraphServiceClient client) => await client.Places.GraphRoom.Count.GetAsync();

        [API("Places.GraphRoomList.Get")]
        public static async Task<RoomListCollectionResponse?> GetPlacesGraphRoomList(GraphServiceClient client) => await client.Places.GraphRoomList.GetAsync();

        [API("Places.GraphRoomList.Count.Get")]
        public static async Task<int?> GetPlacesGraphRoomListCount(GraphServiceClient client) => await client.Places.GraphRoomList.Count.GetAsync();

        [API("Planner.Get")]
        public static async Task<Planner?> GetPlanner(GraphServiceClient client) => await client.Planner.GetAsync();

        [API("Planner.Buckets.Get")]
        public static async Task<PlannerBucketCollectionResponse?> GetPlannerBuckets(GraphServiceClient client) => await client.Planner.Buckets.GetAsync();

        [API("Planner.Buckets.Count.Get")]
        public static async Task<int?> GetPlannerBucketsC(GraphServiceClient client) => await client.Planner.Buckets.Count.GetAsync();

        [API("Planner.Plans.Get")]
        public static async Task<PlannerPlanCollectionResponse?> GetPlannerPlans(GraphServiceClient client) => await client.Planner.Plans.GetAsync();

        [API("Planner.Plans.Count.Get")]
        public static async Task<int?> GetPlannerPlansCount(GraphServiceClient client) => await client.Planner.Plans.Count.GetAsync();

        [API("Planner.Tasks.Get")]
        public static async Task<PlannerTaskCollectionResponse?> GetPlannerTasks(GraphServiceClient client) => await client.Planner.Tasks.GetAsync();

        [API("Planner.Tasks.Count.Get")]
        public static async Task<int?> GetPlannerTasksCount(GraphServiceClient client) => await client.Planner.Tasks.Count.GetAsync();

        [API("Policies.Get")]
        public static async Task<PolicyRoot?> GetPolicies(GraphServiceClient client) => await client.Policies.GetAsync();

        [API("Print.Get")]
        public static async Task<Print?> GetPrint(GraphServiceClient client) => await client.Print.GetAsync();

        [API("Privacy.Get")]
        public static async Task<Privacy?> GetPrivacy(GraphServiceClient client) => await client.Privacy.GetAsync();

        [API("Reports.Get")]
        public static async Task<ReportRoot?> GetReports(GraphServiceClient client) => await client.Reports.GetAsync();

        [API("RoleManagement.Get")]
        public static async Task<RoleManagement?> GetRoleManagement(GraphServiceClient client) => await client.RoleManagement.GetAsync();

        [API("SchemaExtensions.Get")]
        public static async Task<SchemaExtensionCollectionResponse?> GetSchemaExtensions(GraphServiceClient client) => await client.SchemaExtensions.GetAsync();

        [API("ScopedRoleMemberships.Get")]
        public static async Task<ScopedRoleMembershipCollectionResponse?> GetScopedRoleMemberships(GraphServiceClient client) => await client.ScopedRoleMemberships.GetAsync();

        [API("Search.Get")]
        public static async Task<SearchEntity?> GetSearch(GraphServiceClient client) => await client.Search.GetAsync();

        [API("Search.Acronyms.Get")]
        public static async Task<AcronymCollectionResponse?> GetSearchAcronyms(GraphServiceClient client) => await client.Search.Acronyms.GetAsync();

        [API("Search.Acronyms.Count.Get")]
        public static async Task<int?> GetSearchAcronymsCount(GraphServiceClient client) => await client.Search.Acronyms.Count.GetAsync();

        [API("Search.Bookmarks.Get")]
        public static async Task<BookmarkCollectionResponse?> GetSearchBookmarks(GraphServiceClient client) => await client.Search.Bookmarks.GetAsync();

        [API("Search.Bookmarks.Count.Get")]
        public static async Task<int?> GetSearchBookmarksCount(GraphServiceClient client) => await client.Search.Bookmarks.Count.GetAsync();

        [API("Search.Qnas.Get")]
        public static async Task<QnaCollectionResponse?> GetSearchQnas(GraphServiceClient client) => await client.Search.Qnas.GetAsync();

        [API("Search.Qnas.Count.Get")]
        public static async Task<int?> GetSearhQnasCount(GraphServiceClient client) => await client.Search.Qnas.Count.GetAsync();

        [API("Security.Get")]
        public static async Task<Security?> GetSecurity(GraphServiceClient client) => await client.Security.GetAsync();

        [API("Security.Alerts.Get")]
        public static async Task<Microsoft.Graph.Models.AlertCollectionResponse?> GetSecurityCount(GraphServiceClient client) => await client.Security.Alerts.GetAsync();

        [API("Security.Alerts.Count.Get")]
        public static async Task<int?> GetSecurityAlertsCount(GraphServiceClient client) => await client.Security.Alerts.Count.GetAsync();

        [API("Security.Alerts_v2.Get")]
        public static async Task<Microsoft.Graph.Models.Security.AlertCollectionResponse?> GetSecurityAlertsV2(GraphServiceClient client) => await client.Security.Alerts_v2.GetAsync();

        [API("Security.Alerts_v2.Count.Get")]
        public static async Task<int?> GetSecurityAlertsV2Count(GraphServiceClient client) => await client.Security.Alerts_v2.Count.GetAsync();

        [API("Security.AttackSimulation.Get")]
        public static async Task<AttackSimulationRoot?> GetSecurityAttackSimulation(GraphServiceClient client) => await client.Security.AttackSimulation.GetAsync();

        [API("Security.AttackSimulation.EndUserNotifications.Get")]
        public static async Task<EndUserNotificationCollectionResponse?> GetSecurityAttackSimulationEndUserNotifications(GraphServiceClient client) => await client.Security.AttackSimulation.EndUserNotifications.GetAsync();

        [API("Security.AttackSimulation.EndUserNotifications.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationEndUserNotificationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.EndUserNotifications.Count.GetAsync();

        [API("Security.AttackSimulation.LandingPages.Get")]
        public static async Task<LandingPageCollectionResponse?> GetSecurityAttackSimulationLandingPages(GraphServiceClient client) => await client.Security.AttackSimulation.LandingPages.GetAsync();

        [API("Security.AttackSimulation.LandingPages.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationLandingPagesCount(GraphServiceClient client) => await client.Security.AttackSimulation.LandingPages.Count.GetAsync();

        [API("Security.AttackSimulation.LoginPages.Get")]
        public static async Task<LoginPageCollectionResponse?> GetSecurityAttackSimulationLoginPages(GraphServiceClient client) => await client.Security.AttackSimulation.LoginPages.GetAsync();

        [API("Security.AttackSimulation.LoginPages.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationLoginPagesCount(GraphServiceClient client) => await client.Security.AttackSimulation.LoginPages.Count.GetAsync();

        [API("Security.AttackSimulation.Operations.Get")]
        public static async Task<AttackSimulationOperationCollectionResponse?> GetSecurityAttackSimulationOperations(GraphServiceClient client) => await client.Security.AttackSimulation.Operations.GetAsync();

        [API("Security.AttackSimulation.Operations.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationOperationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Operations.Count.GetAsync();

        [API("Security.AttackSimulation.Payloads.Get")]
        public static async Task<PayloadCollectionResponse?> GetSecurityAttackSimulationPayloads(GraphServiceClient client) => await client.Security.AttackSimulation.Payloads.GetAsync();

        [API("Security.AttackSimulation.Payloads.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationPayloadsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Payloads.Count.GetAsync();

        [API("Security.AttackSimulation.SimulationAutomations.Get")]
        public static async Task<SimulationAutomationCollectionResponse?> GetSecurityAttackSimulationSimulationAutomations(GraphServiceClient client) => await client.Security.AttackSimulation.SimulationAutomations.GetAsync();

        [API("Security.AttackSimulation.SimulationAutomations.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationSimulationAutomationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.SimulationAutomations.Count.GetAsync();

        [API("Security.AttackSimulation.Simulations.Get")]
        public static async Task<SimulationCollectionResponse?> GetSecurityAttackSimulationSimulations(GraphServiceClient client) => await client.Security.AttackSimulation.Simulations.GetAsync();

        [API("Security.AttackSimulation.Simulations.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationSimulationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Simulations.Count.GetAsync();

        [API("Security.AttackSimulation.Trainings.Get")]
        public static async Task<TrainingCollectionResponse?> GetSecurityAttackSimulationTrendings(GraphServiceClient client) => await client.Security.AttackSimulation.Trainings.GetAsync();

        [API("Security.AttackSimulation.Trainings.Count.Get")]
        public static async Task<int?> GetSecurityAttackSimulationTrendingsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Trainings.Count.GetAsync();

        [API("Security.Cases.Get")]
        public static async Task<CasesRoot?> GetSecurityCases(GraphServiceClient client) => await client.Security.Cases.GetAsync();

        [API("Security.Cases.EdiscoveryCases.Get")]
        public static async Task<EdiscoveryCaseCollectionResponse?> GetSecurityCasesEdiscoveryCases(GraphServiceClient client) => await client.Security.Cases.EdiscoveryCases.GetAsync();

        [API("Security.Cases.EdiscoveryCases.Count.Get")]
        public static async Task<int?> GetSecurityCasesEdiscoveryCasesCount(GraphServiceClient client) => await client.Security.Cases.EdiscoveryCases.Count.GetAsync();

        [API("Security.Incidents.Get")]
        public static async Task<IncidentCollectionResponse?> GetSecurityIncidents(GraphServiceClient client) => await client.Security.Incidents.GetAsync();

        [API("Security.Incidents.Count.Get")]
        public static async Task<int?> GetSecurityIncidentsCount(GraphServiceClient client) => await client.Security.Incidents.Count.GetAsync();

        [API("Security.SecureScoreControlProfiles.Get")]
        public static async Task<SecureScoreControlProfileCollectionResponse?> GetSecuritySecureScoreControlProfiles(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.GetAsync();

        [API("Security.SecureScoreControlProfiles.Count.Get")]
        public static async Task<int?> GetSecuritySecureScoreControlProfilesCount(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.Count.GetAsync();

        [API("Security.SecureScores.Get")]
        public static async Task<SecureScoreCollectionResponse?> GetSecurityScores(GraphServiceClient client) => await client.Security.SecureScores.GetAsync();

        [API("Security.SecureScores.Count.Get")]
        public static async Task<int?> GetSecuritySecureScoresCount(GraphServiceClient client) => await client.Security.SecureScores.Count.GetAsync();

        [API("Security.SubjectRightsRequests.Get")]
        public static async Task<SubjectRightsRequestCollectionResponse?> GetSecuritySubjectRightsRequests(GraphServiceClient client) => await client.Security.SubjectRightsRequests.GetAsync();

        [API("Security.SubjectRightsRequests.Count.Get")]
        public static async Task<int?> GetSecuritySubjectRightsRequestsCount(GraphServiceClient client) => await client.Security.SubjectRightsRequests.Count.GetAsync();

        [API("Security.ThreatIntelligence.Get")]
        public static async Task<ThreatIntelligence?> GetSecurityThreatIntelligence(GraphServiceClient client) => await client.Security.ThreatIntelligence.GetAsync();

        [API("Security.ThreatIntelligence.ArticleIndicators.Get")]
        public static async Task<ArticleIndicatorCollectionResponse?> GetSecurityThreatIntelligenceArticleIndicators(GraphServiceClient client) => await client.Security.ThreatIntelligence.ArticleIndicators.GetAsync();

        [API("Security.ThreatIntelligence.ArticleIndicators.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceArticleIndicatorsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.ArticleIndicators.Count.GetAsync();

        [API("Security.ThreatIntelligence.Articles.Get")]
        public static async Task<ArticleCollectionResponse?> GetSecurityThreatIntelligenceArticles(GraphServiceClient client) => await client.Security.ThreatIntelligence.Articles.GetAsync();

        [API("Security.ThreatIntelligence.Articles.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceArticlesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Articles.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostComponents.Get")]
        public static async Task<HostComponentCollectionResponse?> GetSecurityThreatIntelligenceHostComponents(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostComponents.GetAsync();

        [API("Security.ThreatIntelligence.HostComponents.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostComponentsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostComponents.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostCookies.Get")]
        public static async Task<HostCookieCollectionResponse?> GetSecurityThreatIntelligenceHostCookies(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostCookies.GetAsync();

        [API("Security.ThreatIntelligence.HostCookies.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostCookiesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostCookies.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostPairs.Get")]
        public static async Task<HostPairCollectionResponse?> GetSecurityThreatIntelligenceHostPairs(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPairs.GetAsync();

        [API("Security.ThreatIntelligence.HostPairs.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostPairsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPairs.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostPorts.Get")]
        public static async Task<HostPortCollectionResponse?> GetSecurityThreatIntelligenceHostPorts(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPorts.GetAsync();

        [API("Security.ThreatIntelligence.HostPorts.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostPortsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPorts.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostSslCertificates.Get")]
        public static async Task<HostSslCertificateCollectionResponse?> GetSecurityThreatIntelligenceHostSslCertificates(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostSslCertificates.GetAsync();

        [API("Security.ThreatIntelligence.HostSslCertificates.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostSslCertificatesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostSslCertificates.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostTrackers.Get")]
        public static async Task<HostTrackerCollectionResponse?> GetSecurityThreatIntelligenceHostTrackers(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostTrackers.GetAsync();

        [API("Security.ThreatIntelligence.HostTrackers.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostTrackersCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostTrackers.Count.GetAsync();

        [API("Security.ThreatIntelligence.Hosts.Get")]
        public static async Task<HostCollectionResponse?> GetSecurityThreatIntelligenceHosts(GraphServiceClient client) => await client.Security.ThreatIntelligence.Hosts.GetAsync();

        [API("Security.ThreatIntelligence.Hosts.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceHostsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Hosts.Count.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfiles.Get")]
        public static async Task<IntelligenceProfileCollectionResponse?> GetSecurityThreatIntelligenceIntelProfiles(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelProfiles.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfiles.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceIntelProfilesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelProfiles.Count.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfileIndicators.Get")]
        public static async Task<IntelligenceProfileIndicatorCollectionResponse?> GetSecurityThreatIntelligenceIntelligenceProfileIndicators(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelligenceProfileIndicators.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfileIndicators.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceIntelligenceProfileIndicatorsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelligenceProfileIndicators.Count.GetAsync();

        [API("Security.ThreatIntelligence.PassiveDnsRecords.Get")]
        public static async Task<PassiveDnsRecordCollectionResponse?> GetSecurityThreatIntelligencePassiveDnsRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.PassiveDnsRecords.GetAsync();

        [API("Security.ThreatIntelligence.PassiveDnsRecords.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligencePassiveDnsRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.PassiveDnsRecords.Count.GetAsync();

        [API("Security.ThreatIntelligence.SslCertificates.Get")]
        public static async Task<SslCertificateCollectionResponse?> GetSecurityThreatIntelligenceSslCertificates(GraphServiceClient client) => await client.Security.ThreatIntelligence.SslCertificates.GetAsync();

        [API("Security.ThreatIntelligence.SslCertificates.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceSslCertificatesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.SslCertificates.Count.GetAsync();

        [API("Security.ThreatIntelligence.Subdomains.Get")]
        public static async Task<SubdomainCollectionResponse?> GetSecurityThreatIntelligenceSubdomains(GraphServiceClient client) => await client.Security.ThreatIntelligence.Subdomains.GetAsync();

        [API("Security.ThreatIntelligence.Subdomains.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceSubdomainsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Subdomains.Count.GetAsync();

        [API("Security.ThreatIntelligence.Vulnerabilities.Get")]
        public static async Task<VulnerabilityCollectionResponse?> GetSecurityThreatIntelligenceVulnerabilities(GraphServiceClient client) => await client.Security.ThreatIntelligence.Vulnerabilities.GetAsync();

        [API("Security.ThreatIntelligence.Vulnerabilities.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceVulnerabilitiesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Vulnerabilities.Count.GetAsync();

        [API("Security.ThreatIntelligence.WhoisHistoryRecords.Get")]
        public static async Task<WhoisHistoryRecordCollectionResponse?> GetSecurityThreatIntelligenceWhoisHistoryRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisHistoryRecords.GetAsync();

        [API("Security.ThreatIntelligence.WhoisHistoryRecords.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceWhoisHistoryRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisHistoryRecords.Count.GetAsync();

        [API("Security.ThreatIntelligence.WhoisRecords.Get")]
        public static async Task<WhoisRecordCollectionResponse?> GetSecurityThreatIntelligenceWhoisRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisRecords.GetAsync();

        [API("Security.ThreatIntelligence.WhoisRecords.Count.Get")]
        public static async Task<int?> GetSecurityThreatIntelligenceWhoisRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisRecords.Count.GetAsync();

        [API("Security.TriggerTypes.Get")]
        public static async Task<TriggerTypesRoot?> GetSecurityTriggerTypes(GraphServiceClient client) => await client.Security.TriggerTypes.GetAsync();

        [API("Security.TriggerTypes.RetentionEventTypes.Get")]
        public static async Task<RetentionEventTypeCollectionResponse?> GetSecurityTriggerTypesRetentionEventTypes(GraphServiceClient client) => await client.Security.TriggerTypes.RetentionEventTypes.GetAsync();

        [API("Security.TriggerTypes.RetentionEventTypes.Count.Get")]
        public static async Task<int?> GetSecurityTriggerTypesRetentionEventTypesCount(GraphServiceClient client) => await client.Security.TriggerTypes.RetentionEventTypes.Count.GetAsync();

        [API("Security.Triggers.Get")]
        public static async Task<TriggersRoot?> GetSecurityTriggers(GraphServiceClient client) => await client.Security.Triggers.GetAsync();

        [API("Security.Triggers.RetentionEvents.Get")]
        public static async Task<RetentionEventCollectionResponse?> GetSecurityTriggersRetensionEvents(GraphServiceClient client) => await client.Security.Triggers.RetentionEvents.GetAsync();

        [API("Security.Triggers.RetentionEvents.Count.Get")]
        public static async Task<int?> GetSecurityTriggersRetensionEventsCount(GraphServiceClient client) => await client.Security.Triggers.RetentionEvents.Count.GetAsync();

        [API("ServicePrincipals.Get")]
        public static async Task<ServicePrincipalCollectionResponse?> GetServicePrincipals(GraphServiceClient client) => await client.ServicePrincipals.GetAsync();

        [API("ServicePrincipals.Count.Get")]
        public static async Task<int?> GetServicePrincipalsCount(GraphServiceClient client) => await client.ServicePrincipals.Count.GetAsync();

        [API("ServicePrincipals.Delta.Get")]
        public static async Task<Microsoft.Graph.ServicePrincipals.Delta.DeltaGetResponse?> GetServicePrincipalsDelta(GraphServiceClient client) => await client.ServicePrincipals.Delta.GetAsDeltaGetResponseAsync();

        [API("Shares.Get")]
        public static async Task<SharedDriveItemCollectionResponse?> GetShares(GraphServiceClient client) => await client.Shares.GetAsync();

        [API("Shares.Count.Get")]
        public static async Task<int?> GetSharesCount(GraphServiceClient client) => await client.Shares.Count.GetAsync();

        [API("Sites.Get")]
        public static async Task<SiteCollectionResponse?> GetSites(GraphServiceClient client) => await client.Sites.GetAsync();

        [API("Sites.Count.Get")]
        public static async Task<int?> GetSitesCount(GraphServiceClient client) => await client.Sites.Count.GetAsync();

        [API("Sites.Add.Get")]
        public static async Task<Microsoft.Graph.Sites.Delta.DeltaGetResponse?> GetSitesDelta(GraphServiceClient client) => await client.Sites.Delta.GetAsDeltaGetResponseAsync();

        [API("Sites.GetAllSites.Get")]
        public static async Task<Microsoft.Graph.Sites.GetAllSites.GetAllSitesGetResponse?> GetSitesGetAllSites(GraphServiceClient client) => await client.Sites.GetAllSites.GetAsGetAllSitesGetResponseAsync();

        [API("Solutions.Get")]
        public static async Task<SolutionsRoot?> GetSolutions(GraphServiceClient client) => await client.Solutions.GetAsync();

        [API("Solutions.BookingBusinesses.Get")]
        public static async Task<BookingBusinessCollectionResponse?> GetSolutionsBookingBusinesses(GraphServiceClient client) => await client.Solutions.BookingBusinesses.GetAsync();

        [API("Solutions.BookingBusinesses.Count.Get")]
        public static async Task<int?> GetSolutionsBookingBusinessesCount(GraphServiceClient client) => await client.Solutions.BookingBusinesses.Count.GetAsync();

        [API("Solutions.BookingCurrencies.Get")]
        public static async Task<BookingCurrencyCollectionResponse?> GetSolutionsBookingCurrencies(GraphServiceClient client) => await client.Solutions.BookingCurrencies.GetAsync();

        [API("Solutions.BookingCurrencies.Count.Get")]
        public static async Task<int?> GetSolutionsBookingCurrenciesCount(GraphServiceClient client) => await client.Solutions.BookingCurrencies.Count.GetAsync();

        [API("Solutions.VirtualEvents.Get")]
        public static async Task<VirtualEventsRoot?> GetSolutionsVirtualEvents(GraphServiceClient client) => await client.Solutions.VirtualEvents.GetAsync();

        [API("Solutions.VirtualEvents.Events.Get")]
        public static async Task<VirtualEventCollectionResponse?> GetSolutionsVirtualEventsEvents(GraphServiceClient client) => await client.Solutions.VirtualEvents.Events.GetAsync();

        [API("Solutions.VirtualEvents.Events.Count.Get")]
        public static async Task<int?> GetSolutionsVirtualEventsEventsCount(GraphServiceClient client) => await client.Solutions.VirtualEvents.Events.Count.GetAsync();

        [API("Solutions.VirtualEvents.Webinars.Get")]
        public static async Task<VirtualEventWebinarCollectionResponse?> GetSolutionsVirtualEventsWebinars(GraphServiceClient client) => await client.Solutions.VirtualEvents.Webinars.GetAsync();

        [API("Solutions.VirtualEvents.Webinars.Count.Get")]
        public static async Task<int?> GetSolutionsVirtualEventsWebinarsCount(GraphServiceClient client) => await client.Solutions.VirtualEvents.Webinars.Count.GetAsync();

        [API("SubscribedSkus.Get")]
        public static async Task<SubscribedSkuCollectionResponse?> GetSubscribedSkus(GraphServiceClient client) => await client.SubscribedSkus.GetAsync();

        [API("Subscriptions.Get")]
        public static async Task<SubscriptionCollectionResponse?> GetSubscriptions(GraphServiceClient client) => await client.Subscriptions.GetAsync();

        [API("Teams.Get")]
        public static async Task<TeamCollectionResponse?> GetTeams(GraphServiceClient client) => await client.Teams.GetAsync();

        [API("Teams.Count.Get")]
        public static async Task<int?> GetTeamsCount(GraphServiceClient client) => await client.Teams.Count.GetAsync();

        [API("Teams.GetAllMessages.Get")]
        public static async Task<Microsoft.Graph.Teams.GetAllMessages.GetAllMessagesGetResponse?> GetTeamsGetAllMessages(GraphServiceClient client) => await client.Teams.GetAllMessages.GetAsGetAllMessagesGetResponseAsync();

        [API("TeamsTemplates.Get")]
        public static async Task<TeamsTemplateCollectionResponse?> GetTeamsTemplates(GraphServiceClient client) => await client.TeamsTemplates.GetAsync();

        [API("TeamsTemplates.Count.Get")]
        public static async Task<int?> GetTeamsTemplatesCount(GraphServiceClient client) => await client.TeamsTemplates.Count.GetAsync();

        [API("Teamwork.Get")]
        public static async Task<Teamwork?> GetTeamwork(GraphServiceClient client) => await client.Teamwork.GetAsync();

        [API("Teamwork.DeletedChats.Get")]
        public static async Task<DeletedChatCollectionResponse?> GetTeamworkDeletedChats(GraphServiceClient client) => await client.Teamwork.DeletedChats.GetAsync();

        [API("Teamwork.DeletedChats.Count.Get")]
        public static async Task<int?> GetTeamworkDeletedChatsCount(GraphServiceClient client) => await client.Teamwork.DeletedChats.Count.GetAsync();

        [API("Teamwork.DeletedTeams.Get")]
        public static async Task<DeletedTeamCollectionResponse?> GetTeamworkDeletedTeams(GraphServiceClient client) => await client.Teamwork.DeletedTeams.GetAsync();

        [API("Teamwork.DeletedTeams.Count.Get")]
        public static async Task<int?> GetTeamworkDeletedTeamsCount(GraphServiceClient client) => await client.Teamwork.DeletedTeams.Count.GetAsync();

        [API("Teamwork.DeletedTeams.GetAllMessages.Get")]
        public static async Task<Microsoft.Graph.Teamwork.DeletedTeams.GetAllMessages.GetAllMessagesGetResponse?> GetTeamworkDeletedTeamsGetAllMessages(GraphServiceClient client) => await client.Teamwork.DeletedTeams.GetAllMessages.GetAsGetAllMessagesGetResponseAsync();

        [API("Teamwork.TeamsAppSettings.Get")]
        public static async Task<TeamsAppSettings?> GetTeamworkTeamsAppSettings(GraphServiceClient client) => await client.Teamwork.TeamsAppSettings.GetAsync();

        [API("Teamwork.WorkforceIntegrations.Get")]
        public static async Task<WorkforceIntegrationCollectionResponse?> GetTeamworkWorkforceIntegrations(GraphServiceClient client) => await client.Teamwork.WorkforceIntegrations.GetAsync();

        [API("Teamwork.WorkforceIntegrations.Count.Get")]
        public static async Task<int?> GetTeamworkWorkforceIntegrationsCount(GraphServiceClient client) => await client.Teamwork.WorkforceIntegrations.Count.GetAsync();

        [API("TenantRelationships.Get")]
        public static async Task<TenantRelationship?> GetTenantRelationships(GraphServiceClient client) => await client.TenantRelationships.GetAsync();

        [API("TenantRelationships.DelegatedAdminCustomers.Get")]
        public static async Task<DelegatedAdminCustomerCollectionResponse?> GetTenantRelationshipsDelegatedAdminCustomers(GraphServiceClient client) => await client.TenantRelationships.DelegatedAdminCustomers.GetAsync();

        [API("TenantRelationships.DelegatedAdminCustomers.Count.Get")]
        public static async Task<int?> GetTenantRelationshipsDelegatedAdminCustomersCount(GraphServiceClient client) => await client.TenantRelationships.DelegatedAdminCustomers.Count.GetAsync();

        [API("Users.Get")]
        public static async Task<UserCollectionResponse?> GetUsers(GraphServiceClient client) => await client.Users.GetAsync();

        [API("Users.Count.Get")]
        public static async Task<int?> GetUsersCount(GraphServiceClient client) => await client.Users.Count.GetAsync();

        [API("Users.Delta.Get")]
        public static async Task<Microsoft.Graph.Users.Delta.DeltaGetResponse?> GetUsersDelta(GraphServiceClient client) => await client.Users.Delta.GetAsDeltaGetResponseAsync();

        #endregion // API
    }
}
