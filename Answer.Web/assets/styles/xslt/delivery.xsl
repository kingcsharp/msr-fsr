<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!--##################################################
    ##  PURCHASE_SERVICE_REPORT                     ##
	################################################## -->
<xsl:template match="PURCHASE_SERVICE_REPORT">
<xsl:variable name="t" select="." />

<table width="775px" style="margin: 25px;"><tr><td style="height: 500px; border-bottom: 1px dashed black;">

<!-- Top Half -->
<!-- Header Section -->
<xsl:for-each select="purchaseItems/record">
	<div>
		<xsl:if test="position()=1">
			<xsl:variable name="pItem" select="." />
			<xsl:call-template name="PRINT_PURCHASE_ITEM_HEADER">
				<xsl:with-param name="pItem" select="$pItem" />
				<xsl:with-param name="t" select="$t" />
			</xsl:call-template>

			<xsl:call-template name="PURCH_ITEM_DETAIL_HEADER">
				<xsl:with-param name="t" select="$t" />
				<xsl:with-param name="pItem" select="$pItem" />
				<xsl:with-param name="p" select="." />	
			</xsl:call-template>

		</xsl:if>
	</div>
</xsl:for-each>

<!-- Detail Section -->


<xsl:for-each select="purchaseItems/record">
	<div>
		<xsl:variable name="pItem" select="." />
		<xsl:call-template name="PRINT_PURCHASE_ITEM_DETAIL">
			<xsl:with-param name="pItem" select="$pItem" />
			<xsl:with-param name="t" select="$t" />
		</xsl:call-template>
	</div>
</xsl:for-each>
</td></tr>
<tr><td>


<xsl:call-template name="PRINT_SIGNATURE_LINE">
</xsl:call-template>

</td></tr>
</table>
</xsl:template>

<!--##################################################
    ##  PRINT_PURCHASE_ITEM_HEADER                    ##
	################################################## -->
<xsl:template name="PRINT_PURCHASE_ITEM_HEADER">
<xsl:param name="pItem"/>
<xsl:param name="t"/>
<xsl:call-template name="PURCH_ITEM_HEADER">
	<xsl:with-param name="t" select="$t" />
	<xsl:with-param name="pItem" select="$pItem" />
	<xsl:with-param name="p" select="." />	
</xsl:call-template>

</xsl:template>

<!--##################################################
    ##  PRINT_PURCHASE_ITEM_DETAIL                    ##
	################################################## -->
<xsl:template name="PRINT_PURCHASE_ITEM_DETAIL">
<xsl:param name="pItem"/>
<xsl:param name="t"/>
<xsl:call-template name="PURCH_ITEM_DETAIL">
	<xsl:with-param name="t" select="$t" />
	<xsl:with-param name="pItem" select="$pItem" />
	<xsl:with-param name="p" select="." />	
</xsl:call-template>

</xsl:template>

<!--##################################################
    ##  putHeaderVal                                ##
	################################################## -->
<xsl:template name="putHeaderVal">
<xsl:param name="n"/>
<xsl:param name="v"/>
<xsl:if test="string-length($v) &gt; 0">
<td class="border">
<div style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$n"/></xsl:with-param></xsl:call-template></div>
<div style="text-align:center"><xsl:value-of select="$v"/></div>
</td>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  PURCH_ITEM_HEADER                                  ##
	################################################## -->
<xsl:template name="PURCH_ITEM_HEADER">
<xsl:param name="t" />
<xsl:param name="pItem" />
<xsl:param name="p" />

<!--This is the supplier and customer data:-->
<table style="width:100%;">
	<tr>
		<td>
			<xsl:call-template name="printCompanyTable">
				<xsl:with-param name="c" select="$pItem/supplier" />
				<xsl:with-param name="n">Ship From</xsl:with-param>
			</xsl:call-template>
		</td>
		<td style="padding-left:5px;">
			<xsl:call-template name="printCompanyTable">
				<xsl:with-param name="c" select="$pItem/customer" />
				<xsl:with-param name="n">Ship to:</xsl:with-param>
			</xsl:call-template>
		</td>
		<td style="padding-left:5px;">
			<xsl:call-template name="printShippingInfo">
				<xsl:with-param name="p" select="$p"/>
				<xsl:with-param name="t" select="$t"/>
				<xsl:with-param name="pItem" select="$pItem"/>
			</xsl:call-template>
		</td>
	</tr>
