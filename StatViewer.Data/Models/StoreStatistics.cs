namespace StatViewer.Data.Models
{
    public class StoreStatistics
    {
        public Store Store { get; set; }

        public ICollection<Statistics> Statistics { get; set; }
    }
}
