<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output indent="no" omit-xml-declaration="yes" method="xml" encoding="utf-8"/>
  <xsl:strip-space elements="*"/>

  <!--##################################################
    ## calendar                                     ##
	################################################## -->
<xsl:template match="obj[@type='resultSet']">
  <xsl:copy-of select="data"/>
</xsl:template>

  <xsl:template match="/">
    <xsl:apply-templates select="//obj[@type='resultSet']" />
  </xsl:template>


</xsl:stylesheet>
