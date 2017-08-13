<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet	version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--##################################################
    ## TSR			                                ##
	################################################## -->
<xsl:template match="TSR_TASK_DATA">
<xsl:variable name="t" select="." />
  <div class="printableReport">
    <table>
    <tr>
      <td>
        <div class="ncr_supplierName">
          <xsl:value-of select="fillInfo/record/field[@name='SUP_NAME']/@value"/>
        </div>
      </td>
    </tr>
  </table>
    <table class="ncr_report">
      <tr>
        <td class="ncr_Label">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">Customer</xsl:with-param>
          </xsl:call-template>
        </td>
        <td class="ncr_data">
          <xsl:value-of select="fillInfo/record/CUST_NAME"/>
        </td>
      </tr>
      <tr>
        <td class="ncr_Label">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">Product</xsl:with-param>
          </xsl:call-template>
        </td>
        <td class="ncr_data">
          <xsl:value-of select="fillInfo/record/PROD_NAME"/>
          <xsl:text>[</xsl:text>
          <xsl:value-of select="fillInfo/record/PROC_ID"/>
          <xsl:text>]</xsl:text>
        </td>
      </tr>
      <tr>
        <td class="ncr_Label">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">Part Info</xsl:with-param>
          </xsl:call-template>
        </td>
        <td class="ncr_data">
          <xsl:value-of select="fillInfo/record/FILL_OBJ_DESC"/>
        </td>
      </tr>
      <tr>
        <td class="ncr_Label">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">Procedure</xsl:with-param>
          </xsl:call-template>
        </td>
        <td class="ncr_data">
          <xsl:value-of select="fillInfo/record/PROC_NAME"/>
        </td>
      </tr>
      <tr>
        <td class="ncr_Label">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">Customer_PO</xsl:with-param>
          </xsl:call-template>
        </td>
        <td class="ncr_data">
          <xsl:value-of select="fillInfo/record/CUST_LINE_ITEM"/>
        </td>
      </tr>
      <tr>
        <td class="ncr_Label">
          <xsl:call-template name="putText">
            <xsl:with-param name="key">Technician</xsl:with-param>
          </xsl:call-template>
        </td>
        <td class="ncr_data">
          <xsl:value-of select="tasks/record[VERB_NAME='NCR']/LATEST_REQUESTEE_NAME"/>
        </td>
      </tr>
    </table>

    <br />
    <xsl:for-each select="tasks/record[VERB_NAME='NCR' and PRINT_ORDER='1']">
      <xsl:variable name="parentId" select="PARENT_ID"></xsl:variable>
      <table  class="ncr_report">
        <tr>
          <td class="ncr_label">
            <span style="width:100px;text-align:right;" >
              <xsl:call-template name="putText">
                <xsl:with-param name="key">NCR Number:</xsl:with-param>
              </xsl:call-template>              
              <xsl:text> </xsl:text>
            </span>
          </td>
          <td class="ncr_data">
            <span style="width:130px;text-align:left;" >
              <xsl:value-of select="$parentId"/>
            </span>
          </td>
        </tr>
        <tr>
          <td class="ncr_label">
            <span style="width:100px;text-align:right;" >
              <xsl:call-template name="putText">
                <xsl:with-param name="key">Step Incurred:</xsl:with-param>
              </xsl:call-template>
              <xsl:text> </xsl:text>
            </span>
          </td>
          <td class="ncr_data">
            <span style="width:130px;text-align:left;" >
              <xsl:variable name="incurStepID" select="../record[STEP_ID=$parentId]/PARENT_ID" />
              
              <xsl:copy-of select="../record[STEP_ID=$incurStepID]/STEP_TEXT_HTML"/>
              <xsl:text>[</xsl:text>
              <xsl:call-template name="putText">
                <xsl:with-param name="key">Step:</xsl:with-param>
              </xsl:call-template>
              <xsl:text> </xsl:text>
              <xsl:value-of select="../record[STEP_ID=$incurStepID]/PRINT_ORDER"/>
              <xsl:text>]</xsl:text>
            </span>
          </td>
        </tr>

        <xsl:for-each select="../record[PARENT_ID=$parentId]">
          <tr>
            <td class="" style="background-color:silver;" colspan="2">
              <xsl:copy-of select="*[name()='STEP_TEXT_HTML']" />
            </td>
          </tr>

        <xsl:variable name="taskID" select="*[name()='STEP_ID']" />
        <xsl:for-each select="../../monitors/record[TASK_ID = $taskID]">
          <tr>
            <td class="ncr_label">
              <span style="width:100px;text-align:right;" >
                <xsl:value-of select="DESCRIPTION"/>
                <xsl:text> </xsl:text>
              </span>
            </td>
            <td class="ncr_data">
              <span style="width:130px;text-align:left;" >
                <xsl:value-of select="PRINT_RESULT"/>
              </span>
            </td>
          </tr>
        </xsl:for-each>

        </xsl:for-each>

      </table>
      <br />
      </xsl:for-each>
    <xsl:if test="attachments/record">
      <table class="ncrAttachments">
        <tr>
          <td colspan="2">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Attachments:</xsl:with-param>
            </xsl:call-template>
            <br />
            <xsl:for-each select="attachments/record">
              <table class="ncr_attachment">
                <tr>
                  <td>
                    <img class="printable" src="../documents/viewDocument.asp?docID={FILE_ID}" alt="{FILE_DESCRIPTION}" />
                  </td>
                </tr>
                <tr>
                  <td style="text-align:center;">
                    <xsl:value-of select="FILE_DESCRIPTION"/>
                  </td>
                </tr>
              </table>
            </xsl:for-each>
          </td>
        </tr>
      </table>
    </xsl:if>

    <table>
      <xsl:for-each select="tasks/record">
        <tr>
          <td>
            <xsl:attribute name="style">
              <xsl:text>padding-left:</xsl:text>
              <xsl:value-of select="LEVEL * 15"/>
              <xsl:text>px;</xsl:text>
            </xsl:attribute>
            <xsl:choose>
              <xsl:when test="PRINT_ORDER = ''">
                <xsl:call-template name="putText">
                  <xsl:with-param name="key">Procedure:</xsl:with-param>
                </xsl:call-template>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="PRINT_ORDER"/>
                <xsl:text>.</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </td>
          <td class="normalTable" style="border-bottom:thin silver solid">
            <xsl:attribute name="style">
              <xsl:text>padding-left:</xsl:text>
              <xsl:value-of select="15 + 10*(*[name()='TREE_LEVEL'])"/>
            </xsl:attribute>
            <xsl:copy-of select="*[name()='STEP_TEXT_HTML']" />
            <xsl:if test="A_START/answerDate != ''">
            <span style="color:silver;font-decoration:italics">
              <xsl:text>(</xsl:text>
              <xsl:value-of select="A_START/answerDate"/>
              <xsl:text>)</xsl:text>

            </span>
              <span style="color:silver;font-decoration:italics">
                <xsl:text>(</xsl:text>
                 <xsl:value-of select="ID"/>
                <xsl:text>)</xsl:text>
              </span>
            </xsl:if>

            <xsl:variable name="taskID" select="*[name()='STEP_ID']" />
            <xsl:for-each select="../../monitors/record[STEPPER_ID = $taskID]">
              <div class="monitor" >
                <xsl:attribute name="style">
                  <xsl:text>padding-left:20px;</xsl:text>
                  <xsl:choose>
                    <xsl:when test="IS_PASSING=0">
                      <xsl:text>color:red;font-weight:bold;</xsl:text>
                    </xsl:when>
                    <xsl:when test="IS_PASSING=''">
                      <xsl:text>color:silver;font-weight:normal;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>color:green</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:attribute>
                <xsl:call-template name="putText">
                  <xsl:with-param name="key">NCR_Monitor:</xsl:with-param>
                </xsl:call-template>
                <xsl:value-of select="DESCRIPTION"/>
                <xsl:text> </xsl:text>
                <span style="color:black">
                  <xsl:value-of select="PRINT_RESULT"/>
                </span>
              </div>
              <xsl:if test="COMMENT!=''">
                <div class="monitor monitorComment" style="padding-left:20px">
                  <xsl:call-template name="putText">
                    <xsl:with-param name="key">TechnicianComments:</xsl:with-param>
                  </xsl:call-template>
                  <xsl:value-of select="COMMENT"/>
                </div>
              </xsl:if>
            </xsl:for-each>
            
          </td>
        </tr>
      </xsl:for-each>

    </table>
  
  </div>
