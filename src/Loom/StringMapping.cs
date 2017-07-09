namespace Loom
{
    public abstract class StringMapping
    {
        protected StringMapping(bool suppressNull)
        {
            SuppressNull = suppressNull;
        }

        public string this[string from] => Get(from);

        protected bool SuppressNull { get; }

        protected abstract string Get(string from);
    }
}