using Msr.Models.Procedures;
using Msr.Services.jqGrid;
using Msr.Services.Procedures;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using Msr.Services.Roles;
using Msr.Services.Users;

namespace Answer.Web.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly ProceduresService _proceduresService;
        private readonly UserService _userService;

        const string AppDataGlobalsettingsXml = @"/App_Data//procedureFolder/";
        const string XSLTPath = @"/assets//styles//xslt//procedures.xsl";

        public ProceduresController()
        {
            _proceduresService = new ProceduresService();
            _userService = new UserService();
        }


        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Procedures";

            return View(viewModel);
        }
        public ActionResult GetProcedures(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            return PartialView("_Procedures");
        }

        public ActionResult ProceduresData(JqGridParam param)
        {
            var procedureService = new ProceduresService();

            var totalRows = procedureService.GetProceduresQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.VerbName))
                    {
                        totalRows = totalRows.Where(x => x.VerbName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.SecurityLevel))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevel.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(ProcedureView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }

            var orderBy = nameof(ProcedureView.Id);

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

        public ActionResult Create()
        {
            var saveProcedureViewModel = new SaveProcedureViewModel();

            saveProcedureViewModel.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService());

            return View(saveProcedureViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveProcedureViewModel model)
        {
            var procedureService = new ProceduresService();

            //need to be dynamic
            model.NTLogin = "1618";

            if (ModelState.IsValid)
            {
                var response = procedureService.Create(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService());

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var saveProcedureViewModel = new SaveProcedureViewModel();
            var procedureService = new ProceduresService();

            var model = procedureService.GetProcedureById(id: id);

            saveProcedureViewModel = saveProcedureViewModel.MapToDto(model);
            saveProcedureViewModel.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService());

            return View(saveProcedureViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveProcedureViewModel model)
        {
            var procedureService = new ProceduresService();

            //need to be dynamic
            model.NTLogin = "1618";

            if (ModelState.IsValid)
            {
                var response = procedureService.Save(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService());

            return View(model);
        }

        public ActionResult View(string id)
        {
            var saveProcedureViewModel = new ViewProcedureViewModel();

            var procedureService = new ProceduresService();

            var approvedData = procedureService.GetApprovedData(id);
            string filePath = Server.MapPath(AppDataGlobalsettingsXml);

            var doc = new XmlDocument();
            doc.Load(filePath + approvedData.Object_Id + ".xml");

            var html = GetHtml(Server.MapPath(XSLTPath), doc.InnerXml.ToString());

            return View((object)html);
        }

        private string GetHtml(string xsltPath, string xml)
        {
            var stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(xml));
            var document = new XPathDocument(stream);
            var writer = new StringWriter();
            var transform = new XslCompiledTransform();
            transform.Load(xsltPath);
            transform.Transform(document, null, writer);
            return writer.ToString();
        }

        public ActionResult AssignProcedure(string id)
        {
            var vm = new AssignProcedureViewModel();

            vm.Setup(new UserService());

            var procedureName = _proceduresService.GetProcedureName(id);

            vm.ProcedureName = procedureName;

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AssignProcedure(AssignProcedureViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _proceduresService.SaveAssignProcedure(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Procedure has been created successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_userService);

                return View(model);
            }

            model.Setup(_userService);

            return View(model);
        }
    }
}