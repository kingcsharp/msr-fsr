<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ##  ANSWER_PROCEDURE                            ##
	################################################## -->
<xsl:template match="ANSWER_PROCEDURE">
type=<xsl:value-of select="@type"/>
<xsl:choose>
	<xsl:when test="@type='shortProc'>
		<div class="shortProc">
		Short Procedure:
		</div>
	</xsl:when>
	<xsl:otherwise>
		<div class="procedure">
			<div class="assistantTable"><xsl:apply-templates select="headerData"/></div>
			<xsl:call-template name="assistantTable" />
			<xsl:call-template name="printPartsProvidedStay" />
			<xsl:call-template name="printPartsProvidedTake" />
			<div class="assistantTable"><xsl:apply-templates select="stepData/obj" /></div>
			<xsl:apply-templates select="revisionData" />
		</div>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>
<!--##################################################
    ##  revisionData                                ##
	################################################## -->
<xsl:template match="revisionData">
<div class="revData">
	<table class="revData">
			<tr>
				<th class="revData">Revision Num</th>
				<th class="revData">Revision Date</th>
				<th class="revData">Revision Comment</th>
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
					<xsl:value-of select="field[@name='REV_INFO']/@value"/>
				</td>
			</tr>
		</xsl:for-each>
	</table>
</div>
</xsl:template>


<!--##################################################
    ##  PROCEDURE_PRECEDING_STEP                    ##
	################################################## -->
<xsl:template match="PROCEDURE_PRECEDING_STEP">
	<xsl:for-each select="record">
	<xsl:if test="string-length(field[@name='PREV_STEP']/@value) != 0">
    	<xsl:value-of select="field[@name='COUNTER_VALUE']/@value"/><xsl:text> </xsl:text>
		<xsl:value-of select="field[@name='COUNTER_UNIT']/@value"/><xsl:text> </xsl:text>
		<xsl:choose>
			<xsl:when test="string-length(field[@name='REL_OR_ABS']/@value) != 0">
				<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='REL_OR_ABS']/@value"/></xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
				<xsl:call-template name="putText"><xsl:with-param name="key">toStep</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
			</xsl:when>
			<xsl:when test="string-length(field[@name='PREV_STEP']/@value) != 0">
				<xsl:call-template name="putText"><xsl:with-param name="key">after</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
			</xsl:when>
		</xsl:choose>
		<xsl:variable name="prevStep"><xsl:value-of select="field[@name='PREV_STEP']/@value"/></xsl:variable>
		<xsl:value-of select="./ancestor::data/record[field[@name='ID']/@value=$prevStep]/field[@name='actualCount']/@value"/>
		<xsl:text> </xsl:text>
		<xsl:choose>
			<xsl:when test="string-length(field[@name='FROM_START_OR_STOP']/@value) = 0">
				<xsl:call-template name="putText"><xsl:with-param name="key">FROM_STOP</xsl:with-param></xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="putText"><xsl:with-param name="key">FROM_<xsl:value-of select="field[@name='FROM_START_OR_STOP']/@value"/></xsl:with-param></xsl:call-template>
			</xsl:otherwise> 
		</xsl:choose>
	</xsl:if>
    </xsl:for-each>
</xsl:template>

<!--##################################################
    ##  REFERENCE_FILES                                           ##
	################################################## -->
<xsl:template match="REFERENCE_FILES">
<xsl:if test="record">
	<div class="procedureStepRefFiles">
		<xsl:for-each select="record">
			<xsl:choose>
				<xsl:when test="contains(field[@name = 'SERVER_PATH']/@value,'JPG') or contains(field[@name = 'SERVER_PATH']/@value,'GIF')">
					<img class="procedurePicture">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/></xsl:attribute>
					</img>
				</xsl:when>
				<xsl:otherwise>
					<a target="_blank" class="prodbutton">
						<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/></xsl:attribute>
						<img border="0">
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>document.gif</xsl:attribute>
							<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template></xsl:attribute>
						</img>					
						<div style="display:none" class="hiddenForWords">
							<xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template>                    
		                </div>
					</a>
				</xsl:otherwise>
			</xsl:choose>		
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
    ##   procedure_step_monitor                       ##
	################################################## -->
