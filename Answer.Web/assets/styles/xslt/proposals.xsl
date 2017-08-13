<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ##  ANSWER_PROCEDURE                            ##
	################################################## -->
<xsl:template match="ANSWER_PROPOSAL">
<div>
	<xsl:call-template name="pheader" />
	<br />
	<xsl:call-template name="qheader" />
	<br />
	<xsl:call-template name="intro" />
	<br />
	<xsl:call-template name="priceTable" />
	<br />
	<xsl:call-template name="putText"><xsl:with-param name="key">Notes:</xsl:with-param></xsl:call-template>
	<p>
		<xsl:copy-of select="ADD_NOTES/root"/>
	</p>
	<br />
	<p>
		<xsl:copy-of select="CLOSING/root"/>
	</p>
	<br />
	<xsl:call-template name="putText"><xsl:with-param name="key">Best Regards,</xsl:with-param></xsl:call-template>
	<br />
	<br />
	<br />
	<xsl:value-of select="record/CREATOR_NAME"/><br />
	<xsl:value-of select="record/CREATOR_TITLE"/><br />
	

</div>
</xsl:template>

<!--##################################################
    ##  priceTable                                  ##
	################################################## -->
<xsl:template name="priceTable">
<table class="standard">
	<tr>
		<th  class="standard"><xsl:call-template name="putText"><xsl:with-param name="key">Product No</xsl:with-param></xsl:call-template></th>
		<th  class="standard"><xsl:call-template name="putText"><xsl:with-param name="key">Product Name</xsl:with-param></xsl:call-template></th>
		<th  class="standard"><xsl:call-template name="putText"><xsl:with-param name="key">Part No</xsl:with-param></xsl:call-template></th>
		<th class="standard"><xsl:call-template name="putText"><xsl:with-param name="key">Poc Name</xsl:with-param></xsl:call-template></th>
		<th class="standard"><xsl:call-template name="putText"><xsl:with-param name="key">Price</xsl:with-param></xsl:call-template></th>
		<th class="standard"><xsl:call-template name="putText"><xsl:with-param name="key">Process Time</xsl:with-param></xsl:call-template></th>
	</tr>
	<xsl:for-each select="QUOTE_DATA/record">
		<xsl:for-each select="record">
			<tr>
				<td class="standard"><xsl:value-of select="PRODUCT_ID"/></td>
				<td class="standard"><xsl:value-of select="PRODUCT_NAME"/></td>
				<td class="standard"><xsl:value-of select="COMPANY_PART_NUMBER"/></td>
				<td class="standard"><xsl:value-of select="PROC_NAME"/></td>
				<td class="standard"><xsl:value-of select="TOTAL_PRICE/text()"/></td>
				<td class="standard"><xsl:value-of select="PRODUCTION_TIME"/><xsl:text> </xsl:text><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="PRODUCTION_TIME_UNIT"/></xsl:with-param></xsl:call-template></td>
			</tr>
		</xsl:for-each>
	</xsl:for-each>
</table>
</xsl:template>



<!--##################################################
    ##  intro                                           ##
	################################################## -->
<xsl:template name="intro">
<table>
	<tr>
		<td><xsl:call-template name="putText"><xsl:with-param name="key">Dear</xsl:with-param></xsl:call-template></td>
		<td>
			<xsl:for-each select="CUSTOMER_CONTACTS/record">
				<xsl:value-of select="FULL_NAME"/>,<br />
			</xsl:for-each>
		</td>
	</tr>
</table>
<p>
	<xsl:copy-of select="OPENING/root"/>
</p>
</xsl:template>


<!--##################################################
    ##  qheader                                     ##
	################################################## -->
<xsl:template name="qheader">
<div style="font-size:large;font-weight:bold;color:blue;text-align:center;">
	<xsl:call-template name="putText"><xsl:with-param name="key">Quotation</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
	<xsl:value-of select="record/ROOT"/>
</div>
<table>
	<tr>
		<td><xsl:call-template name="putText"><xsl:with-param name="key">Quotation Date:</xsl:with-param></xsl:call-template></td>
		<td><xsl:value-of select="record/PROP_DATE/shortDate"/></td>
		<td><xsl:call-template name="putText"><xsl:with-param name="key">Quotation Valid till:</xsl:with-param></xsl:call-template></td>
		<td><xsl:value-of select="record/PROP_VALID_DATE/shortDate"/></td>
	</tr>