</xsl:template>

<!--##################################################
    ##  PURCHASE_SERVICE_REPORT                     ##
	################################################## -->
<xsl:template match="PURCHASE_SERVICE_REPORT">
<xsl:variable name="t" select="." />
<xsl:for-each select="purchaseItems/record">
	<div>
		<xsl:if test="position()!=last()">
			<xsl:attribute name="style">page-break-after:always</xsl:attribute>
		</xsl:if>
		<xsl:variable name="pItem" select="." />
		<xsl:for-each select="../../fillData/record[PURCH_ITEM_ID=$pItem/ID]">
			<xsl:call-template name="PRINT_PURCHASE_ITEM_PAGE">
				<xsl:with-param name="pItem" select="$pItem" />
				<xsl:with-param name="t" select="$t" />
			</xsl:call-template>
		</xsl:for-each>
	</div>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  PRINT_PURCHASE_ITEM_PAGE                                           ##
	################################################## -->
<xsl:template name="PRINT_PURCHASE_ITEM_PAGE">
<xsl:param name="pItem"/>
<xsl:param name="t"/>
<xsl:call-template name="PURCH_ITEM_HEADER">
	<xsl:with-param name="t" select="$t" />
	<xsl:with-param name="pItem" select="$pItem" />
	<xsl:with-param name="p" select="." />	
