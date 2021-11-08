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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class CycleCountHistoryService : ICycleCountHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CycleCountHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CycleCountHistoryModel> CreateCycleCountHistoryAsync(CreateCycleCountHistory command)
        {
            CycleCountHistoryModel ret;
            
            CycleCountHistory cycleCountHistory = _mapper.Map<CycleCountHistory>(command);
            
            _unitOfWork.CycleCountHistory.Add(cycleCountHistory);

            await _unitOfWork.SaveChangesAsync();

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
            
            ret = _mapper.Map<CycleCountHistoryModel>(current);

            return ret;
        }
        
        public async Task<ICollection<CycleCountHistoryModel>> ImportCycleCountHistories(string csvData)
        {
            IEnumerable records = CSVHelper.ParseRecords<CycleCountHistoryImportItem>(csvData);
            List<UpdateCycleCountHistory> updates = new List<UpdateCycleCountHistory>();
            List<CreateCycleCountHistory> inserts = new List<CreateCycleCountHistory>();
            List<CycleCountHistoryModel> results = new List<CycleCountHistoryModel>();

            var cycleCountHistoryEntities = await _unitOfWork.CycleCountHistory.Query().ToListAsync();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.CycleCountImport, EnumPrivilege.CanCreate))
            {
                throw new DomainException($"Permission denied for user {CurrentUser.GetId()}", DomainError.BadRequest);
            }
            
            foreach (CycleCountHistoryImportItem record in records)
            {
                if (string.IsNullOrWhiteSpace(record.PartNumber) && string.IsNullOrWhiteSpace(record.SerialNumber) && string.IsNullOrWhiteSpace(record.CycleCount)) continue;

                var id = cycleCountHistoryEntities.Where(x => x.PartNumber == record.PartNumber && x.SerialNumber == record.SerialNumber).Select(x => x.Id).FirstOrDefault();

                if (id != 0)
                {
                    var updateCycleCountHistoryModel = _mapper.Map<UpdateCycleCountHistory>(record);
                    if (int.TryParse(record.CycleCount.ToString(), out var cycleCountValue))
                    {
                        updateCycleCountHistoryModel.CycleCount = cycleCountValue;
                    }
                    updateCycleCountHistoryModel.Id = id;
                    updates.Add(updateCycleCountHistoryModel); 
                }
                else
                {
                    var createCycleCountHistoryModel = _mapper.Map<CreateCycleCountHistory>(record);
                    if (int.TryParse(record.CycleCount.ToString(), out var cycleCountValue))
                    {
                        createCycleCountHistoryModel.CycleCount = cycleCountValue;
                    }
                    inserts.Add(createCycleCountHistoryModel);
                }
            }

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
