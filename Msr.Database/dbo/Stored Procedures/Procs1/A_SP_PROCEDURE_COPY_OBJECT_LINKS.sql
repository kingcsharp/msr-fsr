
--This is called from:
--A_SP_PROCEDURE_COPY_ONE
--A_SP_PROCEDURE_STEP_COPY_ONE

CREATE       PROCEDURE A_SP_PROCEDURE_COPY_OBJECT_LINKS
@oldProcID nvarchar(50),
@oldStepID nvarchar(50),
@newProcID nvarchar(50),
@newStepID nvarchar(50),
@strNTLogin nvarchar(50)
AS
--if the step id is null then just pull all the ones where the procid is the same and step is null
print 'Copying the step procedure objects'
print 'Old step ID = '
print @oldStepID
print 'oldProcID = '
print @oldProcID
print '@newProcID'
print @newProcID
print '@newStepID'
print @newStepID
print '-------'
--SELECT 'This is the objects for step ID = ' + isnull(@oldStepID,'NULL') + ' proc ID ' + isnull(@oldProcID,'NULL')  
--SELECT newID(),PROCEDURE_ID,STEP_ID,APPROVED_OBJECT_ID,
--		QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,ID,LABOR_ROLE
--		FROM A_PROCEDURE_OBJECT_LINK WHERE STEP_ID = isNULL(@oldStepID,'%')

if @oldStepID is null
	begin
		INSERT INTO A_PROCEDURE_OBJECT_LINK(ID,PROCEDURE_ID,STEP_ID,APPROVED_OBJECT_ID,
		QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,COPIED_FROM,LABOR_ROLE)
		SELECT newID(),@newProcID,@newStepID,APPROVED_OBJECT_ID,QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,ID,LABOR_ROLE
		FROM A_PROCEDURE_OBJECT_LINK WHERE PROCEDURE_ID = @oldProcID and STEP_ID is null
	end
else
	begin
		INSERT INTO A_PROCEDURE_OBJECT_LINK(ID,PROCEDURE_ID,STEP_ID,APPROVED_OBJECT_ID,
			QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,COPIED_FROM,LABOR_ROLE)
		SELECT newID(),@newProcID,@newStepID,APPROVED_OBJECT_ID,QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,ID,LABOR_ROLE
		FROM A_PROCEDURE_OBJECT_LINK WHERE STEP_ID = @oldStepID
	end


