<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:variable
    name="path_to_top"
    select="expression">
  </xsl:variable>

  <xsl:variable
    name="path_to_images"
    select="expression">
  </xsl:variable>
  <xsl:variable
    name="stringEdit"
    select="expression">
  </xsl:variable>
<!--##################################################
    ##  ANSWER_PROCEDURE                            ##
	################################################## -->
<xsl:template match="ANSWER_PROCEDURE">
<xsl:choose>
	<xsl:when test="@type='shortProc'">
		<div class="shortProc">
			<xsl:call-template name="smallHeader" />
			<xsl:apply-templates select="stepData/obj" />
		</div>
	</xsl:when>
	<xsl:otherwise>
		<div class="procedure">
			<div class="assistantTable"><xsl:apply-templates select="headerData"/></div>
			<xsl:call-template name="printLaborProvidedAndTakenBack" />
			<xsl:call-template name="printPartsProvidedStay" />
			<xsl:call-template name="printPartsProvidedTake" />
			<xsl:call-template name="printPartsProvidedConsumed" />
			<xsl:call-template name="printComments">
				<xsl:with-param name="LOC">1-Header</xsl:with-param>
      </xsl:call-template>
			<xsl:apply-templates select="headerData/REFERENCE_FILES"/>
			<xsl:apply-templates select="headerData/HEADER_REFERENCE_THEORIES"/>
			<xsl:apply-templates select="headerData/procedure_step_monitor"/>
			<div class="assistantTable">
				<xsl:apply-templates select="stepData/obj" />
			</div>
			<xsl:call-template name="printComments">
				<xsl:with-param name="LOC">2-Footer</xsl:with-param>
			</xsl:call-template>
			<xsl:apply-templates select="revisionData" />
		</div>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ##  smallHeader                                 ##
	################################################## -->
<xsl:template name="smallHeader">
<div class="subTitle"><xsl:value-of select="headerData/record/field[@name='NAME']/@value"/><xsl:text> </xsl:text>
<xsl:call-template name="putText"><xsl:with-param name="key">Revision:</xsl:with-param></xsl:call-template>
<xsl:value-of select="headerData/record/field[@name='REV']/@value"/>
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
				<th class="revData"><xsl:call-template name="putText"><xsl:with-param name="key">Reviser</xsl:with-param></xsl:call-template></th>
				<th class="revData"><xsl:call-template name="putText"><xsl:with-param name="key">WorkFlow</xsl:with-param></xsl:call-template></th>
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
				<td class="revData">
					<xsl:value-of select="field[@name='CREATOR_NAME']/@value"/>
				</td>
				<td class="revData">
					<xsl:value-of select="field[@name='WF_NAME']/@value"/>
				</td>
			</tr>
		</xsl:for-each>
	</table>
</div>
</xsl:template>

<!--##################################################
    ##  PROCEDURE_STEP_LABOR                        ##
	################################################## -->
<xsl:template match="PROCEDURE_STEP_LABOR">
<xsl:for-each select="record[field[@name='LABOR_ROLE']/@value='LABOR_OWNER']">
	<table class="procedureStepMonitors">
		<tr>
			<td><xsl:call-template name="putText"><xsl:with-param name="key">Step Owner Role</xsl:with-param></xsl:call-template></td>
			<td><xsl:value-of select="field[@name='ROLE_NAME']/@value"/></td>
			<td><xsl:value-of select="field[@name='QTY']/@value"/> <xsl:text> </xsl:text> <xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template></td>
			
		</tr>
	</table>
</xsl:for-each>

<xsl:for-each select="record[field[@name='LABOR_ROLE']/@value='LABOR_ASSISTANT']">
	<table class="procedureStepMonitors">
		<tr>
			<td><xsl:call-template name="putText"><xsl:with-param name="key">Step Assistant Role</xsl:with-param></xsl:call-template></td>
			<td><xsl:value-of select="field[@name='ROLE_NAME']/@value"/></td>
			<td><xsl:value-of select="field[@name='QTY']/@value"/> <xsl:text> </xsl:text> <xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template></td>
			
		</tr>
	</table>
