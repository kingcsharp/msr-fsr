<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:include href="standardPage_TEA_supplement.xsl"/>

<!--##################################################
    ##                                              ##
	################################################## -->
<xsl:template match="*">
<xsl:copy-of select="."/>
</xsl:template>


<!--##################################################
    ## obj                                          ##
	## input:
	<obj name="something" type="something">
		inside depends on the object.
	</obj>
	################################################## -->
<xsl:template match="obj">
	<xsl:choose>
		<xsl:when test="@type = 'image'">
			<xsl:call-template name="objImage">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>		
		<xsl:when test="@type = 'password'">
			<xsl:call-template name="objPassword">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>		
		<xsl:when test="@type = 'form'">
			<xsl:call-template name="objForm">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>	
		<xsl:when test="@type = 'formRow'">
			<xsl:call-template name="objFormRow">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>	
		<xsl:when test="@type = 'text'">
			<xsl:call-template name="objText">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>	
		<xsl:when test="@type = 'popUpBox'">
			<xsl:call-template name="objPopUpBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>	
		<xsl:when test="@type = 'textBox'">
			<xsl:call-template name="objTextBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>	
		<xsl:when test="@type = 'submitButton'">
			<xsl:call-template name="objSubmitButton">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>	
		<xsl:when test="@type = 'resultSet'">
			<xsl:call-template name="objResultSet">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'button'">
			<xsl:call-template name="objButton">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'tree'">
			<xsl:call-template name="objtree">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'menu'">
			<xsl:call-template name="objMenu">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'menuItem'">
			<xsl:call-template name="objMenuItem">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'dropDown'">
			<xsl:call-template name="objDropDown">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'textArea'">
			<xsl:call-template name="objTextArea">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'fileUpload'">
			<xsl:call-template name="objFileUpload">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'popUpView'">
			<xsl:call-template name="objPopUpViewer">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'row'">
			<xsl:call-template name="objRow">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'col'">
			<xsl:call-template name="objCol">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'table'">
			<xsl:call-template name="objTable">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'dateBox'">
			<xsl:call-template name="objDateBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'document'">
			<xsl:call-template name="objDocument">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'normalButton'">
			<xsl:call-template name="objNormalButton">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'textAdder'">
			<xsl:call-template name="objTextAdder">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'inputFile'">
			<xsl:call-template name="objInputFile">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
		<xsl:when test="@type = 'link'">
			<xsl:call-template name="objLink">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
	</xsl:choose>
</xsl:template>

<!--##################################################
    ## objLink                                      ##
	################################################## -->
<xsl:template name="objLink">
<xsl:param name="obj"/>
	<a>
		<xsl:if test="$obj/attribute[@name='class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name='href']">
				<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/><xsl:value-of select="$obj/attribute[@name = 'href']/@value"/></xsl:attribute>		
			</xsl:when>
			<xsl:when test="$obj/attribute[@name='specialHref']">
				<xsl:attribute name="href"><xsl:value-of select="$obj/attribute[@name = 'specialHref']/@value"/></xsl:attribute>		
			</xsl:when>
				
		</xsl:choose>
		
		<xsl:apply-templates/>
	</a>
</xsl:template>

<!--##################################################
    ## objImage                                     ##
	################################################## -->
<xsl:template name="objImage">
<xsl:param name="obj"/>
	<img>
		<xsl:call-template name="putCommonAttributes">
			<xsl:with-param name="obj" select="$obj" />
		</xsl:call-template>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$obj/attribute[@name = 'src']/@value"/></xsl:attribute>
		<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
	</img>
	<xsl:if test="$stringEdit = 'yes'">
    	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
    </xsl:if>
</xsl:template>

<!--##################################################
    ## objInputFile                                 ##
	################################################## -->
<xsl:template name="objInputFile">
<xsl:param name="obj"/>
	<input type="file" class="standard">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
	</input>

</xsl:template>



<!--##################################################
    ## objnNormalButton                             ##
	################################################## -->
<xsl:template name="objNormalButton">
<xsl:param name="obj" />
<input type="button"> 
	<xsl:if test="$obj/attribute[@name = 'class']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>	
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'style']">
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>	
	</xsl:if>
	<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
	<xsl:attribute name="onClick"><xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/></xsl:attribute>	
</input>
<xsl:if test="$stringEdit = 'yes'">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template>
</xsl:if>
</xsl:template>


<!--##################################################
    ## document                                     ##
	################################################## -->
<xsl:template name="objDocument">
<xsl:param name="obj" />
	<xsl:choose>
		<xsl:when test="contains($obj/record/field[@name = 'SERVER_PATH']/@value,'JPG') or contains($obj/record/field[@name = 'SERVER_PATH']/@value,'GIF')">
			<img>
				<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute> 
				<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute> 
				<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$obj/record/field[@name = 'ID']/@value"/></xsl:attribute>
			</img>
		</xsl:when>
		<xsl:otherwise>
			<a target="_blank" class="prodbutton">
				<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="field[@name = 'LINKED_DOC_ID']/@value"/></xsl:attribute>
				<img border="0">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>document.gif</xsl:attribute>
					<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template></xsl:attribute>
				</img>					
				<xsl:if test="$stringEdit = 'yes'">
					<xsl:call-template name="putText"><xsl:with-param name="key">OpenDocInNewWindow</xsl:with-param></xsl:call-template>                    
                   </xsl:if>
			</a>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--##################################################
    ## objPassword                                  ##
	################################################## -->
<xsl:template name="objPassword">
<xsl:param name="obj" />
<input type="password" class="norm" onfocus="select()">
	<!--name-->
	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>	
</input>
</xsl:template>


<!--##################################################
    ## objDateBox                                   ##
	## attributes:
	## name - name of the value sent up
	## notEditable - (anything) if it exits then it is not editable 
	## clearable - (anything) if it exists then it is clearable 
	## value - the current value
	## class - the class of the input box
	################################################## -->
<xsl:template name="objDateBox">
<xsl:param name="obj" />
<table class="tight">
	<tr>
		<td class="tight">
			<input type="text">
				<xsl:if test="$obj/attribute[@name = 'class']">
					<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
				</xsl:if>
				<xsl:attribute name="onBlur">if(!(validateDate(document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>))){alert('<xsl:call-template name="jPutText"><xsl:with-param name="key">DateErrMsg</xsl:with-param></xsl:call-template>');
				document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>.focus()}</xsl:attribute>
				<xsl:if test="$obj/attribute[@name = 'notEditable']">
					<xsl:attribute name="style">background-color: #FFFFFF;color: #555555;font-style: italic;margin-right: 1;margin-left: 1;</xsl:attribute>
					<xsl:attribute name="readOnly">true</xsl:attribute>
				</xsl:if>
				<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:attribute>				
				<xsl:if test="$stringEdit = 'yes'">
                	<xsl:call-template name="putText"><xsl:with-param name="key">DateErrMsg</xsl:with-param></xsl:call-template>
                </xsl:if>
			</input>
		</td>
		<xsl:if test="not($obj/attribute[@name = 'notEditable'])">
			<td class="tight" style="vertical-align:middle">
				<xsl:call-template name="putCurDate">
					<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
					<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:with-param>
				</xsl:call-template>		
			</td>		
			<td class="tight" style="vertical-align:middle">
				<xsl:call-template name="putCalendarLink">
					<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
					<xsl:with-param name="date"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param>
					<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:with-param>
				</xsl:call-template>		
			</td>
			<xsl:if test="$obj/attribute[@name = 'clearable']">
				<td class="tight" style="vertical-align:middle">
					<a class="prodbutton">
						<xsl:attribute name="onClick">document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>.value = ''</xsl:attribute>
						<img border="0">
							<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">ClearDate</xsl:with-param></xsl:call-template></xsl:attribute>
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>minus.gif</xsl:attribute>
						</img>
						<xsl:if test="$stringEdit = 'yes'">
                        	<xsl:call-template name="putText"><xsl:with-param name="key">ClearDate</xsl:with-param></xsl:call-template>
                        </xsl:if>
					</a>
				</td>
			</xsl:if>		
		</xsl:if>
	</tr>
</table>		
</xsl:template>

<!--##################################################
    ## putCurDate                                   ##
	################################################## -->
<xsl:template name="putCurDate">
<xsl:param name="form" />
<xsl:param name="field" />
<a class="prodButton">
	<xsl:attribute name="href">javascript:insertCurDate(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>)</xsl:attribute>
	<img border="0">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>nowCalendar.gif</xsl:attribute>
		<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">Todays Date</xsl:with-param></xsl:call-template></xsl:attribute>
	</img>
</a>
<xsl:if test="$stringEdit = 'yes'">
	<xsl:call-template name="putText"><xsl:with-param name="key">Todays Date</xsl:with-param></xsl:call-template>
</xsl:if>
</xsl:template>


<!--##################################################
    ## objCol                                       ##
	## output: <td> <apply-templates> </td>
	## attributes:
	## class = class of the column
	## colspan = colspan
	## style = style to modify the class
	################################################## -->
<xsl:template name="objCol">
<xsl:param name="obj" />
<td class="tight">
	<xsl:if test="$obj/attribute[@name = 'class']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>	
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'colspan']">
		<xsl:attribute name="colspan"><xsl:value-of select="$obj/attribute[@name = 'colspan']/@value"/></xsl:attribute>	
	</xsl:if>	
	<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
	<xsl:apply-templates/>
</td>
</xsl:template>

<!--##################################################
    ## objRow                                       ##
	## output: <tr> <apply-templates> </tr>
	## attributes:
	## class = class of the row
	## style = style to modify the class
	################################################## -->
<xsl:template name="objRow">
<xsl:param name="obj" />
<tr>
	<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
	<xsl:apply-templates/>
</tr>
</xsl:template>

<!--##################################################
    ## objTable                                     ##
	## output: <table> <apply-templates> </table>
	## attributes:
	## class = class of the table
	## style = style to modify the class	
	################################################## -->
<xsl:template name="objTable">
<xsl:param name="obj" />
<table class="tight">
	<xsl:if test="$obj/attribute[@name = 'class']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>	
	</xsl:if>
	<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
	<xsl:apply-templates/>
</table>
</xsl:template>

<!--##################################################
    ## objPopUpViewer                               ##
	## output:
	## attributes:
	## class = class of the select if no class the class= norm
	## style = style to modify the class
	## defaultValue = value of the default when there are no records
	## defaultShow = Show for the default when there are no records
	## defaultPutText = if this attribute exists then use put text
	## actualPutText = if this attribute exists then use put text
	## extraShowPutText = if this exists then use Put Text For the Extra Show
	## input: 
		<obj name="popupviewer">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="defaultPutText" value="whatever">
			<attribute name="actualPutText" value="whatever">
			<attribute name="extraShowPutText" value="whatever">
			<data>
				<record>
					<field name="SHOW"></field>
					<field name="VALUE"></field>
					<field name="EXTRASHOW"></field>
				</record>
			</data>
		</obj>
	################################################## -->
<xsl:template name="objPopUpViewer">
	<xsl:param name="obj" />
	<select multiple="multiple" size="1" style="color:#505050;font-style: italic;">
		<xsl:if test="$obj/attribute[@name = 'multiple']">
			<xsl:attribute name="multiple"><xsl:value-of select="$obj/attribute[@name = 'multiple']/@value"/></xsl:attribute>
		</xsl:if>	 
		<xsl:if test="$obj/attribute[@name = 'class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
		</xsl:if>
		<!--now check to see if we use the default value-->
		<xsl:choose>
			<xsl:when test="$obj/data/useDefault or not($obj/data/record)">
				<!--We are using the default value so go ahead and just get the info from the attributes-->
				<option>
					<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'defaultPutText']">
							<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='defaultShow']/@value"/></xsl:with-param></xsl:call-template>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$obj/attribute[@name='defaultShow']/@value"/>						
						</xsl:otherwise>
					</xsl:choose>
				</option>
			</xsl:when>
			<xsl:otherwise>
				<xsl:for-each select="$obj/data/record">
					<option>
						<xsl:attribute name="value"><xsl:value-of select="field[@name = 'VALUE']/@value"/></xsl:attribute>
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'actualPutText']">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'SHOW']/@value"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="field[@name = 'SHOW']/@value"/>					
							</xsl:otherwise>
						</xsl:choose>
						<xsl:text> </xsl:text>
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'extraShowPutText']">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'EXTRA_SHOW']/@value"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="field[@name = 'EXTRA_SHOW']/@value"/>				
							</xsl:otherwise>
						</xsl:choose>					
					</option>
				</xsl:for-each>
			</xsl:otherwise>
		</xsl:choose>
	</select>
	<script language="javascript">
		arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
	</script>	

</xsl:template>

<!--##################################################
    ## objTextArea                                  ##
	## output: <textArea></textarea>
	## attributes:
	## class = class of the select if no class the class= norm
	## style = style to modify the class
	## defaultValue = value of the default when there are no records
	## defaultShow = Show for the default when there are no records
	## putText = if this attribute exists then use put text
	## extraShowPutText = if this exists then use Put Text For the Extra Show
	## input: 
		<obj name="popupviewer">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="putText" value="whatever">
			<attribute name="size" value="number of rows">
			<attribute name="cols" value="number of cols">
			<attribute name="name" value="Name of the Text Area">
			<attribute name="value" value="Value to go in the TextArea">
		</obj>
	################################################## -->
