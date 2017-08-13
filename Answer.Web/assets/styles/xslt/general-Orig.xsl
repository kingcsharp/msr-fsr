<xsl:stylesheet	version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" output="XML">

<!--##################################################
    ##  languageStrings                             ##
	## This is so this node does not print anything ##
	################################################## -->
<xsl:template match="languageStrings"></xsl:template>

<!--##################################################
    ##  texter                                           ##
	################################################## -->
<xsl:template match="texter">
<html>
	<xsl:apply-templates />
</html>
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
					<xsl:attribute name="key"><xsl:value-of select="$key"/></xsl:attribute>
					<xsl:attribute name="filename"><xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/../../@filename"/></xsl:attribute>
					<xsl:choose>	
						<xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]"><xsl:attribute name="value"><xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/></xsl:attribute></xsl:when>
						<xsl:otherwise><xsl:attribute name="value"></xsl:attribute></xsl:otherwise>
					</xsl:choose>					
				</EditableString>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$nobr = 'true'">
						<nobr>
							<xsl:choose>
								<xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]"><xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/></xsl:when>
								<xsl:otherwise><xsl:value-of select="$key"/>***<xsl:if test="$stringEdit = 'yes'"><stringMissing><xsl:attribute name="id"><xsl:value-of select="$key"/></xsl:attribute></stringMissing></xsl:if></xsl:otherwise>
							</xsl:choose>
						</nobr>
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]"><xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/></xsl:when>
							<xsl:otherwise><xsl:value-of select="$key"/>***<xsl:if test="$stringEdit = 'yes'"><stringMissing><xsl:attribute name="id"><xsl:value-of select="$key"/></xsl:attribute></stringMissing></xsl:if></xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:if>
</xsl:template>

<!--##################################################
    ##  javaScript                                  ##
	################################################## -->
<xsl:template match="javaScript">
<xsl:if test="not(/Doc_Webpage/@email = 'true')">
	<script language="JavaScript">
		<xsl:value-of select="."/>
	</script>
</xsl:if>
</xsl:template>

<!--##################################################
    ## SendDocumentInfo                             ##
	################################################## -->
<xsl:template match="SendDocumentInfo">
<!--function AddToOptionBox(strForm,strField,strVal,strShow,mult)-->
<xsl:if test="not(/Doc_Webpage/@email = 'true')">
<script language="JavaScript">
	window.opener.AddToOptionBox(
	'<xsl:value-of select="@Form"/>',
	'<xsl:value-of select="@Field"/>',
	'<xsl:value-of select="@Value"/>',
	jXMLDecode('<xsl:value-of select="@Show"/>'),
	'<xsl:value-of select="@Multiple"/>');
	window.close()
</script>
</xsl:if>
</xsl:template>


<!--##################################################
    ## error                                        ##
	################################################## -->
<xsl:template match="error">
	<div class="error">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="."/></xsl:with-param></xsl:call-template>
	</div>
</xsl:template>

<!--##################################################
    ## div                                          ##
	################################################## -->
<xsl:template match="div">
<div>
	<xsl:copy-of select="@*" />
	<xsl:apply-templates/>
</div>
</xsl:template>

<!--##################################################
    ## putCalendarLink                              ##
	################################################## -->
<xsl:template name="putCalendarLink">
<xsl:param name="date" />
<xsl:param name="form" />
<xsl:param name="field" />
	<a class="prodButton" tabindex="-1">
		<xsl:attribute name="href">javascript:PopWindow('<xsl:value-of select="$path_to_top"/>asp/calendar/calendar.asp?date=<xsl:value-of select="$date"/>&amp;field=<xsl:value-of select="$field"/>&amp;form=<xsl:value-of select="$form"/>',700,450,'CalendarWindow')</xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>calendar.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">ShowCalendar</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
		<div style="display:none" class="hiddenForWords">
        	<xsl:call-template name="putText"><xsl:with-param name="key">ShowCalendar</xsl:with-param></xsl:call-template>
        </div>
	</a>
</xsl:template>

<!--##################################################
    ## rePrintQString                               ##
	################################################## -->
<xsl:template name="rePrintQString"><xsl:param name="qItems" /><xsl:for-each select="$qItems"><xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;</xsl:for-each></xsl:template>

<!--##################################################
    ## paging                                       ##
	## for use with the ASP function printPagingData##
	## all = pageData created by printPagingData function
	## qItems = QueryString Items with @name and @value
	## curPageName = the @name which is the current displayed page
	## nextPhrase = the phrase to show the next page
	## prevPhrase = the phrase to show the previous page
	################################################## -->
<xsl:template name="paging">
<xsl:param name="all" />
<xsl:param name="curPageName" />
<xsl:param name="qItems" />
<xsl:param name="nextPhrase" />
<xsl:param name="prevPhrase" />
	<br />
<table class="tight" width="100%">
	<tr>
		<td class="tight" width="20%" style="text-align: left">
			<nobr>
				<xsl:call-template name="putText"><xsl:with-param name="key">Displaying</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
				<xsl:value-of select="((($all/curPage/@value) - 1) * $all/recsToShow/@value) + 1" /><xsl:text> </xsl:text>
				<xsl:call-template name="putText"><xsl:with-param name="key">to</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
				<xsl:choose>
					<xsl:when test="(($all/curPage/@value) * $all/recsToShow/@value) &lt; $all/totRecs/@value">
						<xsl:value-of select="($all/curPage/@value) * $all/recsToShow/@value" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$all/totRecs/@value" />
					</xsl:otherwise>
				</xsl:choose>
				<xsl:text> </xsl:text>
				<xsl:call-template name="putText"><xsl:with-param name="key">out of</xsl:with-param></xsl:call-template><xsl:text> </xsl:text>
				<xsl:value-of select="$all/totRecs/@value"/>
				<xsl:if test="not(/Doc_Webpage/content/@printable='true')">
				<a target="_blank">
					<xsl:attribute name="href"><xsl:value-of select="/Doc_Webpage/pageName/@val"/>?<xsl:for-each select="/Doc_Webpage/queryString/item[@value!='']"><xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;</xsl:for-each>makeExcel=true</xsl:attribute>
					<img border="0">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>excel.gif</xsl:attribute>
						<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Get complete results in Excel</xsl:with-param></xsl:call-template></xsl:attribute>
					</img>
				</a>
				</xsl:if>
