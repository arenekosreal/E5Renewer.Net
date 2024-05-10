namespace E5Renewer.Modules
{
    /// <summary>The api interface of loadable module.</summary>
    public interface IModule
    {
        private readonly static SemVer targetSemVer = new SemVer(0, 1, 0);
        /// <value>The name of the module.</value>
        public string name { get; }
        /// <value>The author of the module.</value>
        public string author { get; }
        /// <value>The api version of the module.</value>
        public SemVer apiVersion { get; }
        /// <value> If the module is deprecated.</value>
        public bool isDeprecated { get => !apiVersion.IsCompatibleTo(targetSemVer); }
    }
}
