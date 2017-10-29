using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Services.Quotes.ViewModels;

namespace Msr.Services.Quotes
{
    public class QuoteService
    {
        public ResultNotification<string> Create(FreeFormQuoteViewModel model, List<QuoteItemsViewModel> quoteItemsViewModels)
        {
            var response = new ResultNotification<string>();
            try
            {
                //need to know what to use procedure or entity framework savechanges()
                response.SuccessMessage = "Quote has been saved successfully.";
                return response;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
                return response;
            }
           
        }
        public ResultNotification<string> Edit(FreeFormQuoteViewModel model, List<QuoteItemsViewModel> quoteItemsViewModels)
        {
            var response = new ResultNotification<string>();
            try
            {
                //need to know what to use procedure or entity framework savechanges()
                response.SuccessMessage = "Quote has been edited successfully.";
                return response;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
                return response;
            }

        }
    }
}