<xsl:template name="objTextArea">
<xsl:param name="obj"/>
<textarea onfocus="select()" ondblclick="storeCaret(this)"
	onselect="storeCaret(this);" onclick="storeCaret(this)" onkeyup="storeCaret(this)" onmouseup="storeCaret(this)"> 
	<xsl:attribute name="onBlur">lastTextArea =  'document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>';</xsl:attribute>
	<xsl:if test="$obj/attribute[@name = 'notEditable']">
		<xsl:attribute name="readOnly">true</xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'ondblclick']">
		<xsl:attribute name="ondblclick">storeCaret(this);<xsl:value-of select="$obj/attribute[@name = 'ondblclick']/@value"/></xsl:attribute>
	</xsl:if>

	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
	<xsl:attribute name="rows"><xsl:value-of select="$obj/attribute[@name = 'size']/@value"/></xsl:attribute>
	<xsl:attribute name="cols"><xsl:value-of select="$obj/attribute[@name = 'cols']/@value"/></xsl:attribute>
	<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'notEditable']/@value">
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/>;background-color: #EEEEEE;</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
		</xsl:otherwise>		
	</xsl:choose>

	<xsl:choose>
		<xsl:when test="($obj/attribute[@name = 'putText'])">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$obj/attribute[@name = 'value']/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</textarea>
<xsl:if test="$obj/attribute[@name='setDefault'] or $obj/attribute[@name='onLoadFocus']">
	<script language="javascript">
		objFocusOn = document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>
	</script>
</xsl:if>
</xsl:template>

<!--##################################################
    ## objDropDown                                  ##
	## output: <select></select>
	## attributes:
	## class = class of the select if no class the class= norm
	## style = style to modify the class
	## matchDataSize = grow the box with the number of rows
	## defaultValue = value that should be selected when we first open the drop down
	## defaultPutText = if this attribute exists then use put text for the show
	## actualPutText = if this attribute exists then use put text for the show
	## input: 
		<obj type="objDropDown">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="defaultPutText" value="whatever">
			<attribute name="actualPutText" value="whatever">
			<attribute name="matchDataSize" value="anything">
			<attribute name="defaultValue" value="value">
			<attribute name="notEditable" value="wahtever" />
			<defaultData>
				<data value="value" show="show"/> 
			<defaultData>
			OR
			<data>
				<record>
					<field name="XXX" value="CCC" />
				</record> 
			<data>
		</obj>	
	################################################## -->
<xsl:template name="objDropDown">
<xsl:param name="obj"/>
	<select>
	<!--Attributes-->
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name = 'specialDrop']/@value = 'ttCat'">
				<xsl:attribute name="onchange">
					disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");
					clearSelectBox(document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'verbName']/@value"/>);
					clearSelectBox(document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'nounName']/@value"/>);											
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="onChange">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");<xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/></xsl:attribute>			
			</xsl:otherwise>		
		</xsl:choose>
		

		<xsl:if test="$obj/attribute[@name = 'notEditable']">
			<xsl:attribute name="disabled">true</xsl:attribute>
			<xsl:attribute name="style">background-color:#EEEEEE</xsl:attribute>
		</xsl:if>
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
		
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name = 'class']">
				<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="class">norm</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:if test="$obj/attribute[@name = 'matchDataSize']">
			<xsl:choose>
				<xsl:when test="not($obj/data/record)">
					<xsl:attribute name="size"><xsl:value-of select="count(defaultData/data)"/></xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="size"><xsl:value-of select="count(data/record)"/></xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
		
	<!--Options-->	
		<xsl:choose>
			<xsl:when test="not($obj/data/record)">
				<xsl:for-each select="defaultData/data">
					<option>
						<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
						<xsl:if test="@value = $obj/attribute[@name = 'defaultValue']/@value"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'defaultPutText']">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@show"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="@show"/>					
							</xsl:otherwise>
						</xsl:choose>
					</option>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="defVal"><xsl:value-of select="string($obj/attribute[@name = 'defaultValue']/@value)"/></xsl:variable>				
				<xsl:for-each select="$obj/data/record">
					<xsl:variable name="myVal"><xsl:value-of select="string(field[@name = 'value']/@value)"/></xsl:variable>
					<option>
						<xsl:if test="string($myVal) = string($defVal)"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>
						<xsl:attribute name="value"><xsl:value-of select="field[@name = 'value']/@value"/></xsl:attribute>
<!--						is *<xsl:value-of select="$myVal"/>* = *<xsl:value-of select="$defVal"/>*
						<xsl:if test="$myVal = $defVal"> Yes They are the same</xsl:if>-->

						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'actualPutText'] or $obj/attribute[@name = 'usePutText']">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'show']/@value"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="field[@name = 'show']/@value"/>					
							</xsl:otherwise>
						</xsl:choose>
					</option>
				</xsl:for-each>
			</xsl:otherwise>
		</xsl:choose>
	</select>
</xsl:template>

<!--##################################################
    ## objMenu                                      ##
	## attributes:
	## width: width of the menu table
	## class: table class
	## style: style of the table
	## input:
		<obj type="menu">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="width" value="number">
			<menuItems>
		</obj>	
	
	################################################## -->
<xsl:template name="objMenu">
	<xsl:param name="obj"/>
	<table class="standard">
		<xsl:attribute name="width"><xsl:value-of select="$obj/attribute[@name = 'width']/@value"/></xsl:attribute>
		<xsl:if test="$obj/attribute[@name = 'class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>		
		</xsl:if>
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
		<xsl:apply-templates select="$obj/node()"/>
	</table>
</xsl:template>

<!--##################################################
    ## objMenuItem                                  ##
	## attributes:
	## pageID (optional): this will be the pageID that we jump to with this menu Item
	## url (Optional): This will be the actual URL we will jumpt to
	## style: style of the column
	## class: class of the column
	## input:
		<obj type="objMenuItem">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="width" value="number">
		</obj>	

		output:
		<tr>
			<td>
				Value of: objName_Name
			</td>
		</tr>
	################################################## -->
<xsl:template name="objMenuItem">
	<xsl:param name="obj"/>
	<tr>
		<td>
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'class']">
					<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="class">standard</xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
			
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
			<img>
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
				<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/@ID"/>_info</xsl:with-param></xsl:call-template></xsl:attribute>
			</img>
			<xsl:if test="$stringEdit = 'yes'">
            	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/@ID"/>_info</xsl:with-param></xsl:call-template>
            </xsl:if>

			<xsl:text> </xsl:text>
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'pageID']">
					<a>
						<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=<xsl:value-of select="$obj/attribute[@name = 'pageID']/@value"/></xsl:attribute>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/@ID"/>_Name</xsl:with-param></xsl:call-template>
					</a>
				</xsl:when>
				<xsl:when test="$obj/attribute[@name = 'url']">
					<a>
						<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/><xsl:value-of select="$obj/attribute[@name = 'url']/@value"/></xsl:attribute>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/@ID"/>_Name</xsl:with-param></xsl:call-template>
					</a>
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/@ID"/>_Name</xsl:with-param></xsl:call-template>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:for-each select="menuCounter">
				<xsl:if test="@value &gt; 0">
					<br />
					<nobr>
						<xsl:choose>
							<xsl:when test="not(pageLink)">
									<span style="color:red">
										<xsl:if test="@class">
											<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>					
										</xsl:if>
										<xsl:if test="@style">
											<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>				
										</xsl:if>
										<xsl:if test="@name">
											<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@name"/></xsl:with-param></xsl:call-template>
										</xsl:if>
										<xsl:value-of select="@value"/>
									</span>
							</xsl:when>
							<xsl:otherwise>
									<a class="pageLink">
										<xsl:attribute name="href"><xsl:value-of select="pageLink"/></xsl:attribute>
										<xsl:if test="@class">
											<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>					
										</xsl:if>
										<xsl:if test="@style">
											<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>				
										</xsl:if>
										<xsl:if test="@name">
											<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@name"/></xsl:with-param></xsl:call-template>
										</xsl:if>
										<xsl:value-of select="@value"/>
									</a>
							</xsl:otherwise>										
						</xsl:choose>
					</nobr>
				</xsl:if>
			</xsl:for-each>
		</td>
	</tr>
</xsl:template>

<!--##################################################
    ## objImageButton                               ##
	## attributes:
	## href: where the button takes us
	## image: image
	## value: value to print in the alt of the image
	## class: class of the a
	## style: style of the a
	## input:
		<obj type="imageButton">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="src" value="image path from the root">
			<attribute name="value" value="alt message">
		</obj>	

		output:
		<tr>
			<td>
				Value of: objName_Name
			</td>
		</tr>
	################################################## -->	
<xsl:template name="objImageButton">
<xsl:param name="obj" />
<a class="prodbutton">
	<xsl:if test="$obj/attribute[@name = 'class']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'style']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:attribute name="href"><xsl:value-of select="$obj/attribute[@name = 'href']/@value"/></xsl:attribute>	
	<img border="0">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$obj/attribute[@name = 'image']/@value"/></xsl:attribute>
		<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
	</img>
</a>
	<xsl:if test="$stringEdit = 'yes'">
    	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
    </xsl:if>
</xsl:template>

<!--##################################################
    ## objButton                                    ##
	## attributes:
	## href: where the button takes us
	## image: image  if image then we call image button
	## value: value to print in the alt of the image or on the button
	## class: class of the a in image and class of the input on regular
	## style: style of the a in image and style of the input on regular
	## jumpTo: where to jumpto using the button
	## qString: if exists then put the Qstring info
	## input:
		<obj type="button">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="width" value="number">
			<attribute name="specialButton" value="number">
			<qItem name="something" value="something" />
			<qItem name="something" value="something" />
			<qItem name="something" value="something" />
		</obj>	
		output:
		<input type="button" onclick="document.location = 'jumpto?qitems'" />
	################################################## -->	
<xsl:template name="objButton">
<xsl:param name="obj" />
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'image']">
			<xsl:call-template name="objImageButton">
				<xsl:with-param name="obj" select="$obj" />
			</xsl:call-template>
		</xsl:when>
		
		<xsl:when test="$obj/attribute[@name = 'specialButton']">
			<input type="button">
				<xsl:choose>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'TakeBackButton'">
						<xsl:attribute name="onclick">
							javascript:if
							(ConfirmButton
							('<xsl:call-template name="jPutText"><xsl:with-param name="key">Are You Sure you want to take it back</xsl:with-param></xsl:call-template>'))
							{document.location='<xsl:value-of select="$path_to_top"/>asp/ActualTasks/takeTaskBack.asp?ActualTaskID=<xsl:value-of select="$obj/attribute[@name = 'taskID']/@value"/>&amp;sessionID=<xsl:value-of select="$obj/attribute[@name = 'sessionID']/@value"/>'}
						</xsl:attribute>
					</xsl:when>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'deleteButton'">
						<xsl:attribute name="onclick">
							javascript:if
							(ConfirmButton
							('<xsl:call-template name="jPutText"><xsl:with-param name="key">Are You Sure you want to delete it?</xsl:with-param></xsl:call-template>'))
							{document.location='<xsl:value-of select="$path_to_top"/>asp/ActualTasks/deleteTask.asp?ActualTaskID=<xsl:value-of select="$obj/attribute[@name = 'taskID']/@value"/>&amp;sessionID=<xsl:value-of select="$obj/attribute[@name = 'sessionID']/@value"/>&amp;redirectTo=<xsl:value-of select="$obj/attribute[@name = 'referer']/@value"/>'}
						</xsl:attribute>
					</xsl:when>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'AcceptButton'">
						<xsl:attribute name="onclick">
							javascript:if
							(ConfirmButton
							('<xsl:call-template name="jPutText"><xsl:with-param name="key">Are You Sure You WANT TO ACCEPT THIS TASK?</xsl:with-param></xsl:call-template>'))
							{document.location='<xsl:value-of select="$path_to_top"/>asp/ActualTasks/acceptATask.asp?ActualTaskID=<xsl:value-of select="$obj/attribute[@name = 'taskID']/@value"/>&amp;sessionID=<xsl:value-of select="$obj/attribute[@name = 'sessionID']/@value"/>'}
						</xsl:attribute>
					</xsl:when>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'RejectButton'">
						<xsl:attribute name="onclick">
							javascript:if
							(ConfirmButton
							('<xsl:call-template name="jPutText"><xsl:with-param name="key">Are You Sure You WANT TO Reject THIS TASK?</xsl:with-param></xsl:call-template>'))
							{document.location='<xsl:value-of select="$path_to_top"/>asp/ActualTasks/rejectATask.asp?ActualTaskID=<xsl:value-of select="$obj/attribute[@name = 'taskID']/@value"/>&amp;sessionID=<xsl:value-of select="$obj/attribute[@name = 'sessionID']/@value"/>'}
						</xsl:attribute>
					</xsl:when>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'ForwardButton'">
						<xsl:attribute name="onclick">
							forwardTaskCheck(document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>,'<xsl:call-template name="jPutText"><xsl:with-param name="key">Can not forward to a group and a person</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">You need to pick a Role or Person to forward to.</xsl:with-param></xsl:call-template>','<xsl:value-of select="$path_to_top"/>')
						</xsl:attribute>
					</xsl:when>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'AddKPIButton'">
						<xsl:attribute name="onClick">
							if (ConfirmButton('<xsl:call-template name="jPutText"><xsl:with-param name="key">This task will be saved and then you will be taken to the page to add KPIs</xsl:with-param></xsl:call-template>'))
							{
							escapeAllSelects();
							if (validateEditTaskForm(document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>,'<xsl:call-template name="jPutText"><xsl:with-param name="key">Submitting This Task Will Close It</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">No Requestor</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">EnterADescription</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">YouCanNotAssignBothAPersonAndARole</xsl:with-param></xsl:call-template>'))
								{
								document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>.redirectTo.value = '../StandardPage.asp?pageID=AddKPI&amp;ID=<xsl:value-of select="$obj/attribute[@name = 'taskID']/@value"/>';
								document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>.submit();
								}
							}
						</xsl:attribute>
					</xsl:when>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'viewWorkFlow'">
						<xsl:attribute name="onClick">
							if (document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>.WorkFlow.value)
								{
								var viewWindow = window.open('<xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=viewWorkFlow&amp;ID=' + document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>.WorkFlow.value,'PopUpWindow','width=500,height=400,location=yes,toolbar=no,resizable=yes,scrollbars=yes')
								}
						</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
				<xsl:if test="$stringEdit = 'yes'">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
                </xsl:if>
			</input>
			<xsl:if test="$stringEdit = 'yes'">
            	<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure you want to take it back</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure you want to delete it?</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure You WANT TO ACCEPT THIS TASK?</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure You WANT TO Reject THIS TASK?</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Can not forward to a group and a person</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">You need to pick a Role or Person to forward to.</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">This task will be saved and then you will be taken to the page to add KPIs</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Submitting This Task Will Close It</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">No Requestor</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">EnterADescription</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">YouCanNotAssignBothAPersonAndARole</xsl:with-param></xsl:call-template>
            </xsl:if>
		</xsl:when>

		<xsl:otherwise>
			<span class="button">
			<a class="button">
				<xsl:if test="$obj/attribute[@name = 'class']">
					<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
				</xsl:if>
				
				<xsl:if test="$obj/attribute[@name='jumpTo']">
					<xsl:attribute name="href"><xsl:value-of select="$obj/attribute[@name='jumpTo']/@value"/><xsl:if test="$obj/attribute[@name='qString']"><xsl:for-each select="$obj/qItem"><xsl:value-of select="./@name"/>=<xsl:call-template name="putOneQitem"><xsl:with-param name="name" select="./@name" /></xsl:call-template>&amp;</xsl:for-each></xsl:if>
					</xsl:attribute>
				</xsl:if>
				<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
			</a>
			</span>
			
			<xsl:if test="$stringEdit = 'yes'">
				<xsl:choose>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'TakeBackButton'">
						<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure you want to take it back</xsl:with-param></xsl:call-template>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
					</xsl:otherwise>
				</xsl:choose>
            </xsl:if>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--##################################################
    ## putOneQitem                                  ##
	## prints a Querystring Item ##
	## parameters:
	##	name - the name of the Querystring Item to print
	## looks in the XML of the querysting to get the value
	################################################## -->
