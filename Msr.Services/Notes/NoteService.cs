using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.Notes;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Notes.Messages;
using Msr.Services.Notes.Procedures;

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
            try
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
            catch (Exception e)
            {
                ////TODO add logging
            }
        }

        public List<AspNetRole> GetRoles()
        {
            return _dbContext.AspNetRoles.ToList();
        }

        public void AddNoteToTask(AddNoteToTaskRequest request)
        {
            try
            {
                var addCommentToTaskProcedure = new AddCommentToTaskProcedure {Comment = request.Comment, TaskId = request.TaskId };


                _dbContext.Database.ExecuteStoredProcedure(addCommentToTaskProcedure);
            }
            catch (Exception e)
            {
                ////TODO add logging
            }
        }
    }
}
