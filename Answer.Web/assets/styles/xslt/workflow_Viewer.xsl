<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ## work_flow_chart                              ##
	################################################## -->
<xsl:template match="work_flow_chart">
	<table class="wf_chart">
		<tr>
			<td class="wf_title" align="center">
				<xsl:attribute name="colspan"><xsl:value-of select="count(WFstage)"/></xsl:attribute>
				<a class="approvalLink">
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=editWorkFlow&amp;ID=<xsl:value-of select="@WF_ID"/></xsl:attribute>
					<xsl:value-of select="@name"/>
				</a>
			</td>
		</tr>
		<tr>
			<xsl:for-each select="WFstage">
				<td valign="middle">
					<table>
						<tr>
							<td class="wf_viewer">
								<xsl:apply-templates select="."/>			
							</td>
							<td class="wf_viewer">
								<xsl:if test="position() != last()">
									<img>
										<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>wf_arrow.gif</xsl:attribute>
									</img>
								</xsl:if>
							</td>
						</tr>
					</table>		
				</td>
			</xsl:for-each>
		</tr>
		<tr>
			<td class="wf_viewer">
			<xsl:attribute name="colspan"><xsl:value-of select="count(WFstage)"/></xsl:attribute>
<!--				<xsl:apply-templates select="WFApprovals"/>-->
			</td>
		</tr>
	</table>
</xsl:template>

<!--##################################################
    ## WFApprovals                                  ##
	################################################## -->
<xsl:template match="WFApprovals">
	<table class="wf_approvals">
		<xsl:for-each select="WFapproval">
			<tr>
				<td class="Approval">
				<a>
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=editWorkFlow&amp;ID=<xsl:value-of select="@WF_ID"/></xsl:attribute>
					<xsl:value-of select="@name"/>
				</a>
				</td>
			</tr>
		</xsl:for-each>
	</table>
</xsl:template>

<!--##################################################
    ## WFstage                                      ##
	################################################## -->
<xsl:template match="WFstage">
	<table class="wf_stage">
		<tr>
			<td class="wf_stage_title" align="center" >
				<a class="approvalLink">
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=editApprovalStage&amp;ID=<xsl:value-of select="@WF_STAGE_ID"/></xsl:attribute>
					<xsl:value-of select="@name"/>
				</a>
			</td>
		</tr>
		<xsl:for-each select="WFgroup">
			<tr>
				<td class="wf_viewer">
					<xsl:apply-templates select="."/>
				</td>
			</tr>
		</xsl:for-each>
		<tr>
			<td class="wf_viewer">
<!--				<xsl:apply-templates select="WFApprovals"/>-->
			</td>
		</tr>
	</table>
</xsl:template>

<!--##################################################
    ## WFgroup                                      ##
	################################################## -->
<xsl:template match="WFgroup">
<table class="wf_group">
	<tr>
		<td class="wf_group_title" align="center">
			<a class="approvalLink">
				<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=editApprovalGroup&amp;ID=<xsl:value-of select="@WF_GROUP_ID"/></xsl:attribute>
				<xsl:value-of select="@name"/>
			</a>
		</td>
	</tr>
	<xsl:for-each select="WFapprover">
		<tr>
			<td class="wf_person">
				<xsl:apply-templates select="."/>
			</td>
		</tr>
	</xsl:for-each>
		<tr>
			<td class="wf_viewer">
<!--				<xsl:apply-templates select="WFApprovals"/>-->
			</td>
		</tr>

</table>
</xsl:template>

<!--##################################################
    ## WFapprover                                   ##
	################################################## -->
<xsl:template match="WFapprover">
	<a class="approvalLink">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=editPerson&amp;ID=<xsl:value-of select="@ID"/></xsl:attribute>
		<xsl:value-of select="@name"/>
	</a>
</xsl:template>
</xsl:stylesheet>