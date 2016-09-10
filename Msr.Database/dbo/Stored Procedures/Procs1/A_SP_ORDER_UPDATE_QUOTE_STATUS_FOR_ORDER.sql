
CREATE  PROCEDURE dbo.A_SP_ORDER_UPDATE_QUOTE_STATUS_FOR_ORDER 
@myOrderID varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating the order status based on all quotes Status'

declare @myOrdHistID varchar(50)

SELECT @myOrdHistID = HISTORY_REF_ID FROM A_ORDERS WHERE ID = @myOrderID

declare @quotedTester varchar(50),@newTester varchar(50),@rejectedTester varchar(50),@acceptedByCustomer varchar(50)
print 'First we will test to see if there are any quotes that have been sent '
SELECT @quotedTester = ID 
	FROM A_V_QUOTES_APPROVED_DATA 
	WHERE ORDER_ID = @myOrderID AND PROGRESS = 'SENT_TO_CUSTOMER'

print 'Next we will test to see if there are any quotes that have not been sent '
SELECT @newTester = ID 
	FROM A_V_QUOTES_APPROVED_DATA 
	WHERE ORDER_ID = @myOrderID AND PROGRESS = 'NEW'

print 'Next we will test to see if there are any quotes that have been rejected '
SELECT @rejectedTester = ID 
	FROM A_V_QUOTES_APPROVED_DATA 
	WHERE ORDER_ID = @myOrderID AND PROGRESS = 'REJECTED_BY_CUSTOMER'

print 'Next we will test to see if there are any quotes that have been Accepted by the customer '
SELECT @acceptedByCustomer = ID 
	FROM A_V_QUOTES_APPROVED_DATA 
	WHERE ORDER_ID = @myOrderID AND PROGRESS = 'ACCEPTED_BY_CUSTOMER'

if @rejectedTester is  null
	begin
		if @newTester is null
			begin
				if @quotedTester is not null
					begin
					UPDATE A_ORDERS_HISTORY SET PROGRESS = 'ALL_QUOTED_WAIT_ACCEPTANCE' WHERE ID = @myOrdHistID
					end
				else
					begin
					if @acceptedByCustomer is not null
						begin
						UPDATE A_ORDERS_HISTORY SET PROGRESS = 'ALL_QUOTES_ACCEPTED' WHERE ID = @myOrdHistID
						end
					end
			end
		else
			begin
			UPDATE A_ORDERS_HISTORY SET PROGRESS = 'WAITING_ON_AT_LEAST_ONE_QUOTE' WHERE ID = @myOrdHistID
			end
	end
else
	begin
	UPDATE A_ORDERS_HISTORY SET PROGRESS = 'ONE_QUOTE_REJECTED_BY_CUSTOMER' WHERE ID = @myOrdHistID
	end




