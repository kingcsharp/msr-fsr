using System.Linq;
using System.Web.Mvc;
using Msr.Models.Objects;
using Msr.Services.jqGrid;
using Msr.Services.Objects;

namespace Answer.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ObjectsService _objectsService;

        public SearchController()
        {
            _objectsService = new ObjectsService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResultData(JqGridParam param, string searchTerm)
        {
            var totalRows = _objectsService.GetObjectSearchQueryable();

            if (param.HasGridFilters())
            {
                foreach (var rule in param.where.rules)
                {
                    if (string.IsNullOrWhiteSpace(rule.data))
                    {
                        continue;
                    }

                    rule.data = rule.data.Trim();

                    if (rule.field == nameof(ObjectSearchView.ItemType))
                    {
                        totalRows = totalRows.Where(x => x.ItemType.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ObjectSearchView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ObjectSearchView.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ObjectSearchView.ItemType))
                    {
                        totalRows = totalRows.Where(x => x.ItemType.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.Trim();
                    totalRows = totalRows.Where(x => x.ObjectId == searchTerm || x.Description.Contains(searchTerm) || x.Name.Contains(searchTerm));
                }
            }

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                param.sortColumn = nameof(ObjectSearchView.Date);
            }

            var result = totalRows.ApplyPaging(param);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}