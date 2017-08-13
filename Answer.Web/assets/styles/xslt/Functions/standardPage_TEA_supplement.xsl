<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!--##################################################
    ## tea_discussion_hierarchy                         ##
	################################################## -->
<xsl:template match="tea_discussion_hierarchy">
	<xsl:apply-templates/>
</xsl:template>

<!--##################################################
    ## tea_discussion_parent                            ##
	################################################## -->
<xsl:template match="tea_discussion_parent">
	<xsl:apply-templates/>
	<xsl:if test="tea_discussion_parent">/</xsl:if>
	<xsl:choose>
		<xsl:when test="viewable">
			<a>
				<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=showDiscussion&amp;ID=<xsl:value-of select="@ID"/></xsl:attribute>
				<xsl:value-of select="@ID"/>
			</a>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="@ID"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ## tea_product_parent                         ##
	################################################## -->
<xsl:template match="tea_product_hierarchy">
	<xsl:apply-templates/>
</xsl:template>

<!--##################################################
    ## tea_product_parent                            ##
	################################################## -->
<xsl:template match="tea_product_parent">
	<xsl:apply-templates/>
	<xsl:if test="tea_product_parent">/</xsl:if>
	<a>
		<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=editProduct&amp;ID=<xsl:value-of select="@ID"/></xsl:attribute>
		<xsl:value-of select="@ID"/>
	</a>	
</xsl:template>


<!--##################################################
    ## tea_survey_hierarchy                         ##
	################################################## -->
<xsl:template match="tea_survey_hierarchy">
	<xsl:apply-templates/>
</xsl:template>

<!--##################################################
    ## tea_survey_parent                            ##
	################################################## -->
<xsl:template match="tea_survey_parent">
	<xsl:apply-templates/>
	<xsl:if test="tea_survey_parent">/</xsl:if>
	<xsl:choose>
		<xsl:when test="viewable">
			<a>
				<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=viewSurvey&amp;ID=<xsl:value-of select="@ID"/></xsl:attribute>
				<xsl:value-of select="@ID"/>
			</a>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="@ID"/>
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>


<!--##################################################
    ## procedure_step_roles_Assigned                                             ##
	################################################## -->
<xsl:template match="procedure_step_roles_Assigned">
	<xsl:for-each select="record">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>
				asp/standardPage.asp?pageID=viewRoleChildren&amp;ID=<xsl:value-of select="field[@name = 'ROLE_ASSIGNED']/@value"/>
			</xsl:attribute>
			<xsl:value-of select="field[@name = 'NAME']/@value"/>
			<xsl:if test="position() != last()">
				,<xsl:text> </xsl:text>
			</xsl:if>
		</a>

	</xsl:for-each>
</xsl:template>



<!--##################################################
    ## TEA_ShippingInformation                 		##
	################################################## -->
<xsl:template match="TEA_ShippingInformation">
<form name="TEA_Shipping">
<table class="tight">
	<tr>
		<td colspan="2">

			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">Please Ship this To</xsl:with-param>
				<xsl:with-param name="name">TopLine</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>



		</td>
	</tr>
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">shipToName:</xsl:with-param>
				<xsl:with-param name="name">shipToName</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'TO_2']/@value"/></xsl:with-param>
				<xsl:with-param name="name">TO_2</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">CompanyName:</xsl:with-param>
				<xsl:with-param name="name">CompanyName</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'TO_1']/@value"/></xsl:with-param>
				<xsl:with-param name="name">TO_1</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">StreetAddress</xsl:with-param>
				<xsl:with-param name="name">StreetAddress</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'ADD1']/@value"/>
