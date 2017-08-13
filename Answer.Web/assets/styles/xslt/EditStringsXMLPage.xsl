<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">


<!--##################################################
    ## editStringsForm                              ##
	################################################## -->
<xsl:template match="html">
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/editStringsImport.js</xsl:attribute>
		//placeholder
	</script>
	
	<form name="strings" method="post">
		<xsl:attribute name="action"><xsl:value-of select="$path_to_top"/>asp/updateStrings.asp</xsl:attribute>
		<table>
			<input type="hidden" name="langFiles">
				<xsl:attribute name="value"><xsl:value-of select="//myLanguageFileName/@value"/></xsl:attribute>
			</input>
			<input type="Hidden" name="pathToLangFolders">
				<xsl:attribute name="value"><xsl:value-of select="$path_to_lang_folders"/></xsl:attribute>
			</input>
			<xsl:for-each select="//EditableString">
				<xsl:sort select="@filename"/>
				<xsl:sort select="@key"/>
				<tr>
					<td>
						<xsl:value-of select="@key"/>
					</td>
					<td>
						<input type="text" size="100">
							<xsl:attribute name="name"><xsl:value-of select="@key"/>_____Text</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
						</input>
					</td>
					<td>
						<xsl:call-template name="putGlobal">
							<xsl:with-param name="myFile" select="."/>
						</xsl:call-template>
					</td>
				</tr>
				
			</xsl:for-each>
			<tr>
				<td colspan="3">
					<input type="submit" />
				</td>
			</tr>
			
		</table>
	</form>
</xsl:template>


<!--##################################################
    ## putStringFiles                               ##
	################################################## -->
<xsl:template name="putGlobal">
<xsl:param name="myFile" />
<input type="Checkbox" value="true">
	<xsl:attribute name="name"><xsl:value-of select="$myFile/@key"/>_____isGlobal</xsl:attribute>
	<xsl:if test="contains($myFile/@filename,'globalStrings.xml')">
		<xsl:attribute name="checked">checked</xsl:attribute>
	</xsl:if>
</input>
Global

</xsl:template>

</xsl:stylesheet>