<!--				<a>
					<xsl:attribute name="href">javaScript:saveSearch('<xsl:value-of select="/Doc_Webpage/pageName/@val"/>','<xsl:for-each select="/Doc_Webpage/queryString/item[@value!='']"><xsl:value-of select="@name"/>=<xsl:value-of select="translate(@value,'&quot;','&quot;&;')"/></xsl:for-each>');</xsl:attribute>
					<img border="0">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>save.gif</xsl:attribute>
						<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Get complete results in Excel</xsl:with-param></xsl:call-template></xsl:attribute>
					</img>
				</a>-->

			</nobr>
		</td>
		<td class="tight" width="60%" style="text-align: center">
			<table class="tight">
				<tr>
					<td class="tight" style="padding-right: 5;">
						<xsl:choose>
							<xsl:when test="$all/lowPage/@value &gt; 1">
								<xsl:call-template name="pageLink">
									<xsl:with-param name="page" select="$all/lowPage/@value - 1"/>
									<xsl:with-param name="curPageName" select="$curPageName" />
									<xsl:with-param name="qItems" select="$qItems" />
									<xsl:with-param name="phrase" select="$prevPhrase"/>
								</xsl:call-template>
							</xsl:when> 
							<xsl:otherwise>
								<span style="color:silver"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$prevPhrase"/></xsl:with-param></xsl:call-template></span>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="tight" style="padding-left: 5;padding-right: 5">
						<xsl:for-each select="$all/page">
							<xsl:if test="not ((position() = 1) and (position() = last())) ">
								<xsl:choose>
									<xsl:when test="$all/curPage/@value != ./@page">
										<a>
											<xsl:attribute name="href"><xsl:value-of select="$strThisFile"/>?curPage=<xsl:value-of select="@page"/>&amp;<xsl:call-template name="printQItems"><xsl:with-param name="Q" select="$qItems[@name != $curPageName]" /></xsl:call-template></xsl:attribute>
											<xsl:value-of select="./@page"/>
										</a>
									</xsl:when>
									<xsl:otherwise>
											<span style="color:silver"><xsl:value-of select="./@page"/></span>
									</xsl:otherwise>
								</xsl:choose>						
								<xsl:if test="position() != last()">
								|
								</xsl:if>
							</xsl:if>
						</xsl:for-each>
					</td>			
					<td class="tight" style="padding-left: 5;">
						<xsl:choose>
							<xsl:when test="$all/highPage/@value &lt; $all/maxPage/@value">
								<xsl:call-template name="pageLink">
									<xsl:with-param name="page" select="$all/highPage/@value + 1"/>
									<xsl:with-param name="qItems" select="$qItems" />
									<xsl:with-param name="phrase" select="$nextPhrase"/>
								</xsl:call-template>
							</xsl:when> 
							<xsl:otherwise>
								<span style="color:silver"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$nextPhrase"/></xsl:with-param></xsl:call-template></span>
							</xsl:otherwise>
						</xsl:choose>
					</td>
				</tr>
			</table>
		</td>
		<td class="tight" width="20%" style="text-align: right">
			<xsl:call-template name="numRecordsSetBox" >
				<xsl:with-param name="recsPerPage" select="$all/recsToShow/@value" />
			</xsl:call-template>
		</td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ## numRecordsSetBox                             ##
	################################################## -->
<xsl:template name="numRecordsSetBox">
<xsl:param name="recsPerPage" />
	<table class="tight">
		<tr>
			<xsl:attribute name="action"><xsl:value-of select="$path_to_lib"/>asp/common/setSessionVariable.asp</xsl:attribute>
			<td class="tight">
				<xsl:call-template name="putNumberBox">
					<xsl:with-param name="top" select="1" />
					<xsl:with-param name="bottom" select="100" />
					<xsl:with-param name="pad" select="0" />
					<xsl:with-param name="name">recordCount</xsl:with-param>
					<xsl:with-param name="default" select="$recsPerPage"/>
					<xsl:with-param name="onChange">setPerPageVal(this.value);</xsl:with-param>
				</xsl:call-template>
				<nobr><xsl:call-template name="putText"><xsl:with-param name="key">Per Page</xsl:with-param></xsl:call-template></nobr>
			</td>
		</tr>
	</table>
</xsl:template>

<!--##################################################
    ## PageLink                                     ##
	################################################## -->
<xsl:template name="pageLink">
<xsl:param name="page" />
<xsl:param name="curPageName" />
<xsl:param name="qItems" />
<xsl:param name="phrase" />
	<a>
		<xsl:attribute name="href"><xsl:value-of select="$strThisFile"/>?
		<xsl:if test="not($qItems[@name = $curPageName])">
				curPage=<xsl:value-of select="$page"/>&amp;
		</xsl:if>
		<xsl:for-each select="$qItems">			
			<xsl:choose>
				<xsl:when test="@name = $curPageName">
					<xsl:value-of select="@name"/>=<xsl:value-of select="$page"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>					
				</xsl:otherwise>
			</xsl:choose>
			&amp;
		</xsl:for-each>
		</xsl:attribute>
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$phrase"/></xsl:with-param></xsl:call-template>
	</a>
</xsl:template>

<!--##################################################
    ## printQItems                                  ##
	################################################## -->
<xsl:template name="printQItems">
<xsl:param name="Q"/>
<xsl:for-each select="$Q">
	<xsl:value-of select="@name"/>=<xsl:call-template name="replace">
										<xsl:with-param name="string" select="@value"/>
										<xsl:with-param name="pattern" select="'&amp;'"/>
										<xsl:with-param name="replacement" select="'%26'"/></xsl:call-template>&amp;</xsl:for-each>
</xsl:template>

<!--##################################################
    ## printQstring                                 ##
	################################################## -->
<xsl:template name="printQstring"><xsl:for-each select="/Doc_Webpage/queryString/node()"><xsl:value-of select="name()"/>=<xsl:value-of select="."/>&amp;</xsl:for-each>thisPage=<xsl:value-of select="@page"/>
</xsl:template>

<!--##################################################
    ## makeQueryString                              ##
	################################################## -->
<xsl:template name="makeQueryString"><xsl:param name="items" /><xsl:for-each select="$items"><xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;</xsl:for-each></xsl:template>

<!--##################################################
    ##  putHiddenRequestForm                        ##
	################################################## -->
<xsl:template name="putHiddenRequestForm">
<xsl:param name="items" />
	<xsl:for-each select="$items">
		<input type="hidden">
			<xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
			<xsl:attribute name="value"><xsl:value-of select="@Value"/></xsl:attribute>
		</input>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  quickAddBox                                 ##
	################################################## -->
<xsl:template name="quickAddBox">
	<select>
		<xsl:attribute name="onChange">if(String(this.value).length != 0){document.location=this.value}</xsl:attribute>
		<option value=""><xsl:call-template name="putText"><xsl:with-param name="key">Choose Quick Add</xsl:with-param></xsl:call-template></option>
		<option>
			<xsl:attribute name="value"><xsl:value-of select="$path_to_top"/>asp/ActualTasks/editTask.asp?pageID=editTask&amp;assignToJobRole=true&amp;JOB=CUSTOMER_COMPLAINTS</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Customer Complaint</xsl:with-param></xsl:call-template>
		</option>
		<option>
			<xsl:attribute name="value"><xsl:value-of select="$path_to_top"/>asp/ActualTasks/editTask.asp?pageID=editTask&amp;assignToJobRole=true&amp;JOB=IMPROVEMENT_REQ_HANDLER</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Improvement Requests</xsl:with-param></xsl:call-template>
		</option>
		<option>
			<xsl:attribute name="value"><xsl:value-of select="$path_to_top"/>asp/ActualTasks/editTask.asp?pageID=editTask</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Quick Add Task</xsl:with-param></xsl:call-template>
		</option>
		<option>
			<xsl:attribute name="value"><xsl:value-of select="$path_to_top"/>asp/meetings/editMeeting.asp?pageID=editMeeting</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Quick Add Meeting</xsl:with-param></xsl:call-template>
		</option>
		<option>
			<xsl:attribute name="value"><xsl:value-of select="$path_to_top"/>asp/discussions/editDiscussion.asp?pageID=editDiscussion</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Quick Add Discussion</xsl:with-param></xsl:call-template>
		</option>
		<option>
			<xsl:attribute name="value"><xsl:value-of select="$path_to_top"/>asp/messages/editMessages.asp?pageID=editMessages</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Quick Add Message</xsl:with-param></xsl:call-template>
		</option>
	</select>
