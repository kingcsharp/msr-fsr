

CREATE   PROCEDURE dbo.A_SP_PROD_REQ_FORM_SEND_UPDATE_EMAIL
@ID varchar(50),
@strNTLogin varchar(50)
AS

declare @linkPath varchar (1000), @emailSubject varchar(1000),
	@emailBody varchar(8000)

set @linkPath = dbo.xmlEncode('asp/prodReqForm/editReqForm.asp?ID=' + @ID)

declare @curProg varchar(50)
SELECT @curProg = PROGRESS FROM A_PROD_REQ_FORMS WHERE ID = @ID
set @emailSubject = 'Product Request Form Notification'

declare 
	@CustPersonID varchar(50),
	@CustPersonName nvarchar(1000),
	@CustName nvarchar(1000),
	@CustID varchar(50),
	@supplierName nvarchar(1000),
	@supplierID nvarchar(1000),
	@customProd nvarchar(2000),
	@customProdID varchar(2000),
	@resultingProductName nvarchar(1000),
	@custMGR varchar(50)

SELECT	
	@custPersonID = CUST_PERSON_ID,
	@custPersonName = CUST_PERSON_NAME,
	@custID = CUSTOMER_CO,
	@custNAME = CUSTOMER_NAME,
	@supplierNAME = SUPPLIER_NAME,
	@supplierID = SUPPLIER_ID,
	@customProd = PRODUCT_NAME,
	@customProdID = PRODUCT_ID,
	@resultingProductName = RES_PROD_NAME,
	@custMGR = CUST_MGR_ROLE
	FROM A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA
	WHERE ID = @ID


set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Customizable Product" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@customProd,50)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Supplier" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@supplierNAME,20)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Customer Name" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@custNAME,20)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Progress" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull('PRIORITY_' + dbo.xmlEncode(@curProg),'') + '" /></obj>
		</obj>
	</obj>
</obj>
'

declare @combinedRequesteeList varchar(2000)

if @curProg = 'PRQ_SUBMITTED' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@custMGR,null,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @curProg = 'PRQ_ACCEPTED' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@custPersonID,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @curProg = 'PRQ_ENG_CAN_MAKE' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@custPersonID,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @curProg = 'PRQ_ENG_MADE_PROD' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@custPersonID,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @curProg = 'PRQ_IMPOSSIBLE_TO_MAKE' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@custPersonID,@linkPath,@emailSubject,@emailBody,@strNTLogin





