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
            System.Console.WriteLine("ADD ENTRY");
            var userId = User.GetUserId();
            var entryToAdd = new Entry
            {
                Note = entryDto.Note,
                Contacted = entryDto.Contacted,
                //CreatedByUserId = userId,
                StudentId = entryDto.StudentId,
                CreatedAtDate = DateTime.Now,
                // ContactedDate = DateTime.Now,
                CreatedByUserId = userId,
                // LastEditByUserId = userId,
                //   LastUpdatedDate = DateTime.UtcNow,
                ContactedDate = entryDto.Contacted == true ? DateTime.Now : null
            };
            var entryId = await _entryRepository.AddEntryAsync(entryToAdd);
            var createdEntry = new EntryDto
            {
                Id = entryId,
                Note = entryDto.Note,
                Contacted = entryDto.Contacted,
                CreatedAtDate = entryToAdd.CreatedAtDate,
                StudentId = entryDto.StudentId,
                // Grade = entryDto.Grade,
                ContactedDate = entryDto.Contacted == true ? DateTime.Now : null

            };
            if (entryId > 0) return StatusCode(201, createdEntry);
            return BadRequest("Unable to create entry");
        }
        [HttpDelete()]
        public async Task<ActionResult> DeleteEntry([FromQuery] int entryId)
        {
            System.Console.WriteLine($"EntryID is {entryId}");
            System.Console.WriteLine("DELETE ENTRY");
            var userId = User.GetUserId();
            var entry = await _entryRepository.GetEntryByIdAsync(entryId);
            if (entry == null) return NotFound();
            if (userId != entry.CreatedByUserId)
            {
                return Unauthorized("You are not allowed to edit this entry");
            }
            var entryToDelete = await _entryRepository.DeleteEntryAsync(entryId);
            return Ok();

        }
    }
}