</table><br />
<table>
	<tr>
		<td><xsl:call-template name="putText"><xsl:with-param name="key">TO:</xsl:with-param></xsl:call-template></td>
		<td>
			<xsl:value-of select="CUSTOMER_DATA/record/NAME"/><br/>
			<xsl:value-of select="CUSTOMER_ADDRESS/record/ADDRESS_1"/><br />
			<xsl:if test="string-length(CUSTOMER_ADDRESS/record/ADDRESS_2) &gt; 1">
				<xsl:value-of select="CUSTOMER_ADDRESS/record/ADDRESS_2" /><br />
			</xsl:if>
			<xsl:value-of select="CUSTOMER_ADDRESS/record/CITY"/>,
			<xsl:text> </xsl:text><xsl:value-of select="CUSTOMER_ADDRESS/record/STATE"/><xsl:text> </xsl:text>
			<xsl:value-of select="CUSTOMER_ADDRESS/record/POSTAL_CODE"/><br />
			<xsl:value-of select="CUSTOMER_ADDRESS/record/COUNTRY"/><br />
		</td>
	</tr>
	<tr>
		<td><xsl:call-template name="putText"><xsl:with-param name="key">Attention:</xsl:with-param></xsl:call-template></td>
		<td>
			<xsl:for-each select="CUSTOMER_CONTACTS/record">
				<xsl:value-of select="NAME"/><br/>
			</xsl:for-each>
		</td>
	</tr>
</table>
<br />
<table>
	<tr>
		<td><xsl:call-template name="putText"><xsl:with-param name="key">Reference Files:</xsl:with-param></xsl:call-template></td>
		<td>
			<xsl:for-each select="REF_FILES/record">
				<a>
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/documents/viewdocument.asp?docID=<xsl:value-of select="VALUE"/></xsl:attribute>
					<xsl:value-of select="SHOW"/>
				</a><br/>
			</xsl:for-each>
		</td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  header                                      ##
	################################################## -->
<xsl:template name="pheader">
<xsl:variable name="s" select="SUPPLIER_ADDRESS/record" />
<table>
	<tr>
		<td rowspan="3">
			<img border="0" width="103" height="36">
				<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="SUPPLIER_LOGO/record/LOGO_ID"/>&amp;width=103&amp;height=36</xsl:attribute>
			</img>
		</td>
		<td>
			<xsl:value-of select="SUPPLIER_DATA/record/NAME"/>
		</td>
	</tr>
	<tr><td><xsl:value-of select="$s/ADDRESS_1"/></td></tr>
	<tr><td><xsl:value-of select="$s/CITY"/>, <xsl:value-of select="$s/STATE"/> <xsl:value-of select="$s/POSTAL_CODE"/></td></tr>
</table>
</xsl:template>


<!--##################################################
    ##  REFERENCE_FILES                             ##
	################################################## -->
<xsl:template match="REFERENCE_FILES">
<xsl:if test="record">
	<div class="procedureStepRefFiles">
		<xsl:call-template name="putText"><xsl:with-param name="key">Reference Files:</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
		<br />
		<xsl:for-each select="record">
			<xsl:choose>
				<xsl:when test="contains(field[@name = 'SERVER_PATH']/@value,'JPG') or contains(field[@name = 'SERVER_PATH']/@value,'GIF')">
					<img class="procedurePicture">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/>&amp;width=400</xsl:attribute>
					</img>
				</xsl:when>
				<xsl:otherwise>
					<a target="_blank" class="normal">
						<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/></xsl:attribute>
						<img border="0">
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>document.gif</xsl:attribute>
							<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template></xsl:attribute>
						</img>
						<xsl:value-of select="field[@name = 'NAME']/@value"/>
						<div style="display:none" class="hiddenForWords">
							<xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template>                    
						</div>
					</a>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
		</xsl:for-each>
	</div>
</xsl:if>
</xsl:template>


</xsl:stylesheet>