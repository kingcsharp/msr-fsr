
CREATE FUNCTION dbo.A_FN_PURCHSES_GET_QUOTE_ACCOUNT_STATUS
	(@PURCHASE_ID varchar(50),@QUOTE_ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
	declare @tester varchar(50), @ret varchar(50)
	SELECT @tester = ID FROM A_V_PURCHASE_ITEM_COMPLETE_DATA WHERE 
		PURCHASE_ID = @PURCHASE_ID AND QUOTE_ID = @QUOTE_ID AND ACCT_ID is NULL
	if @tester is not null	set @ret = 'ITEM_NEEDS_ACCOUNT'
	else set @ret = 'ALL_ITEMS_HAVE_ACCOUNTS'
return @ret
END