<xsl:value-of select="record/field[@name = 'ADD2']/@value"/></xsl:with-param>
				<xsl:with-param name="name">STREET</xsl:with-param>
				<xsl:with-param name="rows" select="2" />
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">CityStateZip:</xsl:with-param>
				<xsl:with-param name="name">City</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'CITY']/@value"/>, <xsl:value-of select="record/field[@name = 'STATE']/@value"/> . <xsl:value-of select="record/field[@name = 'ZIP']/@value"/></xsl:with-param>
				<xsl:with-param name="name">CityStateZip</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">Telephone</xsl:with-param>
				<xsl:with-param name="name">TELE</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'PHONE']/@value"/><xsl:value-of select="record/field[@name = 'EXT']/@value"/></xsl:with-param>
				<xsl:with-param name="name">PHONE</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">ShipBy</xsl:with-param>
				<xsl:with-param name="name">ShipBy</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal" colspan="1">
			<select name="shippingType">
				<option>
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">PriorityOverNight</xsl:with-param></xsl:call-template></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">PriorityOverNight</xsl:with-param></xsl:call-template>
				</option>
				<option>
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">StandardOverNight</xsl:with-param></xsl:call-template></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">StandardOverNight</xsl:with-param></xsl:call-template>
				</option>
				<option>
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">TwoDay</xsl:with-param></xsl:call-template></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">TwoDay</xsl:with-param></xsl:call-template>
				</option>
				<option>
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">International</xsl:with-param></xsl:call-template></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">International</xsl:with-param></xsl:call-template>
				</option>
				<option>
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Surface</xsl:with-param></xsl:call-template></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">Surface</xsl:with-param></xsl:call-template>
				</option>
			</select>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">from</xsl:with-param>
				<xsl:with-param name="name">from</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'FromName']/@value"/></xsl:with-param>
				<xsl:with-param name="name">FromName</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">date</xsl:with-param>
				<xsl:with-param name="name">date</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"><xsl:value-of select="record/field[@name = 'curDate']/@value"/></xsl:with-param>
				<xsl:with-param name="name">curDate</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value">Dept</xsl:with-param>
				<xsl:with-param name="name">Dept</xsl:with-param>
				<xsl:with-param name="usePutText">1</xsl:with-param>
			</xsl:call-template>
		</td>
		<td class="normal">
			<xsl:call-template name="TEA_textArea">
				<xsl:with-param name="value"></xsl:with-param>
				<xsl:with-param name="name">myDepartMent</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td colspan="2" style="text-align:center">
			<input type="button" onClick="printShippingLabel(document.TEA_Shipping)">
				<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">ShowShippingLabel</xsl:with-param></xsl:call-template></xsl:attribute>
			</input>	
			<div class="hidderForWords">
            	<xsl:call-template name="putText"><xsl:with-param name="key">ShowShippingLabel</xsl:with-param></xsl:call-template>
            </div>
		</td>
	</tr>
</table>
</form>



</xsl:template>

<!--##################################################
    ## TEA_textArea                                 ##
	################################################## -->
<xsl:template name="TEA_textArea">
<xsl:param name="usePutText"/>
<xsl:param name="value"/>
<xsl:param name="name"/>
<xsl:param name="cols"/>
<xsl:param name="rows"/>

<xsl:choose>
	<xsl:when test="$usePutText">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$value"/></xsl:with-param></xsl:call-template>
		<input type="hidden">
			<xsl:attribute name="name"><xsl:value-of select="$name"/></xsl:attribute>
			<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$value"/></xsl:with-param></xsl:call-template></xsl:attribute>
		</input>
	</xsl:when>
	<xsl:otherwise>
		<textarea cols="40" rows="1" 
		style="scrollbar-3dlight-color: #FFFFFF;scrollbar-arrow-color: #FFFFFF; scrollbar-base-color:#FFFFFF; scrollbar-darkshadow-color: #FFFFFF; scrollbar-face-color: #FFFFFF; scrollbar-highlight-color: #FFFFFF; scrollbar-shadow-color: #FFFFFF;">
			<xsl:if test="$cols"><xsl:attribute name="cols"><xsl:value-of select="$cols"/></xsl:attribute></xsl:if>
			<xsl:if test="$rows"><xsl:attribute name="rows"><xsl:value-of select="$rows"/></xsl:attribute></xsl:if>
			<xsl:attribute name="name"><xsl:value-of select="$name"/></xsl:attribute>
			<xsl:value-of select="$value"/>	
		</textarea>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## TTViewer                                     ##
	################################################## -->