</xsl:template>

<!--##################################################
    ## editStringsLink                              ##
	################################################## -->
<xsl:template name="editStringsLink">
	<xsl:if test="/Doc_Webpage/ROLES/ARole[@name = $adminRole]">
				<table class="tight">
					<tr>
						<form method="post" name="editStringForm">
							<xsl:attribute name="action"><xsl:value-of select="/Doc_Webpage/editStringsLink/@path"/></xsl:attribute>
							<xsl:call-template name="putHiddenRequestForm">
								<xsl:with-param name="items" select="//RequestForm/FormItem" />
							</xsl:call-template>
							<td class="tight">
								<input class="extraSmall" type="submit">
									<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Edit Strings</xsl:with-param></xsl:call-template></xsl:attribute>
								</input>			
								<div style="display:none" class="hiddenForWords">
                                	<xsl:call-template name="putText"><xsl:with-param name="key">Edit Strings</xsl:with-param></xsl:call-template>
                                </div>
							</td>
						</form>
					</tr>
				</table>
	</xsl:if>
</xsl:template>

<!--##################################################
    ## editPagePermissionsLink                      ##
	################################################## -->
<xsl:template name="editPagePermissionsLink">
	<xsl:if test="/Doc_Webpage/ROLES/ARole[@name = $adminRole]">
				<table class="tight">
					<tr>
						<form method="post" name="editPagePermissions">
							<xsl:attribute name="action"><xsl:value-of select="$strThisFile"/>?<xsl:call-template name="rePrintQString"><xsl:with-param name="qItems" select="/Doc_Webpage/queryString/item" /></xsl:call-template>editPermissions=true</xsl:attribute>
							<xsl:call-template name="putHiddenRequestForm">
								<xsl:with-param name="items" select="/Doc_Webpage/RequestForm/FormItem" />
							</xsl:call-template>
							<td class="tight">
								<input class="extraSmall" type="submit" style="padding:0">
									<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Edit Permissions</xsl:with-param></xsl:call-template></xsl:attribute>
								</input>			
								<div style="display:none" class="hiddenForWords">
                                	<xsl:call-template name="putText"><xsl:with-param name="key">Edit Permissions</xsl:with-param></xsl:call-template>
                                </div>
							</td>
						</form>
					</tr>
				</table>
				<xsl:apply-templates select="/Doc_Webpage/settings" />
	</xsl:if>
</xsl:template>

<!--##################################################
    ## putNumberBox                                 ##
	################################################## -->
<xsl:template name="putNumberBox">
	<xsl:param name="top" />
	<xsl:param name="pad" />
	<xsl:param name="bottom" />
	<xsl:param name="name" />
	<xsl:param name="default" />
	<xsl:param name="onChange" />
	<select>
		<xsl:attribute name="onchange"><xsl:value-of select="$onChange"/></xsl:attribute>
		<xsl:attribute name="name"><xsl:value-of select="$name"/></xsl:attribute>
		<xsl:call-template name="putNumberOption">
			<xsl:with-param name="top" select="$top" />
			<xsl:with-param name="pad" select="$pad" />
			<xsl:with-param name="bottom" select="$bottom"/>
			<xsl:with-param name="default" select="$default"/>
		</xsl:call-template>
	</select>
</xsl:template>

<!--##################################################
    ## putNumberOption                              ##
	################################################## -->
<xsl:template name="putNumberOption">
	<xsl:param name="top" />
	<xsl:param name="bottom" />
	<xsl:param name="default" />
	<xsl:param name="pad" />
	<xsl:choose>
		<xsl:when test = "$top &gt; $bottom">
			<option>
				<xsl:attribute name="value">
					<xsl:call-template name="addLeadingZeros">
						<xsl:with-param name="val" select="$top" />
						<xsl:with-param name="pad" select="$pad" />
					</xsl:call-template>
				</xsl:attribute>
				<xsl:if test="$top = $default">
					<xsl:attribute name="selected">true</xsl:attribute>
				</xsl:if>
					<xsl:call-template name="addLeadingZeros">
						<xsl:with-param name="val" select="$top" />
						<xsl:with-param name="pad" select="$pad" />
					</xsl:call-template>
			</option>
			<xsl:call-template name="putNumberOption">
				<xsl:with-param name="top" select="$top - 1" />
				<xsl:with-param name="bottom" select="$bottom" />
				<xsl:with-param name="default" select="$default" />
				<xsl:with-param name="pad" select="$pad" />			
			</xsl:call-template>
		</xsl:when>
		<xsl:when test = "$top &lt; $bottom">
			<option>
				<xsl:attribute name="value">
					<xsl:call-template name="addLeadingZeros">
						<xsl:with-param name="val" select="$top" />
						<xsl:with-param name="pad" select="$pad" />
					</xsl:call-template>
				</xsl:attribute>
				<xsl:if test="$top = $default">
					<xsl:attribute name="selected">true</xsl:attribute>
				</xsl:if>
					<xsl:call-template name="addLeadingZeros">
						<xsl:with-param name="val" select="$top" />
						<xsl:with-param name="pad" select="$pad" />
					</xsl:call-template>
			</option>
			<xsl:call-template name="putNumberOption">
				<xsl:with-param name="top" select="$top + 1" />
				<xsl:with-param name="bottom" select="$bottom" />
				<xsl:with-param name="default" select="$default" />
				<xsl:with-param name="pad" select="$pad" />			
			</xsl:call-template>
		</xsl:when>				
		<xsl:otherwise>
			<option>
				<xsl:attribute name="value">
					<xsl:call-template name="addLeadingZeros">
						<xsl:with-param name="val" select="$top" />
						<xsl:with-param name="pad" select="$pad" />
					</xsl:call-template>
				</xsl:attribute>
				<xsl:if test="$top = $default">
					<xsl:attribute name="selected">true</xsl:attribute>
				</xsl:if>
					<xsl:call-template name="addLeadingZeros">
						<xsl:with-param name="val" select="$top" />
						<xsl:with-param name="pad" select="$pad" />
					</xsl:call-template>
			</option>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--##################################################
    ## addLeadingZeros                              ##
	################################################## -->
<xsl:template name="addLeadingZeros">
<xsl:param name="val" />
<xsl:param name="pad" />
<xsl:choose>
	<xsl:when test="string-length(string($val)) &lt; $pad">0<xsl:call-template name="addLeadingZeros">
			<xsl:with-param name="val" select="$val" />
			<xsl:with-param name="pad" select="number($pad) - 1" />
		</xsl:call-template>
	</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="$val"/>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>


<!--##################################################
    ## jPutText                                      ##
	################################################## -->
