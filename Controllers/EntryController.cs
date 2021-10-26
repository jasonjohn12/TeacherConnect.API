using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Dto;
using TeacherConnect.Entities;
using TeacherConnect.Extensions;

namespace TeacherConnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EntryController : BaseApiController
    {
        private readonly IEntryRepository _entryRepository;
        public EntryController(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;

        }
        [HttpPost]
        public async Task<ActionResult<Entry>> AddEntry(EntryDto entryDto)
        {
            var userId = User.GetUserId();
            var entryToAdd = new Entry
            {
                Note = entryDto.Note,
                Contacted = entryDto.Contacted,
                //CreatedByUserId = userId,
                StudentId = entryDto.StudentId,
                CreatedAtDate = DateTime.UtcNow,
                CreatedByUserId = userId,
                // LastEditByUserId = userId,
                //   LastUpdatedDate = DateTime.UtcNow,
                ContactedDate = entryDto.Contacted == true ? DateTime.UtcNow : null
            };
            var entryId = await _entryRepository.AddEntryAsync(entryToAdd);
            var createdEntry = new EntryDto
            {
                Id = entryId,
                Note = entryDto.Note,
                Contacted = entryDto.Contacted,
                CreatedAtDate = entryToAdd.CreatedAtDate,
                StudentId = entryDto.StudentId,
                Grade = entryDto.Grade
            };
            if (entryId > 0) return StatusCode(201, createdEntry);
            return BadRequest("Unable to create entry");
        }
    }
}