<xsl:template name="putOneQitem"><xsl:param name="name" /><xsl:value-of select="//queryString/item[@name = $name]/@value"/></xsl:template>

<!--##################################################
    ## objResultSet                                 ##
	## class: class of the table of the resultset
	## style: style of the table of the resultset
	## jumpTo: where to jumpto using the button
	## qString: if exists then put the Qstring info
	## noPaging: if exists then do not put the paging information.  Just print all results.
	## input:
		<obj type="resulset">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="noPaging" value="whatever">
			<attribute name="pagingAtBottom" value="whatever">
			<attribute name="standardSortButton" value="whatever" text="Push to Return To standard Search" sortBy = "fields to sort by">
			
			<columns [noTitles]>
				<column 
					putText="true" pageID="editRole" sortable="Anything" title="Title" field="ResultSet Field" class="whatever" special="Special Processing Name"/> 
					pageID = page to go ot using this data.  The recordset turns into the querystring
					sortable = if it exists then it is a sortable column based on the field
					title = Title of the column
					field = field in the result set to print in this column
					class = the class of this column.  the title will be in a th and the results in a td
					special = if there is something sepecial to do it needs to be specified here and then the action to take for something special needs to be defined in the result set row.
					putText = if exists then use putText for the data in the column
					<specialPutText> This is for using put text only when a rule is met
						<rule type="field">  a rule with type of field means that a field has to have a certain value to use put text
							<field/> the field whose value must be checked
							<value/> the value of the field to check
						</rule>
					</specialPutText>
			</columns>
			<data>
				<record>
					<field @name="name" @value="value"/>
					<field @name="name" @value="value"/>
					<field @name="name" @value="value"/>
				</record>
			</data>
		</obj>	
	################################################## -->
<xsl:template name="objResultSet">
<xsl:param name="obj" />
<xsl:choose>
	<xsl:when test="data/record">
		<xsl:if test="not($obj/attribute[@name='noPaging']) and not($obj/attribute[@name = 'pagingAtBottom'])">
			<xsl:call-template name="paging">
				<xsl:with-param name="all" select="$obj/data/pageData"/>
				<xsl:with-param name="qItems" select="//queryString/item[@name != 'curPage']" />
				<xsl:with-param name="prevPhrase">PrevArrow</xsl:with-param>
				<xsl:with-param name="nextPhrase">NextArrow</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
		<table class="resultSet">
			<xsl:if test="$obj/attribute[@name = 'class']">
				<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>			
			</xsl:if>
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>

			
			<xsl:if test="$obj/columns/column/@sortable">
				<tr>
					<td align="left">
						<xsl:attribute name="colspan"><xsl:value-of select="count($obj/columns/column)"/></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">Click Col Heading to sort</xsl:with-param></xsl:call-template>			
					<xsl:if test="$obj/attribute[@name = 'standardSortButton']">
						<xsl:call-template name="putText"><xsl:with-param name="key">OR Do This </xsl:with-param></xsl:call-template>
						<xsl:call-template name="putColumnSearcher">
							<xsl:with-param name="normalSort">true</xsl:with-param>
							<xsl:with-param name="name"><xsl:value-of select="$obj/attribute[@name = 'standardSortButton']/@text"/></xsl:with-param>
							<xsl:with-param name="sortBy"><xsl:value-of select="$obj/attribute[@name = 'standardSortButton']/@sortBy"/></xsl:with-param>
						</xsl:call-template>
						<xsl:call-template name="spacer" />
					</xsl:if>
					</td>

				</tr>			
			</xsl:if>
			<xsl:if test="$obj/tableHeader">
				<tr>
					<th class="resultSet">
						<xsl:if test="$obj/tableHeader/@class">
								<xsl:attribute name="class"><xsl:value-of select="$obj/tableHeader/@class"/></xsl:attribute>
							</xsl:if>
							<xsl:if test="$obj/tableHeader/@style">
								<xsl:attribute name="style"><xsl:value-of select="$obj/tableHeader/@style"/></xsl:attribute>
							</xsl:if>					
						<xsl:attribute name="colspan"><xsl:value-of select="count($obj/columns/column)"/></xsl:attribute>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/tableHeader"/></xsl:with-param></xsl:call-template>
					</th>
				</tr>
			</xsl:if>

			<xsl:if test="not($obj/columns/@noTitles)">
				<tr>
					<xsl:for-each select="$obj/columns/column">
						<th class="resultSet">
							<xsl:if test="@class">
								<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
							</xsl:if>
							<xsl:if test="@style">
								<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>
							</xsl:if>
							
							<table class="tight">
								<tr>
									<xsl:if test="@titleHelpIcon">
										<td class="tight" valign="middle">
											<img align="middle">
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
												<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param></xsl:call-template></xsl:attribute>
											</img>
											<xsl:if test="$stringEdit = 'yes'">
                                            	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param></xsl:call-template>
                                            </xsl:if>
										</td>
									</xsl:if>
									<xsl:choose>
										<xsl:when test="@sortable='yes'">
											<xsl:call-template name="putColumnSearcher">
												<xsl:with-param name="name"><xsl:value-of select="@title"/></xsl:with-param>
												<xsl:with-param name="sortBy"><xsl:choose>
														<xsl:when test="@sortName">
															<xsl:value-of select="@sortName"/>
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="@field"/>
														</xsl:otherwise>
													</xsl:choose>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:when>
										<xsl:otherwise>
											<td>
												<nobr>
													<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@title"/></xsl:with-param></xsl:call-template>
												</nobr>
											
											</td>
										</xsl:otherwise>
									</xsl:choose>
								</tr>
							</table>
						</th>
					</xsl:for-each>
				</tr>
			</xsl:if>
			<xsl:for-each select="$obj/data/record">

				<!--Any Record could have post data to add to the normal stuff-->
				<xsl:apply-templates select="preRecordData"/>

				<tr>
					<xsl:call-template name="printResultSetRow">
						<xsl:with-param name="resultSet" select="$obj" />
						<xsl:with-param name="record" select="." />
						<xsl:with-param name="position" select="position()" />
					</xsl:call-template>
				</tr>	
				
				<!--Any Record could have post data to add to the normal stuff-->
				<xsl:apply-templates select="postRecordData"/>
				
			</xsl:for-each>
		</table>
		<xsl:if test="not($obj/attribute[@name='noPaging']) and ($obj/attribute[@name = 'pagingAtBottom'])">
			<xsl:call-template name="paging">
				<xsl:with-param name="all" select="$obj/data/pageData"/>
				<xsl:with-param name="qItems" select="//queryString/item[@name != 'curPage']" />
				<xsl:with-param name="prevPhrase">PrevArrow</xsl:with-param>
				<xsl:with-param name="nextPhrase">NextArrow</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="putText"><xsl:with-param name="key">No Matching Data Please Change Search Criteria</xsl:with-param></xsl:call-template>
	</xsl:otherwise>
	
</xsl:choose>
</xsl:template>

<!--##################################################
    ## postRecordData			                    ##
	################################################## -->
<xsl:template match="postRecordData">
	<xsl:apply-templates/>
</xsl:template>
<!--##################################################
    ## preRecordData			                    ##
	################################################## -->
<xsl:template match="preRecordData">
	<xsl:apply-templates/>
</xsl:template>



<!--##################################################
    ## printResultSetRow                            ##
	## params:
		resultset = the resulSet Object
		record = this row in the recordset
		position = the poition() of this record
		
		refer to resultset for more details
	################################################## -->
<xsl:template name="printResultSetRow">
<xsl:param name="resultSet" />
<xsl:param name="record" />
<xsl:param name="position" />
<xsl:for-each select="$resultSet/columns/column">
	<td>
		<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
		<xsl:if test="not(contains('actions,counter',@class))">
			<xsl:attribute name="style"><xsl:call-template name="rowStyle"><xsl:with-param name="rec" select="$record" /></xsl:call-template></xsl:attribute>	
		</xsl:if> 
		<xsl:choose>
<!--Action Column -->		
			<xsl:when test="@class = 'actions'">
				<table class="tight">
					<tr>
						<xsl:for-each select="$resultSet/rowActionObjects/obj[@type='rowActionButton']">
							<td class="tight">
								<xsl:call-template name="objRowActionButton">
									<xsl:with-param name="obj" select="." />
									<xsl:with-param name="record" select="$record" />										
								</xsl:call-template>				
							</td>
						</xsl:for-each>
					</tr>
				</table>
			</xsl:when>
<!--Counter Column -->			
			<xsl:when test="@class = 'counter'">
				<xsl:attribute name="style">text-align:right;color:silver;font-weight:300;</xsl:attribute>
				<xsl:value-of select="$record/field[@name = 'actualCount']/@value"/>
			</xsl:when>
			<xsl:otherwise>			
				<xsl:choose>
<!--Special Column-->				
					<xsl:when test="@special">
						<xsl:choose>
							<xsl:when test="@special = 'approvalItem'">
								<xsl:call-template name="printApprovalItem">
									<xsl:with-param name="record" select="$record" />
								</xsl:call-template>
							</xsl:when>
							<xsl:when test="@special = 'insStep'">
								<xsl:copy-of select="$record/value/root"/>
							</xsl:when>
							<xsl:when test="@special = 'actionPlanHierarchy'">
								<xsl:call-template name="putActionPlanHierarchy">
									<xsl:with-param name="record" select="$record"/>
									<xsl:with-param name="pageID" select="@pageID"/>
								</xsl:call-template>
							</xsl:when>
							<xsl:when test="@special = 'EMAIL'">
								<a>
									<xsl:attribute name="href">
									mailto:<xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@field"/>
										<xsl:with-param name="record" select="$record"/>
									</xsl:call-template></xsl:attribute>
									<xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@field"/>
										<xsl:with-param name="record" select="$record"/>
									</xsl:call-template>	
								</a>
							</xsl:when>
							<xsl:when test="@special = 'requestStatus'">
							<xsl:variable name="status"><xsl:call-template name="printOneItem"><xsl:with-param name="field" select="@field"/><xsl:with-param name="record" select="$record"/></xsl:call-template></xsl:variable>
								<xsl:choose>
									<xsl:when test="$status = 'REJECTED'">
										<img>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
											<xsl:attribute name="alt"><xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@field2"/>
										<xsl:with-param name="record" select="$record"/>
								</xsl:call-template></xsl:attribute>
										</img>
									</xsl:when>
								</xsl:choose>
								<xsl:value-of select="$status"/>
							</xsl:when>
							<xsl:when test="@special = 'TTStatus'">
								<xsl:variable name="status"><xsl:call-template name="printOneItem"><xsl:with-param name="field">STATUS</xsl:with-param><xsl:with-param name="record" select="$record"/></xsl:call-template></xsl:variable>
								<xsl:choose>
									<xsl:when test="$status = 'TP_DENIED'">
										<img>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
											<xsl:attribute name="alt"><xsl:value-of select="$record/denialReason/record/field[@name = 'DENIAL_REASON']/@value"/></xsl:attribute>
										</img>
									</xsl:when>
								</xsl:choose>
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$status"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:variable name="specName" select="@special"/>
								<xsl:apply-templates select="$record/specialColumn[@name = $specName]"/>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
