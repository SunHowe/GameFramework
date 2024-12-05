//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// 字符串hash工具。
    /// </summary>
    public static partial class Utility
    {
        public static class StringHash
        {
            public static long GetLongHashCode(string str)
            {
                const uint seed = 1313; // 31 131 1313 13131 131313 etc..

                ulong hash = 0;
                for (int i = 0; i < str.Length; ++i)
                {
                    char c = str[i];
                    byte high = (byte)(c >> 8);
                    byte low = (byte)(c & byte.MaxValue);
                    hash = hash * seed + high;
                    hash = hash * seed + low;
                }

                return (long)hash;
            }

            public static int Mode(string strText, int mode)
            {
                if (mode <= 0)
                {
                    throw new GameFrameworkException($"string mode < 0: {strText} {mode}");
                }

                return (int)((ulong)GetLongHashCode(strText) % (uint)mode);
            }
        }
    }
}