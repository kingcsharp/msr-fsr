using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<SearchView>> Search(GetSearch command, CancellationToken cancellationToken = default)
        {
            var retSearch = new List<SearchView>();
            if (DateTime.TryParse(command.SearchTerm, out var searchDate))
            {
                //For dates we only search for things 
                var workOrders = await _unitOfWork.WorkOrders.Query().Where(i => (i.ActualStartDate.HasValue && i.ActualStartDate.Value.Date == searchDate)
                                                                            || (i.ActualEndDate.HasValue && i.ActualEndDate.Value.Date == searchDate)
                                                                            || (i.ScheduledStartDate.Date == searchDate)).ToListAsync();

                foreach(var workOrder in workOrders)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"WorkOrder: {workOrder.Id}",
                        ItemId = workOrder.Id,
                        ItemName = $"WorkOrder: {workOrder.Id}",
                        ItemType = nameof(WorkOrder),
                        LastUpdatedBy = workOrder.LastUpdatedBy.HasValue ? $"{workOrder.LastUpdated.FirstName} {workOrder.LastUpdated.LastName}" : $"{workOrder.LastUpdated.FirstName} {workOrder.LastUpdated.LastName}",
                        LastUpdatedOn = workOrder.LastUpdatedOn ?? workOrder.CreatedOn
                    });
                }

                return retSearch;
            }
            else if(int.TryParse(command.SearchTerm, out var searchNumber))
            {
                var workOrder = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false,i => i.Id == searchNumber);
                var part = await _unitOfWork.Parts.FirstOrDefaultAsync(false, i => i.Id == searchNumber);
                var product = await _unitOfWork.Products.FirstOrDefaultAsync(false, i => i.Id == searchNumber);
                var procedure = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, i => i.Id == searchNumber);
                var document = await _unitOfWork.Documents.FirstOrDefaultAsync(false, i => i.Id == searchNumber);

                if (workOrder != null)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"WorkOrder: {workOrder.Id}",
                        ItemId = workOrder.Id,
                        ItemName = $"WorkOrder: {workOrder.Id}",
                        ItemType = nameof(WorkOrder),
                        LastUpdatedBy = workOrder.LastUpdatedBy.HasValue ? $"{workOrder.LastUpdated.FirstName} {workOrder.LastUpdated.LastName}" : $"{workOrder.LastUpdated.FirstName} {workOrder.LastUpdated.LastName}",
                        LastUpdatedOn = workOrder.LastUpdatedOn ?? workOrder.CreatedOn
                    });
                }
                if (part != null)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Part: {part.Id}",
                        ItemId = part.Id,
                        ItemName = part.Name,
                        ItemType = nameof(EntityFramework.Entities.Part),
                        LastUpdatedBy = part.LastUpdatedBy.HasValue ? $"{part.LastUpdated.FirstName} {part.LastUpdated.LastName}" : $"{part.LastUpdated.FirstName} {part.LastUpdated.LastName}",
                        LastUpdatedOn = part.LastUpdatedOn ?? part.CreatedOn
                    });
                }
                if (product != null)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Product: {product.Id}",
                        ItemId = product.Id,
                        ItemName = product.Name,
                        ItemType = nameof(Product),
                        LastUpdatedBy = product.LastUpdatedBy.HasValue ? $"{product.LastUpdated.FirstName} {product.LastUpdated.LastName}" : $"{product.LastUpdated.FirstName} {product.LastUpdated.LastName}",
                        LastUpdatedOn = product.LastUpdatedOn ?? product.CreatedOn
                    });
                }
                if (procedure != null)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Procedure: {procedure.Id}",
                        ItemId = procedure.Id,
                        ItemName = procedure.Name,
                        ItemType = nameof(Procedure),
                        LastUpdatedBy = procedure.LastUpdatedBy.HasValue ? $"{procedure.LastUpdated.FirstName} {procedure.LastUpdated.LastName}" : $"{procedure.LastUpdated.FirstName} {procedure.LastUpdated.LastName}",
                        LastUpdatedOn = procedure.LastUpdatedOn ?? procedure.CreatedOn
                    });
                }
                if (document != null)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Document: {document.Id}",
                        ItemId = document.Id,
                        ItemName = document.Name,
                        ItemType = nameof(Document),
                        LastUpdatedBy = document.LastUpdatedBy.HasValue ? $"{document.LastUpdated.FirstName} {document.LastUpdated.LastName}" : $"{document.LastUpdated.FirstName} {document.LastUpdated.LastName}",
                        LastUpdatedOn = document.LastUpdatedOn ?? document.CreatedOn
                    });
                }

                return retSearch;
            }
            else //Treat it like a string
            {
                var workOrders = await _unitOfWork.WorkOrders.Query().Include(i => i.WorkOrderParts).ThenInclude(j => j.Part)
                                                                    .Include(i => i.Product)
                                                                    .Where(i => i.WorkOrderParts.Any(j => j.SerialNumber.Contains(command.SearchTerm))
                                                                            || i.WorkOrderParts.Any(j => j.Part != null && (j.Part.Name.Contains(command.SearchTerm)) || j.Part.PartNumber.Contains(command.SearchTerm) || j.Part.OEMPartNumber.Contains(command.SearchTerm))
                                                                            || i.Product != null && i.Product.Name.Contains(command.SearchTerm)).ToListAsync();

                var parts = await _unitOfWork.Parts.Query().Where(i => i.Name.Contains(command.SearchTerm) || i.PartNumber.Contains(command.SearchTerm) || i.OEMPartNumber.Contains(command.SearchTerm)).ToListAsync();
                var products = await _unitOfWork.Products.Query().Where(i => i.Name.Contains(command.SearchTerm)).ToListAsync();
                var procedures = await _unitOfWork.Procedures.Query().Where(i => i.Name.Contains(command.SearchTerm)).ToListAsync();
                var documents = await _unitOfWork.Documents.Query().Where(i => i.Name.Contains(command.SearchTerm)).ToListAsync();

                foreach (var workOrder in workOrders)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"WorkOrder: {workOrder.Id}",
                        ItemId = workOrder.Id,
                        ItemName = $"WorkOrder: {workOrder.Id}",
                        ItemType = nameof(WorkOrder),
                        LastUpdatedBy = workOrder.LastUpdatedBy.HasValue ? $"{workOrder.LastUpdated.FirstName} {workOrder.LastUpdated.LastName}" : $"{workOrder.LastUpdated.FirstName} {workOrder.LastUpdated.LastName}",
                        LastUpdatedOn = workOrder.LastUpdatedOn ?? workOrder.CreatedOn
                    });
                }
                foreach (var part in parts)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Part: {part.Id}",
                        ItemId = part.Id,
                        ItemName = part.Name,
                        ItemType = nameof(EntityFramework.Entities.Part),
                        LastUpdatedBy = part.LastUpdatedBy.HasValue ? $"{part.LastUpdated.FirstName} {part.LastUpdated.LastName}" : $"{part.LastUpdated.FirstName} {part.LastUpdated.LastName}",
                        LastUpdatedOn = part.LastUpdatedOn ?? part.CreatedOn
                    });
                }
                foreach (var product in products)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Product: {product.Id}",
                        ItemId = product.Id,
                        ItemName = product.Name,
                        ItemType = nameof(Product),
                        LastUpdatedBy = product.LastUpdatedBy.HasValue ? $"{product.LastUpdated.FirstName} {product.LastUpdated.LastName}" : $"{product.LastUpdated.FirstName} {product.LastUpdated.LastName}",
                        LastUpdatedOn = product.LastUpdatedOn ?? product.CreatedOn
                    });
                }
                foreach (var procedure in procedures)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Procedure: {procedure.Id}",
                        ItemId = procedure.Id,
                        ItemName = procedure.Name,
                        ItemType = nameof(Procedure),
                        LastUpdatedBy = procedure.LastUpdatedBy.HasValue ? $"{procedure.LastUpdated.FirstName} {procedure.LastUpdated.LastName}" : $"{procedure.LastUpdated.FirstName} {procedure.LastUpdated.LastName}",
                        LastUpdatedOn = procedure.LastUpdatedOn ?? procedure.CreatedOn
                    });
                }
                foreach (var document in documents)
                {
                    retSearch.Add(new SearchView()
                    {
                        Description = $"Document: {document.Id}",
                        ItemId = document.Id,
                        ItemName = document.Name,
                        ItemType = nameof(Document),
                        LastUpdatedBy = document.LastUpdatedBy.HasValue ? $"{document.LastUpdated.FirstName} {document.LastUpdated.LastName}" : $"{document.LastUpdated.FirstName} {document.LastUpdated.LastName}",
                        LastUpdatedOn = document.LastUpdatedOn ?? document.CreatedOn
                    });
                }

                return retSearch;
            }
        }
    }
}