<!--This column jumps somewhere-->									
					<xsl:when test="@pageID != ''">
						<a>
							<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/StandardPage.asp?pageID=<xsl:value-of select="@pageID"/>&amp;
								<xsl:choose>
									<xsl:when test="./translateRecsToQuery">
										<xsl:for-each select="translateRecsToQuery/item">
											<xsl:value-of select="@show"/>=<xsl:call-template name="printOneItem">
												<xsl:with-param name="field" select="@field"/>
												<xsl:with-param name="record" select="$record"/>
											</xsl:call-template>
										</xsl:for-each>
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="putRecordAsQueryString">
											<xsl:with-param name="record" select="$record" />
										</xsl:call-template>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<xsl:call-template name="printOneItem">
								<xsl:with-param name="field" select="@field"/>
								<xsl:with-param name="record" select="$record"/>
							</xsl:call-template>
						</a>					
					</xsl:when>
<!--This column has an HREF -->
					<xsl:when test="@href != ''">
						<a>
							<xsl:attribute name="href"><xsl:value-of select="@href"/>?
								<xsl:if test="./queryString">
									<xsl:value-of select="./querystring"/>&amp;
								</xsl:if>
								<xsl:choose>
									<xsl:when test="./translateRecsToQuery">
										<xsl:for-each select="translateRecsToQuery/item">
											<xsl:value-of select="@show"/>=<xsl:call-template name="printOneItem">
												<xsl:with-param name="field" select="@field"/>
												<xsl:with-param name="record" select="$record"/>
											</xsl:call-template>
										</xsl:for-each>
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="putRecordAsQueryString">
											<xsl:with-param name="record" select="$record" />
										</xsl:call-template>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<xsl:call-template name="printOneItem">
								<xsl:with-param name="field" select="@field"/>
								<xsl:with-param name="record" select="$record"/>
							</xsl:call-template>
						</a>					
					</xsl:when>
<!--This column uses put text if certain rules are met otherwise just put the value-->
					<xsl:when test="specialPutText">
						<xsl:call-template name="specialPutTextForColumn">
							<xsl:with-param name="specialPutText" select="specialPutText" />
							<xsl:with-param name="column" select="." />
							<xsl:with-param name="record" select="$record"/>
						</xsl:call-template>
					</xsl:when>
					
<!--This column uses the putText function for its result-->					
					<xsl:when test="@usePutText">
						<xsl:call-template name="putText">
							<xsl:with-param name="key"><xsl:call-template name="printOneItem">
								<xsl:with-param name="field" select="@field"/>
								<xsl:with-param name="record" select="$record"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:when>
<!--This column is just a straight up data column dont do anything special-->					
					<xsl:otherwise>
						<xsl:call-template name="printOneItem">
							<xsl:with-param name="field" select="@field"/>
							<xsl:with-param name="record" select="$record"/>
						</xsl:call-template>						
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</td>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ## specialPutTextForColumn                      ##
	################################################## -->
<xsl:template name="specialPutTextForColumn">
<xsl:param name="specialPutText"/>
<xsl:param name="column"/>
<xsl:param name="record"/>



</xsl:template>


<!--##################################################
    ## specialColumn                                ##
	################################################## -->
<xsl:template match="specialColumn">
<xsl:apply-templates/>
</xsl:template>

<!--##################################################
    ## hierarchy                                    ##
	################################################## -->
<xsl:template match="hierarchy">
	<nobr><xsl:apply-templates/></nobr>
</xsl:template>

<!--##################################################
    ## hierParent                                   ##
	################################################## -->
<xsl:template match="hierParent">
	<xsl:apply-templates select="hierParent"/>
	<xsl:if test="hierParent"><xsl:text> / </xsl:text></xsl:if>
	<xsl:variable name="curParent" select="."/>
	<xsl:for-each select="./ancestor::hierarchy/field">
		<xsl:variable name="myField" select="@field"/>
		<xsl:variable name="myVal" select="$curParent/field[@name=$myField]/@value"/>
		<!--Now we need to print a link or no link if we have a page ID-->
		<xsl:choose>
			<xsl:when test="@pageID != ''">
				<a>
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=<xsl:value-of select="@pageID"/>&amp;
						<xsl:for-each select="translateRecsToQuery/item">
							<xsl:variable name="thisField" select="@field"/>
							<xsl:value-of select="@show"/>=<xsl:value-of select="$curParent/field[@name=$thisField]/@value"/>&amp;
						</xsl:for-each>
					</xsl:attribute>
					<xsl:value-of select="$myVal"/>
				</a>
			</xsl:when>
			<xsl:when test="@picPageID != ''">
				<a>
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=<xsl:value-of select="@picPageID"/>&amp;
						<xsl:for-each select="translateRecsToQuery/item">
							<xsl:variable name="thisField" select="@field"/>
							<xsl:value-of select="@show"/>=<xsl:value-of select="$curParent/field[@name=$thisField]/@value"/>&amp;
						</xsl:for-each>
					</xsl:attribute>
					<img>
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="@pictureFile"/></xsl:attribute>
						<xsl:attribute name="alt"><xsl:value-of select="@alt"/></xsl:attribute>
						<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>
						<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
					</img>
				</a>
			</xsl:when>
		</xsl:choose>
		
	</xsl:for-each>
</xsl:template>



<!--##################################################
    ## putRowStyle                                  ##
	################################################## -->
<xsl:template name="rowStyle">
<xsl:param name="rec" />
	<xsl:choose>
		<xsl:when test="$rec/unOpened">
			background-color:#B4E4E4;
		</xsl:when>
		<xsl:when test="$rec/kpis/kpi[@name = 'CLOSED']/@value='true'">
			background-color:#A4F2BC;			
		</xsl:when>
		<xsl:when test="$rec/kpis/kpi/@status = 'FAILING'">
			background-color:#F7B7B9;
		</xsl:when>
		<xsl:when test="$rec/kpis/kpi/@status = 'WARNING'">
			background-color:#FFFC9E;
		</xsl:when>
	</xsl:choose>
</xsl:template>

<!--##################################################
    ## actionPlanHierarchy                          ##
	##input:
		record = 
			<record>
				<parentTree>
					<parent>
						<parent>
							<parent/>
						</parent>
					</parent>
				</parentTree>
			</record>
	################################################## -->
<xsl:template name="putActionPlanHierarchy">
<xsl:param name="record"/>
<xsl:param name="pageID"/>
<xsl:if test="$record/parentTree/parent">
	<table class="tight">
		<tr>
			<xsl:call-template name="putParentNode">
				<xsl:with-param name="parent" select="$record/parentTree/parent" />
				<xsl:with-param name="pageID" select="$pageID" />
			</xsl:call-template>		
		</tr>
	</table>
</xsl:if>
</xsl:template>

<!--##################################################
    ## putParentNode                                ##
	input:
	parent = 
		<parent>
			<parent />
		</parent>
	if there are more parents then keep calling yourself unitl the top
	use arrows when calling putparent within a parent.
	################################################## -->
<xsl:template name="putParentNode">
<xsl:param name="parent"/>
<xsl:param name="putArrows"/>
<xsl:param name="pageID"/>

<xsl:if test="$parent/parent">
	<xsl:call-template name="putParentNode"><xsl:with-param name="parent" select="$parent/parent" /><xsl:with-param name="putArrows" select="1" /></xsl:call-template>
</xsl:if>
<td class="tight">
	<a>
		<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/StandardPage.asp?pageID=<xsl:value-of select="$pageID"/>&amp;actualCount=1&amp;ID=<xsl:value-of select="$parent/@ID"/></xsl:attribute>
		<xsl:value-of select="$parent/@ID"/>
	</a>
	<xsl:if test="$putArrows">/</xsl:if>
</td>
</xsl:template>

<!--##################################################
    ## putColumnSearcher                            ##
	################################################## -->
<xsl:template name="putColumnSearcher">
<xsl:param name="name"/>
<xsl:param name="sortBy"/>
<xsl:param name="normalSort"/>
<xsl:choose>
	<xsl:when test="$normalSort">
		<a class="button">
			<xsl:attribute name="href">
				<xsl:value-of select="$strThisFile"/>?
				<xsl:call-template name="makeQueryString">
					<xsl:with-param name="items" select="//queryString/item[@name != 'sortBy' and @name != 'sortDescending']"/>
				</xsl:call-template>
				sortBy=<xsl:value-of select="$sortBy"/>&amp;
			</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$name"/></xsl:with-param></xsl:call-template>
		</a>
	</xsl:when>
	<xsl:otherwise>
		<td class="tight">
			<a>
				<xsl:attribute name="href">
					<xsl:value-of select="$strThisFile"/>?
					<xsl:call-template name="makeQueryString">
						<xsl:with-param name="items" select="//queryString/item[@name != 'sortBy' and @name != 'sortDescending']"/>
					</xsl:call-template>
					sortBy=<xsl:value-of select="$sortBy"/>&amp;
					<xsl:if test="//queryString/item[@name = 'sortBy']/@value = $sortBy and not(//queryString/item[@name = 'sortDescending'])">
						sortDescending=True&amp;
					</xsl:if>
				</xsl:attribute>
				<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$name"/></xsl:with-param></xsl:call-template>
			</a>
		</td>
		<td class="tight" style="vertical-align: middle;">
			<xsl:if test="//queryString/item[@name = 'sortBy']/@value = $sortBy and not(//queryString/item[@name = 'sortDescending'])">
				<xsl:text> </xsl:text>
				<img border="0">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>ascending.gif</xsl:attribute>
				</img>
			</xsl:if>
			<xsl:if test="//queryString/item[@name = 'sortBy']/@value = $sortBy and (//queryString/item[@name = 'sortDescending'])">
				<xsl:text> </xsl:text>
				<img border="0">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>decending.gif</xsl:attribute>
				</img>
			</xsl:if>
		</td>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## putRecordAsQueryString                       ##
	################################################## -->
<xsl:template name="putRecordAsQueryString">
	<xsl:param name="record" />
	<xsl:for-each select="$record/field">
		<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## objRowActionButton                           ##
	################################################## -->
<xsl:template name="objRowActionButton">
<xsl:param name="obj" />
<xsl:param name="record" />
<myCurrentRecord>
	<xsl:copy-of select="$record"/>
</myCurrentRecord>
<xsl:choose>
	<xsl:when test="$obj/attribute[@name = 'showOnly']">
		<xsl:variable name="showOnly"><xsl:call-template name="getShowOnlyList">
				<xsl:with-param name="obj" select="$obj" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="showOnlyHere"><xsl:call-template name="getShowOnlyHereList">
				<xsl:with-param name="obj" select="$obj" />
				<xsl:with-param name="record" select="$record" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:if test="$showOnly = $showOnlyHere">
			<xsl:call-template name="putRowActionButton">
				<xsl:with-param name="obj" select="." />
				<xsl:with-param name="record" select="$record" />										
			</xsl:call-template>						
		</xsl:if>
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="putRowActionButton">
			<xsl:with-param name="obj" select="." />
			<xsl:with-param name="record" select="$record" />										
		</xsl:call-template>							
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ##    getShowOnlyList                           ##
	################################################## -->
<xsl:template name="getShowOnlyList">
<xsl:param name="obj"/>
	<xsl:for-each select="$obj/attribute[@name = 'showOnly']">
		<xsl:value-of select="@value"/>	
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ##    getShowOnlyHereList                       ##
	################################################## -->
<xsl:template name="getShowOnlyHereList">
<xsl:param name="obj"/>
<xsl:param name="record"/>
	<xsl:for-each select="$obj/attribute[@name = 'showOnly']">
		<xsl:variable name="myVal" select="@value"/>
		<xsl:if test="$record/node()[name() = $myVal]/record">
			<xsl:value-of select="@value"/>	
		</xsl:if>
	</xsl:for-each>
</xsl:template>


<!--##################################################
    ## putRowActionButton                           ##
	################################################## -->
<xsl:template name="putRowActionButton">
<xsl:param name="obj" />
<xsl:param name="record" />
<span class="button">

