






CREATE     PROCEDURE A_SP_NOUN_HIER_DELETE
@id nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'Deleting a Noun Hierarchy'
--First Delete all the children
print 'Deleting the children first'
DELETE FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE HIERARCHY_ID = @id
--Then delete the main item
print 'Deleting the hierarchy now'
DELETE FROM A_NOUN_HIERARCHIES_HISTORY WHERE ID = @id





