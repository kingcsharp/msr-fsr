<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ##  ANSWER_THEORY                            ##
	################################################## -->
<xsl:template match="ANSWER_THEORY">
<div class="theory">
	<div class="assistantTable"><xsl:apply-templates select="headerData"/></div>
	<xsl:call-template name="printComments">
		<xsl:with-param name="LOC">1-Header</xsl:with-param>
	</xsl:call-template>
	<xsl:apply-templates select="headerData/REFERENCE_FILES"/>
	<div class="assistantTable">
		<xsl:apply-templates select="paragraphData/obj" />
	</div>
	<xsl:call-template name="printComments">
		<xsl:with-param name="LOC">2-Footer</xsl:with-param>
	</xsl:call-template>
	<xsl:apply-templates select="revisionData" />
</div>
</xsl:template>

<!--##################################################
    ##  printComments                               ##
	################################################## -->
<xsl:template name="printComments">
<xsl:param name="LOC"/>
<xsl:if test="//headerData/comments/COMMENT[@LOCATION=$LOC]">
	<div class="procedureCommentBox">
		<xsl:for-each select = "//headerData/comments/COMMENT[@LOCATION=$LOC]">
			<div class="commentBox">
				<xsl:copy-of select="root"/>
			</div>
		</xsl:for-each>
	</div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  revisionData                                ##
	################################################## -->
<xsl:template match="revisionData">
<div class="revData">
	<table class="revData">
			<tr>
				<th class="revData"><xsl:call-template name="putText"><xsl:with-param name="key">Revision Num</xsl:with-param></xsl:call-template></th>
				<th class="revData"><xsl:call-template name="putText"><xsl:with-param name="key">Revision Create Date</xsl:with-param></xsl:call-template></th>
				<th class="revData"><xsl:call-template name="putText"><xsl:with-param name="key">Revision Approval Date</xsl:with-param></xsl:call-template></th>
				<th class="revData"><xsl:call-template name="putText"><xsl:with-param name="key">Revision Comment</xsl:with-param></xsl:call-template></th>
			</tr>
		<xsl:for-each select="record">
			<tr>
				<td class="revData">
					<xsl:value-of select="field[@name='REV']/@value"/>
				</td>
				<td class="revData">
					<xsl:value-of select="field[@name='CREATE_DATE']/@value"/>
				</td>
				<td class="revData">
					<xsl:value-of select="field[@name='APPROVAL_DATE']/@value"/>
				</td>
				<td class="revData">
					<xsl:value-of select="field[@name='REV_INFO']/@value"/>
				</td>
			</tr>
		</xsl:for-each>
	</table>
</div>
</xsl:template>




<!--##################################################
    ##  REFERENCE_FILES                             ##
	################################################## -->
<xsl:template match="REFERENCE_FILES">
<xsl:if test="record">
	<div class="procedureStepRefFiles">
		<xsl:call-template name="putText"><xsl:with-param name="key">Reference Files:</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
		<br />
		<xsl:for-each select="record">
			<xsl:choose>
				<xsl:when test="contains(field[@name = 'SERVER_PATH']/@value,'JPG') or contains(field[@name = 'SERVER_PATH']/@value,'GIF')">
					<img class="procedurePicture">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/></xsl:attribute>
					</img>
				</xsl:when>
				<xsl:otherwise>
					<a target="_blank" class="normal">
						<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/></xsl:attribute>
						<img border="0">
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>document.gif</xsl:attribute>
							<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template></xsl:attribute>
						</img>
						<xsl:value-of select="field[@name = 'NAME']/@value"/>
						<xsl:if test="$stringEdit = 'yes'">
							<xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template>                    
	                   </xsl:if>
					</a>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
		</xsl:for-each>
	</div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  REFERENCE_PROCEDURES                       ##
	################################################## -->
<xsl:template match="REFERENCE_PROCEDURES">
<xsl:if test="record">
	<div class="procedureStepRefProcedures">
		<xsl:call-template name="putText"><xsl:with-param name="key">Related Procedures:</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
		<xsl:for-each select="record">
			<a>
				<xsl:choose>
					<xsl:when test="//content/@printable">
						<xsl:attribute name="href">viewProcedure.asp?objID=<xsl:value-of select="field[@name = 'PROC_OBJ_ID']/@value"/>&amp;printable=trueBro</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="href">viewProcedure.asp?objID=<xsl:value-of select="field[@name = 'PROC_OBJ_ID']/@value"/></xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>			
				<xsl:value-of select="field[@name='PROC_NAME']/@value"/>
			</a>
			<xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
		</xsl:for-each>
	</div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  ANSWER_PROCEDURE_POST_RECORD               ##
	################################################## -->
<xsl:template match="ANSWER_PROCEDURE_POST_RECORD">
<tr>
	<td class="procedurePostRecordColumn">
		<xsl:attribute name="colspan"><xsl:value-of select="count(./ancestor::obj[@type='resultSet']/columns/column) -1"/></xsl:attribute>
		<xsl:apply-templates />
	</td>
</tr>
</xsl:template>



<!--##################################################
    ##   PROCEDURE_COMMENT                          ##
	################################################## -->