<xsl:template name="jPutText">
<xsl:param name="key" />
<xsl:param name="nobr" />
<xsl:choose>
	<xsl:when test="$stringEdit = 'yes'">
		<EditableString>
			<xsl:attribute name="key"><xsl:value-of select="$key"/></xsl:attribute>
			<xsl:attribute name="filename"><xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/../../@filename"/></xsl:attribute>
			<xsl:choose>	
				<xsl:when test="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]"><xsl:attribute name="value"><xsl:value-of select="/Doc_Webpage/languageStrings/lang/txt/textString[@id = $key]/@value"/></xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="value"></xsl:attribute></xsl:otherwise>
			</xsl:choose>					
		</EditableString>
	</xsl:when>
	<xsl:otherwise>
		<xsl:variable name="val"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$key"/></xsl:with-param></xsl:call-template></xsl:variable>
		<xsl:call-template name="replace">
			<xsl:with-param name="string" select="$val" />
			<xsl:with-param name="pattern">'</xsl:with-param>
			<xsl:with-param name="replacement">\'</xsl:with-param>
		</xsl:call-template>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## Doc_Webpage                                  ##
	################################################## -->
<xsl:template match="Doc_Webpage">
<xsl:choose>
	<xsl:when test="content/@printable or content/PAGE_IS_PRINTABLE">
		<xsl:call-template name="Printable_Doc_WebPage" />	
	</xsl:when>
	<xsl:when test="@justText">
		<html><xsl:apply-templates /></html>
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="Doc_WebPage" />		
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## Doc_Webpage                                  ##
	################################################## -->
<xsl:template match="HTML_EMAIL_BODY">
<html><xsl:apply-templates /></html>
</xsl:template>


<!--##################################################
    ##  Doc_Webpage                                 ##
	################################################## -->
<xsl:template name="Doc_WebPage">
<html>
	<div style="display:none" class="hiddenForWords">
		<xsl:call-template name="putText"><xsl:with-param name="key">login.aspTitle</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">login.aspHelp</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">LOGIN</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">PASSWORD</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">LoginButton</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">Login And Auto Login from now on</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">LoginError</xsl:with-param></xsl:call-template>
		<xsl:for-each select="/Doc_Webpage/languageStrings/lang">
			<LanguageFiles>
				<xsl:attribute name="name"><xsl:value-of select="@filename"/></xsl:attribute>
			</LanguageFiles>
		</xsl:for-each>
		<myLanguageFileName><xsl:attribute name="value"><xsl:value-of select="/Doc_Webpage/languageStrings/myLanguageFileName"/></xsl:attribute></myLanguageFileName>
	</div>		
	<xsl:call-template name="header"/>
	<body>
		<xsl:attribute name="onLoad"><xsl:call-template name="setUpHideColumns" />resetSelectClose();resetTextAdder();onLoadFunction();startLogOutTimer();<xsl:if test="/Doc_Webpage/queryString/item[@name='showToolBox']/@value='true' or /Doc_Webpage/whoAmI/record/field[@name='TOOL_BOX']/@value='1'">showRowActionItems();</xsl:if><xsl:for-each select="/Doc_Webpage/onLoadDoThis"><xsl:value-of select="."/></xsl:for-each></xsl:attribute>
		<xsl:if test="/Doc_Webpage/whoAmI/record/field[@name='BG_IMAGE']/@value!=''">
			<xsl:attribute name="style">background-image:url(<xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="/Doc_Webpage/whoAmI/record/field[@name='BG_IMAGE']/@value"/>);</xsl:attribute>
		</xsl:if>
		
		
		<table width="100%" class="tight">
			<tr>
<!--				<xsl:if test="/Doc_Webpage/whoAmI">
					<td class="tight">
						<xsl:call-template name="quickNavMenu" />
					</td>
				</xsl:if>-->
				<td class="tight" style="text-align:center">
					<xsl:call-template name="Insides"/>
				</td>
			</tr>
		</table>
		<div id="AjaxSaveData" style="font-size: x-small;color:silver;"></div>
		<xsl:if test="/Doc_Webpage/ROLES/ARole[@name = $adminRole]">
			<xsl:variable name="adminOnClick">toggle('openAdminImage');toggle('closeAdminImage');toggle('adminSettings');</xsl:variable>
			<div id="openAdminImage">
				<xsl:attribute name="onClick"><xsl:value-of select="$adminOnClick"/></xsl:attribute>
				<a href="javascript:"> 
					<img border="0">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>openAdminSettings.gif</xsl:attribute>
						<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Show the admin tools</xsl:with-param></xsl:call-template></xsl:attribute>
					</img>
					<div style="display:none" class="hiddenForWords">
                       	<xsl:call-template name="putText"><xsl:with-param name="key">Show the admin tools</xsl:with-param></xsl:call-template>
                    </div>
				</a>
			</div>
			<div id="closeAdminImage" style="display:none">
				<xsl:attribute name="onClick"><xsl:value-of select="$adminOnClick"/></xsl:attribute>
				<a href="javascript:"> 
					<img border="0">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>closeAdminSettings.gif</xsl:attribute>
						<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Hide the tools</xsl:with-param></xsl:call-template></xsl:attribute>
					</img>
					<div style="display:none" class="hiddenForWords">
                       	<xsl:call-template name="putText"><xsl:with-param name="key">Hide the tools</xsl:with-param></xsl:call-template>
                    </div>
				</a>
			</div>
			<div id="adminSettings" style="display:none">
				<xsl:call-template name="editStringsLink" />
				<xsl:call-template name="editPagePermissionsLink" />
			</div>		
		</xsl:if>
		<xsl:call-template name="editPermissionsForm"></xsl:call-template>
		<form name="timerForm">
			<input type="hidden" size="2" name="timerBox" style="background-color:gray;color:silver;border:0;" />
		</form>
		<div id="tip_popUp" class="tip_popUp" style="display:none;position:absolute;">
		</div>

	</body>
</html>
</xsl:template>
<!--##################################################
    ##  setUpHideColumns                            ##
	################################################## -->
<xsl:template name="setUpHideColumns">
<xsl:for-each select="/Doc_Webpage/cookie[contains(@name,'ColumnCollapsed') and contains(@name,/Doc_Webpage/searchFilePath)]">collapseColumn('<xsl:value-of select="@value"/>');</xsl:for-each>
</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template name="Insides">
	<xsl:call-template name="titleBar" />
	<div class="dataBody">
	<xsl:apply-templates select="content"/>
	</div>
	<div class="bottomRow">
	<xsl:if test="/Doc_Webpage/whoAmI">
		<xsl:call-template name="bottomBar" />
	</xsl:if>
	</div>
</xsl:template>

<!--##################################################
    ##  bottomBar                                   ##
	################################################## -->
<xsl:template name="bottomBar">
<div style="width:100%;border-bottom: ridge thin;border-top: ridge thin;margin:0px;" class="alumBG">
	<table style="width:100%">
		<tr>
			<td class="bottomRowCol">
				<table class="tight topRow">
					<tr>
						<td class="bottomRowCol">
							<nobr><xsl:call-template name="putText"><xsl:with-param name="key">user:</xsl:with-param></xsl:call-template></nobr>
						</td>
						<td class="bottomRowCol">
							<nobr>
							<xsl:value-of select="/Doc_Webpage/whoAmI/record/field[@name = 'NAME']/@value"/><xsl:text> </xsl:text>
							<xsl:value-of select="/Doc_Webpage/whoAmI/record/field[@name = 'LAST_NAME']/@value"/>
							<xsl:text> </xsl:text>
							(
							<a>
								<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/utilities/switchUsers.asp</xsl:attribute>
								<xsl:call-template name="putText"><xsl:with-param name="key">Switch</xsl:with-param></xsl:call-template>
							</a>
							)
							<xsl:text> </xsl:text>
							</nobr>
						</td>
						<td class="bottomRowCol" style="padding-right:5px;"><nobr>(<xsl:value-of select="/Doc_Webpage/whoAmI/COMPANY_NAME"/>)</nobr></td>
						<td class="bottomRowCol" style="padding-right:5px;">
							<xsl:apply-templates select="/Doc_Webpage/HIT_COUNT"/>																								
						</td>
						<td class="bottomRowCol">
							<nobr>
								<xsl:call-template name="putText"><xsl:with-param name="key">Cur Time</xsl:with-param></xsl:call-template>
								<xsl:text> </xsl:text>
								<xsl:value-of select="/Doc_Webpage/USER_TIME"/>
							</nobr>
						</td>
