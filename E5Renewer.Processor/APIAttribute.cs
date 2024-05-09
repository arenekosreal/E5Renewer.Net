namespace E5Renewer.Processor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class APIAttribute : Attribute
    {
        public string name { get; private set; }
        public APIAttribute(string name)
        {
            this.name = name;
        }
    }
}
