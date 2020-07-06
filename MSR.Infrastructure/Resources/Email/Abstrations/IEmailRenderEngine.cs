using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Email.Abstrations
{
    public interface IEmailRenderEngine
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
