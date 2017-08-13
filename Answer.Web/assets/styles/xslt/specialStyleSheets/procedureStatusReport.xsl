<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ##                                              ##
	################################################## -->
<xsl:template match="procedureProgressReport">
<table class="standard">
	<tr><td class="standard KPIFailing" style="background-color:#F7B7B9;border: thin ridge;"><xsl:call-template name="putText"><xsl:with-param name="key">OverDue</xsl:with-param></xsl:call-template></td></tr>
	<tr><td class="standard KPIWarning" style="background-color:#FFFC9E;border: thin ridge;"><xsl:call-template name="putText"><xsl:with-param name="key">Due Now</xsl:with-param></xsl:call-template></td></tr>
	<tr><td class="standard KPINormal" style="background-color:white;border: thin ridge;"><xsl:call-template name="putText"><xsl:with-param name="key">Future Task</xsl:with-param></xsl:call-template></td></tr>
	<tr><td class="standard KPIInSpec" style="background-color:#A4F2BC;border: thin ridge;"><xsl:call-template name="putText"><xsl:with-param name="key">Completed</xsl:with-param></xsl:call-template></td></tr>
	
</table>
<div class="procedureProgressReport">
<xsl:value-of select="curDate"/>-Procedure Progress Report:<br />
<xsl:call-template name="createTable" />
</div>
</xsl:template>

<!--##################################################
    ##  createTable                                 ##
	################################################## -->
<xsl:template name="createTable">
<table class="procProgRep">
	<tr>
		<td class="procProgRep" rowspan="2">
		</td>
		<td class="procProgRep">
		Employee
		</td>
		<xsl:call-template name="getEmployeeColumns">
			<xsl:with-param name="root" select="."></xsl:with-param>
        </xsl:call-template>
	</tr>
	<tr>
		<td class="procProgRep">
		shift
		</td>
		<xsl:call-template name="getShiftColumns">
			<xsl:with-param name="root" select="."></xsl:with-param>
        </xsl:call-template>
	</tr>
	<xsl:call-template name="getTrainingRows">
		<xsl:with-param name="root" select="."></xsl:with-param>
		
	</xsl:call-template>
</table>
</xsl:template>

<!--##################################################
    ##  getEmployeeColumns                         ##
	################################################## -->
<xsl:template name="getEmployeeColumns">
<xsl:param name="root" />
<xsl:for-each select="assignees/id">
	<xsl:variable name="myID"><xsl:value-of select="."/></xsl:variable>
	<td class="procProgRep"><xsl:value-of select="$root/tasks/data/record[ASSIGNEE_ID = $myID]/ASSIGNEE_NAME"/></td>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  getShiftColumns                         ##
	################################################## -->
<xsl:template name="getShiftColumns">
<xsl:param name="root" />
<xsl:for-each select="assignees/id">
	<td class="procProgRep"></td>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  getTrainingRows                             ##
	################################################## -->
<xsl:template name="getTrainingRows">
<xsl:param name="root" />
<xsl:for-each select="$root/tasks/data/record[not(preceding-sibling::record/REPORT___NAME = REPORT___NAME)]">
	<xsl:variable name="REPORT_NAME"><xsl:value-of select="REPORT___NAME"/></xsl:variable>
	<xsl:for-each select="$root/procedureSteps/step">
		<xsl:if test="$root/tasks/data/record[REPORT___NAME=$REPORT_NAME]/PROCEDURE_STEP_ID = STEP_ID">
		<xsl:variable name="procStep" select="." />
		<tr>
			<td class="procProgRep"><nobr><xsl:value-of select="$REPORT_NAME"/></nobr></td>
			<td class="procProgRep"><div style="width:20em;height:2.5em;overflow:auto;"><xsl:copy-of select="STEP_TEXT"/></div></td>
			<xsl:for-each select="$root/assignees/id">
				<xsl:variable name="ASSIGNEE_ID"><xsl:value-of select="."/></xsl:variable>
				<td>
					<xsl:call-template name="printTaskData">
						<xsl:with-param name="myTask" select="$root/tasks/data/record[ASSIGNEE_ID = $ASSIGNEE_ID and PROCEDURE_STEP_ID = $procStep/STEP_ID]"/>
						<xsl:with-param name="root" select="$root"/>
                    </xsl:call-template>
				</td>
			</xsl:for-each>
		</tr>
		</xsl:if>
	</xsl:for-each>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  printTaskData                               ##
	################################################## -->
<xsl:template name="printTaskData">
<xsl:param name="myTask"/>
<xsl:param name="root"/>
<xsl:choose>
	<xsl:when test="$myTask/STATUS = 'CLOSED'">
		<xsl:attribute name="class">procProgRep KPIInSpec</xsl:attribute>
		<xsl:attribute name="style">background-color:#A4F2BC;border: thin ridge;</xsl:attribute>
		<nobr><xsl:value-of select="$myTask/ACTUAL_STOP_DATE"/></nobr>
	</xsl:when>
	<xsl:otherwise>
		<xsl:choose>
			<xsl:when test="$myTask/CUR_PLANNED_STOP_DATE/@num &gt; $root/curDate/@num and $myTask/CUR_PLANNED_START_DATE/@num &lt; $root/curDate/@num">
				<xsl:attribute name="class">procProgRep KPIWarning</xsl:attribute>
				<xsl:attribute name="style">background-color:#FFFC9E;border: thin ridge;</xsl:attribute>
				<xsl:value-of select="$myTask/CUR_PLANNED_START_DATE"/>-
				<xsl:value-of select="$myTask/CUR_PLANNED_STOP_DATE"/>
			</xsl:when>
			<xsl:when test="$myTask/CUR_PLANNED_STOP_DATE/@num &lt; $root/curDate/@num">
				<xsl:attribute name="class">procProgRep KPIFailing</xsl:attribute>
				<xsl:attribute name="style">background-color:#F7B7B9;border: thin ridge;</xsl:attribute>
				<xsl:value-of select="$myTask/CUR_PLANNED_START_DATE"/>-
				<xsl:value-of select="$myTask/CUR_PLANNED_STOP_DATE"/>
			</xsl:when>
			<xsl:when test="$myTask/CUR_PLANNED_STOP_DATE/@num &gt; $root/curDate/@num and $myTask/CUR_PLANNED_START_DATE/@num &gt; $root/curDate/@num">
				<xsl:attribute name="class">procProgRep KPINormal</xsl:attribute>
				<xsl:attribute name="style">background-color:white;border: thin ridge;</xsl:attribute>
				<xsl:value-of select="$myTask/CUR_PLANNED_START_DATE"/>-
				<xsl:value-of select="$myTask/CUR_PLANNED_STOP_DATE"/>
			</xsl:when>
			<xsl:otherwise>
				??
				<xsl:value-of select="$myTask/ID"/>-
				<xsl:value-of select="$myTask/CUR_PLANNED_START_DATE"/>-
				<xsl:value-of select="$myTask/CUR_PLANNED_STOP_DATE"/>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:if test="not($myTask/ASSIGNEE_NAME = $myTask/LATEST_REQUESTEE_NAME)">
			<xsl:text> </xsl:text><xsl:value-of select="LATEST_REQUESTEE_NAME"/>
		</xsl:if>
			
	</xsl:otherwise>
</xsl:choose>	

</xsl:template>


</xsl:stylesheet>