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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        
        public async Task<ICollection<CycleCountHistoryModel>> ImportCycleCountHistories(string csvData)
        {
            IEnumerable records = CSVHelper.ParseRecords<CycleCountHistoryImportItem>(csvData);
            List<CycleCountHistoryModel> results = new List<CycleCountHistoryModel>();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.CycleCountImport, EnumPrivilege.CanCreate))
            {
                throw new DomainException($"Permission denied for user {CurrentUser.GetId()}", DomainError.BadRequest);
            }

            foreach (CycleCountHistoryImportItem record in records)
            {
                var createCycleCountHistoryModel = _mapper.Map<CreateCycleCountHistory>(record);
                results.Add(await CreateCycleCountHistoryAsync(createCycleCountHistoryModel));
            }

            return results;
        }
    }
}
