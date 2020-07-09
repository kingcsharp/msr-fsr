using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services
{
    public class WorkflowApprovalService : IWorkflowApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkflowApprovalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task CreateApprovalAsync(PostApprovalModel command)
        {
            throw new NotImplementedException();
        }



        public async Task<PendingApprovalModel> DeactivateApprovalAsync(DeactivateApprovalModel command)
        {
            var status = _unitOfWork.Status.Query().FirstOrDefault(x => x.Name == "Cancelled");
            IQueryable<ApprovalEntity> approvalEntity = null;
            switch (command.Table)
            {
                case EnumApprovalTables.CustomerApproval:
                    approvalEntity = _unitOfWork.CustomerApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.DocumentApproval:
                    approvalEntity = _unitOfWork.DocumentApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.LocationApproval:
                    approvalEntity = _unitOfWork.LocationApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.PartApproval:
                    approvalEntity = _unitOfWork.PartApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.ProcedureApproval:
                    approvalEntity = _unitOfWork.ProcedureApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.ProductApproval:
                    approvalEntity = _unitOfWork.ProductApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.PurchaseOrderApproval:
                    approvalEntity = _unitOfWork.PurchaseOrderApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                case EnumApprovalTables.UserApproval:
                    approvalEntity = _unitOfWork.UserApprovals.Query().Where(x => x.Id == command.Id);
                    break;
                default:
                    break;
            }

            var toCancel = approvalEntity.FirstOrDefault();
            toCancel.Status = status;
            _unitOfWork.SaveChanges();
            var ret = _mapper.Map<PendingApprovalModel>(toCancel);
            return ret;
        }

        public async Task<ICollection<PendingApprovalModel>> GetPendingApprovalAsync(GetPendingApproval command)
        {
            List<PendingApprovalModel> ret = new List<PendingApprovalModel>();
            if (command.Table == EnumApprovalTables.All)
            {
                foreach (int enumVal in Enum.GetValues(typeof(EnumApprovalTables)))
                {
                    if (enumVal != (int)EnumApprovalTables.All && DelegateHandler.CanReadActivity((EnumApprovalTables)enumVal))
                    {
                        ret.AddRange(await GetPendingApprovalByTable((EnumApprovalTables)enumVal));
                    }
                }
            }
            else if (DelegateHandler.CanReadActivity(command.Table))
            {
                ret.AddRange(await GetPendingApprovalByTable(command.Table));
            }

            return ret;
        }

        private async Task<List<PendingApprovalModel>> GetPendingApprovalByTable(EnumApprovalTables table)
        {
            IQueryable<ApprovalEntity> approvalEntity = null;
            switch (table)
            {
                case EnumApprovalTables.CustomerApproval:
                    approvalEntity = _unitOfWork.CustomerApprovals.Query();
                    break;
                case EnumApprovalTables.DocumentApproval:
                    approvalEntity = _unitOfWork.DocumentApprovals.Query();
                    break;
                case EnumApprovalTables.LocationApproval:
                    approvalEntity = _unitOfWork.LocationApprovals.Query();
                    break;
                case EnumApprovalTables.PartApproval:
                    approvalEntity = _unitOfWork.PartApprovals.Query();
                    break;
                case EnumApprovalTables.ProcedureApproval:
                    approvalEntity = _unitOfWork.ProcedureApprovals.Query();
                    break;
                case EnumApprovalTables.ProductApproval:
                    approvalEntity = _unitOfWork.ProductApprovals.Query();
                    break;
                case EnumApprovalTables.PurchaseOrderApproval:
                    approvalEntity = _unitOfWork.PurchaseOrderApprovals.Query();
                    break;
                case EnumApprovalTables.UserApproval:
                    approvalEntity = _unitOfWork.UserApprovals.Query();
                    break;
                default:
                    break;
            }

            var approvalEntityList = await approvalEntity.ToListAsync();
            var ret = approvalEntityList.Select(approvalEnt => _mapper.Map<PendingApprovalModel>(approvalEnt)).ToList();
            return ret;
        }
    }
}


