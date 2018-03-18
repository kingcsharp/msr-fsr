<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:include href="standardPage_TEA_supplement.xsl"/>


<!--##################################################
    ##  EMAIL_HTML_BODY                             ##
	################################################## -->
<xsl:template name="EMAIL_HTML_BODY">
Test

</xsl:template>

<!--##################################################
    ##                                              ##
	################################################## -->
<xsl:template match="*">
<xsl:copy-of select="."/>
</xsl:template>

<!--##################################################
    ##                                              ##
	################################################## -->
<xsl:template match="stringMissing">stringMissing</xsl:template>


<!--##################################################
    ##                                              ##
	################################################## -->
<xsl:template match="oneSpace">
	<span style="margin-right: 5px"> </span>
</xsl:template>

<!--##################################################
    ## obj                                          ##
	## input:
	<obj name="something" type="something">
		inside depends on the object.
	</obj>
	We will check this with the permissions to see if we
	can even show this object.  If we can not then it will
	not be processed.
	################################################## -->
<xsl:template match="obj">
	<!--<xsl:variable name="processObj"><xsl:call-template name="checkObjPermissions"><xsl:with-param name="obj" select="." /></xsl:call-template></xsl:variable>-->
	<!--<xsl:if test="$processObj = 'true'">-->
			<xsl:call-template name="processObject">
			</xsl:call-template>				
<!--	</xsl:if>-->
</xsl:template>

<xsl:template match="permissions">
</xsl:template>

<xsl:template match="attribute">
</xsl:template>


<!--##################################################
    ## checkObjPermissions                          ##
	################################################## -->
<xsl:template name="checkObjPermissions">
<xsl:param name="obj" />
<xsl:variable name="myID"><xsl:value-of select="$obj/@ID"/></xsl:variable>
<xsl:choose>
	<xsl:when test="$myID = ''">true</xsl:when>
	<xsl:when test="not(/Doc_Webpage/objectPermissions/permission[@objID=$myID][@type='VIEW'])">
		<xsl:choose>
			<xsl:when test="/Doc_Webpage/objectPermissions/permission[@objID=$myID][@type='NO_VIEW'][@roleID=/Doc_Webpage/ARole/@name]">false</xsl:when>
			<xsl:otherwise>true</xsl:otherwise>
		</xsl:choose>
	</xsl:when>
	<xsl:when test="/Doc_Webpage/objectPermissions/permission[@objID=$myID][@roleID=/Doc_Webpage/ARole/@name][@type='VIEW']">true</xsl:when>
	<xsl:otherwise>false</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ##  copyCommonAttributes                        ##
	################################################## -->
<xsl:template name="copyCommonAttributes">
	<xsl:attribute name="style"><xsl:value-of select="attribute[@name = 'style']/@value"/></xsl:attribute>
	<xsl:attribute name="class"><xsl:value-of select="attribute[@name = 'class']/@value"/></xsl:attribute>
	<xsl:attribute name="id"><xsl:value-of select="attribute[@name = 'id']/@value"/></xsl:attribute>
	<xsl:attribute name="onClick"><xsl:value-of select="attribute[@name = 'onClick']/@value"/></xsl:attribute>
	<xsl:attribute name="onMouseOver"><xsl:value-of select="attribute[@name = 'onMouseOver']/@value"/></xsl:attribute>
</xsl:template>

<!--##################################################
    ## processObject                                ##
	## then current location should still be in the
	## object that we were processing above.
	################################################## -->
<xsl:template name="processObject">
	<xsl:choose>
		<xsl:when test="@type = 'asyncFileUpload'">
			<xsl:call-template name="objAsyncFileUpload">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'searchDrop'">
			<xsl:call-template name="objSearchDrop">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'div'">
			<xsl:call-template name="objDiv">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'imageButton'">
			<xsl:call-template name="objImageButton">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'roundBox'">
			<xsl:call-template name="objRoundBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'dbImage'">
			<xsl:call-template name="objdbImage">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'preText'">
			<xsl:call-template name="objPreText">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'straightHTML'">
			<xsl:copy-of select="./*" />
		</xsl:when>			
		<xsl:when test="@type = 'hiddenTextBox'">
			<xsl:call-template name="objHiddenTextBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'emailDate'">
			<xsl:call-template name="objEmailDate">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'infoBox'">
			<xsl:call-template name="objInfoBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
		<xsl:when test="@type = 'butt'">
			<xsl:call-template name="objButt">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>			
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
		<xsl:when test="@type = 'span'">
			<xsl:call-template name="objSpan">
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
		<xsl:when test="@type = 'checkBox'">
			<xsl:call-template name="objCheckBox">
				<xsl:with-param name="obj" select="." />
			</xsl:call-template>
		</xsl:when>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ##  objCheckBox                                 ##
	################################################## -->
<xsl:template name="objCheckBox">
<xsl:param name="obj"/>
<input type="checkBox">
	<xsl:call-template name="putCommonAttributes">
		<xsl:with-param name="obj" select="$obj" />
	</xsl:call-template>
	<xsl:if test="$obj/attribute[@name='checked']/@value='true'">
		<xsl:attribute name="checked">true</xsl:attribute>
	</xsl:if>
</input>
</xsl:template>

<!--##################################################
    ##   objDiv                                     ##
	################################################## -->
<xsl:template name="objDiv">
<xsl:param name="obj"/>
<div>
	<xsl:call-template name="copyCommonAttributes" />
	<span style="display:none"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></span>		
	<xsl:choose>
		<xsl:when test="attribute[@name='nobr']/@value='true'">
			<nobr>
				<xsl:apply-templates />
			</nobr>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="attribute[@name='id']/@value='ajaxObj'">
					<ajaxObj><xsl:apply-templates /></ajaxObj>
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</div>


</xsl:template>

<!--##################################################
    ##  objdbImage                                  ##
	################################################## -->
<xsl:template name="objdbImage">
<xsl:param name="obj"/>
<xsl:if test="not(/Doc_Webpage/@email='true')">
<xsl:if test="$obj/attribute[@name='value']/@value">
	<img>
		<xsl:if test="$obj/attribute[@name = 'class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name='class']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:if test="$obj/attribute[@name = 'style']">
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:attribute name="src"><xsl:value-of select="$http_Root"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:attribute>
	</img>
</xsl:if>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  objEmailDate                                ##
	## This will make a date and will adjust it by  ##
	## looking up the recipients time zone.         ##
	################################################## -->
<xsl:template name="objEmailDate">
<xsl:param name="obj"/>
<xsl:variable name="adjuster"><xsl:value-of select="//recipientData/record[field[@name='name']/@value = 'G_DIFF']"/></xsl:variable>
<xsl:variable name="this-id"><xsl:value-of select="generate-id($obj)"/></xsl:variable>
<table><tr><td>
<div>
	<xsl:attribute name="ID"><xsl:value-of select="$this-id"/></xsl:attribute>
	<xsl:value-of select="$obj/field/@answerDate"/> ( <xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="//senderData/record/field[@name='TIME_ZONE_NAME']/@value"/></xsl:with-param></xsl:call-template> )
</div>
</td></tr></table>
</xsl:template>

<!--##################################################
    ## objPreText                                      ##
	################################################## -->
<xsl:template name="objPreText">
<xsl:param name="obj" />
<pre>
<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name='class']/@value"/></xsl:attribute>
<xsl:attribute name="style">word-wrap:soft;<xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
	<xsl:choose>
		<xsl:when test="not($obj/attribute[@name = 'dontUsePutText'])">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>		
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$obj/attribute[@name='value']/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</pre>
</xsl:template>

<!--##################################################
    ##  objButt                                     ##
	## This is a new button object.  The old one was##
	## making buttons which did not look like normal##
	## buttons, so I needed one that would.         ##
	################################################## -->
<xsl:template name="objButt">
<xsl:param name="obj"/>
	<input type="button">
       	<xsl:if test="($obj/attribute[@name = 'disabled']/@value != '')">
			<xsl:attribute name="disabled"><xsl:value-of select="$obj/attribute[@name = 'disabled']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
		<xsl:choose>
        	<xsl:when test="not($obj/attribute[@name = 'dontUsePutText'])">
        		<xsl:attribute name="value">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template>
				</xsl:attribute>
        	</xsl:when>
        	<xsl:otherwise>
        		<xsl:attribute name="value">
	        		<xsl:value-of select="attribute[@name = 'value']/@value"/>					
				</xsl:attribute>				
        	</xsl:otherwise>
        </xsl:choose>
		<xsl:choose>
			<xsl:when test="$obj/confirm">
				<xsl:choose>
                  	<xsl:when test="not($obj/confirm[@dontUsePutText])">
                		<xsl:attribute name="onClick">if(confirm('<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/confirm/@msg"/></xsl:with-param></xsl:call-template>')){
						<xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/>}</xsl:attribute>
                    </xsl:when>
                    <xsl:otherwise>
                	    <xsl:attribute name="onClick">if(confirm('<xsl:value-of select="$obj/confirm/@msg"/>'){
						<xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/>}</xsl:attribute>						
                	</xsl:otherwise>
                </xsl:choose>
			</xsl:when>
			<xsl:otherwise>
           	    <xsl:attribute name="onClick"><xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/></xsl:attribute>						
			</xsl:otherwise>
		</xsl:choose>
	</input>
	<div style="display:none" class="hiddenForWords">
       	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template>
		<xsl:if test="$obj/confirm/@msg">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/confirm/@msg"/></xsl:with-param></xsl:call-template>
		</xsl:if>
    </div>
	
</xsl:template>

<!--##################################################
    ## objLink                                      ##
	################################################## -->
<xsl:template name="objLink">
<xsl:param name="obj"/>
	<a>
		<xsl:call-template name="putCommonAttributes">
			<xsl:with-param name="obj" select="$obj" />
		</xsl:call-template>
		<xsl:if test="$obj/attribute[@name='target']">
			<xsl:attribute name="target"><xsl:value-of select="$obj/attribute[@name = 'target']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name='href']">
				<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/><xsl:value-of select="$obj/attribute[@name = 'href']/@value"/></xsl:attribute>		
			</xsl:when>
			<xsl:when test="$obj/attribute[@name='url']">
				<xsl:attribute name="href"><xsl:value-of select="$obj/attribute[@name = 'url']/@value"/></xsl:attribute>		
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
		<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
		<xsl:attribute name="onClick"><xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/></xsl:attribute>
		
	</img>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
    </div>
</xsl:template>

<!--##################################################
    ## objInfoBox                                   ##
	################################################## -->
<xsl:template name="objInfoBox">
<xsl:param name="obj"/>
<xsl:call-template name="infoBox">
	<xsl:with-param name="infoText"><xsl:value-of select="$obj/@msg"/><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param>
</xsl:call-template>
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
		<xsl:attribute name="onChange"><xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/></xsl:attribute>
	</input>
</xsl:template>

<!--##################################################
    ## objnNormalButton                             ##
	<object type="normalButton">
		<attribute class>
		<attribute style>
		<attribute value>
		<attribute onClick>
		<attribute noPutText>
	</object>
	################################################## -->
<xsl:template name="objNormalButton">
<xsl:param name="obj" />
<input type="button"> 
	<xsl:call-template name="putCommonAttributes">
		<xsl:with-param name="obj" select="$obj" />
	</xsl:call-template>
	<xsl:choose>
		<xsl:when test="($obj/attribute[@name = 'noPutText']) or ($obj/attribute[@name = 'dontUsePutText'])">
			<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:attribute>		
		</xsl:when>
		<xsl:otherwise>
				<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
	
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'onClick']/@value">
			<xsl:attribute name="onClick"><xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/></xsl:attribute>	
		</xsl:when>
		<xsl:when test="$obj/attribute[@name = 'href']/@value">
			<xsl:attribute name="onClick">document.location='<xsl:value-of select="$obj/attribute[@name = 'href']/@value"/>'</xsl:attribute>	
		</xsl:when>
	</xsl:choose>
</input>
<xsl:if test="$stringUpdate = 'yes' and not($obj/attribute[@name = 'noPutText'])">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template>
</xsl:if>
</xsl:template>

<!--##################################################
    ## document                                     ##
	################################################## -->
<xsl:template name="objDocument">
<xsl:param name="obj" />
	<xsl:choose>
		<xsl:when test="contains($obj/record/field[@name = 'SERVER_PATH']/@value,'JPG') or contains($obj/record/field[@name = 'SERVER_PATH']/@value,'GIF') or contains($obj/record/field[@name = 'SERVER_PATH']/@value,'PNG')">
			<img>
				<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute> 
				<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute> 
				<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$obj/record/field[@name = 'DOC_ID']/@value"/>&amp;width=<xsl:value-of select="$obj/attribute[@name = 'width']/@value"/>&amp;height=<xsl:value-of select="$obj/attribute[@name = 'height']/@value"/></xsl:attribute>
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
</xsl:template>

<!--##################################################
    ## objPassword                                  ##
	################################################## -->
<xsl:template name="objPassword">
<xsl:param name="obj" />
<input type="password" class="norm" onfocus="select()">
	<!--name-->
	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>	
	<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name='class']/@value"/></xsl:attribute>	
	<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>	
	<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:attribute>	
	<xsl:attribute name="onBlur"><xsl:value-of select="$obj/attribute[@name = 'onBlur']/@value"/></xsl:attribute>
	<xsl:attribute name="onChange"><xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/></xsl:attribute>

	<xsl:if test="$obj/attribute[@name = 'notUpdateable'] or $obj/ancestor::obj[@type='form']/readOnly">
		<xsl:attribute name="readOnly">true</xsl:attribute>
	</xsl:if>
</input>
<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
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
			<input type="text" onkeypress="return noenter()" >
				<xsl:if test="$obj/attribute[@name = 'class']">
					<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
				</xsl:if>
				<xsl:if test="$obj/attribute[@name = 'style']">
					<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
				</xsl:if>
				<xsl:if test="$obj/attribute[@name = 'showDayOfWeek']">
					<xsl:attribute name="onChange">setDayOfWeekBox('<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>','<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>');</xsl:attribute>				
				</xsl:if>
				<xsl:attribute name="onBlur">if(!(validateDate(document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>))){alert('<xsl:call-template name="jPutText"><xsl:with-param name="key">DateErrMsg</xsl:with-param></xsl:call-template>');
				document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>.focus()}</xsl:attribute>

				<xsl:if test="$obj/attribute[@name = 'notUpdateable'] or $obj/ancestor::obj[@type='form']/readOnly">
					<xsl:attribute name="style">background-color: #EEEEEE;color: #555555;font-style: italic;margin-right: 1;margin-left: 1;
					<xsl:value-of select="$obj/attribute[@name = 'style']/@value"/>
					</xsl:attribute>
					<xsl:attribute name="readOnly">true</xsl:attribute>
				</xsl:if>

				<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>

				<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:attribute>				

				<div style="display:none" class="hiddenForWords">
                	<xsl:call-template name="putText"><xsl:with-param name="key">DateErrMsg</xsl:with-param></xsl:call-template>
                </div>
			</input>
		</td>
		<xsl:if test="$obj/attribute[@name = 'showDayOfWeek']">
			<td class="tight">
				<input type="text" readonly="true" tabindex="-1">
					<xsl:if test="$obj/attribute[@name = 'class']">
						<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
					</xsl:if>
					<xsl:attribute name="style">background-color: #EEEEEE;color: #555555;font-style: italic;margin-right: 1;margin-left: 1;
						<xsl:value-of select="$obj/attribute[@name = 'style']/@value"/>
					</xsl:attribute>
					<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_dayOfWeek</xsl:attribute>
				</input>
			</td>
		</xsl:if>
		<xsl:if test="not($obj/attribute[@name = 'notUpdateable'] or $obj/ancestor::obj[@type='form']/readOnly)">
			<td class="tight">
				<xsl:call-template name="putCurDate">
					<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
					<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:with-param>
					<xsl:with-param name="obj" select="$obj"></xsl:with-param>
				</xsl:call-template>		
			</td>		
			<td class="tight" >
				<xsl:call-template name="putCalendarLink">
					<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
					<xsl:with-param name="date"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param>
					<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:with-param>
				</xsl:call-template>		
			</td>
			<xsl:if test="$obj/attribute[@name = 'clearable']">
				<td class="tight">
					<a class="prodbutton">
						<xsl:attribute name="onClick">document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>.value = ''</xsl:attribute>
						<img border="0">
							<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">ClearDate</xsl:with-param></xsl:call-template></xsl:attribute>
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>minus.gif</xsl:attribute>
						</img>
						<div style="display:none" class="hiddenForWords">
                        	<xsl:call-template name="putText"><xsl:with-param name="key">ClearDate</xsl:with-param></xsl:call-template>
                        </div>
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
<xsl:param name="obj" />
<a class="prodButton" tabindex="-1">
	<xsl:attribute name="href">javascript:insertCurDate(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>,'<xsl:value-of select="$obj/attribute[@name = 'format']/@value" />');
		<xsl:if test="$obj/attribute[@name = 'showDayOfWeek']">setDayOfWeekBox('<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>','<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>');</xsl:if>	
	</xsl:attribute>
	<img border="0">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>nowCalendar.gif</xsl:attribute>
		<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Todays Date</xsl:with-param></xsl:call-template></xsl:attribute>
	</img>
</a>
<div style="display:none" class="hiddenForWords">
	<xsl:call-template name="putText"><xsl:with-param name="key">Todays Date</xsl:with-param></xsl:call-template>
</div>
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
<td>
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
//		arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
	pushSelectBox('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>'));
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
<textarea ondblclick="storeCaret(this)"
	onselect="storeCaret(this);" onclick="storeCaret(this)" onkeyup="storeCaret(this)" onmouseup="storeCaret(this)"> 
	<xsl:attribute name="onBlur">lastTextArea =  'document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>';</xsl:attribute>
	<xsl:if test="$obj/attribute[@name = 'notUpdateable']  or $obj/ancestor::obj[@type='form']/readOnly">
		<xsl:attribute name="readOnly">true</xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'ondblclick']">
		<xsl:attribute name="ondblclick">storeCaret(this);<xsl:value-of select="$obj/attribute[@name = 'ondblclick']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'onfocus']">
		<xsl:attribute name="onfocus">storeCaret(this);<xsl:value-of select="$obj/attribute[@name = 'onfocus']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'onFocus']">
		<xsl:attribute name="onfocus">storeCaret(this);<xsl:value-of select="$obj/attribute[@name = 'onFocus']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'onBlur']">
		<xsl:attribute name="onBlur"><xsl:value-of select="$obj/attribute[@name = 'onBlur']/@value"/>;lastTextArea =  'document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>';</xsl:attribute>
	</xsl:if>
	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
	<xsl:attribute name="rows"><xsl:value-of select="$obj/attribute[@name = 'size']/@value"/></xsl:attribute>
	<xsl:attribute name="cols"><xsl:value-of select="$obj/attribute[@name = 'cols']/@value"/></xsl:attribute>
	<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'notUpdateable']/@value or $obj/ancestor::obj[@type='form']/readOnly">
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

<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
<xsl:if test="$obj/attribute[@name='setDefault'] or $obj/attribute[@name='onLoadFocus']">
	<script language="javascript">
		objFocusOn = document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>
	</script>
</xsl:if>
</xsl:template>


<!--##################################################
    ## objSearchDrop                                  ##
	## attributes:
	## multiple = if exeists then multiple can be selected
	## input: 