<!--						<td class="bottomRowCol">
							Sessions: <xsl:value-of select="/Doc_Webpage/CURRENT_USERS"/>
						</td>-->
					</tr>
				</table>
			</td>
			<td class="topRow bottomRow" style="text-align:right;">
				<table class="tight">
					<tr>
						<td class="cottomRowCol">
							<xsl:call-template name="screenSwitcher"/>			
						</td>
						<td  class="bottomRowCol">
							<xsl:call-template name="quickAddBox"/>			
						</td>
						<td  class="bottomRowCol">
							<xsl:apply-templates select="FeedBackForm"/>			
						</td>
						<td class="bottomRowCol">
							<input class="extraSmall" type="button"><xsl:attribute name="onClick">document.location = '<xsl:value-of select="$path_to_top"/>asp/people/editMyAccount.asp?inside=true'</xsl:attribute><xsl:attribute name="value">
									<xsl:call-template name="putText"><xsl:with-param name="key">My Preferences</xsl:with-param></xsl:call-template>
								</xsl:attribute>
							</input>
							<div style="display:none" class="hiddenForWords">
								<xsl:call-template name="putText"><xsl:with-param name="key">My Preferences</xsl:with-param></xsl:call-template>
							</div>
						</td>
						<td class="bottomRowCol">
							<xsl:apply-templates select ="/Doc_Webpage/logout" />
						</td>				
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
</xsl:template>

<!--##################################################
    ##  titleBar                                    ##
	################################################## -->
<xsl:template name="titleBar">
	<!--Top Row of Page -->
	<div style="width:100%;border-bottom: ridge thin;border-top: ridge thin;margin:0px;" class="alumBG">
	<table class="tight" width="100%" style="margin:0">
		<tr>
			<td width="1px" rowspan="2">
				<div onmouseover="toggle('shortMenu');">
					<img border="0">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>viewShortMenu.gif</xsl:attribute>
					</img>
				</div>
			</td>
			<td class="new_body" style="text-align: left;font-size: x-small;">
				<table width="100%">
					<tr>
						<td width="1%">
							<a class="title" style="color: black;" tabindex="-1">
								<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/Settings/</xsl:attribute>
								<nobr><span><xsl:call-template name="putText"><xsl:with-param name="key">FSOT MFER</xsl:with-param></xsl:call-template></span></nobr>
							</a>
						</td>
						<td style="text-align:center" class="breadCrum">
						<xsl:choose>
							<xsl:when test="/Doc_Webpage/whoAmI">
								<a class="black" tabindex="-1">
									<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/messages/editMessages.asp?action=help</xsl:attribute>
									<nobr><span><xsl:call-template name="putText"><xsl:with-param name="key">CALL FOR HELP</xsl:with-param></xsl:call-template></span></nobr>
								</a>
							</xsl:when>
							<xsl:otherwise>
									<nobr><span class="black"><xsl:call-template name="putText"><xsl:with-param name="key">login.aspHelp</xsl:with-param></xsl:call-template></span></nobr>
							</xsl:otherwise>
						</xsl:choose>
						</td>
					</tr>
				</table>
			</td>
			<td class="new_body" style="text-align: right;width:2;" rowspan="2">
				<a tabindex="-1">
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp</xsl:attribute>
					<img border="0"  width="103" height="36">
						<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Bark LOGO Bark</xsl:with-param></xsl:call-template></xsl:attribute>
						<xsl:choose>
							<xsl:when test="/Doc_Webpage/whoAmI/record/field[@name='LOGO_ID']/@value != ''">
								<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="/Doc_Webpage/whoAmI/record/field[@name='LOGO_ID']/@value"/></xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>sriLogo.gif</xsl:attribute>
							</xsl:otherwise>
						</xsl:choose>
					</img>
					<div style="display:none" class="hiddenForWords">
                    	<xsl:call-template name="putText"><xsl:with-param name="key">Bark LOGO Bark</xsl:with-param></xsl:call-template>
                    </div>
				</a>
			</td>
		</tr>
		<tr>
			<td  class="new_body" style="text-align: right;">
				<div class="cookieCrum">
					<xsl:call-template name="putCookieCrum">
						<xsl:with-param name="cookieCrums" select="cookieCrums"/>
					</xsl:call-template>
				</div>
			</td>
		</tr>
	</table>
	</div>
</xsl:template>

<!--##################################################
    ## editPermissionsForm                          ##
	################################################## -->
<xsl:template name="editPermissionsForm">
	<xsl:if test="/Doc_Webpage/queryString/item[@name='editPermissions']/@value ='true' and /Doc_Webpage/ROLES/ARole[@name = $adminRole]">
		<table class="standard">
			<tr>
				<th class="standard">
					<xsl:call-template name="putText"><xsl:with-param name="key">Object Name</xsl:with-param></xsl:call-template>
				</th>
				<th class="standard">
					<xsl:call-template name="putText"><xsl:with-param name="key">View</xsl:with-param></xsl:call-template>
				</th>
				<th class="standard">
					<xsl:call-template name="putText"><xsl:with-param name="key">No View</xsl:with-param></xsl:call-template>
				</th>
				<th class="standard">
					<xsl:call-template name="putText"><xsl:with-param name="key">Edit</xsl:with-param></xsl:call-template>
				</th>
			</tr>
			<form name="pagePermissionForm" method="post" onSubmit="escapeAllSelects();">
				<xsl:attribute name="action"><xsl:value-of select="$path_to_top"/>asp/common/updatePermissions.asp</xsl:attribute>
				<input type="hidden" name="sessionID">
					<xsl:attribute name="value"><xsl:value-of select="/Doc_Webpage/sessionID"/></xsl:attribute>
				</input>
				<xsl:for-each select="/Doc_Webpage/pagePermissionsForm/obj/obj[@ID != 'NO_ID']">
					<input type="hidden" name="objectList">
						<xsl:attribute name="value"><xsl:value-of select="@ID"/></xsl:attribute>
					</input>
					<tr>
						<td class="standard">
							<xsl:value-of select="@ID"/>
						</td>
						<td class="standard">
							<xsl:apply-templates select="./view/obj" />
						</td>
						<td class="standard">
							<xsl:apply-templates select="./noView/obj" />
						</td>
						<td class="standard">
							<xsl:apply-templates select="./edit/obj" />
						</td>
					</tr>
				</xsl:for-each>
					<tr>
						<td colspan="4" class="standard" style="text-align: center">
							<input type="Submit">
								<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Update Permissions</xsl:with-param></xsl:call-template></xsl:attribute>
							</input>
						</td>
					</tr>
			</form>
		</table>
	</xsl:if>
