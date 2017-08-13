<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:variable name="path_to_top">../</xsl:variable>
<xsl:variable name="path_to_lib">../AnswerLibrary</xsl:variable>
<xsl:variable name="path_to_images">AnswerLibrary/images/</xsl:variable>

<!--##################################################
    ##                                
	b i ul red pur bl gr yel or br/ /b /i /ul /red /pur /bl /gr /yel /or
	##
	################################################## -->
<xsl:template match="step">
	<root>
		<xsl:apply-templates/>
	</root>
</xsl:template>
<!--##################################################
    ## url                                          ##
	################################################## -->
<xsl:template match="link">
	<a>
		<xsl:attribute name="href"><xsl:value-of select="url"/></xsl:attribute>
		<xsl:value-of select="text"/>
	</a>
</xsl:template>
<!--##################################################
    ## red                                          ##
	################################################## -->
<xsl:template match="li">
	<li type="disc" />
</xsl:template>
<!--##################################################
    ## red                                          ##
	################################################## -->
<xsl:template match="red">
	<span style="color: red"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## blu                                          ##
	################################################## -->
<xsl:template match="bl">
	<span style="color: blue"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## pur                                          ##
	################################################## -->
<xsl:template match="pur">
	<span style="color: purple"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## gr                                          ##
	################################################## -->
<xsl:template match="gr">
	<span style="color: green"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## yel                                          ##
	################################################## -->
<xsl:template match="yel">
	<span style="color: yellow"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## or                                          ##
	################################################## -->
<xsl:template match="or">
	<span style="color: orange"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## b                                          ##
	################################################## -->
<xsl:template match="b">
	<span style="font-weight: bold"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## i                                          ##
	################################################## -->
<xsl:template match="i">
	<span style="font-style:italic;"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## ul                                          ##
	################################################## -->
<xsl:template match="ul">
	<span style="text-decoration: underline"><xsl:apply-templates/></span>
</xsl:template>
<!--##################################################
    ## br                                          ##
	################################################## -->
<xsl:template match="br">
	<br /><xsl:apply-templates/>
</xsl:template>
<!--##################################################
    ## theta                                        ##
	################################################## -->
<xsl:template match="theta">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>theta.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## omega                                        ##
	################################################## -->
<xsl:template match="omega">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>omega.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## ang                                        ##
	################################################## -->
<xsl:template match="ang">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>Angstrom.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## delta                                        ##
	################################################## -->
<xsl:template match="delta">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>delta.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## sigma                                        ##
	################################################## -->
<xsl:template match="sigma">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>sigma.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## pi                                        ##
	################################################## -->
<xsl:template match="pi">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>pi.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## lambda                                        ##
	################################################## -->
<xsl:template match="lambda">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>lambda.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## beta                                        ##
	################################################## -->
<xsl:template match="beta">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>beta.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## alpha                                        ##
	################################################## -->
<xsl:template match="alpha">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>alpha.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## degree                                        ##
	################################################## -->
<xsl:template match="degree">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>degree.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## micro                                        ##
	################################################## -->
<xsl:template match="micro">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>micro.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## phase                                        ##
	################################################## -->
<xsl:template match="phase">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>phase.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## therefore                                        ##
	################################################## -->
<xsl:template match="therefore">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>therefore.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## divide                                        ##
	################################################## -->
<xsl:template match="divide">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>divide.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## approx                                        ##
	################################################## -->
<xsl:template match="approx">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>approx.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## notEqual                                        ##
	################################################## -->
<xsl:template match="notEqual">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>notEqual.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## lessEqual                                        ##
	################################################## -->
<xsl:template match="lessEqual">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>lessEqual.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## greatEqual                                        ##
	################################################## -->
<xsl:template match="greatEqual">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>greaterEqual.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## angle                                        ##
	################################################## -->
<xsl:template match="angle">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>angle.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## infinity                                        ##
	################################################## -->
<xsl:template match="infinity">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>infinity.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## infinity                                        ##
	################################################## -->
<xsl:template match="plusMinus">
	<img>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>plusMinus.gif</xsl:attribute>
	</img>
</xsl:template>
<!--##################################################
    ## tab                                        ##
	################################################## -->
<xsl:template match="tab">
<span class="tab"> </span>
</xsl:template>


</xsl:stylesheet>