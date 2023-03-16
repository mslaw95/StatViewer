using Microsoft.EntityFrameworkCore;
using StatViewer.Api.Services.Interfaces;
using StatViewer.Api.Utils;
using StatViewer.Data.DataTransfer;
using StatViewer.Data.Models;

namespace StatViewer.Api.Services
{
    public class StoreService : IStoreService
    {
        private readonly IDbContextFactory<StoreContext> _dbContextFactory;

        public StoreService(IDbContextFactory<StoreContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<BaseResponse> AddEntryAsync(Entry entry)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            try
            {
                await context.AddAsync(entry);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BaseResponse(null, false, e.ToString());
            }

            return new BaseResponse(null, true);
        }

        public async Task<BaseResponse> AddStoreAsync(Store store)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            try
            {
                await context.AddAsync(store);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BaseResponse(store.Id, false, e.ToString());
            }

            return new BaseResponse(store.Id, true);
        }

        public async Task<BaseResponse> DeleteStoreAsync(Store store)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            try
            {
                context.Remove(store);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BaseResponse(store.Id, false, e.ToString());
            }

            return new BaseResponse(store.Id, true);
        }

        public async Task<BaseResponse<StoreStatistics>> GetStatisticsAsync(int storeId, DateTime startDate, DateTime endDate)
        {
            var validEntries = await GetValidEntriesAsync(storeId, startDate, endDate);
            var dates = validEntries.Select(e => e.EntryDate).Distinct();

            var store = await GetStoreByIdAsync(storeId);
            var result = new StoreStatistics
            {
                Store = store.Data,
                Statistics = new List<Statistics>()
            };

            foreach (var date in dates)
            {
                var statistics = new Statistics
                {
                    Date = date,
                    Count = validEntries.CountEntries(date)
                };
                
                result.Statistics.Add(statistics);
            }

            return new BaseResponse<StoreStatistics>(result, null, true);
        }

        private async Task<IEnumerable<Entry>> GetValidEntriesAsync(int storeId, DateTime startDate, DateTime endDate)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return context.Entries.Where(e => e.StoreID == storeId && e.EntryDate.IsValid(startDate, endDate)).ToList();
        }

        public async Task<BaseResponse<Store>> GetStoreByIdAsync(int id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var result =  await context.Stores.FirstOrDefaultAsync(s => s.Id == id);
            if (result is null)
            {
                return new BaseResponse<Store>(result, id, false, "Store doesn't exist!");
            }
            
            return new BaseResponse<Store>(result, id, true);
        }

        public async Task<BaseResponse> UpdateStoreAsync(int id, Store store)
        {
            var storeUpdate = store with { Id = id };

            using var context = await _dbContextFactory.CreateDbContextAsync();
            try
            {
                context.Update(storeUpdate);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BaseResponse(id, false, e.ToString());
            }

            return new BaseResponse(id, true);
        }
    }
}
