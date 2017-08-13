<xsl:stylesheet  version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:variable name="utilitiesFolder">/utilities</xsl:variable>

<!--##################################################
    ##  /                                           ##
	################################################## -->
<xsl:template match="vCalendarEvent">
BEGIN:VCALENDAR
VERSION:1.0
BEGIN: VEVENT
DTStart:<xsl:value-of select="START_DATE"/>
DTEnd:<xsl:value-of select="STOP_DATE" />
SUMMARY;ENCODING=QUOTED-PRINTABLE:<xsl:value-of select="SUBJECT"/>
DESCRIPTION;ENCODING=QUOTED-PRINTABLE:<xsl:value-of select="BODY"/>
Location;ENCODING=QUOTED-PRINTABLE:<xsl:value-of select="LOCATION"/>
UID:<xsl:value-of select="ID"/>
PRIORITY:3
End:VEVENT
End:VCALENDAR
</xsl:template>

</xsl:stylesheet>