</xsl:template>


<!--##################################################
    ## putCookieCrum                                ##
	################################################## -->
<xsl:template name="putCookieCrum">
<xsl:param name="cookieCrums"/>
	<xsl:for-each select="$cookieCrums/crum">
		<a tabindex="-1" class="black">
			<xsl:attribute name="href"><xsl:value-of select="@path"/></xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@name"/></xsl:with-param></xsl:call-template>
		</a>
		<xsl:if test="position() != last()">
			<xsl:text> </xsl:text>><xsl:text> </xsl:text>
		</xsl:if>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## logout                                       ##
	################################################## -->
<xsl:template match="logout">
<input class="extrasmall" type="Button" style="font-size:xx-small">
	<xsl:attribute name="onclick">document.location = '<xsl:value-of select="$path_to_top" />logout.asp'</xsl:attribute>
	<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Logout</xsl:with-param></xsl:call-template></xsl:attribute>
</input>
<div style="display:none" class="hiddenForWords">
	<xsl:call-template name="putText"><xsl:with-param name="key">Logout</xsl:with-param></xsl:call-template>
</div>
</xsl:template>

<!--##################################################
    ## HIT_COUNT                                    ##
	################################################## -->
<xsl:template match="HIT_COUNT">
	<nobr><xsl:call-template name="putText"><xsl:with-param name="key">Hits</xsl:with-param></xsl:call-template><xsl:value-of select="@COUNT"/></nobr>
</xsl:template>

<!--##################################################
    ## FeedBackForm                                 ##
	################################################## -->
<xsl:template match="FeedBackForm">
	<input type="Button">
		<xsl:attribute name="onClick">document.location='<xsl:value-of select="$path_to_top"/>asp/common/feedback.asp'</xsl:attribute>
		<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Provide Feedback about Page</xsl:with-param></xsl:call-template></xsl:attribute>
	</input>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">Provide Feedback about Page</xsl:with-param></xsl:call-template>
    </div>
</xsl:template>

<!--##################################################
    ## message                                      ##
	################################################## -->
<xsl:template match="message">
	<div class="message">
		<xsl:value-of select="@content"/>
	</div>
</xsl:template>

<!--##################################################
    ## default_body                                 ##
	################################################## -->
<xsl:template match="default_body">
	<div class="default_body">
		<xsl:if test="@class">
			<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
		</xsl:if>
		<xsl:apply-templates/>
	</div>
</xsl:template>

<!--##################################################
    ## straight_HTML                                ##
	################################################## -->
<xsl:template match="straight_HTML">
	<xsl:copy-of select="."/>
</xsl:template>	

<!--##################################################
    ## script                                       ##
	################################################## -->
<xsl:template match="script">
	<xsl:copy-of select="."/>
</xsl:template>	

<!--##################################################
    ## content                                      ##
	################################################## -->
<xsl:template match="content">
	<xsl:apply-templates/>
</xsl:template>

<!--###################################################################
    ## debug                                                         ##
	###################################################################-->
<xsl:template match="debug">
	<span class="debug">
		debug: <xsl:value-of select="."/><br/>
	</span>
</xsl:template>


<!--##################################################
    ## spacer                                       ##
	################################################## -->
<xsl:template name="spacer">
	<div class="spacer">
		<br/>
	</div>
</xsl:template>


<!--##################################################
    ## Header                                       ##
	################################################## -->
<xsl:template name="header">
 <head>
 	<title>
		<xsl:call-template name="putText"><xsl:with-param name="key">title</xsl:with-param></xsl:call-template>
	</title>
	<xsl:if test="not(/Doc_Webpage/@email = 'true')">
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>config.js</xsl:attribute>
		//placeholder
	</script>	
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/TEA_supplement.js</xsl:attribute>
		//placeholder
	</script>	
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/OptionBox.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/validateDate.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/deny.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/ValidateDocumentSumit.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/tips.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/cb.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/ajax.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/newAjax.js</xsl:attribute>
		//placeholder
	</script>
	<script>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_lib"/>asp/common/javascript/searchDrop.js</xsl:attribute>
		//placeholder
	</script>
	</xsl:if>
		
	<xsl:for-each select="js_file">
		<script>
			<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>js/<xsl:value-of select="@name"/></xsl:attribute>
			//placeholder
		</script>
	</xsl:for-each>
	<xsl:for-each select="javaScriptFile">
		<script>
			<xsl:attribute name="src"><xsl:value-of select="@path"/></xsl:attribute>
			//placeholder
		</script>
	</xsl:for-each>
	
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/theory.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/procedures.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/tabs.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/wf_viewer.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/folders.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/general.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/windowDesign.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/search.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/file.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/menu.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/myFolder.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/myApprovals.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/hit_data.css</xsl:attribute>
	</link>
	<link rel="stylesheet" type="text/css">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_lib"/>style/css/tables.css</xsl:attribute>
	</link>
<!-- Style for layers -->

 </head>
</xsl:template>

<!--##################################################
    ## select                                       ##
	################################################## -->
<xsl:template match="select">
	<xsl:copy-of select="."/>
</xsl:template>

<!--##################################################
    ## auto_menu                                    ##
	################################################## -->
<xsl:template match="auto_menu">
	<table>
		<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
		<xsl:for-each select="menu_item">
			<tr>
				<td>
					<xsl:attribute name="class"><xsl:value-of select="../@class"/></xsl:attribute>
					<a>
						<xsl:attribute name="href">
							<xsl:value-of select="@page_addy"/>
						</xsl:attribute>
						<xsl:value-of select="."/>										
					</a>
				</td>
			</tr>
		</xsl:for-each>
	</table>
</xsl:template>


<!--##################################################
    ## HITDATA                                      ##
	################################################## -->
<xsl:template match="HITDATA">
	<table class="hitdata">
		<tr>
			<th class="hit_data_title">
				<xsl:value-of select="@DocumentLabel"/>
			</th>
			<th class="hit_data_title">
				<xsl:value-of select="@HIT_LABEL"/>
			</th>
			<th class="hit_data_title">
				<xsl:value-of select="@INFO_LABEL"/>
			</th>
			
		</tr>
		<xsl:apply-templates select="HIT_DATA"/>
		<tr>
			<td class="hitdata_center" colspan="3">
				<xsl:if test="SHOWPREV">
					<a tabindex="-1">
						<xsl:attribute name="href"><xsl:value-of select="strThisFile"/>?page=<xsl:value-of select="SHOWPREV/@page"/></xsl:attribute>
						<xsl:call-template name="putText"><xsl:with-param name="key">Previous</xsl:with-param></xsl:call-template>
					</a>
					<xsl:if test="SHOWNEXT">
						<xsl:text> | </xsl:text>
					</xsl:if>
				</xsl:if>	
				<xsl:if test="SHOWNEXT">		
					<a tabindex="-1">
						<xsl:attribute name="href"><xsl:value-of select="strThisFile"/>?page=<xsl:value-of select="SHOWNEXT/@page"/></xsl:attribute>
						<xsl:call-template name="putText"><xsl:with-param name="key">Next</xsl:with-param></xsl:call-template>
					</a>		
				</xsl:if>			
			</td>
		</tr>
			
	</table>
</xsl:template>

<!--##################################################
    ## HIT_DATA                                     ##
	################################################## -->
