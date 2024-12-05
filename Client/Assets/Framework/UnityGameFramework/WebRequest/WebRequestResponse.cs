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

        /// <summary>
        /// 设置原始数据。
        /// </summary>
        public WebRequestResponse SetRawData(byte[] rawData)
        {
            RawData = rawData;
            return this;
        }
    }
}