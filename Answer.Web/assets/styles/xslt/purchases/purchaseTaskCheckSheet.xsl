<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ## TSR			                                ##
	################################################## -->
<xsl:template match="PURCHASE_TASK_CHECK_SHEET">
<xsl:for-each select="PURCHASE">
	<xsl:call-template name="putPurchase" />
</xsl:for-each>

</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template name="putPurchase">
<div>
<xsl:call-template name="putText"><xsl:with-param name="key">Check Sheet for purchase:</xsl:with-param></xsl:call-template><xsl:value-of select="ID"/>
<xsl:for-each select="PURCHASE_ITEM">
	<xsl:call-template name="putPurchaseItem" />
</xsl:for-each>
</div>
</xsl:template>

<!--##################################################
    ##  putOverAllStatUpdater                             ##
	################################################## -->
<xsl:template name="putOverAllStatUpdater">
<xsl:param name="taskID"/>
<div>
	<table>
		<tr>
			<td><xsl:call-template name="putText"><xsl:with-param name="key">setAllTasks</xsl:with-param></xsl:call-template></td>
			<td><xsl:attribute name="ID">OVERALL___<xsl:value-of select="$taskID"/></xsl:attribute></td>
		</tr>
	</table>
</div>
<script>ajaxReplace('../purchases/ajax/getPurchaseItemOverAllDropDown.asp','PURCH_ITEM_ID=<xsl:value-of select="ID"/>','OVERALL___<xsl:value-of select="$taskID"/>');</script>
</xsl:template>


<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template name="putPurchaseItem">
<div>
<xsl:call-template name="putText"><xsl:with-param name="key">Purchase Item:</xsl:with-param></xsl:call-template>
<xsl:value-of select="ID"/>
<br />
<xsl:call-template name="putOverAllStatUpdater">
</xsl:call-template>


<table class="standard">
	<tr>
		<th class="standard">Task ID</th>
		<th class="standard">Serial</th>
		<th class="standard">Part Desc</th>
		<th class="standard">Location</th>
		<th class="standard">Progress</th>
		
	</tr>
	<xsl:for-each select="TASK">
		<xsl:sort select="OBJECT/SERIAL" />
		<xsl:call-template name="putTask" />
	</xsl:for-each>

</table>
</div>
</xsl:template>

<!--##################################################
    ##  putTask                                     ##
	################################################## -->
<xsl:template name="putTask">
<xsl:variable name="myID"><xsl:value-of select="ID"/></xsl:variable>
<xsl:for-each select="OBJECT">
	<xsl:call-template name="putObject" >
		<xsl:with-param name="taskID"><xsl:value-of select="$myID"/></xsl:with-param>
	</xsl:call-template>
</xsl:for-each>
</xsl:template>


<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template name="putObject">
<xsl:param name="taskID" />
<tr>
	<td class="standard"><nobr><xsl:value-of select="$taskID"/></nobr></td>
	<td class="standard"><nobr><xsl:value-of select="SERIAL"/></nobr></td>
	<td class="standard"><nobr><xsl:value-of select="PART_DESC"/></nobr></td>
	<td class="standard"><div><xsl:attribute name="ID">LOCATION___<xsl:value-of select="ID"/></xsl:attribute><nobr><xsl:value-of select="LOCATION_NAME"/></nobr></div></td>
	<td class="standard"><xsl:call-template name="putProgressCode"><xsl:with-param name="taskID"><xsl:value-of select="$taskID"/></xsl:with-param></xsl:call-template></td>
</tr>
</xsl:template>

<!--##################################################
    ##  putProgressCode                             ##
	################################################## -->
<xsl:template name="putProgressCode">
<xsl:param name="taskID"/>
<div>
	<xsl:attribute name="ID">TASK_STATUS___<xsl:value-of select="$taskID"/></xsl:attribute>
</div>
<script>ajaxReplace('../actualTasks/ajax/xmlGetGrouperStatus.asp','TASK_ID=<xsl:value-of select="$taskID"/>','TASK_STATUS___<xsl:value-of select="$taskID"/>');</script>

</xsl:template>

</xsl:stylesheet>
