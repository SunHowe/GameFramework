
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
public sealed partial class TestExcelBean2 : Luban.BeanBase
{
    public TestExcelBean2(ByteBuf _buf) 
    {
        Y1 = _buf.ReadInt();
        Y2 = _buf.ReadString();
        Y3 = _buf.ReadFloat();
    }

    public static TestExcelBean2 DeserializeTestExcelBean2(ByteBuf _buf)
    {
        return new test.TestExcelBean2(_buf);
    }

    /// <summary>
    /// 最高品质
    /// </summary>
    public readonly int Y1;
    /// <summary>
    /// 黑色的
    /// </summary>
    public readonly string Y2;
    /// <summary>
    /// 蓝色的
    /// </summary>
    public readonly float Y3;
   
    public const int __ID__ = -1738345159;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(DataTableModule tables)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "y1:" + Y1 + ","
        + "y2:" + Y2 + ","
        + "y3:" + Y3 + ","
        + "}";
    }
}

}

