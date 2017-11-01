using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class ReferenceTheoryResponse
    {
        public ReferenceTheoryResponse()
        {
            TheoryGetInfoResult = new TheoryGetInfoResult();
            ObjectInfoResult = new ObjectInfoResult();
            WorkflowDataResult = new WorkflowDataResult();
            TheoryAdditionalCommentResults = new List<TheoryAdditionalCommentResult>();
            GetTheoryReferenceFilesResults = new List<GetTheoryReferenceFilesResult>();
        }

        public int TheoryId { get; set; }
        public int PhStepId { get; set; }
        public TheoryGetInfoResult TheoryGetInfoResult { get; set; }
        public ObjectInfoResult ObjectInfoResult { get; set; }
        public WorkflowDataResult WorkflowDataResult { get; set; }
        public List<TheoryAdditionalCommentResult> TheoryAdditionalCommentResults { get; set; }
        public List<GetTheoryReferenceFilesResult> GetTheoryReferenceFilesResults { get; set; }
        public List<ReferenceTheoryResult> ReferenceTheoryResults { get; set; }
        public List<TheoryGetDepartmentResult> TheoryGetDepartmentResults { get; set; }
        public List<TheoryGetCompanyAddressResult> TheoryGetCompanyAddressResults { get; set; }
        public List<TheoryGetSecurityLevelResult> TheoryGetSecurityLevelResults { get; set; }
        public List<TheoryGetRolesResult> TheoryGetRolesResults { get; set; }
        public List<TheoryGetParagraphDataResult> TheoryGetParagraphDataResults { get; set; }
        public List<TheoryGetRevisionDataResult> TheoryGetRevisionDataResults { get; set; }
    }
}