</table>

</xsl:template>

<!--##################################################
    ##  PURCH_ITEM_DETAIL_HEADER                        ##
	################################################## -->
<xsl:template name="PURCH_ITEM_DETAIL_HEADER">
<xsl:param name="t" />
<xsl:param name="pItem" />
<xsl:param name="p" />

<table class="standard" style="width:765px;">
	<tr>
		<th class="border" style="width:15%;"><xsl:call-template name="putText"><xsl:with-param name="key">Customer P/N</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:35%;"><xsl:call-template name="putText"><xsl:with-param name="key">Description</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:15%;"><xsl:call-template name="putText"><xsl:with-param name="key">Serial #</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:35%;"><xsl:call-template name="putText"><xsl:with-param name="key">Notes</xsl:with-param></xsl:call-template></th>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  PURCH_ITEM_DETAIL                               ##
	################################################## -->
<xsl:template name="PURCH_ITEM_DETAIL">
<xsl:param name="t" />
<xsl:param name="pItem" />
<xsl:param name="p" />

<!-- <table class="tight" style="width:100%">
	<tr>
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Purchase Num</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$pItem/PURCHASE_ID"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Purchase Item Num</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$pItem/PURCH_ITEM_ID"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Blanket PO Num</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$pItem/../accountInfo/record/REFERENCE_PO"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Cust Line Item</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$pItem/CUST_LINE_ITEM"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Phys Part ID</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$p/FILL_OBJ_ID"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Serial</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$p/SERIAL"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Qty</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$p/PURCHASE_QTY"/></xsl:with-param>
		</xsl:call-template>	
		<xsl:call-template name="putHeaderVal">
			<xsl:with-param name="n">Acct Num</xsl:with-param>
			<xsl:with-param name="v"><xsl:value-of select="$pItem/../accountInfo/record/ID"/></xsl:with-param>
		</xsl:call-template>	
	</tr>
</table>
<br /> -->

<table class="standard" style="width:765px;">
	<tr>
		<td class="border" style="width:15%; border-top: 0px solid black;"><xsl:value-of select="$pItem/COMPANY_PART_NUMBER"/></td>
		<td class="border" style="width:35%; border-top: 0px solid black;"><xsl:value-of select="$p/PROD_NAME"/><xsl:text> - </xsl:text><xsl:value-of select="$pItem/PROC_NAME"/></td>
		<td class="border" style="width:15%; border-top: 0px solid black;"><xsl:value-of select="$pItem/SERIAL"/></td>
		<td class="border" style="width:35%; border-top: 0px solid black;"><xsl:value-of select="$pItem/COMMENTS"/>
			<xsl:if test="$pItem/DONT_BILL = '1'">
				<xsl:call-template name="putText"><xsl:with-param name="key">NOT BILLED</xsl:with-param></xsl:call-template>
			</xsl:if>
		</td>
	</tr>
</table>

</xsl:template>

<!--##################################################
    ##  printShippingInfo                           ##
	################################################## -->
<xsl:template name="printShippingInfo">
<xsl:param name="p"/>						
<xsl:param name="pItem"/>						
<xsl:param name="t"/>						

<table>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">ShipDate</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/MY_SHIP_DATE"/></td>
	</tr>
<!--	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">ShipVia</xsl:with-param></xsl:call-template></td>
		<td class="label"></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">Shipper#</xsl:with-param></xsl:call-template></td>
		<td class="label"></td>
	</tr>-->
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">MTREF#</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/MT_NUM"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">PO#</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$t/purchase/CUST_PURCH_NUM"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">Order Qty:</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$p/PURCHASE_QTY"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">Purchase #:</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/PURCHASE_ID"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">Purchase Item #:</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/PURCH_ITEM_ID"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">Blanket PO Num:</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/../accountInfo/record/REFERENCE_PO"/></td>
	</tr>
