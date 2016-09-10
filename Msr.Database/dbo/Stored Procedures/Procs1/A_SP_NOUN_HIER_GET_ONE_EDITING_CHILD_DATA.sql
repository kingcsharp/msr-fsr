










CREATE    PROCEDURE A_SP_NOUN_HIER_GET_ONE_EDITING_CHILD_DATA
	@strHier nvarchar(50),
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
SELECT * FROM A_V_NOUN_HIER_CHILDREN_EDITING_DATA WHERE ID = @strID AND HIERARCHY_ID = @strHier










