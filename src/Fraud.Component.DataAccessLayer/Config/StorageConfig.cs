namespace Fraud.Component.DataAccessLayer.Config
{
    public class StorageConfig
    {
        public bool EnableCache { set; get; }
        public int ExpireAfterInMinutes { set; get; }
        public string StorageType { set; get; }
    }
}
