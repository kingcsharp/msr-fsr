<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="html">
<xsl:apply-templates />
</xsl:template>


<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="span">
<xsl:value-of select="."/>
</xsl:template>


</xsl:stylesheet>
