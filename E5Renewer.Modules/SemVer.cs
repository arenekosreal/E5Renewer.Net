namespace E5Renewer.Modules
{
    public struct SemVer
    {
        public SemVer(uint major, uint minor, uint patch)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
        }
        public uint major { get; }
        public uint minor { get; }
        public uint patch { get; }
        public override string ToString()
        {
            return $"{this.major}.{this.minor}.{this.patch}";
        }
        public static SemVer operator +(SemVer a, SemVer b)
        {
            return new SemVer(
                a.major + b.major,
                a.minor + b.minor,
                a.patch + b.patch
            );
        }
        public static SemVer operator -(SemVer a, SemVer b)
        {
            uint major = a.major > b.major ? a.major - b.major : 0;
            uint minor = a.minor > b.minor ? a.minor - b.minor : 0;
            uint patch = a.patch > b.patch ? a.patch - b.patch : 0;
            return new SemVer(major, minor, patch);
        }
        public static bool operator >(SemVer a, SemVer b)
        {
            return a.major > b.major ||
                a.minor > b.minor ||
                a.patch > b.patch;
        }
        public static bool operator <(SemVer a, SemVer b)
        {
            return a.major < b.major ||
                a.minor < b.minor ||
                a.patch < b.patch;
        }
        public static bool operator ==(SemVer a, SemVer b)
        {
            return a.major == b.major &&
                a.minor == b.minor &&
                a.patch == b.patch;
        }
        public static bool operator !=(SemVer a, SemVer b)
        {
            return a.major != b.major ||
                a.minor != b.minor ||
                a.patch != b.patch;
        }
        public override bool Equals(object? obj) => obj is SemVer semVerObj && this.Equals(semVerObj);
        public override int GetHashCode() => this.ToString().GetHashCode();
        public bool IsCompatibleTo(SemVer target)
        {
            return this.major == target.major &&
                this.minor == target.minor;
        }
    }
}
