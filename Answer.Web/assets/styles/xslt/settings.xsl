<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">


<!--##################################################
    ## settings                                     ##
	################################################## -->
<xsl:template match="settings">
	<form method="post">
	<xsl:attribute name="action"><xsl:value-of select="$path_to_top"/>asp/settings/set_sessions.asp</xsl:attribute>
	<table class="settings">
		<xsl:for-each select="session_var">
			<tr>
				<th class="settings">
					<xsl:value-of select="@display"/>
				</th>
				<td class="settings">
					<xsl:if test="@type='drop'">
						<select>
							<xsl:if test="onChange"><xsl:attribute name="onChange">this.form.submit();</xsl:attribute></xsl:if>
							<xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
							<xsl:for-each select="option">
								<option>
									<xsl:if test="@value = ../@value">
										<xsl:attribute name="selected">YES</xsl:attribute>
									</xsl:if>
									<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
									<xsl:value-of select="."/>
								</option>
							</xsl:for-each>
						</select>
					</xsl:if>
				</td>
			</tr>
		</xsl:for-each>
		<xsl:if test="not(@noSubmit)">
			<tr>
				<td class="settings" colspan="20" align="center"><input type="submit"><xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">SubmitIt</xsl:with-param></xsl:call-template></xsl:attribute></input></td>
			</tr>
		</xsl:if>
		<div style="display:none" class="hiddenForWords">
        	<xsl:call-template name="putText"><xsl:with-param name="key">SubmitIt</xsl:with-param></xsl:call-template>
        </div>
	</table>
	</form>
</xsl:template>
</xsl:stylesheet>