</xsl:call-template>
</xsl:template>

<!--##################################################
    ##  PRINT_TECHNICAL_SERVICE_REPORT              ##
	################################################## -->
<xsl:template name="PRINT_TECHNICAL_SERVICE_REPORT">
<xsl:param name="pItem"/>
<xsl:param name="t"/>
<xsl:call-template name="TSR_HEADER">
	<xsl:with-param name="t" select="$t" />
	<xsl:with-param name="pItem" select="$pItem" />
	<xsl:with-param name="fill" select="." />	
</xsl:call-template>
</xsl:template>

<!--##################################################
    ##   TSR_HEADER                                 ##
	################################################## -->
<xsl:template name="TSR_HEADER">
<xsl:param name="t" />
<xsl:param name="pItem" />
<xsl:param name="fill" />
<table>
	<tr>
		<td>
			<xsl:call-template name="printCompanyTable">
				<xsl:with-param name="c" select="$t/supplier" />
			</xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="printProcedureHeader">
				<xsl:with-param name="procID"><xsl:value-of select="$fill/PURCHASE_HIST_ID" /></xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="printFillDataII">
				<xsl:with-param name="fill" select="$fill" />
			</xsl:call-template>
		</td>
	</tr>
</table>
<xsl:call-template name="printProcedure">
	<xsl:with-param name="procID"><xsl:value-of select="$fill/PURCHASE_HIST_ID" /></xsl:with-param>
	<xsl:with-param name="fill" select="$fill" /><xsl:with-param name="t" select="$t" /><xsl:with-param name="pItem" select="$pItem" />
</xsl:call-template>
</xsl:template>

<!--##################################################
    ##  printProcedure                              ##
	################################################## -->
<xsl:template name="printProcedure">
<xsl:param name="procID" />
<xsl:param name="fill" />
<xsl:param name="t" />
<xsl:param name="pItem" />

<xsl:for-each select="/Doc_Webpage/content/default_body/*/PROCEDURES/ONE_PROCEDURE/ANSWER_PROCEDURE">
<table>
	<tr>
		<td></td>
		<td>
			<xsl:call-template name="putText"><xsl:with-param name="key">Data</xsl:with-param></xsl:call-template>
		</td>
		<td>
			<xsl:call-template name="putText"><xsl:with-param name="key">Status</xsl:with-param></xsl:call-template>
		</td>
	</tr>
	<xsl:for-each select="./stepData/obj/data/record">
		<xsl:call-template name="printStep">
			<xsl:with-param name="pos"><xsl:value-of select="position()"/></xsl:with-param>
        </xsl:call-template>
	</xsl:for-each>
</table>
</xsl:for-each>
</xsl:template>

<!--##################################################
    ##  printStep                                   ##
	################################################## -->
<xsl:template name="printStep">
<xsl:param name="pos" />
<xsl:variable name="stepID"><xsl:value-of select="./field[@name='ID']/@value"/></xsl:variable>
<xsl:variable name="stepT"><xsl:copy-of select="./field[@name='STEP_TEXT']"/></xsl:variable>

<tr>
	<td style="padding-right:5px;"><xsl:value-of select="$pos"/>.</td>
	<td>
		<xsl:copy-of select="field[@name='STEP_TEXT']"/>
	</td>
	<xsl:for-each select="/Doc_Webpage/content/default_body/*/TASK_DATA/record[PROCEDURE_STEP_ID=$stepID]">
	<td>
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="STATUS"/></xsl:with-param></xsl:call-template>
	</td>																
	</xsl:for-each>
