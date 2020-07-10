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

        public async Task CreateApprovalAsync(PostApprovalModel command)
        {
            switch (command.Table)
            {
                case EnumApprovalTables.CustomerApproval:
                    var e = _unitOfWork.CustomerApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    break;
                //case EnumApprovalTables.DocumentApproval:
                //    approvalEntity = _unitOfWork.DocumentApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                //    break;
                //case EnumApprovalTables.LocationApproval:
                //    approvalEntity = _unitOfWork.LocationApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                //    break;
                //case EnumApprovalTables.PartApproval:
                //    approvalEntity = _unitOfWork.PartApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                //    break;
                //case EnumApprovalTables.ProcedureApproval:
                //    approvalEntity = _unitOfWork.ProcedureApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                //    break;
                //case EnumApprovalTables.ProductApproval:
                //    approvalEntity = _unitOfWork.ProductApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                //    break;
                //case EnumApprovalTables.PurchaseOrderApproval:
                //    approvalEntity = _unitOfWork.PurchaseOrderApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                //    break;
                case EnumApprovalTables.UserApproval:
                    var userApproval = _unitOfWork.UserApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    if (userApproval != null)
                    {
                        var currUser = _unitOfWork.Users.Query().FirstOrDefault(x => x.Id == userApproval.UserId);
                        UpdateEntity(currUser, userApproval);
                        _unitOfWork.SaveChanges();

                    }
                    break;
                default:
                    break;
            }

        }

        void UpdateEntity<C, A>(C currentEntity, A approvalEntity)
        {
            try
            {
                var entityType = currentEntity.GetType();
                foreach (System.Reflection.PropertyInfo property in typeof(A).GetProperties())
                {
                    if (property.Name != "Id")
                    {
                        var prop = entityType.GetProperty(property.Name);

                        if (prop == null) { continue; }

                        var val = property.GetValue(approvalEntity, null);
                        if (val == null)
                        {

                        }
                        else
                        {
                            prop.SetValue(currentEntity, val, null);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        //void UpdateEntity(EntityFramework.Entities.User currUser, UserApproval approvalEntity)
        //{
        //    var userType = currUser.GetType();
        //    foreach (System.Reflection.PropertyInfo property in typeof(UserApproval).GetProperties().Where(i => i.Name != nameof(approvalEntity.Id)))
        //    {
        //        var prop = userType.GetProperty(property.Name);

        //        if (prop == null) { continue; }

        //        prop.SetValue(currUser, property.GetValue(approvalEntity), null);
        //    }
        //}



        public async Task<PendingApprovalModel> DeactivateApprovalAsync(DeactivateApprovalModel command)
        {
            var status = _unitOfWork.Status.Query().FirstOrDefault(x => x.Name == "Cancelled");
            IQueryable<ApprovalEntity> approvalEntity = null;
            switch (command.Table)
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
                    var userApprovals = _unitOfWork.UserApprovals.Query().Where(x => x.Id == command.Id).FirstOrDefault();
                    userApprovals.Status = status;
                    _unitOfWork.SaveChanges();
                    var result = _mapper.Map<PendingApprovalModel>(userApprovals);
                    return result;
                default:
                    break;
            }

            var toCancel = approvalEntity.Where(x => x.Id == command.Id).FirstOrDefault();
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
                    var userApprovals = await _unitOfWork.UserApprovals.Query().ToListAsync();
                    var result = userApprovals.Select(approvalEnt => _mapper.Map<PendingApprovalModel>(approvalEnt)).ToList();
                    return result;
                default:
                    break;
            }

            var approvalEntityList = await approvalEntity.ToListAsync();
            var ret = approvalEntityList.Select(approvalEnt => _mapper.Map<PendingApprovalModel>(approvalEnt)).ToList();
            return ret;
        }
    }
}


