<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

<!--##################################################
    ## txt                                     ##
	################################################## -->
<xsl:template match="txt">
	<txt>
		<xsl:for-each select="textString">
			<xsl:sort select="@id" order="ascending"/>
			<textString>
				<xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
			</textString>
		</xsl:for-each>
	</txt>
</xsl:template>
</xsl:stylesheet>