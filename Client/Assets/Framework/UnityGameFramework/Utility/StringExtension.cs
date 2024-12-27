//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Text;

/// <summary>
/// 对 string 的扩展方法。
/// </summary>
public static class StringExtension
{
    private static readonly StringBuilder s_StringBuilder = new StringBuilder();
    
    /// <summary>
    /// 从指定字符串中的指定位置处开始读取一行。
    /// </summary>
    /// <param name="rawString">指定的字符串。</param>
    /// <param name="position">从指定位置处开始读取一行，读取后将返回下一行开始的位置。</param>
    /// <returns>读取的一行字符串。</returns>
    public static string ReadLine(this string rawString, ref int position)
    {
        if (position < 0)
        {
            return null;
        }

        int length = rawString.Length;
        int offset = position;
        while (offset < length)
        {
            char ch = rawString[offset];
            switch (ch)
            {
                case '\r':
                case '\n':
                    if (offset > position)
                    {
                        string line = rawString.Substring(position, offset - position);
                        position = offset + 1;
                        if ((ch == '\r') && (position < length) && (rawString[position] == '\n'))
                        {
                            position++;
                        }

                        return line;
                    }

                    offset++;
                    position++;
                    break;

                default:
                    offset++;
                    break;
            }
        }

        if (offset > position)
        {
            string line = rawString.Substring(position, offset - position);
            position = offset;
            return line;
        }

        return null;
    }

    /// <summary>
    /// 转成首字母大写的驼峰字符串。
    /// </summary>
    public static string ToUpperCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        
        // 将字符串中所有_后的字符转换为大写 然后确保首字母是大写
        s_StringBuilder.Clear();
        var upper = true;
                
        foreach (var c in str)
        {
            if (c == '_')
            {
                upper = true;
            }
            else
            {
                s_StringBuilder.Append(upper ? char.ToUpper(c) : c);
                upper = false;
            }
        }
                
        return s_StringBuilder.ToString();
    }
}
