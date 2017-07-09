namespace Loom.Web.UI.PageParts
{
    public interface IClientScriptProvider
    {
        string InlineScript { get; }
        string FunctionKey { get; }
        string Function { get; }
    }
}