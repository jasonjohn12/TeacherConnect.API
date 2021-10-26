using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Dto;
using TeacherConnect.Entities;

namespace TeacherConnect.Data
{
    public class EntryRepository : BaseRepository, IEntryRepository
    {
        private readonly IConfiguration _configuration;
        public EntryRepository(IConfiguration configuration) : base(configuration, "TeacherConnectDB")
        {
            _configuration = configuration;

        }
        public async Task<int> AddEntryAsync(Entry entry)
        {
            string command = @"INSERT INTO dbo.entry(studentid,note, createdatdate, lastupdatedate,createdbyuserid, contacted, contacteddate)
              VALUES(@studentid, @note, @createdatdate,@lastupdatedate, @createdbyuserid, @contacted, @contacteddate) RETURNING id
            ";
            return await ExecuteScalarAsync<int>(sql: command, param: new
            {
                studentid = entry.StudentId,
                note = entry.Note,
                createdatdate = entry.CreatedAtDate,
                //lastupdatedate = entry.LastUpdatedDate,
                userid = entry.CreatedByUserId,
                createdbyuserid = entry.CreatedByUserId,
                //  lasteditedbyuserid = entry.LastEditByUserId,
                madecontactwithparent = entry.Contacted,
                contacted = entry.Contacted,
                contacteddate = entry.ContactedDate
            });
        }
    }
}