</tr>
	<tr>
		<td></td>
		<td>
			<xsl:if test="postRecordData/ANSWER_PROCEDURE_POST_RECORD/procedure_step_monitor/record">
			<table class="standard">
				<tr><td class="standard title">Description</td><td class="standard title">Result</td></tr>
			<xsl:for-each select="postRecordData/ANSWER_PROCEDURE_POST_RECORD/procedure_step_monitor/record">
				<xsl:variable name="ri"><xsl:value-of select="field[@name='ROLL_UP_ID']/@value"/></xsl:variable>
				<tr>
					<td class="standard">
						<xsl:choose>
							<xsl:when test="string-length(./field[@name='DESCRIPTION']/@value) &gt; 0">
								<xsl:value-of select="./field[@name='DESCRIPTION']/@value"/>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$stepT"/>
							</xsl:otherwise>
						</xsl:choose>
					</td>				
					<td class="standard">
						<xsl:for-each select="/Doc_Webpage/content/default_body/*/TASK_DATA/record[PROCEDURE_STEP_ID=$stepID]">
							<xsl:variable name="taskID"><xsl:value-of select="ID"/></xsl:variable>
							<xsl:for-each select="/Doc_Webpage/content/default_body/*/monitors/record[TASK_ID=$taskID and ROLL_UP_ID = $ri]">
								<xsl:choose>
									<xsl:when test="MONITOR_TYPE='YES_NO'">
										<xsl:call-template name="putText"><xsl:with-param name="key">YES_NO_<xsl:value-of select="PRINT_RESULT"/></xsl:with-param></xsl:call-template>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="PRINT_RESULT"/>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</xsl:for-each>
						
					</td>
				</tr>
			</xsl:for-each>
		</table>
		</xsl:if>
		</td>
	</tr>
</xsl:template>


<!--##################################################
    ##  printFillData                               ##
	################################################## -->
<xsl:template name="printFillDataII">
<xsl:param name="fill"/>
<table>
	<xsl:value-of select="$fill/FILL_OBJ_DESC"/>
</table>
</xsl:template>

<!--##################################################
    ##  putHeaderVal                                ##
	################################################## -->
<xsl:template name="putHeaderVal">
<xsl:param name="n"/>
<xsl:param name="v"/>
<div style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$n"/></xsl:with-param></xsl:call-template></div>
<div style="text-align:center"><xsl:value-of select="$v"/></div>
<xsl:if test="string-length($v) &gt; 0">
	<div style="text-align:center"><span class="barcode"><nobr>*<xsl:value-of select="$v"/>*</nobr></span></div>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  PURCH_ITEM_HEADER                                  ##
	################################################## -->
<xsl:template name="PURCH_ITEM_HEADER">
<xsl:param name="t" />
<xsl:param name="pItem" />
<xsl:param name="p" />
<table class="tight">
	<tr>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Purchase Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$t/purchase/OBJECT_ID"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Purchase Item Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/ID"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Single PO Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$t/purchase/CUST_PURCH_NUM"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Blanket PO Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/../accountInfo/record/REFERENCE_PO"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Cust Line Item</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/CUST_LINE_ITEM"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Phys Part ID</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$p/FILL_OBJ_ID"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Serial</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$p/SERIAL"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Qty</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$p/QTY"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
		<td class="border">
			<xsl:call-template name="putHeaderVal">
				<xsl:with-param name="n">Acct Num</xsl:with-param>
				<xsl:with-param name="v"><xsl:value-of select="$pItem/../accountInfo/record/ID"/></xsl:with-param>
		    </xsl:call-template>	
		</td>
	</tr>
</table>
<!--This is the supplier and customer data:-->
<table style="width:8in">
	<tr>
		<td>
			<xsl:call-template name="printCompanyTable">
				<xsl:with-param name="c" select="$t/supplier" />
				<xsl:with-param name="n">Supplier:</xsl:with-param>
			</xsl:call-template>
		</td>
		<td style="padding-left:5px;">
			<xsl:call-template name="printCompanyTable">
				<xsl:with-param name="c" select="$t/customer" />
				<xsl:with-param name="n">Customer:</xsl:with-param>
			</xsl:call-template>
		</td>
	</tr>
</table>

