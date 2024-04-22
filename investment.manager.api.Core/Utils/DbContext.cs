namespace investiment.manager.api.Utils
{
    public class DbContext
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public Collection Collection { get; set; }
    }
    public class Collection
    {
        public string Investment { get; set; }
        public string Wallet { get; set; }
        public string User { get; set; }
    }
}