<xsl:template match="procedure_step_monitor">
<xsl:if test="./record">
<div class="procedureStepMonitors">
	<table class="procedureStepMonitors">
		<tr>
			<td class="procedureStepMonitors">
				<xsl:call-template name="putText"><xsl:with-param name="key">Monitors</xsl:with-param></xsl:call-template>
			</td>
			<td class="procedureStepMonitors">
				<xsl:for-each select="record">
					<xsl:value-of select="field[@name='DESCRIPTION']/@value"/><xsl:text> </xsl:text>
				</xsl:for-each>
			</td>
		</tr>
	</table>
</div>
</xsl:if>
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
				<xsl:call-template name="putText"><xsl:with-param name="key">Comments</xsl:with-param></xsl:call-template>
			</td>
			<td class="procedureComment">
				<xsl:copy-of select="root" />
			</td>
		</tr>
	</table>
</div>
</xsl:if>


</xsl:template>

<!--##################################################
    ##   procedure_step_labor                       ##
	################################################## -->
<xsl:template match="procedure_step_labor">
<xsl:if test="./record[field[@name='LABOR_TYPE']/@value='ASSISTANT']">
<div class="procedureStepAssistantRoles">
	<table class="procedureStepAssistantRoles">
		<tr>
			<td class="procedureStepAssistantRoles">
				Assistant Roles:
			</td>
			<td class="procedureStepAssistantRoles">
				<xsl:for-each select="record[field[@name='LABOR_TYPE']/@value='ASSISTANT']">
					<xsl:value-of select="field[@name='ROLE_NAME']/@value"/><xsl:text> </xsl:text>
					<xsl:call-template name="putText"><xsl:with-param name="key">for</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
					<xsl:value-of select="field[@name='DURATION']/@value"/><xsl:text> </xsl:text>
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='DURATION_TYPE_NAME']/@value"/></xsl:with-param></xsl:call-template>
					<xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
				</xsl:for-each>
			</td>
		</tr>
	</table>
</div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  printPartsProvidedTake                  ##
	################################################## -->
<xsl:template name="printPartsProvidedTake">
<xsl:if test="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_TAKE_BACK']">
<div class="partTable">
<div class="partTableTitle">
	<xsl:call-template name="putText"><xsl:with-param name="key">Parts Provided and Taken Back</xsl:with-param></xsl:call-template>
</div>
<table class="partTable">
	<tr>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Type</xsl:with-param></xsl:call-template>
		</td>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Name</xsl:with-param></xsl:call-template>
		</td>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Minutes</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_TAKE_BACK']">
		<tr>
			<td class="partTableData"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='OBJ_TABLE']/@value"/></xsl:with-param></xsl:call-template></td>
			<td class="partTableData"><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td class="partTableData"><xsl:value-of select="field[@name='DURATION']/@value"/></td>
		</tr>
	</xsl:for-each>
</table>
</div>
</xsl:if>

</xsl:template>

<!--##################################################
    ##  printPartsProvidedStay                      ##
	################################################## -->
<xsl:template name="printPartsProvidedStay">
<xsl:if test="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_STAY']">
<div class="partTableTitle">
	<xsl:call-template name="putText"><xsl:with-param name="key">Parts Provided that Stay</xsl:with-param></xsl:call-template>
</div>
<div class="partTable">
<table class="partTable">
	<tr>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Type</xsl:with-param></xsl:call-template>
		</td>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Name</xsl:with-param></xsl:call-template>
		</td>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Qty</xsl:with-param></xsl:call-template>
		</td>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Ordering Unit</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_STAY']">
		<tr>
			<td class="partTableData"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='OBJ_TABLE']/@value"/></xsl:with-param></xsl:call-template></td>
			<td class="partTableData"><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td class="partTableData"><xsl:value-of select="field[@name='QTY']/@value"/></td>
			<td class="partTableData"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></td>
		</tr>
	</xsl:for-each>
</table>
</div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  assistantTable                             ##
	################################################## -->
