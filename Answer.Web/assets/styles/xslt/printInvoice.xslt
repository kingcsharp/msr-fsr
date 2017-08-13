<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="printableInvoice">
      <div class="printableInvoice">
        <div class="invoiceLogoHeader">
          <table style="width:100%;border:none;">
            <tr>
              <td style="text-align:left">
                <xsl:if test="supplierInfo/record/LOGO_ID != ''">
                  <img src="../documents/viewDocument.asp?docID={supplierInfo/record/LOGO_ID}" class="invoiceLogo" />
                </xsl:if>
              </td>
              <td style="text-align:right;vertical-align:middle;">
                <div class="invoiceTitle">
                  <xsl:text>INVOICE</xsl:text>
                </div>
              </td>
            </tr>
          </table>
          <hr />
          <xsl:call-template name="putRemitToLine" />
          <hr />
          <xsl:call-template name="putCustomerInfo" />
          <xsl:call-template name="putInvoiceItems" />
        </div>
      </div>
      <!-- ./../asp/documents/viewDocument.asp?docID=84609 -->
    </xsl:template>

  <xsl:template name="putInvoiceItems">
    <div class="invoiceItems">
      <xsl:if test="supplierInfo/record/LOGO_ID != ''">
        <img src="../documents/viewDocument.asp?docID={supplierInfo/record/LOGO_ID}" class="invoiceLogo" />
      </xsl:if>
      <table class="invoiceItems">
        <tr>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Date POSTED</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Act Inv Item Type</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Cust Purchase Number</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Cust Line Item</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">AII Purchase Number</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">AII Item ID</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">QTY</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Description</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Unit Price</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Amt of Inv Items</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceItemTitle">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Tax</xsl:with-param>
            </xsl:call-template>
          </td>
        </tr>
        <xsl:for-each select="data/data/record">
          <tr>
            <td class="invoiceItemItem money">
              <xsl:value-of select="field[@name='DATE_POSTED']/@shortDate" />
            </td>
            <td class="invoiceItemItem">
              <xsl:value-of select="field[@name='ITEM_TYPE']/@value" />
            </td>
            <td class="invoiceItemItem">
              <xsl:value-of select="field[@name='CUST_PURCH_NUM']/@value" />
            </td>
            <td class="invoiceItemItem">
              <xsl:text> </xsl:text>
              <xsl:value-of select="field[@name='PURCH_ITEM_ID']/@value" />
            </td>
            <td class="invoiceItemItem">
              <xsl:value-of select="field[@name='PURCHASE_ID']/@value" />
            </td>
            <td class="invoiceItemItem">
              <xsl:value-of select="field[@name='CUST_LINE_ITEM']/@value" />
            </td>
            <td class="invoiceItemItem">
              <xsl:value-of select="field[@name='QTY']/@value" />
            </td>
            <td class="invoiceItemItem">
              <xsl:value-of select="field[@name='DESCRIPTION']/@value" />
            </td>
            <td class="invoiceItemItem money">
              <xsl:value-of select="field[@name='UNIT_PRICE']/@value" />
            </td>
            <td class="invoiceItemItem money">
              <xsl:value-of select="field[@name='AMOUNT']/@value" />
            </td>
            <td class="invoiceItemItem money">
              <xsl:value-of select="field[@name='TAX']/@value" />
            </td>
          </tr>
        </xsl:for-each>
      </table>
    </div>
    <div class="invoiceTotals">
      <table class="invoiceTotals">
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Account ID:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal">
            <xsl:value-of select="invoiceInfo/record/ACCOUNT_ID"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Invoice ID:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal">
            <xsl:value-of select="invoiceInfo/record/INVOICE_ID"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Invoice Date:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal">
            <xsl:value-of select="invoiceInfo/record/INVOICE_DATE/shortDate"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Payment Due Date:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal">
            <xsl:value-of select="invoiceInfo/record/DUE_DATE/shortDate"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">New Payments and Credits:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal money">
            <xsl:value-of select="invoiceInfo/record/TOTAL_CREDITS/text()"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">New Debits:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal money">
            <xsl:value-of select="invoiceInfo/record/TOTAL_DEBITS/text()"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">New Late Fees:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal money">
            <xsl:value-of select="invoiceInfo/record/LATE_FEES/text()"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Tax:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal money">
            <xsl:value-of select="invoiceInfo/record/TOTAL_TAX/text()"/>
          </td>
        </tr>
        <tr>
          <td class="invoiceTotalsLabel">
            <xsl:call-template name="putText">
              <xsl:with-param name="key">Total Due:</xsl:with-param>
            </xsl:call-template>
          </td>
          <td class="invoiceTotalsVal money">
            <xsl:value-of select="invoiceInfo/record/TOTAL_DUE/text()"/>
          </td>
        </tr>
      </table>
    </div>
    
  </xsl:template>
    
  <xsl:template name="putRemitToLine">
    <div class="remitLine">
      <table style="width:100%">
        <tr>
          <td>
            <table>
              <tr>
                <td>
                  <xsl:text>Remit To:</xsl:text>
                </td>
                <td>
                  <xsl:value-of select="supplierInfo/record/COMPANY_NAME"/>
                  <br />
                  <xsl:value-of select="supplierInfo/record/ADDRESS_1"/>
                  <br />
                  <xsl:if test="supplierInfo/record/ADDRESS_2 != ''">
                    <xsl:value-of select="supplierInfo/record/ADDRESS_2"/>
                    <br />
                  </xsl:if>
                  <xsl:value-of select="supplierInfo/record/CITY"/>
                  <xsl:text>, </xsl:text>
                  <xsl:value-of select="supplierInfo/record/STATE"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="supplierInfo/record/POSTAL_CODE"/>
                  <br />
                  <xsl:value-of select="supplierInfo/record/PHONE"/>
                </td>
              </tr>
            </table>
          </td>
          <td style="vertical-align:middle;text-align:right">
            <div class="invoiceNumber">
              <table class="invoiceNumber" style="width:100%">
                <tr>
                  <td style="width:75%; text-align:right;">
                    <xsl:text>Invoice No: </xsl:text>
                  </td>
                  <td style="width:25%; text-align:right;">
                    <xsl:value-of select="invoiceInfo/record/INVOICE_ID"/>
                  </td>
                </tr>
                <tr>
                  <td style="width:75%; text-align:right;">
                    <xsl:text>Invoice Date: </xsl:text>
                  </td>
                  <td style="width:75%; text-align:right;">
                    <xsl:value-of select="invoiceInfo/record/INVOICE_DATE/shortDate"/>
                  </td>
                </tr>
              </table>
            </div>
          </td>
        </tr>
      </table>
    </div>
  </xsl:template>
  
  <xsl:template name="putCustomerInfo">
    <div class="invoiceCustomerLine">
      <table style="width:100%">
        <tr>
          <td>
            <div class="customerBox">
              <xsl:choose>
                <xsl:when test="customerBillInfo/record">
                  <xsl:value-of select="customerBillInfo/record/COMPANY_NAME"/>
                  <br />
                  <xsl:value-of select="customerBillInfo/record/ADDRESS_1"/>
                  <br />
                  <xsl:if test="customerBillInfo/record/ADDRESS_2 != ''">
                    <xsl:value-of select="customerBillInfo/record/ADDRESS_2"/>
                    <br />
                  </xsl:if>
                  <xsl:value-of select="customerBillInfo/record/CITY"/>
                  <xsl:text>, </xsl:text>
                  <xsl:value-of select="customerBillInfo/record/STATE"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="customerBillInfo/record/POSTAL_CODE"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="customerInfo/record/COMPANY_NAME"/>
                  <br />
                  <xsl:value-of select="customerInfo/record/ADDRESS_1"/>
                  <br />
                  <xsl:if test="customerInfo/record/ADDRESS_2 != ''">
                    <xsl:value-of select="customerInfo/record/ADDRESS_2"/>
                    <br />
                  </xsl:if>
                  <xsl:value-of select="customerInfo/record/CITY"/>
                  <xsl:text>, </xsl:text>
                  <xsl:value-of select="customerInfo/record/STATE"/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="customerInfo/record/POSTAL_CODE"/>
                </xsl:otherwise>
              </xsl:choose>
            </div>
          </td>
          <td style="vertical-align:middle;text-align:right">
            <div class="invoicePONUM">
              <table class="invoicePONUM">
                <tr>
                  <td>
                    <xsl:text>P.O. #</xsl:text>
                  </td>
                  <td>
                    <xsl:value-of select="invoiceInfo/record/PO_NUMBER"/>
                  </td>
                </tr>
                <xsl:if test="invoiceInfo/record/REFERENCE_NAME != ''">
                  <tr>
                    <td>
                      <xsl:text>P.O. Name:</xsl:text>

                    </td>
                    <td>
                      <xsl:value-of select="invoiceInfo/record/REFERENCE_NAME"/>
                    </td>
                  </tr>
                </xsl:if>
              </table>
            </div>
          </td>
        </tr>
      </table>
    </div>
  </xsl:template>
</xsl:stylesheet>
