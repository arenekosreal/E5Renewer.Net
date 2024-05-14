namespace E5Renewer.Models.Modules
{
    /// <summary>The api interface of checking module.</summary>
    public interface IModulesChecker : IAspNetModule
    {
        /// <summary>Check the module.</summary>
        /// <param name="module">The module to check.</param>
        public void CheckModules(IModule module);
    }
}
