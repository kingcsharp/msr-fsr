<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!--##################################################
    ##  MASS_DELIVERY_TICKETS                     ##
	################################################## -->
<xsl:template match="MASS_DELIVERY_TICKETS">
<xsl:variable name="t" select="." />
<xsl:for-each select="record[not (FROM_LOC_ID=preceding-sibling::record/FROM_LOC_ID)]">
	<xsl:variable name="fromLoc"><xsl:value-of select="FROM_LOC_ID"/></xsl:variable>
	<xsl:for-each select="$t/record[FROM_LOC_ID = $fromLoc and (not (TO_LOC_ID=preceding-sibling::record/TO_LOC_ID))]">
		<xsl:variable name="toLoc"><xsl:value-of select="TO_LOC_ID"/></xsl:variable>
		<div>
		<xsl:if test="position()!=last()">
			<xsl:attribute name="style">page-break-after:always</xsl:attribute>
		</xsl:if>
		<xsl:for-each select="$t/record[FROM_LOC_ID = $fromLoc and TO_LOC_ID = $toLoc and (not (CUST_PURCH_NUM=preceding-sibling::record/CUST_PURCH_NUM))]">
			<xsl:variable name="po"><xsl:value-of select="CUST_PURCH_NUM"/></xsl:variable>
			<xsl:call-template name="printHeader">
				<xsl:with-param name="pItem" select="."></xsl:with-param>
            </xsl:call-template>
			<xsl:call-template name="printBody">
				<xsl:with-param name="fromLoc"><xsl:value-of select="$fromLoc"/></xsl:with-param>
				<xsl:with-param name="toLoc"><xsl:value-of select="$toLoc"/></xsl:with-param>
				<xsl:with-param name="po"><xsl:value-of select="$po"/></xsl:with-param>
				<xsl:with-param name="t" select="$t" />
            </xsl:call-template>
			<br />
			<br />
			<table style="width:100%">
				<tr>
					<td width="10%">
						<nobr>
						<xsl:call-template name="putText"><xsl:with-param name="key">Recipient Signiture</xsl:with-param></xsl:call-template>
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
		</xsl:for-each>
			</div>
	</xsl:for-each>
</xsl:for-each>
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
<xsl:if test="string-length($v) &gt; 0">
	<div style="text-align:center"><span class="barcode"><nobr>*<xsl:value-of select="$v"/>*</nobr></span></div>
</xsl:if>
</td>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  PURCH_ITEM_HEADER                                  ##
	################################################## -->
<xsl:template name="printHeader">
<xsl:param name="pItem" />
<table class="tight" style="width:100%">
	<tr>
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Purchase Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/PURCHASE_ID"/></xsl:with-param>
		    </xsl:call-template>	
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Single PO Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/CUST_PURCH_NUM"/></xsl:with-param>
		    </xsl:call-template>	
<!--			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Blanket PO Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/../accountInfo/record/REFERENCE_PO"/></xsl:with-param>
		    </xsl:call-template>	-->
	</tr>
</table>
<!--This is the supplier and customer data:-->
<table style="width:100%">
	<tr>
		<td>
			<xsl:call-template name="printSupplierTable">
				<xsl:with-param name="c" select="$pItem" />
				<xsl:with-param name="n">Ship From</xsl:with-param>
			</xsl:call-template>
		</td>
		<td style="padding-left:5px;">
			<xsl:call-template name="printCustomerTable">
				<xsl:with-param name="c" select="$pItem" />
				<xsl:with-param name="n">Ship to:</xsl:with-param>
			</xsl:call-template>
		</td>
		<td style="padding-left:5px;">
			<xsl:call-template name="printShippingInfo">
				<xsl:with-param name="pItem" select="$pItem"/>
			</xsl:call-template>
		</td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  printBody                                   ##
	################################################## -->
<xsl:template name="printBody">
<xsl:param name="fromLoc"/>
<xsl:param name="toLoc"/>
<xsl:param name="po"/>
<xsl:param name="t"/>

<table class="standard" style="width:100%">
	<tr>
		<th class="border" style="width:10%"><xsl:call-template name="putText"><xsl:with-param name="key">Line</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:10%"><xsl:call-template name="putText"><xsl:with-param name="key">Qty</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:35%"><xsl:call-template name="putText"><xsl:with-param name="key">Customer P/N</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:35%"><xsl:call-template name="putText"><xsl:with-param name="key">Description</xsl:with-param></xsl:call-template></th>
		<th class="border" style="width:10%"><xsl:call-template name="putText"><xsl:with-param name="key">Notes</xsl:with-param></xsl:call-template></th>
	</tr>
