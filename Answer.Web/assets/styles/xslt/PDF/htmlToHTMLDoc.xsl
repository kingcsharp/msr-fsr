<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ##  html                                        ##
	################################################## -->
<xsl:template match="/">
<div>
	<font face="Arial" size="-6">
		<xsl:apply-templates select="//div[@class='default_body']" />
		<xsl:apply-templates select="*" />
	</font>
</div>
</xsl:template>

<!--##################################################
    ##  form                                        ##
	################################################## -->
<xsl:template match="form">
	<xsl:apply-templates />
</xsl:template>

<!--##################################################
    ##  div                                           ##
	################################################## -->
<xsl:template match="div">
<div>
	<xsl:apply-templates />
</div>
</xsl:template>

<!--##################################################
    ##  br                                          ##
	################################################## -->
<xsl:template match="br"><br /></xsl:template>

<!--##################################################
    ##  table                                       ##
	################################################## -->
<xsl:template match="table">
<table>
	<xsl:choose>
		<xsl:when test="@class='standard'"><xsl:attribute name="border">outset</xsl:attribute></xsl:when>
		<xsl:otherwise>	<xsl:attribute name="border">0</xsl:attribute></xsl:otherwise>
	</xsl:choose>
	<xsl:apply-templates />
</table>
</xsl:template>


<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="tr">
<tr>
	<xsl:apply-templates />
</tr>
</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="td">
<td>
	<xsl:choose>
		<xsl:when test="@class='standard'"><xsl:attribute name="border">inset</xsl:attribute></xsl:when>
		<xsl:otherwise><xsl:attribute name="border">0</xsl:attribute></xsl:otherwise>
	</xsl:choose>
	<xsl:call-template name="getColBG" />
	<font>
		<xsl:call-template name="getFontStyle" />
		<xsl:apply-templates /><font size="-20"></font>
	</font>
</td>
</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="th">
<th>
	<xsl:choose>
		<xsl:when test="@class='standard'">
			<xsl:attribute name="border">inset</xsl:attribute>
			<xsl:attribute name="bgColor">#CCCCCC</xsl:attribute>
		</xsl:when>
		<xsl:otherwise><xsl:attribute name="border">0</xsl:attribute></xsl:otherwise>
	</xsl:choose>
	<font color="#000000">
		<b>
			<xsl:apply-templates />
		</b>
	</font>
</th>

</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="img">
<xsl:copy-of select="." />
</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="a">
<xsl:apply-templates />
</xsl:template>

<!--##################################################
    ##  getColBG                                   ##
	################################################## -->
<xsl:template name="getColBG">
<xsl:choose>
	<xsl:when test="contains(@style,'background-color:#aaaaaa')"><xsl:attribute name="bgcolor">#aaaaaa</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'background-color:#b2b2b2')"><xsl:attribute name="bgcolor">#b2b2b2</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'background-color:#bbbbbb')"><xsl:attribute name="bgcolor">#bbbbbb</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'background-color:#cccccc')"><xsl:attribute name="bgcolor">#cccccc</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'background-color:#dddddd')"><xsl:attribute name="bgcolor">#dddddd</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'background-color:#eaeaea')"><xsl:attribute name="bgcolor">#eaeaea</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'background-color:#FFFFFF')"><xsl:attribute name="bgcolor">#FFFFFF</xsl:attribute></xsl:when>
</xsl:choose>
</xsl:template>

<!--##################################################
    ##  getFontStyle                                ##
	################################################## -->
<xsl:template name="getFontStyle">
<xsl:choose>
	<xsl:when test="contains(@style,'color:#000000')"><xsl:attribute name="color">#000000</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'color:#804000')"><xsl:attribute name="color">#804000</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'color:#EF000C')"><xsl:attribute name="color">#EF000C</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'color:#C500EA')"><xsl:attribute name="color">#C500EA</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'color:#1705F9')"><xsl:attribute name="color">#1705F9</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'color:#399700')"><xsl:attribute name="color">#399700</xsl:attribute></xsl:when>
	<xsl:when test="contains(@style,'color:#FF9931')"><xsl:attribute name="color">#FF9931</xsl:attribute></xsl:when>
</xsl:choose>

</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="*">
</xsl:template>
</xsl:stylesheet>