<table class="standard">
	<xsl:call-template name="printDataRow">
		<xsl:with-param name="l">Product (ID) Name</xsl:with-param>
		<xsl:with-param name="val"><xsl:value-of select="$p/PROD_NAME"/> (<xsl:value-of select="$p/PROD_ID"/>)</xsl:with-param>
	</xsl:call-template>
	<xsl:call-template name="printDataRow">
		<xsl:with-param name="l">Procedure (ID) Name</xsl:with-param>
		<xsl:with-param name="val"><xsl:value-of select="$p/PROC_NAME"/> (<xsl:value-of select="$p/PROC_ID"/>)</xsl:with-param>
	</xsl:call-template>
	<xsl:call-template name="printDataRow">
		<xsl:with-param name="l">Part Description</xsl:with-param>
		<xsl:with-param name="val"><xsl:value-of select="$p/PART_DESC"/> (<xsl:value-of select="$p/COMPANY_PART_NUMBER"/>)</xsl:with-param>
	</xsl:call-template>
	<xsl:call-template name="printDataRow">
		<xsl:with-param name="l">Fill Date</xsl:with-param>
		<xsl:with-param name="val"><xsl:value-of select="$p/FILL_DATE/answerDate"/></xsl:with-param>
	</xsl:call-template>
</table>

</xsl:template>

<!--##################################################
    ##  printDataRow                                ##
	################################################## -->
<xsl:template name="printDataRow">
<xsl:param name="l"/>
<xsl:param name="val"/>
<tr>
	<td class="bold standard">
		<xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$l"/></xsl:with-param></xsl:call-template>
	</td>
	<td class="standard">
		<xsl:value-of select="$val"/>
	</td>
</tr>
</xsl:template>


<!--##################################################
    ##  printCompanyTable                           ##
	################################################## -->
<xsl:template name="printCompanyTable">
<xsl:param name="c"/>						
<xsl:param name="n"/>						
			<table>
			<xsl:if test="$c/logo/record/LINKED_DOC_ID">
				<tr><td>
				<img border="0" width="103" height="36">
					<xsl:attribute name="src"><xsl:value-of select="$path_to_top"/>asp/documents/viewDocument.asp?docID=<xsl:value-of select="$c/logo/record/LINKED_DOC_ID"/>&amp;width=103&amp;height=36</xsl:attribute>
				</img>

				</td></tr>
			</xsl:if>
				<tr><td class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key"><xsl:value-of select="$n"/></xsl:with-param></xsl:call-template></td></tr>
				<tr><td><xsl:value-of select="$c/NAME"/></td></tr>
			<xsl:if test="string-length($c/LOCATION_NAME) &gt; 0">
				<tr><td><xsl:value-of select="$c/LOCATION_NAME"/></td></tr>
				<tr><td><xsl:value-of select="$c/phone/PHONE"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/ADDRESS_1"/></td></tr>
				<tr><td><xsl:value-of select="$c/location/record/CITY"/>,
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/STATE"/>
				<xsl:text> </xsl:text>
				<xsl:value-of select="$c/location/record/POSTAL_CODE"/>
				</td></tr>
				<tr><td><xsl:value-of select="$c/location/record/COUNTRY"/></td></tr>
			</xsl:if>
			</table>



</xsl:template>


<!--##################################################
    ##  signatureLine                                           ##
	################################################## -->
<xsl:template name="signatureLine">
<xsl:param name="t" />
<br />
<br />
<table style="" width="100%">
	<tr>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><nobr><xsl:value-of select="$t/TASK_PURCHASE_DATA/record/field[@name='CUSTOMER_NAME']/@value"/></nobr></td>
		<td valign="bottom" width="60%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
	</tr>
	<tr>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Company</xsl:with-param></xsl:call-template></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Signature</xsl:with-param></xsl:call-template></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">date</xsl:with-param></xsl:call-template></td>
	</tr>
	<tr>
		<td>
		<br />
		<br />
		</td>
	</tr>
	<tr>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><nobr><xsl:value-of select="$t/TASK_PURCHASE_DATA/record/field[@name='SUPPLIER_NAME']/@value"/></nobr></td>
		<td valign="bottom" width="60%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
		<td valign="bottom" width="20%" style="border-bottom:1px solid black;font-size:14px;"><xsl:text>X</xsl:text></td>
	</tr>
	<tr>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Company</xsl:with-param></xsl:call-template><br /><br /></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">Signature</xsl:with-param></xsl:call-template><br /><br /></td>
		<td valign="top" style="text-align:center"><xsl:call-template name="putText"><xsl:with-param name="key">date</xsl:with-param></xsl:call-template><br /><br /></td>
	</tr>
	
