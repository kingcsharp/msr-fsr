

CREATE   PROCEDURE dbo.A_X_UPDATE_THINGS_FOR_NEW_DATABASE_REVISION
AS
--exec A_Z_OBJECTS_UPDATE_ALL_NAMES
--exec A_X_TASKS_UPDATE_DATE_TO_FIRE_TRIGGER

INSERT INTO A_Z_ORDER_ITEMS_BILL_TYPES (ID,DESCRIPTION,DRCM,MODBY)
	VALUES 
('ACTUAL','This item will only be used when the purchase is determined to be actuals only',getDate(),'SYSTEM')
INSERT INTO A_Z_ORDER_ITEMS_BILL_TYPES (ID,DESCRIPTION,DRCM,MODBY)
	VALUES 
('ESTIMATE','This item will only be used when the purchase is determined to be estimates only',getDate(),'SYSTEM')
INSERT INTO A_Z_ORDER_ITEMS_BILL_TYPES (ID,DESCRIPTION,DRCM,MODBY)
	VALUES 
('ACT_EST','This item will only be used when the purchase is determined to be actuals or estimate',getDate(),'SYSTEM')




