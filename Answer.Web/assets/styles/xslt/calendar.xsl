<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!--##################################################
    ## calendar                                     ##
	################################################## -->
<xsl:template match="calendar">
<!-- Outer Table is simply to get the pretty border-->
<TABLE class="calendar">
<TR>
<TD>
<TABLE class="insideCalendar">
	<TR>
		<TD class="calendarTopRow" colspan="7">
			<TABLE WIDTH="100%" BORDER="0" CELLSPACING="0" CELLPADDING="0">
				<TR>
					<TD ALIGN="right">
						<A>
							<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/calendar/calendar.asp?showYear=<xsl:value-of select="prevButtonYear/@value"/>&amp;showMonth=<xsl:value-of select="prevButtonMonth/@value"/>&amp;<xsl:call-template name="rePrintQString"><xsl:with-param name="qItems" select="/Doc_Webpage/queryString/item[not(contains('showMonth,showYear',@name))]"/></xsl:call-template></xsl:attribute>
							&lt;&lt;
						</A>
					</TD>
					<th ALIGN="center"><B><xsl:value-of select="title/@value"/></B></th>
					<td style="text-align: right;">
						<a>
							<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/calendar/calendar.asp?showYear=<xsl:value-of select="nextButtonYear/@value"/>&amp;showMonth=<xsl:value-of select="nextButtonMonth/@value"/>&amp;<xsl:call-template name="rePrintQString"><xsl:with-param name="qItems" select="/Doc_Webpage/queryString/item[not(contains('showMonth,showYear',@name))]"/></xsl:call-template></xsl:attribute>
							&gt;&gt;
						</a>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">SUN</xsl:with-param></xsl:call-template></B><br /></TD>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">MON</xsl:with-param></xsl:call-template></B><br /></TD>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">TUES</xsl:with-param></xsl:call-template></B><br /></TD>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">WEDS</xsl:with-param></xsl:call-template></B><br /></TD>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">THURS</xsl:with-param></xsl:call-template></B><br /></TD>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">FRI</xsl:with-param></xsl:call-template></B><br /></TD>
		<TD class="DayTitle"><B><xsl:call-template name="putText"><xsl:with-param name="key">SAT</xsl:with-param></xsl:call-template></B><br /></TD>
	</TR>
	<xsl:for-each select="TR">
		<tr>
			<xsl:for-each select="TD">
				<td>
					<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
					<a>
						<xsl:attribute name="href">javascript:selectMatchingOptions(document.actual_date.usemonth,'<xsl:value-of select="month/@value"/>');selectMatchingOptions(document.actual_date.useday,'<xsl:value-of select="day/@value"/>');selectMatchingOptions(document.actual_date.useyear,'<xsl:value-of select="year/@value"/>');</xsl:attribute>
						<xsl:value-of select="."/>
					</a>
				</td>
			</xsl:for-each>		
		</tr>
	</xsl:for-each>
</TABLE>
</TD>
</TR>
</TABLE>

<br />

<TABLE class="dateSearch">
<TR>
	<TD style="text-align:center;">
		<FORM METHOD="GET">
			<xsl:for-each select="/Doc_Webpage/queryString/item[not(contains('showMonth,showYear',@name))]">
				<input type="hidden">
					<xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
				</input>
				<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/><br />
			</xsl:for-each>
		
		<xsl:call-template name="monthDropDown">
			<xsl:with-param name="name">showMonth</xsl:with-param>
			<xsl:with-param name="month" select="showMonth/@value" />
		</xsl:call-template>
		<xsl:call-template name="yearDropDown">
			<xsl:with-param name="name">showYear</xsl:with-param>
			<xsl:with-param name="year" select="showYear/@value" />
		</xsl:call-template>
		<input type="submit">
			<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">showThisMonth</xsl:with-param></xsl:call-template></xsl:attribute>
		</input>
		<div style="display:none" class="hiddenForWords">
        	<xsl:call-template name="putText"><xsl:with-param name="key">showThisMonth</xsl:with-param></xsl:call-template>
        </div>
		</FORM>
	</TD>
