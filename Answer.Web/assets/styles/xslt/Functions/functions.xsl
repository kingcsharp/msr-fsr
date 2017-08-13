<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--###########Includes for the files in the functions library-->
<xsl:include href="standardPage.xsl"/>





<!--##################################################
    ## getTemplateTaskTypes                         ##
	################################################## -->
<xsl:template name="getTemplateTaskTypes">
	<TT_Types>
		<item id="PROCEDURE">
			<xsl:attribute name="name"><xsl:call-template name="putText"><xsl:with-param name="key">Procedure</xsl:with-param></xsl:call-template></xsl:attribute>
		</item>
		<item id="INSTRUCTION">
			<xsl:attribute name="name"><xsl:call-template name="putText"><xsl:with-param name="key">Instruction</xsl:with-param></xsl:call-template></xsl:attribute>
		</item>
	</TT_Types>
</xsl:template>


</xsl:stylesheet>