using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>functions container for calling msgraph apis.</summary>
    public class GetAPIFunctionsContainer : IAPIFunctionsContainer
    {
        private readonly ILogger<GetAPIFunctionsContainer> logger;
        private readonly List<KeyValuePair<string, APIFunction>> cache = new();

        /// <summary> Initialize <c>GetAPIFunctionsContainer</c> with parameters given.</summary>
        /// <param name="logger">The logger to generate logs.</param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public GetAPIFunctionsContainer(ILogger<GetAPIFunctionsContainer> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, APIFunction>> GetAPIFunctions()
        {
            if (cache.Count() > 0)
            {
                this.logger.LogDebug("Using cache to provide results.");
                return cache;
            }
            else
            {
                this.logger.LogDebug("Cache miss, generating manually.");
                return GetAPIFunctionsWithoutCache();
            }
        }

        private IEnumerable<KeyValuePair<string, APIFunction>> GetAPIFunctionsWithoutCache()
        {
            this.cache.Clear();
            foreach (MethodInfo methodInfo in this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).
                Where((methodInfo) => methodInfo.IsDefined(typeof(AsyncStateMachineAttribute)))
            )
            {
                this.logger.LogDebug("Parsing {0}", methodInfo.Name);
                string? id = methodInfo.GetCustomAttribute<APIAttribute>()?.id;
                if (methodInfo.IsPrivate && !string.IsNullOrEmpty(id) && !methodInfo.IsDefined(typeof(ObsoleteAttribute)))
                {
                    this.logger.LogDebug("Method {0} is allowed to be converted to {}", methodInfo.Name, nameof(APIFunction));
                    Delegate d = Delegate.CreateDelegate(typeof(Func<GraphServiceClient, Task<object?>>), this, methodInfo);
                    Func<GraphServiceClient, Task<object?>>? func = d as Func<GraphServiceClient, Task<object?>>;
                    if (func is not null)
                    {
                        KeyValuePair<string, APIFunction> kv = FuncExtends.ToAPIFunction(func, id, this.logger);
                        this.cache.Add(kv);
                        yield return kv;
                    }
                }
            }
        }
        #region API function
        [API("AgreementAcceptances.Get")]
        private async Task<object?> GetAgreementAcceptance(GraphServiceClient client) => await client.AgreementAcceptances.GetAsync();

        [API("Admin.Get")]
        private async Task<object?> GetAdmin(GraphServiceClient client) => await client.Admin.GetAsync();

        [API("Agreements.Get")]
        private async Task<object?> GetAgreements(GraphServiceClient client) => await client.Agreements.GetAsync();

        [API("AppCatalogs.Get")]
        private async Task<object?> GetAppCatalogs(GraphServiceClient client) => await client.AppCatalogs.GetAsync();

        [API("ApplicationTemplates.Get")]
        private async Task<object?> GetApplicationTemplates(GraphServiceClient client) => await client.ApplicationTemplates.GetAsync();

        [API("Applications.Get")]
        private async Task<object?> GetApplications(GraphServiceClient client) => await client.Applications.GetAsync();

        [API("AuditLogs.Get")]
        private async Task<object?> GetAuditLogs(GraphServiceClient client) => await client.AuditLogs.GetAsync();

        [API("AuthenticationMethodConfigurations.Get")]
        private async Task<object?> GetAuthenticationMethodConfigurations(GraphServiceClient client) => await client.AuthenticationMethodConfigurations.GetAsync();

        [API("AuthenticationMethodsPolicy.Get")]
        private async Task<object?> GetAuthenticationMethodsPolicy(GraphServiceClient client) => await client.AuthenticationMethodsPolicy.GetAsync();

        [API("CertificateBasedAuthConfiguration.Get")]
        private async Task<object?> GetCertificateBasedAuthConfiguration(GraphServiceClient client) => await client.CertificateBasedAuthConfiguration.GetAsync();

        [API("Chats.Get")]
        private async Task<object?> GetChats(GraphServiceClient client) => await client.Chats.GetAsync();

        [API("Communications.Get")]
        private async Task<object?> GetCommunications(GraphServiceClient client) => await client.Communications.GetAsync();

        [API("Compliance.Get")]
        private async Task<object?> GetCompliance(GraphServiceClient client) => await client.Compliance.GetAsync();

        [API("Connections.Get")]
        private async Task<object?> GetConnections(GraphServiceClient client) => await client.Connections.GetAsync();

        [API("Contacts.Get")]
        private async Task<object?> GetContacts(GraphServiceClient client) => await client.Contacts.GetAsync();

        [API("DataPolicyOperations.Get")]
        private async Task<object?> GetDataPolicyOperations(GraphServiceClient client) => await client.DataPolicyOperations.GetAsync();

        [API("DeviceAppManagement.Get")]
        private async Task<object?> GetDeviceAppManagement(GraphServiceClient client) => await client.DeviceAppManagement.GetAsync();

        [API("DeviceManagement.Get")]
        private async Task<object?> GetDeviceManagement(GraphServiceClient client) => await client.DeviceManagement.GetAsync();

        [API("Devices.Get")]
        private async Task<object?> GetDevies(GraphServiceClient client) => await client.Devices.GetAsync();

        [API("Direcory.Get")]
        private async Task<object?> GetDirecory(GraphServiceClient client) => await client.Directory.GetAsync();

        [API("DirectoryObjects.Get")]
        private async Task<object?> GetDirectoryObjects(GraphServiceClient client) => await client.DirectoryObjects.GetAsync();

        [API("DirectoryRoleTemplates.Get")]
        private async Task<object?> GetDirectoryRoleTemplates(GraphServiceClient client) => await client.DirectoryRoleTemplates.GetAsync();

        [API("DirectoryRoles.Get")]
        private async Task<object?> GetDirectoryRoles(GraphServiceClient client) => await client.DirectoryRoles.GetAsync();

        [API("DomainDnsRecords.Get")]
        private async Task<object?> GetDomainDnsRecords(GraphServiceClient client) => await client.DomainDnsRecords.GetAsync();

        [API("Domains.Get")]
        private async Task<object?> GetDomains(GraphServiceClient client) => await client.Domains.GetAsync();

        [API("Drives.Get")]
        private async Task<object?> GetDrives(GraphServiceClient client) => await client.Drives.GetAsync();

        [API("Education.Get")]
        private async Task<object?> GetEducation(GraphServiceClient client) => await client.Education.GetAsync();

        [API("EmployeeExperience.Get")]
        private async Task<object?> GetEmployeeExperience(GraphServiceClient client) => await client.EmployeeExperience.GetAsync();

        [API("External.Get")]
        private async Task<object?> GetExternal(GraphServiceClient client) => await client.External.GetAsync();

        [API("FilterOperators.Get")]
        private async Task<object?> GetFilterOperators(GraphServiceClient client) => await client.FilterOperators.GetAsync();

        [API("Functions.Get")]
        private async Task<object?> GetFunctions(GraphServiceClient client) => await client.Functions.GetAsync();

        [API("GroupLifecyclePolicies.Get")]
        private async Task<object?> GetGroupLifecyclePolicies(GraphServiceClient client) => await client.GroupLifecyclePolicies.GetAsync();

        [API("GroupSettingTemplates.Get")]
        private async Task<object?> GetGroupSettingTemplates(GraphServiceClient client) => await client.GroupSettingTemplates.GetAsync();

        [API("GroupSetings.Get")]
        private async Task<object?> GetGroupSettings(GraphServiceClient client) => await client.GroupSettings.GetAsync();

        [API("Groups.Get")]
        private async Task<object?> GetGroups(GraphServiceClient client) => await client.Groups.GetAsync();

        [API("Identity.Get")]
        private async Task<object?> GetIdentity(GraphServiceClient client) => await client.Identity.GetAsync();

        [API("IdentityGovernance.Get")]
        private async Task<object?> GetIdentityGovernance(GraphServiceClient client) => await client.IdentityGovernance.GetAsync();

        [API("IdentityProtection.Get")]
        private async Task<object?> GetIdentityProtection(GraphServiceClient client) => await client.IdentityProtection.GetAsync();

        [Obsolete("GraphServiceClient.IdentityProviders.GetAsync is obsolete.")]
        [API("IdentityProviders.Get")]
        private async Task<object?> GetIdentityProviders(GraphServiceClient client) => await client.IdentityProviders.GetAsync();

        [API("InformationProtecion.Get")]
        private async Task<object?> GetInfomationProtecion(GraphServiceClient client) => await client.InformationProtection.GetAsync();

        [API("Invitations.Get")]
        private async Task<object?> GetInvitations(GraphServiceClient client) => await client.Invitations.GetAsync();

        [API("OAuth2PermissionGrants.Get")]
        private async Task<object?> GetOAuth2PermissionGrants(GraphServiceClient client) => await client.Oauth2PermissionGrants.GetAsync();

        [API("Organization.Get")]
        private async Task<object?> GetOrganization(GraphServiceClient client) => await client.Organization.GetAsync();

        [API("PermissionGrants.Get")]
        private async Task<object?> GetPermissionGrants(GraphServiceClient client) => await client.PermissionGrants.GetAsync();

        [API("Places.Count.Get")]
        private async Task<object?> GetPlacesCount(GraphServiceClient client) => await client.Places.Count.GetAsync();

        [API("Places.GraphRoom.Get")]
        private async Task<object?> GetPlacesGraphRoom(GraphServiceClient client) => await client.Places.GraphRoom.GetAsync();

        [API("Places.GraphRoom.Count.Get")]
        private async Task<object?> GetPlacesGraphRoomCount(GraphServiceClient client) => await client.Places.GraphRoom.Count.GetAsync();

        [API("Places.GraphRoomList.Get")]
        private async Task<object?> GetPlacesGraphRoomList(GraphServiceClient client) => await client.Places.GraphRoomList.GetAsync();

        [API("Places.GraphRoomList.Count.Get")]
        private async Task<object?> GetPlacesGraphRoomListCount(GraphServiceClient client) => await client.Places.GraphRoomList.Count.GetAsync();

        [API("Planner.Get")]
        private async Task<object?> GetPlanner(GraphServiceClient client) => await client.Planner.GetAsync();

        [API("Planner.Buckets.Get")]
        private async Task<object?> GetPlannerBuckets(GraphServiceClient client) => await client.Planner.Buckets.GetAsync();

        [API("Planner.Buckets.Count.Get")]
        private async Task<object?> GetPlannerBucketsC(GraphServiceClient client) => await client.Planner.Buckets.Count.GetAsync();

        [API("Planner.Plans.Get")]
        private async Task<object?> GetPlannerPlans(GraphServiceClient client) => await client.Planner.Plans.GetAsync();

        [API("Planner.Plans.Count.Get")]
        private async Task<object?> GetPlannerPlansCount(GraphServiceClient client) => await client.Planner.Plans.Count.GetAsync();

        [API("Planner.Tasks.Get")]
        private async Task<object?> GetPlannerTasks(GraphServiceClient client) => await client.Planner.Tasks.GetAsync();

        [API("Planner.Tasks.Count.Get")]
        private async Task<object?> GetPlannerTasksCount(GraphServiceClient client) => await client.Planner.Tasks.Count.GetAsync();

        [API("Policies.Get")]
        private async Task<object?> GetPolicies(GraphServiceClient client) => await client.Policies.GetAsync();

        [API("Print.Get")]
        private async Task<object?> GetPrint(GraphServiceClient client) => await client.Print.GetAsync();

        [API("Privacy.Get")]
        private async Task<object?> GetPrivacy(GraphServiceClient client) => await client.Privacy.GetAsync();

        [API("Reports.Get")]
        private async Task<object?> GetReports(GraphServiceClient client) => await client.Reports.GetAsync();

        [API("RoleManagement.Get")]
        private async Task<object?> GetRoleManagement(GraphServiceClient client) => await client.RoleManagement.GetAsync();

        [API("SchemaExtensions.Get")]
        private async Task<object?> GetSchemaExtensions(GraphServiceClient client) => await client.SchemaExtensions.GetAsync();

        [API("ScopedRoleMemberships.Get")]
        private async Task<object?> GetScopedRoleMemberships(GraphServiceClient client) => await client.ScopedRoleMemberships.GetAsync();

        [API("Search.Get")]
        private async Task<object?> GetSearch(GraphServiceClient client) => await client.Search.GetAsync();

        [API("Search.Acronyms.Get")]
        private async Task<object?> GetSearchAcronyms(GraphServiceClient client) => await client.Search.Acronyms.GetAsync();

        [API("Search.Acronyms.Count.Get")]
        private async Task<object?> GetSearchAcronymsCount(GraphServiceClient client) => await client.Search.Acronyms.Count.GetAsync();

        [API("Search.Bookmarks.Get")]
        private async Task<object?> GetSearchBookmarks(GraphServiceClient client) => await client.Search.Bookmarks.GetAsync();

        [API("Search.Bookmarks.Count.Get")]
        private async Task<object?> GetSearchBookmarksCount(GraphServiceClient client) => await client.Search.Bookmarks.Count.GetAsync();

        [API("Search.Qnas.Get")]
        private async Task<object?> GetSearchQnas(GraphServiceClient client) => await client.Search.Qnas.GetAsync();

        [API("Search.Qnas.Count.Get")]
        private async Task<object?> GetSearhQnasCount(GraphServiceClient client) => await client.Search.Qnas.Count.GetAsync();

        [API("Security.Get")]
        private async Task<object?> GetSecurity(GraphServiceClient client) => await client.Security.GetAsync();

        [Obsolete("Security.Alerts.GetAsync is obsolete.")]
        [API("Security.Alerts.Get")]
        private async Task<object?> GetSecurityCount(GraphServiceClient client) => await client.Security.Alerts.GetAsync();

        [Obsolete("Security.Alerts.Count.GetAsync is obsolete.")]
        [API("Security.Alerts.Count.Get")]
        private async Task<object?> GetSecurityAlertsCount(GraphServiceClient client) => await client.Security.Alerts.Count.GetAsync();

        [API("Security.Alerts_v2.Get")]
        private async Task<object?> GetSecurityAlertsV2(GraphServiceClient client) => await client.Security.Alerts_v2.GetAsync();

        [API("Security.Alerts_v2.Count.Get")]
        private async Task<object?> GetSecurityAlertsV2Count(GraphServiceClient client) => await client.Security.Alerts_v2.Count.GetAsync();

        [API("Security.AttackSimulation.Get")]
        private async Task<object?> GetSecurityAttackSimulation(GraphServiceClient client) => await client.Security.AttackSimulation.GetAsync();

        [API("Security.AttackSimulation.EndUserNotifications.Get")]
        private async Task<object?> GetSecurityAttackSimulationEndUserNotifications(GraphServiceClient client) => await client.Security.AttackSimulation.EndUserNotifications.GetAsync();

        [API("Security.AttackSimulation.EndUserNotifications.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationEndUserNotificationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.EndUserNotifications.Count.GetAsync();

        [API("Security.AttackSimulation.LandingPages.Get")]
        private async Task<object?> GetSecurityAttackSimulationLandingPages(GraphServiceClient client) => await client.Security.AttackSimulation.LandingPages.GetAsync();

        [API("Security.AttackSimulation.LandingPages.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationLandingPagesCount(GraphServiceClient client) => await client.Security.AttackSimulation.LandingPages.Count.GetAsync();

        [API("Security.AttackSimulation.LoginPages.Get")]
        private async Task<object?> GetSecurityAttackSimulationLoginPages(GraphServiceClient client) => await client.Security.AttackSimulation.LoginPages.GetAsync();

        [API("Security.AttackSimulation.LoginPages.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationLoginPagesCount(GraphServiceClient client) => await client.Security.AttackSimulation.LoginPages.Count.GetAsync();

        [API("Security.AttackSimulation.Operations.Get")]
        private async Task<object?> GetSecurityAttackSimulationOperations(GraphServiceClient client) => await client.Security.AttackSimulation.Operations.GetAsync();

        [API("Security.AttackSimulation.Operations.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationOperationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Operations.Count.GetAsync();

        [API("Security.AttackSimulation.Payloads.Get")]
        private async Task<object?> GetSecurityAttackSimulationPayloads(GraphServiceClient client) => await client.Security.AttackSimulation.Payloads.GetAsync();

        [API("Security.AttackSimulation.Payloads.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationPayloadsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Payloads.Count.GetAsync();

        [API("Security.AttackSimulation.SimulationAutomations.Get")]
        private async Task<object?> GetSecurityAttackSimulationSimulationAutomations(GraphServiceClient client) => await client.Security.AttackSimulation.SimulationAutomations.GetAsync();

        [API("Security.AttackSimulation.SimulationAutomations.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationSimulationAutomationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.SimulationAutomations.Count.GetAsync();

        [API("Security.AttackSimulation.Simulations.Get")]
        private async Task<object?> GetSecurityAttackSimulationSimulations(GraphServiceClient client) => await client.Security.AttackSimulation.Simulations.GetAsync();

        [API("Security.AttackSimulation.Simulations.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationSimulationsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Simulations.Count.GetAsync();

        [API("Security.AttackSimulation.Trainings.Get")]
        private async Task<object?> GetSecurityAttackSimulationTrendings(GraphServiceClient client) => await client.Security.AttackSimulation.Trainings.GetAsync();

        [API("Security.AttackSimulation.Trainings.Count.Get")]
        private async Task<object?> GetSecurityAttackSimulationTrendingsCount(GraphServiceClient client) => await client.Security.AttackSimulation.Trainings.Count.GetAsync();

        [API("Security.Cases.Get")]
        private async Task<object?> GetSecurityCases(GraphServiceClient client) => await client.Security.Cases.GetAsync();

        [API("Security.Cases.EdiscoveryCases.Get")]
        private async Task<object?> GetSecurityCasesEdiscoveryCases(GraphServiceClient client) => await client.Security.Cases.EdiscoveryCases.GetAsync();

        [API("Security.Cases.EdiscoveryCases.Count.Get")]
        private async Task<object?> GetSecurityCasesEdiscoveryCasesCount(GraphServiceClient client) => await client.Security.Cases.EdiscoveryCases.Count.GetAsync();

        [API("Security.Incidents.Get")]
        private async Task<object?> GetSecurityIncidents(GraphServiceClient client) => await client.Security.Incidents.GetAsync();

        [API("Security.Incidents.Count.Get")]
        private async Task<object?> GetSecurityIncidentsCount(GraphServiceClient client) => await client.Security.Incidents.Count.GetAsync();

        [API("Security.SecureScoreControlProfiles.Get")]
        private async Task<object?> GetSecuritySecureScoreControlProfiles(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.GetAsync();

        [API("Security.SecureScoreControlProfiles.Count.Get")]
        private async Task<object?> GetSecuritySecureScoreControlProfilesCount(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.Count.GetAsync();

        [API("Security.SecureScores.Get")]
        private async Task<object?> GetSecurityScores(GraphServiceClient client) => await client.Security.SecureScores.GetAsync();

        [API("Security.SecureScores.Count.Get")]
        private async Task<object?> GetSecuritySecureScoresCount(GraphServiceClient client) => await client.Security.SecureScores.Count.GetAsync();

        [API("Security.SubjectRightsRequests.Get")]
        private async Task<object?> GetSecuritySubjectRightsRequests(GraphServiceClient client) => await client.Security.SubjectRightsRequests.GetAsync();

        [API("Security.SubjectRightsRequests.Count.Get")]
        private async Task<object?> GetSecuritySubjectRightsRequestsCount(GraphServiceClient client) => await client.Security.SubjectRightsRequests.Count.GetAsync();

        [API("Security.ThreatIntelligence.Get")]
        private async Task<object?> GetSecurityThreatIntelligence(GraphServiceClient client) => await client.Security.ThreatIntelligence.GetAsync();

        [API("Security.ThreatIntelligence.ArticleIndicators.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceArticleIndicators(GraphServiceClient client) => await client.Security.ThreatIntelligence.ArticleIndicators.GetAsync();

        [API("Security.ThreatIntelligence.ArticleIndicators.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceArticleIndicatorsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.ArticleIndicators.Count.GetAsync();

        [API("Security.ThreatIntelligence.Articles.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceArticles(GraphServiceClient client) => await client.Security.ThreatIntelligence.Articles.GetAsync();

        [API("Security.ThreatIntelligence.Articles.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceArticlesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Articles.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostComponents.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostComponents(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostComponents.GetAsync();

        [API("Security.ThreatIntelligence.HostComponents.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostComponentsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostComponents.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostCookies.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostCookies(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostCookies.GetAsync();

        [API("Security.ThreatIntelligence.HostCookies.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostCookiesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostCookies.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostPairs.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostPairs(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPairs.GetAsync();

        [API("Security.ThreatIntelligence.HostPairs.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostPairsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPairs.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostPorts.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostPorts(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPorts.GetAsync();

        [API("Security.ThreatIntelligence.HostPorts.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostPortsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostPorts.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostSslCertificates.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostSslCertificates(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostSslCertificates.GetAsync();

        [API("Security.ThreatIntelligence.HostSslCertificates.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostSslCertificatesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostSslCertificates.Count.GetAsync();

        [API("Security.ThreatIntelligence.HostTrackers.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostTrackers(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostTrackers.GetAsync();

        [API("Security.ThreatIntelligence.HostTrackers.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostTrackersCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.HostTrackers.Count.GetAsync();

        [API("Security.ThreatIntelligence.Hosts.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHosts(GraphServiceClient client) => await client.Security.ThreatIntelligence.Hosts.GetAsync();

        [API("Security.ThreatIntelligence.Hosts.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceHostsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Hosts.Count.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfiles.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceIntelProfiles(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelProfiles.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfiles.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceIntelProfilesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelProfiles.Count.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfileIndicators.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceIntelligenceProfileIndicators(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelligenceProfileIndicators.GetAsync();

        [API("Security.ThreatIntelligence.IntelligenceProfileIndicators.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceIntelligenceProfileIndicatorsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.IntelligenceProfileIndicators.Count.GetAsync();

        [API("Security.ThreatIntelligence.PassiveDnsRecords.Get")]
        private async Task<object?> GetSecurityThreatIntelligencePassiveDnsRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.PassiveDnsRecords.GetAsync();

        [API("Security.ThreatIntelligence.PassiveDnsRecords.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligencePassiveDnsRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.PassiveDnsRecords.Count.GetAsync();

        [API("Security.ThreatIntelligence.SslCertificates.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceSslCertificates(GraphServiceClient client) => await client.Security.ThreatIntelligence.SslCertificates.GetAsync();

        [API("Security.ThreatIntelligence.SslCertificates.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceSslCertificatesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.SslCertificates.Count.GetAsync();

        [API("Security.ThreatIntelligence.Subdomains.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceSubdomains(GraphServiceClient client) => await client.Security.ThreatIntelligence.Subdomains.GetAsync();

        [API("Security.ThreatIntelligence.Subdomains.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceSubdomainsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Subdomains.Count.GetAsync();

        [API("Security.ThreatIntelligence.Vulnerabilities.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceVulnerabilities(GraphServiceClient client) => await client.Security.ThreatIntelligence.Vulnerabilities.GetAsync();

        [API("Security.ThreatIntelligence.Vulnerabilities.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceVulnerabilitiesCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.Vulnerabilities.Count.GetAsync();

        [API("Security.ThreatIntelligence.WhoisHistoryRecords.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceWhoisHistoryRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisHistoryRecords.GetAsync();

        [API("Security.ThreatIntelligence.WhoisHistoryRecords.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceWhoisHistoryRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisHistoryRecords.Count.GetAsync();

        [API("Security.ThreatIntelligence.WhoisRecords.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceWhoisRecords(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisRecords.GetAsync();

        [API("Security.ThreatIntelligence.WhoisRecords.Count.Get")]
        private async Task<object?> GetSecurityThreatIntelligenceWhoisRecordsCount(GraphServiceClient client) => await client.Security.ThreatIntelligence.WhoisRecords.Count.GetAsync();

        [API("Security.TriggerTypes.Get")]
        private async Task<object?> GetSecurityTriggerTypes(GraphServiceClient client) => await client.Security.TriggerTypes.GetAsync();

        [API("Security.TriggerTypes.RetentionEventTypes.Get")]
        private async Task<object?> GetSecurityTriggerTypesRetentionEventTypes(GraphServiceClient client) => await client.Security.TriggerTypes.RetentionEventTypes.GetAsync();

        [API("Security.TriggerTypes.RetentionEventTypes.Count.Get")]
        private async Task<object?> GetSecurityTriggerTypesRetentionEventTypesCount(GraphServiceClient client) => await client.Security.TriggerTypes.RetentionEventTypes.Count.GetAsync();

        [API("Security.Triggers.Get")]
        private async Task<object?> GetSecurityTriggers(GraphServiceClient client) => await client.Security.Triggers.GetAsync();

        [API("Security.Triggers.RetentionEvents.Get")]
        private async Task<object?> GetSecurityTriggersRetensionEvents(GraphServiceClient client) => await client.Security.Triggers.RetentionEvents.GetAsync();

        [API("Security.Triggers.RetentionEvents.Count.Get")]
        private async Task<object?> GetSecurityTriggersRetensionEventsCount(GraphServiceClient client) => await client.Security.Triggers.RetentionEvents.Count.GetAsync();

        [API("ServicePrincipals.Get")]
        private async Task<object?> GetServicePrincipals(GraphServiceClient client) => await client.ServicePrincipals.GetAsync();

        [API("ServicePrincipals.Count.Get")]
        private async Task<object?> GetServicePrincipalsCount(GraphServiceClient client) => await client.ServicePrincipals.Count.GetAsync();

        [API("ServicePrincipals.Delta.Get")]
        private async Task<object?> GetServicePrincipalsDelta(GraphServiceClient client) => await client.ServicePrincipals.Delta.GetAsDeltaGetResponseAsync();

        [API("Shares.Get")]
        private async Task<object?> GetShares(GraphServiceClient client) => await client.Shares.GetAsync();

        [API("Shares.Count.Get")]
        private async Task<object?> GetSharesCount(GraphServiceClient client) => await client.Shares.Count.GetAsync();

        [API("Sites.Get")]
        private async Task<object?> GetSites(GraphServiceClient client) => await client.Sites.GetAsync();

        [API("Sites.Count.Get")]
        private async Task<object?> GetSitesCount(GraphServiceClient client) => await client.Sites.Count.GetAsync();

        [API("Sites.Add.Get")]
        private async Task<object?> GetSitesDelta(GraphServiceClient client) => await client.Sites.Delta.GetAsDeltaGetResponseAsync();

        [API("Sites.GetAllSites.Get")]
        private async Task<object?> GetSitesGetAllSites(GraphServiceClient client) => await client.Sites.GetAllSites.GetAsGetAllSitesGetResponseAsync();

        [API("Solutions.Get")]
        private async Task<object?> GetSolutions(GraphServiceClient client) => await client.Solutions.GetAsync();

        [API("Solutions.BookingBusinesses.Get")]
        private async Task<object?> GetSolutionsBookingBusinesses(GraphServiceClient client) => await client.Solutions.BookingBusinesses.GetAsync();

        [API("Solutions.BookingBusinesses.Count.Get")]
        private async Task<object?> GetSolutionsBookingBusinessesCount(GraphServiceClient client) => await client.Solutions.BookingBusinesses.Count.GetAsync();

        [API("Solutions.BookingCurrencies.Get")]
        private async Task<object?> GetSolutionsBookingCurrencies(GraphServiceClient client) => await client.Solutions.BookingCurrencies.GetAsync();

        [API("Solutions.BookingCurrencies.Count.Get")]
        private async Task<object?> GetSolutionsBookingCurrenciesCount(GraphServiceClient client) => await client.Solutions.BookingCurrencies.Count.GetAsync();

        [API("Solutions.VirtualEvents.Get")]
        private async Task<object?> GetSolutionsVirtualEvents(GraphServiceClient client) => await client.Solutions.VirtualEvents.GetAsync();

        [API("Solutions.VirtualEvents.Events.Get")]
        private async Task<object?> GetSolutionsVirtualEventsEvents(GraphServiceClient client) => await client.Solutions.VirtualEvents.Events.GetAsync();

        [API("Solutions.VirtualEvents.Events.Count.Get")]
        private async Task<object?> GetSolutionsVirtualEventsEventsCount(GraphServiceClient client) => await client.Solutions.VirtualEvents.Events.Count.GetAsync();

        [API("Solutions.VirtualEvents.Webinars.Get")]
        private async Task<object?> GetSolutionsVirtualEventsWebinars(GraphServiceClient client) => await client.Solutions.VirtualEvents.Webinars.GetAsync();

        [API("Solutions.VirtualEvents.Webinars.Count.Get")]
        private async Task<object?> GetSolutionsVirtualEventsWebinarsCount(GraphServiceClient client) => await client.Solutions.VirtualEvents.Webinars.Count.GetAsync();

        [API("SubscribedSkus.Get")]
        private async Task<object?> GetSubscribedSkus(GraphServiceClient client) => await client.SubscribedSkus.GetAsync();

        [API("Subscriptions.Get")]
        private async Task<object?> GetSubscriptions(GraphServiceClient client) => await client.Subscriptions.GetAsync();

        [API("Teams.Get")]
        private async Task<object?> GetTeams(GraphServiceClient client) => await client.Teams.GetAsync();

        [API("Teams.Count.Get")]
        private async Task<object?> GetTeamsCount(GraphServiceClient client) => await client.Teams.Count.GetAsync();

        [API("Teams.GetAllMessages.Get")]
        private async Task<object?> GetTeamsGetAllMessages(GraphServiceClient client) => await client.Teams.GetAllMessages.GetAsGetAllMessagesGetResponseAsync();

        [API("TeamsTemplates.Get")]
        private async Task<object?> GetTeamsTemplates(GraphServiceClient client) => await client.TeamsTemplates.GetAsync();

        [API("TeamsTemplates.Count.Get")]
        private async Task<object?> GetTeamsTemplatesCount(GraphServiceClient client) => await client.TeamsTemplates.Count.GetAsync();

        [API("Teamwork.Get")]
        private async Task<object?> GetTeamwork(GraphServiceClient client) => await client.Teamwork.GetAsync();

        [API("Teamwork.DeletedChats.Get")]
        private async Task<object?> GetTeamworkDeletedChats(GraphServiceClient client) => await client.Teamwork.DeletedChats.GetAsync();

        [API("Teamwork.DeletedChats.Count.Get")]
        private async Task<object?> GetTeamworkDeletedChatsCount(GraphServiceClient client) => await client.Teamwork.DeletedChats.Count.GetAsync();

        [API("Teamwork.DeletedTeams.Get")]
        private async Task<object?> GetTeamworkDeletedTeams(GraphServiceClient client) => await client.Teamwork.DeletedTeams.GetAsync();

        [API("Teamwork.DeletedTeams.Count.Get")]
        private async Task<object?> GetTeamworkDeletedTeamsCount(GraphServiceClient client) => await client.Teamwork.DeletedTeams.Count.GetAsync();

        [API("Teamwork.DeletedTeams.GetAllMessages.Get")]
        private async Task<object?> GetTeamworkDeletedTeamsGetAllMessages(GraphServiceClient client) => await client.Teamwork.DeletedTeams.GetAllMessages.GetAsGetAllMessagesGetResponseAsync();

        [API("Teamwork.TeamsAppSettings.Get")]
        private async Task<object?> GetTeamworkTeamsAppSettings(GraphServiceClient client) => await client.Teamwork.TeamsAppSettings.GetAsync();

        [API("Teamwork.WorkforceIntegrations.Get")]
        private async Task<object?> GetTeamworkWorkforceIntegrations(GraphServiceClient client) => await client.Teamwork.WorkforceIntegrations.GetAsync();

        [API("Teamwork.WorkforceIntegrations.Count.Get")]
        private async Task<object?> GetTeamworkWorkforceIntegrationsCount(GraphServiceClient client) => await client.Teamwork.WorkforceIntegrations.Count.GetAsync();

        [API("TenantRelationships.Get")]
        private async Task<object?> GetTenantRelationships(GraphServiceClient client) => await client.TenantRelationships.GetAsync();

        [API("TenantRelationships.DelegatedAdminCustomers.Get")]
        private async Task<object?> GetTenantRelationshipsDelegatedAdminCustomers(GraphServiceClient client) => await client.TenantRelationships.DelegatedAdminCustomers.GetAsync();

        [API("TenantRelationships.DelegatedAdminCustomers.Count.Get")]
        private async Task<object?> GetTenantRelationshipsDelegatedAdminCustomersCount(GraphServiceClient client) => await client.TenantRelationships.DelegatedAdminCustomers.Count.GetAsync();

        [API("Users.Get")]
        private async Task<object?> GetUsers(GraphServiceClient client) => await client.Users.GetAsync();

        [API("Users.Count.Get")]
        private async Task<object?> GetUsersCount(GraphServiceClient client) => await client.Users.Count.GetAsync();

        [API("Users.Delta.Get")]
        private async Task<object?> GetUsersDelta(GraphServiceClient client) => await client.Users.Delta.GetAsDeltaGetResponseAsync();
        #endregion
    }
}