<xsl:template match="HIT_DATA">
	<tr>
		<td class="hitdata">
			<xsl:value-of select="@PAGETYPE"/> - <xsl:value-of select="@ID"/>
		</td>
		<td class="hitdata_center">
			<xsl:value-of select="@HIT_COUNT"/>
		</td>
		<td class="hitdata_center">
			<xsl:value-of select="@INFO"/>
		</td>
	</tr>
</xsl:template>


<!--##################################################
    ## standardDropDownOptions                      ##
	################################################## -->
<xsl:template name="standardDropDownOptions">
	<xsl:param name="valueFields" />
	<xsl:param name="showFields" />
	<xsl:param name="myRecords" />
	<xsl:param name="default" />
	<xsl:for-each select="$myRecords/record">
		<xsl:call-template name="printStandardOption">
			<xsl:with-param name="myRecord" select="." />
			<xsl:with-param name="valueFields" select="$valueFields" />
			<xsl:with-param name="showFields" select="$showFields" />	
			<xsl:with-param name="default" select="$default"/>	
		</xsl:call-template>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## printStandardOption                          ##
	################################################## -->
<xsl:template name="printStandardOption">
	<xsl:param name="myRecord" />
	<xsl:param name="valueFields" />
	<xsl:param name="showFields" />
	<xsl:param name="default" />
	<option>
		<xsl:if test="$default = $myRecord/field[@name = $valueFields]/@value">
			<xsl:attribute name="selected">true</xsl:attribute>
		</xsl:if>
		<xsl:attribute name="value">
			<xsl:call-template name="printStandardOptionText">
				<xsl:with-param name="fields" select="$valueFields" />
				<xsl:with-param name="myRecord" select="$myRecord" />
			</xsl:call-template>
		</xsl:attribute>
		<xsl:call-template name="printStandardOptionText">
			<xsl:with-param name="fields" select="$showFields" />
			<xsl:with-param name="myRecord" select="$myRecord" />
			<xsl:with-param name="seperator"><xsl:text> </xsl:text></xsl:with-param>
		</xsl:call-template>
	</option>
</xsl:template>


<!--##################################################
    ## printStandardOptionText                      ##
	################################################## -->
<xsl:template name="printStandardOptionText">
	<xsl:param name="fields" />
	<xsl:param name="myRecord" />	
	<xsl:param name="seperator" />	
	
	<xsl:variable name="myField" select="$myRecord/field/@name" />
	<xsl:choose>
		<xsl:when test="contains($fields,',')">
			<xsl:value-of select="$myRecord/field[@name = substring-before($fields,',')]/@value"/><xsl:value-of select="$seperator"/>
			<xsl:call-template name="printStandardOptionText">
				<xsl:with-param name="fields" select="substring-after($fields,',')" />
				<xsl:with-param name="myRecord" select="$myRecord" />
			</xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$myRecord/field[@name = $fields]/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ## putRemoveButton                              ##
	################################################## -->
<xsl:template name="putRemoveButton">
	<xsl:param name="form" />
	<xsl:param name="field" />
	<xsl:param name="phrase" />
	<a  tabindex="-1" class="prodButton">
		<xsl:attribute name="href">javascript:disableOtherForms("<xsl:value-of select="$form"/>");delOptions(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>);<xsl:value-of select="./attribute[@name='doWhenRemoving']/@value"/></xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>minus.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">remove</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">remove</xsl:with-param></xsl:call-template>
    </div>
</xsl:template>

<!--##################################################
    ## putPreviewButton                              ##
	################################################## -->
<xsl:template name="putPreviewDocumentButton">
	<xsl:param name="form" />
	<xsl:param name="field" />
	<xsl:param name="alt" />
	<a tabindex="-1" class="prodButton">
		<xsl:attribute name="href">javascript:if(getSelectedValue(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>) != "NOTHING" &amp;&amp; getSelectedValue(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>) != ""){PopWindow('<xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?dl=true&amp;docID=' + getSelectedValue(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>),400,400,'previewWindow')}else{alert('<xsl:call-template name="jPutText"><xsl:with-param name="key">Please Select An Item to preview first</xsl:with-param></xsl:call-template>')};</xsl:attribute>
		<div style="display:none" class="hiddenForWords">
	        <xsl:call-template name="putText"><xsl:with-param name="key">Please Select An Item to preview first</xsl:with-param></xsl:call-template>
        </div>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>view.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:value-of select="$alt"/></xsl:attribute>
		</img>
	</a>
</xsl:template>


<!--##################################################
    ## putLinkButton                                ##
	################################################## -->
<xsl:template name="putLinkButton">
<xsl:param name="obj" />
<xsl:param name="doWhenAdding" />
<xsl:param name="destination" />
<xsl:param name="fieldName" />
<xsl:param name="formName" />
<xsl:param name="image" />
<xsl:param name="id" />
<xsl:param name="multiple" />
<xsl:param name="recsPerPage" />
<xsl:param name="extraQueryData" />
<xsl:param name="specialPop" />
<xsl:param name="ttCat" />
<xsl:param name="popOnLoad" />
	<a class="prodButton">
		<xsl:attribute name="href">javascript:PopWindow('<xsl:value-of select="$destination"/>?showToolBox=true&amp;firstTime=TRUE&amp;link=yes&amp;recsPerPage=<xsl:value-of select="$recsPerPage"/>&amp;RECEIVER_FORM=<xsl:value-of select="$formName"/>&amp;RECEIVER_FIELD=<xsl:value-of select="$fieldName"/>&amp;PopUp_ID=<xsl:value-of select="$id"/>&amp;multiple=<xsl:value-of select="$multiple"/>&amp;pageID=<xsl:value-of select="$id"/>&amp;doWhenAdding=<xsl:value-of select="$doWhenAdding"/>&amp;<xsl:value-of select="$extraQueryData"/>',1000,800,'<xsl:value-of select="$formName"/>_<xsl:value-of select="$fieldName"/>');</xsl:attribute>		
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>plus.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">add</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">add</xsl:with-param></xsl:call-template>
    </div>
	<xsl:if test="$popOnLoad = 'true'">
		<script language="JavaScript">addOnLoadFunction('PopWindow(\'<xsl:value-of select="$destination"/>?firstTime=TRUE&amp;link=yes&amp;recsPerPage=<xsl:value-of select="$recsPerPage"/>&amp;RECEIVER_FORM=<xsl:value-of select="$formName"/>&amp;RECEIVER_FIELD=<xsl:value-of select="$fieldName"/>&amp;PopUp_ID=<xsl:value-of select="$id"/>&amp;multiple=<xsl:value-of select="$multiple"/>&amp;pageID=<xsl:value-of select="$id"/>&amp;doWhenAdding=<xsl:value-of select="$doWhenAdding"/>&amp;<xsl:value-of select="$extraQueryData"/>\',800,600,\'<xsl:value-of select="$formName"/>_<xsl:value-of select="$fieldName"/>\')');</script>
	</xsl:if>
</xsl:template>

<!--##################################################
    ## replace                                      ##
	################################################## -->
