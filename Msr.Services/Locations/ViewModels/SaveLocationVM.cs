using Msr.Models.Locations;
using Msr.Services.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.Locations.ViewModels
{
    public class SaveLocationViewModel : BaseNotificationRequest
    {
        public SaveLocationViewModel()
        {
            ParentsLocations = new List<SelectListItem>();
        }
        public string ObjectId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Name { get; set; }

        [Display(Name = "Parent Location")]
        public string Parent { get; set; }

        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "State/Province")]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        public string Region { get; set; }

        [Display(Name = "Internal Address")]
        public string InternalAddress { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public List<SelectListItem> ParentsLocations { get; set; }

        public List<SelectListItem> Regions { get; set; }

        public void Setup(RegionService regionService, LocationService locationService)
        {
            Regions = regionService.RegionsQueryable.Where(x=>x.Status == "APPROVED").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();

            ParentsLocations = locationService.GetLocationsQueryable().Where(x=> x.Status == "APPROVED").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjId
            }).OrderBy(o => o.Text).ToList();

            Countries = GetCountries();

        }
        public SaveLocationViewModel MapToDto(LocationView model)
        {
            return new SaveLocationViewModel
            {
                ObjectId = model.ObjectId,
                Name = model.Name,
                Parent = model.ParentLocation,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PostalCode = model.PostalCode,
                Region = model.Region,
                InternalAddress = model.InternalAddress
            };
        }

        private List<SelectListItem> GetCountries()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "United States of America",
                    Value = "United States of America",
                },
                new SelectListItem
                {
                    Text = "Afganistan",
                    Value = "Afganistan"
                },
                new SelectListItem
                {
                    Text = "Albania",
                    Value = "Albania"
                },
                new SelectListItem
                {
                    Text = "Algeria",
                    Value = "Algeria"
                },
                new SelectListItem
                {
                    Text = "American Samoa",
                    Value = "American Samoa"
                },
                new SelectListItem
                {
                    Text = "Andorra",
                    Value = "Andorra"
                },
                new SelectListItem
                {
                    Text = "Angola",
                    Value = "Angola"
                },
                new SelectListItem
                {
                    Text = "Anguilla",
                    Value = "Anguilla"
                },
                new SelectListItem
                {
                    Text = "Antigua & Barbuda",
                    Value = "Antigua & Barbuda"
                },
                new SelectListItem
                {
                    Text = "Argentina",
                    Value = "Argentina"
                },
                new SelectListItem
                {
                    Text = "Armenia",
                    Value = "Armenia"
                },
                new SelectListItem
                {
                    Text = "Aruba",
                    Value = "Aruba"
                },
                new SelectListItem
                {
                    Text = "Australia",
                    Value = "Australia"
                },
                new SelectListItem
                {
                    Text = "Austria",
                    Value = "Austria"
                },
                new SelectListItem
                {
                    Text = "Azerbaijan",
                    Value = "Azerbaijan"
                },
                new SelectListItem
                {
                    Text = "Azores",
                    Value = "Azores"
                },
                new SelectListItem
                {
                    Text = "Bahamas",
                    Value = "Bahamas"
                },
                new SelectListItem
                {
                    Text = "Bahrain",
                    Value = "Bahrain"
                },
                new SelectListItem
                {
                    Text = "Bangladesh",
                    Value = "Bangladesh"
                },
                new SelectListItem
                {
                    Text = "Barbados",
                    Value = "Barbados"
                },
                new SelectListItem
                {
                    Text = "Belarus",
                    Value = "Belarus"
                },
                new SelectListItem
                {
                    Text = "Belgium",
                    Value = "Belgium"
                },
                new SelectListItem
                {
                    Text = "Belize",
                    Value = "Belize"
                },
                new SelectListItem
                {
                    Text = "Benin",
                    Value = "Benin"
                },
                new SelectListItem
                {
                    Text = "Bermuda",
                    Value = "Bermuda"
                },
                new SelectListItem
                {
                    Text = "Bhutan",
                    Value = "Bhutan"
                },
                new SelectListItem
                {
                    Text = "Bolivia",
                    Value = "Bolivia"
                },
                new SelectListItem
                {
                    Text = "Bonaire",
                    Value = "Bonaire"
                },
                new SelectListItem
                {
                    Text = "Bosnia & Herzegovina",
                    Value = "Bosnia & Herzegovina"
                },
                new SelectListItem
                {
                    Text = "Botswana",
                    Value = "Botswana"
                },
                new SelectListItem
                {
                    Text = "Brazil",
                    Value = "Brazil"
                },
                new SelectListItem
                {
                    Text = "British Indian Ocean Ter",
                    Value = "British Indian Ocean Ter"
                },
                new SelectListItem
                {
                    Text = "Brunei",
                    Value = "Brunei"
                },
                new SelectListItem
                {
                    Text = "Bulgaria",
                    Value = "Bulgaria"
                },
                new SelectListItem
                {
                    Text = "Burkina Faso",
                    Value = "Burkina Faso"
                },
                new SelectListItem
                {
                    Text = "Burundi",
                    Value = "Burundi"
                },
                new SelectListItem
                {
                    Text = "Cambodia",
                    Value = "Cambodia"
                },
                new SelectListItem
                {
                    Text = "Cameroon",
                    Value = "Cameroon"
                },
                new SelectListItem
                {
                    Text = "Canada",
                    Value = "Canada"
                },
                new SelectListItem
                {
                    Text = "Canary Islands",
                    Value = "Canary Islands"
                }
                ,
                new SelectListItem
                {
                    Text = "Cape Verde",
                    Value = "Cape Verde"
                },
                new SelectListItem
                {
                    Text = "Cayman Islands",
                    Value = "Cayman Islands"
                },
                new SelectListItem
                {
                    Text = "Central African Republic",
                    Value = "Central African Republic"
                },
                new SelectListItem
                {
                    Text = "Chad",
                    Value = "Chad"
                },
                new SelectListItem
                {
                    Text = "Channel Islands",
                    Value = "Channel Islands"
                },
                new SelectListItem
                {
                    Text = "Chile",
                    Value = "Chile"
                },
                new SelectListItem
                {
                    Text = "China",
                    Value = "China"
                },
                new SelectListItem
                {
                    Text = "Christmas Island",
                    Value = "Christmas Island"
                },
                new SelectListItem
                {
                    Text = "Cocos Island",
                    Value = "Cocos Island"
                },
                new SelectListItem
                {
                    Text = "Columbia",
                    Value = "Columbia"
                } ,
                new SelectListItem
                {
                    Text = "Comoros",
                    Value = "Comoros"
                },
                new SelectListItem
                {
                    Text = "Congo",
                    Value = "Congo"
                },
                new SelectListItem
                {
                    Text = "Cook Islands",
                    Value = "Cook Islands"
                },
                new SelectListItem
                {
                    Text = "Costa Rica",
                    Value = "Costa Rica"
                },
                new SelectListItem
                {
                    Text = "Cote D'Ivoire",
                    Value = "Cote DIvoire"
                },
                new SelectListItem
                {
                    Text = "Croatia",
                    Value = "Croatia"
                },
                new SelectListItem
                {
                    Text = "Cuba",
                    Value = "Cuba"
                },
                new SelectListItem
                {
                    Text = "Curaco",
                    Value = "Curaco"
                },
                new SelectListItem
                {
                    Text = "Cyprus",
                    Value = "Cyprus"
                },
                new SelectListItem
                {
                    Text = "Czech Republic",
                    Value = "Czech Republic"
                },
                new SelectListItem
                {
                    Text = "Denmark",
                    Value = "Denmark"
                },
                new SelectListItem
                {
                    Text = "Djibouti",
                    Value = "Djibouti"
                },
                new SelectListItem
                {
                    Text = "Dominica",
                    Value = "Dominica"
                },
                new SelectListItem
                {
                    Text = "Dominican Republic",
                    Value = "Dominican Republic"
                },
                new SelectListItem
                {
                    Text = "East Timor",
                    Value = "East Timor"
                },
                new SelectListItem
                {
                    Text = "Ecuador",
                    Value = "Ecuador"
                },
                new SelectListItem
                {
                    Text = "Egypt",
                    Value = "Egypt"
                },
                new SelectListItem
                {
                    Text = "El Salvador",
                    Value = "El Salvador"
                },
                new SelectListItem
                {
                    Text = "Equatorial Guinea",
                    Value = "Equatorial Guinea"
                },
                new SelectListItem
                {
                    Text = "Eritrea",
                    Value = "Eritrea"
                },
                new SelectListItem
                {
                    Text = "Estonia",
                    Value = "Estonia"
                },
                new SelectListItem
                {
                    Text = "Ethiopia",
                    Value = "Ethiopia"
                },
                new SelectListItem
                {
                    Text = "Falkland Islands",
                    Value = "Falkland Islands"
                },
                new SelectListItem
                {
                    Text = "Faroe Islands",
                    Value = "Faroe Islands"
                },
                new SelectListItem
                {
                    Text = "Fiji",
                    Value = "Fiji"
                },
                new SelectListItem
                {
                    Text = "Finland",
                    Value = "Finland"
                },
                new SelectListItem
                {
                    Text = "France",
                    Value = "France"
                },
                new SelectListItem
                {
                    Text = "French Guiana",
                    Value = "French Guiana"
                },
                new SelectListItem
                {
                    Text = "French Polynesia",
                    Value = "French Polynesia"
                },
                new SelectListItem
                {
                    Text = "French Southern Ter",
                    Value = "French Southern Ter"
                },
                new SelectListItem
                {
                    Text = "Gabon",
                    Value = "Gabon"
                },
                new SelectListItem
                {
                    Text = "Gambia",
                    Value = "Gambia"
                },
                new SelectListItem
                {
                    Text = "Georgia",
                    Value = "Georgia"
                },
                new SelectListItem
                {
                    Text = "Germany",
                    Value = "Germany"
                },
                new SelectListItem
                {
                    Text = "Ghana",
                    Value = "Ghana"
                },
                new SelectListItem
                {
                    Text = "Gibraltar",
                    Value = "Gibraltar"
                },
                new SelectListItem
                {
                    Text = "Great Britain",
                    Value = "Great Britain"
                },
                new SelectListItem
                {
                    Text = "Greece",
                    Value = "Greece"
                },
                new SelectListItem
                {
                    Text = "Greenland",
                    Value = "Greenland"
                },
                new SelectListItem
                {
                    Text = "Grenada",
                    Value = "Grenada"
                },
                new SelectListItem
                {
                    Text = "Guadeloupe",
                    Value = "Guadeloupe"
                },
                new SelectListItem
                {
                    Text = "Guam",
                    Value = "Guam"
                },
                new SelectListItem
                {
                    Text = "Guatemala",
                    Value = "Guatemala"
                },
                new SelectListItem
                {
                    Text = "Guinea",
                    Value = "Guinea"
                },
                new SelectListItem
                {
                    Text = "Guyana",
                    Value = "Guyana"
                },
                new SelectListItem
                {
                    Text = "Haiti",
                    Value = "Haiti"
                },
                new SelectListItem
                {
                    Text = "Hawaii",
                    Value = "Hawaii"
                },
                new SelectListItem
                {
                    Text = "Honduras",
                    Value = "Honduras"
                },
                new SelectListItem
                {
                    Text = "Hong Kong",
                    Value = "Hong Kong"
                },
                new SelectListItem
                {
                    Text = "Hungary",
                    Value = "Hungary"
                },
                new SelectListItem
                {
                    Text = "Iceland",
                    Value = "Iceland"
                },
                new SelectListItem
                {
                    Text = "India",
                    Value = "India"
                },
                new SelectListItem
                {
                    Text = "Indonesia",
                    Value = "Indonesia"
                },
                new SelectListItem
                {
                    Text = "Iran",
                    Value = "Iran"
                },
                new SelectListItem
                {
                    Text = "Iraq",
                    Value = "Iraq"
                },
                new SelectListItem
                {
                    Text = "Ireland",
                    Value = "Ireland"
                },
                new SelectListItem
                {
                    Text = "Isle of Man",
                    Value = "Isle of Man"
                },
                new SelectListItem
                {
                    Text = "Israel",
                    Value = "Israel"
                },
                new SelectListItem
                {
                    Text = "Italy",
                    Value = "Italy"
                },
                new SelectListItem
                {
                    Text = "Jamaica",
                    Value = "Jamaica"
                },
                new SelectListItem
                {
                    Text = "Japan",
                    Value = "Japan"
                },
                new SelectListItem
                {
                    Text = "Jordan",
                    Value = "Jordan"
                },
                new SelectListItem
                {
                    Text = "Kazakhstan",
                    Value = "Kazakhstan"
                },
                new SelectListItem
                {
                    Text = "Kenya",
                    Value = "Kenya"
                },
                new SelectListItem
                {
                    Text = "Kiribati",
                    Value = "Kiribati"
                },
                new SelectListItem
                {
                    Text = "Korea North",
                    Value = "Korea North"
                },
                new SelectListItem
                {
                    Text = "Korea Sout",
                    Value = "Korea Sout"
                },
                new SelectListItem
                {
                    Text = "Kuwait",
                    Value = "Kuwait"
                },
                new SelectListItem
                {
                    Text = "Kyrgyzstan",
                    Value = "Kyrgyzstan"
                },
                new SelectListItem
                {
                    Text = "Laos",
                    Value = "Laos"
                },
                new SelectListItem
                {
                    Text = "Latvia",
                    Value = "Latvia"
                },
                new SelectListItem
                {
                    Text = "Lebanon",
                    Value = "Lebanon"
                },
                new SelectListItem
                {
                    Text = "Lesotho",
                    Value = "Lesotho"
                },
                new SelectListItem
                {
                    Text = "Liberia",
                    Value = "Liberia"
                },
                new SelectListItem
                {
                    Text = "Libya",
                    Value = "Libya"
                },
                new SelectListItem
                {
                    Text = "Liechtenstein",
                    Value = "Liechtenstein"
                },
                new SelectListItem
                {
                    Text = "Lithuania",
                    Value = "Lithuania"
                },
                new SelectListItem
                {
                    Text = "Luxembourg",
                    Value = "Luxembourg"
                },
                new SelectListItem
                {
                    Text = "Macau",
                    Value = "Macau"
                },
                new SelectListItem
                {
                    Text = "Macedonia",
                    Value = "Macedonia"
                },
                new SelectListItem
                {
                    Text = "Madagascar",
                    Value = "Madagascar"
                },
                new SelectListItem
                {
                    Text = "Malaysia",
                    Value = "Malaysia"
                },
                new SelectListItem
                {
                    Text = "Malawi",
                    Value = "Malawi"
                },
                new SelectListItem
                {
                    Text = "Maldives",
                    Value = "Maldives"
                },
                new SelectListItem
                {
                    Text = "Mali",
                    Value = "Mali"
                },
                new SelectListItem
                {
                    Text = "Malta",
                    Value = "Malta"
                },
                new SelectListItem
                {
                    Text = "Marshall Islands",
                    Value = "Marshall Islands"
                },
                new SelectListItem
                {
                    Text = "Martinique",
                    Value = "Martinique"
                },
                new SelectListItem
                {
                    Text = "Mauritania",
                    Value = "Mauritania"
                },
                new SelectListItem
                {
                    Text = "Mauritius",
                    Value = "Mauritius"
                },
                new SelectListItem
                {
                    Text = "Mayotte",
                    Value = "Mayotte"
                },
                new SelectListItem
                {
                    Text = "Mexico",
                    Value = "Mexico"
                },
                new SelectListItem
                {
                    Text = "Midway Islands",
                    Value = "Midway Islands"
                },
                new SelectListItem
                {
                    Text = "Moldova",
                    Value = "Moldova"
                },
                new SelectListItem
                {
                    Text = "Monaco",
                    Value = "Monaco"
                },
                new SelectListItem
                {
                    Text = "Mongolia",
                    Value = "Mongolia"
                },
                new SelectListItem
                {
                    Text = "Montserrat",
                    Value = "Montserrat"
                },
                new SelectListItem
                {
                    Text = "Morocco",
                    Value = "Morocco"
                },
                new SelectListItem
                {
                    Text = "Mozambique",
                    Value = "Mozambique"
                },
                new SelectListItem
                {
                    Text = "Myanmar",
                    Value = "Myanmar"
                },
                new SelectListItem
                {
                    Text = "Nambia",
                    Value = "Nambia"
                },
                new SelectListItem
                {
                    Text = "Nauru",
                    Value = "Nauru"
                },
                new SelectListItem
                {
                    Text = "Nepal",
                    Value = "Nepal"
                },
                new SelectListItem
                {
                    Text = "Netherland Antilles",
                    Value = "Netherland Antilles"
                },
                new SelectListItem
                {
                    Text = "Netherlands",
                    Value = "Netherlands"
                },
                new SelectListItem
                {
                    Text = "Nevis",
                    Value = "Nevis"
                },
                new SelectListItem
                {
                    Text = "New Caledonia",
                    Value = "New Caledonia"
                },
                new SelectListItem
                {
                    Text = "New Zealand",
                    Value = "New Zealand"
                },
                new SelectListItem
                {
                    Text = "Nicaragua",
                    Value = "Nicaragua"
                },
                new SelectListItem
                {
                    Text = "Niger",
                    Value = "Niger"
                },
                new SelectListItem
                {
                    Text = "Nigeria",
                    Value = "Nigeria"
                },
                new SelectListItem
                {
                    Text = "Niue",
                    Value = "Niue"
                },
                new SelectListItem
                {
                    Text = "Norfolk Island",
                    Value = "Norfolk Island"
                },
                new SelectListItem
                {
                    Text = "Norway",
                    Value = "Norway"
                },
                new SelectListItem
                {
                    Text = "Oman",
                    Value = "Oman"
                },
                new SelectListItem
                {
                    Text = "Pakistan",
                    Value = "Pakistan"
                },
                new SelectListItem
                {
                    Text = "Palau Island",
                    Value = "Palau Island"
                },
                new SelectListItem
                {
                    Text = "Palestine",
                    Value = "Palestine"
                },
                new SelectListItem
                {
                    Text = "Panama",
                    Value = "Panama"
                },
                new SelectListItem
                {
                    Text = "Papua New Guinea",
                    Value = "Papua New Guinea"
                },
                new SelectListItem
                {
                    Text = "Paraguay",
                    Value = "Paraguay"
                },
                new SelectListItem
                {
                    Text = "Peru",
                    Value = "Peru"
                },
                new SelectListItem
                {
                    Text = "Phillipines",
                    Value = "Phillipines"
                },
                new SelectListItem
                {
                    Text = "Pitcairn Island",
                    Value = "Pitcairn Island"
                },
                new SelectListItem
                {
                    Text = "Poland",
                    Value = "Poland"
                },
                new SelectListItem
                {
                    Text = "Portugal",
                    Value = "Portugal"
                },
                new SelectListItem
                {
                    Text = "Puerto Rico",
                    Value = "Puerto Rico"
                },
                new SelectListItem
                {
                    Text = "Qatar",
                    Value = "Qatar"
                },
                new SelectListItem
                {
                    Text = "Reunion",
                    Value = "Reunion"
                },
                new SelectListItem
                {
                    Text = "Romania",
                    Value = "Romania"
                },
                new SelectListItem
                {
                    Text = "Russia",
                    Value = "Russia"
                },
                new SelectListItem
                {
                    Text = "Rwanda",
                    Value = "Rwanda"
                },
                new SelectListItem
                {
                    Text = "St Barthelemy",
                    Value = "St Barthelemy"
                },
                new SelectListItem
                {
                    Text = "St Eustatius",
                    Value = "St Eustatius"
                },
                new SelectListItem
                {
                    Text = "St Helena",
                    Value = "St Helena"
                },
                new SelectListItem
                {
                    Text = "St Kitts-Nevis",
                    Value = "St Kitts-Nevis"
                },
                new SelectListItem
                {
                    Text = "St Lucia",
                    Value = "St Lucia"
                },
                new SelectListItem
                {
                    Text = "St Maarten",
                    Value = "St Maarten"
                },
                new SelectListItem
                {
                    Text = "St Pierre & Miquelon",
                    Value = "St Pierre & Miquelon"
                },
                new SelectListItem
                {
                    Text = "St Vincent & Grenadines",
                    Value = "St Vincent & Grenadines"
                },
                new SelectListItem
                {
                    Text = "Saipan",
                    Value = "Saipan"
                },
                new SelectListItem
                {
                    Text = "Samoa",
                    Value = "Samoa"
                },
                new SelectListItem
                {
                    Text = "Samoa American",
                    Value = "Samoa American"
                },
                new SelectListItem
                {
                    Text = "San Marino",
                    Value = "San Marino"
                },
                new SelectListItem
                {
                    Text = "Sao Tome & Principe",
                    Value = "Sao Tome & Principe"
                },
                new SelectListItem
                {
                    Text = "Saudi Arabia",
                    Value = "Saudi Arabia"
                },
                new SelectListItem
                {
                    Text = "Senegal",
                    Value = "Senegal"
                },
                new SelectListItem
                {
                    Text = "Seychelles",
                    Value = "Seychelles"
                },
                new SelectListItem
                {
                    Text = "Serbia & Montenegro",
                    Value = "Serbia & Montenegro"
                },
                new SelectListItem
                {
                    Text = "Sierra Leone",
                    Value = "Sierra Leone"
                },
                new SelectListItem
                {
                    Text = "Singapore",
                    Value = "Singapore"
                },
                new SelectListItem
                {
                    Text = "Slovakia",
                    Value = "Slovakia"
                },
                new SelectListItem
                {
                    Text = "Slovenia",
                    Value = "Slovenia"
                },
                new SelectListItem
                {
                    Text = "Solomon Islands",
                    Value = "Solomon Islands"
                },
                new SelectListItem
                {
                    Text = "Somalia",
                    Value = "Somalia"
                },
                new SelectListItem
                {
                    Text = "South Africa",
                    Value = "South Africa"
                },
                new SelectListItem
                {
                    Text = "Spain",
                    Value = "Spain"
                },
                new SelectListItem
                {
                    Text = "Sri Lanka",
                    Value = "Sri Lanka"
                },
                new SelectListItem
                {
                    Text = "Sudan",
                    Value = "Sudan"
                },
                new SelectListItem
                {
                    Text = "Suriname",
                    Value = "Suriname"
                },
                new SelectListItem
                {
                    Text = "Swaziland",
                    Value = "Swaziland"
                },
                new SelectListItem
                {
                    Text = "Sweden",
                    Value = "Sweden"
                },
                new SelectListItem
                {
                    Text = "Switzerland",
                    Value = "Switzerland"
                },
                new SelectListItem
                {
                    Text = "Syria",
                    Value = "Syria"
                },
                new SelectListItem
                {
                    Text = "Tahiti",
                    Value = "Tahiti"
                },
                new SelectListItem
                {
                    Text = "Taiwan",
                    Value = "Taiwan"
                },
                new SelectListItem
                {
                    Text = "Tajikistan",
                    Value = "Tajikistan"
                },
                new SelectListItem
                {
                    Text = "Tanzania",
                    Value = "Tanzania"
                },
                new SelectListItem
                {
                    Text = "Thailand",
                    Value = "Thailand"
                },
                new SelectListItem
                {
                    Text = "Togo",
                    Value = "Togo"
                },
                new SelectListItem
                {
                    Text = "Tokelau",
                    Value = "Tokelau"
                },
                new SelectListItem
                {
                    Text = "Tonga",
                    Value = "Tonga"
                },
                new SelectListItem
                {
                    Text = "Trinidad & Tobago",
                    Value = "Trinidad & Tobago"
                },
                new SelectListItem
                {
                    Text = "Tunisia",
                    Value = "Tunisia"
                },
                new SelectListItem
                {
                    Text = "Turkey",
                    Value = "Turkey"
                },
                new SelectListItem
                {
                    Text = "Turkmenistan",
                    Value = "Turkmenistan"
                },
                new SelectListItem
                {
                    Text = "Turks & Caicos Is",
                    Value = "Turks & Caicos Is"
                },
                new SelectListItem
                {
                    Text = "Tuvalu",
                    Value = "Tuvalu"
                },
                new SelectListItem
                {
                    Text = "Uganda",
                    Value = "Uganda"
                },
                new SelectListItem
                {
                    Text = "Ukraine",
                    Value = "Ukraine"
                },
                new SelectListItem
                {
                    Text = "United Arab Erimates",
                    Value = "United Arab Erimates"
                },
                new SelectListItem
                {
                    Text = "United Kingdom",
                    Value = "United Kingdom"
                },
                new SelectListItem
                {
                    Text = "United States of America",
                    Value = "United States of America"
                },
                new SelectListItem
                {
                    Text = "Uraguay",
                    Value = "Uraguay"
                },
                new SelectListItem
                {
                    Text = "Uzbekistan",
                    Value = "Uzbekistan"
                },
                new SelectListItem
                {
                    Text = "Vanuatu",
                    Value = "Vanuatu"
                },
                new SelectListItem
                {
                    Text = "Vatican City State",
                    Value = "Vatican City State"
                },
                new SelectListItem
                {
                    Text = "Venezuela",
                    Value = "Venezuela"
                },
                new SelectListItem
                {
                    Text = "Vietnam",
                    Value = "Vietnam"
                },
                new SelectListItem
                {
                    Text = "Virgin Islands (Brit)",
                    Value = "Virgin Islands (Brit)"
                },
                new SelectListItem
                {
                    Text = "Virgin Islands (USA)",
                    Value = "Virgin Islands (USA)"
                },
                new SelectListItem
                {
                    Text = "Wake Island",
                    Value = "Wake Island"
                },
                new SelectListItem
                {
                    Text = "Wallis & Futana Is",
                    Value = "Wallis & Futana Is"
                },
                new SelectListItem
                {
                    Text = "Yemen",
                    Value = "Yemen"
                },
                new SelectListItem
                {
                    Text = "Zaire",
                    Value = "Zaire"
                },
                new SelectListItem
                {
                    Text = "Zambia",
                    Value = "Zambia"
                },
                new SelectListItem
                {
                    Text = "Zimbabwe",
                    Value = "Zimbabwe"
                }

            };
        }
    }
}
