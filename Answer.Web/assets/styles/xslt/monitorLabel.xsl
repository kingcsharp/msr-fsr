<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!--##################################################
    ##  PURCHASE_SERVICE_REPORT                     ##
	################################################## -->
<xsl:template match="MONITOR_LABEL">
<xsl:call-template name="printLabels">
	<xsl:with-param name="myType">MONITOR</xsl:with-param>
</xsl:call-template>
</xsl:template>

<!--##################################################
    ##  PRINT_PURCHASE_ITEM_PAGE                    ##
	################################################## -->
<xsl:template name="PRINT_MONITOR_LABEL">
<xsl:param name="pItem"/>
<xsl:param name="t"/>
<xsl:for-each select="$pItem">
<div style="font-size: x-small;">
	<div style="border-bottom: solid 1px black">
	<xsl:copy-of select="TASK_DESC"/><br />
	<xsl:value-of select="DESCRIPTION"/>
	</div>
	<table style="font-size:8px; border-bottom:1px solid black;width:100%;">
		<tr>
			<td>
				<xsl:call-template name="putText"><xsl:with-param name="key">Part #</xsl:with-param></xsl:call-template>
			</td>
			<td>
				<xsl:value-of select="COMPANY_PART_NUMBER"/>
			</td>
			<td>
				<xsl:call-template name="putText"><xsl:with-param name="key">Part Name</xsl:with-param></xsl:call-template>
			</td>
		</tr>
		<tr>
			<td>
				<xsl:call-template name="putText"><xsl:with-param name="key">Purch Item#</xsl:with-param></xsl:call-template>
			</td>
			<td>
				<xsl:value-of select="PURCH_ITEM_ID"/>
			</td>
			<td>
				<xsl:value-of select="PART_DESC"/>
			</td>
		</tr>
	</table>
	<div style="border-bottom: solid 1px black;width:100%;">
	<xsl:choose>
		<xsl:when test="MONITOR_TYPE = 'YES_NO'">
			<xsl:call-template name="printYesNoResult" />
		</xsl:when>
		<xsl:when test="MONITOR_TYPE = 'NUMBER'">
			<xsl:call-template name="printNumberResult" />
		</xsl:when>
	</xsl:choose>
	<xsl:value-of select="COMMENT"/>
	</div>
	<table style="font-size:8px; border-bottom:1px solid black;width:100%;">
		<tr>
			<td>
				<xsl:call-template name="putText"><xsl:with-param name="key">Requestee:</xsl:with-param></xsl:call-template>
			</td>
			<td>
				<xsl:value-of select="LATEST_REQUESTEE_NAME"/>
			</td>
		</tr>
		<tr>
			<td>
				<xsl:call-template name="putText"><xsl:with-param name="key">Completion Date:</xsl:with-param></xsl:call-template>
			</td>
			<td>
				<xsl:value-of select="ACTUAL_STOP_DATE/answerDate"/>
			</td>
		</tr>
	</table>
</div>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##   printYesNoResult                           ##
	################################################## -->
<xsl:template name="printYesNoResult">
<table style="8px;">
	<tr>
		<td>
			<xsl:call-template name="putText"><xsl:with-param name="key">Target:</xsl:with-param></xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="putText"><xsl:with-param name="key">Result:</xsl:with-param></xsl:call-template>
		</td>
		<td rowspan="2" style="font-size:small;">
			<xsl:choose>
				<xsl:when test="IS_PASSING = '1'">
					<span style="color:green">
						<xsl:call-template name="putText"><xsl:with-param name="key">PASSING</xsl:with-param></xsl:call-template>
					</span>
				</xsl:when>
				<xsl:otherwise>
					<span style="color:red">
						<xsl:call-template name="putText"><xsl:with-param name="key">FAILING</xsl:with-param></xsl:call-template>
					</span>
				</xsl:otherwise>
			</xsl:choose>
		</td>
	</tr>
	<tr>
		<td>
			<xsl:choose>
				<xsl:when test="YES_NO_ANSWER = '1'">
					Yes
				</xsl:when>
				<xsl:otherwise>
					No
				</xsl:otherwise>
			</xsl:choose>
		</td>
		<td>
			<xsl:choose>
				<xsl:when test="MY_ANSWER = '1'">
					Yes
				</xsl:when>
				<xsl:otherwise>
					No
				</xsl:otherwise>
			</xsl:choose>
		</td>
	</tr>
</table>		
			
</xsl:template>

<!--##################################################
    ##   printNumberResult                           ##
	################################################## -->
<xsl:template name="printNumberResult">
<table style="font-size:8px;margin:1px;" class="tight">
	<tr>
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="HIGHEST_THRESHOLD"/></xsl:with-param>
			<xsl:with-param name="str">Highest Threshold</xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="HIGH_THRESHOLD"/></xsl:with-param>
			<xsl:with-param name="str">High Threshold</xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="TARGET"/></xsl:with-param>
			<xsl:with-param name="str">Target</xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="LOW_THRESHOLD"/></xsl:with-param>
			<xsl:with-param name="str">Low Threshold</xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="LOWEST_THRESHOLD"/></xsl:with-param>
			<xsl:with-param name="str">lowest Threshold</xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="MY_ANSWER"/></xsl:with-param>
			<xsl:with-param name="str">Result</xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColHead">
			<xsl:with-param name="val"><xsl:value-of select="PRINT_RESULT"/></xsl:with-param>
			<xsl:with-param name="str">Value</xsl:with-param>
		</xsl:call-template>			

	</tr>
	<tr>
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:value-of select="HIGHEST_THRESHOLD"/></xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:value-of select="HIGH_THRESHOLD"/></xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:value-of select="TARGET"/></xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:value-of select="LOW_THRESHOLD"/></xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:value-of select="LOWEST_THRESHOLD"/></xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="MY_ANSWER"/></xsl:with-param></xsl:call-template></xsl:with-param>
		</xsl:call-template>			
		<xsl:call-template name="numColVal">
			<xsl:with-param name="val"><xsl:value-of select="PRINT_RESULT"/></xsl:with-param>
		</xsl:call-template>			
	</tr>
</table>
</xsl:template>


<!--##################################################
    ##  numColHead                                           ##
	################################################## -->
<xsl:template name="numColHead">
<xsl:param name="val"/>
<xsl:param name="str"/>
<xsl:if test="string-length($val)&gt;0">
	<td style="border:1px solid black"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$str"/></xsl:with-param></xsl:call-template></td>
</xsl:if>
</xsl:template>
<!--##################################################
    ##  numColVal                                           ##
	################################################## -->
<xsl:template name="numColVal">
<xsl:param name="val"/>
<xsl:if test="string-length($val)&gt;0">
	<td style="border:1px solid black"><xsl:value-of select="$val"/></td>
</xsl:if>
</xsl:template>


</xsl:stylesheet>
