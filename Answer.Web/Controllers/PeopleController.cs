using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.People;
using Msr.Services.Orders;
using Msr.Services.People.ViewModels;
using Msr.Services.Companies;
using Msr.Infrastructure.Helpers;
using Msr.Services.Documents;

namespace Answer.Web.Controllers
{
    [Authorize]
    public class PeopleController : BaseController
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "People";

            return View(viewModel);
        }      

        public ActionResult PeopleData(JqGridParam param)
        {
            var peopleService = new PeopleService();

            var totalRows = peopleService.GetPeople();

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
                    //else if (rule.field == nameof(PeopleObjectView.DateHired))
                    //{
                    //    totalRows = totalRows.Where(x => x.DateHired.ToString().Contains(rule.data.ToLower()));
                    //}
                    else if (rule.field == nameof(PeopleObjectView.SystemStatus))
                    {
                        totalRows = totalRows.Where(x => x.SystemStatus.ToLower().Contains(rule.data.ToLower()));
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
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Add(AddPeopleViewModel model)
        {
            var peopleService = new PeopleService();

            if (ModelState.IsValid)
            {
                model.NTLogin = "1618"; // need to dynamic
                model.Password = AuthenticationHelper.PassWordEncrypt("test"); //pending to find password creation
                
                var response = peopleService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "People has been added successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(new DocumentFilesService(), new PeopleService(), new CompanyService());

                    return View(model);
                }
            }

            return View();
        }
        public ActionResult Edit(string id)
        {
            var taskService = new PeopleService();
            var model = taskService.GetPeopleById(id);
            var phoneInfo = taskService.GetPhoneInfoByObjId(id);
            var emailInfo = taskService.GetEmailInfoByObjId(id);
            var locationInfo = taskService.GetLocationInfoByObjId(id);
            var people = new EditPeopleViewModel();
            people = people.MapToDto(model);
            people.Setup(new DocumentFilesService(), new PeopleService(), new CompanyService());
            people.PrimaryPhoneNumber = phoneInfo.PrimaryPhoneNumber;
            people.TypePrimaryPhoneNumber = phoneInfo.TypePrimaryPhoneNumber;
            people.ExtPrimaryPhoneNumber = phoneInfo.ExtPrimaryPhoneNumber;
            people.PinPrimaryPhoneNumber = phoneInfo.PinPrimaryPhoneNumber;
            people.EmailPrimary = emailInfo.EmailPrimary;
            people.EmailTypePrimary = emailInfo.EmailTypePrimary;
            people.EmailTextTypePrimary = emailInfo.EmailTextTypePrimary;
            //people.AddressLocation = locationInfo.AddressLocation;
///            people.AddressType = locationInfo.AddressType;
            return View(people);
        }
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditPeopleViewModel model)
        {
            var peopleService = new PeopleService();
           
            if (ModelState.IsValid)
            {
                model.NTLogin = "1618"; // need to dynamic
                model.Password = AuthenticationHelper.PassWordEncrypt("test"); //pending to find password creation

                var response = peopleService.Edit(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "People has been updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(new DocumentFilesService(), new PeopleService(), new CompanyService());

                    return View(model);
                }
            }

            return View();
        }
    }
}