</xsl:for-each>

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
						<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/>&amp;width=400</xsl:attribute>
					</img>
				</xsl:when>
				<xsl:otherwise>
					<a target="_blank" class="normal">
						<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'DOC_ID']/@value"/></xsl:attribute>
						<img border="0">
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>document.gif</xsl:attribute>
							<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template></xsl:attribute>
						</img>
						<xsl:value-of select="field[@name = 'NAME']/@value"/>
						<div style="display:none" class="hiddenForWords">
							<xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template>                    
						</div>
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
					<xsl:when test="/Doc_Webpage/content/@printable">
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
    ##  REFERENCE_THEORIES                       ##
	################################################## -->
<xsl:template match="REFERENCE_THEORIES">
<xsl:if test="record">
	<div class="procedureStepRefProcedures">
		<xsl:call-template name="putText"><xsl:with-param name="key">Related Theories:</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
		<xsl:for-each select="record">
			<a>
				<xsl:if test="../@target"><xsl:attribute name="target"><xsl:value-of select="../@target"/></xsl:attribute></xsl:if>
				<xsl:choose>
					<xsl:when test="/Doc_Webpage/content/@printable">
						<xsl:attribute name="href">../theory/viewTheory.asp?objID=<xsl:value-of select="field[@name = 'THEORY_OBJ_ID']/@value"/>&amp;ID=<xsl:value-of select="field[@name = 'THEORY_ID']/@value"/>&amp;printable=trueBro</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="href">../theory/viewTheory.asp?objID=<xsl:value-of select="field[@name = 'THEORY_OBJ_ID']/@value"/>&amp;ID=<xsl:value-of select="field[@name = 'THEORY_ID']/@value"/></xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:choose>
					<xsl:when test="string-length(field[@name='APPROVED_NAME']/@value) &gt; 0">				
						<xsl:value-of select="field[@name='APPROVED_NAME']/@value"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="field[@name='THEORY_NAME']/@value"/>
					</xsl:otherwise>
				</xsl:choose>
			</a>
			<xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
		</xsl:for-each>
	</div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  HEADER_REFERENCE_THEORIES                       ##
	################################################## -->
<xsl:template match="HEADER_REFERENCE_THEORIES">
<xsl:if test="record">
	<div class="procedureStepRefProcedures">
		<xsl:call-template name="putText"><xsl:with-param name="key">Related Theories:</xsl:with-param></xsl:call-template><br />
		<xsl:for-each select="record">
			<a>
				<xsl:if test="../@target"><xsl:attribute name="target"><xsl:value-of select="../@target"/></xsl:attribute></xsl:if>
				<xsl:choose>
					<xsl:when test="/Doc_Webpage/content/@printable">
						<xsl:attribute name="href">../theory/viewTheory.asp?ID=<xsl:value-of select="field[@name = 'LINKED_THEORY_ID']/@value"/>&amp;printable=trueBro</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="href">../theory/viewTheory.asp?ID=<xsl:value-of select="field[@name = 'LINKED_THEORY_ID']/@value"/></xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>			
				<xsl:value-of select="field[@name='LINKED_THEORY_NAME']/@value"/>
			</a>
			<xsl:if test="position()!=last()"><br /><xsl:text> </xsl:text></xsl:if>
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
					<xsl:call-template name="printAMonitor">
						<xsl:with-param name="o" select="." />
					</xsl:call-template>
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
<xsl:for-each select="./record[field[@name='RELATIONSHIP']/@value='LABOR_PROVIDE_TAKE_BACK']">
	<table class="procedureStepAssistantRoles">
		<tr>
			<td><xsl:call-template name="putText"><xsl:with-param name="key">LABOR_ROLE_<xsl:value-of select="field[@name='LABOR_ROLE']/@value"/></xsl:with-param></xsl:call-template></td>
			<td><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td><xsl:value-of select="field[@name='QTY']/@value"/></td>
			<td><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template></td>
		</tr>
	</table>