<xsl:template name="assistantTable">
<xsl:if test="procedureLabor/record[field[@name='LABOR_TYPE']/@value='ASSISTANT']">
<div class="assistantTable">
<table class="procedureAssistantTable">
	<tr>
		<td class="procedureAssistantTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Role</xsl:with-param></xsl:call-template>
		</td>
		<td class="procedureAssistantTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Minutes</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureLabor/record[field[@name='LABOR_TYPE']/@value='ASSISTANT']">
		<tr>
			<td class="procedureAssistantData"><xsl:value-of select="field[@name='ROLE_NAME']/@value"/></td>
			<td class="procedureAssistantData"><xsl:value-of select="field[@name='DURATION']/@value"/></td>
		</tr>
	</xsl:for-each>
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
		<td class="procedureHeaderTableRight">
			<xsl:value-of select="../securityLevelData/record/field[@name='ID']/@value"/><xsl:text> </xsl:text>
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
					<xsl:value-of select="field[@name='ROLE_NAME']/@value"/><xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
				</xsl:for-each>
			</td>
		</tr>
	</xsl:if>
	<xsl:if test="../procedureLabor/record[field[@name='LABOR_TYPE']/@value='OWNER']/field[@name='ROLE_NAME']/@value">
		<xsl:if test="string-length(../procedureLabor/record[field[@name='LABOR_TYPE']/@value='OWNER']/field[@name='ROLE_NAME']/@value) != 0">
		<tr>
			<td class="procedureHeaderTableLeft">
				<xsl:call-template name="putText"><xsl:with-param name="key">Owner Role</xsl:with-param></xsl:call-template>
			</td>
			<td class="procedureHeaderTableRight">
				<xsl:value-of select="../procedureLabor/record[field[@name='LABOR_TYPE']/@value='OWNER']/field[@name='ROLE_NAME']/@value" />
			</td>
		</tr>
		</xsl:if>
		<xsl:if test="string-length(../procedureLabor/record[field[@name='LABOR_TYPE']/@value='OWNER']/field[@name='DURATION']/@value) != 0">
		<tr>
			<td class="procedureHeaderTableLeft">
				<xsl:call-template name="putText"><xsl:with-param name="key">Minutes</xsl:with-param></xsl:call-template>
			</td>
			<td class="procedureHeaderTableRight">
				<xsl:value-of select="../procedureLabor/record[field[@name='LABOR_TYPE']/@value]/field[@name='DURATION']/@value" />
			</td>
		</tr>
		</xsl:if>
	</xsl:if>
</table>
</xsl:template>

<!--##################################################
    ##  headerData                            ##
	################################################## -->
<xsl:template match="headerData">
	<table class="procedureHeader">
		<tr>
			<td class="procedureHeaderTitle">
				<table class="procedureTitle">
					<tr>
						<td class="procedureName">
							<xsl:value-of select="record/field[@name='NAME']/@value"/>
						</td>
						<td class="procedureRevInfo">
							(<xsl:call-template name="putText"><xsl:with-param name="key">procedure number</xsl:with-param></xsl:call-template>
							<xsl:value-of select="record/field[@name='ROOT']/@value"/>,<xsl:text> </xsl:text>
							<xsl:call-template name="putText"><xsl:with-param name="key">rev number</xsl:with-param></xsl:call-template>
							<xsl:value-of select="record/field[@name='REV']/@value"/>)
						</td>
					</tr>
				</table>
				<div class="headerTable"><xsl:call-template name="headerTable" /></div>
			</td>
			<td class="procedureHeaderTitle" style="text-align:right">
			<xsl:if test="string-length(../creatingCompanyData/record/field[@name='LOGO_ID']/@value)">
				<img class="procedureLogo">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="../creatingCompanyData/record/field[@name='LOGO_ID']/@value"/></xsl:attribute>
				</img>
				<br />
			</xsl:if>
				<span>
					<xsl:attribute name="class">AP_STAT_<xsl:value-of select="record/field[@name = 'STATUS']/@value"/></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">Procedure_View_<xsl:value-of select="record/field[@name='STATUS']/@value"/></xsl:with-param></xsl:call-template>
				</span>
			</td>
		</tr>
	</table>
</xsl:template>





</xsl:stylesheet>