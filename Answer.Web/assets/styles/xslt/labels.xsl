<xsl:stylesheet	version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" output="XML">
<!--##################################################
    ##  printLabels                                 ##
	################################################## -->
<xsl:template name="printLabels">
<xsl:param name="myType"/>
<xsl:variable name="t" select="." />
<xsl:variable name="l" select="labelInfo" />

<xsl:for-each select="page">
<div>
	<xsl:attribute name="style">position:absolute;top:0px;left:0px;width:<xsl:value-of select="$l/pageWidth"/>in;height:<xsl:value-of select="$l/pageHeight"/>in;<xsl:if test="position() != last()">page-break-after:always</xsl:if></xsl:attribute>
	<xsl:variable name="p" select="." />
	<xsl:for-each select="row">
		<xsl:variable name="myRow"><xsl:value-of select="position()"/></xsl:variable>
		<xsl:for-each select="col">
			<xsl:variable name="myCol"><xsl:value-of select="position()"/></xsl:variable>
			<div>
				<xsl:attribute name="style">padding-left:<xsl:value-of select="$l/lPad"/>in;height:<xsl:value-of select="$l/height"/>in;width:<xsl:value-of select="$l/width - $l/lPad"/>in;position:absolute;padding-top:0;padding-bottom:0;padding-right:0;margin:0;left:<xsl:value-of select="(number($myCol)-1)* (number($l/width + number($l/colRPad)))"/>in;top:<xsl:value-of select="number($l/height) * number(($myRow)-1)"/>in;</xsl:attribute>
<!--				<xsl:value-of select="$myCol"/>,<xsl:value-of select="$myRow"/>-<xsl:value-of select="$l/width"/>-<xsl:value-of select="$l/height"/>-->
				<br />
				<xsl:if test="item">
					<xsl:variable name="cnt"><xsl:value-of select="item/@cnt" /></xsl:variable>
					<xsl:choose>
						<xsl:when test="$myType = 'HISTORY'">
							<xsl:call-template name="PRINT_HISTORY_LABEL">
								<xsl:with-param name="pItem" select="$t/purchaseItems/record[actualCount = $cnt]" />
								<xsl:with-param name="t" select="$t" />
							</xsl:call-template>
						</xsl:when>
						<xsl:when test="$myType = 'MONITOR'">
							<xsl:call-template name="PRINT_MONITOR_LABEL">
								<xsl:with-param name="pItem" select="$t/purchaseItems/record[actualCount = $cnt]" />
								<xsl:with-param name="t" select="$t" />
							</xsl:call-template>
						</xsl:when>
					</xsl:choose>
				</xsl:if>
			</div>
		</xsl:for-each>
	</xsl:for-each>
</div>
</xsl:for-each>


</xsl:template>


</xsl:stylesheet>