</table>



</xsl:template>



<!--##################################################
    ##  printCompanyTable                           ##
	################################################## -->
<xsl:template name="printCompanyTable">
<xsl:param name="c"/>						
<xsl:param name="n"/>						
			<table>
			<xsl:if test="$c/logo/record/LINKED_DOC_ID">
				<tr><td>
				<img border="0" width="103" height="36">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$c/logo/record/LINKED_DOC_ID"/>&amp;width=103&amp;height=36</xsl:attribute>
				</img>

				</td></tr>
			</xsl:if>
				<tr><td class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$n"/></xsl:with-param></xsl:call-template></td></tr>
				<tr><td><xsl:value-of select="$c/NAME"/></td></tr>
			<xsl:if test="string-length($c/LOCATION_NAME) &gt; 0">
				<tr><td><xsl:value-of select="$c/LOCATION_NAME"/></td></tr>
				<tr><td><xsl:value-of select="$c/phone/PHONE"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/ADDRESS_1"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/CITY"/>,
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/STATE"/>
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/POSTAL_CODE"/>
				</td></tr>
				<tr><td><xsl:value-of select="$c/location/record/COUNTRY"/></td></tr>
			</xsl:if>
			</table>



</xsl:template>


<!--##################################################
    ##  signatureLine                                           ##
	################################################## -->
<xsl:template name="signatureLine">
<xsl:param name="t" />
<br />
<br />
<table style="" width="100%">
	<tr>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><nobr><xsl:value-of select="$t/TASK_PURCHASE_DATA/record/field[@name='CUSTOMER_NAME']/@value"/></nobr></td>
		<td valign="bottom" width="60%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
	</tr>
	<tr>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Company</xsl:with-param></xsl:call-template></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Signature</xsl:with-param></xsl:call-template></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">date</xsl:with-param></xsl:call-template></td>
	</tr>
	<tr>
		<td>
		<br />
		<br />
		</td>
	</tr>
	<tr>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><nobr><xsl:value-of select="$t/TASK_PURCHASE_DATA/record/field[@name='SUPPLIER_NAME']/@value"/></nobr></td>
		<td valign="bottom" width="60%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
	</tr>
	<tr>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Company</xsl:with-param></xsl:call-template><br /><br /></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Signature</xsl:with-param></xsl:call-template><br /><br /></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">date</xsl:with-param></xsl:call-template><br /><br /></td>
	</tr>
	
</table>

</xsl:template>

<!--##################################################
    ##  printPurchaseItem                           ##
	################################################## -->
<xsl:template name="printPurchaseItem">
<xsl:param name="level" />
<xsl:variable name="purchItemID"><xsl:value-of select="field[@name='PURCH_ITEM_ID']/@value"/></xsl:variable>
<xsl:variable name="prodHistID"><xsl:value-of select="field[@name='PRODUCT_HIST_ID']/@value"/></xsl:variable>

