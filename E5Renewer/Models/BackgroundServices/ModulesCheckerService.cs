using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;

namespace E5Renewer.Models.BackgroundServices
{
    /// <summary>class to check modules passed to AspNet.Core part.</summary>
    public class ModulesCheckerService : BackgroundService
    {
        private readonly IEnumerable<IGraphAPICaller> graphAPICallers;
        private readonly IEnumerable<IModulesChecker> modulesCheckers;
        private readonly IEnumerable<IAspNetModule> aspNetModules;

        /// <summary>Initialize <c>ModulesCheckerService</c> with parameters given.</summary>
        /// <param name="graphAPICallers">All GraphAPICallers modules.</param>
        /// <param name="modulesCheckers">All ModulesChecker modules.</param>
        /// <param name="aspNetModules">All modules do not belongs to apicaller or modules checker.</param>
        /// <remarks>All the parameters should be injected by Asp.Net Core.</remarks>
        public ModulesCheckerService(IEnumerable<IGraphAPICaller> graphAPICallers, IEnumerable<IModulesChecker> modulesCheckers, IEnumerable<IAspNetModule> aspNetModules)
        {
            this.graphAPICallers = graphAPICallers;
            this.modulesCheckers = modulesCheckers;
            this.aspNetModules = aspNetModules;
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken token)
        {
            List<IModule> modules = new List<IModule>();
            modules.AddRange(this.graphAPICallers);
            modules.AddRange(this.aspNetModules);
            foreach (IModule module in modules)
            {
                foreach (IModulesChecker checker in this.modulesCheckers)
                {
                    checker.CheckModules(module);
                }
            }
            return Task.CompletedTask;
        }
    }
}
