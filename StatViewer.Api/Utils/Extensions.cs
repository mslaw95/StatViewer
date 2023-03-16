using StatViewer.Data.Models;

namespace StatViewer.Api.Utils
{
    public static class Extensions
    {
        public static bool IsValid(this DateTime currentDate, DateTime startDate, DateTime endDate)
            => currentDate >= startDate && currentDate <= endDate;

        public static int CountEntries(this IEnumerable<Entry> entries, DateTime currentDate)
            => entries.Where(e => e.EntryDate == currentDate).Count();
    }
}