</table>

</xsl:template>

<!--##################################################
    ##  printPurchaseItem                           ##
	################################################## -->
<xsl:template name="printPurchaseItem">
<xsl:param name="level" />
<xsl:variable name="purchItemID"><xsl:value-of select="field[@name='PURCH_ITEM_ID']/@value"/></xsl:variable>
<xsl:variable name="prodHistID"><xsl:value-of select="field[@name='PRODUCT_HIST_ID']/@value"/></xsl:variable>

<div>
	<xsl:attribute name="style">margin-left:<xsl:value-of select="$level * 10"/>px;margin-right:5px;</xsl:attribute>
	<div class="subTitle"><xsl:value-of select="$purchItemID"/></div>
	<div class="barcode"><xsl:value-of select="$purchItemID"/></div>
	<xsl:if test="../../fillData/record/field[@name='PURCH_ITEM_ID' and @value=$purchItemID]">
		<xsl:call-template name="printPurchItemProdInfo">
			<xsl:with-param name="fill" select="../../fillData/record[field[@name='PURCH_ITEM_ID' and @value=$purchItemID]]" />
			<xsl:with-param name="level" select="$level" />
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="string-length(field[@name='ACCOUNT_ID']/@value) != 0">
		<xsl:value-of select="field[@name='EX_DESC']/@value"/><xsl:text> </xsl:text>
		<xsl:call-template name="putText"><xsl:with-param name="key">Account:</xsl:with-param></xsl:call-template>
		<xsl:value-of select="field[@name='ACCOUNT_ID']/@value"/><xsl:text> </xsl:text>
		<xsl:value-of select="field[@name='NAME']/@value"/><xsl:text> </xsl:text>
	</xsl:if>


	<xsl:if test="../../fillData/record/field[@name='PURCH_ITEM_ID' and @value=$purchItemID] and $level = 0">
		<div>
			<div class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key">Fill Data:</xsl:with-param></xsl:call-template></div>
			<xsl:if test="string-length(../../fillData/record[field[@name='PURCH_ITEM_ID' and @value=$purchItemID]]/field[@name='COMPANY_PART_NUMBER']/@value)!=0">
				<table class="standard sm">
					<tr>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Number</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Serial Kit Number</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Qty</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Part Description</xsl:with-param></xsl:call-template>
							</span>
						</th>
						<th class="standard sm">
							<span class="sm">
							<xsl:call-template name="putText"><xsl:with-param name="key">Date Filled</xsl:with-param></xsl:call-template>
							</span>
						</th>
					</tr>
					<xsl:for-each select="../../fillData/record[field[@name='PURCH_ITEM_ID' and @value=$purchItemID]]">
						<xsl:sort select="field[@name='SERIAL']/@value" />
						<xsl:sort select="field[@name='NICK_NAME']/@value" />
						<xsl:sort select="field[@name='COMPANY_PART_NUMBER']/@value" />
						<xsl:call-template name="printFillData" >
						</xsl:call-template>
					</xsl:for-each>
				</table>
			</xsl:if>
		</div><br />
	</xsl:if>
	<xsl:if test="/Doc_Webpage/queryString/item[@name = 'SHOW_SHIPPING' and @value='YES']">
	<xsl:for-each select="../record[field[@name='PARENT' and @value=$purchItemID] and field[@name='DEST' and @value='to']]">
		<xsl:call-template name="printPurchaseItem" >
			<xsl:with-param name="level" select="$level + 1" />
		</xsl:call-template>
		<br />
	</xsl:for-each>
	</xsl:if>
	<xsl:if test="../../fillData/record/field[@name='PURCH_ITEM_ID' and @value=$purchItemID]">
		<div>
			<xsl:attribute name="style">padding-left:<xsl:value-of select="($level+1)*10"/>px;</xsl:attribute>
			<div class="subtitle"><xsl:call-template name="putText"><xsl:with-param name="key">Procedure:</xsl:with-param></xsl:call-template></div>
			<xsl:for-each select="../../PROCEDURES/ONE_PROCEDURE[@prodHistID=$prodHistID]">
				<xsl:apply-templates />
			</xsl:for-each>
		</div>
	</xsl:if>

	<xsl:if test="../../PURCHASE_TASK_DATA/record[field[@name='PURCHASE_ITEM_ID' and @value=$purchItemID]]">
		<div>
			<xsl:attribute name="style">padding-left:<xsl:value-of select="($level+1)*10"/>px;</xsl:attribute>
			<div class="subTitle"><xsl:call-template name="putText"><xsl:with-param name="key">Actual Tasks:</xsl:with-param></xsl:call-template></div>
			<table class="standard" style="font-size: x-small;">
				<tr>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Task ID</xsl:with-param></xsl:call-template></th>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Description</xsl:with-param></xsl:call-template></th>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Due Date</xsl:with-param></xsl:call-template></th>
					<th class="standard sm" style="font-size: x-small;"><xsl:call-template name="putText"><xsl:with-param name="key">Actual Stop Date</xsl:with-param></xsl:call-template></th>

				</tr>
				<xsl:for-each select = "../../PURCHASE_TASK_DATA/record[field[@name='PURCHASE_ITEM_ID' and @value=$purchItemID]]">
					<tr>
						<td class="standard sm" style="font-size: x-small;"><xsl:value-of select="field[@name='ID']/@value"/></td>
						<td class="standard sm" style="font-size: x-small;"><span style="font-size: x-small;"><xsl:value-of select="field[@name='DESCRIPTION']/@value"/></span></td>
						
						<td class="standard sm" style="font-size: x-small;"><xsl:value-of select="field[@name='CUR_PLANNED_STOP_DATE']/@answerDate"/></td>
						<td class="standard sm" style="font-size: x-small;"><xsl:value-of select="field[@name='ACTUAL_STOP_DATE']/@answerDate"/></td>
					</tr>
				</xsl:for-each>
			</table>
		</div>
	</xsl:if>
	<br />

	<xsl:if test="/Doc_Webpage/queryString/item[@name = 'SHOW_SHIPPING' and @value='YES']">
	<xsl:for-each select="../record[field[@name='PARENT' and @value=$purchItemID] and field[@name='DEST' and @value='from']]">
		<xsl:call-template name="printPurchaseItem" >
			<xsl:with-param name="level" select="$level + 1" />
		</xsl:call-template>
		<br />
	</xsl:for-each>
	</xsl:if>
