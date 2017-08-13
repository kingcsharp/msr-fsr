<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ##  html                                        ##
	################################################## -->
<xsl:template match="html">
<fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">

<fo:layout-master-set>
  <fo:simple-page-master master-name="A4">
	  <fo:region-body margin="5in" />
  </fo:simple-page-master>
</fo:layout-master-set>

<fo:page-sequence master-reference="A4">
  <fo:flow flow-name="xsl-region-body">
    <fo:block>Hello W3Schools</fo:block>
  </fo:flow>
</fo:page-sequence></fo:root>

</xsl:template>

<!--##################################################
    ##    /                                         ##
	################################################## -->
<xsl:template match="/">
<xsl:param name="obj"/>
<xsl:apply-templates select="html" />
</xsl:template>






</xsl:stylesheet>