</xsl:for-each>
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
		<td class="partTableTitle" colspan="2">
			<xsl:call-template name="putText"><xsl:with-param name="key">Time Needed</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_TAKE_BACK']">
		<tr>
			<td class="partTableData"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='OBJ_TABLE']/@value"/></xsl:with-param></xsl:call-template></td>
			<td class="partTableData"><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td class="partTableData" style="text-align:right;border-right:none;"><xsl:value-of select="field[@name='QTY']/@value"/></td>
			<td class="partTableData" style="border-left:none;">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template>
			</td>
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
		<td class="partTableTitle" colspan="2">
			<xsl:call-template name="putText"><xsl:with-param name="key">Qty</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_STAY']">
		<tr>
			<td class="partTableData"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='OBJ_TABLE']/@value"/></xsl:with-param></xsl:call-template></td>
			<td class="partTableData"><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td class="partTableData" style="text-align:right;border-right:none;"><xsl:value-of select="field[@name='QTY']/@value"/></td>
			<td class="partTableData" style="border-left:none;">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template>
			</td>
		</tr>
	</xsl:for-each>
</table>
</div>
</xsl:if>
</xsl:template>


<!--##################################################
    ##  printPartsProvidedAndConsumed               ##
	################################################## -->
<xsl:template name="printPartsProvidedConsumed">
<xsl:if test="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_CONSUMED']">
<div class="partTableTitle">
	<xsl:call-template name="putText"><xsl:with-param name="key">Parts Provided that are Consumed</xsl:with-param></xsl:call-template>
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
		<td class="partTableTitle" colspan="2">
			<xsl:call-template name="putText"><xsl:with-param name="key">Qty</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureObjects/record[field[@name='RELATIONSHIP']/@value='PARTS_PROVIDE_CONSUMED']">
		<tr>
			<td class="partTableData"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='OBJ_TABLE']/@value"/></xsl:with-param></xsl:call-template></td>
			<td class="partTableData"><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td class="partTableData" style="text-align:right;border-right:none;"><xsl:value-of select="field[@name='QTY']/@value"/></td>
			<td class="partTableData" style="border-left:none;">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template>
			</td>
		</tr>
	</xsl:for-each>
</table>
</div>
</xsl:if>
</xsl:template>


<!--##################################################
    ##  printPartsLaborProvidedAndTakenBack         ##
	################################################## -->
<xsl:template name="printLaborProvidedAndTakenBack">

<xsl:if test="procedureObjects/record[field[@name='LABOR_ROLE']/@value='LABOR_ASSISTANT']">
<div class="partTableTitle">
	<xsl:call-template name="putText"><xsl:with-param name="key">Assistants</xsl:with-param></xsl:call-template>
</div>
<div class="partTable">
<table class="partTable">
	<tr>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">LABOR ROLE</xsl:with-param></xsl:call-template>
		</td>
		<td class="partTableTitle">
			<xsl:call-template name="putText"><xsl:with-param name="key">Duration</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="procedureObjects/record[field[@name='LABOR_ROLE']/@value='LABOR_ASSISTANT']">
		<tr>
			<td class="partTableData"><xsl:value-of select="field[@name='OBJ_DESC']/@value"/></td>
			<td class="partTableData" style="text-align:right;border-right:none;"><xsl:value-of select="field[@name='QTY']/@value"/></td>
			<td class="partTableData" style="border-left:none;">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template>
			</td>
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
					<xsl:value-of select="field[@name='ROLE_NAME']/@value"/><xsl:if test="position()!=last()">,<xsl:text> </xsl:text></xsl:if>
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
			</td>
			<td class="procedureHeaderTableRight">
				<xsl:value-of select="../procedureObjects/record[field[@name='LABOR_ROLE']/@value='LABOR_OWNER']/field[@name='QTY']/@value" />
				<xsl:text> </xsl:text>
				<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name='QTY_TYPE']/@value"/></xsl:with-param></xsl:call-template> )
			</td>
		</tr>
	</xsl:if>
</table>
</xsl:template>

<!--##################################################
    ##  headerData                            ##
	################################################## -->