</div>
</xsl:template>

<!--##################################################
    ##  printFillData                               ##
	################################################## -->
<xsl:template name="printFillData">
<tr>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='COMPANY_PART_NUMBER']/@value"/></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='SERIAL']/@value"/><span class="barCode"><xsl:value-of select="field[@name='SERIAL']/@value"/></span></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='QTY']/@value"/></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='PART_DESC']/@value"/></span></td>
	<td class="standard sm"><span class="sm"><xsl:value-of select="field[@name='FILL_DATE']/@answerDate"/></span></td>
</tr>

</xsl:template>


<!--##################################################
    ##  printPurchItemProdInfo                      ##
	################################################## -->
<xsl:template name="printPurchItemProdInfo">
<xsl:param name="fill"/>
<xsl:param name="level"/>
<span>
	<xsl:if test="$level = 0"><xsl:attribute name="style">font-weight:bold;font-size:large;</xsl:attribute></xsl:if>
	<xsl:value-of select="$fill/field[@name='PROD_NAME']/@value"/><xsl:text> </xsl:text>
	<xsl:call-template name="putText"><xsl:with-param name="key">QTY</xsl:with-param></xsl:call-template>
	<xsl:text> </xsl:text>
	<xsl:value-of select="$fill/field[@name='TOT_QTY']/@value"/>
	<xsl:text> </xsl:text>
	

</span>
</xsl:template>

<!--##################################################
    ##  printTaskRow                                ##
	################################################## -->
<xsl:template name="printTaskRow">
<xsl:variable name="myID"><xsl:value-of select="field[@name='ID']/@value"/></xsl:variable>
<table style="border:1px solid black;width:100%;">
	<tr>
		<td>
			<xsl:attribute name="style">padding-left:<xsl:value-of select="(number(field[@name = 'TREE_LEVEL']/@value) * 15)"/>px;</xsl:attribute>
			<i><xsl:value-of select="field[@name='ACTUAL_START_DATE']/@answerDate"/></i><xsl:text> - </xsl:text>
			<b><xsl:value-of select="field[@name='DESCRIPTION']/@value"/></b>
			<table style="border-collapse:collapse">
				<tr><td></td></tr>
			<xsl:call-template name="printTimeInfo" />
			<xsl:call-template name="printMonitors" />
			</table>
		</td>
	</tr>
