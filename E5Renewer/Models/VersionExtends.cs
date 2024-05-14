namespace E5Renewer.Models
{
    /// <summary>Extended methods to
    /// <see cref="Version">System.Version</see> and
    /// <see cref="SemanticVersioning.Version">SemanticVersioning.Version</see>.
    /// </summary>
    public static class VersionExtends
    {
        /// <summary>
        /// Convert
        /// <see cref="Version">System.Version</see> to
        /// <see cref="SemanticVersioning.Version">SemanticVersioning.Version</see>.
        /// </summary>
        public static SemanticVersioning.Version ToSemanticVersion(this Version version)
        {
            return new(version.Major, version.Minor, version.Build);
        }
        /// <summary>
        /// Get
        /// <see cref="SemanticVersioning.Range">SemanticVersioning.Range</see>
        /// which indicates versions compatible to
        /// <paramref name="version"/>
        /// <see cref="SemanticVersioning.Version">SemanticVersioning.Version</see>.
        /// </summary>
        public static SemanticVersioning.Range GetCompatibleVersionRange(this SemanticVersioning.Version version)
        {
            return new(string.Format("~{0}.{1}", version.Major, version.Minor));
        }
    }
}
