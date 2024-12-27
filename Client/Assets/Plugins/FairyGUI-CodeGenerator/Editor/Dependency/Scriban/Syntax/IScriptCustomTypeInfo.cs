namespace Scriban.Syntax
{
#if SCRIBAN_PUBLIC || UNITY_EDITOR
    public
#else
    internal
#endif
    interface IScriptCustomTypeInfo
    {
        string TypeName { get; }
    }
}