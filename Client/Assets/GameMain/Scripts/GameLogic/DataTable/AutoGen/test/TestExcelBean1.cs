
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace GameLogic.test
{
/// <summary>
/// 这是个测试excel结构
/// </summary>
public sealed partial class TestExcelBean1 : Luban.BeanBase
{
    public TestExcelBean1(ByteBuf _buf) 
    {
        X1 = _buf.ReadInt();
        X2 = _buf.ReadString();
        X3 = _buf.ReadInt();
        X4 = _buf.ReadFloat();
    }

    public static TestExcelBean1 DeserializeTestExcelBean1(ByteBuf _buf)
    {
        return new test.TestExcelBean1(_buf);
    }

    /// <summary>
    /// 最高品质
    /// </summary>
    public readonly int X1;
    /// <summary>
    /// 黑色的
    /// </summary>
    public readonly string X2;
    /// <summary>
    /// 蓝色的
    /// </summary>
    public readonly int X3;
    /// <summary>
    /// 最差品质
    /// </summary>
    public readonly float X4;
   
    public const int __ID__ = -1738345160;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(DataTableModule tables)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "x1:" + X1 + ","
        + "x2:" + X2 + ","
        + "x3:" + X3 + ","
        + "x4:" + X4 + ","
        + "}";
    }
}

}

