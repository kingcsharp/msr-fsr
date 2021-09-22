using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using MSR.Infrastructure.Resources.Queries;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class CycleCountHistoryService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IMessageHubClient _messageHub;

        public CycleCountHistoryService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IMessageHubClient messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _messageHub = messageHub;
        }

        public async Task<CycleCountHistoryModel> CreateCycleCountHistoryAsync(CreateCycleCountHistory command)
        {
            CycleCountHistoryModel ret;
            
            CycleCountHistory cycleCountHistory = _mapper.Map<CycleCountHistory>(command);
            
            _unitOfWork.CycleCountHistory.Add(cycleCountHistory);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.LogApprovalTransaction(cycleCountHistory, cycleCountHistory.Id);

            ret = _mapper.Map<CycleCountHistoryModel>(cycleCountHistory);

            return ret;
        }

        public async Task<CycleCountHistoryModel> UpdateCycleCountHistoryAsync(UpdateCycleCountHistory command)
        {
            CycleCountHistory current = await _unitOfWork.CycleCountHistory.Query()
                .Where(x => x.Id == command.Id)
                .FirstOrDefaultAsync();

            if (current is null)
            {
                throw new DomainException($"{nameof(CycleCountHistory)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            CycleCountHistoryModel ret;

            _mapper.Map(command, current);
            _unitOfWork.CycleCountHistory.Update(current);

            await _unitOfWork.LogApprovalTransaction(current, current.Id);
            ret = _mapper.Map<CycleCountHistoryModel>(current);

            return ret;
        }
        
        public async Task<ICollection<CycleCountHistoryModel>> ImportCycleCountHistories(string csvData)
        {
            IEnumerable records = CSVHelper.ParseRecords<CycleCountHistoryImportItem>(csvData);
            List<UpdateCycleCountHistory> updates = new List<UpdateCycleCountHistory>();
            List<CreateCycleCountHistory> inserts = new List<CreateCycleCountHistory>();
            List<CycleCountHistoryModel> results = new List<CycleCountHistoryModel>();

            var ids = await _unitOfWork.CycleCountHistory.Query().Select(i => i.Id).ToListAsync();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.CycleCountImport, EnumPrivilege.CanApprove))
            {
                throw new DomainException($"Permission denied for user {CurrentUser.GetId()}", DomainError.BadRequest);
            }

            // First parse the file to ensure valid data
            foreach (CycleCountHistoryImportItem record in records)
            {
                if (record.Id.HasValue && record.Id.Value > 0 && ids.Contains(record.Id.Value))
                {
                    var updateCycleCountHistoryModel = _mapper.Map<UpdateCycleCountHistory>(record);
                    updates.Add(updateCycleCountHistoryModel); 
                }
                else
                {
                    var createCycleCountHistoryModel = _mapper.Map<CreateCycleCountHistory>(record);
                    inserts.Add(createCycleCountHistoryModel);
                }
            }

            // Then perform the update
            foreach (UpdateCycleCountHistory model in updates)
            {
                results.Add(await UpdateCycleCountHistoryAsync(model));
            }
            foreach (CreateCycleCountHistory model in inserts)
            {
                results.Add(await CreateCycleCountHistoryAsync(model));
            }

            return results;
        }
    }
}
