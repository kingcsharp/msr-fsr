<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!--##################################################
    ##  wscrData                                    ##
	################################################## -->
<xsl:template match="wscrData">
<div>
	Weekly Service Calls Raw Data for 3 weeks starting on <xsl:value-of select="dates/date/fullDate"/>
</div>
Normal Time:
<table class="tight">
	<xsl:call-template name="printTitleRow" />
	<xsl:for-each select="serviceCalls/record">
		<xsl:call-template name="printServiceCallRow">
			<xsl:with-param name="type">NORMAL</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
</table>

Over Time:
<table class="tight">
	<xsl:call-template name="printTitleRow" />
	<xsl:for-each select="serviceCalls/record">
		<xsl:call-template name="printServiceCallRow">
			<xsl:with-param name="type">OVER</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
</table>

Total Time:
<table class="tight">
	<xsl:call-template name="printTitleRow" />
	<xsl:for-each select="serviceCalls/record">
		<xsl:call-template name="printServiceCallRow">
			<xsl:with-param name="type">TOTAL</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
</table>

All Together:
<table class="tight">
	<xsl:call-template name="printTitleRow" />
	<xsl:for-each select="serviceCalls/record">
		<xsl:call-template name="printServiceCallRow">
			<xsl:with-param name="type">ALL</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
</table>


</xsl:template>



<!--##################################################
    ##  print Title Row                             ##
	################################################## -->
<xsl:template name="printTitleRow">
	<tr>
		<td class="standard">ID</td>
		<td class="standard">Worker Name</td>
		<td class="standard">Boss Name</td>
		<td class="standard">Machine Name</td>
		<td class="standard">Work Type</td>
		
		<xsl:for-each select="dates/date">
			<td class="standard">
				<xsl:value-of select="fullDate"/>
			</td>
		</xsl:for-each>
	</tr>
</xsl:template>

<!--##################################################
    ##  printServiceCallRow                         ##
	################################################## -->
<xsl:template name="printServiceCallRow">
<xsl:param name="type" />
<xsl:variable name="ID"><xsl:value-of select="ID"/></xsl:variable>
<tr>
	<td><xsl:value-of select="ID"/></td>
	<td><xsl:value-of select="WORKER_NAME"/></td>
	<td><xsl:value-of select="WORK_TYPE_NAME"/></td>
	<xsl:for-each select="../../dates/date">
		<xsl:variable name="y"><xsl:value-of select="year"/></xsl:variable>
		<xsl:variable name="m"><xsl:value-of select="month"/></xsl:variable>
		<xsl:variable name="d"><xsl:value-of select="day"/></xsl:variable>
		<xsl:variable name="normalTime">
			<xsl:value-of select="../../times/record[(WEEKLY_ID = $ID) and (MO = $m) and (YR = $y) and (D = $d) and (HOUR_TYPE = 'NORMAL')]/HOURS"/>
		</xsl:variable>
		<xsl:variable name="overTime">
			<xsl:value-of select="../../times/record[(WEEKLY_ID = $ID) and (MO = $m) and (YR = $y) and (D = $d) and (HOUR_TYPE = 'OVER')]/HOURS"/>
		</xsl:variable>
		<xsl:variable name="totalTime">
			<xsl:value-of select="$normalTime + $overTime"/>
		</xsl:variable>
		<td class="standard">
			<xsl:choose>
				<xsl:when test="$type = 'NORMAL'"><xsl:value-of select="$normalTime"/></xsl:when>
				<xsl:when test="$type = 'OVER'"><xsl:value-of select="$overTime"/></xsl:when>
				<xsl:when test="$type = 'TOTAL'"><xsl:value-of select="$totalTime"/></xsl:when>
				<xsl:when test="$type = 'ALL'"><xsl:value-of select="$normalTime"/>/<xsl:value-of select="$overTime"/>/<xsl:value-of select="$totalTime"/></xsl:when>
			</xsl:choose>				
		</td>
	</xsl:for-each>
</tr>

</xsl:template>


</xsl:stylesheet>