<div>
	<xsl:attribute name="style">margin-left:<xsl:value-of select="$level * 10"/>px;margin-right:5px;</xsl:attribute>
	<div class="subTitle"><xsl:value-of select="$purchItemID"/></div>
	<div class="barcode"><xsl:value-of select="$purchItemID"/></div>
	<xsl:if test="../../fillData/record/field[@name='PURCH_ITEM_ID' and @value=$purchItemID]">
		<xsl:call-template name="printPurchItemProdInfo">
			<xsl:with-param name="fill" select="../../fillData/record[field[@name='PURCH_ITEM_ID' and @value=$purchItemID]]" />
			<xsl:with-param name="level" select="$level" />
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="string-length(field[@name='ACCOUNT_ID']/@value) != 0">
		<xsl:value-of select="field[@name='EX_DESC']/@value"/><xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">Account:</xsl:with-param></xsl:call-template>
		<xsl:value-of select="field[@name='ACCOUNT_ID']/@value"/><xsl:text> </xsl:text>
		<xsl:value-of select="field[@name='NAME']/@value"/><xsl:text> </xsl:text>
	</xsl:if>


	<xsl:if test="../../fillData/record/field[@name='PURCH_ITEM_ID' and @value=$purchItemID] and $level = 0">
		<div>
			<div class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key">Fill Data:</xsl:with-param></xsl:call-template></div>
			<xsl:if test="string-length(../../fillData/record[field[@name='PURCH_ITEM_ID' and @value=$purchItemID]]/field[@name='COMPANY_PART_NUMBER']/@value)!=0">
				<table class="standard sm">
					<tr>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Number</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Serial Kit Number</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Qty</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Part Description</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Date Filled</xsl:with-param></xsl:call-template>
							</span>
						</th>
					</tr>
					<xsl:for-each select="../../fillData/record[field[@name='PURCH_ITEM_ID' and @value=$purchItemID]]">
						<xsl:sort select="field[@name='SERIAL']/@value" />
						<xsl:sort select="field[@name='NICK_NAME']/@value" />
						<xsl:sort select="field[@name='COMPANY_PART_NUMBER']/@value" />
						<xsl:call-template name="printFillData" >
						</xsl:call-template>
					</xsl:for-each>
				</table>
			</xsl:if>
		</div><br />
	</xsl:if>
	<xsl:if test="/Doc_Webpage/queryString/item[@name = 'SHOW_SHIPPING' and @value='YES']">
	<xsl:for-each select="../record[field[@name='PARENT' and @value=$purchItemID] and field[@name='DEST' and @value='to']]">
		<xsl:call-template name="printPurchaseItem" >
			<xsl:with-param name="level" select="$level + 1" />
		</xsl:call-template>
		<br />
	</xsl:for-each>
	</xsl:if>
	<xsl:if test="../../fillData/record/field[@name='PURCH_ITEM_ID' and @value=$purchItemID]">
		<div>
			<xsl:attribute name="style">padding-left:<xsl:value-of select="($level+1)*10"/>px;</xsl:attribute>
			<div class="subtitle"><xsl:call-template name="putText"><xsl:with-param name="key">Procedure:</xsl:with-param></xsl:call-template></div>
			<xsl:for-each select="../../PROCEDURES/ONE_PROCEDURE[@prodHistID=$prodHistID]">
				<xsl:apply-templates />
			</xsl:for-each>
		</div>
	</xsl:if>

	<xsl:if test="../../PURCHASE_TASK_DATA/record[field[@name='PURCHASE_ITEM_ID' and @value=$purchItemID]]">
		<div>
			<xsl:attribute name="style">padding-left:<xsl:value-of select="($level+1)*10"/>px;</xsl:attribute>
			<div class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key">Actual Tasks:</xsl:with-param></xsl:call-template></div>
			<table class="standard" style="font-size: x-small;">
				<tr>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Task ID</xsl:with-param></xsl:call-template></th>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Description</xsl:with-param></xsl:call-template></th>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Due Date</xsl:with-param></xsl:call-template></th>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Actual Stop Date</xsl:with-param></xsl:call-template></th>

				</tr>
				<xsl:for-each select = "../../PURCHASE_TASK_DATA/record[field[@name='PURCHASE_ITEM_ID' and @value=$purchItemID]]">
					<tr>
						<td class="standard sm" style="font-size: x-small;"><xsl:value-of select="field[@name='ID']/@value"/></td>
						<td class="standard sm" style="font-size: x-small;"><span style="font-size: x-small;"><xsl:value-of select="field[@name='DESCRIPTION']/@value"/></span></td>
						
						<td class="standard sm" style="font-size: x-small;"><xsl:value-of select="field[@name='CUR_PLANNED_STOP_DATE']/@answerDate"/></td>
						<td class="standard sm" style="font-size: x-small;"><xsl:value-of select="field[@name='ACTUAL_STOP_DATE']/@answerDate"/></td>
					</tr>
				</xsl:for-each>
			</table>
		</div>
	</xsl:if>
	<br />

	<xsl:if test="/Doc_Webpage/queryString/item[@name = 'SHOW_SHIPPING' and @value='YES']">
	<xsl:for-each select="../record[field[@name='PARENT' and @value=$purchItemID] and field[@name='DEST' and @value='from']]">
		<xsl:call-template name="printPurchaseItem" >
			<xsl:with-param name="level" select="$level + 1" />
		</xsl:call-template>
		<br />
	</xsl:for-each>
	</xsl:if>
