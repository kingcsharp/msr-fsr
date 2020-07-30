using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
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
            List<EntityFramework.Entities.WorkOrder> procedures;
            if (command.Id.HasValue) {
                procedures = await _unitOfWork.WorkOrders.Query().Where(x => x.Id == command.Id.Value).ToListAsync();
                if (procedures.Count == 0) {
                    throw new DomainException($"Work Order ID {command.Id.Value} not found", DomainError.NotFound);
                }
            } else {
                procedures = await _unitOfWork.WorkOrders.Query().ToListAsync();
            }
            var result = procedures.Select(x => {
                var wom = _mapper.Map<Domain.Models.WorkOrderModel>(x);
                // unlink the backpointers to the work order model, which cause a loops.
                wom.Purchase.WorkOrders = null;
                wom.Product.WorkOrders = null;
                return wom;
            }).OrderBy(x => x.Id).ToList();

            return result;
        }
        public async Task<Domain.Models.WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.WorkOrderModel ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                EntityFramework.Entities.WorkOrder procedure = _mapper.Map<EntityFramework.Entities.WorkOrder>(command);
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                _unitOfWork.WorkOrders.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.WorkOrderModel>(procedure);
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

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.WorkOrders.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.WorkOrderModel>(procedure);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.WorkOrderModel)} uid {user.Id}");
            }

            return ret;

        }
    }
}