<xsl:template match="TTViewer">
	<table class="TTHeader" width="100%">
		<tr>
			<td class="tight" width="20%">
			</td>
			<td class="TTHeader" width="60%" style="text-align: center;">
				<xsl:apply-templates select="centerHeaderColumn/*"/>
			</td>
			<td class="TTHeader" width="20%" rowspan="2">
				<xsl:apply-templates select="rightHeaderColumn"/>
			</td>
		</tr>
		<tr>
			<td class="leftHeader" width="35%" style="vertical-align:bottom;" colspan="2">
				<xsl:apply-templates select="leftHeaderColumn/*"/>
			</td>
		</tr>
	</table>
	<div class="TTBody">
		<xsl:apply-templates select="TTBody/*"/>
	</div>
</xsl:template>

<!--##################################################
    ## TT_documents                                 ##
	################################################## -->
<xsl:template match="TT_documents">
<xsl:if test="record">
	<tr>
		<td class="TEA_TT_POST">
			<xsl:attribute name="colspan"><xsl:value-of select="count(ancestor::obj[@type='resultSet']/columns/column)"/></xsl:attribute>
			<xsl:for-each select="record">
				<xsl:variable name="docObj"><xsl:copy-of select="."/></xsl:variable>
				<xsl:call-template name="objDocument">
					<xsl:with-param name="obj" select="$docObj"/>
				</xsl:call-template>
			</xsl:for-each>
			<xsl:apply-templates select="extraData"/>
		</td>
	</tr>
</xsl:if>
</xsl:template>

<!--##################################################
    ## note							                ##
	################################################## -->
<xsl:template match="note">
<tr>
	<td class="TEA_TT_Pre">
		<xsl:attribute name="colspan"><xsl:value-of select="count(ancestor::obj[@type='resultSet']/columns/column)"/></xsl:attribute>
		<div class="note">
			<xsl:copy-of select="root"/>		
		</div>
	</td>
</tr>
</xsl:template>

<!--##################################################
    ## TT_LINKS                                     ##
	################################################## -->
<xsl:template match="TT_links">
<xsl:if test="record">
	<tr>
		<td style="text-align: center;padding-top: 5">
			<xsl:attribute name="colspan"><xsl:value-of select="count(ancestor::obj[@type='resultSet']/columns/column)"/></xsl:attribute>
			<table class="tight" width="90%">
				<tr>
					<td class="tight" style="text-align: right">
						<nobr><xsl:call-template name="putText"><xsl:with-param name="key">See Also:</xsl:with-param></xsl:call-template></nobr>
					</td>
					<td class="tight">
						<xsl:for-each select="record">
							<nobr>
								<a>
									<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=viewTT&amp;ID=<xsl:value-of select="field[@name = 'ID']/@value"/></xsl:attribute>
									<xsl:value-of select="field[@name = 'NAME']/@value"/>
									<xsl:call-template name="putText"><xsl:with-param name="key">REV</xsl:with-param></xsl:call-template>
									<xsl:value-of select="field[@name = 'REV']/@value"/>
								</a>
								<xsl:if test="field[@name = 'STATUS']">
									<span class="revInfo">
										(<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'STATUS']/@value"/></xsl:with-param></xsl:call-template>)
									</span>
								</xsl:if>
								<xsl:if test="position() != last()">
								, 
								</xsl:if>
							</nobr>
						</xsl:for-each>
					
					</td>
				</tr>
			</table>
		</td>
	</tr>
</xsl:if>
</xsl:template>

</xsl:stylesheet>