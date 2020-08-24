using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Resources.Services.Invoices
{
    public class QuoteService : IQuoteService

    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public QuoteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuoteModel>> GetQuotesAsync()
        {
            var quoteList = new List<QuoteModel>();
            var quotes = _unitOfWork.Quotes
                            .Query()
                            .Include(q => q.Product)
                            .Include(q => q.Customer)
                            .Include(q => q.Status);

            foreach (var quote in quotes.ToList())
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
                            .Include(q => q.Product)
                            .Include(q => q.Customer)
                            .Include(q => q.Status)
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
    }
}
