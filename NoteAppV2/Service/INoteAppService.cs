using NoteAppV2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppV2.Service
{
    public interface  INoteAppService
    {
        Task<List<NoteItem>> GetItemAsync();

        Task InsertItemAsync(NoteItem item);
        Task UpdateItemAsync(NoteItem item);
        Task DeleteItemAsync(NoteItem item);
    }
}
