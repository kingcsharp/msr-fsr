using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Microsoft.Extensions.Logging;

namespace MSR.Infrastructure.Resources.Services.Invoices
{
    public class QuoteService : IQuoteService

    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger _logger;
        private ICustomerService _customerService;
        
        public QuoteService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<QuoteService> logger, ICustomerService customerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<IEnumerable<QuoteModel>> GetQuotesAsync()
        {
            var quoteList = new List<QuoteModel>();
            IQueryable<Quote> quotes = _unitOfWork.Quotes
                            .Query()
                            .Include(q => q.Products)
                            .Include(q => q.Customer)
                            .Include(q => q.SubmittedBy)
                            .Include(q => q.Status);

            foreach (var quote in await quotes.ToListAsync())
            {
                quoteList.Add(_mapper.Map<QuoteModel>(quote));
            }

            return quoteList.AsEnumerable();
        }

        public async Task<IEnumerable<QuoteModel>> GetQuoteAsync(int id)
        {
            var quoteList = new List<QuoteModel>();

            var quotes = _unitOfWork.Quotes
                            .Query()
                            .Include(q => q.Customer)
                            .Include(q => q.Status)
                            .Include(q => q.SubmittedBy)
                            .Where(q => q.Id == id)
                            .AsQueryable();

            foreach (var quote in await quotes.ToListAsync())
            {
                quoteList.Add(_mapper.Map<QuoteModel>(quote));
            }

            return quoteList.AsEnumerable();
        }

        public async Task<QuoteModel> CreateQuoteAsync(CreateQuote command)
        {
            var quote = _mapper.Map<Quote>(command);

            // Unused by required by the model
            quote.StatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "Pending").Id;
            quote.SubmittedById = CurrentUser.GetId();
            quote.SubmittedDate = DateTime.UtcNow;

            // Save the new Quote
            await _unitOfWork.Quotes.AddAndSaveChangesAsync(quote);

            // Returning QuoteModel from the inserted quote
            var retQuote = _mapper.Map<QuoteModel>(quote);

            return retQuote;
        }

        public async Task<QuoteModel> DeleteQuoteAsync(DeleteQuote command)
        {
            var quote = await _unitOfWork.Quotes.Query()
                                                    .Include(q => q.QuoteItems)
                                                    .Where(x => x.Id == command.Id)
                                                    .FirstOrDefaultAsync();
            if (quote is null)
            {
                throw new DomainException($"{nameof(Quote)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            _unitOfWork.Quotes.Delete(false, quote, true);

            await _unitOfWork.SaveChangesAsync();

            var ret = _mapper.Map<QuoteModel>(quote);
            return ret;
        }

        public async Task<IEnumerable<QuoteModel>> ImportQuotes(string csvData)
        {
            var records = CSVHelper.ParseRecords<QuoteImportItem>(csvData);
            var quoteModels = new List<QuoteModel>();
            var customerIds = await _unitOfWork.Customers.Query().Select(i => i.Id).ToListAsync();
            

            if (!CurrentUser.HasPrivilege(EnumMenuItem.QuotesProducts, EnumPrivilege.CanApprove))
            {
                throw new DomainException("Permission denied for import", DomainError.BadRequest);
            }

            foreach (var record in records)
            {
                try
                {
                    var customerId = _customerService.GetCustomerByNameAsync(record.Company).Result.Id;

                    if (customerIds.Contains(customerId))
                    {
                        var quoteModel = await CreateQuoteAsync(_mapper.Map<CreateQuote>(record));
                        quoteModels.Add(quoteModel);
                    }
                }
                catch (Exception exception)
                {
                    //If we get an error on a single import dump it and keep going. 
                    _logger.LogError(exception, exception.Message);
                }
            }

            return quoteModels;
        }
    }
}
