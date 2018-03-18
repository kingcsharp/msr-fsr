using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Models.People;
using Msr.Services.Orders;
using Msr.Services.People.ViewModels;
using Msr.Services.Companies;
using Msr.Services.Documents;
using Msr.Services.Workflows;

namespace Answer.Web.Controllers
{
    public class PeopleController : BaseController
    {
        private readonly PeopleService _peopleService;

        private readonly CompanyService _companyService;

        private readonly DocumentFilesService _documentFilesService;

        private WorkflowService _workflowService;

        public PeopleController()
        {
            _peopleService = new PeopleService();
            _workflowService = new WorkflowService();
            _companyService = new CompanyService();
            _documentFilesService = new DocumentFilesService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "People";

            return View(viewModel);
        }

        public ActionResult PeopleData(JqGridParam param)
        {

            var defaultStatusList = "CREATING,DENIED,APPROVED,APPROVED_BUT_REVISING,APPROVED_BUT_DELETING".Split(',');

            var totalRows = _peopleService.GetPeople().Where(x => defaultStatusList.Contains(x.Status));

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PeopleObjectView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PeopleObjectView.FirstName))
                    {
                        totalRows = totalRows.Where(x => x.FirstName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.LastName))
                    {
                        totalRows = totalRows.Where(x => x.LastName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.LoginId))
                    {
                        totalRows = totalRows.Where(x => x.LoginId.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.PicRecord))
                    {
                        totalRows = totalRows.Where(x => x.PicRecord.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.PositionName))
                    {
                        totalRows = totalRows.Where(x => x.PositionName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.BossName))
                    {
                        totalRows = totalRows.Where(x => x.BossName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.RootCoName))
                    {
                        totalRows = totalRows.Where(x => x.RootCoName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.CompanyName))
                    {
                        totalRows = totalRows.Where(x => x.CompanyName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.LocationName))
                    {
                        totalRows = totalRows.Where(x => x.LocationName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.PrimaryPhoneNumber))
                    {
                        totalRows = totalRows.Where(x => x.PrimaryPhoneNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.SecondaryPhoneNumber))
                    {
                        totalRows = totalRows.Where(x => x.SecondaryPhoneNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.WorkEmailAddress))
                    {
                        totalRows = totalRows.Where(x => x.WorkEmailAddress.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.DateHired))
                    {
                        totalRows = totalRows.Where(x => x.DateHired.ToString().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.SystemStatus))
                    {
                        totalRows = totalRows.Where(x => x.SystemStatus.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.ReferenceFiles))
                    {
                        totalRows = totalRows.Where(x => x.ReferenceFiles.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(PeopleObjectView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }

                }
            }
            var orderBy = nameof(PeopleObjectView.FirstName);
            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }
            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(orderBy);
            }
            else
            {
                totalRows = totalRows.OrderBy(orderBy);
            }

            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));

            totalRows = totalRows.Take(param.pageSize);
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var people = new AddPeopleViewModel();

            people.Setup(new DocumentFilesService(), new PeopleService(), new CompanyService());

            return View(people);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(AddPeopleViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = _peopleService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "People has been added successfully.";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;

                model.Setup(_documentFilesService, _peopleService, _companyService);

                return View(model);
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            var people = new EditPeopleViewModel();

            var currentUser = GetCurrentUser();
            var result = _workflowService.CheckOutObject(id, currentUser.Id);

            var model = _peopleService.GetPeopleById(result.Entity);

            var phoneInfo = _peopleService.GetPhoneInfoByObjId(id);
            var emailInfo = _peopleService.GetEmailInfoByObjId(id);
            var locationInfo = _peopleService.GetLocationInfoByObjId(id);


            people.MapToDto(model);

            people.Setup(_documentFilesService, _peopleService, _companyService, currentUser.Id);

            if (phoneInfo != null)
            {
                people.PrimaryPhoneNumber = phoneInfo.PrimaryPhoneNumber;
                people.TypePrimaryPhoneNumber = phoneInfo.TypePrimaryPhoneNumber;
                people.ExtPrimaryPhoneNumber = phoneInfo.ExtPrimaryPhoneNumber;
                people.PinPrimaryPhoneNumber = phoneInfo.PinPrimaryPhoneNumber;
            }
            if (emailInfo != null)
            {
                people.EmailPrimary = emailInfo.EmailPrimary;
                people.EmailTypePrimary = emailInfo.EmailTypePrimary;
                people.EmailTextTypePrimary = emailInfo.EmailTextTypePrimary;
                people.EmailIdPrimary = emailInfo.EmailIdPrimary;
            }
            if (locationInfo != null)
            {
                people.AddressLocation = locationInfo.AddressLocation;
                people.AddressType = locationInfo.AddressType;

            }

            var preview = string.Join(",", people.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));

            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();

            var previewConfig = jsonSerialiser.Serialize(people.DocLinks.Select(x => new
            {
                caption = x.NAME,
                type = x.TYPE,
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new {file = x.LINKED_DOC_ID}),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }));

            ViewBag.PreviewConfig = previewConfig;

            return View(people);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(EditPeopleViewModel model, string passowrdReminder)
        {
            var currentUser = GetCurrentUser();

            if (!ModelState.IsValid)
            {
                model.Setup(_documentFilesService, _peopleService, _companyService, currentUser.Id);
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(passowrdReminder))
            {
                var result = _peopleService.PasswordReminderEmail(model.LoginId, model.EmailPrimary);

                if (result.HasErrors())
                {
                    TempData["ErrorMessage"] = result.ErrorMessage;
                }
                else
                {
                    TempData["SuccessMessage"] = "Password reminder email has been sent successfully.";
                }

                return RedirectToAction("Edit", new {model.ObjectId});
            }

            model.NTLogin = currentUser.Id;

            var response = _peopleService.Update(model);

            if (response)
            {
                TempData["SuccessMessage"] = "User has been updated successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";

            return View(model);
        }

        public ActionResult Delete(string id)
        {

            string ntLogin = GetCurrentUser().Id;
            var response = _peopleService.Delete(id, ntLogin);

            if (response)
            {
                TempData["SuccessMessage"] = "People deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
    }
}