<xsl:template name="replace">
<xsl:param name="string" select="''"/>
<xsl:param name="pattern" select="''"/>
<xsl:param name="replacement" select="''"/>
<xsl:choose>
	<xsl:when test="$pattern != '' and $string != '' and contains($string, $pattern)">
		<xsl:value-of select="substring-before($string, $pattern)"/>
		<!--
		Use "xsl:copy-of" instead of "xsl:value-of" so that users
		may substitute nodes as well as strings for $replacement.
		-->
		<xsl:copy-of select="$replacement"/>
		<xsl:call-template name="replace">
			<xsl:with-param name="string" select="substring-after($string, $pattern)"/>
			<xsl:with-param name="pattern" select="$pattern"/>
			<xsl:with-param name="replacement" select="$replacement"/>
		</xsl:call-template>
	</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="$string"/>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## Email_Body                                        ##
	################################################## -->
<xsl:template match="Email_Body">
	<xsl:apply-templates select="obj"/>
</xsl:template>

<!--##################################################
    ## Email_Subject                                      ##
	################################################## -->
<xsl:template match="Email_Subject">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="."/></xsl:with-param></xsl:call-template>
</xsl:template>

<!--##################################################
    ## Printable_Doc_Webpage                        ##
	################################################## -->
<xsl:template name="Printable_Doc_WebPage">
<html>
	<xsl:call-template name="header"/>
	<body onload="window.focus();resetSelectClose();resetTextAdder();onLoadFunction();startLogOutTimer();">
		<xsl:apply-templates select="content"/>
	</body>
</html>
</xsl:template>


<!--##################################################
    ##  quickNavMenu                                ##
	################################################## -->
<xsl:template name="quickNavMenu">
	<div id="shortMenu" style="display:none;">
	<br />
	<br />
	<xsl:call-template name="putText"><xsl:with-param name="key">Quick Links</xsl:with-param></xsl:call-template>
	<xsl:call-template name="createShortMenuGroup"><xsl:with-param name="it">tasks</xsl:with-param></xsl:call-template>
	<xsl:call-template name="createShortMenuGroup"><xsl:with-param name="it">objects</xsl:with-param></xsl:call-template>
	<xsl:if test="/Doc_Webpage/ROLES/ARole[@name = $adminRole]">
		<xsl:call-template name="createShortMenuGroup"><xsl:with-param name="it">Administration</xsl:with-param></xsl:call-template>
	</xsl:if>
	</div>
</xsl:template>


<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template name="createShortMenuGroup">
<xsl:param name="it" />
<div class="shortMenuGroup">
	<div class="shortMenuGroupTitle">
		<xsl:attribute name="onClick">toggle('shortMenuItems_<xsl:value-of select="$it"/>');toggle('shortMenuItemsPlus_<xsl:value-of select="$it"/>');toggle('shortMenuItemsMinus_<xsl:value-of select="$it"/>');</xsl:attribute>
		<table class="tight">
			<tr>
				<td  class="tight">
					<div>
						<xsl:attribute name="ID">shortMenuItemsPlus_<xsl:value-of select="$it"/></xsl:attribute>						
						<a class="black" href="javascript:">
							<img border="0">
								<xsl:attribute name="onMouseOver">toggle('shortMenuItems_<xsl:value-of select="$it"/>');toggle('shortMenuItemsPlus_<xsl:value-of select="$it"/>');toggle('shortMenuItemsMinus_<xsl:value-of select="$it"/>');</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>plus.gif</xsl:attribute>
							</img>
						</a>
					</div>
				</td>
				<td class="tight">
					<div style="display:none">
						<xsl:attribute name="ID">shortMenuItemsMinus_<xsl:value-of select="$it"/></xsl:attribute>						
						<a class="black" href="javascript:">
							<img border="0">
								<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>minus.gif</xsl:attribute>
							</img>
						</a>
					</div>
				</td>
				<td class="tight">
					<div>
						<a class="black" href="javascript:">
							<xsl:call-template name="putText"><xsl:with-param name="key">shortMenuGroupTitle_<xsl:value-of select="$it"/></xsl:with-param></xsl:call-template>			
						</a>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div class="shortMenuLinks" style="display:none">
		<xsl:attribute name="ID">shortMenuItems_<xsl:value-of select="$it"/></xsl:attribute>
		<xsl:call-template name="printLinkItems">
			<xsl:with-param name="it"><xsl:value-of select="$it"/></xsl:with-param>
		</xsl:call-template>
	</div>
</div>

</xsl:template>


<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template name="printLinkItems">
<xsl:param name="it" />
<xsl:choose>
	<xsl:when test="$it='tasks'">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/ActualTasks/searchTasks.asp?people=<xsl:value-of select="/Doc_Webpage/whoAmI/record/field[@name='ID']/@value"/>&amp;searchMethod=Mine&amp;STATUS=REQUESTED,ACCEPTED</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">My Tasks</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/ActualTasks/searchTasks.asp?people=<xsl:value-of select="/Doc_Webpage/whoAmI/record/field[@name='ID']/@value"/>&amp;searchMethod=MyRole&amp;STATUS=REQUESTED,ACCEPTED</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Tasks for my roles</xsl:with-param></xsl:call-template>
		</a>
	</xsl:when>
	<xsl:when test="$it='objects'">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/People/searchPeople.asp?firstTime=true&amp;STATUS=CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">People</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/Companies/searchCompanies.asp?firstTime=true&amp;STATUS=CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Companies</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/Roles/searchRoles.asp?firstTime=true&amp;STATUS=CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Roles</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/Companies/searchCompanies.asp?firstTime=true&amp;STATUS=CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Companies</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/Companies/searchCompanies.asp?firstTime=true&amp;STATUS=CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Companies</xsl:with-param></xsl:call-template>
		</a>
		<br />

	</xsl:when>
	
	
	
	<xsl:when test="$it='Administration'">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/administration/assignRoleToJob.asp</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Assign Role To Job</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/administration/assignAccRecievableRole.asp</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Assign Accounts receivable roles</xsl:with-param></xsl:call-template>
		</a>
		<br />
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$http_Root"/>asp/administration/editGlobalWords.asp</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Edit Global Words</xsl:with-param></xsl:call-template>
		</a>
	</xsl:when>




</xsl:choose>

</xsl:template>

<!--##################################################
    ##  screenSwitcher                              ##
	################################################## -->
<xsl:template name="screenSwitcher">
<select>
	<xsl:attribute name="onChange">document.location='<xsl:value-of select="$path_to_top"/>asp/utilities/setScreen.asp?screen=' + this.value</xsl:attribute>
	<option value=""><xsl:call-template name="putText"><xsl:with-param name="key">Change Screen...</xsl:with-param></xsl:call-template></option>
	<option value="WORK_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Worker Screen</xsl:with-param></xsl:call-template></option>
	<option value="SHIP_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Shipper Screen</xsl:with-param></xsl:call-template></option>
	<option value="DELIVERY_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Delivery Screen</xsl:with-param></xsl:call-template></option>
<!--	<option value="ENGINEER_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Engineer Screen</xsl:with-param></xsl:call-template></option>-->
	<option value="NORMAL_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Standard Screen</xsl:with-param></xsl:call-template></option>
	<option value="ORDER_ENTRY_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Order Entry Screen</xsl:with-param></xsl:call-template></option>
	<option value="ADMIN_SCREEN"><xsl:call-template name="putText"><xsl:with-param name="key">Admin Screen</xsl:with-param></xsl:call-template></option>
</select>

</xsl:template>


</xsl:stylesheet>
