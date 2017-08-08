using System;
using System.Collections.Generic;
using System.Linq;
using Msr.Models.Notes;
using Msr.Models.Users;
using Msr.Repositories;

namespace Msr.Services.Notes
{
    public class NoteService
    {
        private readonly MsrDbContext _dbContext;

        public NoteService()
        {
            _dbContext = new MsrDbContext();
        }

        public void AddNote(string entityId, string message, int entityTypeId, string loggedUserId)
        {
            var newNote = new Note
            {
                Id = Guid.NewGuid(),
                EntityId = entityId,
                Message = message,
                EntityTypeId = entityTypeId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = loggedUserId
            };

            _dbContext.Notes.Add(newNote);

            _dbContext.SaveChanges();
        }

        public List<AspNetRole> GetRoles()
        {
            return _dbContext.AspNetRoles.ToList();
        }


        
    }
}
