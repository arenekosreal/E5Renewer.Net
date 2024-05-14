namespace E5Renewer.Models.Modules
{
    /// <summary>The api interface of loadable module.</summary>
    public interface IModule
    {
        private static readonly SemanticVersioning.Range targetVersions =
            typeof(IModule).Assembly.GetName().Version?.ToSemanticVersion().GetCompatibleVersionRange() ?? new("~0.1");

        /// <value>The name of the module.</value>
        public string name { get; }

        /// <value>The author of the module.</value>
        public string author { get; }

        /// <value>The api version of the module.</value>
        public SemanticVersioning.Version apiVersion { get; }

        /// <value> If the module is deprecated.</value>
        public bool isDeprecated { get => !IModule.targetVersions.IsSatisfied(this.apiVersion); }
    }
}
