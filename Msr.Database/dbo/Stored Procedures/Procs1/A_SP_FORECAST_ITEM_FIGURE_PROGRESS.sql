






CREATE       PROCEDURE A_SP_FORECAST_ITEM_FIGURE_PROGRESS
@ID varchar(50),
@strNTLogin varchar(50)
AS
declare @acct as varchar(50),
@acctProduct as varchar(50),
@funnelStep as smallInt,
@custom as smallint, @custID varchar(50)

set @funnelStep = 1

SELECT @acct = ACCOUNT_ID FROM A_FORECAST_ITEMS WHERE ID = @ID
SELECT @acctProduct = PRODUCT_ID, @custID = CUSTOMER_CO
	FROM A_V_ACCOUNTS_APPROVED_DATA	WHERE ID = @acct

SELECT * FROM A_V_ACCOUNTS_APPROVED_DATA	WHERE ID = @acct
if @acctProduct is not null
	begin
	print 'We got an account with a product'
	select @custom = CUSTOMIZABLE FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @acctProduct
	if @custom = 1
		begin
		print 'This is a custom Product'
--Enter Prod Requirement form stuff when ready		
--loop through all related prod requirement forms
	--Call a procedure for each one that will figure out what the funnel step is for that one
		--check for a product for the requirement form and call the product funnel checker

		end
	else
		begin
		print 'This is for an Actual product so automatically jump to funnel step 4'
		set @funnelStep = 4
		if @custID is null
			begin
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_ORDERS_HISTORY h,A_OBJECTS o 
			WHERE i.PRODUCT_ID = @acctProduct AND i.ORDER_ID = h.ID AND h.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED')
				set @funnelStep = 5
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_QUOTES_HISTORY h 
			WHERE i.PRODUCT_ID = @acctProduct AND i.QUOTE_ID = h.ID AND h.PROGRESS = 'SENT_TO_CUSTOMER')
				set @funnelStep = 6
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_QUOTES_HISTORY h 
			WHERE i.PRODUCT_ID = @acctProduct AND i.QUOTE_ID = h.ID AND h.PROGRESS = 'ACCEPTED_BY_CUSTOMER')
				set @funnelStep = 7
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_PURCHASES_HISTORY h,A_OBJECTS o 
			WHERE i.PRODUCT_ID = @acctProduct AND i.PURCHASE_HIST_ID = h.ID AND h.OBJECT_ID = o.ID AND o.STATUS = 'CREATING')
				set @funnelStep = 8
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_PURCHASES_HISTORY h,A_OBJECTS o 
			WHERE i.PRODUCT_ID = @acctProduct AND i.PURCHASE_HIST_ID = h.ID AND h.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED')
				set @funnelStep = 9
			end
		else
			begin
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_ORDERS_HISTORY h,A_OBJECTS o
			WHERE i.PRODUCT_ID = @acctProduct AND i.ORDER_ID = h.ID AND h.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED'
			and h.CUSTOMER_CO = @custID)
				set @funnelStep = 5
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_QUOTES_HISTORY h 
			WHERE i.PRODUCT_ID = @acctProduct AND i.QUOTE_ID = h.ID AND h.PROGRESS = 'SENT_TO_CUSTOMER'
			and h.CUSTOMER_CO = @custID)
				set @funnelStep = 6
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_QUOTES_HISTORY h 
			WHERE i.PRODUCT_ID = @acctProduct AND i.QUOTE_ID = h.ID AND h.PROGRESS = 'ACCEPTED_BY_CUSTOMER'
			and h.CUSTOMER_CO = @custID)
				set @funnelStep = 7
			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_PURCHASES_HISTORY h,A_OBJECTS o,A_V_ORDERS_APPROVED_DATA ord
			WHERE 
				i.PRODUCT_ID = @acctProduct 
				AND i.PURCHASE_HIST_ID = h.ID 
				AND h.OBJECT_ID = o.ID
				AND o.STATUS IN ('CREATING','APPROVED','IN_WORK_FLOW')
				AND ord.CUSTOMER_CO = @custID
			 	AND h.ORDER_ID = ord.ID 
				)
				set @funnelStep = 8


			if exists(SELECT i.ID FROM A_ORDER_ITEMS i,A_PURCHASES_HISTORY h,A_OBJECTS o,A_V_ORDERS_APPROVED_DATA ord
			WHERE 
				i.PRODUCT_ID = @acctProduct 
				AND i.PURCHASE_HIST_ID = h.ID 
				AND h.OBJECT_ID = o.ID
				AND o.STATUS = 'APPROVED' 
				AND ord.CUSTOMER_CO = @custID
			 	AND h.ORDER_ID = ord.ID 
				)
				set @funnelStep = 9
			end
		print 'We ended up with a funnel step of :'
		print @funnelStep
		--call a function that checks where this product is in the funnel
		end
	end
else
	begin
	print 'We got an account with no product'
--When we figure this out we will be happy

	end

UPDATE A_FORECAST_ITEMS SET PROGRESS = @funnelStep WHERE ID = @ID









