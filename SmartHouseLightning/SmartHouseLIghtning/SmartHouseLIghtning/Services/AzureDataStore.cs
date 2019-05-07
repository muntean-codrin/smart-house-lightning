using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using SmartHouseLIghtning.Models;

namespace SmartHouseLIghtning.Services
{
    public class AzureDataStore : IDataStore<Item>
    {
        HttpClient client;
        IEnumerable<Item> items;
        IEnumerable<Connection> connections;
        IEnumerable<Room> rooms;
        IEnumerable<Light> lights;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Item>();
            connections = new List<Connection>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Connection>> GetConnectionsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/connections");
                connections = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Connection>>(json));
            }

            return connections;
        }

        public async Task<bool> UpdateConnectionAsync(Connection connection)
        {
            if (connection == null || connection.Guid == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(connection);
            var content = new StringContent(serializedItem.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/connections", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/rooms");
                rooms = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Room>>(json));
            }

            return rooms;
        }

        public async Task<bool> AddRoomAsync(Room room)
        {
            if (room == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(room);

            var response = await client.PostAsync($"api/rooms", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetAutoModeAsync()
        {
            if (IsConnected)
            {
                var autoMode = await client.GetStringAsync($"api/Settings/automode");
                return autoMode;
            }

            return null;
        }

        public async Task<bool> UpdateAutoModeAsync(string value)
        {
            if (value == null || !IsConnected)
                return false;

            var response = await client.PutAsync($"api/Settings?ID=automode&value={value}", new StringContent(String.Empty));

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Light>> GetControllersAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"/api/connections/controllers");
                lights = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Light>>(json));
            }

            return lights;
        }

        public async Task<bool> UpdateControllerStateAsync(string guid, bool value)
        {
            if (guid == null || !IsConnected)
                return false;

            var response = await client.PutAsync($"api/connections/controller?guid={guid}&mode={value}", new StringContent(String.Empty));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LocateEspAsync(string guid)
        {
            if (guid == null || !IsConnected)
                return false;

            var response = await client.PutAsync($"/api/connections/locate?guid={guid}", new StringContent(String.Empty));

            return response.IsSuccessStatusCode;
        }



        
    }
}