</table>
</xsl:template>

<!--##################################################
    ##  printTimeInfo                               ##
	################################################## -->
<xsl:template name="printTimeInfo">
<xsl:variable name="myID"><xsl:value-of select="field[@name='ID']/@value"/></xsl:variable>
<xsl:if test="../../minutes/record[field[@name='TASK_ID']/@value =$myID]">
	<tr>
		<td style="">
			<i><xsl:call-template name="putText"><xsl:with-param name="key">Recorded Time on Task:</xsl:with-param></xsl:call-template>:</i>
		</td>
		<td style="font-weight:600;">
			<xsl:for-each select="../../minutes/record[field[@name='TASK_ID']/@value =$myID]">
				<xsl:call-template name="printOnePersonsTime" />
			</xsl:for-each>
		</td>
	</tr>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  printOnePersonsTime                         ##
	################################################## -->
<xsl:template name="printOnePersonsTime">
<xsl:value-of select="field[@name='WORKER_NAME']/@value"/>:
<xsl:if test="number(field[@name='HRS']/@value) &gt; 0">
	<xsl:value-of select="field[@name='HRS']/@value"/><xsl:text> </xsl:text><xsl:call-template name="putText"><xsl:with-param name="key">hr</xsl:with-param></xsl:call-template> <xsl:text> </xsl:text>
</xsl:if>
<xsl:if test="number(field[@name='MY_MIN']/@value) &gt; 0">
	<xsl:value-of select="field[@name='MY_MIN']/@value"/><xsl:text> </xsl:text><xsl:call-template name="putText"><xsl:with-param name="key">min</xsl:with-param></xsl:call-template> <xsl:text> </xsl:text>
</xsl:if>
<xsl:if test="position() != last()">
	<br />
</xsl:if>
</xsl:template>



<!--##################################################
    ##   printMonitors                              ##
	################################################## -->
<xsl:template name="printMonitors">
<xsl:variable name="myID"><xsl:value-of select="field[@name='ID']/@value"/></xsl:variable>
<xsl:if test="../../monitors/record[field[@name='TASK_ID']/@value =$myID]">
	<tr>
		<td style="">
			<i><xsl:call-template name="putText"><xsl:with-param name="key">Tests:</xsl:with-param></xsl:call-template></i>
		</td>
		<td style="font-weight:600;">
			<xsl:for-each select="../../monitors/record[field[@name='TASK_ID']/@value =$myID]">
				<xsl:call-template name="printMonitor" />
			</xsl:for-each>
		</td>
	</tr>
</xsl:if>
</xsl:template>

<!--##################################################
    ##  printMonitor                                ##
	################################################## -->
<xsl:template name="printMonitor">
	<xsl:value-of select="field[@name='DESCRIPTION']/@value"/><xsl:text> </xsl:text>
	<xsl:call-template name="putText"><xsl:with-param name="key">Result:</xsl:with-param></xsl:call-template>
	<xsl:text> </xsl:text>
	<xsl:choose>
		<xsl:when test="field[@name='MONITOR_TYPE']/@value = 'YES_NO'">
			<xsl:call-template name="putText"><xsl:with-param name="key">YES_NO_<xsl:value-of select="field[@name='MY_ANSWER']/@value"/></xsl:with-param></xsl:call-template>
		</xsl:when>
	
		<xsl:otherwise>
			<xsl:value-of select="field[@name='MY_ANSWER']/@value"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!--##################################################
    ##  printProcedureHeader                        ##
	################################################## -->
<xsl:template name="printProcedureHeader">
<xsl:param name="procID" />
<xsl:for-each select="/Doc_Webpage/content/default_body/*/PROCEDURES/ONE_PROCEDURE/ANSWER_PROCEDURE">
<table>
	<tr>
		<td>
			<xsl:value-of select="headerData/record/field[@name='NAME']/@value"/>
		</td>
		<td style="padding-left:5px;padding-right:5px;">
			<xsl:call-template name="putText"><xsl:with-param name="key">Revision:</xsl:with-param></xsl:call-template>
		</td>
		<td>
			<xsl:value-of select="headerData/record/field[@name='REV']/@value"/>
		</td>
	</tr>
</table>
</xsl:for-each>
</xsl:template>

</xsl:stylesheet>
