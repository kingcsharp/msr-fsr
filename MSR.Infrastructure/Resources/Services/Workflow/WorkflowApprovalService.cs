using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.Exceptions;
using MSR.Domain.Views;

namespace MSR.Infrastructure.Resources.Services
{
    public class WorkflowApprovalService : IWorkflowApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPartService _partService;
        private readonly IFileService _fileService;

        public WorkflowApprovalService(IUnitOfWork unitOfWork, IMapper mapper, IPartService partService, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _partService = partService;
            _fileService = fileService;

        }

        public async Task<PendingApprovalModel> CreateApprovalAsync(PostApprovalModel command)
        {
            ApprovalEntity result = null;
            var status = await _unitOfWork.Status.Query().FirstOrDefaultAsync(x => x.Name == "Approved");
            switch (command.Table)
            {

                case EnumApprovalTables.CustomerApproval:
                    result = await ApproveCustomer(command, status);
                    break;
                case EnumApprovalTables.DocumentApproval:
                    result = await ApproveDocument(command,status);
                    break;
                case EnumApprovalTables.LocationApproval:
                    result = await ApproveLocation(command, status);
                    break;
                case EnumApprovalTables.PartApproval:
                    PartApproval partApproval = await _unitOfWork.PartApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);

                    string data = partApproval.ApprovalJSON;
                    var dataObj = JsonConvert.DeserializeObject<UpdatePart>(data);
                    if (partApproval.PartId.HasValue)
                    {
                        // approval for update
                        dataObj.Id = partApproval.PartId.Value;
                        await _partService.UpdatePartAsync(dataObj);
                    }
                    else
                    {
                        // approval for create
                        CreatePart createObj = JsonConvert.DeserializeObject<CreatePart>(data);
                        PartModel createResult = await _partService.CreatePartAsync(createObj);
                        partApproval.PartId = createResult.Id;
                    }

                    var part = await _unitOfWork.Parts.Query().FirstOrDefaultAsync(x => x.Id == partApproval.PartId);
                    partApproval.Status = status;
                    _mapper.Map(partApproval, part);
                    _unitOfWork.PartApprovals.Update(partApproval);
                    _unitOfWork.SaveChanges();
                    await _unitOfWork.LogApprovalTransaction(partApproval, partApproval.Id, status.Name, command.Comments);
                    result = partApproval;
                    break;
                case EnumApprovalTables.ProcedureApproval:
                    var procedureApproval = await _unitOfWork.ProcedureApprovals.Query()
                        .Include(x => x.ProcedureType).Include(x => x.ProcedureStepApprovals).FirstOrDefaultAsync(x => x.Id == command.Id);
                    var procedure = await _unitOfWork.Procedures.Query()
                        .Include(x => x.ProcedureType).Include(x => x.ProcedureSteps).FirstOrDefaultAsync(x => x.Id == command.Id);
                    procedureApproval.Status = status;
                    _mapper.Map(procedureApproval, procedure);
                    _unitOfWork.ProcedureApprovals.Update(procedureApproval);
                    procedure.Revision++;
                    _unitOfWork.Procedures.Update(procedure);
                    _unitOfWork.SaveChanges();
                    await _unitOfWork.LogApprovalTransaction(procedureApproval, procedureApproval.Id, status.Name, command.Comments);
                    result = procedureApproval;
                    break;
                case EnumApprovalTables.ProductApproval:
                    var productApproval = await _unitOfWork.ProductApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                    var product = await _unitOfWork.Products.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                    productApproval.Status = status;
                    _mapper.Map(productApproval, product);
                    _unitOfWork.ProductApprovals.Update(productApproval);
                    product.Revision++;
                    _unitOfWork.Products.Update(product);
                    _unitOfWork.SaveChanges();
                    await _unitOfWork.LogApprovalTransaction(productApproval, productApproval.Id, status.Name, command.Comments);
                    result = productApproval;
                    break;
                case EnumApprovalTables.PurchaseOrderApproval:
                    result = await ApprovePurchaseOrder(command, status);
                    break;
                case EnumApprovalTables.UserApproval:
                    var userApproval = await _unitOfWork.UserApprovals.Query().Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == command.Id);
                    var currUser = await _unitOfWork.Users.Query().Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == userApproval.UserId);
                    userApproval.Status = status;
                    _unitOfWork.UserApprovals.Update(userApproval);
                    _mapper.Map(userApproval, currUser);
                    _unitOfWork.Users.Update(currUser);
                    _unitOfWork.SaveChanges();
                    await _unitOfWork.LogApprovalTransaction(userApproval, userApproval.Id, status.Name, command.Comments);
                    var uerApprovalResult = _mapper.Map<PendingApprovalModel>(userApproval);
                    uerApprovalResult.Comments = command.Comments;
                    return uerApprovalResult;
                default:
                    break;
            }


            var ret = _mapper.Map<PendingApprovalModel>(result);
            ret.Comments = command.Comments;
            return ret;
        }

        public async Task DeactivateApprovalAsync(DeactivateApprovalModel command)
        {
            switch (command.Table)
            {
                case EnumApprovalTables.CustomerApproval:
                    var customerApproval = _unitOfWork.CustomerApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.CustomerApprovals.Delete(false, customerApproval, true);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.LogApprovalTransaction(customerApproval, customerApproval.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.DocumentApproval:
                    var documentApproval = _unitOfWork.DocumentApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.DocumentApprovals.Delete(false, documentApproval, true);
                    await _unitOfWork.LogApprovalTransaction(documentApproval, documentApproval.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.LocationApproval:
                    var locationApprovals = _unitOfWork.LocationApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.LocationApprovals.Delete(false, locationApprovals, true);
                    await _unitOfWork.LogApprovalTransaction(locationApprovals, locationApprovals.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.PartApproval:
                    var partApproval = _unitOfWork.PartApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.PartApprovals.Delete(false, partApproval, true);
                    await _unitOfWork.LogApprovalTransaction(partApproval, partApproval.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.ProcedureApproval:
                    var procedureApproval = _unitOfWork.ProcedureApprovals.Query()
                        .Include(x => x.ProcedureStepApprovals)
                        .ThenInclude(x => x.ProcedureStepDocumentApprovals)
                        .Include(x => x.ProcedureStepApprovals)
                        .ThenInclude(x => x.ProcedureStepMonitorApprovals)
                        .FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.ProcedureApprovals.Delete(false, procedureApproval, true);
                    await _unitOfWork.LogApprovalTransaction(procedureApproval, procedureApproval.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.ProductApproval:
                    var productApproval = _unitOfWork.ProductApprovals.Query().FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.ProductApprovals.Delete(false, productApproval, true);
                    await _unitOfWork.LogApprovalTransaction(productApproval, productApproval.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.PurchaseOrderApproval:
                    var purchaseApproval = _unitOfWork.PurchaseOrderApprovals.Query()
                        .FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.PurchaseOrderApprovals.Delete(false, purchaseApproval, true);
                    await _unitOfWork.LogApprovalTransaction(purchaseApproval, purchaseApproval.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                case EnumApprovalTables.UserApproval:
                    var userApprovals = _unitOfWork.UserApprovals.Query()
                        .Include(x => x.UserRoleApprovals)
                        .FirstOrDefault(x => x.Id == command.Id);
                    _unitOfWork.UserApprovals.Delete(false, userApprovals, true);
                    await _unitOfWork.LogApprovalTransaction(userApprovals, userApprovals.Id, EnumUtils.GetDescription(ApprovalStatusEnum.Cancelled), command.Comment);
                    break;
                default:
                    throw new DomainException("Deactivate Action Does Not Exists", DomainError.NotFound);
            }


            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PendingApprovalPopoverModel> GetApprovalChangesAsync(GetPendingApprovalDetailsModel command)
        {
            var status = await _unitOfWork.Status.Query().FirstOrDefaultAsync(x => x.Name == "Approved");
            switch (command.Table)
            {
                case EnumApprovalTables.CustomerApproval:
                    var customerApproval = await _unitOfWork.CustomerApprovals.Query().Include(x => x.SecondarContactUser).Include(x => x.PrimaryContactUser).Include(x => x.Location).FirstOrDefaultAsync(x => x.Id == command.Id);
                    var customer = await _unitOfWork.Customers.Query().Include(x => x.SecondaryContactUser).Include(x => x.PrimaryContactUser).Include(x => x.Location).FirstOrDefaultAsync(x => x.Id == customerApproval.CustomerId);
                    var customerApprovalChanges = new PendingApprovalPopoverModel();
                    customerApprovalChanges.AddRow("Name", customer?.Name, customerApproval.Name);
                    customerApprovalChanges.AddRow("Address", customer?.Address, customerApproval.Address);
                    customerApprovalChanges.AddRow("Phone", customer?.Phone, customerApproval.Phone);
                    customerApprovalChanges.AddBoolRow("IsActive", customer?.IsActive, customerApproval.IsActive);
                    customerApprovalChanges.AddRow("Location Name", customer?.Location?.Name, customerApproval.Location?.Name);
                    customerApprovalChanges.AddRow("PrimaryContactUser", customer?.PrimaryContactUser?.GetFullName(), customerApproval.PrimaryContactUser?.GetFullName());
                    customerApprovalChanges.AddRow("SecondaryContactUser", customer?.SecondaryContactUser?.GetFullName(), customerApproval.SecondarContactUser?.GetFullName());
                    return customerApprovalChanges;
                case EnumApprovalTables.DocumentApproval:
                    var documentApproval = await _unitOfWork.DocumentApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                    var document = await _unitOfWork.Documents.Query().FirstOrDefaultAsync(x => x.Id == documentApproval.DocumentId);
                    var documentApprovalChanges = new PendingApprovalPopoverModel();
                    documentApprovalChanges.AddRow("Name", document?.Name, documentApproval.Name);
                    documentApprovalChanges.AddRow("Revision", document?.Revision, documentApproval.Revision);
                    return documentApprovalChanges;
                case EnumApprovalTables.LocationApproval:
                    var locationApproval = await _unitOfWork.LocationApprovals.Query().Include(x => x.TimeZone).FirstOrDefaultAsync(x => x.Id == command.Id);
                    var location = await _unitOfWork.Locations.Query().Include(x => x.TimeZone).FirstOrDefaultAsync(x => x.Id == locationApproval.LocationId);
                    var locationApprovalChanges = new PendingApprovalPopoverModel();
                    locationApprovalChanges.AddRow("Name", location?.Name, locationApproval.Name);
                    locationApprovalChanges.AddRow("Address1", location?.Address1, locationApproval.Address1);
                    locationApprovalChanges.AddRow("Address2", location?.Address2, locationApproval.Address2);
                    locationApprovalChanges.AddRow("City", location?.City, locationApproval.City);
                    locationApprovalChanges.AddRow("State", location?.State, locationApproval.State);
                    locationApprovalChanges.AddRow("Postal Code", location?.PostalCode, locationApproval.PostalCode);
                    locationApprovalChanges.AddRow("Country", location?.Country, locationApproval.Country);
                    locationApprovalChanges.AddRow("Phone", location?.Phone, locationApproval.Phone);
                    locationApprovalChanges.AddRow("Internal Address", location?.InternalAddress, locationApproval.InternalAddress);
                    locationApprovalChanges.AddRow("Invoice Class", location?.InvoiceClass, locationApproval.InvoiceClass);
                    locationApprovalChanges.AddRow("Timezone", location?.TimeZone?.Description, locationApproval?.TimeZone?.Description);
                    return locationApprovalChanges;
                case EnumApprovalTables.PartApproval:
                    var partApproval = await _unitOfWork.PartApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                    var part = await _unitOfWork.Parts.Query().FirstOrDefaultAsync(x => x.Id == partApproval.PartId);
                    var partApprovalChanges = new PendingApprovalPopoverModel();
                    partApprovalChanges.AddRow("Name", part?.Name, partApproval.Name);
                    partApprovalChanges.AddRow("Part Number", part?.PartNumber, partApproval.PartNumber);
                    partApprovalChanges.AddRow("OEM Part Number", part?.OEMPartNumber, partApproval.OEMPartNumber);
                    partApprovalChanges.AddRow("NickName", part?.NickName, partApproval.NickName);
                    partApprovalChanges.AddRow("Maximum Cycles", part?.MaximumCycles, partApproval.MaximumCycles);
                    return partApprovalChanges;
                case EnumApprovalTables.ProcedureApproval:
                    var procedureApproval = await _unitOfWork.ProcedureApprovals.Query()
                        .Include(x => x.ProcedureType).Include(x => x.ProcedureStepApprovals).FirstOrDefaultAsync(x => x.Id == command.Id);
                    var procedure = await _unitOfWork.Procedures.Query()
                        .Include(x => x.ProcedureType).Include(x => x.ProcedureSteps).FirstOrDefaultAsync(x => x.Id == procedureApproval.ProcedureId);
                    var procedureApprovalChanges = new PendingApprovalPopoverModel();
                    procedureApprovalChanges.AddRow("Name", procedure?.Name, procedureApproval.Name);
                    procedureApprovalChanges.AddRow("Duration Type", procedure?.DurationType, procedureApproval.DurationType);
                    procedureApprovalChanges.AddRow("Procedure Type", procedure?.ProcedureType?.Name, procedureApproval.ProcedureType?.Name);
                    procedureApprovalChanges.AddRow("Name", procedure?.Name, procedureApproval.Name);
                    procedureApprovalChanges.AddRow("Name", procedure?.Name, procedureApproval.Name);
                    this.GetProcedureStepApprovals(procedureApprovalChanges, procedure?.ProcedureSteps, procedureApproval.ProcedureStepApprovals);
                    return procedureApprovalChanges;
                case EnumApprovalTables.ProductApproval:
                    var productApproval = await _unitOfWork.ProductApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                    var product = await _unitOfWork.Products.Query().FirstOrDefaultAsync(x => x.Id == productApproval.ProductId);
                    var productApprovalChanges = new PendingApprovalPopoverModel();
                    productApprovalChanges.AddRow("Name", product?.Name, productApproval.Name);
                    productApprovalChanges.AddRow("Revision", product?.Revision, productApproval.Revision);
                    // TODO: there is no such field
                    //productApprovalChanges.AddRow("Customer Requirement Id", product.CustomerRequirementId, productApproval.CustomerRequirementId);
                    productApprovalChanges.AddRow("Equipment Cost", product?.EquipmentCost, productApproval.EquipmentCost);
                    productApprovalChanges.AddRow("Material Cost", product?.MaterialCost, productApproval.MaterialCost);
                    productApprovalChanges.AddRow("Sales Tax", product?.SalesTax, productApproval.SalesTax);
                    productApprovalChanges.AddRow("Total Sale Price", product?.TotalSalePrice, productApproval.TotalSalePrice);
                    productApprovalChanges.AddRow("Cycle Time", product?.CycleTime, productApproval.CycleTime);
                    return productApprovalChanges;
                case EnumApprovalTables.PurchaseOrderApproval:
                    var purchaseOrderApproval = await _unitOfWork.PurchaseOrderApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                    var purchaseOrder = await _unitOfWork.PurchaseOrders.Query().FirstOrDefaultAsync(x => x.Id == purchaseOrderApproval.PurchaseOrderId);
                    var purchaseOrderApprovalChanges = new PendingApprovalPopoverModel();
                    purchaseOrderApprovalChanges.AddRow("Name", purchaseOrder?.Name, purchaseOrderApproval.Name);
                    purchaseOrderApprovalChanges.AddRow("Reference PO", purchaseOrder?.ReferencePO, purchaseOrderApproval.ReferencePO);
                    purchaseOrderApprovalChanges.AddRow("Reference Name", purchaseOrder?.ReferenceName, purchaseOrderApproval.ReferenceName);
                    purchaseOrderApprovalChanges.AddRow("Open Date", purchaseOrder?.OpenDate, purchaseOrderApproval.OpenDate);
                    purchaseOrderApprovalChanges.AddRow("Close Date", purchaseOrder?.CloseDate, purchaseOrderApproval.CloseDate);
                    purchaseOrderApprovalChanges.AddRow("Total Purchase Limit", purchaseOrder?.TotalPurchaseLimit, purchaseOrderApproval.TotalPurchaseLimit);
                    purchaseOrderApprovalChanges.AddRow("CustomerReference", purchaseOrder?.CustomerReference, purchaseOrderApproval.CustomerReference);
                    return purchaseOrderApprovalChanges;
                case EnumApprovalTables.UserApproval:
                    var userApproval = await _unitOfWork.UserApprovals.Query()
                        .Include(x => x.Customer).Include(x => x.Location).Include(x => x.UserRoleApprovals).FirstOrDefaultAsync(x => x.Id == command.Id);
                    var currUser = await _unitOfWork.Users.Query()
                        .Include(x => x.Customer).Include(x => x.Location).Include(x => x.Roles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Id == userApproval.UserId);
                    var userApprovalChanges = new PendingApprovalPopoverModel();
                    userApprovalChanges.AddRow("First Name", currUser?.FirstName, userApproval.FirstName);
                    userApprovalChanges.AddRow("Last Name", currUser?.LastName, userApproval.LastName);
                    userApprovalChanges.AddRow("User Name", currUser?.UserName, userApproval.UserName);
                    userApprovalChanges.AddRow("Title", currUser?.Title, userApproval.Title);
                    userApprovalChanges.AddRow("Email", currUser?.Email, userApproval.Email);
                    userApprovalChanges.AddRow("Phone", currUser?.Phone, userApproval.Phone);
                    userApprovalChanges.AddRow("Name", currUser?.Location?.Name, userApproval.Location?.Name);
                    userApprovalChanges.AddBoolRow("Answer User", currUser?.IsAnswerUser, userApproval.IsAnswerUser);
                    userApprovalChanges.AddRow("Customer Name", currUser?.Customer?.Name, userApproval.Customer?.Name);
                    userApprovalChanges.AddRow("Lockout End Date Utc", currUser?.LockoutEndDateUtc, userApproval.LockoutEndDateUtc);
                    userApprovalChanges.AddRow("Access Failed Count", currUser?.AccessFailedCount, userApproval.AccessFailedCount);
                    userApprovalChanges.AddRow("Time Zone Id", currUser?.TimeZoneId, userApproval.TimeZoneId);
                    userApprovalChanges.AddRow("Roles", currUser?.UserName, userApproval.UserName);
                    this.GetUserRoleApprovals(userApprovalChanges, currUser?.Roles, userApproval.UserRoleApprovals);
                    return userApprovalChanges;
                default:
                    break;
            }

            return new PendingApprovalPopoverModel(); ;
        }

        private void GetUserRoleApprovals(PendingApprovalPopoverModel pendingApprovalModel, ICollection<UserRole> userRoles, ICollection<UserRoleApproval> userRoleApprovals)
        {
            var from = "";
            foreach (var role in userRoles)
            {
                from += role.Role.Name + ",";
            }
            if (from.Length > 1)
            {
                from = from.Substring(0, from.Length - 1);
            }

            var to = "";
            foreach (var roleApproval in userRoleApprovals)
            {
                to += roleApproval.Role.Name + ",";
            }

            if (to.Length > 1)
            {
                to = to.Substring(0, to.Length - 1);
            }

            if (from.Length > 0 || to.Length > 0)
            {
                pendingApprovalModel.AddRow("User Roles", from, to);
            }
        }

        private void GetProcedureStepApprovals(PendingApprovalPopoverModel pendingApprovalModel, ICollection<ProcedureStep> procSteps, ICollection<ProcedureStepApproval> procStepApprovals)
        {
            var from = "";
            foreach (var procedure in procSteps)
            {
                from += procedure.Title + ",";
            }
            if (from.Length > 1)
            {
                from = from.Substring(0, from.Length - 1);
            }

            var to = "";
            foreach (var procedureApproval in procStepApprovals)
            {
                to += procedureApproval.Title + ",";
            }

            if (to.Length > 1)
            {
                to = to.Substring(0, to.Length - 1);
            }
            if (from.Length > 0 || to.Length > 0)
            {
                pendingApprovalModel.AddRow("Procedure Steps", from, to);
            }
        }

        public async Task<ICollection<PendingApprovalModel>> GetPendingApprovalAsync(GetPendingApprovalModel command)
        {
            var ret = new List<PendingApprovalModel>();
            if (!CurrentUser.HasPrivilege(EnumMenuItem.ApprovalWorkflows, EnumPrivilege.CanRead))
            {
                return ret;
            }

            if (command.Table == EnumApprovalTables.All)
            {
                foreach (int enumVal in Enum.GetValues(typeof(EnumApprovalTables)))
                {
                    if (enumVal != (int)EnumApprovalTables.All)
                    {
                        ret.AddRange(await GetPendingApprovalByTable((EnumApprovalTables)enumVal));
                    }
                }
            }
            else if (CurrentUser.CanReadActivity(command.Table))
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
                    var userApprovals = await _unitOfWork.UserApprovals.Query()
                        .Include(x => x.Workflow).Include(x => x.WorkflowGroup)
                        .ToListAsync();
                    var result = userApprovals.Select(approvalEnt => _mapper.Map<PendingApprovalModel>(approvalEnt)).ToList();
                    return result;
                default:
                    break;
            }

            var approvalEntityList = await approvalEntity.Include(x => x.Workflow).Include(x => x.WorkflowGroup).ToListAsync();
            var pendingApprovalModelResult = approvalEntityList.Select(approvalEnt => _mapper.Map<PendingApprovalModel>(approvalEnt)).ToList();

            return pendingApprovalModelResult;
        }

        private async Task<ApprovalEntity> ApproveCustomer(PostApprovalModel command, Status status)
        {
            var customerApproval = await _unitOfWork.CustomerApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);

            if (customerApproval.CustomerId is null)
            {
                var customer = _mapper.Map<EntityFramework.Entities.Customer>(customerApproval);
                customer.IsActive = true;
                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(customer, customer.Id, status.Name, command.Comments);
            }
            else
            {
                var customer = await _unitOfWork.Customers.Query().FirstOrDefaultAsync(x => x.Id == customerApproval.CustomerId);
                _mapper.Map(customerApproval, customer);

                if (!customerApproval.IsActive)
                {
                    var users = _unitOfWork.Users.Query().Where(i => i.CustomerId == customer.Id).ToList();
                    foreach (var user in users)
                    {
                        user.IsActive = false;
                        _unitOfWork.Users.Update(user);
                    }
                }
                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(customer, customer.Id, status.Name, command.Comments);
            }

            _unitOfWork.CustomerApprovals.Delete(false, customerApproval);
            await _unitOfWork.SaveChangesAsync();

            return customerApproval;
        }

        private async Task<ApprovalEntity> ApproveLocation(PostApprovalModel command, Status status)
        {
            var locationApproval = await _unitOfWork.LocationApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);

            if (locationApproval.LocationId is null)
            {
                var location = _mapper.Map<EntityFramework.Entities.Location>(locationApproval);
                location.IsActive = true;
                await _unitOfWork.Locations.AddAsync(location);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(location, location.Id, status.Name, command.Comments);
            }
            else
            {
                var location = await _unitOfWork.Locations.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                _mapper.Map(locationApproval, location);
                _unitOfWork.Locations.Update(location);
                if (!locationApproval.IsActive)
                {
                    foreach (var childLocation in await _unitOfWork.Locations.Query().Where(i => i.ParentId == location.Id).ToListAsync())
                    {
                        childLocation.IsActive = false;
                        _unitOfWork.Locations.Update(childLocation);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(location, location.Id, status.Name, command.Comments);
            }

            _unitOfWork.LocationApprovals.Delete(false, locationApproval);
            await _unitOfWork.SaveChangesAsync();

            return locationApproval;
        }

        private async Task<ApprovalEntity> ApprovePurchaseOrder(PostApprovalModel command, Status status)
        {
            var purchaseOrderApproval = await _unitOfWork.PurchaseOrderApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
            if (purchaseOrderApproval.PurchaseOrderId is null)
            {
                //It's a new one so create it
                var purchaseOrder = _mapper.Map<EntityFramework.Entities.PurchaseOrder>(purchaseOrderApproval);
                purchaseOrder.Revision = 1;

                await _unitOfWork.PurchaseOrders.AddAsync(purchaseOrder);

                var purchaseOrderProductApprovals = await _unitOfWork.PurchaseOrderProductApprovals.Query().Where(i => i.PurchaseOrderApprovalId == purchaseOrderApproval.Id).ToListAsync();

                foreach (var purchaseOrderProduct in purchaseOrderProductApprovals)
                {
                    var map = new PurchaseOrderProduct()
                    {
                        PurchaseOrder = purchaseOrder,
                        ProductId = purchaseOrderProduct.ProductId
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(map);
                    _unitOfWork.PurchaseOrderProductApprovals.Delete(false, purchaseOrderProduct);
                }
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, status.Name, command.Comments);
            }
            else
            {
                var purchaseOrder = await _unitOfWork.PurchaseOrders.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
                purchaseOrderApproval.Status = status;

                _mapper.Map(purchaseOrderApproval, purchaseOrder);
                purchaseOrder.Revision = purchaseOrder.Revision == null ? 1 : purchaseOrder.Revision + 1;
                _unitOfWork.PurchaseOrders.Update(purchaseOrder);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, status.Name, command.Comments);
            }

            _unitOfWork.PurchaseOrderApprovals.Delete(false, purchaseOrderApproval);
            await _unitOfWork.SaveChangesAsync();
            return purchaseOrderApproval;
        }

        private async Task<ApprovalEntity> ApproveDocument(PostApprovalModel command, Status status)
        {
            var documentApproval = await _unitOfWork.DocumentApprovals.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
            var supportingData = JsonConvert.DeserializeObject<Dictionary<string,List<int>>>(documentApproval.ApprovalJSON);

            if (documentApproval.DocumentId is null)
            {
                var document = _mapper.Map<EntityFramework.Entities.Document>(documentApproval); 
                document.Revision = 1;
                await _unitOfWork.Documents.AddAsync(document);
                await _unitOfWork.SaveChangesAsync();

                if (supportingData != null)
                {
                    foreach (var role in supportingData["RoleIds"] ?? new List<int>())
                    {
                        var map = new DocumentRoleMap()
                        {
                            Document = document,
                            DocumentId = document.Id,
                            RoleId = role
                        };

                        await _unitOfWork.DocumentRoles.AddAsync(map);
                    }

                    foreach (var referenceFile in supportingData["ReferenceFileIds"] ?? new List<int>())
                    {
                        var map = new FileEntityMap()
                        {
                            EntityId = document.Id,
                            EntityTableName = "document",
                            FileId = referenceFile
                        };

                        await _unitOfWork.FileEntityMap.AddAsync(map);
                    }
                }
                await _unitOfWork.LogApprovalTransaction(document, document.Id, status.Name, command.Comments);
                await SetPDFData(_mapper.Map<DocumentView>(document));
            }
            else
            {
                var document = await _unitOfWork.Documents.Query().FirstOrDefaultAsync(x => x.Id == documentApproval.DocumentId);

                _mapper.Map(documentApproval, document);
                document.Revision++;
                _unitOfWork.DocumentApprovals.Update(documentApproval);
                await _unitOfWork.SaveChangesAsync();

                var curRoleIds = await _unitOfWork.DocumentRoles.Query().Where(i => i.DocumentId == document.Id).Select(i => i.RoleId).ToListAsync();
                var curFileIds = await _unitOfWork.FileEntityMap.Query().Where(i => i.EntityId == document.Id && i.EntityTableName == "document").Select(i => i.FileId).ToListAsync();

                if (supportingData != null)
                {
                    //Exists in DB but not in list: Remove
                    var rolesToRemove = curRoleIds.Except(supportingData["RoleIds"] ?? new List<int>());
                    var filesToRemove = curFileIds.Except(supportingData["ReferenceFileIds"] ?? new List<int>());
                    //Does not Exist in DB: Add
                    var rolesToAdd = supportingData["RoleIds"] ?? new List<int>().Except(curRoleIds);
                    var filesToAdd = supportingData["ReferenceFileIds"] ?? new List<int>().Except(curFileIds);

                    var removeRoles = await _unitOfWork.DocumentRoles.Query().Where(i => i.DocumentId == document.Id && rolesToRemove.Contains(i.RoleId)).ToListAsync();
                    foreach (var role in removeRoles)
                    {
                        _unitOfWork.DocumentRoles.Delete(false, role);
                    }

                    var removeFiles = await _unitOfWork.FileEntityMap.Query().Where(i => i.EntityId == document.Id && i.EntityTableName == "document" && filesToRemove.Contains(i.FileId)).ToListAsync();
                    foreach (var file in removeFiles)
                    {
                        _unitOfWork.FileEntityMap.Delete(false, file);
                    }
                    foreach (var referenceFile in filesToAdd)
                    {
                        var map = new FileEntityMap()
                        {
                            EntityId = document.Id,
                            EntityTableName = "document",
                            FileId = referenceFile
                        };

                        await _unitOfWork.FileEntityMap.AddAsync(map);
                    }
                    foreach (var role in rolesToAdd)
                    {
                        var map = new DocumentRoleMap()
                        {
                            Document = document,
                            DocumentId = document.Id,
                            RoleId = role
                        };

                        await _unitOfWork.DocumentRoles.AddAsync(map);
                    }
                }
                await _unitOfWork.LogApprovalTransaction(document, document.Id, status.Name, command.Comments);
                await SetPDFData(_mapper.Map<DocumentView>(document));
            }

            _unitOfWork.DocumentApprovals.Delete(false, documentApproval);
            await _unitOfWork.SaveChangesAsync();
            return documentApproval;
        }

        private async Task SetPDFData(DocumentView document)
        {
            var files = _unitOfWork.FileEntityMap.Query().Include(i => i.FileObject)
                                                         .Where(i => i.EntityTableName == "Documents" && i.EntityId == document.Id
                                                                && i.FileObject != null && i.FileObject.ContentType == "application/pdf").Select(i => i.FileObject);
            

            foreach(var file in files)
            {
                await _fileService.EditPdfFile(_mapper.Map<FileModel>(file), document);
            }
        }
    }
}


