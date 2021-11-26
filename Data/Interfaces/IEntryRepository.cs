using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherConnect.Dto;
using TeacherConnect.Entities;

namespace TeacherConnect.Data.Interfaces
{
    public interface IEntryRepository
    {
        Task<int> AddEntryAsync(Entry entry);
        Task<Entry> GetEntryByIdAsync(int entryid);
        Task<int> DeleteEntryAsync(int entryid);
    }
}