CREATE VIEW dbo.A_V_SERVICE_CALL_PAYMENT_LOCATIONS
AS
SELECT     ar_role.RECIEVABLE_ROLE, ar_role.PAYMENT_LOCATION, ar_locaiton.NAME AS SUPPLIER_LOCATION_NAME, 
                      company_logo_phone.LINKED_DOC_ID, ar_locaiton.ADDRESS_1 AS SUPPLIER_ADDRESS, ar_locaiton.CITY AS SUPPLIER_CITY, 
                      ar_locaiton.STATE AS SUPPLIER_STATE, ar_locaiton.POSTAL_CODE AS SUPPLIER_POSTAL_CODE, 
                      company_logo_phone.PHONE AS SUPPLIER_PHONE, company_logo_phone.ID AS COMPANY_ID
FROM         dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE ar_role INNER JOIN
                      dbo.A_APPROVED_LOCATIONS ar_locaiton ON ar_role.PAYMENT_LOCATION = ar_locaiton.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_COMPANIES_WITH_LOGOS company_logo_phone ON ar_role.COMPANY_ID = company_logo_phone.ID