<a class="button">
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name='pageID']/@value != ''">
			<xsl:attribute name="href">
				<xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=<xsl:value-of select="$obj/attribute[@name='pageID']/@value"/>
				<xsl:choose>
					<xsl:when test="$obj/attribute[@name = 'queryString']/@value">
						&amp;<xsl:value-of select="$obj/attribute[@name = 'queryString']/@value"/>
					</xsl:when>
				</xsl:choose>
				
				<xsl:choose>
					<xsl:when test="$obj/translationInformation">
						<xsl:for-each select="$obj/translationInformation/field">
							&amp;<xsl:value-of select="@show"/>=
							<xsl:call-template name="putOneRecordField">
								<xsl:with-param name="record" select="$record" />
								<xsl:with-param name="field" select="@name" />
							</xsl:call-template>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="putRowActionItemsQstring">
							<xsl:with-param name="obj" select="." />
							<xsl:with-param name="record" select="$record" />										
						</xsl:call-template>						
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
		</xsl:when>
		<xsl:when test="$obj/attribute[@name='special']/@value = 'sendTo'">
			<xsl:attribute name="href">
				<xsl:call-template name="AddToOptionBox">
						<xsl:with-param name="rForm" select="//queryString/item[@name = 'RECEIVER_FORM']/@value" />
						<xsl:with-param name="rField" select="//queryString/item[@name = 'RECEIVER_FIELD']/@value" />
						<xsl:with-param name="value"><xsl:call-template name="printOneItem">
								<xsl:with-param name="field" select="$obj/attribute[@name = 'valField']/@value" />
								<xsl:with-param name="record" select="$record" />
							</xsl:call-template>
						</xsl:with-param>
						<xsl:with-param name="show"><xsl:call-template name="printOneItem">
								<xsl:with-param name="field" select="$obj/attribute[@name = 'showField']/@value" />
								<xsl:with-param name="record" select="$record" />
								<xsl:with-param name="usePutText" select="$obj/attribute[@name = 'usePutText']/@value" />
							</xsl:call-template>
						</xsl:with-param>							
						<xsl:with-param name="multiple" select="//queryString/item[@name = 'multiple']/@value" />
					</xsl:call-template>						
			</xsl:attribute>
		</xsl:when>
		<xsl:when test="$obj/attribute[@name='special']/@value = 'docUnlock'">
			<xsl:attribute name="href">javascript:
				if (ConfirmButton('
				<xsl:call-template name="putText"><xsl:with-param name="key">Continueing will Unlock and Discard any changes</xsl:with-param></xsl:call-template>'))
				{document.location = '<xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=unlockTT&amp;ID=<xsl:value-of select="$record/field[@name = 'ID']/@value"/>&amp;redirectTo=<xsl:value-of select="$encoded_http_Address"/>'}
			</xsl:attribute>
			<xsl:if test="$stringEdit = 'yes'">
				<xsl:call-template name="putText"><xsl:with-param name="key">Continueing will Unlock and Discard any changes</xsl:with-param></xsl:call-template>            
            </xsl:if>
		</xsl:when>
		<xsl:when test="$obj/attribute[@name='special']/@value = 'stopDiscussion'">
			<xsl:attribute name="href">javascript:
				if (ConfirmButton('<xsl:call-template name="putText"><xsl:with-param name="key">Continueing will close this discussion.</xsl:with-param></xsl:call-template>'))
				{document.location = '<xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=closeDiscussion&amp;ID=<xsl:value-of select="$record/field[@name = 'ID']/@value"/>&amp;redirectTo=<xsl:value-of select="$encoded_http_Address"/>'}
			</xsl:attribute>
			<xsl:if test="$stringEdit = 'yes'">
	            <xsl:call-template name="putText"><xsl:with-param name="key">Continueing will close this discussion.</xsl:with-param></xsl:call-template>
            </xsl:if>
		</xsl:when>
		<xsl:when test="$obj/attribute[@name='special']/@value = 'stopSurvey'">
			<xsl:attribute name="href">javascript:
				if (ConfirmButton('<xsl:call-template name="putText"><xsl:with-param name="key">Continueing will close this survey.</xsl:with-param></xsl:call-template>'))
				{document.location = '<xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=closeSurvey&amp;ID=<xsl:value-of select="$record/field[@name = 'ID']/@value"/>&amp;redirectTo=<xsl:value-of select="$encoded_http_Address"/>'}
			</xsl:attribute>
			<xsl:if test="$stringEdit = 'yes'">
            	<xsl:call-template name="putText"><xsl:with-param name="key">Continueing will close this survey.</xsl:with-param></xsl:call-template>
            </xsl:if>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="href">
				<xsl:value-of select="$obj/attribute[@name='url']/@value"/>
				<xsl:choose>
					<xsl:when test="$obj/translationInformation">
						<xsl:for-each select="$obj/translationInformation/field">
							<xsl:value-of select="@show"/>=
							<xsl:call-template name="putOneRecordField">
								<xsl:with-param name="record" select="$record" />
								<xsl:with-param name="field" select="@name" />
							</xsl:call-template>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="putRowActionItemsQstring">
							<xsl:with-param name="obj" select="." />
							<xsl:with-param name="record" select="$record" />										
						</xsl:call-template>						
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
	<xsl:choose>
	 	<xsl:when test="$obj/attribute[@name='image']">
			<img border="0" class="rowAction">
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$obj/attribute[@name='image']/@value"/></xsl:attribute>
				<xsl:choose>
					<xsl:when test="$obj/attribute[@name = 'TTAlt']">
						<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">Locked By: </xsl:with-param></xsl:call-template><xsl:value-of select="$record/field[@name = 'LOCKED_BY_NAME']/@value"/> --</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:if test="$stringEdit = 'yes'">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
                	<xsl:call-template name="putText"><xsl:with-param name="key">Locked By:</xsl:with-param></xsl:call-template>
                </xsl:if>
			</img>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'usePutText']">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$obj/attribute[@name = 'value']/@value"/>			
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>		
	</xsl:choose>
</a>
</span>
<xsl:if test="$stringEdit = 'yes'">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
</xsl:if>
</xsl:template>

<!--##################################################
    ## putOneRecordField                            ##
	## input
	## record: <record><field><field></record>
	## field: fieldName
	## return: value in the field of the record with the name = field
	################################################## -->
<xsl:template name="putOneRecordField">
<xsl:param name="record"/><xsl:param name="field"/><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:template>


<!--##################################################
    ## putRowActionItemsQstring                     ##
	################################################## -->
<xsl:template name="putRowActionItemsQstring">
<xsl:param name="obj" />
<xsl:param name="record" />

	<xsl:choose>
		<xsl:when test="$obj/fields/field">
			<xsl:for-each select="$obj/fields/field">
				&amp;<xsl:value-of select="@alias"/>=<xsl:call-template name="printOneItem"><xsl:with-param name="field" select="@name" /><xsl:with-param name="record" select="$record" /></xsl:call-template>
			</xsl:for-each>
		</xsl:when>
		<xsl:otherwise>
			<xsl:for-each select="$record/field">
				&amp;<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>
			</xsl:for-each>
		</xsl:otherwise>
	</xsl:choose>			
</xsl:template>

<!--##################################################
    ## printOneItem                                 ##
	################################################## -->
<xsl:template name="printOneItem">
<xsl:param name="field"/>
<xsl:param name="record"/>
<xsl:param name="usePutText"/>
	<xsl:choose>
		<xsl:when test="$usePutText">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:with-param></xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$record/field[@name = $field]/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--##################################################
    ## objForm                                      ##
	################################################## -->
<xsl:template name="objForm">
<xsl:param name="obj"/>
	<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
	<table class="form">
		<xsl:if test="$obj/attribute[@name = 'class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
			<script language="JavaScript">
				arrForms.push("<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>");
			</script>
	
		<form method="get">
			<!--encType-->
			<xsl:if test="$obj/attribute[@name = 'encType']">
				<xsl:attribute name="encType"><xsl:value-of select="$obj/attribute[@name = 'encType']/@value"/></xsl:attribute>
			</xsl:if>
			<!--name-->
			<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
			<!--method-->
			<xsl:attribute name="method"><xsl:value-of select="$obj/attribute[@name = 'method']/@value"/></xsl:attribute>
			<!--action-->
			<xsl:attribute name="action"><xsl:value-of select="$obj/attribute[@name = 'action']/@value"/></xsl:attribute>
			<xsl:attribute name="onSubmit"><xsl:value-of select="$obj/attribute[@name = 'onSubmit']/@value"/></xsl:attribute>
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'specailOnSubmit']/@value = 'editTask'">
					<xsl:attribute name="onSubmit">
						return(validateEditTaskForm(document.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>,'<xsl:call-template name="jPutText"><xsl:with-param name="key">Submitting This Task Will Close It</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">No Requestor</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">EnterADescription</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">YouCanNotAssignBothAPersonAndARole</xsl:with-param></xsl:call-template>'))					
					</xsl:attribute>
				</xsl:when>
				<xsl:when test="$obj/attribute[@name = 'specailOnSubmit']/@value = 'editPerson'">
					<xsl:attribute name="onSubmit">
						return(
						validatePersonForm(
						document.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>,
						'<xsl:call-template name="jPutText"><xsl:with-param name="key">Password MisMatchError</xsl:with-param></xsl:call-template>',
						'<xsl:call-template name="jPutText"><xsl:with-param name="key">Error</xsl:with-param></xsl:call-template>',
						'<xsl:call-template name="jPutText"><xsl:with-param name="key">Old PassWord Needed To Change Login</xsl:with-param></xsl:call-template>'
						)
						)					
					</xsl:attribute>
				</xsl:when>

				<xsl:otherwise>
					<xsl:attribute name="onSubmit"><xsl:value-of select="$obj/attribute[@name = 'onSubmit']/@value"/></xsl:attribute>		
				</xsl:otherwise>
			</xsl:choose>
			<xsl:apply-templates />
		</form>
	</table>
</xsl:template>

<!--##################################################
    ## objFormRow                                   ##
	################################################## -->
<xsl:template name="objFormRow">
<xsl:param name="obj" />
	<tr>
		<xsl:for-each select="obj">
			<td class="form">
				<xsl:attribute name="colspan"><xsl:value-of select="$obj/attribute[@name='colspan']/@value"/></xsl:attribute>
				<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
				<xsl:apply-templates select="."/>
			</td>
		</xsl:for-each>
		<xsl:apply-templates select="hidden" />
	</tr>
</xsl:template>

<!--##################################################
    ## objText                                      ##
	################################################## -->
<xsl:template name="objText">
<xsl:param name="obj" />
<div class="norm">
<xsl:if test="$obj/attribute[@name = 'class']">
	<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name='class']/@value"/></xsl:attribute>
</xsl:if>
<xsl:variable name="nobr"><xsl:choose>
	<xsl:when test="$obj/attribute[@name = 'class']/@value='label'">true</xsl:when>
	<xsl:otherwise>false</xsl:otherwise>
</xsl:choose>
</xsl:variable>	
	<xsl:choose>
		<xsl:when test="not($obj/attribute[@name = 'dontUsePutText'])">
			<xsl:call-template name="putText"><xsl:with-param name="nobr" select="$nobr"/><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>		
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$obj/attribute[@name='value']/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</div>
</xsl:template>

<!--##################################################
    ## objTextBox                                   ##
	################################################## -->
<xsl:template name="objTextBox">
<xsl:param name="obj" />
<input type="text" class="norm" onfocus="select()">
	<xsl:attribute name="onChange">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");</xsl:attribute>
	<xsl:if test="$obj/attribute[@name = 'class']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'notEditable']/@value">
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/>;background-color: #EEEEEE;</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
		</xsl:otherwise>		
	</xsl:choose>
	<xsl:if test="$obj/attribute[@name = 'notEditable']">
		<xsl:attribute name="readOnly">true</xsl:attribute>
	</xsl:if>
	<!--name-->
	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>	
	<xsl:choose>
		<xsl:when test="($obj/attribute[@name='value']/@value = '') or not($obj/attribute[@name='value'])">
			<!--We are using the default value so go ahead and just get the info from the attributes-->
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'dontUsePutText']">
					<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<!-- We should have a value so use it-->
				<xsl:when test="$obj/attribute[@name = 'usePutText']">
					<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>			
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</input>
<xsl:if test="$stringEdit = 'yes'">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:with-param></xsl:call-template>
	<xsl:if test="$obj/attribute[@name = 'usePutText']">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
	</xsl:if>
</xsl:if>

<xsl:if test="$obj/attribute[@name = 'exact']">
	<xsl:variable name="temp_name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_MATCH_EXACTLY</xsl:variable>
	<xsl:variable name="exactMatchValue"><xsl:value-of select="//queryString/item[@name = $temp_name]/@value" /></xsl:variable>
	<nobr>
		<input type="Checkbox" value="TRUE">
			<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_MATCH_EXACTLY</xsl:attribute>
			<xsl:if test="$exactMatchValue = 'TRUE'"><xsl:attribute name="checked">checked</xsl:attribute></xsl:if>
		</input>
		<img align="middle" height="10" width="10">
			<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">match exactly</xsl:with-param></xsl:call-template></xsl:attribute>
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
		</img>
	</nobr>
</xsl:if>
<input type="hidden">
	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/>_default</xsl:attribute>
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'dontUsePutText']">
			<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
</input>

<xsl:if test="$obj/attribute[@name='setDefault'] or $obj/attribute[@name='onLoadFocus']">
	<script language="javascript">
		document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>.select();
		document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>.focus();
		objFocusOn = document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>;
	</script>
</xsl:if>
</xsl:template>

<!--##################################################
    ## objSubmitButton                              ##
	################################################## -->
<xsl:template name="objSubmitButton">
<xsl:param name="obj" />
	<input type="submit">
		<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
		<xsl:attribute name="onClick"><xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/></xsl:attribute>		
	</input>
<xsl:if test="$stringEdit = 'yes'">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
</xsl:if>	
</xsl:template>

<!--##################################################
    ## objPopUpBox                                  ##
	## output:
	## attributes:
	## class = class of the select if no class the class= norm
	## style = style to modify the class
	## defaultValue = value of the default when there are no records
	## defaultShow = Show for the default when there are no records
	## putText = if this attribute exists then use put text
	## extraShowPutText = if this exists then use Put Text For the Extra Show
	## input: 
		<obj name="popUpBox">
		<attribute name="class" value="whatever">
		<attribute name="style" value="whatever">
		<attribute name="putText" value="whatever">
		<attribute name="extraShowPutText" value="whatever">
		<attribute name="actualPutText" value="whatever">
		<attribute name="orderButtons" value="true" /> 
		<extraQueryData>
			<item name="whatever" value="whatever" />
		</extraQueryData>
		<viewPage pageID="showDiscussion" varName="ID"/>
		<data>
			<record>
				<field name="SHOW"></field>
				<field name="VALUE"></field>
				<field name="EXTRASHOW"></field>
			</record>
		</data>
		</obj>
	################################################## -->
<xsl:template name="objPopUpBox">
	<xsl:param name="obj" />
