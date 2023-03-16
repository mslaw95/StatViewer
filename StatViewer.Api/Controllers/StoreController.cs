using Microsoft.AspNetCore.Mvc;
using StatViewer.Api.Services.Interfaces;
using StatViewer.Api.Utils;
using StatViewer.Data.Models;

namespace StatViewer.Api.Controllers
{
    [ApiController]
    [Route("api/stores")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("/{id}")]
        public async Task<BaseResponse<Store>> GetStoreByIdAsync(int id)
        {
            return await _storeService.GetStoreByIdAsync(id);
        }

        [HttpPost]
        public async Task<BaseResponse> AddStoreAsync(Store store)
        {
            return await _storeService.AddStoreAsync(store);
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse> UpdateStoreAsync(int id, Store store)
        {
            var oldStore = await _storeService.GetStoreByIdAsync(id);
            if (oldStore is null)
            {
                return new BaseResponse(null, false, "Store doesn't exist!");
            }

            return await _storeService.UpdateStoreAsync(id, store);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse> DeleteStoreAsync(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store is null)
            {
                return new BaseResponse(null, false, "Store doesn't exist!");
            }

            return await _storeService.DeleteStoreAsync(store.Data);
        }

        [HttpPost("/entries")]
        public async Task<BaseResponse> AddEntryAsync(Entry entry)
        {
            return await _storeService.AddEntryAsync(entry);
        }

        [HttpGet("/statistics/{storeId}")]
        public async Task<BaseResponse<StoreStatistics>> GetStatisticsAsync(int storeId, string startDate, string endDate)
        {
            var startDateValid = DateTime.TryParse(startDate, out DateTime startDateTime);
            if (!startDateValid)
            {
                return new BaseResponse<StoreStatistics>(null, null, false, "StartDate in wrong format");
            }

            var endDateValid = DateTime.TryParse(endDate, out DateTime endDateTime);
            if (!endDateValid)
            {
                return new BaseResponse<StoreStatistics>(null, null, false, "EndDate in wrong format");
            }

            return await _storeService.GetStatisticsAsync(storeId, startDateTime, endDateTime);
        }
    }
}