</TR>
<tr>
	<td>
		<form name="actual_date">
			<table class="tight">
				<tr>
					<th class="tight" style="vertical-align: top;"><xsl:call-template name="putText"><xsl:with-param name="key">YEAR</xsl:with-param></xsl:call-template></th>
					<th class="tight" style="vertical-align: top;"><xsl:call-template name="putText"><xsl:with-param name="key">MONTH</xsl:with-param></xsl:call-template></th>
					<th class="tight" style="vertical-align: top;"><xsl:call-template name="putText"><xsl:with-param name="key">DAY</xsl:with-param></xsl:call-template></th>
					<th class="tight" style="vertical-align: top;"><xsl:call-template name="putText"><xsl:with-param name="key">HOUR</xsl:with-param></xsl:call-template></th>
					<th class="tight" style="vertical-align: top;"><xsl:call-template name="putText"><xsl:with-param name="key">MIN</xsl:with-param></xsl:call-template></th>
				</tr>
				<tr>
					<td class="tight">
						<xsl:call-template name="yearDropDown">
							<xsl:with-param name="name">useyear</xsl:with-param>
							<xsl:with-param name="year"><xsl:value-of select="showYear/@value"/></xsl:with-param>			
						</xsl:call-template>			
					</td>		
					<td class="tight">
						<xsl:call-template name="monthDropDown">
							<xsl:with-param name="name">usemonth</xsl:with-param>
							<xsl:with-param name="month"><xsl:value-of select="showMonth/@value"/></xsl:with-param>
						</xsl:call-template>			
					</td>
					<td class="tight">
								<xsl:call-template name="putNumberBox">
									<xsl:with-param name="pad" select="2" />
									<xsl:with-param name="top" select="31" />
									<xsl:with-param name="bottom" select="1" />
									<xsl:with-param name="name">useday</xsl:with-param>
									<xsl:with-param name="default"><xsl:value-of select="showDay/@value"/></xsl:with-param>
								</xsl:call-template>			
					</td>
					<td class="tight">
								<xsl:call-template name="putNumberBox">
									<xsl:with-param name="pad" select="2" />
									<xsl:with-param name="top" select="23" />
									<xsl:with-param name="bottom" select="0" />
									<xsl:with-param name="name">usehour</xsl:with-param>
									<xsl:with-param name="default"><xsl:value-of select="showHour/@value"/></xsl:with-param>
								</xsl:call-template>			
					</td>
					<td class="tight">
								<xsl:call-template name="putNumberBox">
									<xsl:with-param name="pad" select="2" />
									<xsl:with-param name="top" select="60" />
									<xsl:with-param name="bottom" select="0" />
									<xsl:with-param name="name">useminute</xsl:with-param>
									<xsl:with-param name="default" select="showMinute/@value"/>
								</xsl:call-template>			
					</td>
					<input type="hidden" value="00">
						<xsl:attribute name="name">usesecond</xsl:attribute>
					</input>
				</tr>
				<tr>
					<th class="tight" colspan="5">
						<input type="button">
							<xsl:attribute name="onclick">window.opener.insertDate(document.actual_date.usemonth.value + '/' + document.actual_date.useday.value + '/' + document.actual_date.useyear.value + ' ' + document.actual_date.usehour.value + ':' + document.actual_date.useminute.value + ':00','<xsl:value-of select="/Doc_Webpage/queryString/item[@name = 'field']/@value"/>','<xsl:value-of select="/Doc_Webpage/queryString/item[@name = 'form']/@value"/>');window.close();</xsl:attribute>
							<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Use This Date and Time</xsl:with-param></xsl:call-template></xsl:attribute>
						</input>
						<div style="display:none" class="hiddenForWords">
				        	<xsl:call-template name="putText"><xsl:with-param name="key">Use This Date and Time</xsl:with-param></xsl:call-template>
				        </div>
					
					</th>
				</tr>
			</table>
		</form>
	</td>
</tr>
</TABLE>

</xsl:template>


<!--##################################################
    ## yearDropDown                                 ##
	################################################## -->
<xsl:template name="yearDropDown">
<xsl:param name="name"/>
<xsl:param name="year"/>
<select>
	<xsl:attribute name="name"><xsl:value-of select="$name"/></xsl:attribute>
	<xsl:choose>
		<xsl:when test="($year) &gt; 0">
			<xsl:call-template name="putYearOption">
				<xsl:with-param name="now" select="1990" />
				<xsl:with-param name="maxYear" select="2020"/>
				<xsl:with-param name="year" select="$year"/>
			</xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="putYearOption">
				<xsl:with-param name="now" select="2003" />
				<xsl:with-param name="maxYear" select="2020"/>
				<xsl:with-param name="year" select="CurrentDate/year/@value"/>
			</xsl:call-template>
		</xsl:otherwise>
	</xsl:choose>
