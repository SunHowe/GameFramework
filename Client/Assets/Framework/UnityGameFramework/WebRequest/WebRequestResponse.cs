using System;
using System.Text;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// Web请求应答包。
    /// </summary>
    public sealed class WebRequestResponse : Response
    {
        /// <summary>
        /// 原始数据。
        /// </summary>
        public byte[] RawData { get; private set; }

        public override void Clear()
        {
            base.Clear();
            RawData = null;
        }

        /// <summary>
        /// 设置原始数据。
        /// </summary>
        public WebRequestResponse SetRawData(byte[] rawData)
        {
            RawData = rawData;
            return this;
        }

        /// <summary>
        /// 将原始数据通过json反序列化成指定类型对象。失败则返回null。
        /// </summary>
        public T ParseData<T>()
        {
            try
            {
                return Utility.Json.ToObject<T>(Encoding.UTF8.GetString(RawData));
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return default;
            }
        }
    }
}