<xsl:for-each select="$t/record[FROM_LOC_ID = $fromLoc and TO_LOC_ID = $toLoc and CUST_PURCH_NUM=$po]">
	<tr>
		<td class="border"><xsl:value-of select="CUST_LINE_ITEM"/></td>
		<td class="border"><xsl:value-of select="QTY"/></td>
		<td class="border"><xsl:value-of select="COMPANY_PART_NUMBER"/></td>
		<td class="border"><xsl:value-of select="PRODUCT_NAME"/><xsl:text> - </xsl:text><xsl:value-of select="PROCEDURE_NAME"/></td>
		<td class="border"><xsl:value-of select="NOTES"/></td>
	</tr>
</xsl:for-each>
</table>


</xsl:template>


<!--##################################################
    ##  printShippingInfo                           ##
	################################################## -->
<xsl:template name="printShippingInfo">
<xsl:param name="pItem"/>						
<table>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">ShipDate</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/../date"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">MTREF#</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/MT_NUM"/></td>
	</tr>
	<tr>
		<td class="label"><xsl:call-template name="putText"><xsl:with-param name="key">PO#</xsl:with-param></xsl:call-template></td>
		<td class="label"><xsl:value-of select="$pItem/CUST_PURCH_NUM"/></td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  printCompanyTable                           ##
	################################################## -->
<xsl:template name="printSupplierTable">
<xsl:param name="c"/>						
<xsl:param name="n"/>						
			<table>
			<xsl:choose>
				<xsl:when test="string-length($c/SUP_LOGO) &gt; 0">
				<tr><td>
					<img border="0"  width="103" height="36">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$c/SUP_LOGO"/>&amp;width=103&amp;height=36</xsl:attribute>
					</img>
				</td></tr>
				</xsl:when>
				<xsl:when test="string-length($c/SUP_ROOT_LOGO) &gt; 0">
				<tr><td>
					<img border="0" width="103" height="36">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$c/SUP_ROOT_LOGO"/>&amp;width=103&amp;height=36</xsl:attribute>
					</img>
				</td></tr>
				</xsl:when>
			</xsl:choose>
				<tr><td class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$n"/></xsl:with-param></xsl:call-template></td></tr>
				<tr><td><xsl:value-of select="$c/SUP_NAME"/></td></tr>
				<tr><td><xsl:value-of select="$c/FROM_NAME"/></td></tr>
				<tr><td><xsl:value-of select="$c/phone/FROM_PHONE"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/FROM_ADDRESS_1"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/FROM_ADDRESS_2"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/FROM_CITY"/>,
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/FROM_STATE"/>
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/FROM_POSTAL_CODE"/>
				</td></tr>
				<tr><td><xsl:value-of select="$c/location/record/FROM_COUNTRY"/></td></tr>
			</table>
</xsl:template>

<!--##################################################
    ##  printCompanyTable                           ##
	################################################## -->
<xsl:template name="printCustomerTable">
<xsl:param name="c"/>						
<xsl:param name="n"/>						
			<table>
			<xsl:choose>
				<xsl:when test="string-length($c/CUST_LOGO) &gt; 0">
				<tr><td>
					<img border="0" width="103" height="36">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$c/SUP_LOGO"/>&amp;width=103&amp;height=36</xsl:attribute>
					</img>
				</td></tr>
				</xsl:when>
				<xsl:when test="string-length($c/CUST_ROOT_LOGO) &gt; 0">
				<tr><td>
					<img border="0" width="103" height="36">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$c/SUP_ROOT_LOGO"/>&amp;width=103&amp;height=36</xsl:attribute>
					</img>
				</td></tr>
				</xsl:when>
			</xsl:choose>
				<tr><td class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$n"/></xsl:with-param></xsl:call-template></td></tr>
				<tr><td><xsl:value-of select="$c/CUST_NAME"/></td></tr>
				<tr><td><xsl:value-of select="$c/TO_NAME"/></td></tr>
				<tr><td><xsl:value-of select="$c/phone/TO_PHONE"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/TO_ADDRESS_1"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/TO_ADDRESS_2"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/TO_CITY"/>,
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/TO_STATE"/>
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/TO_POSTAL_CODE"/>
				</td></tr>
				<tr><td><xsl:value-of select="$c/location/record/TO_COUNTRY"/></td></tr>
			</table>
</xsl:template>
</xsl:stylesheet>