so = so & openObj("","searchDrop") & addAtt("size","1") & addAtt("name","PROCEDURE_ID") & addAtt("class","tt")
	so = so & "<sql>"
		so = so & xmlEncode("SELECT TOP 30 ID,NAME FROM A_V_PROCEDURES_APPROVED_DATA WHERE CREATING_CO = " & nullify(session("ROOT_COMPANY"),"s"))
	so = so & "</sql>"
	so = so & "<sort>"
		so = so & " ORDER BY NAME "
	so = so & "</sort>"
	so = so & "<pathToTop>" & strPathToTop & "</pathToTop>"
	so = so & addAtt("searchLabel","Search Label")
	so = so & "<searchField joiner=""AND"">NAME</searchField>"
	so = so & "<showField>NAME</showField><valField>ID</valField>"
	if not boolNew then
		so = so & "<defaultData>"
			so = so & "<record>"
				so = so & "<field name=""PROCEDURE_NAME"" value=""" & xmlEncode(objRS("PROCEDURE_NAME")) & """/>"
				so = so & "<field name=""PROCEDURE_ID"" value=""" & xmlEncode(objRS("PROCEDURE_ID")) & """/>"
			so = so & "</record>"
		so = so & "</defaultData>"
	end if
so = so & closeobj()
	################################################## -->
<xsl:template name="objSearchDrop">
<xsl:param name="obj"/>
<script>
	var boolHide<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>=true;
</script>
<xsl:choose>
	<xsl:when test="$obj/attribute[@name = 'notUpdateable']/@value  or $obj/ancestor::obj[@type='form']/readOnly">
				<select>
					<xsl:call-template name="putCommonAttributes">
						<xsl:with-param name="obj" select="$obj" />
					</xsl:call-template>
					<xsl:if test="$obj/attribute[@name = 'notUpdateable']  or $obj/ancestor::obj[@type='form']/readOnly">
						<xsl:attribute name="disabled">true</xsl:attribute>
						<xsl:attribute name="style">background-color:#EEEEEE</xsl:attribute>
					</xsl:if>
					<xsl:if test="$obj/attribute[@name='multiple']"><xsl:attribute name="multiple">multiple</xsl:attribute></xsl:if>
					<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'matchDataSize']">
							<xsl:choose>
								<xsl:when test="not($obj/data/record)">
									<xsl:attribute name="size"><xsl:value-of select="count(defaultData/data)"/></xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="size"><xsl:value-of select="count(data/record)"/></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="size"><xsl:value-of select="$obj/attribute[@name = 'size']/@value"/></xsl:attribute>
						</xsl:otherwise>
					</xsl:choose>
					<!--Options-->	
					<xsl:choose>
						<xsl:when test="$obj/defaultData/record">
							<xsl:variable name="show"><xsl:value-of select="$obj/showField"/></xsl:variable>
							<xsl:variable name="val"><xsl:value-of select="$obj/valField"/></xsl:variable>
							<xsl:for-each select="defaultData/record">
								<option>
									<xsl:attribute name="value"><xsl:value-of select="field[@name = $val]/@value"/></xsl:attribute>
									<xsl:value-of select="field[@name = $show]/@value"/>				
								</option>
							</xsl:for-each>
						</xsl:when>
						<xsl:otherwise>
								<option>
									<xsl:attribute name="value"></xsl:attribute>
									<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'searchInstructions']/@value"/></xsl:with-param></xsl:call-template>
								</option>
						</xsl:otherwise>
					</xsl:choose>
				</select>
	</xsl:when>
	<xsl:otherwise>
<div class="tight">
	<xsl:attribute name="id">mainDiv____<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
	<table class="tight">
		<tr>
			<td class="">
				<xsl:choose>
					<xsl:when test="$obj/sql">
						<input style="width:5em;" type="text" onkeypress="return noenter()" ><xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>____SearchText</xsl:attribute><xsl:attribute name="onChange">searchDropDown(this,this.form.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>,'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="$obj/sql"/></xsl:with-param></xsl:call-template>','<xsl:value-of select="searchField"/>','<xsl:value-of select="searchField/@joiner"/>','<xsl:value-of select="pathToTop"/>','<xsl:value-of select="showField"/>','<xsl:value-of select="valField"/>','<xsl:value-of select="sort"/>');</xsl:attribute></input>
					</xsl:when>
					<xsl:otherwise>
						<input type="text" onkeypress="return noenter()">
							<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>____SearchText</xsl:attribute>
							<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
							<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
							<xsl:attribute name="onChange">searchDropDownII(this,this.form.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>,'table=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="$obj/table"/></xsl:with-param></xsl:call-template>&amp;addFields=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="$obj/addFields"/></xsl:with-param></xsl:call-template>&amp;sfj=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="$obj/searchField/@joiner"/></xsl:with-param></xsl:call-template>&amp;sf=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="$obj/searchField"/></xsl:with-param></xsl:call-template>&amp;showField=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="showField"/></xsl:with-param></xsl:call-template>&amp;valField=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="valField"/></xsl:with-param></xsl:call-template>&amp;sort=<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="sort"/></xsl:with-param></xsl:call-template>&amp;<xsl:call-template name="getSearchFields"><xsl:with-param name="obj" select="$obj" /></xsl:call-template>','<xsl:value-of select="pathToTop"/>');</xsl:attribute>
						</input>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:if test="$obj/attribute[@name='onLoadFocus']">
					<script language="javascript">
						objFocusOn = document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>____SearchText;
					</script>
				</xsl:if>
			</td>
			<td class="" style="padding-top:2px;">
				<select>
					<xsl:if test="$obj/attribute[@name='multiple']"><xsl:attribute name="multiple">multiple</xsl:attribute></xsl:if>
					<xsl:call-template name="putCommonAttributes">
						<xsl:with-param name="obj" select="$obj" />
					</xsl:call-template>
					<xsl:if test="$obj/attribute[@name = 'notUpdateable']  or $obj/ancestor::obj[@type='form']/readOnly">
						<xsl:attribute name="disabled">true</xsl:attribute>
						<xsl:attribute name="style">background-color:#EEEEEE</xsl:attribute>
					</xsl:if>
					<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'matchDataSize']">
							<xsl:choose>
								<xsl:when test="not($obj/data/record)">
									<xsl:attribute name="size"><xsl:value-of select="count(defaultData/data)"/></xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="size"><xsl:value-of select="count(data/record)"/></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="size"><xsl:value-of select="$obj/attribute[@name = 'size']/@value"/></xsl:attribute>
						</xsl:otherwise>
					</xsl:choose>
					<!--Options-->	
					<xsl:choose>
						<xsl:when test="$obj/defaultData/record">
							<xsl:variable name="show"><xsl:value-of select="$obj/showField"/></xsl:variable>
							<xsl:variable name="val"><xsl:value-of select="$obj/valField"/></xsl:variable>
							<xsl:for-each select="defaultData/record">
								<option>
									<xsl:attribute name="value"><xsl:value-of select="field[@name = $val]/@value"/></xsl:attribute>
									<xsl:value-of select="field[@name = $show]/@value"/>				
								</option>
							</xsl:for-each>
						</xsl:when>
						<xsl:otherwise>
								<option>
									<xsl:attribute name="value"></xsl:attribute>
									<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'searchInstructions']/@value"/></xsl:with-param></xsl:call-template>
								</option>
						</xsl:otherwise>
					</xsl:choose>
				</select>
			</td>
		</tr>
	</table>
</div>

	</xsl:otherwise>		
</xsl:choose>




<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
</xsl:template>

<!--##################################################
    ##  getSearchFields                             ##
	################################################## -->
<xsl:template name="getSearchFields">
<xsl:param name="obj"/><xsl:for-each select="$obj/searchField"><xsl:variable name="pos" select="position()" /><xsl:variable name="ms">mainSearch<xsl:value-of select="$pos"/></xsl:variable><xsl:for-each select="@*"><xsl:choose>
	<xsl:when test="string(name())='mainSearch'">searchString<xsl:value-of select="$pos"/>=' + escape(this.value) + '&amp;</xsl:when>
	<xsl:when test="string(name())='formField'">searchString<xsl:value-of select="$pos"/>=' + escape(this.form.<xsl:value-of select="."/>.value) + '&amp;</xsl:when>
	<xsl:otherwise><xsl:value-of select="name()"/><xsl:value-of select="$pos"/>=' + escape('<xsl:value-of select="."/>') + '&amp;</xsl:otherwise></xsl:choose></xsl:for-each></xsl:for-each>
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
	## multiple = if exeists then multiple can be selected
	## input: 
		<obj type="objDropDown">
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="defaultPutText" value="whatever">
			<attribute name="actualPutText" value="whatever">
			<attribute name="matchDataSize" value="anything">
			<attribute name="defaultValue" value="value">
			<attribute name="notEditable" value="wahtever" />
			<attribute name="copyPrev" value="nameOfDropDownToCopy" />
			<attribute name="showLevels" value="whatever" />
			<attribute name="showField" value="whatever" />
			<attribute name="valField" value="whatever" />
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
				<xsl:attribute name="onchange"><xsl:if test="$obj/attribute[@name = 'dontDisable']/@value != 'true'">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");</xsl:if>clearSelectBox(document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'verbName']/@value"/>);clearSelectBox(document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name = 'nounName']/@value"/>);</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="not($obj/attribute[@name = 'noDisable']) and not($obj/attribute[@name = 'dontDisable'])">
						<xsl:attribute name="onChange">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");<xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/></xsl:attribute>			
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="onChange"><xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/></xsl:attribute>			
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>		
		</xsl:choose>
		<xsl:if test="$obj/attribute[@name = 'onChange']">
			<xsl:attribute name="onChange"><xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/></xsl:attribute>			
		</xsl:if>
		<xsl:if test="$obj/attribute[@name = 'onFocus']">
			<xsl:attribute name="onFocus"><xsl:value-of select="$obj/attribute[@name = 'onFocus']/@value"/></xsl:attribute>			
		</xsl:if>
		<xsl:if test="$obj/attribute[@name = 'onDoubleClick']">
			<xsl:attribute name="onDoubleClick"><xsl:value-of select="$obj/attribute[@name = 'onDoubleClick']/@value"/></xsl:attribute>			
		</xsl:if>
		<xsl:if test="$obj/attribute[@name='multiple']"><xsl:attribute name="multiple">multiple</xsl:attribute></xsl:if>
		<xsl:if test="$obj/attribute[@name = 'notUpdateable']  or $obj/ancestor::obj[@type='form']/readOnly">
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
		<xsl:if test="$obj/attribute[@name = 'style']">
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name = 'matchDataSize']">
				<xsl:choose>
					<xsl:when test="not($obj/data/record)">
						<xsl:attribute name="size"><xsl:value-of select="count(defaultData/data)"/></xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="size"><xsl:value-of select="count(data/record)"/></xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="size"><xsl:value-of select="$obj/attribute[@name = 'size']/@value"/></xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:attribute name="selectIndex"><xsl:value-of select="$obj/attribute[@name = 'defaultValue']/@value"/></xsl:attribute>
	<!--Options-->	
		<xsl:choose>
			<xsl:when test="not($obj/data/record)">
				<xsl:variable name="defVal"><xsl:value-of select="string($obj/attribute[@name = 'defaultValue']/@value)"/></xsl:variable>				
				<xsl:variable name="show"><xsl:value-of select="string($obj/attribute[@name = 'showField']/@value)"/><xsl:value-of select="$obj/showField/@value"/></xsl:variable>				
				<xsl:variable name="val"><xsl:value-of select="string($obj/attribute[@name = 'valField']/@value)"/><xsl:value-of select="$obj/valField/@value"/></xsl:variable>				
				<xsl:for-each select="defaultData/data">
					<xsl:variable name="myVal"><xsl:value-of select="string(field[@name = $val]/@value)"/></xsl:variable>
					<option>
						<xsl:if test="$obj/currentValues/record/field[@name='value']/@value = $myVal"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>						
						<xsl:attribute name="value"><xsl:value-of select="@value"/><xsl:value-of select="field[@name = $val]/@value"/></xsl:attribute>
						<xsl:if test="$obj/attribute[@name = 'defaultValue'] and @value = $obj/attribute[@name = 'defaultValue']/@value"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>
						<xsl:if test="$boolDebug = 'true'">val='<xsl:value-of select="@value"/>' defaultval = '<xsl:value-of select="$obj/attribute[@name = 'defaultValue']/@value"/>'</xsl:if>
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'defaultPutText']">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@show"/></xsl:with-param></xsl:call-template>
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = $show]/@value"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="@show"/>		
								<xsl:value-of select="field[@name = $show]/@value"/>				
							</xsl:otherwise>
						</xsl:choose>
					</option>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="defVal"><xsl:value-of select="string($obj/attribute[@name = 'defaultValue']/@value)"/></xsl:variable>				
				<xsl:variable name="show"><xsl:value-of select="string($obj/attribute[@name = 'showField']/@value)"/><xsl:value-of select="$obj/showField/@value"/></xsl:variable>				
				<xsl:variable name="val"><xsl:value-of select="string($obj/attribute[@name = 'valField']/@value)"/><xsl:value-of select="$obj/valField/@value"/></xsl:variable>				
				<xsl:for-each select="$obj/data/record">
					<xsl:variable name="myVal"><xsl:value-of select="string(field[@name = $val]/@value)"/></xsl:variable>
					<option>
						<xsl:if test="$obj/attribute[@name = 'defaultValue'] and string($myVal) = string($defVal)"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>
						<xsl:if test="$obj/currentValues/record/field[@name=$val]/@value = $myVal"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>
<!--						<xsl:if test="string-length($myVal) = 0 and not($obj/currentValues/record) and not($obj/attribute[@name = 'defaultValue'] and string($myVal) = string($defVal))"><xsl:attribute name="selected">selected</xsl:attribute></xsl:if>-->
						<xsl:attribute name="value"><xsl:value-of select="field[@name = $val]/@value"/></xsl:attribute>
						<xsl:if test="$boolDebug = 'true'">
							is *<xsl:value-of select="$myVal"/>* = *<xsl:value-of select="$defVal"/>*
							<xsl:if test="$myVal = $defVal"> Yes They are the same</xsl:if>
						</xsl:if>
						<xsl:if  test="$obj/attribute[@name = 'showLevels']">
							<xsl:variable name="levelField"><xsl:value-of select="$obj/attribute[@name = 'showLevels']/@value"/></xsl:variable>
							<xsl:variable name="intLev"><xsl:value-of select="field[@name=$levelField]"/></xsl:variable>
							<xsl:variable name="curLevel" select="0" /><xsl:call-template name="putLevelDash">
								<xsl:with-param name="curLev" select="0" />
								<xsl:with-param name="maxLev" select="field[@name=$levelField]/@value"/>
							</xsl:call-template>
						</xsl:if>	
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'actualPutText'] or $obj/attribute[@name = 'usePutText'] or @usePutText">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = $show]/@value"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:when test="field[@name='USE_PUT_TEXT']/@value = 'True'">
								<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name = $show]/@value"/></xsl:with-param></xsl:call-template>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="field[@name = $show]/@value"/>					
							</xsl:otherwise>
						</xsl:choose>
					</option>
				</xsl:for-each>
			</xsl:otherwise>
		</xsl:choose>
	</select>
	<xsl:call-template name="putViewerButton">
		<xsl:with-param name="obj" select="$obj"/>
		<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
		<xsl:with-param name="boxName"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
	</xsl:call-template>
	<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
	<xsl:if test="$obj/attribute[@name='multiple'] and not($obj/attribute[@name='noSelectAll'])">
		<script language="javascript">
		pushSelectBox('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
		//arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
		</script>	
	</xsl:if>
</xsl:template>

<!--##################################################
    ##      putLevelDash                            ##
	################################################## -->
<xsl:template name="putLevelDash">
<xsl:param name="curLev" />
<xsl:param name="maxLev" />
<xsl:if test="$curLev &lt;= $maxLev">
	<xsl:choose>
	<xsl:when test="$curLev = ($maxLev)">+</xsl:when>
	<xsl:otherwise><xsl:text>|</xsl:text></xsl:otherwise>
	</xsl:choose>
	<xsl:call-template name="putLevelDash">
		<xsl:with-param name="curLev"><xsl:value-of select="$curLev + 1"/></xsl:with-param>
		<xsl:with-param name="maxLev"><xsl:value-of select="$maxLev"/></xsl:with-param>
	</xsl:call-template>
</xsl:if>
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
			<xsl:if test="not($obj/attribute[@name = 'noInfo'])">
				<xsl:call-template name="infoBox">
					<xsl:with-param name="infoText"><xsl:value-of select="$obj/@ID"/>_info</xsl:with-param>
				</xsl:call-template>
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
								<table class="tight">
									<tr>
										<xsl:if test="pageLink[@info]">
											<td>
												<xsl:call-template name="infoBox">
                                                	<xsl:with-param name="infoText">info_for_<xsl:value-of select="@name"/></xsl:with-param>
                                                </xsl:call-template>
											</td>
										</xsl:if>
										<td class="tight">
											<nobr>
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
											</nobr>
										</td>
									</tr>
								</table>
							</xsl:otherwise>										
						</xsl:choose>
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
	<xsl:call-template name="putCommonAttributes">
		<xsl:with-param name="obj" select="$obj" />
	</xsl:call-template>
	<xsl:attribute name="href"><xsl:value-of select="$obj/attribute[@name = 'href']/@value"/></xsl:attribute>	
	<img border="0">
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name = 'path']/@value">
					<xsl:attribute name="src"><xsl:value-of select="$obj/attribute[@name = 'path']/@value"/></xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$obj/attribute[@name = 'image']/@value"/></xsl:attribute>
			</xsl:otherwise>		
		</xsl:choose>
		<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:with-param></xsl:call-template><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
	</img>
</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
    </div>
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
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'viewWorkFlow'">
						<xsl:attribute name="onClick">
							if (document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>.WorkFlow.value)
								{
								var viewWindow = window.open('<xsl:value-of select="$path_to_top"/>asp/approvalWF/viewWF.asp?pageID=viewWorkFlow&amp;ID=' + document.<xsl:value-of select="$obj/ancestor::obj[@type = 'form']/attribute[@name='name']/@value"/>.WorkFlow.value,'PopUpWindow','width=500,height=400,location=yes,toolbar=no,resizable=yes,scrollbars=yes')
								}
						</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
				<div style="display:none" class="hiddenForWords">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
                </div>
			</input>
			<div style="display:none" class="hiddenForWords">
            	<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure you want to take it back</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure you want to delete it?</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure You WANT TO ACCEPT THIS TASK?</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure You WANT TO Reject THIS TASK?</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Can not forward to a group and a person</xsl:with-param></xsl:call-template>','<xsl:call-template name="jPutText"><xsl:with-param name="key">You need to pick a Role or Person to forward to.</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">Submitting This Task Will Close It</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">No Requestor</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">EnterADescription</xsl:with-param></xsl:call-template>
				<xsl:call-template name="putText"><xsl:with-param name="key">YouCanNotAssignBothAPersonAndARole</xsl:with-param></xsl:call-template>
            </div>
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
			<div style="display:none" class="hiddenForWords">
				<xsl:choose>
					<xsl:when test="$obj/attribute[@name = 'specialButton']/@value = 'TakeBackButton'">
						<xsl:call-template name="putText"><xsl:with-param name="key">Are You Sure you want to take it back</xsl:with-param></xsl:call-template>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
					</xsl:otherwise>
				</xsl:choose>
            </div>
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
<xsl:template name="putOneQitem"><xsl:param name="name" /><xsl:value-of select="/Doc_Webpage/queryString/item[@name = $name]/@value"/></xsl:template>

<!--##################################################
    ## objResultSet                                 ##
	## class: class of the table of the resultset
	## style: style of the table of the resultset
	## jumpTo: where to jumpto using the button
	## qString: if exists then put the Qstring info
	## noPaging: if exists then do not put the paging information.  Just print all results.
	## input:
		<obj type="resulset">
			<attribute name="noDataMessage" value="Message to display if no data" />
			<attribute name="class" value="whatever">
			<attribute name="style" value="whatever">
			<attribute name="noPaging" value="whatever">
			<attribute name="pagingAtBottom" value="whatever">
			<attribute name="standardSortButton" value="whatever" text="Push to Return To standard Search" sortBy = "fields to sort by">
			showEmptyColumns
			<columns [noTitles]>
				<column 
					putText="true" 
					pageID = page to go ot using this data.  The recordset turns into the querystring
					sortable = if it exists then it is a sortable column based on the field
					title = Title of the column
					field = field in the result set to print in this column
					class = the class of this column.  the title will be in a th and the results in a td
					special = if there is something sepecial to do it needs to be specified here and then the action to take for something special needs to be defined in the result set row.
					putText = if exists then use putText for the data in the column
					titleHelpIcon = title help message
					>
					<specialPutText> This is for using put text only when a rule is met
						<rule type="field">  a rule with type of field means that a field has to have a certain value to use put text
							<field/> the field whose value must be checked
							<value/> the value of the field to check
						</rule>
					</specialPutText>
					<showOnly>
						<rule type="field"> 
							<field/> the field whose value must be checked
							<value/> the value of the field to check
						</rule>
					</showOnly>
					<dateFormat>
						<datePart val="YEAR" /> 2005
						<datePart val="GENERAL_DATE" /> 6/10/2005 4:52:14 PM
						<datePart val="LONG_DATE" /> Friday, June 10, 2005
						<datePart val="SHORT_DATE" /> 6/10/2005
						<datePart val="LONG_TIME" /> returns 4:52:12 PM
						<datePart val="SHORT_TIME" /> returns time in militsary 15:00
						<datePart val="ANSWER_DATE" /> returns date formatted like this: 6/9/05 2:00PM
						<datePart val="MONTH" /> 6
						<datePart val="DAY" /> 10
						<datePart val="HOUR" /> 16
						<datePart val="MINUTES" /> 52
						<datePart val="SECONDS" /> 14
						<datePart val="STD_HOUR" /> 2
						<datePart val="SUFFIX" /> PM
						<datePart val="SMALL_YEAR" /> 05
						<datePart val="Anything Else" /> returns Anything Else (use for / or -)
					</dateFormat>
			</columns>
			<groupBy field="field to show groupongs of" displayField="field to display between groupings"" pretext="Text Before Display field in printout" />
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
<!-- When we have data this is the way we roll -->
	<xsl:when test="data/record">
		<!--put the paging information at the top-->
		<xsl:if test="not($obj/attribute[@name='noPaging']) and not($obj/attribute[@name = 'pagingAtBottom'])">
			<xsl:call-template name="paging">
				<xsl:with-param name="all" select="$obj/data/pageData"/>
				<xsl:with-param name="qItems" select="/Doc_Webpage/queryString/item[@name != 'curPage']" />
				<xsl:with-param name="prevPhrase">PrevArrow</xsl:with-param>
				<xsl:with-param name="nextPhrase">NextArrow</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
		<!--This is the resultset table-->
		<table class="resultSet">
			<!--Change the class if there is one-->
			<xsl:if test="$obj/attribute[@name = 'class']">
				<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>			
			</xsl:if>
			<!-- just set the style because we do not have any-->
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
			<!--if we are debugging then show the query string-->
			<xsl:if test="$boolDebug = 'true'">
				<tr>
					<td style="font-size: x-small;">
						<xsl:attribute name="colspan"><xsl:value-of select="count($obj/columns/column)"/></xsl:attribute>
						sql = <xsl:value-of select="$obj/data/sql"/>
					</td>
				</tr>
			</xsl:if>
			<!--if there is one column that is sortable then we need to tell them to sort by clicking-->
			<xsl:if test="$obj/columns/column/@sortable">
				<tr>
					<td align="left">
						<xsl:attribute name="colspan"><xsl:value-of select="count($obj/columns/column)"/></xsl:attribute>
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
			<!--If there is a table header then put it-->
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
			<!--Put the column titles-->
			<xsl:if test="not($obj/columns/@noTitles)">
				<tr>
					<xsl:for-each select="$obj/columns/column[not(@inputType) or @inputType!='hidden']">
						<xsl:variable name="ssField"><xsl:value-of select="@field"/></xsl:variable>
						<xsl:if test="(($obj/data/record[field[@name=$ssField and string-length(@value) &gt; 0]]) or (string-length($ssField)=0) or $obj/attribute[@name='showEmptyColumns'] or @alwaysShow)">
							<th class="resultSet">
								<xsl:if test="@class">
									<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
								</xsl:if>
								<xsl:if test="@style">
									<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>
								</xsl:if>
								
								<table class="tight">
									<tr>
										<xsl:if test="not (/Doc_Webpage/content/@printable)">
										<td class="tight">
											<xsl:call-template name="putColumnCollapser" />
										</td>
										</xsl:if>
										<td class="tight">
											<div>
												<xsl:attribute name="id">columnHeader__<xsl:value-of select="position()"/></xsl:attribute>
												<table class="tight">
													<tr>
														<xsl:if test="@titleHelpIcon">
															<td class="tight" valign="middle">
																<xsl:call-template name="infoBox">
					                                            	<xsl:with-param name="infoText"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param>
					                                            </xsl:call-template>
															</td>
														</xsl:if>
														<xsl:if test="@inputType = 'checkBox' and not(@noCheckAll)">
															<td class="tight" valign="middle">
																<xsl:call-template name="putCheckAllBox">
					                                            	<xsl:with-param name="col" select="."></xsl:with-param>
					                                            </xsl:call-template>
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
																	<xsl:choose>
																		<xsl:when test="title">
																			<xsl:apply-templates select="title/*"/>
																		</xsl:when>
																	
																		<xsl:when test="not(@titleDontUsePutText)">
																			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@title"/></xsl:with-param></xsl:call-template>
																		</xsl:when>
																		<xsl:otherwise>
																			<xsl:value-of select="@title"/>
																		</xsl:otherwise>
																	</xsl:choose>
																	<xsl:if test="@class='actions'">
																		<div id="openToolBox">
																		<a href="javascript:"> 
																			<img border="0" onClick="showRowActionItems();">
																				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>closedToolBox.gif</xsl:attribute>
																				<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Show the tools</xsl:with-param></xsl:call-template></xsl:attribute>
																			</img>
																		</a>
																		</div>
																		<div id="closeToolBox" style="display:none">
																		<a href="javascript:"> 
																			<img border="0" onClick="hideRowActionItems();">
																				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>openToolBox.gif</xsl:attribute>
																				<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Hide the tools</xsl:with-param></xsl:call-template></xsl:attribute>
																			</img>
																		</a>
																		</div>
																		
																		<div class="hiddenForWords">
																			<xsl:call-template name="putText"><xsl:with-param name="key">Show the tools</xsl:with-param></xsl:call-template>
																			<xsl:call-template name="putText"><xsl:with-param name="key">Hide the tools</xsl:with-param></xsl:call-template>
																		</div>
																		
																	</xsl:if>
																</td>
															</xsl:otherwise>
														</xsl:choose>
														<xsl:if test="$boolDebug = 'true'">(<xsl:value-of select="@field"/>)</xsl:if>
														<xsl:if test="@treeMainData">
															<td class="tight" valign="middle">
																<xsl:call-template name="treeMainDataToggle">
					                                            </xsl:call-template>
															</td>
														</xsl:if>
													</tr>
												</table>
											</div>
										</td>
									</tr>
								</table>
							</th>
						</xsl:if>
					</xsl:for-each>
				</tr>
			</xsl:if>
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'noForm']/@value">
				<xsl:for-each select="$obj/data/record">
					<!--Any Record could have post data to add to the normal stuff-->
					<xsl:apply-templates select="preRecordData"/>
					<xsl:call-template name="rowGrouper" />
					<tr>
						<xsl:if test="$obj/formID">
							<input type="hidden">
								<xsl:variable name="idField"><xsl:value-of select="$obj/formID/field"/></xsl:variable>
								<xsl:attribute name="name"><xsl:value-of select="$obj/formID/idName"/></xsl:attribute>
								<xsl:attribute name="value"><xsl:value-of select="./field[@name = $idField]/@value"/></xsl:attribute>
							</input>
						</xsl:if>
						<xsl:call-template name="printResultSetRow">
							<xsl:with-param name="resultSet" select="$obj" />
							<xsl:with-param name="record" select="." />
							<xsl:with-param name="position" select="position()" />
						</xsl:call-template>
					</tr>
					<!--Any Record could have post data to add to the normal stuff-->
					<xsl:apply-templates select="postRecordData"/>
					<xsl:if test="$obj/attribute[@name='specialResSet']/@value = 'MainMenu'">
						<xsl:call-template name="printMainMenuWarningRow">
							<xsl:with-param name="resultSet" select="$obj" />
							<xsl:with-param name="record" select="." />
							<xsl:with-param name="position" select="position()" />
						</xsl:call-template>
					</xsl:if>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<form name="standardUpdateForm" method="post">
					<xsl:attribute name="action"><xsl:value-of select="$obj/attribute[@name = 'formAction']/@value"/></xsl:attribute>
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
							<xsl:if test="$obj/attribute[@name='specialResSet']/@value = 'MainMenu'">
								<xsl:call-template name="printMainMenuWarningRow">
									<xsl:with-param name="resultSet" select="$obj" />
									<xsl:with-param name="record" select="." />
									<xsl:with-param name="position" select="position()" />
								</xsl:call-template>
							</xsl:if>
					</xsl:for-each>
					<xsl:if test="$obj/attribute[@name = 'formAction']">
						<tr>
							<td style="text-align:center">
								<xsl:attribute name="colspan"><xsl:value-of select="count($obj/columns/column)"/></xsl:attribute>
								<input type="submit" value="Update"/>
							</td>
						</tr>
					</xsl:if>
				</form>
			</xsl:otherwise>
			</xsl:choose>
		</table>
		<xsl:if test="not($obj/attribute[@name='noPaging']) and ($obj/attribute[@name = 'pagingAtBottom'])">
			<xsl:call-template name="paging">
				<xsl:with-param name="all" select="$obj/data/pageData"/>
				<xsl:with-param name="qItems" select="/Doc_Webpage/queryString/item[@name != 'curPage']" />
				<xsl:with-param name="prevPhrase">PrevArrow</xsl:with-param>
				<xsl:with-param name="nextPhrase">NextArrow</xsl:with-param>
			</xsl:call-template>
		</xsl:if>
		<xsl:if test="$boolDebug = 'true'">
		<table class="tight" style="border:thin ridge;font-size: x-small;" >
			<tr>
				<xsl:for-each select="$obj/data/record[position() = 1]/field">
					<th style="border:thin ridge;font-size: x-small;"><xsl:value-of select="@name"/></th>
				</xsl:for-each>	
			</tr>
			<xsl:for-each select="$obj/data/record">
				<tr>
					<xsl:for-each select="field">
						<td style="border:thin ridge;font-size: x-small;"><xsl:value-of select="@value"/></td>
					</xsl:for-each>	
				</tr>
			</xsl:for-each>	
		</table>
		</xsl:if>
	</xsl:when>
	<xsl:otherwise>
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name='noDataMessage']">
				<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='noDataMessage']/@value"/></xsl:with-param></xsl:call-template>
			</xsl:when>
			<xsl:otherwise>			
				<xsl:call-template name="putText"><xsl:with-param name="key">No Matching Data Please Change Search Criteria</xsl:with-param></xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ##  rowGrouper                                  ##
	################################################## -->
<xsl:template name="rowGrouper">
<xsl:if test="../../groupBy">
	<xsl:variable name="field"><xsl:value-of select="../../groupBy/@field"/></xsl:variable>
	<xsl:variable name="groupField"><xsl:value-of select="../../groupBy/@displayField"/></xsl:variable>
	<xsl:variable name="myField"><xsl:value-of select="field[@name = $field]/@value"/></xsl:variable>
	<xsl:variable name="myDisplayField"><xsl:value-of select="field[@name = $groupField]/@value"/></xsl:variable>
	<xsl:variable name="myPosition" select="position()"/>
	<xsl:variable name="prevPosition" ><xsl:value-of select="position()-1"/></xsl:variable>
	<xsl:variable name="prevDisplayField"><xsl:value-of select="../record[position() = $prevPosition]/field[@name = $groupField]/@value"/></xsl:variable>
<xsl:if test="$prevDisplayField = '' or $prevDisplayField != $myDisplayField">
<tr>
	<td>
 		<xsl:attribute name="colspan"><xsl:value-of select="count(../../columns/column)"/></xsl:attribute>
		<xsl:choose>
			<xsl:when test="field[@name='NUM_DROPPED_TESTS']/@value = '1'">
				<xsl:call-template name="putText"><xsl:with-param name="key">When we roll back the test </xsl:with-param></xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="putText"><xsl:with-param name="key">and we also roll back the test</xsl:with-param></xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:text> </xsl:text><xsl:value-of select="$myDisplayField"/>
	</td>
</tr>
</xsl:if>

</xsl:if>

</xsl:template>

<!--##################################################
    ##  putColumnCollapser                          ##
	################################################## -->
<xsl:template name="putColumnCollapser">
<xsl:param name="obj"/>
<xsl:if test="@class != 'counter' and @class != 'actions' and /Doc_Webpage/searchFilePath">
	<td valign="top">
		<div>
			<xsl:attribute name="id">column_collapser__<xsl:value-of select="position()"/></xsl:attribute>
			<a href="javascript:" class="collapse">
				<xsl:attribute name="onClick">collapseColumn(<xsl:value-of select="position()"/>);createCookie('<xsl:value-of select="/Doc_Webpage/searchFilePath"/>_ColumnCollapsed_<xsl:value-of select="position()"/>','<xsl:value-of select="position()"/>',365);</xsl:attribute>
				<img border="0">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>columnOpened.gif</xsl:attribute>
					<xsl:attribute name="title">
						<xsl:call-template name="putText"><xsl:with-param name="key">Collapse the column</xsl:with-param></xsl:call-template>
						<xsl:text> </xsl:text>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@title"/></xsl:with-param></xsl:call-template>
					</xsl:attribute>
				</img>
			</a>
		</div>	
		<div style="display:none">
			<xsl:attribute name="id">column_expander__<xsl:value-of select="position()"/></xsl:attribute>
			<a href="javascript:" class="collapse">
				<xsl:attribute name="onClick">eraseCookie('<xsl:value-of select="/Doc_Webpage/searchFilePath"/>_ColumnCollapsed_<xsl:value-of select="position()"/>');expandColumn(<xsl:value-of select="position()"/>)</xsl:attribute>
				<img border="0">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>columnClosed.gif</xsl:attribute>
					<xsl:attribute name="title">
						<xsl:call-template name="putText"><xsl:with-param name="key">Expand the column</xsl:with-param></xsl:call-template>
						<xsl:text> </xsl:text>
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@title"/></xsl:with-param></xsl:call-template>
					</xsl:attribute>
				</img>
			</a>
		</div>
	</td>
	<div style="display:none" class="hiddenForWords">
        <xsl:call-template name="putText"><xsl:with-param name="key">Collapse the column</xsl:with-param></xsl:call-template>
        <xsl:call-template name="putText"><xsl:with-param name="key">Expand the column</xsl:with-param></xsl:call-template>
    </div>
</xsl:if>
</xsl:template>


<!--##################################################
    ##  printMainMenuWarningRow                     ##
	################################################## -->
<xsl:template name="printMainMenuWarningRow">
<xsl:param name="resultSet" />
<xsl:param name="record" />
<xsl:param name="position" />
<xsl:if test="$record/field[@name='NAME']/@value='Actual Parts'">
	<tr>
		<td>
			<xsl:attribute name="colspan"><xsl:value-of select="count($resultSet/columns/column) - 1"/></xsl:attribute>
			<div>
				<xsl:attribute name="ID">PART_SAFETY_STOCK_WARNING</xsl:attribute>
			</div>
			<xsl:apply-templates select="/Doc_Webpage/PART_SAFETY_STOCK_WARNING/*" />
		</td>
	</tr>
</xsl:if>
<xsl:variable name="myID"><xsl:value-of select="$record/field[@name='ID']/@value"/></xsl:variable>
<tr>
	<td class="MainMenuWarning">
		<xsl:attribute name="colspan"><xsl:value-of select="count($resultSet/columns/column) - 1"/></xsl:attribute>
		<div>
			<xsl:attribute name="id"><xsl:value-of select="$myID"/></xsl:attribute>
		</div>
	</td>
</tr>
<xsl:for-each select="/Doc_Webpage/content/default_body/div/MM_WARN_DATA/w/i[contains(@d,$myID)]">
	<xsl:if test="@v != 0 and @v != ''">
		<tr>
			<td class="MainMenuWarning">
				<xsl:attribute name="colspan"><xsl:value-of select="count($resultSet/columns/column) - 1"/></xsl:attribute>
				<div>
					<xsl:attribute name="id"><xsl:value-of select="$myID"/></xsl:attribute>
					<xsl:value-of select="$myID"/>
				</div>
				<a>
					<xsl:attribute name="class"><xsl:value-of select="@c"/></xsl:attribute>
					<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/><xsl:value-of select="@h"/></xsl:attribute>
					<xsl:call-template name="putText"><xsl:with-param name="key">Wrn_<xsl:value-of select="@d"/>_<xsl:value-of select="@n"/></xsl:with-param></xsl:call-template>
					<xsl:text> </xsl:text>
					<xsl:value-of select="@v"/>
				</a>
			</td>
		</tr>
	</xsl:if>
</xsl:for-each>

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
    ## columnGetStyle                               ##
	################################################## -->
<xsl:template name="columnGetStyle">
<xsl:param name="record" />
<xsl:param name="field" />
<xsl:param name="column" />
<xsl:value-of select="@style"/>;
<xsl:if test="@class != 'actions' and @class != 'counter'">
	<xsl:value-of select="$record/style"/>;
	<xsl:value-of select="$record/columnStyle[@field = $field]"/>;
</xsl:if>
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
<xsl:for-each select="$resultSet/columns/column[not(@inputType) or @inputType!='hidden']">
	<xsl:variable name="ssField"><xsl:value-of select="@field"/></xsl:variable>
	<xsl:if test="(($resultSet/data/record[field[@name=$ssField and string-length(@value) &gt; 0]]) or (string-length($ssField)=0) or $resultSet/attribute[@name='showEmptyColumns'] or @alwaysShow)">
    <td>
			<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
			<xsl:attribute name="style"><xsl:call-template name="columnGetStyle">
					<xsl:with-param name="record" select="$record" />
					<xsl:with-param name="field" select="$ssField" />
					<xsl:with-param name="column" select="." />
				</xsl:call-template>
			</xsl:attribute>
			<div class="tight">
        <xsl:attribute name="id">res_column__<xsl:value-of select="$position"/>__<xsl:value-of select="position()"/></xsl:attribute>
				<xsl:choose>
					<xsl:when test="@noBreak">
						<nobr>
							<xsl:call-template name="printResultSetPart1">
								<xsl:with-param name="resultSet" select="$resultSet" />
								<xsl:with-param name="record" select="$record" />
								<xsl:with-param name="position" select="$position" />
							</xsl:call-template>
						</nobr>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="printResultSetPart1">
								<xsl:with-param name="resultSet" select="$resultSet" />
								<xsl:with-param name="record" select="$record" />
								<xsl:with-param name="position" select="$position" />
						</xsl:call-template>				
					</xsl:otherwise>
				</xsl:choose>			
			</div>
		</td>
	</xsl:if>
</xsl:for-each>
<xsl:for-each select="$resultSet/columns/column[@inputType='hidden']">
	<xsl:variable name="ssField"><xsl:value-of select="@field"/></xsl:variable>
	<xsl:call-template name="printResultSetPart1">
		<xsl:with-param name="resultSet" select="$resultSet" />
		<xsl:with-param name="record" select="$record" />
		<xsl:with-param name="position" select="$position" />
	</xsl:call-template>
</xsl:for-each>

</xsl:template>

<!--##################################################
    ##  printResultSetPart1                         ##
	################################################## -->
<xsl:template name="printResultSetPart1">
<xsl:param name="resultSet" />
<xsl:param name="record" />
<xsl:param name="position" />
<!--We need to add the showOnly tester-->
	<xsl:choose>
		<xsl:when test="./attribute[@name = 'showOnly']">
			<xsl:variable name="showOnly"><xsl:call-template name="getShowOnlyList">
					<xsl:with-param name="obj" select="." />
				</xsl:call-template>
			</xsl:variable>
			<xsl:variable name="showOnlyHere"><xsl:call-template name="getShowOnlyHereList">
					<xsl:with-param name="obj" select="." />
					<xsl:with-param name="record" select="$record" />
				</xsl:call-template>
			</xsl:variable>
			<xsl:if test="$boolDebug='true'">showOnly = "<xsl:value-of select="$showOnly"/>" showOnlyHere = "<xsl:value-of select="$showOnlyHere"/>"</xsl:if>
			<xsl:choose>
				<xsl:when test="$showOnly = $showOnlyHere">
					<xsl:call-template name="printResultSetPart2">
						<xsl:with-param name="resultSet" select="$resultSet" />
						<xsl:with-param name="record" select="$record" />
						<xsl:with-param name="position" select="$position" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="@showOnlyReplacementText">
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@showOnlyReplacementText"/></xsl:with-param></xsl:call-template>
					</xsl:if>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<xsl:call-template name="printResultSetPart2">
				<xsl:with-param name="resultSet" select="$resultSet" />
				<xsl:with-param name="record" select="$record" />
				<xsl:with-param name="position" select="$position" />
			</xsl:call-template>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>		

<!--##################################################
    ##  printResultSetPart2                         ##
	################################################## -->
<xsl:template name="printResultSetPart2">
<xsl:param name="resultSet" />
<xsl:param name="record" />
<xsl:param name="position" />

  <xsl:variable name="infoText">Info_About_<xsl:call-template name="printOneItem">
				<xsl:with-param name="field" select="@field"/>
				<xsl:with-param name="record" select="$record"/>
			</xsl:call-template>
</xsl:variable>
<xsl:variable name="ssField"><xsl:value-of select="@field"/></xsl:variable>
<xsl:choose>
<!--Action Column -->		
	<xsl:when test="@class = 'actions'">
		<xsl:choose>
			<xsl:when test="@style"><xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute></xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="style">text-align:left;color:silver;font-weight:300;</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
		
		<table class="tight">
			<tr>
				<td class="tight">
					<div  class="prodButton">
						<xsl:attribute name="id">SMALL_TOOL_BOX_<xsl:value-of select="$position"/></xsl:attribute>
						<xsl:attribute name="onMouseOver">hideTogDiv('SMALL_TOOL_BOX_<xsl:value-of select="$position"/>');showTogDiv('SMALL_TOOLS_<xsl:value-of select="$position"/>');showTogDiv('SMALL_TOOL_BOX_CLOSER_<xsl:value-of select="$position"/>');</xsl:attribute>
						<img>
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>smallToolBoxClosed.gif</xsl:attribute>
						</img>
					</div>
				</td>
				<td class="tight">
					<div style="display:none">
						<xsl:attribute name="id">SMALL_TOOLS_<xsl:value-of select="$position"/></xsl:attribute>
								<xsl:for-each select="$resultSet/rowActionObjects/obj[@type='rowActionButton']">
									<xsl:variable name="processObj"><xsl:call-template name="checkObjPermissions"><xsl:with-param name="obj" select="." /></xsl:call-template></xsl:variable>
									<xsl:if test="$processObj = 'true'">
											<xsl:call-template name="objRowActionButton">
												<xsl:with-param name="obj" select="." />
												<xsl:with-param name="record" select="$record" />					
											</xsl:call-template>				
									</xsl:if>
								</xsl:for-each>
						
					</div>
					
				</td>
				<td class="tight">
					<div style="display:none">
						<xsl:attribute name="id">SMALL_TOOL_BOX_CLOSER_<xsl:value-of select="$position"/></xsl:attribute>
						<xsl:attribute name="onMouseOver">showTogDiv('SMALL_TOOL_BOX_<xsl:value-of select="$position"/>');hideTogDiv('SMALL_TOOLS_<xsl:value-of select="$position"/>');hideTogDiv('SMALL_TOOL_BOX_CLOSER_<xsl:value-of select="$position"/>');</xsl:attribute>
						<img>
							<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>smallToolBoxOpened.gif</xsl:attribute>
						</img>
					</div>
				</td>
				<td>
					<div style="display:none" class="rowActionButtonDiv">
						<xsl:attribute name="id">ROW_ACTION_<xsl:value-of select="$position"/></xsl:attribute>
								<xsl:for-each select="$resultSet/rowActionObjects/obj[@type='rowActionButton']">
									<xsl:variable name="processObj"><xsl:call-template name="checkObjPermissions"><xsl:with-param name="obj" select="." /></xsl:call-template></xsl:variable>
									<xsl:if test="$processObj = 'true'">
											<xsl:call-template name="objRowActionButton">
												<xsl:with-param name="obj" select="." />
												<xsl:with-param name="record" select="$record" />
												<xsl:with-param name="position" select="$position" />
																
											</xsl:call-template>				
									</xsl:if>
								</xsl:for-each>
					</div>
				</td>
			</tr>
		</table>
	</xsl:when>
<!--Counter Column -->			
	<xsl:when test="@class = 'counter'">
		<xsl:attribute name="style">text-align:right;color:silver;font-weight:300;</xsl:attribute>
		<xsl:value-of select="$record/field[@name = 'actualCount']/@value"/>
	</xsl:when>
	<xsl:otherwise>			
<div>
	<xsl:if test="@info">
    <xsl:attribute name="title">
      <xsl:call-template name="jScriptEscape">
        <xsl:with-param name="val">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">
              <xsl:value-of select="$infoText"/>
            </xsl:with-param>
          </xsl:call-template>
        </xsl:with-param>
      </xsl:call-template>
    </xsl:attribute>
	</xsl:if>
	<xsl:if test="@fav">
		<xsl:attribute name="onmouseover"></xsl:attribute>
	</xsl:if>
	<xsl:if test="@info">
		<div style="display:none" class="hiddenForWords">
           	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$infoText"/></xsl:with-param></xsl:call-template>
        </div>
	</xsl:if>
	
			<xsl:choose>
<!--Special Column-->				
			<xsl:when test="@special">
				<xsl:choose>
					<xsl:when test="@special = 'referencePic'">
						<xsl:call-template name="printReferencePic">
							<xsl:with-param name="record" select="$record" />
						</xsl:call-template>
					</xsl:when>
					<xsl:when test="@special = 'xmlReferencePic'">
						<xsl:call-template name="printXmlReferencePic">
							<xsl:with-param name="record" select="$record" />
						</xsl:call-template>
					</xsl:when>
					<xsl:when test="@special = 'approvalItem'">
						<xsl:call-template name="printApprovalItem">
							<xsl:with-param name="record" select="$record" />
						</xsl:call-template>
					</xsl:when>
					<xsl:when test="@special = 'insStep'">
						<xsl:copy-of select="$record/value/root"/>
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
									<xsl:attribute name="title"><xsl:call-template name="printOneItem">
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
									<xsl:attribute name="title"><xsl:value-of select="$record/denialReason/record/field[@name = 'DENIAL_REASON']/@value"/></xsl:attribute>
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
									<xsl:value-of select="@value"/>
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
				<a target="_blank">
					<xsl:attribute name="href"><xsl:value-of select="@href"/><xsl:if test="not(@noQString)">?</xsl:if>

						<xsl:if test="./queryString">
							<xsl:value-of select="./queryString"/>&amp;
						</xsl:if>
						<xsl:choose>
							<xsl:when test="putFieldValue">
								<xsl:for-each select="putFieldValue/item">
									<xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@field"/>
										<xsl:with-param name="record" select="$record"/>
									</xsl:call-template>
								</xsl:for-each>
							</xsl:when>
							<xsl:when test="./translateRecsToQuery">
								<xsl:for-each select="translateRecsToQuery/item">
									<xsl:value-of select="@show"/>=<xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@field"/>
										<xsl:with-param name="record" select="$record"/>
									</xsl:call-template>
									<xsl:value-of select="@value"/>&amp;
								</xsl:for-each>
							</xsl:when>
							<xsl:otherwise>
								<xsl:call-template name="putRecordAsQueryString">
									<xsl:with-param name="record" select="$record" />
								</xsl:call-template>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:for-each select="extraQueryStringData/item">
							<xsl:value-of select="@show"/>=<xsl:value-of select="@value"/>&amp;
						</xsl:for-each>

					</xsl:attribute>
					<xsl:attribute name="target"><xsl:value-of select="@target"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="@image">
							<img>
								<xsl:attribute name="src"><xsl:value-of select="@image"/></xsl:attribute>
								<xsl:attribute name="title"><xsl:value-of select="@alt"/></xsl:attribute>
							</img>
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="printOneItem">
								<xsl:with-param name="field" select="@field"/>
								<xsl:with-param name="record" select="$record"/>
							</xsl:call-template>
						</xsl:otherwise>
					</xsl:choose>
				</a>					
			</xsl:when>
<!--This column has a URL in the data-->
			<xsl:when test="@url != ''">
				<xsl:variable name="myF"><xsl:value-of select="@url"/></xsl:variable>
				<a style="text-decoration:none" >
					<xsl:attribute name="target"><xsl:value-of select="@target"/></xsl:attribute>
					<xsl:attribute name="href"><xsl:value-of select="@preURL"/><xsl:value-of select="$record/field[@name=$myF]/@value"/></xsl:attribute>
					<xsl:if test="$boolDebug = 'true'">
						<xsl:value-of select="$myF"/> pre = <xsl:value-of select="@preURL"/>
					</xsl:if>
					<xsl:choose>
						<xsl:when test="@image">
							<img>
								<xsl:attribute name="src"><xsl:value-of select="@image"/></xsl:attribute>
								<xsl:attribute name="title"><xsl:value-of select="@alt"/></xsl:attribute>
							</img>
						</xsl:when>
						<xsl:otherwise>
							<xsl:variable name="myF2"><xsl:value-of select="@field"/></xsl:variable>
							<xsl:choose>
								<xsl:when test="@usePutText">
									<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$record/field[@name=$myF2]/@value"/></xsl:with-param></xsl:call-template>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$record/field[@name=$myF2]/@value"/>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
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
<!--This Column is an input column.-->
			<xsl:when test="@inputType">
				<xsl:call-template name="printOneItemInput">
					<xsl:with-param name="col" select="."/>
					<xsl:with-param name="field" select="@field"/>
					<xsl:with-param name="record" select="$record"/>
					<xsl:with-param name="position" select="$position"/>
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
</div>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--##################################################
    ##  printReferencePic                           ##
	## this will put a hidden layer with the first  ##
	## picture of each item in it. There will be an icon
	## That when clicked it will show.
	################################################## -->
<xsl:template name="printReferencePic">
<xsl:param name="record"/>
<xsl:variable name="p" select="$record/picInfo"/>
<xsl:if test="contains($p/record/field[@name = 'SERVER_PATH']/@value,'JPG') or contains($p/record/field[@name = 'SERVER_PATH']/@value,'GIF')">
	<span>
		<xsl:attribute name="onMouseOver">javascript:addToShowList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
		<xsl:attribute name="onMouseOut">javascript:addToCloseList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
	<a>
		<!--<xsl:attribute name="href">javascript:toggleDivVisibility('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>-->
		<img height="30" width="30">
			<xsl:attribute name="class">refPic</xsl:attribute> 
			<xsl:attribute name="style">refPic</xsl:attribute> 
			<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$p/record/field[@name = 'LINKED_DOC_ID']/@value"/></xsl:attribute>
		</img>
	</a>
	
	<div class="hiddenReferencePicture" style="position:absolute;visibility:hidden;">
		<xsl:attribute name="id">ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/></xsl:attribute>
<!--		<xsl:attribute name="onMouseOver">javascript:addToShowList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
-->		<xsl:attribute name="onMouseOut">javascript:addToCloseList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
		<a class="closeX"><xsl:attribute name="href">javascript:toggleDivVisibility('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
			<div class="closeX"><span class="closeX">X</span></div>
			<img>
				<xsl:attribute name="class">refPic</xsl:attribute> 
				<xsl:attribute name="style">refPic</xsl:attribute> 
				<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$p/record/field[@name = 'LINKED_DOC_ID']/@value"/></xsl:attribute>
			</img>
		</a>
	</div>
	</span>
</xsl:if>

</xsl:template>

<!--##################################################
    ##  printXmlReferencePic                           ##
	## this will put a hidden layer with the first  ##
	## picture of each item in it. There will be an icon
	## That when clicked it will show.
	################################################## -->
<xsl:template name="printXmlReferencePic">
<xsl:param name="record"/>
<xsl:variable name="p" select="$record/xmlPicInfo"/>
<xsl:if test="contains($p/r/n,'JPG') or contains($p/r/n,'GIF')">
	<span>
		<xsl:attribute name="onMouseOver">javascript:addToShowList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
		<xsl:attribute name="onMouseOut">javascript:addToCloseList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
	<a>
		<!--<xsl:attribute name="href">javascript:toggleDivVisibility('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>-->
		<img height="30" width="30">
			<xsl:attribute name="class">refPic</xsl:attribute> 
			<xsl:attribute name="style">refPic</xsl:attribute> 
			<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$p/r/i"/></xsl:attribute>
		</img>
	</a>
	
	<div class="hiddenReferencePicture" style="position:absolute;visibility:hidden;">
		<xsl:attribute name="id">ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/></xsl:attribute>
<!--		<xsl:attribute name="onMouseOver">javascript:addToShowList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
-->		<xsl:attribute name="onMouseOut">javascript:addToCloseList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
		<a class="closeX"><xsl:attribute name="href">javascript:toggleDivVisibility('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
			<div class="closeX"><span class="closeX">X</span></div>
			<img>
				<xsl:attribute name="class">refPic</xsl:attribute> 
				<xsl:attribute name="style">refPic</xsl:attribute> 
				<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$p/r/i"/></xsl:attribute>
			</img>
		</a>
	</div>
	</span>
</xsl:if>
<xsl:if test="contains($p/record/field[@name='SERVER_PATH']/@value,'JPG') or contains($p/record/field[@name='SERVER_PATH']/@value,'GIF')">
	<xsl:variable name="ref"><xsl:value-of select="$p/record/field[@name='LINKED_DOC_ID']/@value"/></xsl:variable>
	<span>
		<xsl:attribute name="onMouseOver">javascript:addToShowList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
		<xsl:attribute name="onMouseOut">javascript:addToCloseList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
	<a>
		<!--<xsl:attribute name="href">javascript:toggleDivVisibility('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>-->
		<img height="45" width="45">
			<xsl:attribute name="class">refPic</xsl:attribute> 
			<xsl:attribute name="style">refPic</xsl:attribute> 
			<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$ref"/></xsl:attribute>
		</img>
	</a>
	
	<div class="hiddenReferencePicture" style="position:absolute;visibility:hidden;">
		<xsl:attribute name="id">ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/></xsl:attribute>
<!--		<xsl:attribute name="onMouseOver">javascript:addToShowList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
-->		<xsl:attribute name="onMouseOut">javascript:addToCloseList('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
		<a class="closeX"><xsl:attribute name="href">javascript:toggleDivVisibility('ReferencePicture_<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>');</xsl:attribute>
			<div class="closeX"><span class="closeX">X</span></div>
			<img>
				<xsl:attribute name="class">refPic</xsl:attribute> 
				<xsl:attribute name="style">refPic</xsl:attribute> 
				<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$ref"/></xsl:attribute>
			</img>
		</a>
	</div>
	</span>
</xsl:if>

</xsl:template>



<!--##################################################
    ## specialPutTextForColumn                      ##
	## input :
	specialPutText - 
	<specialPutText>
		<rule>
			<field>FieldName</field>
			<value>FieldValue</value>		
		</rule>
	</specialPutText>
	column - 
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
		</column>
	record - 
		<record>
			<field @name="name" @value="value"/>
		</record>
	################################################## -->
<xsl:template name="specialPutTextForColumn">
<xsl:param name="specialPutText"/>
<xsl:param name="column"/>
<xsl:param name="record"/>
<xsl:variable name="myField"><xsl:value-of select="$column/@field"/></xsl:variable>
<xsl:variable name="rulesResults"><xsl:call-template name="rulesCheck"><xsl:with-param name="record" select="$record" /><xsl:with-param name="rules" select="$specialPutText" /></xsl:call-template></xsl:variable>
<xsl:choose>
	<xsl:when test="string-length ($rulesResults) &gt; 0">
		<xsl:value-of select="$record/field[@name = $myField]/@value"/>
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$record/field[@name = $myField]/@value"/></xsl:with-param></xsl:call-template>
	</xsl:otherwise>
</xsl:choose>

</xsl:template>

<!--#######################################################
    ## rulesCheck                                        ##
	## if the rules pass then the return will be nothing.##
	## otherwise there will be something in it.          ##
	#######################################################-->
<xsl:template name="rulesCheck">
<xsl:param name="record"/>
<xsl:param name="rules"/>
<xsl:for-each select="$rules/rule">
<!--	field = <xsl:value-of select="./field"/>
	value = <xsl:value-of select="./value"/>-->
	<xsl:variable name="testField"><xsl:value-of select="./field"/></xsl:variable>
	<xsl:variable name="testVal"><xsl:value-of select="./value"/></xsl:variable>
	<xsl:choose><xsl:when test="$record/field[@name = $testField]/@value = $testVal"></xsl:when>
	<xsl:otherwise>NO</xsl:otherwise></xsl:choose>
</xsl:for-each>
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
						<xsl:attribute name="title"><xsl:value-of select="@alt"/></xsl:attribute>
						<xsl:attribute name="style"><xsl:value-of select="@style"/></xsl:attribute>
						<xsl:attribute name="class"><xsl:value-of select="@class"/></xsl:attribute>
					</img>
				</a>
			</xsl:when>
		</xsl:choose>
	</xsl:for-each>
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
					<xsl:with-param name="items" select="/Doc_Webpage/queryString/item[@name != 'sortBy' and @name != 'sortDescending']"/>
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
						<xsl:with-param name="items" select="/Doc_Webpage/queryString/item[@name != 'sortBy' and @name != 'sortDescending']"/>
					</xsl:call-template>
					sortBy=<xsl:value-of select="$sortBy"/>&amp;
					<xsl:if test="/Doc_Webpage/queryString/item[@name = 'sortBy']/@value = $sortBy and not(/Doc_Webpage/queryString/item[@name = 'sortDescending'])">
						sortDescending=True&amp;
					</xsl:if>
				</xsl:attribute>
				<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$name"/></xsl:with-param></xsl:call-template>
			</a>
		</td>
		<td class="tight" style="vertical-align: middle;">
			<xsl:if test="/Doc_Webpage/queryString/item[@name = 'sortBy']/@value = $sortBy and not(/Doc_Webpage/queryString/item[@name = 'sortDescending'])">
				<xsl:text> </xsl:text>
				<img border="0">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>ascending.gif</xsl:attribute>
				</img>
			</xsl:if>
			<xsl:if test="/Doc_Webpage/queryString/item[@name = 'sortBy']/@value = $sortBy and (/Doc_Webpage/queryString/item[@name = 'sortDescending'])">
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
<xsl:param name="position" />

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
		<xsl:if test="$boolDebug='true'">showOnly = "<xsl:value-of select="$showOnly"/>" showOnlyHere = "<xsl:value-of select="$showOnlyHere"/>"</xsl:if>
		<xsl:if test="$showOnly = $showOnlyHere">
			<xsl:call-template name="putRowActionButton">
				<xsl:with-param name="obj" select="." />
				<xsl:with-param name="record" select="$record" />
				<xsl:with-param name="position" select="$position" />
			</xsl:call-template>						
		</xsl:if>
<!--		<xsl:if test="$boolDebug = 'true'">
			<table class="tight" style="font-size: x-small" border="1">
				<tr><th>showonly</th><th>herelist</th></tr>
				<tr><td><xsl:value-of select="$showOnly"/></td><td><xsl:value-of select="$showOnlyHere"/></td></tr>
			</table>
		</xsl:if>-->
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="putRowActionButton">
			<xsl:with-param name="obj" select="." />
			<xsl:with-param name="record" select="$record" />										
			<xsl:with-param name="position" select="$position" />
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
<!--This is for straight up regular searches..  It checks to make sure there is
	a child node to the record node which has the same name as the showOnly that it is looking for-->
	<xsl:for-each select="$obj/attribute[@name = 'showOnly']">
		<xsl:variable name="myVal" select="@value"/>
		<xsl:if test="$record/node()[name() = $myVal]/record">
			<xsl:value-of select="@value"/>	
		</xsl:if>
	</xsl:for-each>
<!--For a tree it is different.  In a tree we need to go up to the ancestor.  MAke sure the ancestor is a tree
and then check to see if the tree node has a child with the same name as the showOnly-->
	<xsl:if test="$record/parent::tree">
		<xsl:for-each select="$obj/attribute[@name = 'showOnly']">
			<xsl:variable name="myVal" select="@value"/>
			<xsl:if test="$record/parent::tree/node()[name() = $myVal]/record">
				<xsl:value-of select="@value"/>	
			</xsl:if>
		</xsl:for-each>
	</xsl:if>
</xsl:template>
<!--##################################################
    ## putRowActionRealButton                       ##
	################################################## -->
<xsl:template name="putRowActionRealButton">
<xsl:param name="obj" />
<xsl:param name="record" />
<xsl:choose>
	<xsl:when test="$obj/showOnlyField">
		<xsl:variable name="showOnly"><xsl:call-template name="getFieldShowOnlyList">
				<xsl:with-param name="obj" select="$obj" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="showOnlyHere"><xsl:call-template name="getFieldShowOnlyHereList">
				<xsl:with-param name="obj" select="$obj" />
				<xsl:with-param name="record" select="$record" />
			</xsl:call-template>
		</xsl:variable>
		<!--showOnly = <xsl:value-of select="$showOnly"/>
		here = <xsl:value-of select="$showOnlyHere"/>-->
		<xsl:if test="$showOnly = $showOnlyHere">
			<xsl:call-template name="putRowActionRealButtonII">
				<xsl:with-param name="obj" select="." />
				<xsl:with-param name="record" select="$record" />										
			</xsl:call-template>						
		</xsl:if>
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="putRowActionRealButtonII">
			<xsl:with-param name="obj" select="." />
			<xsl:with-param name="record" select="$record" />										
		</xsl:call-template>							
	</xsl:otherwise>		
</xsl:choose>
</xsl:template>
<!--##################################################
    ## putRowActionButton                           ##
	################################################## -->
<xsl:template name="putRowActionButton">
<xsl:param name="obj" />
<xsl:param name="record" />
<xsl:param name="position" />
<xsl:choose>
	<xsl:when test="$obj/showOnlyField">
		<xsl:variable name="showOnly"><xsl:call-template name="getFieldShowOnlyList">
				<xsl:with-param name="obj" select="$obj" />
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="showOnlyHere"><xsl:call-template name="getFieldShowOnlyHereList">
				<xsl:with-param name="obj" select="$obj" />
				<xsl:with-param name="record" select="$record" />
			</xsl:call-template>
		</xsl:variable>
		<!--showOnly = <xsl:value-of select="$showOnly"/>
		here = <xsl:value-of select="$showOnlyHere"/>-->
		<xsl:if test="$showOnly = $showOnlyHere">
			<xsl:call-template name="putRowActionButtonII">
				<xsl:with-param name="obj" select="." />
				<xsl:with-param name="record" select="$record" />
				<xsl:with-param name="position" select="$position" />									
			</xsl:call-template>						
		</xsl:if>
	</xsl:when>
	<xsl:otherwise>
		<xsl:call-template name="putRowActionButtonII">
			<xsl:with-param name="obj" select="." />
			<xsl:with-param name="record" select="$record" />
			<xsl:with-param name="position" select="$position" />							
		</xsl:call-template>							
	</xsl:otherwise>		
</xsl:choose>
</xsl:template>
<!--##################################################
    ##    getFieldShowOnlyList                      ##
	################################################## -->
<xsl:template name="getFieldShowOnlyList">
<xsl:param name="obj"/>
	<xsl:for-each select="$obj/showOnlyField">
		<xsl:value-of select="@value"/>	
	</xsl:for-each>
</xsl:template>
<!--##################################################
    ##    getFieldShowOnlyHereList                      ##
	################################################## -->
<xsl:template name="getFieldShowOnlyHereList">
<xsl:param name="obj"/>
<xsl:param name="record"/>
	<xsl:for-each select="$obj/showOnlyField">
		<xsl:variable name="fn"><xsl:value-of select="@field"/></xsl:variable>
		<xsl:value-of select="$record/field[@name = $fn]/@value"/>	
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## putRowActionButtonII                         ##
	################################################## -->
<xsl:template name="putRowActionButtonII">
<xsl:param name="obj" />
<xsl:param name="record" />
<xsl:param name="position" />

<span class="rowActButton">
	<xsl:if test="$record/field[@name = 'ID']/@value"><xsl:attribute name="id">ROW_ACT_BUTTON_<xsl:value-of select="$record/field[@name = 'ID']/@value"/></xsl:attribute></xsl:if>
	<a class="rowActButton" >
		<xsl:choose>
			<xsl:when test="$obj/attribute[@name='special']/@value = 'showWFData'">
				<xsl:attribute name="href">javascript:showWFInfo('<xsl:value-of select="$record/field[@name='OBJ_ID']/@value"/>');</xsl:attribute>
			</xsl:when>
			<xsl:when test="$obj/attribute[@name='special']/@value = 'sendTo'">
				<xsl:attribute name="href">javascript:
					<xsl:if test="$obj/attribute[@name = 'showField']/@value">
						<xsl:call-template name="AddToOptionBox">
								<xsl:with-param name="rForm" select="/Doc_Webpage/queryString/item[@name = 'RECEIVER_FORM']/@value" />
								<xsl:with-param name="rField" select="/Doc_Webpage/queryString/item[@name = 'RECEIVER_FIELD']/@value" />
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
								<xsl:with-param name="multiple" select="/Doc_Webpage/queryString/item[@name = 'multiple']/@value" />
						</xsl:call-template>
						;
					</xsl:if>
						<xsl:if test="/Doc_Webpage/queryString/item[@name = 'doWhenAdding']/@value != ''">
							opener.<xsl:value-of select="/Doc_Webpage/queryString/item[@name = 'doWhenAdding']/@value"/>;
						</xsl:if>
						<xsl:for-each select="$obj/extraSendToFields/item">;
							<xsl:call-template name="AddToOptionBox">
								<xsl:with-param name="rForm" select="/Doc_Webpage/queryString/item[@name = 'RECEIVER_FORM']/@value" />
								<xsl:with-param name="rField" select="@receiver" />
								<xsl:with-param name="value"><xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@valField" />
										<xsl:with-param name="record" select="$record" />
									</xsl:call-template>
								</xsl:with-param>
								<xsl:with-param name="show"><xsl:call-template name="printOneItem">
										<xsl:with-param name="field" select="@showField" />
										<xsl:with-param name="record" select="$record" />
										<xsl:with-param name="usePutText" select="@usePutText" />
									</xsl:call-template>
								</xsl:with-param>							
								<xsl:with-param name="multiple" select="/Doc_Webpage/queryString/item[@name = 'multiple']/@value" />
							</xsl:call-template>
						</xsl:for-each>
						<xsl:for-each select="$obj/sendText">
							<xsl:variable name="fn"><xsl:value-of select="field"/></xsl:variable>
							window.opener.<xsl:value-of select="/Doc_Webpage/queryString/item[@name = 'RECEIVER_FORM']/@value"/>.<xsl:value-of select="./destination"/>.value='<xsl:value-of select="$record/field[@name=$fn]/@value"/>';
							window.close();
						</xsl:for-each>
				</xsl:attribute>
			</xsl:when>
			<xsl:when test="$obj/attribute[@name='special']/@value = 'addToFavorites'">
				<xsl:choose>
					<xsl:when test="$record/field[@name = 'ROOT']/@value != ''">
						<xsl:attribute name="href">javascript:addToFavorites('<xsl:value-of select="$record/field[@name = 'ROOT']/@value"/>','<xsl:value-of select="$obj/favoriteType/@value"/>')</xsl:attribute>
					</xsl:when>
					<xsl:when test="$record/field[@name = 'OBJ_ID']/@value != ''">
						<xsl:attribute name="href">javascript:addToFavorites('<xsl:value-of select="$record/field[@name = 'OBJ_ID']/@value"/>','<xsl:value-of select="$obj/favoriteType/@value"/>')</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="href">javascript:addToFavorites('<xsl:value-of select="$record/field[@name = 'ID']/@value"/>','<xsl:value-of select="$obj/favoriteType/@value"/>')</xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$obj/jScriptButton">
				<xsl:attribute name="href">javascript:<xsl:for-each select="jScriptButton/jScriptAction">
						<xsl:choose>
							<xsl:when test="@type='text'"><xsl:value-of select="."/></xsl:when>
							<xsl:when test="@type='field'"><xsl:variable name="myField"><xsl:value-of select="."/></xsl:variable><xsl:value-of select="$record/field[@name = $myField]/@value"/></xsl:when>
						</xsl:choose>
				</xsl:for-each></xsl:attribute>
			</xsl:when>
			<xsl:when test="$obj/attribute[@name='ajax']/@value">
				<xsl:attribute name="href">
				<xsl:if test="$obj/confirm">javascript:if (ConfirmButton('<xsl:call-template name="jScriptEscape">
							<xsl:with-param name="val"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/confirm/@msg"/></xsl:with-param></xsl:call-template></xsl:with-param>
						</xsl:call-template>'))
					{
					var myPost = new String('<xsl:choose>
						<xsl:when test="$obj/translationInformation">
							<xsl:for-each select="$obj/translationInformation/field">
								<xsl:value-of select="@show"/>=
								<xsl:call-template name="putOneRecordField">
									<xsl:with-param name="record" select="$record" />
									<xsl:with-param name="field" select="@name" />
								</xsl:call-template>
								<xsl:value-of select="@value"/>
								<!--<xsl:if test="position() &lt; last()">&amp;</xsl:if>-->
							</xsl:for-each>
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="putRowActionItemsQstring">
								<xsl:with-param name="obj" select="." />
								<xsl:with-param name="record" select="$record" />										
							</xsl:call-template>						
						</xsl:otherwise>
					</xsl:choose>');
					ajaxSaveToFile('<xsl:value-of select="$obj/attribute[@name='ajax']/@value"/>','USING_AJAX=true&amp;' + myPost.substr(1));
					if(hideTogDiv('ROW_ACTION_<xsl:value-of select="$position"/>')){};
					}
				</xsl:if>
				</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="href">
					<xsl:if test="$obj/confirm">javascript:if (ConfirmButton('<xsl:call-template name="jScriptEscape">
							<xsl:with-param name="val"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/confirm/@msg"/></xsl:with-param></xsl:call-template></xsl:with-param>
						</xsl:call-template>'))
					{document.location = '
					</xsl:if>
					<xsl:value-of select="$obj/attribute[@name='url']/@value"/>
					<xsl:choose>
						<xsl:when test="$obj/translationInformation">
							<xsl:for-each select="$obj/translationInformation/field">
								<xsl:value-of select="@show"/>=
								<xsl:call-template name="putOneRecordField">
									<xsl:with-param name="record" select="$record" />
									<xsl:with-param name="field" select="@name" />
								</xsl:call-template>
								<xsl:value-of select="@value"/>
								<xsl:if test="position() &lt; last()">&amp;</xsl:if>
							</xsl:for-each>
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="putRowActionItemsQstring">
								<xsl:with-param name="obj" select="." />
								<xsl:with-param name="record" select="$record" />										
							</xsl:call-template>						
						</xsl:otherwise>
					</xsl:choose>
					<xsl:if test="$obj/attribute[@name = 'redirectTo']">&amp;redirectTo=<xsl:value-of select="$obj/redirectTo/@val"/></xsl:if>
					<xsl:if test="$obj/attribute[@name = 'queryString']">&amp;<xsl:value-of select="$obj/attribute[@name = 'queryString']/@value"/></xsl:if>
					<xsl:if test="$obj/confirm">'}</xsl:if>
				</xsl:attribute>
				<xsl:if test="$obj/attribute[@name = 'target']">
					<xsl:attribute name="target"><xsl:value-of select="$obj/attribute[@name = 'target']/@value"/></xsl:attribute>
				</xsl:if>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:choose>
		 	<xsl:when test="$obj/attribute[@name='image']">
				<img border="0" class="rowAction"  height="10px" width="10px">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$obj/attribute[@name='image']/@value"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'TTAlt']">
							<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Locked By: </xsl:with-param></xsl:call-template><xsl:value-of select="$record/field[@name = 'LOCKED_BY_NAME']/@value"/> --</xsl:attribute>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
						</xsl:otherwise>
					</xsl:choose>
					<div style="display:none" class="hiddenForWords">
						<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
	                	<xsl:call-template name="putText"><xsl:with-param name="key">Locked By:</xsl:with-param></xsl:call-template>
	                </div>
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
	<div class="hiddenForWords" style="display:none">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/confirm/@msg"/></xsl:with-param></xsl:call-template>
	</div>
</span>
</xsl:template>

<!--##################################################
    ## putRowActionRealButtonII                     ##
	################################################## -->
<xsl:template name="putRowActionRealButtonII">
<xsl:param name="obj" />
<xsl:param name="record" />
<xsl:choose>
	<xsl:when test="$obj/attribute[@name='specialShowOnly']/@value = 'quickReview'">
	<form>
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'specialShowOnly']/@value"/>_<xsl:value-of select="$record/field[@name='ID']/@value"/></xsl:attribute>
		<input type="button" name="quickRev">
			<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:attribute>
			<xsl:attribute name="onClick">
				<xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/>;
				window.open('<xsl:value-of select="$path_to_top"/>asp/standardPage.asp?pageID=quickReview&amp;ID=<xsl:value-of select="$record/field[@name='ID']/@value"/>',
				'quickReviewWindow','width=1,height=1,location=yes,toolbar=yes,resizable=no,scrollbars=no');
				document.<xsl:value-of select="$obj/attribute[@name = 'specialShowOnly']/@value"/>_<xsl:value-of select="$record/field[@name='ID']/@value"/>.quickRev.disabled = true;
			</xsl:attribute>
		</input>
	</form>
	</xsl:when>
</xsl:choose>
<div style="display:none" class="hiddenForWords">
	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
</div>
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
				&amp;<xsl:value-of select="@alias"/>=<xsl:value-of select="@value"/><xsl:call-template name="printOneItem"><xsl:with-param name="field" select="@name" /><xsl:with-param name="record" select="$record" /></xsl:call-template>
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
<xsl:variable name="myValue"><xsl:choose>
			<xsl:when test="@allVal"><xsl:value-of select="@allVal"/></xsl:when>
			<xsl:otherwise><xsl:if test="string-length($record/field[@name = $field]/@value) &gt; 0"><xsl:value-of select="@preText"/><xsl:value-of select="$record/field[@name = $field]/@value"/><xsl:value-of select="@postText"/></xsl:if></xsl:otherwise>
			</xsl:choose></xsl:variable>
	<xsl:choose>
		<xsl:when test="$usePutText">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$myValue"/></xsl:with-param></xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="$record/field[@name = $field]/@type = '135'">
					<xsl:choose>
						<xsl:when test="dateFormat">
							<xsl:call-template name="printADate">
								<xsl:with-param name="dateFormat" select="dateFormat"/>
								<xsl:with-param name="fld" select="$record/field[@name = $field]"/>
							</xsl:call-template>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$record/field[@name = $field]/@answerDate"/>					
						</xsl:otherwise>
					</xsl:choose>					
				</xsl:when>			
				<xsl:when test="$record/field[@name = $field]/@type = '6'">
					<xsl:choose>
						<xsl:when test="currencyFormat">
							<xsl:call-template name="printCurrency">
								<xsl:with-param name="curFormat" select="currencyFormat"/>
								<xsl:with-param name="fld" select="$record/field[@name = $field]"/>
							</xsl:call-template>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$record/field[@name = $field]/@value"/>					
						</xsl:otherwise>
					</xsl:choose>					
				</xsl:when>			
				<xsl:otherwise>
					<xsl:value-of select="$myValue"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ##  printADate                                  ##
	################################################## -->
<xsl:template name="printADate">
<xsl:param name="dateFormat"/>
<xsl:param name="fld"/>
	<xsl:for-each select="$dateFormat/datePart">
		<xsl:choose>
			<xsl:when test="@val = 'YEAR'">
				<xsl:value-of select="$fld/@year"/>
			</xsl:when>
			<xsl:when test="@val = 'GENERAL_DATE'">
				<xsl:value-of select="$fld/@generalDate"/>
			</xsl:when>
			<xsl:when test="@val = 'LONG_DATE'">
				<xsl:value-of select="$fld/@longDate"/>
			</xsl:when>
			<xsl:when test="@val = 'SHORT_DATE'">
				<xsl:value-of select="$fld/@shortDate"/>
			</xsl:when>
			<xsl:when test="@val = 'LONG_TIME'">
				<xsl:value-of select="$fld/year/@value"/>
			</xsl:when>
			<xsl:when test="@val = 'SHORT_TIME'">
				<xsl:value-of select="$fld/@shortTime"/>
			</xsl:when>
			<xsl:when test="@val = 'ANSWER_DATE'">
				<xsl:value-of select="$fld/@answerDate"/>
			</xsl:when>
			<xsl:when test="@val = 'MONTH'">
				<xsl:value-of select="$fld/@month"/>
			</xsl:when>
			<xsl:when test="@val = 'DAY'">
				<xsl:value-of select="$fld/@day"/>
			</xsl:when>
			<xsl:when test="@val = 'HOUR'">
				<xsl:value-of select="$fld/@hour"/>
			</xsl:when>
			<xsl:when test="@val = 'MINUTES'">
				<xsl:value-of select="$fld/@minute"/>
			</xsl:when>
			<xsl:when test="@val = 'SECONDS'">
				<xsl:value-of select="$fld/@seconds"/>
			</xsl:when>
			<xsl:when test="@val = 'STD_HOUR'">
				<xsl:value-of select="$fld/@stdHour"/>
			</xsl:when>
			<xsl:when test="@val = 'SUFFIX'">
				<xsl:value-of select="$fld/@suffix"/>
			</xsl:when>
			<xsl:when test="@val = 'SMALL_YEAR'">
				<xsl:value-of select="$fld/@year2"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="@val"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  printCurrency                               ##
	################################################## -->
<xsl:template name="printCurrency">
<xsl:param name="curFormat"/>
<xsl:param name="fld"/>
	<xsl:for-each select="$curFormat/item">
		<xsl:choose>
			<xsl:when test="@val = 'penniless'">
				<xsl:value-of select="$fld/@penniless"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="@val"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:for-each>
</xsl:template>

<!--##################################################
    ## objForm                                      ##
	################################################## -->
<xsl:template name="objForm">
<xsl:param name="obj"/>
	<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
  <div>
		<xsl:if test="$obj/attribute[@name = 'class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
		</xsl:if>
	<table class="form">
		<xsl:if test="$obj/attribute[@name = 'class']">
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
		</xsl:if>
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
			<script language="JavaScript" type="text/javascript">
				arrForms = arrForms.concat(new Array("<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>"));
//				arrForms.push("<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>");
			</script>
		<xsl:if test="$boolDebug = 'true'">Form Name=<xsl:value-of select="$obj/attribute[@name='name']/@value"/> Action=<xsl:value-of select="$obj/attribute[@name='action']/@value"/> Method=<xsl:value-of select="$obj/attribute[@name='method']/@value"/></xsl:if>
		<form method="get">
			<!--encType-->
			<xsl:if test="$obj/attribute[@name = 'encType']">
				<xsl:attribute name="encType"><xsl:value-of select="$obj/attribute[@name = 'encType']/@value"/></xsl:attribute>
			</xsl:if>
			<!--onSubmit-->
			<xsl:attribute name="onSubmit"><xsl:value-of select="$obj/attribute[@name = 'onSubmit']/@value"/>;</xsl:attribute>
			<!--name-->
			<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
			<!--target-->
			<xsl:attribute name="target"><xsl:value-of select="$obj/attribute[@name = 'target']/@value"/></xsl:attribute>
			<!--method-->
			<xsl:attribute name="method"><xsl:value-of select="$obj/attribute[@name = 'method']/@value"/></xsl:attribute>
			<!--action-->
			<xsl:attribute name="action"><xsl:value-of select="$obj/attribute[@name = 'action']/@value"/></xsl:attribute>
<!--			<input type="hidden" name="SYSTEM_STRING_TO_LONG_MESSAGE1">
				<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">STRING_2_LONG_U_HAVE</xsl:with-param></xsl:call-template></xsl:attribute>
			</input>
			<input type="hidden" name="SYSTEM_STRING_TO_LONG_MESSAGE2">
				<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">CHARACTERS_AND_U_R_ALLOWED</xsl:with-param></xsl:call-template></xsl:attribute>
			</input>
-->
			<xsl:apply-templates />
		</form>
	</table>
  </div>
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
	<xsl:if test="$obj/attribute[@name = 'id']">
		<xsl:attribute name="ID"><xsl:value-of select="$obj/attribute[@name='id']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'style']">
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'onMouseOver']">
		<xsl:attribute name="onMouseOver"><xsl:value-of select="$obj/attribute[@name='onMouseOver']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'noBreak'] or $obj/attribute[@name = 'class']/@value = 'label'">
			<nobr>
				<xsl:choose>
					<xsl:when test="not($obj/attribute[@name = 'dontUsePutText']) and not($obj/@dontUsePutText)">
						<xsl:call-template name="putText"><xsl:with-param name="nobr">true</xsl:with-param><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>		
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$obj/attribute[@name='value']/@value"/>
					</xsl:otherwise>
				</xsl:choose>
			</nobr>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="not($obj/attribute[@name = 'dontUsePutText'])">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>		
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$obj/attribute[@name='value']/@value"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</div>
</xsl:template>

<!--##################################################
    ## objSpan                                      ##
	################################################## -->
<xsl:template name="objSpan">
<xsl:param name="obj" />
<xsl:text> </xsl:text>
<span class="norm">
	<xsl:if test="$obj/attribute[@name = 'class']">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name='class']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'id']">
		<xsl:attribute name="id"><xsl:value-of select="$obj/attribute[@name='id']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'style']">
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'noBreak'] or $obj/attribute[@name = 'class']/@value = 'label'">
			<nobr>
				<xsl:choose>
					<xsl:when test="not($obj/attribute[@name = 'dontUsePutText'])">
						<xsl:call-template name="putText"><xsl:with-param name="nobr">true</xsl:with-param><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>		
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$obj/attribute[@name='value']/@value"/>
					</xsl:otherwise>
				</xsl:choose>
			</nobr>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="not($obj/attribute[@name = 'dontUsePutText'])">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>		
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$obj/attribute[@name='value']/@value"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
<xsl:text> </xsl:text>
</span>

</xsl:template>


<!--##################################################
    ## objHiddenTextBox                             ##
	################################################## -->
<xsl:template name="objHiddenTextBox">
<xsl:param name="obj" />
<span class="tight">
	<xsl:attribute name="ID">document_<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>	</xsl:attribute>
<input type="hidden">
	<!--name-->
	<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>	
	<xsl:choose>
		<xsl:when test="$obj/attribute[@name = 'usePutText']">
			<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>			
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
</input>
<div style="display:none" class="hiddenForWords">
	<xsl:if test="$obj/attribute[@name = 'usePutText']">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
	</xsl:if>
</div>
</span>
</xsl:template>

<!--##################################################
    ## objTextBox                                   ##
	################################################## -->
<xsl:template name="objTextBox">
<xsl:param name="obj" />
<table class="tight">
	<tr>
		<td class="tight">
			<span class="tight">
				<xsl:attribute name="ID">document_<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>	</xsl:attribute>
				<input type="text" class="norm" onfocus="select()">
					<xsl:if test="$obj/attribute[@name='noEnter']">
						<xsl:attribute name="onKeyPress">return noenter()</xsl:attribute>
					</xsl:if>
					
					<xsl:attribute name="onfocus">
						<xsl:if test="$obj/attribute[@name='onFocus']">
							<xsl:value-of select="$obj/attribute[@name = 'onFocus']/@value"/>;
						</xsl:if>
						select();
					</xsl:attribute>
					<xsl:attribute name="onBlur">
						<xsl:if test="$obj/attribute[@name='onBlur']">
							<xsl:value-of select="$obj/attribute[@name = 'onBlur']/@value"/>;
						</xsl:if>
					</xsl:attribute>
					<xsl:if test="$obj/attribute[@name='maxLength']">
						<xsl:attribute name="MAXLENGTH"><xsl:value-of select="$obj/attribute[@name = 'maxLength']/@value"/></xsl:attribute>
					</xsl:if>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'onChange']">
							<xsl:attribute name="onChange"><xsl:if test="not($obj/attribute[@name='dontDisable'])">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");</xsl:if><xsl:value-of select="$obj/attribute[@name = 'onChange']/@value"/>;</xsl:attribute>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="onChange"><xsl:if test="not($obj/attribute[@name='dontDisable'])">disableOtherForms("<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>");</xsl:if></xsl:attribute>		
						</xsl:otherwise>	
					</xsl:choose>
					<xsl:if test="$obj/attribute[@name = 'class']">
						<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
					</xsl:if>
					<xsl:choose>
						<xsl:when test="$obj/attribute[@name = 'notUpdateable']/@value  or $obj/ancestor::obj[@type='form']/readOnly">
							<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/>;background-color: #EEEEEE;</xsl:attribute>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
						</xsl:otherwise>		
					</xsl:choose>
					<xsl:if test="$obj/attribute[@name = 'notUpdateable'  or $obj/ancestor::obj[@type='form']/readOnly]">
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
				<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
			</span>
		</td>
		<td  class="tight">
			<xsl:if test="$obj/attribute[@name = 'exact']">
				<xsl:variable name="temp_name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_MATCH_EXACTLY</xsl:variable>
				<xsl:variable name="exactMatchValue"><xsl:value-of select="/Doc_Webpage/queryString/item[@name = $temp_name]/@value" /></xsl:variable>
				<nobr>
					<input type="Checkbox" value="TRUE" tabindex="-1" style="width:1.1em;">
						<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_MATCH_EXACTLY</xsl:attribute>
						<xsl:if test="$exactMatchValue = 'TRUE'"><xsl:attribute name="checked">checked</xsl:attribute></xsl:if>
					</input>
					<xsl:call-template name="infoBox">
			        	<xsl:with-param name="infoText">match exactly</xsl:with-param>
			        </xsl:call-template>
				<div style="display:none" class="hiddenForWords">
					<xsl:call-template name="putText"><xsl:with-param name="key">match exactly</xsl:with-param></xsl:call-template>
				</div>
			
				</nobr>
			</xsl:if>
		</td>
	</tr>
</table>
<div style="display:none" class="hiddenForWords">
	<xsl:choose>
		<xsl:when test="($obj/attribute[@name='value']/@value = '') or not($obj/attribute[@name='value'])">
			<!--We are using the default value so go ahead and just get the info from the attributes-->
			<xsl:choose>
				<xsl:when test="$obj/attribute[@name = 'dontUsePutText']"></xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='defaultValue']/@value"/></xsl:with-param></xsl:call-template>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<!-- We should have a value so use it-->
				<xsl:when test="$obj/attribute[@name = 'usePutText']">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</div>
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
		objFocusOn = document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>;
	</script>
</xsl:if>
</xsl:template>

<!--##################################################
    ## objSubmitButton                              ##
	################################################## -->
<xsl:template name="objSubmitButton">
<xsl:param name="obj"/>
	<xsl:if test="not($obj/ancestor::obj[@type='form']/readOnly) or $obj/attribute[@name = 'showButton']">
		<input type="submit" name="submitButton">
			<xsl:attribute name="style">
				<xsl:variable name="val"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:variable>
<!--				<xsl:variable name="width"><xsl:value-of select="(string-length($val))"/></xsl:variable>
				width:<xsl:value-of select="((floor(string-length($val) div 7) + 1) * 40)"/>px;clear: both;			-->
				<xsl:value-of select="$obj/attribute[@name = 'style']/@value"/>
			</xsl:attribute>			
			<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>			
			<xsl:attribute name="value"><xsl:choose>
				<xsl:when test="$obj/attribute[@name='dontUsePutText']"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:when>
				<xsl:otherwise><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template></xsl:otherwise>
				</xsl:choose></xsl:attribute>
			<xsl:if test="$obj/attribute[@name='name']/@value">
					<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>			
			</xsl:if>
			<xsl:if test="$obj/attribute[@name='disabled']/@value">
					<xsl:attribute name="disabled"><xsl:value-of select="$obj/attribute[@name = 'disabled']/@value"/></xsl:attribute>			
			</xsl:if>
			<xsl:attribute name="onClick">
				<xsl:if test="not($obj/attribute[@name='doNotDisable']/@value) and not($obj/attribute[@name='doNotSelectAll']/@value)">
					this.disabled=true;escapeAllSelects();selectAllSelects();
				</xsl:if>
        <xsl:if test="not($obj/attribute[@name='doNotDisable']/@value) and ($obj/attribute[@name='doNotSelectAll']/@value)">
          this.disabled=true;escapeAllSelects();
        </xsl:if>

        <xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/>;
				<xsl:if test="not($obj/attribute[@name='doNotDisable']/@value)">
					this.form.submit();
				</xsl:if>
			</xsl:attribute>		
			<xsl:attribute name="onMouseOver">
				<xsl:value-of select="$obj/attribute[@name = 'onMouseOver']/@value"/>;
			</xsl:attribute>		
		</input>
	<xsl:if test="not($obj/attribute[@name='dontUsePutText'])">
		<div style="display:none" class="hiddenForWords">
			<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name='value']/@value"/></xsl:with-param></xsl:call-template>
		</div>
	</xsl:if>
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
		<attribute name="copyPrev" value="nameOfDropDownToCopy" />
		<attribute name="doWhenAdding" value="javascript function to do when adding something to the box"/>
		<attribute name="popOnLoad" value="true"/>
		<extraQueryStringData>
			<item name="whatever" value="whatever" />
		</extraQueryStringData>
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
			<td class="tight" rowspan="2" style="padding-top : 1px;padding-bottom:1px;">
				<span >
					<xsl:attribute name="ID">document_<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>	</xsl:attribute>
					<select class="norm">
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'onChange']">
								<xsl:attribute name="onChange"><xsl:value-of select="$obj/attribute[@name='onChange']/@value"/>;disableOtherForms('<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>');</xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="onChange">disableOtherForms('<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>');</xsl:attribute>							
							</xsl:otherwise>
						</xsl:choose>
						<!--Class-->
						<xsl:if test="$obj/attribute[@name = 'class']">
							<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
						</xsl:if>
						<!--Style-->
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'notUpdateable']/@value or $obj/ancestor::obj[@type='form']/readOnly">
								<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/>;background-color: #EEEEEE;</xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name='style']/@value"/></xsl:attribute>
							</xsl:otherwise>		
						</xsl:choose>
						<!--size-->
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name='size']/@value">
								<xsl:attribute name="size"><xsl:value-of select="$obj/attribute[@name='size']/@value"/></xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="count($obj/data/record) &gt; 2">
										<xsl:attribute name="size">3</xsl:attribute>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="size">count($obj/data/record)</xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
						<!--multiple-->
						<xsl:if test="$obj/attribute[@name='multiple']">
							<xsl:attribute name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:attribute>
						</xsl:if>
						<!--name-->
						<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:attribute>
						<!--now check to see if we use the default value-->
						<xsl:choose>
<!--						<xsl:when test="$obj/data/useDefault or not($obj/data/record)">-->
							<xsl:when test="$obj/data/useDefault">
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
							<xsl:when test="not($obj/attribute[@name='multiple']) and not($obj/data/record)">
								<xsl:variable name="myLookup"><xsl:value-of select="//pageName/@val"/>_<xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:variable>
								<xsl:choose>
									<xsl:when test="/Doc_Webpage/ddListPopulator/record[field[@name='POP_UP_KEY']/@value=$myLookup]">
										<option value=""></option>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="multiple">multiple</xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:for-each select="/Doc_Webpage/ddListPopulator/record[field[@name='POP_UP_KEY']/@value=$myLookup]">
									<option><xsl:attribute name="value"><xsl:value-of select="field[@name='VAL']/@value"/></xsl:attribute><xsl:value-of select="field[@name='SHOW']/@value"/></option>
								</xsl:for-each>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="multiple">multiple</xsl:attribute>
								<xsl:for-each select="$obj/data/record">
									<option>
										<xsl:attribute name="value"><xsl:value-of select="field[@name=$obj/valField/@value]/@value"/></xsl:attribute>
										<xsl:choose>
											<xsl:when test="$obj/attribute[@name = 'actualPutText']">
												<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="field[@name=$obj/showField/@value]/@value"/></xsl:with-param></xsl:call-template>
											</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="field[@name=$obj/showField/@value]/@value"/>
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
					<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
				</span>
			</td>
			<xsl:if test="not($obj/attribute[@name = 'notUpdateable'] or $obj/ancestor::obj[@type='form']/readOnly) and $obj/attribute[@name='popUpURL']">
				<td class="tight">
					<div class="tight">
						<xsl:attribute name="id"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="$obj/attribute[@name='name']/@value"/>_Add</xsl:attribute>
						<xsl:call-template name="putLinkButton">
							<xsl:with-param name="obj" select="$obj" />
							<xsl:with-param name="doWhenAdding"><xsl:value-of select="$obj/attribute[@name='doWhenAdding']/@value"/></xsl:with-param>
							<xsl:with-param name="destination"><xsl:value-of select="$obj/attribute[@name='popUpURL']/@value"/></xsl:with-param>
							<xsl:with-param name="fieldName"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
							<xsl:with-param name="formName"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
							<xsl:with-param name="id"><xsl:value-of select="$obj/attribute[@name='popUpID']/@value"/></xsl:with-param>
							<xsl:with-param name="multiple"><xsl:value-of select="$obj/attribute[@name='multiple']/@value"/></xsl:with-param>
							<xsl:with-param name="recsPerPage"><xsl:value-of select="$obj/attribute[@name='recsPerPage']/@value"/></xsl:with-param>
							<xsl:with-param name="extraQueryData"><xsl:for-each select="$obj/extraQueryStringData/item">
									<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/><xsl:if test="position()!=last()">&amp;</xsl:if>
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
							<xsl:with-param name="popOnLoad"><xsl:value-of select="$obj/attribute[@name='popOnLoad']/@value"/></xsl:with-param>
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
			<td class="tight">
				<!--<viewPage pageID="showDiscussion" varName="ID"/>-->
				<xsl:call-template name="putViewerButton">
					<xsl:with-param name="obj" select="$obj"/>
					<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>
					<xsl:with-param name="boxName"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
				</xsl:call-template>
			</td>
			<xsl:if test="$obj/attribute[@name='copyPrev']">
				<xsl:variable name="me"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:variable>
				<xsl:variable name="prev"><xsl:value-of select="$obj/attribute[@name = 'copyPrev']/@value"/></xsl:variable>
				<td class="tight">
					<img>
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>copy.gif</xsl:attribute>
						<xsl:attribute name="onClick">selectAllOptions(<xsl:value-of select="$prev"/>);copySelectedOptions(<xsl:value-of select="$prev"/>,<xsl:value-of select="$me"/>)</xsl:attribute>
					</img>
				</td>
			</xsl:if>
		</tr>
		<tr>
			<xsl:if test="(not($obj/attribute[@name = 'notUpdateable'])  or $obj/ancestor::obj[@type='form']/readOnly) and ($obj/attribute[@name = 'orderButtons'])">
				<xsl:call-template name="putOrderButtons">
						<xsl:with-param name="field"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:with-param>
						<xsl:with-param name="form"><xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/></xsl:with-param>					
				</xsl:call-template>
			</xsl:if>			
		</tr>
	</table>		
	<script language="javascript">
		pushSelectBox('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
		//arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
	</script>	
</div>
</xsl:template>

<!--##################################################
    ## putViewerButton                              ##
	################################################## -->
<xsl:template name="putViewerButton">
	<xsl:param name="obj"/>
	<xsl:param name="form"/>
	<xsl:param name="boxName"/>
<xsl:if test="$obj/viewPage">
	<xsl:variable name="vp" select="$obj/viewPage" />
	<a class="prodButton">
		<xsl:choose>
			<xsl:when test="$vp/@newWindow='false'">
				<xsl:attribute name="href">javascript:if(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$boxName"/>.value != ''){document.location='<xsl:value-of select="$vp/url"/>&amp;<xsl:value-of select="$vp/qsName"/>='+
				document.<xsl:value-of select="$form"/>.<xsl:value-of select="$boxName"/>.value}else{alert('<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>')}</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="href">javascript:popUpNavTo('<xsl:value-of select="$obj/viewPage/url"/>',
				document.<xsl:value-of select="$form"/>.<xsl:value-of select="$boxName"/>,'<xsl:value-of select="$vp/qsName"/>','<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>')</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
		<img border="0" class="smallIcon">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>view.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">view item</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">view item</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>
    </div>
</xsl:if>
<xsl:if test="contains($obj/attribute[@name = 'popUpURL']/@value,'roles/selectRoles.asp') 
				or contains($obj/attribute[@name = 'popUpURL']/@value,'people/peopleSelect.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'objects/selectApprovedObjects.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'people/peopleInYourRootCompanySelect.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'companies/companiesSelect.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'actualparts/selectActualParts.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'forecasts/select.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'locations/selectLocations.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'parts/selectPart.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'procedures/selectProcedure.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'procedures/selectProceduresForProcedureStep.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'products/selectProducts.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'products/selectPurchaseProduct.asp')
				or contains($obj/attribute[@name = 'popUpURL']/@value,'forecasts/select.asp')
				">
	<a class="prodButton">
		<xsl:attribute name="href">javascript:if(getSelectedValue(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$boxName"/>) != 'NOTHING' &amp;&amp; getSelectedValue(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$boxName"/>) != ''){document.location='<xsl:value-of select="$path_to_top"/>asp/objects/viewApprovedObject.asp?ID='+
			document.<xsl:value-of select="$form"/>.<xsl:value-of select="$boxName"/>.value}else{alert('<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>')}</xsl:attribute>
		<img border="0" class="smallIcon">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>view.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">view item</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">view item</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">PleaseChooseSomethingToView</xsl:with-param></xsl:call-template>
    </div>
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
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">move selected options up</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">move selected options up</xsl:with-param></xsl:call-template>
    </div>
</td>
<td class="tight">
	<a class="prodButton">
		<xsl:attribute name="href">javascript:disableOtherForms("<xsl:value-of select="$form"/>");moveOptionDown(document.<xsl:value-of select="$form"/>.<xsl:value-of select="$field"/>);</xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>down.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">move selected option down</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
    	<xsl:call-template name="putText"><xsl:with-param name="key">move selected option down</xsl:with-param></xsl:call-template>
    </div>
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
					<xsl:if test="$obj/attribute[@name = 'notUpdateable'] or $obj/ancestor::obj[@type='form']/readOnly">
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
				<xsl:if test="$boolDebug = 'true'"><xsl:value-of select="$obj/attribute[@name='name']/@value"/></xsl:if>
			</td>
			<td class="tight">
				<table class="tight">
					<tr>
						<xsl:if test="not($obj/attribute[@name = 'notUpdateable'] or $obj/ancestor::obj[@type='form']/readOnly)">
							<td class="tight">
								<xsl:call-template name="putLinkButton">
									<xsl:with-param name="obj" select="$obj" />
									<xsl:with-param name="destination"><xsl:value-of select="$path_to_top"/>asp/files/selectfiles.asp</xsl:with-param>
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
							<div style="display:none" class="hiddenForWords">
                               	<xsl:call-template name="putText"><xsl:with-param name="key">View Selected Item</xsl:with-param></xsl:call-template>
                            </div>				
						</td>
					</tr>					
				</table>
			</td>
		</tr>
	</table>
	<script language="javascript">
		pushSelectBox('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
		//arrSelects.push('document.<xsl:value-of select="$obj/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$obj/attribute[@name='name']/@value"/>');
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
	<a class="prodButton" target="PopUpWindow" onclick="PopWindow('',1000,400)">
		<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/Files/editFiles.asp?pageID=AddDocument&amp;SOURCE_TABLE=<xsl:value-of select="$linkedTable"/>&amp;SOURCE_ID=<xsl:value-of select="$ID"/>&amp;
		RECEIVER_FORM=<xsl:value-of select="$form"/>&amp;
		RECEIVER_FIELD=<xsl:value-of select="$field"/>&amp;
		MULTIPLE=<xsl:value-of select="$multiple"/></xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>upload_paperclip.gif</xsl:attribute>
			<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">AddDocumentsFromYourPC</xsl:with-param></xsl:call-template></xsl:attribute>
		</img>
		<div style="display:none" class="hiddenForWords">
			<xsl:call-template name="putText"><xsl:with-param name="key">AddDocumentsFromYourPC</xsl:with-param></xsl:call-template>            
        </div>
	</a>
</xsl:template>

<!--##################################################
    ## copyQstringItems                             ##
	################################################## -->
<xsl:template match="copyQstringItems">
<xsl:variable name="copyObj" select="."/>
	<xsl:for-each select="/Doc_Webpage/queryString/item">
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
							<xsl:call-template name="infoBox">
                            	<xsl:with-param name="infoText"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param>
                            </xsl:call-template>
							<div style="display:none" class="hiddenForWords">
                            	<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@titleHelpIcon"/></xsl:with-param></xsl:call-template>
                            </div>
						</xsl:if>
						<xsl:choose>
							<xsl:when test="$obj/attribute[@name = 'titleDontUsePutText']/@value">
								<xsl:value-of select="@title"/>					
							</xsl:when>
							<xsl:otherwise>
								<xsl:call-template name="putText"><xsl:with-param name="nobr">true</xsl:with-param><xsl:with-param name="key"><xsl:value-of select="@title"/></xsl:with-param></xsl:call-template>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:if test="@dataExpandable"> 
							<a>
								<xsl:attribute name="href">?<xsl:if test="not(@showAllData)">showAllData=true&amp;</xsl:if><xsl:call-template name="copyQueryString">
									<xsl:with-param name="noList" select="../noList" />
								</xsl:call-template>
								</xsl:attribute>
								<img border="0">
									<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:choose>
											<xsl:when test="@showAllData">leftArrow.gif</xsl:when>
											<xsl:otherwise>rightArrow.gif</xsl:otherwise>
										</xsl:choose>
									</xsl:attribute>
									<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@showAllData"/> Toggle show all data</xsl:with-param></xsl:call-template></xsl:attribute>
								</img>
							</a>
							<div style="display:none" class="hiddenForWords">
                               	<xsl:call-template name="putText"><xsl:with-param name="key">show all data in this column</xsl:with-param></xsl:call-template>
                            </div>
						</xsl:if> 
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
	<div align="center">
		<hr width="100%"/>
	</div>
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
	<xsl:if test="$boolDebug = 'true'"><div>Hidden <xsl:value-of select="@name"/> = <xsl:value-of select="@value"/></div></xsl:if>
</xsl:template>

<!--##################################################
    ##  permissions                                 ##
	################################################## -->
<xsl:template name="permissions">
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
													<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastPlus.gif</xsl:attribute>
												</img>				
												<div style="display:none" class="hiddenForWords">
	                                            	<xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template>
													<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>
	                                            </div>
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
													<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
												</img>				
												<div style="display:none" class="hiddenForWords">
													<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
	                                            </div>
											</a>
										</xsl:when>
										<xsl:when test="($item/tree) and not($item/ancestor::tree[@noCollapse='TRUE'])">
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">remove</xsl:with-param>
													</xsl:call-template>
												</xsl:attribute>										
												<img class="tree">
													<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastMinus.gif</xsl:attribute>
												</img>
												<div style="display:none" class="hiddenForWords">
													<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>                                            
	                                            </div>
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
													<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template></xsl:attribute>
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
													<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
													<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
														<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
													</xsl:if>
													<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
												</img>				
												<div style="display:none" class="hiddenForWords">
													<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
	                                            </div>
											</a>
										</xsl:when>
										<xsl:when test="($item/tree) and not($item/ancestor::tree[@noCollapse='TRUE'])">
											<a>
												<xsl:attribute name="href">
													<xsl:call-template name="buildExpandHREF">
														<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
														<xsl:with-param name="act">remove</xsl:with-param>
													</xsl:call-template>
												</xsl:attribute>										
												<img class="tree">
													<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template></xsl:attribute>
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
											<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template></xsl:attribute>
											<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
												<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
											</xsl:if>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastPlus.gif</xsl:attribute>
										</img>				
										<div style="display:none" class="hiddenForWords">
                                           	<xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template>
											<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>
                                        </div>
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
											<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
											<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
												<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
											</xsl:if>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
										</img>				
										<div style="display:none" class="hiddenForWords">
											<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
                                        </div>
									</a>
								</xsl:when>
								<xsl:when test="($item/tree) and not($item/ancestor::tree[@noCollapse='TRUE'])">
									<a>
										<xsl:attribute name="href">
											<xsl:call-template name="buildExpandHREF">
												<xsl:with-param name="ID" select="$item/record/field[@name='ID']/@value" />
												<xsl:with-param name="act">remove</xsl:with-param>
											</xsl:call-template>
										</xsl:attribute>										
										<img class="tree">
											<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template></xsl:attribute>
											<xsl:if test="$obj/attribute[@name = 'spacerClass']/@value">
												<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'spacerClass']/@value"/></xsl:attribute>
											</xsl:if>
											<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastMinus.gif</xsl:attribute>
										</img>
										<div style="display:none" class="hiddenForWords">
											<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>                                            
                                        </div>
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
			<td>
				<div style="display:none" class="hiddenForWords">
			  		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/confirm/@msg"/></xsl:with-param></xsl:call-template>
				</div>
				<div style="display:none" class="hiddenForWords">
					<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$obj/attribute[@name = 'alt']/@value"/></xsl:with-param></xsl:call-template>
				</div>

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
	<div style="display:none" class="hiddenForWords">
	   	<xsl:call-template name="putText"><xsl:with-param name="key">expand</xsl:with-param></xsl:call-template>
		<xsl:call-template name="putText"><xsl:with-param name="key">collapse</xsl:with-param></xsl:call-template>
	</div>
	<xsl:if test="$item/obj[@type='tree']">
		<tr>
			<td class="tree">
				<xsl:apply-templates select="obj"/>
			</td>
		</tr>
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
			<xsl:if test="$record/style">
				<xsl:attribute name="style"><xsl:value-of select="$record/style"/></xsl:attribute>			
			</xsl:if>
			<span class="treeData">
				<xsl:choose>
					<xsl:when test="@showAllData">
						<xsl:attribute name="class">treeDataShowAllData</xsl:attribute>
						<pre class="standardPre"><xsl:variable name="myField" select="@field" /><xsl:value-of select="$record/wordWrappedData/data[@field=$myField]"/></pre>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="style">width:<xsl:value-of select="@width"/></xsl:attribute>
						<nobr>
							<xsl:call-template name="putExtraDataField">
								<xsl:with-param name="field" select="@field" />
								<xsl:with-param name="record" select="$record" />
								<xsl:with-param name="exCol" select="." />
							</xsl:call-template>	
						</nobr>
					</xsl:otherwise>
				</xsl:choose>
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
		<xsl:when test="url">
			<a class="tree">
				<xsl:if test="not(translationInformation)">
					<xsl:attribute name="href"><xsl:value-of select="url"/><xsl:for-each select="$record/field">&amp;<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/></xsl:for-each></xsl:attribute>		
				</xsl:if>
				<xsl:if test="translationInformation">
					<xsl:attribute name="href"><xsl:value-of select="url"/><xsl:for-each select="translationInformation/field"><xsl:variable name="x" select="@name"/>&amp;<xsl:value-of select="@alias"/>=<xsl:value-of select="$record/field[@name = $x]/@value"/></xsl:for-each></xsl:attribute>
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
<xsl:if test="not(/Doc_Webpage/queryString/item[@name='expandList']) and ($act != 'remove')">
	expandList=<xsl:value-of select="$ID"/>&amp;
</xsl:if>
<xsl:for-each select="/Doc_Webpage/queryString/item[@name != 'expandAll']">
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
javascript:set_tip('<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$show"/></xsl:call-template><xsl:text> </xsl:text><xsl:call-template name="putText"><xsl:with-param name="key">Added to List</xsl:with-param></xsl:call-template>');<xsl:if test="$boolDebug='true'">alert('window.opener.AddToOptionBox(
\'<xsl:value-of select="$rForm"/>\',
\'<xsl:value-of select="$rField"/>\',
\'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$value"/></xsl:call-template>\',
\'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$show"/></xsl:call-template>\',
\'<xsl:value-of select="$multiple"/>\'');
</xsl:if>
window.opener.AddToOptionBox(
'<xsl:value-of select="$rForm"/>',
'<xsl:value-of select="$rField"/>',
'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$value"/></xsl:call-template>',
'<xsl:call-template name="jScriptEscape"><xsl:with-param name="val" select="$show"/></xsl:call-template>',
'<xsl:value-of select="$multiple"/>');
<xsl:choose>
	<xsl:when test="$multiple='' or not($multiple)">window.close()</xsl:when>
<!--	<xsl:otherwise>alert('Multiple = *<xsl:value-of select="$multiple"/>*');</xsl:otherwise> -->
</xsl:choose>

<div style="display:none" class="hiddenForWords"><xsl:call-template name="putText"><xsl:with-param name="key">Added to List</xsl:with-param></xsl:call-template></div>

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
<div class="textAdderMinimized" id="textAdderMinimized">
	<input type="button" onclick="showTextAdder()">
		<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">ShowTextAdder</xsl:with-param></xsl:call-template></xsl:attribute>
	</input>

	<div style="display:none" class="hiddenForWords">
		<xsl:call-template name="putText"><xsl:with-param name="key">ShowTextAdder</xsl:with-param></xsl:call-template>
	</div>
</div>
<div class="textAdder" id="textAdder">
<table style="width:15em;">
	<tr>
		<td colspan="2" style="text-align: right;"><a href="javascript:hideTextAdder()">X</a></td>
	</tr>
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
				<xsl:with-param name="style">font-weight: bold;width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;bb&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/bb&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text To Bold</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">bold</xsl:with-param>
			</xsl:call-template>
		</td>
<!--red Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: red;width:5em;</xsl:with-param>
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
				<xsl:with-param name="style">text-decoration: underline;width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;u&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/u&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text to be Underlined</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">underline</xsl:with-param>
			</xsl:call-template>
		</td>
<!--purple Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: purple;width:5em;</xsl:with-param>
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
				<xsl:with-param name="style">font-style: italic;width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;i&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/i&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text to be Italicized</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">italics</xsl:with-param>
			</xsl:call-template>
		</td>
<!--Blue Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: blue;width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;b&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/b&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Blue Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">blue</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>		
	<tr>
<!--bullet Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;li/&gt;&gt;<xsl:text> </xsl:text></xsl:with-param>
				<xsl:with-param name="val">bullet</xsl:with-param>
			</xsl:call-template>
		</td>
<!--Green Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: green;width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;g&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/g&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Green Text</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">green</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>		
	<tr>
<!--link Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">width:5em;</xsl:with-param>
				<xsl:with-param name="begin">&lt;&lt;link&gt;&gt;&lt;&lt;text&gt;&gt;</xsl:with-param>
				<xsl:with-param name="end">&lt;&lt;/text&gt;&gt;&lt;&lt;url&gt;&gt;<xsl:call-template name="putText"><xsl:with-param name="key">Enter URL Here</xsl:with-param></xsl:call-template>&lt;&lt;/url&gt;&gt;&lt;&lt;/link&gt;&gt;</xsl:with-param>
				<xsl:with-param name="middle"><xsl:call-template name="putText"><xsl:with-param name="key">Enter Text TO DISPLAY HERE</xsl:with-param></xsl:call-template></xsl:with-param>
				<xsl:with-param name="val">link</xsl:with-param>
			</xsl:call-template>
		</td>
<!--Yellow Button-->
		<td>
			<xsl:call-template name="textAdderButton">
				<xsl:with-param name="style">color: yellow;width:5em;</xsl:with-param>
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
				<xsl:with-param name="style">color: orange;width:5em;</xsl:with-param>
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
				<xsl:with-param name="style">color: black;width:5em;</xsl:with-param>
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
	<tr><td>
	<table class="tight">
		<tr>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;ang/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">Angstrom.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;micro/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">micro.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;omega/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">omega.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;phase/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">phase.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;theta/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">theta.gif</xsl:with-param>
				</xsl:call-template>
			</td>
		</tr>
		<tr>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;delta/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">delta.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;pi/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">pi.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;therefore/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">therefore.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;sigma/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">sigma.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;beta/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">beta.gif</xsl:with-param>
				</xsl:call-template>
			</td>
		</tr>
		<tr>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;divide/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">divide.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;plusMinus/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">plusMinus.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">~</xsl:with-param>
					<xsl:with-param name="noPutTextval">~</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;approx/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">approx.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;notEqual/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">notEqual.gif</xsl:with-param>
				</xsl:call-template>
			</td>
		</tr>



		<tr>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;lessEqual/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">lessEqual.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;greatEqual/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">greaterEqual.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;degree/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">degree.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;angle/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">angle.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			<td>
				<xsl:call-template name="textAdderButton">
					<xsl:with-param name="style">color: black;</xsl:with-param>
					<xsl:with-param name="begin"></xsl:with-param>
					<xsl:with-param name="end">&lt;&lt;infinity/&gt;&gt;</xsl:with-param>
					<xsl:with-param name="image">infinity.gif</xsl:with-param>
				</xsl:call-template>
			</td>
			
		</tr>
	</table></td></tr>
		
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
<xsl:param name="image"/>
<xsl:param name="noPutTextval"/>
	<div onmouseover="this.style.cursor='hand'" class="textAdderButton">
		<xsl:attribute name="style"><xsl:value-of select="$style"/></xsl:attribute>
		<xsl:attribute name="onClick" >
		insertAtCarat('<xsl:value-of select="$begin"/>','<xsl:value-of select="$end"/>','<xsl:value-of select="$middle"/>')
		</xsl:attribute>
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$val"/></xsl:with-param></xsl:call-template>
		<xsl:value-of select="$noPutTextval"/>
		<xsl:if test="string-length($image) &gt; 0">
		<img>
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/><xsl:value-of select="$image"/></xsl:attribute>
		</img>
		</xsl:if>
	</div>
</xsl:template>

<!--##################################################
    ## printApprovalItem                            ##
	################################################## -->
<xsl:template name="printApprovalItem">
<xsl:param name="record"/>
		<xsl:call-template name="putText"><xsl:with-param name="key">Approving Changes to</xsl:with-param></xsl:call-template>
		<xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">approval_<xsl:value-of select="$record/field[@name='ITEM_TYPE']/@value"/></xsl:with-param></xsl:call-template>
		<xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">Called</xsl:with-param></xsl:call-template>		
		<xsl:text> </xsl:text>
		<a>
			<xsl:attribute name="href"><xsl:value-of select="$path_to_top"/>asp/WF_Actions/edit.asp?OBJ_ID=<xsl:value-of select="$record/field[@name = 'OBJECT_ID']/@value"/></xsl:attribute>
			<xsl:text> </xsl:text>		
			<xsl:value-of select="$record/field[@name = 'ITEM_NAME']/@value"/>
			<xsl:text> </xsl:text>			
			<xsl:call-template name="putText"><xsl:with-param name="key">Revision:</xsl:with-param></xsl:call-template>
			<xsl:text> </xsl:text>
			<xsl:value-of select="$record/field[@name = 'REVISION']/@value"/>
		</a>
</xsl:template>

<!--##################################################
    ## putCommonAttributes                          ##
	################################################## -->
<xsl:template name="putCommonAttributes">
<xsl:param name="obj" />
	<xsl:if test="$obj/attribute/@name = 'tabIndex'">
		<xsl:attribute name="tabIndex"><xsl:value-of select="$obj/attribute[@name = 'tabIndex']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'class'">
		<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'name'">
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'id'">
		<xsl:attribute name="id"><xsl:value-of select="$obj/attribute[@name = 'id']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'value'">
		<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'value']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'style'">
		<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'onClick'">
		<xsl:attribute name="onClick"><xsl:value-of select="$obj/attribute[@name = 'onClick']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute/@name = 'onMouseOver'">
		<xsl:attribute name="onMouseOver"><xsl:value-of select="$obj/attribute[@name = 'onMouseOver']/@value"/></xsl:attribute>
	</xsl:if>
	<xsl:if test="$obj/attribute[@name = 'disabled']/@value != ''">
		<xsl:attribute name="disabled"><xsl:value-of select="$obj/attribute[@name = 'disabled']/@value"/></xsl:attribute>
	</xsl:if>
</xsl:template>

<!--##################################################
    ## printOneItemInput                            ##
	################################################## -->
<xsl:template name="printOneItemInput">
<xsl:param name="col"/>
<xsl:param name="field"/>
<xsl:param name="record"/>
<xsl:param name="position"/>
<xsl:variable name="myDel"><xsl:choose><xsl:when test="$col/../@inputDel"><xsl:value-of select="$col/../@inputDel"/></xsl:when><xsl:otherwise>___</xsl:otherwise></xsl:choose></xsl:variable>
<xsl:variable name="inputType" select="$col/@inputType"/>
<xsl:variable name="idField" select="$col/@idField"/>
<xsl:variable name="idVal" select="$record/field[@name = $idField]/@value"/>
<xsl:variable name="exFieldName" select="$col/@extraField"/>
<xsl:variable name="exFieldValue" select="$record/field[@name = $exFieldName]/@value"/>
	<xsl:choose>
		<xsl:when test="$inputType = 'text'">
			<input type="text">
				<xsl:attribute name="class"><xsl:value-of select="$col/@inputClass"/></xsl:attribute>
				<xsl:attribute name="style"><xsl:value-of select="$col/@inputStyle"/></xsl:attribute>
				<xsl:attribute name="size"><xsl:value-of select="$col/@inputSize"/></xsl:attribute>
				<xsl:attribute name="onClick"><xsl:value-of select="$col/@onClick"/></xsl:attribute>
				<xsl:attribute name="onBlur"><xsl:value-of select="$col/@onBlur"/></xsl:attribute>
				<xsl:attribute name="onChange"><xsl:value-of select="$col/@onChange"/></xsl:attribute>
				<xsl:attribute name="name">TEXT<xsl:value-of select="$myDel"/><xsl:value-of select="$idVal"/><xsl:value-of select="$myDel"/><xsl:value-of select="$field"/></xsl:attribute>
				<xsl:choose>
					<xsl:when test="$col/@useCounterForValue='true'">
						<xsl:value-of select="$record/field[@name = 'count']/@value"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="$col/@usePutText">
								<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:with-param></xsl:call-template></xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="value"><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:attribute>							
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:if test="$col/@onLoadFocus and $position = 1">
					<script language="javascript">
						objFocusOn = document.<xsl:value-of select="$record/ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.TEXT<xsl:value-of select="$myDel"/><xsl:value-of select="$idVal"/><xsl:value-of select="$myDel"/><xsl:value-of select="$field"/>
					</script>
				</xsl:if>
			</input>
		</xsl:when>
		<xsl:when test="$inputType = 'hidden'"><input type="hidden">
				<xsl:attribute name="name">TEXT<xsl:value-of select="$myDel"/><xsl:value-of select="$idVal"/><xsl:value-of select="$myDel"/><xsl:value-of select="$field"/></xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:attribute>
			</input></xsl:when>
		<xsl:when test="$inputType = 'dropDown'">
			<select>
				<xsl:attribute name="class"><xsl:value-of select="$col/@inputClass"/></xsl:attribute>
				<xsl:attribute name="style"><xsl:value-of select="$col/@inputStyle"/></xsl:attribute>
				<xsl:attribute name="size"><xsl:value-of select="$col/@inputSize"/></xsl:attribute>
				<xsl:attribute name="size"><xsl:value-of select="$col/@multiple"/></xsl:attribute>
				<xsl:attribute name="name">TEXT<xsl:value-of select="$myDel"/><xsl:value-of select="$idVal"/><xsl:value-of select="$myDel"/><xsl:value-of select="$field"/></xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:attribute>
				<xsl:for-each select="$col/dropDownData/record">
					<option>
						<xsl:attribute name="value"><xsl:value-of select="field[@name='VALUE']/@value"/></xsl:attribute>
						<xsl:if test="$record/field[@name = $field]/@value = field[@name='VALUE']/@value">
							<xsl:attribute name="selected">selected</xsl:attribute>
						</xsl:if>
						<xsl:value-of select="field[@name='SHOW']/@value"/>
					</option>
				</xsl:for-each>
			</select>
		</xsl:when>
		<xsl:when test="$inputType = 'checkBox'">
			<xsl:choose>
				<xsl:when test="not($record/field[@name = $field]/@value = $col/noCheckBox/item/@val)">
					<xsl:variable name="myName">CHECK_BOX____<xsl:value-of select="$idVal"/>____<xsl:value-of select="$field"/>____<xsl:value-of select="$exFieldValue"/></xsl:variable>
					<input type="checkBox">
						<xsl:if test="ancestor::obj[@type='form']/readOnly">
							<xsl:attribute name="disabled">true</xsl:attribute>
						</xsl:if>
						<xsl:attribute name="onClick"><xsl:value-of select="$col/@onChange"/></xsl:attribute>
						<xsl:attribute name="class"><xsl:value-of select="$col/@inputClass"/></xsl:attribute>
						<xsl:attribute name="style"><xsl:value-of select="$col/@inputStyle"/></xsl:attribute>
						<xsl:attribute name="size"><xsl:value-of select="$col/@inputSize"/></xsl:attribute>
						<xsl:attribute name="name"><xsl:value-of select="$myName"/></xsl:attribute>
						<xsl:attribute name="value"><xsl:value-of select="$record/field[@name = $field]/@value"/></xsl:attribute>
						<xsl:if test="$record/field[@name = $field]/@value = $col/@checked">
							<xsl:attribute name="checked">true</xsl:attribute>
						</xsl:if>
						<xsl:if test="$record/field[@name = $field]/@value = $col/checkedItems/item/@val">
							<xsl:attribute name="checked">true</xsl:attribute>
						</xsl:if>
					</input>
					<xsl:if test="not($col/@noCheckAll)">
					<script language="JavaScript">
						arr<xsl:value-of select="ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="position()"/> = arr<xsl:value-of select="ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="position()"/>.concat(new Array('<xsl:value-of select="ancestor::obj[@type='form']/attribute[@name='name']/@value"/>.<xsl:value-of select="$myName"/>'))
					</script>
		<!--			<xsl:value-of select="$idVal"/> - <xsl:value-of select="$record/field[@name = $field]/@value"/> - <xsl:value-of select="$col/@checked"/>-->
					</xsl:if>
				</xsl:when>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$record/field[@name = $field]/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ##  infoText                                    ##
	################################################## -->
<xsl:template name="infoBox">
<xsl:param name="infoText" />
	<a tabindex="-1"  >
    <xsl:attribute name="title">
      <xsl:call-template name="jScriptEscape">
        <xsl:with-param name="val">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">
              <xsl:value-of select="$infoText"/>
            </xsl:with-param>
          </xsl:call-template>
        </xsl:with-param>
      </xsl:call-template>
    </xsl:attribute>
		<img border="0">
			<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>info.gif</xsl:attribute>
		</img>
	</a>
	<div style="display:none" class="hiddenForWords">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$infoText"/></xsl:with-param></xsl:call-template>
	</div>
</xsl:template>


<!--##################################################
    ##  parentList                                  ##
	################################################## -->
<xsl:template match="parentList">
<xsl:for-each select="i">
	<a>
		<xsl:attribute name="href"><xsl:value-of select="../url"/>?
		<xsl:variable name="i" select="." />
		<xsl:for-each select="../qs/item">
			<xsl:variable name="fieldName"><xsl:value-of select="@val"/></xsl:variable>
			<xsl:variable name="aliasName"><xsl:value-of select="@alias"/></xsl:variable>
			<xsl:value-of select="$aliasName"/>=<xsl:value-of select="$i/f[@i=$fieldName]"/><xsl:if test="position() != last()">&amp;</xsl:if>
		</xsl:for-each>
		</xsl:attribute>
		<xsl:variable name="fieldName"><xsl:value-of select="../txt/@val"/></xsl:variable>
		<xsl:value-of select="f[@i=$fieldName]"/>
	</a>
	<xsl:if test="position() != last()"> &gt; </xsl:if>
</xsl:for-each>
</xsl:template>


<!--##################################################
    ##  selectCloseButton                           ##
	################################################## -->
<xsl:template match="selectCloseButton">
<div class="selectClose" id="selectClose">
	<input type="button">
		<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@val"/></xsl:with-param></xsl:call-template></xsl:attribute>
		<xsl:attribute name="onClick">window.close();</xsl:attribute>
 	</input>
</div>
<xsl:if test="$stringUpdate='yes'">
<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="@val"/></xsl:with-param></xsl:call-template>
<xsl:call-template name="putText"><xsl:with-param name="key">Added to List</xsl:with-param></xsl:call-template>
</xsl:if>
</xsl:template>


<!--##################################################
    ##  PRECEDING_STEPS                             ##
	################################################## -->
<xsl:template match="PRECEDING_STEPS">
<xsl:for-each select="s">
	<!--<a onmouseout="reset_tip();">
		<xsl:attribute name="onMouseOver">set_tip('<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="t"/></xsl:with-param></xsl:call-template>');</xsl:attribute>
		<xsl:attribute name="href">javascript:set_tip('<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="t"/></xsl:with-param></xsl:call-template>');</xsl:attribute>
	<xsl:if test="string-length(i) != 0">
		<xsl:variable name="i"><xsl:value-of select="i"/></xsl:variable>
		<xsl:choose>
			<xsl:when test="ancestor::data/record[field[@name='ID']/@value=$i]/field[@name='actualCount']/@value">
				<xsl:value-of select="ancestor::data/record[field[@name='ID']/@value=$i]/field[@name='actualCount']/@value"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="putText"><xsl:with-param name="key">Not on this page</xsl:with-param></xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:if>
	</a>-->
	<a onmouseout="reset_tip();">
		<xsl:attribute name="onMouseOver">set_tip('<xsl:call-template name="jScriptEscape"><xsl:with-param name="val"><xsl:value-of select="t"/></xsl:with-param></xsl:call-template>');</xsl:attribute>
		<xsl:attribute name="href">editStep.asp?ID=<xsl:value-of select="i"/></xsl:attribute>
		<xsl:value-of select="i"/>
	</a>
	<xsl:if test="position() != last()"> , </xsl:if>
</xsl:for-each>
	
</xsl:template>


<!--##################################################
    ##  PROCEDURE_PRECEDING_STEP                    ##
	################################################## -->
<xsl:template match="PROCEDURE_PRECEDING_STEP">
hello
	<xsl:for-each select="s">
	<xsl:if test="string-length(i) != 0">hi
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
    ##  TreeExpander                                ##
	################################################## -->
<xsl:template match="TreeExpander">
<xsl:variable name="curExpandList"><xsl:call-template name="buildExpandedList"><xsl:with-param name="data" select="ancestor::data"/></xsl:call-template></xsl:variable>
<xsl:variable name="curShrinkList"><xsl:call-template name="buildShrinkList"><xsl:with-param name="data" select="ancestor::data"/><xsl:with-param name="id" select="ancestor::record/field[@name='ID']/@value"/></xsl:call-template></xsl:variable>
<xsl:variable name="level" select="../../field[@name='TREE_LEVEL']/@value"/>

<xsl:variable name="treeNoList"><item val="expandList"/><item val="expandAll"/><item val="firstTime"/></xsl:variable>

<xsl:call-template name="putTreeBars">
	<xsl:with-param name="count" select="0"/>
	<xsl:with-param name="level" select="../../field[@name='TREE_LEVEL']/@value"/>
	<xsl:with-param name="me" select="." />
</xsl:call-template>

<xsl:choose>
	<xsl:when test="collapsable">
		<a>
		  	<xsl:attribute name="href">?<xsl:call-template name="copyQueryString">
					<xsl:with-param name="noList" select="$treeNoList" />
				</xsl:call-template>expandList=<xsl:value-of select="$curShrinkList"/></xsl:attribute>
			<img class="tree">
				<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">collapse This node</xsl:with-param></xsl:call-template></xsl:attribute>
				<xsl:choose>
					<xsl:when test="ancestor::record/following-sibling::record[field[@name='TREE_LEVEL']/@value = $level]">
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/midminus.gif</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastminus.gif</xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
			</img>
		</a>
	</xsl:when>
<xsl:otherwise>
	<img class="tree">
		<xsl:choose>
			<xsl:when test="ancestor::record/following-sibling::record[field[@name='TREE_LEVEL']/@value = $level]">
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/mid.gif</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/last.gif</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</img>
</xsl:otherwise>

</xsl:choose>
<xsl:choose>
<xsl:when test="expandable">
<a>
  	<xsl:attribute name="href">?<xsl:call-template name="copyQueryString">
					<xsl:with-param name="noList" select="$treeNoList" />
				</xsl:call-template>expandList=<xsl:value-of select="$curExpandList"/><xsl:value-of select="../../field[@name='ID']/@value"/>,</xsl:attribute>
	<img class="tree">
		<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/lastPlus.gif</xsl:attribute>
	</img>
</a>
<a>
  	<xsl:attribute name="href">?<xsl:call-template name="copyQueryString">
					<xsl:with-param name="noList" select="$treeNoList" />
				</xsl:call-template>expandList=<xsl:value-of select="$curExpandList"/>&amp;expandAll=<xsl:value-of select="../../field[@name='ID']/@value"/></xsl:attribute>
	<img class="tree">
		<xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template></xsl:attribute>
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/expandAll.gif</xsl:attribute>
	</img>
</a>
</xsl:when>
<xsl:otherwise>
	<img class="tree">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraLine.gif</xsl:attribute>
	</img>
	<img class="tree">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/extraLine.gif</xsl:attribute>
	</img>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
	<xsl:when test="./fieldToShow">
		<nobr>
		<xsl:variable name="fieldID"><xsl:value-of select="fieldToShow"/></xsl:variable>
		<xsl:value-of select="ancestor::record/field[@name=$fieldID]/@value"/>
		</nobr>
	</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="ancestor::record/field[@name='ID']/@value"/>
	</xsl:otherwise>
</xsl:choose>
	<div style="display:none" class="hiddenForWords">
		<xsl:call-template name="putText"><xsl:with-param name="key">Expand This node completely</xsl:with-param></xsl:call-template>                                            
    </div>

</xsl:template>

<!--##################################################
    ## buildExpandedList                            ##
	################################################## -->
<xsl:template name="putTreeBars">
<xsl:param name="count"/>
<xsl:param name="level"/>
<xsl:param name="me" />
<xsl:if test="$count &lt; $level">
	<img class="tree">
		<xsl:choose>
			<xsl:when test="$me/ancestor::record/following-sibling::record[field[@name='TREE_LEVEL']/@value = $count]">
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/insideSpacer.gif</xsl:attribute>
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>productTreeImages/spacer.gif</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</img>
	<xsl:call-template name="putTreeBars">
		<xsl:with-param name="count" select="$count + 1"/>
		<xsl:with-param name="level" select="$level"/>
		<xsl:with-param name="me" select="$me"/>
	</xsl:call-template>
</xsl:if>
</xsl:template>

<!--##################################################
    ## buildExpandedList                            ##
	################################################## -->
<xsl:template name="buildExpandedList">
<xsl:param name="data"/><xsl:for-each select="$data/record"><xsl:if test="field[@name = 'EXPANDED']/@value = 1"><xsl:value-of select="field[@name = 'ID']/@value"/>,</xsl:if></xsl:for-each></xsl:template>

<!--##################################################
    ## buildShrinkList                              ##
	################################################## -->
<xsl:template name="buildShrinkList">
<xsl:param name="data"/><xsl:param name="id"/><xsl:for-each select="$data/record[field[@name='ID']/@value != $id]"><xsl:if test="field[@name = 'EXPANDED']/@value = 1"><xsl:value-of select="field[@name = 'ID']/@value"/>,</xsl:if></xsl:for-each></xsl:template>

<!--##################################################
    ##  copyQueryString                             ##
	################################################## -->
<xsl:template name="copyQueryString">
<xsl:param name="noList"/>
<xsl:for-each select="/Doc_Webpage/queryString/item">
	<xsl:variable name="itemName"><xsl:value-of select="@name"/></xsl:variable>
	<xsl:if test="not($noList/item[@val = $itemName])"><xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;</xsl:if>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  copyWholeQueryString                             ##
	################################################## -->
<xsl:template name="copyWholeQueryString">
<xsl:for-each select="/Doc_Webpage/queryString/item">
	<xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;
</xsl:for-each>
</xsl:template>

<!--##################################################
    ## putCheckAllBox                              ##
	################################################## -->
<xsl:template name="putCheckAllBox">
<xsl:param name="col"/><script language="JavaScript">
	var arr<xsl:value-of select="ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="position()"/> = new Array(0);
</script><input type="Checkbox" value="everything" name="checkAllBox"><xsl:attribute name="title"><xsl:call-template name="putText"><xsl:with-param name="key">Deselect or Select all items</xsl:with-param></xsl:call-template></xsl:attribute>
	<xsl:attribute name="onClick">
	if(this.checked)
		{
		var f = arr<xsl:value-of select="ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="position()"/>;
		for (j = 0; j &lt; f.length; j++) 
			{
			eval('document.' + f[j] + '.checked = true');
			}
		}
	else
		{
		var f = arr<xsl:value-of select="ancestor::obj[@type='form']/attribute[@name='name']/@value"/>_<xsl:value-of select="position()"/>;
		for (j = 0; j &lt; f.length; j++) 
			{
			eval('document.' + f[j] + '.checked = false');
			}
		}
	</xsl:attribute>
</input></xsl:template>

<!--##################################################
    ##  objRoundBox                                 ##
	################################################## -->
<xsl:template name="objRoundBox">
<xsl:param name="obj"/>
		<table class="roundBox" style="background-color:white;">
			<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
			<tr>
				<td class="window_upper_left">
				<img border="0"><xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>roundBorder/trans.gif</xsl:attribute></img>
				</td>
				<td class="window_upper_middle">
				<img border="0"><xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>roundBorder/trans.gif</xsl:attribute></img>
				</td>
				<td class="window_upper_right">
				<img border="0"><xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>roundBorder/trans.gif</xsl:attribute></img>
				</td>
			</tr>
			<tr>
				<td class="window_left_side"></td>
				<td class="window_middle">
					<xsl:apply-templates />					
				</td>
				<td class="window_right_side"></td>
			</tr>
			<tr>
				<td class="window_lower_left">
				<img border="0"><xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>roundBorder/trans.gif</xsl:attribute></img>
				</td>
				<td class="window_lower_middle">
				<img border="0"><xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>roundBorder/trans.gif</xsl:attribute></img>
				</td>
				<td class="window_lower_right">
				<img border="0"><xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>roundBorder/trans.gif</xsl:attribute></img>
				</td>
			</tr>				
		</table>
</xsl:template>


<!--##################################################
    ##  treeMainDataToggle                          ##
	################################################## -->
<xsl:template name="treeMainDataToggle">
<input type="button" name="treeMainDataToggle">
	<xsl:choose>
		<xsl:when test="/Doc_Webpage/queryString/item[@name='showOneRow']">
			<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Show complete data</xsl:with-param></xsl:call-template></xsl:attribute>	
			<xsl:attribute name="onClick">document.location='?<xsl:call-template name="copyQueryStringWithNoUse"><xsl:with-param name="noUse1">showOneRow</xsl:with-param></xsl:call-template>'</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="value"><xsl:call-template name="putText"><xsl:with-param name="key">Condense to one row</xsl:with-param></xsl:call-template></xsl:attribute>		
			<xsl:attribute name="onClick">document.location='?showOneRow=true&amp;<xsl:call-template name="copyQueryStringWithNoUse" />'</xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
</input>
<div style="display:none" class="hiddenForWords">
   	<xsl:call-template name="putText"><xsl:with-param name="key">Condense to one row</xsl:with-param></xsl:call-template>
   	<xsl:call-template name="putText"><xsl:with-param name="key">Show complete data</xsl:with-param></xsl:call-template>
</div>

</xsl:template>

<!--##################################################
    ##  copyQueryStringWithNoUse                    ##
	################################################## -->
<xsl:template name="copyQueryStringWithNoUse"><xsl:param name="noUse1"/><xsl:param name="noUse2"/><xsl:param name="noUse3"/><xsl:for-each select="/Doc_Webpage/queryString/item[(@name != $noUse1) and (@name != $noUse2) and (@name != $noUse3)]"><xsl:value-of select="@name"/>=<xsl:value-of select="@value"/>&amp;</xsl:for-each></xsl:template>

<!--##################################################
    ##  showMenuCollapsedHeader                     ##
	################################################## -->
<xsl:template match="showMenuCollapsedHeader">
<a href="javascript:" style="color:black;">
	<img align="middle" border="0">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>/borderPlus.gif</xsl:attribute>
	</img>
		<xsl:call-template name="putText"><xsl:with-param name="key">leftMenuGroupHeading_<xsl:value-of select="@group"/></xsl:with-param></xsl:call-template>
</a>
</xsl:template>

<!--##################################################
    ##  showMenuExpandedHeader                      ##
	################################################## -->
<xsl:template match="showMenuExpandedHeader">
<a href="javascript:" style="color:black;">
	<img align="middle" border="0">
		<xsl:attribute name="src"><xsl:value-of select="$path_to_images"/>/borderMinus.gif</xsl:attribute>
	</img>
	<xsl:call-template name="putText"><xsl:with-param name="key">leftMenuGroupHeading_<xsl:value-of select="@group"/></xsl:with-param></xsl:call-template>
</a>
</xsl:template>

<!--##################################################
    ##                                             ##
	################################################## -->
<xsl:template match="fillUpdateWords">
<xsl:variable name="f"><xsl:value-of select="/Doc_Webpage/queryString/item[@name='formName']/@value"/></xsl:variable>
<xsl:variable name="i"><xsl:value-of select="/Doc_Webpage/queryString/item[@name='itemName']/@value"/></xsl:variable>
Filling an Update Word
window.opener.fillTextBox('<xsl:value-of select="$f"/>','<xsl:value-of select="$i"/>',jXMLDecode('<xsl:value-of select="//general/txt/textString[@id=//lookup]/@value"/>'));
	<script language="JavaScript" type="text/javascript">
		window.opener.fillTextBox('<xsl:value-of select="$f"/>','<xsl:value-of select="$i"/>',escape(jXMLDecode('<xsl:value-of select="//general/txt/textString[@id=//lookup]/@value"/>')));
		window.close();
	</script>

</xsl:template>


<!--##################################################
    ## objAsyncFileUpload                           ##
	################################################## -->
<xsl:template name="objAsyncFileUpload">
<xsl:param name="obj"/>
	<form method="post" enctype="multipart/form-data">
		<xsl:attribute name="action"><xsl:value-of select="$path_to_top"/>asp/files/processUploadInIFrame.asp</xsl:attribute>
		<xsl:attribute name="target"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_I_FRAME</xsl:attribute>
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_FORM</xsl:attribute>
		<xsl:attribute name="onSubmit">
		divPutRedText('<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_STATUS_DIV','Uploading...');
		setTimeout('document.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_FORM.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_SUBMIT_BUTTON.disabled = true;',200);
		setTimeout('document.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_FORM.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_description.disabled = true;',200);
		setTimeout('document.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_FORM.<xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_FILE_UPLOAD.disabled = true;',200);
		</xsl:attribute>
		
		<table>
			<tr>
				<td>
					<xsl:call-template name="putText"><xsl:with-param name="key">File to Upload:</xsl:with-param></xsl:call-template>
				</td>
				<td>
					<input type="file">
						<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
						<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
						<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_FILE_UPLOAD</xsl:attribute>
					</input>
					<input type="hidden" name="runAfterUpload">
						<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'afterUpload']/@value"/></xsl:attribute>
					</input>
					<input type="hidden" name="rootName">
						<xsl:attribute name="value"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/></xsl:attribute>
					</input>
				</td>
			</tr>
			<tr>
				<td>
					<xsl:call-template name="putText"><xsl:with-param name="key">File Description</xsl:with-param></xsl:call-template>
				</td>
				<td>
					<input type="text"><xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_description</xsl:attribute>
						<xsl:attribute name="class"><xsl:value-of select="$obj/attribute[@name = 'class']/@value"/></xsl:attribute>
						<xsl:attribute name="style"><xsl:value-of select="$obj/attribute[@name = 'style']/@value"/></xsl:attribute>
					</input>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<input type="submit" value="upload">
						<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_SUBMIT_BUTTON</xsl:attribute>
					</input>
				</td>
			</tr>
		</table>
		<div style="height:1em;">
			<xsl:attribute name="id"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_STATUS_DIV</xsl:attribute>
			<xsl:call-template name="putText"><xsl:with-param name="key">Choose a file, enter a description and hit upload.</xsl:with-param></xsl:call-template>
		</div>
	</form>
	<iframe height="0px" width="0px">
		<xsl:attribute name="name"><xsl:value-of select="$obj/attribute[@name = 'name']/@value"/>_I_FRAME</xsl:attribute>
		<xsl:text> </xsl:text>
	</iframe>
</xsl:template>

</xsl:stylesheet>