<xsl:template match="headerData">
<table>
	<tr>
		<td>
	<table width="100%" class="tight">
		<tr>
			<td class="procedureHeaderTitle" style="text-align:left;width:70%;">
			<xsl:if test="string-length(../creatingDepartmentData/record/field[@name='LINKED_DOC_ID']/@value)">
				<img class="procedureLogo" width="80px" height="20px">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="../creatingDepartmentData/record/field[@name='LINKED_DOC_ID']/@value"/>&amp;width=103&amp;height=36</xsl:attribute>
				</img>
				<br />
			</xsl:if>
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
		
		</td>
		<td>
				<table style="font-size: x-small;" class="tight">
					<tr>
						<td colspan="2">
							<img class="procedureLogo">
								<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="WF_INFO/record/field[@name='STAMP_ID']/@value"/>&amp;width=103&amp;height=36</xsl:attribute>
							</img>
						</td>
					</tr>
					<tr>
						<td><nobr><xsl:call-template name="putText"><xsl:with-param name="key">Approved By:</xsl:with-param></xsl:call-template></nobr></td>
						<td><nobr><xsl:value-of select="WF_INFO/record/field[@name='NAME']/@value"/></nobr></td>
					</tr>
				</table>		
				<table style="font-size: x-small;" class="tight">
					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='CO_NAME']/@value"/></td></tr>
<!--					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='LOC_NAME']/@value"/></td></tr>-->
					<tr><td><xsl:value-of select="../creatingDepartmentData/record/field[@name='ADDRESS_1']/@value"/></td></tr>
						<tr><td><xsl:value-of select="../creatingDepartmentData/record/field[@name='ADDRESS_2']/@value"/></td></tr>
					<tr><td>
					<xsl:value-of select="../creatingDepartmentData/record/field[@name='CITY']/@value"/>,<xsl:text> </xsl:text>
					<xsl:value-of select="../creatingDepartmentData/record/field[@name='STATE']/@value"/><xsl:text> </xsl:text>
					<xsl:value-of select="../creatingDepartmentData/record/field[@name='POSTAL_CODE']/@value"/>
					</td></tr>
<!--					<tr><td><xsl:value-of select="//creatingDepartmentData/record/field[@name='COUNTRY']/@value"/></td></tr>-->
					<tr><td><xsl:value-of select="../creatingDepartmentData/record/field[@name='PHONE']/@value"/></td></tr>
				</table>
		
		</td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  printAMonitor                               ##
	################################################## -->
<xsl:template name="printAMonitor">
<xsl:param name="o"/>
<table class="procedureStepMonitors">
	<tr>
		<td class="procedureStepMonitors">
			<xsl:value-of select="$o/field[@name='DESCRIPTION']/@value"/>
		</td>
		<td class="procedureStepMonitors">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$o/field[@name='SHOULD_BE']/@value"/></xsl:with-param></xsl:call-template>
		</td>
		<td class="procedureStepMonitors">
			<xsl:value-of select="$o/field[@name='TARGET']/@value"/>
		</td>
	</tr>
</table>
</xsl:template>

  <!--##################################################
    ## putText                                      ##
	################################################## -->
  <xsl:template name="putText">
    <xsl:param name="key" />
    <xsl:param name="nobr" />
    <xsl:if test="$key != ''">
      <xsl:choose>
        <xsl:when test="$stringEdit = 'yes'">
          <EditableString>
            <xsl:attribute name="key">
              <xsl:value-of select="$key"/>
            </xsl:attribute>
            <xsl:attribute name="filename">
              <xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/../../@filename"/>
            </xsl:attribute>
            <xsl:choose>
              <xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]">
                <xsl:attribute name="value">
                  <xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/>
                </xsl:attribute>
              </xsl:when>
              <xsl:otherwise>
                <xsl:attribute name="value"></xsl:attribute>
              </xsl:otherwise>
            </xsl:choose>
          </EditableString>
        </xsl:when>
        <xsl:otherwise>
          <xsl:choose>
            <xsl:when test="$nobr = 'true'">
              <nobr>
                <xsl:choose>
                  <xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]">
                    <xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="$key"/>***<xsl:if test="$stringEdit = 'yes'">
                      <stringMissing>
                        <xsl:attribute name="id">
                          <xsl:value-of select="$key"/>
                        </xsl:attribute>
                      </stringMissing>
                    </xsl:if>
                  </xsl:otherwise>
                </xsl:choose>
              </nobr>
            </xsl:when>
            <xsl:otherwise>
              <xsl:choose>
                <xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]">
                  <xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="$key"/>***<xsl:if test="$stringEdit = 'yes'">
                    <stringMissing>
                      <xsl:attribute name="id">
                        <xsl:value-of select="$key"/>
                      </xsl:attribute>
                    </stringMissing>
                  </xsl:if>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>