<div class="tight">
	<xsl:attribute name="id"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
	<table class="tight">
		<tr>
			<td class="tight" rowspan="2">
				<select class="norm">
					<xsl:attribute name="onChange">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");</xsl:attribute>
					<!--Class-->
					<xsl:if test="$obj/attribute[@name = 'class']">
						<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
					</xsl:if>
					<!--Style-->
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'notEditable']/@value">
							<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/>;background-color: #EEEEEE;</xsl:attribute>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
						</xsl:otherwise>		
					</xsl:choose>
					
					<!--size-->
					<xsl:attribute name="size"><xsl:value-of select="$obj/attribute[@name='size']/@value"/></xsl:attribute>
					<!--multiple-->
					<xsl:attribute name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:attribute>
					<!--name-->
					<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>
					<!--now check to see if we use the default value-->
					<xsl:choose>
						<xsl:when test="$obj/data/useDefault or not($obj/data/record)">
							<!--We are using the default value so go ahead and just get the info from the attributes-->
							<option>
								<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:attribute>
								<xsl:choose>
									<xsl:when test="$obj/attribute[@name = 'putText']/@value">
										<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='defaultShow']/@value"/></xsl:with-param></xsl:call-template>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="$obj/attribute[@name='defaultShow']/@value"/>						
									</xsl:otherwise>
								</xsl:choose>
							</option>
						</xsl:when>
						<xsl:otherwise>
							<xsl:for-each select="$obj/data/record">
								<option>
									<xsl:attribute name="value"><xsl:value-of select="field[@name = 'VALUE']/@value"/></xsl:attribute>
									<xsl:choose>
										<xsl:when test="$obj/attribute[@name = 'actualPutText']">
											<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'SHOW']/@value"/></xsl:with-param></xsl:call-template>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="field[@name = 'SHOW']/@value"/>					
										</xsl:otherwise>
									</xsl:choose>
									<xsl:text> </xsl:text>
									<xsl:choose>
										<xsl:when test="$obj/attribute[@name = 'extraShowPutText']">
											<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'EXTRA_SHOW']/@value"/></xsl:with-param></xsl:call-template>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="field[@name = 'EXTRA_SHOW']/@value"/>				
										</xsl:otherwise>
									</xsl:choose>	
								</option>
							</xsl:for-each>
						</xsl:otherwise>
					</xsl:choose>
				</select>
			</td>
			<xsl:if test="not($obj/attribute[@name = 'notEditable'])">
				<td class="tight">
					<div class="tight">
						<xsl:attribute name="id"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="$obj/attribute[@name='name']/@value"/>_Add</xsl:attribute>
						<xsl:call-template name="putLinkButton">
							<xsl:with-param name="destination">asp/standardPage.asp</xsl:with-param>
							<xsl:with-param name="fieldName"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
							<xsl:with-param name="formName"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
							<xsl:with-param name="id"><xsl:value-of select="$obj/attribute[@name='popUpID']/@value"/></xsl:with-param>
							<xsl:with-param name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:with-param>
							<xsl:with-param name="recsPerPage"><xsl:value-of select="$obj/attribute[@name='recsPerPage']/@value"/></xsl:with-param>
							<xsl:with-param name="extraQueryData"><xsl:for-each select="$obj/extraQueryStringData/item">
									<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>
								</xsl:for-each>
							</xsl:with-param>
							<xsl:with-param name="specialPop"><xsl:value-of select="$obj/attribute[@name = 'specialPop']/@value"/></xsl:with-param>
							<xsl:with-param name="ttCat">
								<xsl:choose>
									<xsl:when test="$obj/attribute[@name = 'lookAtCat']">
										document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'lookAtCat']/@value"/>					
									</xsl:when>
									<xsl:otherwise>
										document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.CATEGORY
									</xsl:otherwise>
								</xsl:choose>
							</xsl:with-param>
						</xsl:call-template>
					</div>
				</td>
				<td class="tight">
					<div class="tight">
						<xsl:attribute name="id"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="$obj/attribute[@name='name']/@value"/>_Remove</xsl:attribute>
						<xsl:call-template name="putRemoveButton">
							<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
							<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
						</xsl:call-template>
					</div>
				</td>
			</xsl:if>
			<xsl:if test="$obj/viewPage">
				<td class="tight">
					<!--<viewPage pageID="showDiscussion" varName="ID"/>-->
					<xsl:call-template name="putViewerButton">
						<xsl:with-param name="pageID"><xsl:value-of select="$obj/viewPage/@pageID"/></xsl:with-param>
						<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
						<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
						<xsl:with-param name="varName"><xsl:value-of select="$obj/viewPage/@varName"/></xsl:with-param>
					</xsl:call-template>
				</td>
			</xsl:if>
		</tr>
		<tr>
			<xsl:if test="not($obj/attribute[@name = 'notEditable']) and ($obj/attribute[@name = 'orderButtons'])">
				<xsl:call-template name="putOrderButtons">
						<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
						<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>					
				</xsl:call-template>
			</xsl:if>			
		</tr>
	</table>		
	<script language="javascript">
		arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
	</script>	
</div>
</xsl:template>


<!--##################################################
    ## putViewerButton                              ##
	################################################## -->
<xsl:template name="putViewerButton">
	<xsl:param name="pageID"/>
	<xsl:param name="field"/>
	<xsl:param name="form"/>
	<xsl:param name="varName"/>

	<a class="prodButton">
		<xsl:attribute name="href">javascript:popUpNavTo('<xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=<xsl:value-of select="$pageID"/>',document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>,'<xsl:value-of select="$varName"/>','<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>')</xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>view.gif</xsl:attribute>
			<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">view item</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<xsl:if test="$stringEdit = 'yes'">
    	<xsl:call-template name="putText"><xsl:with-param name="key">view item</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>
    </xsl:if>

	
	
</xsl:template>



<!--##################################################
    ##  putOrderButtons                             ##
	################################################## -->
<xsl:template name="putOrderButtons">
<xsl:param name="field"/>
<xsl:param name="form"/>
<td class="tight">
	<a class="prodButton">
		<xsl:attribute name="href">javascript:disableOtherForms("<xsl:value-of select="$form"/>");moveOptionUp(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>);</xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>up.gif</xsl:attribute>
			<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">move selected options up</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<xsl:if test="$stringEdit = 'yes'">
    	<xsl:call-template name="putText"><xsl:with-param name="key">move selected options up</xsl:with-param></xsl:call-template>
    </xsl:if>
</td>
<td class="tight">
	<a class="prodButton">
		<xsl:attribute name="href">javascript:disableOtherForms("<xsl:value-of select="$form"/>");moveOptionDown(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>);</xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>down.gif</xsl:attribute>
			<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">move selected option down</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<xsl:if test="$stringEdit = 'yes'">
    	<xsl:call-template name="putText"><xsl:with-param name="key">move selected option down</xsl:with-param></xsl:call-template>
    </xsl:if>
</td>

	
	
</xsl:template>


<!--##################################################
    ## objFileUpload                                ##
	################################################## -->
<xsl:template name="objFileUpload">
	<xsl:param name="obj" />
	<table class="tight">
		<tr>
			<td class="tight">
				<select>
					<xsl:if test="$obj/attribute[@name = 'notEditable']">
						<xsl:attribute name="style">color:#000000;font-style:italic;background-color:#EEEEEE;</xsl:attribute>
						
					</xsl:if>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'class']">
							<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="class">norm</xsl:attribute>
						</xsl:otherwise>		
					</xsl:choose>
					<!--size-->
					<xsl:attribute name="size"><xsl:value-of select="$obj/attribute[@name='size']/@value"/></xsl:attribute>
					<!--multiple-->
					<xsl:attribute name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:attribute>
					<!--name-->
					<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>
					<!--now check to see if we use the default value-->
					<xsl:choose>
						<xsl:when test="$obj/data/useDefault or not($obj/data/record)">
							<!--We are using the default value so go ahead and just get the info from the attributes-->
							<option>
								<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:attribute>
								<xsl:choose>
									<xsl:when test="not($obj/attribute[@name = 'putText'])">
										<xsl:value-of select="$obj/attribute[@name='defaultShow']/@value"/>						
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'defaultShow']/@value"/></xsl:with-param></xsl:call-template>
									</xsl:otherwise>
								</xsl:choose>
							</option>
						</xsl:when>
						<xsl:otherwise>
							<xsl:for-each select="$obj/data/record">
								<option>
									<xsl:attribute name="value"><xsl:value-of select="field[@name = 'VALUE']/@value"/></xsl:attribute>
									<xsl:choose>
										<xsl:when test="$obj/attribute[@name = 'recordPutText']/@value">
											<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'SHOW']/@value"/></xsl:with-param></xsl:call-template>
											<xsl:text> </xsl:text>
											<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = 'EXTRA_SHOW']/@value"/></xsl:with-param></xsl:call-template>
										</xsl:when>
										<xsl:otherwise>							
											<xsl:value-of select="field[@name = 'SHOW']/@value"/>
											<xsl:text> </xsl:text>
											<xsl:value-of select="field[@name = 'EXTRA_SHOW']/@value"/>
										</xsl:otherwise>
									</xsl:choose>
								</option>
							</xsl:for-each>
						</xsl:otherwise>
					</xsl:choose>
				</select>
			</td>
			<td class="tight">
				<table class="tight">
					<tr>
						<xsl:if test="not($obj/attribute[@name = 'notEditable'])">
							<td class="tight">
								<xsl:call-template name="putLinkButton">
									<xsl:with-param name="destination">asp/standardPage.asp</xsl:with-param>
									<xsl:with-param name="fieldName"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
									<xsl:with-param name="formName"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
									<xsl:with-param name="id">files</xsl:with-param>
									<xsl:with-param name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:with-param>
									<xsl:with-param name="recsPerPage"><xsl:value-of select="$obj/attribute[@name='recsPerPage']/@value"/></xsl:with-param>
								</xsl:call-template>								
							</td>
							<td class="tight">
								<xsl:call-template name="putAddAttachment">
									<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
									<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
									<xsl:with-param name="linkedTable"><xsl:value-of select="$obj/attribute[@name = 'sourceTable']/@value"/></xsl:with-param>
									<xsl:with-param name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:with-param>
									<xsl:with-param name="ID"><xsl:value-of select="$obj/attribute[@name='sourceID']/@value"/></xsl:with-param>
								</xsl:call-template>
							</td>
							<td>
								<xsl:call-template name="putRemoveButton">
									<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
									<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
								</xsl:call-template>
							</td>
						</xsl:if>
						<td>
							<xsl:call-template name="putPreviewDocumentButton">
								<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
								<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
								<xsl:with-param name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">View Selected Item</xsl:with-param></xsl:call-template></xsl:with-param>
							</xsl:call-template>
							<xsl:if test="$stringEdit = 'yes'">
                               	<xsl:call-template name="putText"><xsl:with-param name="key">View Selected Item</xsl:with-param></xsl:call-template>
                            </xsl:if>						
						</td>
					</tr>					
				</table>
			</td>
		</tr>
	</table>
	<script language="javascript">
		arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
	</script>	
</xsl:template>

<!--##################################################
    ## putAddAttachment                             ##
	################################################## -->
<xsl:template name="putAddAttachment">
	<xsl:param name="field" />
	<xsl:param name="form" />
	<xsl:param name="linkedTable" />
	<xsl:param name="ID" />
	<xsl:param name="multiple" />
	<a class="prodButton" target="PopUpWindow" onclick="PopWindow('',600,400)">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=AddDocument&amp;SOURCE_TABLE=<xsl:value-of select="$linkedTable"/>&amp;SOURCE_ID=<xsl:value-of select="$ID"/>&amp;
		RECEIVER_FORM=<xsl:value-of select="$form"/>&amp;
		RECEIVER_FIELD=<xsl:value-of select="$field"/>&amp;
		MULTIPLE=<xsl:value-of select="$multiple"/></xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>upload_paperclip.gif</xsl:attribute>
			<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">AddDocumentsFromYourPC</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
		<xsl:if test="$stringEdit = 'yes'">
			<xsl:call-template name="putText"><xsl:with-param name="key">AddDocumentsFromYourPC</xsl:with-param></xsl:call-template>            
        </xsl:if>
	</a>
</xsl:template>

<!--##################################################
    ## copyQstringItems                             ##
	################################################## -->
<xsl:template match="copyQstringItems">
<xsl:variable name="copyObj" select="."/>
	<xsl:for-each select="//queryString/item">
		<xsl:if test="$copyObj/copyItem/@name = @name">
			<input type="hidden">
				<xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
			</input>
		</xsl:if>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## objtree                                      ##
	################################################## -->
<xsl:template name="objtree">
<xsl:param name="obj" />
	<table class="tree">
		<xsl:if test="$obj/extraColumn">
			<tr>
				<td class="treeBlank"></td>
				<xsl:for-each select="$obj/extraColumn">
					<th class="tree">
						<xsl:if test="@class">
							<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>						
						</xsl:if>
						<xsl:if test="@titleHelpIcon">
							<img align="middle">
								<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
								<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param></xsl:call-template></xsl:attribute>
							</img>
							<xsl:if test="$stringEdit = 'yes'">
                            	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param></xsl:call-template>
                            </xsl:if>
						</xsl:if>

						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'titleDontUsePutText']/@value">
								<xsl:value-of select="@title"/>					
							</xsl:when>
							<xsl:otherwise>
								<xsl:call-template name="putText"><xsl:with-param name="nobr">true</xsl:with-param><xsl:with-param name="key"><xsl:value-of select="@title"/></xsl:with-param></xsl:call-template>
							</xsl:otherwise>
						</xsl:choose>
					</th>
				</xsl:for-each>
				<td class="treeBlank"></td>				
			</tr>
		</xsl:if>	

			<xsl:for-each select="$obj/tree">
				<xsl:call-template name="treePut">
					<xsl:with-param name="level" select ="0" />
					<xsl:with-param name="isLast" select ="string(position()=last())" />						
					<xsl:with-param name="item" select="."/>
					<xsl:with-param name="obj" select="$obj"/>					
				</xsl:call-template> 	
			</xsl:for-each>
	</table>
</xsl:template>

<!--##################################################
    ## divider                                      ##
	################################################## -->
<xsl:template match="divider">
	<xsl:call-template name="divider" />
</xsl:template>

<!--##################################################
    ## divider                                      ##
	################################################## -->
