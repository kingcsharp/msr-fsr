using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;

namespace Msr.Services.jqGrid
{
    public enum OPERATION
    {
        none,
        add,
        del,
        edit,
        excel
    }

    public class JqGridParam
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sortColumn { get; set; }
        public string sortOrder { get; set; }
        public bool isSearch { get; set; }
        public string id { get; set; }
        public string param { get; set; }
        public string editOper { get; set; }
        public string addOper { get; set; }
        public string delOper { get; set; }
        public JqGridFilter where { get; set; }
        public OPERATION operation { get; set; }
    }

    [DataContract]
    public class JqGridFilter
    {
        [DataMember]
        public string groupOp { get; set; }

        [DataMember]
        public Rule[] rules { get; set; }

        public static JqGridFilter Create(string jsonData)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof (JqGridFilter));
                //System.IO.StringReader reader = new System.IO.StringReader(jsonData);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Default.GetBytes(jsonData));
                return serializer.ReadObject(ms) as JqGridFilter;
            }
            catch
            {
                return null;
            }
        }
    }

    [DataContract]
    public class Rule
    {
        [DataMember]
        public string field { get; set; }

        [DataMember]
        public string op { get; set; }

        [DataMember]
        public string data { get; set; }
    }

}