using StatViewer.Api.Utils;
using StatViewer.Data.Models;

namespace StatViewer.Api.Services.Interfaces
{
    public interface IStoreService
    {
        Task<BaseResponse<Store>> GetStoreByIdAsync(int id);
        Task<BaseResponse> AddStoreAsync(Store store);
        Task<BaseResponse> UpdateStoreAsync(int id, Store store);
        Task<BaseResponse> DeleteStoreAsync(Store store);

        Task<BaseResponse> AddEntryAsync(Entry entry);

        Task<BaseResponse<StoreStatistics>> GetStatisticsAsync(int storeId, DateTime startDate, DateTime endDate);
    }
}
