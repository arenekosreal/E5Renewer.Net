namespace E5Renewer.Models.Config
{
    /// <summary>The api interface for checkable object.</summary>
    public interface ICheckable
    {
        /// <value>If the object is checked.</value>
        public bool isCheckPassed { get; }
    }
}
