namespace E5Renewer.Modules
{
    /// <summary>The struct represents a version</summary>
    /// <seealso href="https://semver.org/">SemVer</seealso>
    /// <remarks>This only supports <c>major.minor.patch</c> pattern.</remarks>
    public struct SemVer
    {
        /// <summary>Make a <c>SemVer</c> by major, minor, and patch.</summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="patch">The patch version number.</param>
        public SemVer(uint major, uint minor, uint patch)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
        }
        /// <value>The major number.</value>
        public uint major { get; }
        /// <value>The minor number.</value>
        public uint minor { get; }
        /// <value>The patch number.</value>
        public uint patch { get; }
        /// <summary>Convert <c>SemVer</c> to string in <c>major.minor.patch</c> format.</summary>
        public override string ToString()
        {
            return $"{this.major}.{this.minor}.{this.patch}";
        }
        /// <inheritdoc/>
        public static SemVer operator +(SemVer a, SemVer b)
        {
            return new SemVer(
                a.major + b.major,
                a.minor + b.minor,
                a.patch + b.patch
            );
        }
        /// <inheritdoc/>
        public static SemVer operator -(SemVer a, SemVer b)
        {
            uint major = a.major > b.major ? a.major - b.major : 0;
            uint minor = a.minor > b.minor ? a.minor - b.minor : 0;
            uint patch = a.patch > b.patch ? a.patch - b.patch : 0;
            return new SemVer(major, minor, patch);
        }
        /// <inheritdoc/>
        public static bool operator >(SemVer a, SemVer b)
        {
            return a.major > b.major ||
                a.minor > b.minor ||
                a.patch > b.patch;
        }
        /// <inheritdoc/>
        public static bool operator <(SemVer a, SemVer b)
        {
            return a.major < b.major ||
                a.minor < b.minor ||
                a.patch < b.patch;
        }
        /// <inheritdoc/>
        public static bool operator ==(SemVer a, SemVer b)
        {
            return a.major == b.major &&
                a.minor == b.minor &&
                a.patch == b.patch;
        }
        /// <inheritdoc/>
        public static bool operator !=(SemVer a, SemVer b)
        {
            return a.major != b.major ||
                a.minor != b.minor ||
                a.patch != b.patch;
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is SemVer semVerObj && this.Equals(semVerObj);
        /// <inheritdoc/>
        public override int GetHashCode() => this.ToString().GetHashCode();
        /// <summary>Is the <c>SemVer</c> compatible to another <c>SemVer</c> given.</summary>
        /// <param name="target">The <c>SemVer</c> to compare.</param>
        /// <returns>If the <c>SemVer</c> is compatible to target.</returns>
        public bool IsCompatibleTo(SemVer target)
        {
            return this.major == target.major &&
                this.minor == target.minor;
        }
    }
}
