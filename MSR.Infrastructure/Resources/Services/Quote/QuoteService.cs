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
            var quotes = _unitOfWork.Quotes.Query();

            foreach (var quote in quotes.ToList())
            {
                quoteList.Add(_mapper.Map<QuoteModel>(quote));
            }

            return quoteList.AsEnumerable();
        }
    }
}
