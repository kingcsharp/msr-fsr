<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ## loginScreen                                  ##
	################################################## -->
<xsl:template match="loginScreen">
	<xsl:apply-templates select="loginError"/>
	<div class="loginBox" style="text-align: center;padding: 5;">
		<table class="loginScreen">
			<form action="login.asp" method="post" name="login">
				<input type="hidden" name="Action" value="CheckUserID" />
				<input type="hidden" name="destinationPage">
					<xsl:attribute name="value"><xsl:value-of select="ref"/></xsl:attribute>
				</input>
				<tr>
					<td class="fLabel">
						<xsl:call-template name="putText"><xsl:with-param name="key">LOGIN</xsl:with-param></xsl:call-template>
					</td>
					<td class="fData">
						<input type="text" name="login" />
						<script language="javascript">
							objFocusOn = document.login.login
						</script>
					</td>
				</tr>
				<tr>
					<td class="fLabel">
						<xsl:call-template name="putText"><xsl:with-param name="key">PASSWORD</xsl:with-param></xsl:call-template>
					</td>
					<td class="fData">
						<input type="password" name="password" />
					</td>
				</tr>
				<input type="hidden" value="false" name="AutoLogin" />
				<tr>
					<th colspan="2">
						<input type="submit">
							<xsl:choose>
								<xsl:when test="$stringEdit='yes'">
									<xsl:call-template name="putText"><xsl:with-param name="key">LoginButton</xsl:with-param></xsl:call-template>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">LoginButton</xsl:with-param></xsl:call-template></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</input>
						<br/>
						<input type="button" onclick="document.login.AutoLogin.value = 'true';document.login.submit();">
							<xsl:choose>
								<xsl:when test="$stringEdit='yes'">
									<xsl:call-template name="putText"><xsl:with-param name="key">Login And Auto Login from now on</xsl:with-param></xsl:call-template>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Login And Auto Login from now on</xsl:with-param></xsl:call-template></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</input>

					</th>
				</tr>
			</form>
		</table>
	</div>
</xsl:template>

<!--##################################################
    ## loginError                                   ##
	################################################## -->
<xsl:template match="loginError">
	<div style="color:red;text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">LoginError</xsl:with-param></xsl:call-template></div>
</xsl:template>


<!--##################################################
    ## loginPage                                    ##
	################################################## -->
<xsl:template match="loginPage">
	<xsl:apply-templates select="content"/>
</xsl:template>


</xsl:stylesheet>