</div>
</xsl:template>

<!--##################################################
    ##  printFillData                               ##
	################################################## -->
<xsl:template name="printFillData">
<tr>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='COMPANY_PART_NUMBER']/@value"/></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='SERIAL']/@value"/><span class="barCode"><xsl:value-of select="field[@name='SERIAL']/@value"/></span></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='QTY']/@value"/></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='PART_DESC']/@value"/></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='FILL_DATE']/@answerDate"/></span></td>
</tr>

</xsl:template>


<!--##################################################
    ##  printPurchItemProdInfo                      ##
	################################################## -->
<xsl:template name="printPurchItemProdInfo">
<xsl:param name="fill"/>
<xsl:param name="level"/>
<span>
	<xsl:if test="$level = 0"><xsl:attribute name="style">font-weight:bold;font-size:large;</xsl:attribute></xsl:if>
	<xsl:value-of select="$fill/field[@name='PROD_NAME']/@value"/><xsl:text> </xsl:text>
	<xsl:call-template name="putText"><xsl:with-param name="key">QTY</xsl:with-param></xsl:call-template>
	<xsl:text> </xsl:text>
	<xsl:value-of select="$fill/field[@name='TOT_QTY']/@value"/>
	<xsl:text> </xsl:text>
	

</span>
</xsl:template>

<!--##################################################
    ##  printTaskRow                                ##
	################################################## -->
<xsl:template name="printTaskRow">
<xsl:variable name="myID"><xsl:value-of select="field[@name='ID']/@value"/></xsl:variable>
<table style="border:1px solid black;width:100%;">
	<tr>
		<td>
			<xsl:attribute name="style">padding-left:<xsl:value-of select="(number(field[@name = 'TREE_LEVEL']/@value) * 15)"/>px;</xsl:attribute>
			<i><xsl:value-of select="field[@name='ACTUAL_START_DATE']/@answerDate"/></i><xsl:text> - </xsl:text>
			<b><xsl:value-of select="field[@name='DESCRIPTION']/@value"/></b>
			<table style="border-collapse:collapse">
				<tr><td></td></tr>
			<xsl:call-template name="printTimeInfo" />
			<xsl:call-template name="printMonitors" />
			</table>
		</td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  printTimeInfo                               ##
	################################################## -->
<xsl:template name="printTimeInfo">
<xsl:variable name="myID"><xsl:value-of select="field[@name='ID']/@value"/></xsl:variable>
<xsl:if test="../../minutes/record[field[@name='TASK_ID']/@value =$myID]">
	<tr>
		<td style="">
			<i><xsl:call-template name="putText"><xsl:with-param name="key">Recorded Time on Task:</xsl:with-param></xsl:call-template>:</i>
		</td>
		<td style="font-weight:600;">
			<xsl:for-each select="../../minutes/record[field[@name='TASK_ID']/@value =$myID]">
				<xsl:call-template name="printOnePersonsTime" />
			</xsl:for-each>
		</td>
	</tr>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  printOnePersonsTime                         ##
	################################################## -->