</select>
</xsl:template>

<!--##################################################
    ## putYearOption                                ##
	################################################## -->
<xsl:template name="putYearOption">
<xsl:param name="now"/>
<xsl:param name="maxYear"/>
<xsl:param name="year"/>
<option>
	<xsl:attribute name="value"><xsl:value-of select="$now"/></xsl:attribute>
	<xsl:if test="$now = $year">
		<xsl:attribute name="selected">selected</xsl:attribute>
	</xsl:if>
	<xsl:value-of select="$now"/>
</option>
<xsl:if test="$now &lt; $maxYear">
	<xsl:call-template name="putYearOption">
		<xsl:with-param name="now" select="$now + 1" />
		<xsl:with-param name="maxYear" select="$maxYear"/>
		<xsl:with-param name="year" select="$year"/>
	</xsl:call-template>	
</xsl:if>
</xsl:template>


<!--##################################################
    ## monthDropDown                                ##
	################################################## -->
<xsl:template name="monthDropDown">
<xsl:param name="name"/>
<xsl:param name="month"/>
<select>
	<xsl:attribute name="name"><xsl:value-of select="$name"/></xsl:attribute>
	<xsl:choose>
		<xsl:when test="($month) &gt; 0">
			<xsl:call-template name="putMonthOption">
				<xsl:with-param name="thisMonth">1</xsl:with-param>
				<xsl:with-param name="month" select="$month"/>
			</xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="putMonthOption">
				<xsl:with-param name="thisMonth">1</xsl:with-param>
				<xsl:with-param name="month" select="/Doc_Webpage/CurrentDate/month/@value"/>
			</xsl:call-template>
		</xsl:otherwise>
	</xsl:choose>
</select>
</xsl:template>


<!--##################################################
    ## putMonthOption                               ##
	################################################## -->
<xsl:template name="putMonthOption">
<xsl:param name="thisMonth"/>
<xsl:param name="month"/>
<option>
	<xsl:attribute name="value"><xsl:value-of select="$thisMonth"/></xsl:attribute>
	<xsl:if test="$thisMonth = $month">
		<xsl:attribute name="selected">selected</xsl:attribute>
	</xsl:if>
	<xsl:call-template name="putText"><xsl:with-param name="key">Month_<xsl:value-of select="$thisMonth"/></xsl:with-param></xsl:call-template>
</option>
<xsl:if test="$thisMonth &lt; 12">
	<xsl:call-template name="putMonthOption">
		<xsl:with-param name="thisMonth" select="$thisMonth + 1" />
		<xsl:with-param name="month" select="$month"/>
	</xsl:call-template>	
</xsl:if>
</xsl:template>

<!--##################################################
    ## dayDropDown                                  ##
	################################################## -->
<xsl:template name="dayDropDown">
<xsl:param name="name"/>
<xsl:param name="day"/>
<select>
	<xsl:attribute name="name"><xsl:value-of select="$name"/></xsl:attribute>
	<xsl:choose>
		<xsl:when test="($day) &gt; 0">
			<xsl:call-template name="putDayOption">
				<xsl:with-param name="now" select="1"/>
				<xsl:with-param name="day" select="$day"/>
			</xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="putDayOption">
				<xsl:with-param name="now" select="1"/>
				<xsl:with-param name="day" select="/Doc_Webpage/CurrentDate/day/@value"/>
			</xsl:call-template>
		</xsl:otherwise>
	</xsl:choose>
</select>
</xsl:template>

<!--##################################################
    ## putDayOption                                ##
	################################################## -->
<xsl:template name="putDayOption">
<xsl:param name="now"/>
<xsl:param name="day"/>
<option>
	<xsl:attribute name="value"><xsl:value-of select="$now"/></xsl:attribute>
	<xsl:if test="$now = $day">
		<xsl:attribute name="selected">selected</xsl:attribute>
	</xsl:if>
	<xsl:value-of select="$now"/>
</option>
<xsl:if test="$now &lt; 31">
	<xsl:call-template name="putDayOption">
		<xsl:with-param name="now" select="$now + 1" />
		<xsl:with-param name="day" select="$day"/>
	</xsl:call-template>	
</xsl:if>
</xsl:template>



</xsl:stylesheet>
