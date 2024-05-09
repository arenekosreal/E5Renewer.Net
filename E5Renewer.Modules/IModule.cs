namespace E5Renewer.Modules
{
    public interface IModule
    {
        private readonly static SemVer targetSemVer = new SemVer(0,1,0);
        public string name{ get; }
        public string author{ get; }
        public SemVer apiVersion { get; }
        public bool IsDeprecated { get => apiVersion < targetSemVer; }
    }
}