<xsl:template match="PROCEDURE_COMMENT">
<xsl:if test="string-length(root/.) != 0">
<div class="procedureComment">
	<table class="procedureComment">
		<tr>
			<td class="procedureComment">
				<xsl:copy-of select="root" />
			</td>
		</tr>
	</table>
</div>
</xsl:if>


</xsl:template>




<!--##################################################
    ##  headerTable                                 ##
	################################################## -->
<xsl:template name="headerTable">
<table class="headTable">
	<tr>
		<td class="procedureHeaderTableLeft">
			<xsl:call-template name="putText"><xsl:with-param name="key">Security Clearance Level</xsl:with-param></xsl:call-template>
		</td>
		<td class="procedureHeaderTableRight"><xsl:call-template name="putText"><xsl:with-param name="key">
			<xsl:value-of select="../securityLevelData/record/field[@name='NAME']/@value"/></xsl:with-param></xsl:call-template>
			<xsl:text> </xsl:text>
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="securityLevelData/record/field[@name='NAME']/@value"/></xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:if test="../rolesAllowedToAccess/record">
		<tr>
			<td class="procedureHeaderTableLeft">
				<xsl:call-template name="putText"><xsl:with-param name="key">Roles Allowed EACCESS</xsl:with-param></xsl:call-template>
			</td>
			<td class="procedureHeaderTableRight">
				<xsl:for-each select="../rolesAllowedToAccess/record">
					<xsl:value-of select="field[@name='NAME']/@value"/><xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
				</xsl:for-each>
			</td>
		</tr>
	</xsl:if>
	<xsl:if test="//procedureObjects/record[field[@name='LABOR_ROLE']/@value='LABOR_OWNER']">
		<tr>
			<td class="procedureHeaderTableLeft">
				<xsl:call-template name="putText"><xsl:with-param name="key">Owner Role</xsl:with-param></xsl:call-template>
			</td>
			<td class="procedureHeaderTableRight">
				<xsl:value-of select="../procedureObjects/record[field[@name='LABOR_ROLE']/@value='LABOR_OWNER']/field[@name='OBJ_DESC']/@value" />
				<xsl:text> </xsl:text>(
				<xsl:value-of select="../procedureObjects/record[field[@name='LABOR_ROLE']/@value='LABOR_OWNER']/field[@name='DURATION']/@value" />
				<xsl:text> </xsl:text>
				<xsl:call-template name="putText"><xsl:with-param name="key">minutes</xsl:with-param></xsl:call-template> )
			</td>
		</tr>
	</xsl:if>
</table>
</xsl:template>

<!--##################################################
    ##  headerData                            ##
	################################################## -->
<xsl:template match="headerData">
	<table width="100%" class="tight">
		<tr>
			<td class="procedureHeaderTitle" style="text-align:left;width:70%;">
			<xsl:if test="string-length(../creatingDepartmentData/record/field[@name='LINKED_DOC_ID']/@value)">
				<img class="procedureLogo">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="../creatingDepartmentData/record/field[@name='LINKED_DOC_ID']/@value"/></xsl:attribute>
				</img>
				<br />
			</xsl:if>
			</td>
			<td style="text-align:right;width:30%">
				<table style="font-size:xx-small;" class="tight">
<!--					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='CO_NAME']/@value"/></td></tr>-->
<!--					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='LOC_NAME']/@value"/></td></tr>-->
					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='ADDRESS_1']/@value"/></td></tr>
					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='ADDRESS_2']/@value"/></td></tr>
					<tr><td>
					<xsl:value-of select="//creatingDepartmentData/record/field[@name='CITY']/@value"/>,<xsl:text> </xsl:text>
					<xsl:value-of select="//creatingDepartmentData/record/field[@name='STATE']/@value"/><xsl:text> </xsl:text>
					<xsl:value-of select="//creatingDepartmentData/record/field[@name='POSTAL_CODE']/@value"/>
					</td></tr>
<!--					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='COUNTRY']/@value"/></td></tr>-->
					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='PHONE']/@value"/></td></tr>
				</table>
			
			</td>
		</tr>
	</table>
	<table class="procedureHeader">
		<tr>
			<td class="procedureHeaderTitle">
				<table class="procedureTitle">
					<tr>
						<td class="procedureName">
							<xsl:value-of select="record/field[@name='NAME']/@value"/>
						</td>
					</tr>
					<tr>
						<td class="procedureRevInfo">
							<span>
								<xsl:attribute name="class">AP_STAT_<xsl:value-of select="record/field[@name = 'STATUS']/@value"/></xsl:attribute>
								<xsl:call-template name="putText"><xsl:with-param name="key">Procedure_View_<xsl:value-of select="record/field[@name='STATUS']/@value"/></xsl:with-param></xsl:call-template>
							</span>

							(<xsl:call-template name="putText"><xsl:with-param name="key">procedure number</xsl:with-param></xsl:call-template>
							<xsl:value-of select="record/field[@name='ROOT']/@value"/>,<xsl:text> </xsl:text>
							<xsl:call-template name="putText"><xsl:with-param name="key">rev number</xsl:with-param></xsl:call-template>
							<xsl:value-of select="record/field[@name='REV']/@value"/>)
						</td>
					</tr>
				</table>
				<div class="headerTable"><xsl:call-template name="headerTable" /></div>
			</td>
		</tr>
	</table>
</xsl:template>



</xsl:stylesheet>