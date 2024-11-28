namespace GameFramework.Resource
{
    /// <summary>
    /// 创建资源包回调函数集。
    /// </summary>
    public class CreatePackageCallbacks
    {
        private readonly CreatePackageSuccessCallback m_CreatePackageSuccessCallback;
        private readonly CreatePackageFailureCallback m_CreatePackageFailureCallback;

        public CreatePackageCallbacks(CreatePackageSuccessCallback createPackageSuccessCallback) : this(createPackageSuccessCallback, null)
        {
        }

        public CreatePackageCallbacks(CreatePackageSuccessCallback createPackageSuccessCallback, CreatePackageFailureCallback createPackageFailureCallback)
        {
            if (createPackageSuccessCallback == null)
            {
                throw new GameFrameworkException("Create package success callback is invalid.");
            }

            m_CreatePackageSuccessCallback = createPackageSuccessCallback;
            m_CreatePackageFailureCallback = createPackageFailureCallback;
        }

        public CreatePackageSuccessCallback CreatePackageSuccessCallback
        {
            get { return m_CreatePackageSuccessCallback; }
        }

        public CreatePackageFailureCallback CreatePackageFailureCallback
        {
            get { return m_CreatePackageFailureCallback; }
        }
    }
}