<xsl:template name="divider">
	<hr width="95%"/>
</xsl:template>

<!--##################################################
    ## spacer                                      ##
	################################################## -->
<xsl:template match="spacer">
	<div class="spacer">
		<br />
		<br />
	</div>
</xsl:template>

<!--##################################################
    ## formSpacer                                   ##
	################################################## -->
<xsl:template match="formSpacer">
	<tr>
		<td clas="tight">
			<div class="spacer">
				<br />
				<br />
			</div>
		</td>
	</tr>
</xsl:template>

<!--##################################################
    ## hidden                                       ##
	################################################## -->
<xsl:template match="hidden">
	<input type="hidden">
		<xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
		<xsl:attribute name="value"><xsl:value-of select="@value"/></xsl:attribute>
	</input>
</xsl:template>

<!--##################################################
    ## treePut                                      ##
	## inputs:
	level (1..N)- the current level of this node
	isLast - is this the last node?
	item - the tree node we are on
	obj - the tree object
	
	obj structure:
	<obj tree>
		<attribute name="treeclass" value="class"/>
		<attribute name="spacerClass" value="spacerClass"/>
		<permissions>
		<treeLink>
			<pageID value="page to jump to when clicking the node" />
			<showFields>
				<field value="NAME" /> 
			</showFields>
		</treeLink>
		<showFields>
			<field class="whatever" style="color:silver" value="ID" before="(" after=")" /> 
		</showFields>
 		<treeActionObjects>
 			<obj ID="standardPage.asp.addChildButton" type="rowActionButton">
 				<permissions>
  					<read /> 
  					<write /> 
  				</permissions>
  				<attribute name="pageID" value="addRoleChild" /> 
 				<attribute name="image" value="addChild.gif" /> 
  				<attribute name="alt" value="Add Child Role" /> 
  			</obj>
  		</treeActionObjects>
 		<tree>
  			<query val="SELECT * FROM A_ROLES WHERE ID = 'tea_224'" /> 
 			<record>
  				<field name="ID" type="202" value="tea_224" /> 
				<field name="NAME" type="202" value="Access Support" /> 
				<field name="MODBY" type="202" value="SYSTEM_IMPORT" /> 
			</record>
			<tree>
				<query val="SELECT * FROM A_ROLES WHERE ID = '1621'" /> 
				<record>
					<field name="ID" type="202" value="1621" /> 
					<field name="NAME" type="202" value="testing 123" /> 
				</record>
  			</tree>
		</tree>
	</obj>		
	</obj>
	################################################## -->
<xsl:template name="treePut">
	<xsl:param name="level" />
	<xsl:param name="isLast" />
	<xsl:param name="item" />
	<xsl:param name="obj"/>
		<tr>
			<td class="tree">
				<xsl:if test="$obj/attribute[@name = 'treeClass']">
					<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'treeClass']/@value"/></xsl:attribute>
				</xsl:if>
				<nobr>
					<xsl:choose>
						<!--The level is greater than 0 so we need to put some spacers in.-->
						<xsl:when test="$level &gt; 0">
							<xsl:call-template name="putSpacer">
								<xsl:with-param name="level" select="$level" />
								<xsl:with-param name="first" select="1" />
								<xsl:with-param name="isLast" select="$isLast" />
								<xsl:with-param name="class" select="$obj/attribute[@name = 'spacerClass']/@value" />
							</xsl:call-template>
							<xsl:choose>
								<xsl:when test="(position() = last()) ">
									<xsl:choose>
										<xsl:when test="$item/child/record ">
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">add</xsl:with-param>
													</xsl:call-template>
												</xsl:attribute>									
												<img class="tree">
													<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastPlus.gif</xsl:attribute>
												</img>				
												<xsl:if test="$stringEdit = 'yes'">
	                                            	<xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template>
													<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>
	                                            </xsl:if>
											</a>
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">add</xsl:with-param>
													</xsl:call-template>
													expandAll=<xsl:value-of select="$item/record/field[@name='ID']/@value"/>&amp;
												</xsl:attribute>									
												<img class="tree">
													<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
												</img>				
												<xsl:if test="$stringEdit = 'yes'">
													<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
	                                            </xsl:if>
											</a>
										</xsl:when>
										<xsl:when test="($item/tree) and ($item/ancestor::tree[@noCollapse='TRUE'])">
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">remove</xsl:with-param>
													</xsl:call-template>
												</xsl:attribute>										
												<img class="tree">
													<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastMinus.gif</xsl:attribute>
												</img>
												<xsl:if test="$stringEdit = 'yes'">
													<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>                                            
	                                            </xsl:if>
											</a>		
											<img class="tree">
												<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
													<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
												</xsl:if>
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraline.gif</xsl:attribute>
											</img>				

										</xsl:when>
										<xsl:otherwise>
											<img class="tree">
												<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
													<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
												</xsl:if>
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/last.gif</xsl:attribute>
											</img>
											<img class="tree">
												<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
													<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
												</xsl:if>
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraline.gif</xsl:attribute>
											</img>															
										</xsl:otherwise>
									</xsl:choose>
								</xsl:when>
								<xsl:otherwise>
									<xsl:choose>
										<xsl:when test="$item/child/record and not($item/expanded)">
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">add</xsl:with-param>
													</xsl:call-template>
												</xsl:attribute>										
												<img class="tree">
													<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/midPlus.gif</xsl:attribute>
												</img>				
											</a>
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">add</xsl:with-param>
													</xsl:call-template>
													expandAll=<xsl:value-of select="$item/record/field[@name='ID']/@value"/>&amp;
												</xsl:attribute>									
												<img class="tree">
													<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
												</img>				
												<xsl:if test="$stringEdit = 'yes'">
													<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
	                                            </xsl:if>
											</a>
										</xsl:when>
										<xsl:when test="($item/tree) and ($item/ancestor::tree[@noCollapse='TRUE'])">
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">remove</xsl:with-param>
													</xsl:call-template>
												</xsl:attribute>										
												<img class="tree">
													<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/midMinus.gif</xsl:attribute>
												</img>				
											</a>
											<img class="tree">
												<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
													<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
												</xsl:if>
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraline.gif</xsl:attribute>
											</img>				
										</xsl:when>
										<xsl:otherwise>
											<img class="tree">
												<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
													<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
												</xsl:if>
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/mid.gif</xsl:attribute>
											</img>
											<img class="tree">
												<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
													<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
												</xsl:if>
												<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraline.gif</xsl:attribute>
											</img>				
				
										</xsl:otherwise>
									</xsl:choose>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
					<!--so that was for one with the level > 0 -->
					<!--Since this is level = 0 we just put one spacer in-->
						<xsl:otherwise>
							<xsl:choose>
								<xsl:when test="$item/child/record ">
									<a>
										<xsl:attribute name="href">
											<xsl:call-template name="buildExpandHREF">
												<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
												<xsl:with-param name="act">add</xsl:with-param>
											</xsl:call-template>
										</xsl:attribute>									
										<img class="tree">
											<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template></xsl:attribute>
											<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
												<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
											</xsl:if>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastPlus.gif</xsl:attribute>
										</img>				
										<xsl:if test="$stringEdit = 'yes'">
                                           	<xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template>
											<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>
                                           </xsl:if>
									</a>
									<a>
										<xsl:attribute name="href">
											<xsl:call-template name="buildExpandHREF">
												<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
												<xsl:with-param name="act">add</xsl:with-param>
											</xsl:call-template>
											expandAll=<xsl:value-of select="$item/record/field[@name='ID']/@value"/>&amp;
										</xsl:attribute>									
										<img class="tree">
											<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
											<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
												<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
											</xsl:if>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
										</img>				
										<xsl:if test="$stringEdit = 'yes'">
											<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
                                           </xsl:if>
									</a>
								</xsl:when>
								<xsl:when test="($item/tree) and ($item/ancestor::tree[@noCollapse='TRUE'])">
									<a>
										<xsl:attribute name="href">
											<xsl:call-template name="buildExpandHREF">
												<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
												<xsl:with-param name="act">remove</xsl:with-param>
											</xsl:call-template>
										</xsl:attribute>										
										<img class="tree">
											<xsl:attribute name="alt"><xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template></xsl:attribute>
											<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
												<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
											</xsl:if>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastMinus.gif</xsl:attribute>
										</img>
										<xsl:if test="$stringEdit = 'yes'">
											<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>                                            
                                           </xsl:if>
									</a>		
									<img class="tree">
										<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
											<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
										</xsl:if>
										<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraline.gif</xsl:attribute>
									</img>				

								</xsl:when>
								<xsl:otherwise>
									<img class="tree">
										<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
											<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
										</xsl:if>
										<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/last.gif</xsl:attribute>
									</img>
									<img class="tree">
										<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
											<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
										</xsl:if>
										<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraline.gif</xsl:attribute>
									</img>															
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
				<!--now for one with a tree link in it-->
					<xsl:choose>
						<xsl:when test="$item/@image">
							<img>
								<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$item/@image"/></xsl:attribute>
							</img>
						</xsl:when>
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="$obj/treeLink">
							<xsl:call-template name="treePutLink">
								<xsl:with-param name="obj" select="$obj" />
								<xsl:with-param name="record" select="$item/record" />
							</xsl:call-template>
						</xsl:when>
					</xsl:choose>
					<xsl:call-template name="treePutShowFields">
						<xsl:with-param name="obj" select="$obj" />
						<xsl:with-param name="record" select="$item/record" />
					</xsl:call-template>
				</nobr>
			</td>
			<xsl:call-template name="treePutExtraColumns">
				<xsl:with-param name="obj" select="$obj" />
				<xsl:with-param name="record" select="$item/record" />
			</xsl:call-template>
		<!--Put the action objects for this row-->
			<td class="tree">
				<xsl:if test="$obj/treeActionObjects/obj[@type='rowActionButton']">
					<nobr>
						<xsl:for-each select="$obj/treeActionObjects/obj[@type='rowActionButton']">
							<xsl:choose>
								<xsl:when test="$obj/treeActionObjects/obj[@type='rowActionButton']/attribute[@name = 'treeShowOnly']">
									<xsl:variable name="showOnlyVal"><xsl:value-of select="$obj/treeActionObjects/obj[@type='rowActionButton']/attribute[@name = 'treeShowOnly']/@value"/></xsl:variable>
									<xsl:if test="$item/record/field[@name = $showOnlyVal]">
										<xsl:call-template name="objRowActionButton">
											<xsl:with-param name="obj" select="." />
											<xsl:with-param name="record" select="$item/record" />										
										</xsl:call-template>				
	
									</xsl:if>
								</xsl:when>
								<xsl:otherwise>
									<xsl:call-template name="objRowActionButton">
										<xsl:with-param name="obj" select="." />
										<xsl:with-param name="record" select="$item/record" />										
									</xsl:call-template>				
								</xsl:otherwise>
							</xsl:choose>
						</xsl:for-each>		
					</nobr>
				</xsl:if>
			</td>

		</tr>	
	<xsl:if test="$stringEdit = 'yes'">
	   	<xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>
	</xsl:if>
	<xsl:for-each select="$item/tree">
		<xsl:call-template name="treePut">
			<xsl:with-param name="level" select ="$level + 1" />
			<xsl:with-param name="isLast" select ="concat($isLast,',',string(position()=last()))" />			
			<xsl:with-param name="item" select="." />
			<xsl:with-param name="obj" select="$obj"/>								
		</xsl:call-template>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## treePutExtraColumns                          ##
	################################################## -->
<xsl:template name="treePutExtraColumns">
<xsl:param name="obj"/>
<xsl:param name="record"/>
	<xsl:for-each select="$obj/extraColumn">
		<td class="tree">
			<xsl:if test="@class">
				<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>			
			</xsl:if>
<!--			<textarea rows="1" readonly="readonly" class="tree" >
				<xsl:attribute name="cols"><xsl:value-of select="@width"/></xsl:attribute>
				<xsl:call-template name="putExtraDataField">
					<xsl:with-param name="field" select="@field" />
					<xsl:with-param name="record" select="$record" />			
				</xsl:call-template>
			</textarea>-->
			<span class="treeData">
				<xsl:attribute name="style">width:<xsl:value-of select="@width"/></xsl:attribute>
				<nobr>
					<xsl:call-template name="putExtraDataField">
						<xsl:with-param name="field" select="@field" />
						<xsl:with-param name="record" select="$record" />
						<xsl:with-param name="exCol" select="." />
					</xsl:call-template>	
				</nobr>
			</span>
		</td>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## putExtraDataField                            ##
	################################################## -->
<xsl:template name="putExtraDataField">
<xsl:param name="field" />
<xsl:param name="record" />
<xsl:param name="exCol" />
<!--Get the value that we need to print and store it in i-->
<xsl:variable name="i"><xsl:call-template name="getExValue">
		<xsl:with-param name="field" select="$field" />
		<xsl:with-param name="record" select="$record" />
		<xsl:with-param name="exCol" select="$exCol" />
	</xsl:call-template>
</xsl:variable>

<!--Create The 2nd part of the Querystring based on aliases-->
<xsl:variable name="QString"><xsl:call-template name="readAliases">
		<xsl:with-param name="aliases" select="$exCol/aliases" />
		<xsl:with-param name="record" select="$record" />
	</xsl:call-template>
</xsl:variable>

<!--Put the words out-->
<xsl:choose>
	<xsl:when test="$exCol/@pageID">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardpage.asp?pageID=<xsl:value-of select="$exCol/@pageID"/>&amp;<xsl:value-of select="$QString"/></xsl:attribute>
			<xsl:value-of select="$i"/>
		</a>
	</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="$i"/>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## getExValue                                   ##
	################################################## -->
