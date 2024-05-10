namespace E5Renewer.Config
{
    /// <summary>The interface of object has a bool property named <c>check</c>.</summary>
    public interface ICheckable
    {
        /// <value><br/>
        /// The <c>check</c> property.<br/>
        /// It shows that if this object is correct.
        /// </value>
        public bool check { get; }
    }
}