<xsl:template name="printOnePersonsTime">
<xsl:value-of select="field[@name='WORKER_NAME']/@value"/>:
<xsl:if test="number(field[@name='HRS']/@value) &gt; 0">
	<xsl:value-of select="field[@name='HRS']/@value"/><xsl:text> </xsl:text><xsl:call-template name="putText"><xsl:with-param name="key">hr</xsl:with-param></xsl:call-template> <xsl:text> </xsl:text>
</xsl:if>
<xsl:if test="number(field[@name='MY_MIN']/@value) &gt; 0">
	<xsl:value-of select="field[@name='MY_MIN']/@value"/><xsl:text> </xsl:text><xsl:call-template name="putText"><xsl:with-param name="key">min</xsl:with-param></xsl:call-template> <xsl:text> </xsl:text>
</xsl:if>
<xsl:if test="position() != last()">
	<br />
</xsl:if>
</xsl:template>



<!--##################################################
    ##   printMonitors                              ##
	################################################## -->
<xsl:template name="printMonitors">
<xsl:variable name="myID"><xsl:value-of select="field[@name='ID']/@value"/></xsl:variable>
<xsl:if test="../../monitors/record[field[@name='TASK_ID']/@value =$myID]">
	<tr>
		<td style="">
			<i><xsl:call-template name="putText"><xsl:with-param name="key">Tests:</xsl:with-param></xsl:call-template></i>
		</td>
		<td style="font-weight:600;">
			<xsl:for-each select="../../monitors/record[field[@name='TASK_ID']/@value =$myID]">
				<xsl:call-template name="printMonitor" />
			</xsl:for-each>
		</td>
	</tr>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  printMonitor                                ##
	################################################## -->
<xsl:template name="printMonitor">
	<xsl:value-of select="field[@name='DESCRIPTION']/@value"/><xsl:text> </xsl:text>
	<xsl:call-template name="putText"><xsl:with-param name="key">Result:</xsl:with-param></xsl:call-template>
	<xsl:text> </xsl:text>
	<xsl:choose>
		<xsl:when test="field[@name='MONITOR_TYPE']/@value = 'YES_NO'">
			<xsl:call-template name="putText"><xsl:with-param name="key">YES_NO_<xsl:value-of select="field[@name='MY_ANSWER']/@value"/></xsl:with-param></xsl:call-template>
		</xsl:when>
	
		<xsl:otherwise>
			<xsl:value-of select="field[@name='MY_ANSWER']/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ##  printProcedureHeader                        ##
	################################################## -->
<xsl:template name="printProcedureHeader">
<xsl:param name="procID" />
<xsl:for-each select="/Doc_Webpage/content/default_body/*/PROCEDURES/ONE_PROCEDURE/ANSWER_PROCEDURE">
<table>
	<tr>
		<td>
			<xsl:value-of select="headerData/record/field[@name='NAME']/@value"/>
		</td>
		<td style="padding-left:5px;padding-right:5px;">
			<xsl:call-template name="putText"><xsl:with-param name="key">Revision:</xsl:with-param></xsl:call-template>
		</td>
		<td>
			<xsl:value-of select="headerData/record/field[@name='REV']/@value"/>
		</td>
	</tr>
</table>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  PRINT_SIGNATURE_LINE                            ##
	################################################## -->
<xsl:template name="PRINT_SIGNATURE_LINE">
<xsl:param name="pItem"/>
<xsl:param name="t"/>
<br />
<br />
<table style="width:100%">
	<tr>
		<td width="10%">
			<nobr>
			<xsl:call-template name="putText"><xsl:with-param name="key">Recipient Signature</xsl:with-param></xsl:call-template>
			</nobr>
		</td>
		<td style="border-bottom:thin solid black; width:60%">
		<xsl:text>: </xsl:text>
		
		</td>
		<td width="5%">
			<nobr>
			<xsl:call-template name="putText"><xsl:with-param name="key">Date</xsl:with-param></xsl:call-template>
			</nobr>
		</td>
		<td style="border-bottom:thin solid black; width:25%">
		<xsl:text>: </xsl:text>
		
		</td>
		
	</tr>
</table>
</xsl:template>

</xsl:stylesheet>