<xsl:template name="getExValue">
<xsl:param name="field" />
<xsl:param name="record" />
<xsl:param name="exCol" />
<xsl:choose>
	<xsl:when test="$exCol/@usePutText">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$record/field[@name=$field]/@value"/></xsl:with-param></xsl:call-template>
	</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="$record/field[@name=$field]/@value"/>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ## readAliases                                  ##
	## $aliases = 
	## 	<aliases>
			<alias name="Parent_ID" field="ID">
			<alias name="Reason" field="SUBJ">
		</aliases>
	   $record = 
	   	<record>
			<field name="ID" value="12345">
			<field name="SUBJ" value="Ans">
		</record>
	
	Output:
		Parent_ID=12345&amp;Reason=Ans&amp;
	################################################## -->
<xsl:template name="readAliases">
<xsl:param name="aliases"/>
<xsl:param name="record"/>
	<xsl:for-each select="$aliases/alias">
		<xsl:variable name="alias_field" select="@field" />
		<xsl:value-of select="@name"/>=<xsl:value-of select="$record/field[@name = $alias_field]/@value"/>
	</xsl:for-each>
</xsl:template>


<!--##################################################
    ## treePutLink                                  ##
	################################################## -->
<xsl:template name="treePutLink">
<xsl:param name="obj"/>
<xsl:param name="record"/>
<xsl:for-each select="$obj/treeLink">
	<xsl:choose>
		<xsl:when test="pageID">
			<a class="tree">
				<xsl:if test="not(translationInformation)">
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=<xsl:value-of select="pageID/@value"/><xsl:for-each select="$record/field">&amp;<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/></xsl:for-each></xsl:attribute>		
				</xsl:if>
				<xsl:if test="translationInformation">
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=<xsl:value-of select="pageID/@value"/><xsl:for-each select="translationInformation/field"><xsl:variable name="x" select="@name"/>&amp;<xsl:value-of select="@alias"/>=<xsl:value-of select="$record/field[@name = $x]/@value"/></xsl:for-each></xsl:attribute>
				</xsl:if>
				<xsl:for-each select="showFields/field">
					<xsl:variable name="showValue"><xsl:call-template name="treeShowField">
							<xsl:with-param name="record" select="$record" />
							<xsl:with-param name="field"><xsl:value-of select="./@value"/></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:if test="$showValue != ''">
						<xsl:value-of select="@before"/>
							<xsl:value-of select="$showValue"/>
						<xsl:value-of select="@after"/>				
					</xsl:if>
				</xsl:for-each>
			</a>
		</xsl:when>
		<xsl:otherwise>
			<xsl:for-each select="showFields/field">
				<xsl:value-of select="@before"/>
				<xsl:call-template name="treeShowField">
					<xsl:with-param name="record" select="$record" />
					<xsl:with-param name="field"><xsl:value-of select="./@value"/></xsl:with-param>
				</xsl:call-template>
				<xsl:value-of select="@after"/>
			</xsl:for-each>
		</xsl:otherwise>
	</xsl:choose>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ## treePutShowFields                                ##
	################################################## -->
<xsl:template name="treePutShowFields">
<xsl:param name="obj"/>
<xsl:param name="record"/>
<!--<table>
	<tr>-->
		<xsl:for-each select="$obj/showFields/field">
			<!--<td>-->
				<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>
				<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
				
				<xsl:call-template name="treeShowField">
					<xsl:with-param name="record" select="$record" />
					<xsl:with-param name="field"><xsl:value-of select="./@value"/></xsl:with-param>
					<xsl:with-param name="before"><xsl:value-of select="./@before"/></xsl:with-param>
					<xsl:with-param name="after"><xsl:value-of select="./@after"/></xsl:with-param>
				</xsl:call-template>
			<!--</td>-->
		</xsl:for-each>
	<!--</tr>
</table>-->
</xsl:template>

<!--##################################################
    ## treeShowField                             ##
	################################################## -->
<xsl:template name="treeShowField">
<xsl:param name="record"/>
<xsl:param name="field"/>
<xsl:param name="before"/>
<xsl:param name="after"/>

	<xsl:if test="$record/field[@name = $field]">
		<xsl:value-of select="$before"/>	
		<xsl:value-of select="$record/field[@name=$field]/@value"/>
		<xsl:value-of select="$after"/>	
	</xsl:if>
</xsl:template>


<!--##################################################
    ## putSpacer                                    ##
	################################################## -->
<xsl:template name="putSpacer">
	<xsl:param name="level" />
	<xsl:param name="first" />
	<xsl:param name="isLast" />
	<xsl:param name="class" />
	<xsl:choose>
		<xsl:when test="($first = 0) and (substring-before($isLast,',')='false')">
			<img class="spacer">
				<xsl:if test="$class">
					<xsl:attribute name="class"><xsl:value-of select="$class"/></xsl:attribute>
				</xsl:if>
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/insidespacer.gif</xsl:attribute>
			</img>	
		</xsl:when>
		<xsl:otherwise>
			<img class="tree">
				<xsl:if test="$class">
					<xsl:attribute name="class"><xsl:value-of select="$class"/></xsl:attribute>
				</xsl:if>
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/spacer.gif</xsl:attribute>
			</img>	
		</xsl:otherwise>
	</xsl:choose>
	<xsl:if test="$level &gt; 1">
		<xsl:call-template name="putSpacer">
			<xsl:with-param name="level" select="$level - 1" />
			<xsl:with-param name="first" select="0" />
			<xsl:with-param name="isLast" select="substring-after($isLast,',')" />	
			<xsl:with-param name="class" select="$class" />
		</xsl:call-template>
	</xsl:if>
</xsl:template>


<!--##################################################
    ## buildExpandHREF                                ##
	################################################## -->
<xsl:template name="buildExpandHREF">
<xsl:param name="ID"/>
<xsl:param name="act"/>
<xsl:value-of select="$strThisFile"/>?
<xsl:if test="not(//queryString/item[@name='expandList']) and ($act != 'remove')">
	expandList=<xsl:value-of select="$ID"/>&amp;
</xsl:if>
<xsl:for-each select="//queryString/item[@name != 'expandAll']">
	<xsl:choose>
		<xsl:when test="@name = 'expandList'">
			<xsl:choose>
				<xsl:when test="$act = 'remove'">
					<xsl:value-of select="@name"/>=
					<xsl:for-each select="//tree[tree]">
						<xsl:if test="record/field[@name='ID']/@value != $ID">
							<xsl:value-of select="record/field[@name='ID']/@value"/>,	
						</xsl:if>
					</xsl:for-each>
					&amp;
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="@name"/>=
					<xsl:for-each select="//tree[tree]">
						<xsl:if test="record/field[@name='ID']/@value != $ID">
							<xsl:value-of select="record/field[@name='ID']/@value"/>,
						</xsl:if>
					</xsl:for-each>
					<xsl:value-of select="$ID"/>
					&amp;
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;
		</xsl:otherwise>
	</xsl:choose>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  AddToOptionBox                              ##
	################################################## -->
<xsl:template name="AddToOptionBox">
<xsl:param name="rForm" />
<xsl:param name="rField" />
<xsl:param name="value" />
<xsl:param name="show" />
<xsl:param name="multiple" />
javascript:window.opener.AddToOptionBox(
'<xsl:value-of select="$rForm"/>',
'<xsl:value-of select="$rField"/>',
'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$value"/></xsl:call-template>',
'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$show"/></xsl:call-template>',
'<xsl:value-of select="$multiple"/>');
<xsl:choose>
	<xsl:when test="$multiple='' or not($multiple)">window.close()</xsl:when>
<!--	<xsl:otherwise>alert('Multiple = *<xsl:value-of select="$multiple"/>*');</xsl:otherwise> -->
</xsl:choose>

</xsl:template>

<!--##################################################
    ## jScriptEscape                                ##
	################################################## -->
<xsl:template name="jScriptEscape">
<xsl:param name="val"/>
		<xsl:call-template name="replace">
			<xsl:with-param name="string" select="$val" />
			<xsl:with-param name="pattern">'</xsl:with-param>
			<xsl:with-param name="replacement">\'</xsl:with-param>
		</xsl:call-template>
</xsl:template>


<!--##################################################
    ## objTextAdder                                 ##
	################################################## -->
<xsl:template name="objTextAdder">
<div class="textAdder" id="textAdder">
<table>
	<tr>
		<td colspan="2">
			<span><xsl:call-template name="putText"><xsl:with-param name="key">Press Button</xsl:with-param></xsl:call-template>
			</span>
		</td>
	</tr>
	<tr>
<!--Bold Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">font-weight: bold;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;bb&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/bb&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text To Bold</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">bold</xsl:with-param>
			</xsl:call-template>
		</td>
<!--red Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: red;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;r&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/r&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Red Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">red</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>
	<tr>
<!--Underline Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">text-decoration: underline;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;u&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/u&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text to be Underlined</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">underline</xsl:with-param>
			</xsl:call-template>
		</td>
<!--purple Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: purple;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;p&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/p&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Purple Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">Purple</xsl:with-param>
			</xsl:call-template>

		</td>
	</tr>		
	<tr>
<!--italics Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">font-style: italic;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;i&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/i&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text to be Italicized</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">italics</xsl:with-param>
			</xsl:call-template>
		</td>
<!--Blue Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: blue;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;b&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/b&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Blue Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">blue</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>		
	<tr>
		<td>
		</td>
<!--Green Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: green;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;g&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/g&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Green Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">green</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>		
	<tr>
		<td>
		</td>
<!--Yellow Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: yellow;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;y&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/y&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Yellow Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">yellow</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>		
	<tr>
		<td>
		</td>
<!--Orange Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: orange;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;o&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/o&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Orange Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">orange</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>		
	<tr>
		<td colspan="2">
			<xsl:call-template name="putText"><xsl:with-param name="key">Pressing button will place a newline</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<tr>
		<td></td>
<!--New Line-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;nl/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">New Line</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<xsl:call-template name="putText"><xsl:with-param name="key">Pressing these buttons will put these characters</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<tr>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;omega/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">omega</xsl:with-param>
			</xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;theta/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">theta</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>	
	<tr>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;ang/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">Angstrom</xsl:with-param>
			</xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;delta/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">delta</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>

	<tr>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;sigma/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">sigma</xsl:with-param>
			</xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;pi/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">pi</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>

	<tr>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;lambda/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">lambda</xsl:with-param>
			</xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;beta/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">beta</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>

	<tr>
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: black;</xsl:with-param>
				<xsl:with-param name="begin"></xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;alpha/&gt;&gt;</xsl:with-param>
				<xsl:with-param name="val">alpha</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>
	
	
</table>

</div>
</xsl:template>

<!--##################################################
    ## textAdderButton                              ##
	################################################## -->
<xsl:template name="textAdderButton">
<xsl:param name="style"/>
<xsl:param name="begin"/>
<xsl:param name="end"/>
<xsl:param name="middle"/>
<xsl:param name="val"/>
	<div onmouseover="this.style.cursor='hand'" class="textAdderButton">
		<xsl:attribute name="style"><xsl:value-of select="$style"/></xsl:attribute>
		<xsl:attribute name="onClick" >
		insertAtCarat('<xsl:value-of select="$begin"/>','<xsl:value-of select="$end"/>','<xsl:value-of select="$middle"/>')
		</xsl:attribute>
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$val"/></xsl:with-param></xsl:call-template>
	</div>
</xsl:template>

<!--##################################################
    ## printApprovalItem                            ##
	################################################## -->
<xsl:template name="printApprovalItem">
<xsl:param name="record"/>
<xsl:choose>
	<xsl:when test="$record/roleAssignment">
		<xsl:variable name="d" select="$record/roleAssignment"/>
		<xsl:call-template name="putText"><xsl:with-param name="key">PreAssigning</xsl:with-param></xsl:call-template>
		<xsl:text> </xsl:text>
		<xsl:value-of select="$d/record/field[@name = 'PERSON_ASSIGNED_FULL_NAME']/@value"/>
		<xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">PostAssigning</xsl:with-param></xsl:call-template>		
		<xsl:text> </xsl:text>
		<xsl:value-of select="$d/record/field[@name = 'ROLE_ASSIGNED_NAME']/@value"/>
		<xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">totherole</xsl:with-param></xsl:call-template>
		<xsl:text> </xsl:text>
		<xsl:value-of select="$d/record/field[@name = 'NAME']/@value"/>
	</xsl:when>
	<xsl:when test="$record/approval">
		<xsl:variable name="d" select="$record/approval"/>
		<xsl:call-template name="putText"><xsl:with-param name="key">PreApprovalOfThe</xsl:with-param></xsl:call-template>
		<xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">ap_<xsl:value-of select="$d/record/field[@name = 'TYPE']/@value"/></xsl:with-param></xsl:call-template>
		<xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">PostApprovalOfThe</xsl:with-param></xsl:call-template>		
		<xsl:text> </xsl:text>
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=viewTT&amp;ID=<xsl:value-of select="$d/record/field[@name = 'ID']/@value"/></xsl:attribute>
			<xsl:text> </xsl:text>		
			<xsl:value-of select="$d/record/field[@name = 'NAME']/@value"/>
			<xsl:text> </xsl:text>			
			<xsl:call-template name="putText"><xsl:with-param name="key">Revision:</xsl:with-param></xsl:call-template>
			<xsl:text> </xsl:text>
			<xsl:value-of select="$d/record/field[@name = 'REV']/@value"/>
		</a>
	</xsl:when>
</xsl:choose>
</xsl:template>


<!--##################################################
    ## putCommonAttributes                          ##
	################################################## -->
<xsl:template name="putCommonAttributes">
<xsl:param name="obj" />
	<xsl:if test="$obj/attribute/@name = 'class'">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'style'">
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
	</xsl:if>
</xsl:template>

</xsl:stylesheet>