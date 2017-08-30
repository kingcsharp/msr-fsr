using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.PrePro.ViewModel
{
    public class ApplicableObjectsView
    {
        public ApplicableObjectsView()
        {
            ApplicableObjectsList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string StepId { get; set; }

        public string LinkId { get; set; }

        public string Quantity { get; set; }

        public string ObjectId { get; set; }

        public string NTLogin { get; set; }

        public string NEW__1 { get; set; }

        public string NEW__2 { get; set; }

        public string NEW__3 { get; set; }

        public string NEW__4 { get; set; }

        public string NEW__5 { get; set; }

        public string NEW__6 { get; set; }

        public string NEW__7 { get; set; }

        public string NEW__8 { get; set; }

        public string NEW__9 { get; set; }

        public string NEW__10 { get; set; }

        public string NEW__11 { get; set; }

        public string NEW__12 { get; set; }

        public List<SelectListItem> ApplicableObjectsList { get; set; }


        public void Setup(PreProServices preProServices, string creatingCo)
        {
            ApplicableObjectsList.Add(new SelectListItem { Value = "", Text = "" });

            ApplicableObjectsList.AddRange(preProServices.GetApplicableObjectsByCreatingCo(creatingCo).Select(
                x => new SelectListItem
                {
                    Text = x.Show,
                    Value = x.Value.ToString()
                }).OrderBy(o => o.Text).ToList());

        }

    }
}
