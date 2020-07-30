using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Role
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.WorkOrderModel>> GetWorkOrderAsync(GetWorkOrder command)
        {
            List<EntityFramework.Entities.WorkOrder> workorders;
            IQueryable<WorkOrder> query;

            if (command.Id.HasValue) {
                query = _unitOfWork.WorkOrders.Query().Where(x => x.Id == command.Id.Value);
            } else {
                query = _unitOfWork.WorkOrders.Query();
            }
            workorders = await query
                .Include(x => x.WorkOrderParts)
                .Include(x => x.WorkOrderTasks)
                .Include(x => x.Product)
                .Include(x => x.Purchase)
                .Include(x => x.Location)
                .ToListAsync();

            if (workorders.Count == 0) {
                throw new DomainException($"Work Order ID {command.Id.GetValueOrDefault()} not found", DomainError.NotFound);
            }

            var result = workorders.Select(x => {
                var wom = _mapper.Map<Domain.Models.WorkOrderModel>(x);
                // unlink the backpointers to the work order model, which causes loops.
                wom.Purchase.WorkOrders = null;
                wom.Product.WorkOrders = null;
                foreach (var wop in wom.WorkOrderParts) {
                    wop.WorkOrder = null;
                    wop.Parent = null;
                    wop.Children = null;
                }
                foreach (var wot in wom.WorkOrderTasks) {
                    wot.WorkOrder = null;
                }
                return wom;
            }).OrderBy(x => x.Id).ToList();

            return result;
        }
        public async Task<Domain.Models.WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.WorkOrderModel ret;

            if (user.CanApprove(EnumMenuItem.WIPMenu))
            {
                WorkOrder workorder = _mapper.Map<WorkOrder>(command);
                await _unitOfWork.LogApprovalTransaction(workorder, workorder.Id);

                _unitOfWork.WorkOrders.Add(workorder);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.WorkOrderModel>(workorder);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.WorkOrderModel)} uid {user.Id}");
            }

            return ret;
        }
        public async Task<Domain.Models.WorkOrderModel> UpdateWorkOrderAsync(UpdateWorkOrder command)
        {
            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.WorkOrderModel ret;

            if (user.CanApprove(EnumMenuItem.WipStatus))
            {
                var workorder = _mapper.Map(command, current);
                _unitOfWork.WorkOrders.Update(workorder);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(workorder, workorder.Id);

                ret = _mapper.Map<Domain.Models.WorkOrderModel>(workorder);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.WorkOrderModel)} uid {user.Id}");
            }

            return ret;

        }
        public async Task<bool> DeleteWorkOrderAsync(DeleteWorkOrder command)
        {
            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();

            if (user.CanApprove(EnumMenuItem.WipStatus)) {
                var workorder = _mapper.Map(command, current);
                _unitOfWork.WorkOrders.Delete(false, workorder.Id);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(workorder, workorder.Id);
            } else {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.WorkOrderModel)} uid {user.Id}");
            }

            return true;
        }
    }
}
