using SmartHouseLIghtning.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHouseLIghtning.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<Connection>> GetConnectionsAsync(bool forceRefresh = false);
        Task<bool> UpdateConnectionAsync(Connection connection);
        Task<IEnumerable<Room>> GetRoomsAsync(bool forceRefresh = false);
        Task<bool> AddRoomAsync(Room room);
        Task<string> GetAutoModeAsync();
        Task<bool> UpdateAutoModeAsync(string value);
        Task<IEnumerable<Light>> GetControllersAsync(bool forceRefresh = false);
        Task<bool> UpdateControllerStateAsync(string guid, bool value);
        Task<bool> LocateEspAsync(string guid);
    }
}
