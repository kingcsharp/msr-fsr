var myUpdateWindow
var PopUpWindow
var BossWindow	
var RoleWindow	
var arrForms
var arrSelects
var lastTextArea
var objFocusOn
var arrOnLoadFunctions
var strObjectPopUPAddress
var strObjPopID
var arrPopUps
var logOutSeconds
var intShowSmallTools
intShowSmallTools = 0;
arrForms = new Array(0);
arrSelects = new Array(0);
arrOnLoadFunctions = new Array(0);
arrPopUps = new Array(0);
var LogOutMinutes = 5
var step = 1

function showMDiv(s)
{
var x;
x = 1;
while (showTogDiv(s + String(x))){x++}
}

function hideMDiv(s)
{
var x;
x = 1;
while (hideTogDiv(s + String(x))){x++}
}

function checkHighlight(obj)
{
if (getSelectedValue(obj) == "NOTHING")
	{alert('nothing selected and nothing here.');checkHighlight=''}
else
	{checkHighlight=getSelectedValue(obj);alert('got ' + getSelectedValue(obj))}
}

function collapseColumn(strID)
{
hideTogDiv('columnHeader__'+strID)
hideTogDiv('column_collapser__'+strID)
showTogDiv('column_expander__'+strID)
var x = 1;
while(hideTogDiv('res_column__' + String(x) + '__' + strID))
	{x++;}
}

function expandColumn(strID)
{
hideTogDiv('column_expander__'+strID)
showTogDiv('columnHeader__'+strID)
showTogDiv('column_collapser__'+strID)
var x = 1;
while(showTogDiv('res_column__' + String(x) + '__' + strID))
	{x++;}


}

function createCookie(name,value,days)
{
	if (days)
	{
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name)
{
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++)
	{
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name)
{
	createCookie(name,"",-1);
}



function showTogDiv(toggleId, e)
{
 if (!e) {
  e = window.event;
 }
 if (!document.getElementById) {
  return false;
 }
 var body = document.getElementById(toggleId);
 if (!body) {
  return false;
 } 
 body.style.display = 'block';
 return(true)
}

function hideTogDiv(toggleId, e)
{
 if (!e) {
  e = window.event;
 }
 if (!document.getElementById) {
  return false;
 }
 var body = document.getElementById(toggleId);
 if (!body) {
  return false;
 } 
 body.style.display = 'none';
 return(true)
}

function hideElement(toggleId, e)
{
 if (!e) {
  e = window.event;
 }
 if (!document.getElementById) {
  return false;
 }
 var body = document.getElementById(toggleId);
 if (!body) {
  return false;
 } 
 body.style.display = 'none';
}



function hideRowActionItems()
{
var x
x = 1
//toggle('ROW_ACTION_' + String(x));
while(hideTogDiv('ROW_ACTION_' + String(x)) && showTogDiv('SMALL_TOOL_BOX_' + String(x)))
	{x++;}
showTogDiv('openToolBox');
hideTogDiv('closeToolBox');
}

function showRowActionItems()
{
var x
x = 1
while(showTogDiv('ROW_ACTION_' + String(x)) && hideTogDiv('SMALL_TOOLS_' + String(x)) &&  hideTogDiv('SMALL_TOOL_BOX_' + String(x)) &&  hideTogDiv('SMALL_TOOL_BOX_CLOSER_' + String(x))  )
	{x++;}
hideTogDiv('openToolBox');
showTogDiv('closeToolBox');
}


function toggle(toggleId, e)
{
 if (!e) {
  e = window.event;
 }
 if (!document.getElementById) {
  return false;
 }
 var body = document.getElementById(toggleId);
 if (!body) {
  return false;
 } 
 var im = toggleId + "_toggle";
 if (body.style.display == 'none') {
  body.style.display = 'block';
 } else {
  body.style.display = 'none';
 }
 if (e) {
  // Stop the event from propagating, which
  // would cause the regular HREF link to
  // be followed, ruining our hard work.
  e.cancelBubble = true;
  if (e.stopPropagation) {
   e.stopPropagation();
  }
 }
 return(true)
}




function setDiscussionColorBox(o)
{
var co = getEl('colorSample');
var coBox = o.COLOR;
if (String(o.selectedIndex) == 'undefined')
	{
	
	}
}



function unCheckLikeMe(c,f)
{
var limit = f.elements.length;
var i;
for (i=0;i<limit;i++) {
	if (f.elements[i].name == c.name)
		{
		f.elements[i].checked = false;
		}
	}
c.checked=true;
}


function startLogOutTimer()
{
logOutSeconds = (LogOutMinutes*60)
if (document.timerForm)
	{
	document.timerForm.timerBox.value = logOutSeconds
	setTimeout('logOutTimerCountDown()',step * 1000);
	}
}

function logOutTimerCountDown()
{
logOutSeconds += (-1);
document.timerForm.timerBox.value = logOutSeconds
setTimeout('logOutTimerCountDown()',step * 1000);
if (logOutSeconds <= 0)
	{
	logOutSeconds = LogOutMinutes * 60
	ajaxSaveToFile('/' + strRootName + '/refreshSession.asp','')	
	//PopWindow('/' + strRootName + '/refreshSession.asp',0,0,'_blank')
	}
}

function jXMLDecode(s)
{
	var str = new String(s);
	str = str.replace( /&amp;/gi,"&");
	return(str);
}

function checkMaxSize(o,intSize)
{
var strME = new String(o.value);
if (strME.length > intSize)
	{
	alert(o.form.SYSTEM_STRING_TO_LONG_MESSAGE1.value + String(strME.length) + o.form.SYSTEM_STRING_TO_LONG_MESSAGE2.value + String(intSize));
	o.select()
	o.focus()
	}
}

function printAnError(e)
{
if (e.description == null) {
  alert("Exception: " + e.message);
 }
 else
 {
  alert("Exception: " + e.description);
 }
}

function closePops()
{

var i
for (i in arrPopUps)
	{alert('got one' + i.type);i.close();if(i){i.close()}}
}



/*function verifyNonNulls(o,strFields)
input: 	o - a form
		strFields - -*- seperated list of inputs to check
The input forms need to have a spna around them and the span
should have an attribute with an id of document_[form name]_[input name]
*/

function verifyNonNulls(o,strFields)
{
var arrFields,oEl,resp
resp = true;
try
	{
	arrFields = strFields.split("-*-");
	for (i in arrFields)
		{
		strVal = new String(eval('document.' + o.name + '.' + arrFields[i] + '.value'))
		if (strVal.length == 0)
			{
			eval('document.' + o.name + '.' + arrFields[i] + '.focus()');
			oEl = document.getElementById('document_' + o.name + "_" + arrFields[i])
			oEl.style.border = "thin red solid";
			oEl.style.bgcolor = "red";
			
			resp = false;
			}
		else
			{
			eval('document.' + o.name + '.' + arrFields[i] + '.focus()');
			oEl = document.getElementById('document_' + o.name + "_" + arrFields[i])
			if(oEl)
				oEl.style.border = "0";			
			}		
		
		}
	}
catch(exception)
	{	
	printAnError(exception);
	resp = false;
	}
return(resp);
}




function onLoadFunction(){
if (objFocusOn)
	{
	objFocusOn.select();
	objFocusOn.focus();
	}
var j
for (j = 0; j < arrOnLoadFunctions.length; j++) 
	{
//	alert(arrOnLoadFunctions[j]);
	eval(arrOnLoadFunctions[j]);
	}
}


function addOnLoadFunction(str)
{
arrOnLoadFunctions = arrOnLoadFunctions.concat(new Array(str));
//arrOnLoadFunctions.push(str)
}

function insertAtCarat(strOpeningText,strClosingText,strMiddleText) { 

var selText = "";
var txtBefore = "";
var txtAfter = "";
var startPos;
var endPos;
if ((lastTextArea + '' == 'undefined'))
	{return}
var textEl = eval(lastTextArea);
textEl.focus ()
if (window.getSelection)
{
		var text = textEl.value;
		startPos = textEl.selectionStart;
		endPos = textEl.selectionEnd;
		txtBefore = text.substring (0, startPos);
		selText = text.substring (startPos, endPos);
		if (selText != '')
		{
			strMiddleText = "";
		} else {
			if (strMiddleText != '')
				{
					startPos = startPos - strClosingText.length;
					endPos = endPos + strMiddleText.length - strClosingText.length;
				}
			startPos = startPos + strClosingText.length;
			endPos = endPos + strClosingText.length;
		}
		txtAfter = text.substring (textEl.selectionEnd, textEl.length);
		textEl.value = txtBefore + strOpeningText + selText + strMiddleText + strClosingText + txtAfter;
		textEl.selectionStart = startPos + strOpeningText.length
		textEl.selectionEnd = endPos + strOpeningText.length
} else {

if (textEl.createTextRange && textEl.caretPos)
{ 
	var CP = textEl.caretPos;
	if (CP.text == '')
		{
		CP.text = strOpeningText + strMiddleText + strClosingText
		}
	else
		{
		var strTT
		strTT = CP.text
		CP.text = strOpeningText + strTT + strClosingText		
		}
} 
	else 
	{return}

textEl.select()	
var myTxt = document.selection.createRange()

if (strMiddleText == '')
	{CP.select();return}


if (myTxt.findText(strMiddleText)) {
	myTxt.moveStart("character", -1);
	myTxt.findText(strMiddleText);
	myTxt.select();
	myTxt.scrollIntoView();
	}
else
	{
	if (myTxt.findText(strOpeningText + strTT + strClosingText)) {
		myTxt.moveStart("character", -1);
		myTxt.findText(strOpeningText + strTT + strClosingText);
		myTxt.select();
		myTxt.scrollIntoView();
		}
	else
		{CP.select()}
	}
}
}

function storeCaret (o) {
if (o.createTextRange) 
	{
	o.caretPos = document.selection.createRange().duplicate(); 
	}
} 


function resetCloseButton()
{
	if (document.all && (typeof closeButton != 'undefined')) {	// Internet Explorer
		closeButton.style.left = (document.body.clientWidth) -10;
		closeButton.style.top = 0;
		closeButton.style.height = (document.body.clientHeight);
		closeButton.style.top = 0;
	} else if (document.layers && typeof document.closeButton != 'undefined') {	// Netscape
		document.closeButton.left = (windwo.innerWidth) -10;
		document.closeButton.top = 0;
		document.closeButton.height = (window.innerHeight);
		document.closeButton.top = 0;
	}
	setTimeout('resetCloseButton',100);
}

var lasty
var origy = 100


function resetTextAdder()
{var o
	if (document.all && (typeof textAdder != 'undefined')) {	// Internet Explorer
		o = textAdder.style;	
		b = textAdderMinimized.style	
	} else 
		if (document.layers && (typeof document.textAdder != 'undefined')) {	// Netscape
		o =document.textAdder;
		b =document.textAdderMinimized;
		}
		else
			return
	if (lasty + '' == 'undefined')
		{
		lasty = origy;}
var divider = 10

//	alert("lasty = " + lasty);
//	alert("top = " + document.body.scrollTop);

var newTop = document.body.scrollTop + 20
var bTop = document.body.scrollTop
	if (Math.round(lasty/divider) == Math.round(newTop/divider))
	{setTimeout('resetTextAdder()',50);	return;}
	if (Math.round(lasty/divider) > Math.round(newTop/divider))
		{lasty = lasty - divider;}
	else
		{lasty = lasty + divider;}
	if ((origy > lasty))
		{lasty = origy;}
	o.top = lasty
	b.top = bTop
	setTimeout('resetTextAdder()',5);
}

var textAdderVisible = true

function toggleTextAdder()
{var o
	if (document.all && (typeof textAdder != 'undefined')) {	// Internet Explorer
		o = textAdder.style;		
	} else 
		if (document.layers && (typeof document.textAdder != 'undefined')) {	// Netscape
		o =document.textAdder;
		}
		else
			return
	if (textAdderVisible)
		{textAdderVisible = false;
		o.visibility = "hidden";}
	else
		{textAdderVisible = true;
		o.visibility = "visible";}
	
}

function hideTextAdder()
{var o,b
	if (document.all && (typeof textAdder != 'undefined')) {	// Internet Explorer
		o = textAdder.style;
		b = textAdderMinimized.style	
	} else 
		if (document.layers && (typeof document.textAdder != 'undefined')) {	// Netscape
		o =document.textAdder;
		b =document.textAdderMinimized;
		}
		else
			return
	textAdderVisible = false;
	o.visibility = "hidden";
	b.visibility = "visible";
}

function showTextAdder()
{var o,b
	if (document.all && (typeof textAdder != 'undefined')) {	// Internet Explorer
		o = textAdder.style;		
		b = textAdderMinimized.style	
	} else 
		if (document.layers && (typeof document.textAdder != 'undefined')) {	// Netscape
		o =document.textAdder;
		b =document.textAdderMinimized;
		}
		else
			return
	textAdderVisible = true;
	o.visibility = "visible";
	b.visibility = "hidden";
	
}


function makeUnclosable()
{
}

function disableOtherForms(strForm)
{
var i;
for (i in arrForms)
	{
	if (arrForms[i] != strForm)
		{
		eval("disableForm(document." +  arrForms[i] + ")");
		}
	}
}

function disableForm(objForm)
{
if (objForm)
	{
	var i;
	var limit = objForm.elements.length;
	for (i=0;i<limit;i++) {
	objForm.elements[i].disabled = true;
	}
}

}

function checkAllGlobals(chkBox)
{
var objForm = chkBox.form
var i;
var limit = objForm.elements.length;
for (i=0;i<limit;i++) {
	if (objForm.elements[i].type == 'checkbox' && objForm.elements[i] != chkBox)
		{
		objForm.elements[i].checked = true;
		}
	}
}

function unCheckAllGlobals(chkBox)
{
var objForm = chkBox.form
var i;
var limit = objForm.elements.length;
for (i=0;i<limit;i++) {
	if (objForm.elements[i].type == 'checkbox' && objForm.elements[i] != chkBox)
		{
		objForm.elements[i].checked = false;
		}
	}
}

function disableAllLinks(objLink)
{
reallyDisableAllLinks(objLink)
}

function reallyDisableAllLinks(objLink)
{
var i;
var x;
var objForm;
var limit;
var objItem;
for (i in arrForms)
	{
		objForm = eval("document." +  arrForms[i])
		limit = objForm.elements.length;
		for (i=0;i<limit;i++) {
			objItem = objForm.elements[i]
			//alert("The object Type = " + objItem.type);
			if ((String(objItem.type) == 'submit') && (objItem != objLink))
				{objItem.disabled = true;}
		}
	}
//The code below was a start on disabling all the a hrefs
//it did not disable them
/*
limit = document.links.length;
for (i=0;i<limit;i++) {
			objItem = document.links[i]
			if ((objItem != objLink))
				{
				objItem.disabled = true;
				objItem.onClick = false;
				}
		}
*/
}
function pushSelectBox(str)
{
//alert('Pushing a select Box');
var arrTemp
arrTemp = new Array(str)
//alert('Before Add');
//printArray(arrSelects);
arrSelects = arrSelects.concat(arrTemp)
//alert('AfterAdd');
//printArray(arrSelects)
}

function printArray(ar)
{
for (i in ar) {
		alert(ar[i]);
		}
}

function selectAllSelects() {
    var limit;
    var i;
    var o;
    limit = arrSelects.length;
    for (i in arrSelects) {
        o = eval(arrSelects[i])
        if (o.multiple) {
            if (o.name != 'SPECIAL_CUSTOMER') {
                eval("selectAllOptions(" + arrSelects[i] + ")");
            }
        }
    }
}

function escapeAllSelects()
{
var limit;
	var i;
	var o;
	limit = arrSelects.length;
	for (i in arrSelects) {
		o = eval(arrSelects[i])
		if (o.multiple)
			{
			eval("escapeAllOptions(" + arrSelects[i] + ")");
			}
		}
}

function validateDocSubmission(objForm)
{
if (objForm.WF.value == "NONE")
	{alert("Select a WorkFlow!!");return(false)}
return(true);
}

function ConfirmButton(strConfirmString)
{
if (!confirm(strConfirmString))
	{return false}
else
	{return true}
}

function feedback(strFilePath)
{
	myWindowHandle = window.open(strFilePath + '?NUM=' + document.FrmFeedBack.NUM.value,
	'Subform','width=550,height=300,location=yes,toolbar=no,resizable=yes,scrollbars=yes');
}

function openUpdateWindow()
{
myUpdateWindow = window.open('','UpdateRecordWindow','width=300,height=100,location=no,toolbar=no,resizable=yes,scrollbars=no')
}

function closeUpdateWindow()
{
myUpdateWindow.close()
}


//function CheckEquality(string1,string2)
//This checks to see if two strings are the same
function CheckEquality(str1,str2,errmsg)
{
if (str1 != str2)
	{
	alert(errmsg);
	return false;
	}
else
	{
	return true;
	}
}

//AddPersonSubmit
//Does all the validation checks for adding a person and then sets up the fields for processing
function AddPersonSubmit(addForm,strPassDifErrMsg,strReqFieldMsg,reqFields)
{
var reply;
reply = true;  //starts out as true

//check the required fields
var strReq;
var arrReqFields;
var myField;
strReq = new String(reqFields);
arrReqFields = strReq.split(",");
for (myField in strReq.split(","))
	{
	if (eval("addForm." + arrReqFields[myField] + ".value == \"\""))
		{
		alert(strReqFieldMsg + " " + arrReqFields[myField]);
		eval("addForm." + arrReqFields[myField] + ".focus()");
		reply = false;
		return false;
		}
	}

//check the passwords to make sure they are the same
if (addForm.PASSWORD.value != addForm.PASSWORD2.value)
	{
	alert(strPassDifErrMsg);
	addForm.PASSWORD.value = "";
	reply = false;
	addForm.PASSWORD.focus();
	}

//clear the second password because we do not want it to show up in the insert query
addForm.PASSWORD2.value = "";

if(reply)
	{
	addForm.redirectTo.value = addForm.redirectTo.value + addForm.PrimaryKeyValue.value
	}
return reply;
}



//function FindABoss
function FindABoss(strPath)
{

BossWindow = window.open(strPath,'BossWindow','width=350,height=700,location=no,toolbar=no,resizable=yes,scrollbars=yes')	
}


function PopWindow(strPath,intWidth,intHeight,strName)
{
var w,h,n
if (intWidth){w = intWidth;}else{w = 300;}
if (intHeight){h = intHeight;}else{h = 300;}
if(PopUpWindow){PopUpWindow.close}
PopUpWindow = null;
if (String(strName) != 'undefined') {n = strName} else {n = 'PopUpWindow'}
PopUpWindow = window.open(strPath,n,'width=' + w + ',height=' + h + ',menubar=yes,location=yes,toolbar=yes,resizable=yes,scrollbars=yes,status=yes')	
//arrPopUps.push(PopUpWindow)
arrPopUps = arrPopUps.concat(new Array(PopUpWindow));
}


//function FindARole
function FindARole(strPath)
{
RoleWindow = window.open(strPath,'RoleWindow','width=300,height=600,location=no,toolbar=no,resizable=yes,scrollbars=yes')	
}


//function InsertPerson(strForm,strField,strMultiple,strValue)
//Inserts the ID of the person into the parent window in the form and field specified
function InsertPerson(strForm,strField,strMultiple,strValue,strShow)
{
	if (strMultiple == "false")
		{
		eval("window.opener.document." + strForm + "." + strField + ".value='" + strValue + "'");
		window.close();
		}
	else
		{
		//alert("EVAL = " + "window.opener.AddToOptionBox('" + unescape(strForm) + "','" + unescape(strField) + "','" + unescape(strValue) + "','" + unescape(strShow) + "')");
		eval("window.opener.AddToOptionBox('" + unescape(strForm) + "','" + unescape(strField) + "','" + unescape(strValue) + "','" + unescape(strShow) + "')");
		}
}

function displayAdding(strVal)
{
alert('Adding ' + strVal);
if (document.all && (typeof selectNote != 'undefined')) {	// Internet Explorer
	alert('IE');
	o = selectNote.style;
	o.innerHTML = strVal + ' Added'
	} else 
		if (document.layers && (typeof document.selectNote != 'undefined')) {	// Netscape
		o =document.selectNote;
		o.innerHTML = strVal + ' Added'
		}

}


function fillTextBox(strForm,strField,strVal)
{
o = eval("document." + strForm + "." + escape(strField));
o.value = strVal
}




function AddToOptionBox(strForm,strField,strVal,strShow,mult)
{
disableOtherForms(strForm);
ClearBlankOptions(strForm,strField)
var o,oNew,strS
strS = new String(strShow);
strS = strS.replace(/---/g,"'");
o = eval("document." + strForm + "." + strField);
if (mult == "")
	{
	selectAllOptions(o)
	delOptions(o)
	} 
	
deselectAllOptions(o)
for (var i=0; i<o.options.length; i++) {
	if (o.options[i].value == strVal)
		{
		o.options[i].selected = true;
		return
		}
	}

	oNew = new Option( strS, strVal, false, false);
	o.options[o.options.length] = oNew;
	o.options[o.options.length-1].selected = true;
}

function ClearBlankOptions(strForm,strField)
{
var o,i
o = eval("document." + strForm + "." + strField);
for (var i=0; i<o.options.length; i++) {
	if (o.options[i].value == "")
		{
		o.options[i] = null;
		return
		}
	}

}


//function addRoleSubmit
//Does a little post processing for the role submit
function addRoleSubmit(objForm,strReqFieldMsg,reqFields)
{
var boolGo
objForm.redirectTo.value = objForm.redirectOriginal.value + objForm.PrimaryKeyValue.value;
boolGo = checkRequiredFields(objForm,strReqFieldMsg,reqFields)
return boolGo;
}


function checkRequiredFields(objForm,strReqFieldMsg,reqFields)
{
var strReq;
var arrReqFields;
var myField;
strReq = new String(reqFields);
arrReqFields = strReq.split(",");
for (myField in strReq.split(","))
	{
	if (eval("objForm." + arrReqFields[myField] + ".value == \"\""))
		{
		alert(strReqFieldMsg);
		eval("objForm." + arrReqFields[myField] + ".focus()");
		reply = false;
		return false;
		}
	}
}

function deleteProduct(objForm,strCon)
{
if (ConfirmButton(strCon))
	{objForm.submit();}
}

function insertDate(strDate,strField,strForm) {
	var execstr
	execstr = "document." + strForm + "." + strField + ".value = '" + strDate + "'";
	
	eval(execstr);
}

function validateEditTaskForm(objF,strClosingString,strErrMsg1,strErrMsg2,strMsg3) {
var boolOK
	boolOK = true;
	if (objF.isNew.value != "1"){
		if (objF.ACTUAL_STOP.value != '') 
			{boolOK = ConfirmButton(strClosingString);}
		}
	else
		{
		if ((objF.AssignedRole.value != "") && (objF.AssignedPerson.value != ""))
			{alert(strMsg3);return(false);}
		if ((objF.REQUESTOR.value == ""))
			{alert(strErrMsg1);return(false);}
		
		}
return (boolOK);
}



var mp3Player
function launchPlayer(mp3File) { 
	mp3PlayerExists = (mp3Player!=null)
    if (mp3PlayerExists && !mp3Player.closed) {mp3Player.focus()}
    mp3Player = window.open("/" + strRootName + "/mp3player.htm?" + mp3File,"mp3Player","height=10,width=250,status=no,toolbar=no,menubar=no,location=no")
}

function closePlayer() {
   	mp3PlayerExists = (mp3Player!=null)
    if (mp3PlayerExists && !mp3Player.closed) {mp3Player.close()}
}


function forwardTaskCheck(o,strMsg1,strMsg2,strRootName)
{
escapeAllSelects(o);
var boolRoleForward = false;
var boolAssForward = false;
if ((o.TASK_ASSIGNED_ROLE_ID.value != getSelectedValue(o.AssignedRole)))
{
	if ((getSelectedValue(o.AssignedRole) == 'NOTHING'))
		{}
	else
		{boolRoleForward = true}
}
if ((o.TASK_ASSIGNEE_ID.value != getSelectedValue(o.AssignedPerson)))
{
	if ((getSelectedValue(o.AssignedPerson) == 'NOTHING'))
		{}
	else
		{boolAssForward = true}
}
if (boolAssForward && boolRoleForward)
	{
		alert(strMsg1);
		return false;
	}
if (boolAssForward)
	{
		document.location = ("" + strRootName + "asp/ActualTasks/forwardTask.asp?ID=" + o.TASK_ID.value + "&assPer=" + getSelectedValue(o.AssignedPerson))
	}
if (boolAssForward && boolRoleForward)
	{
		document.location = ("" + strRootName + "asp/ActualTasks/forwardTask.asp?ID=" + o.TASK_ID.value + "&assRole=" + getSelectedValue(o.AssignedRole))
	}

if (!(boolAssForward) && !(boolRoleForward))
	{
		alert(strMsg2);	
	}
}



function insertCurDate(o,strFormat)
{
var d = new Date();
var newDate = new String(pad(d.getMonth()+1,1,"0") + "/" + pad(d.getDate(),1,"0") + "/" + d.getFullYear() + " " + ((d.getHours() + 1) % 24) + ":00")
if (strFormat == 'dateOnly') {newDate = pad(d.getMonth()+1,1,"0") + "/" + pad(d.getDate(),1,"0") + "/" + d.getFullYear()}
o.value = newDate
}

function pad(v,n,c)
{
var s = new String(v)
var p = new String(c)
if (s.length < n)
	{
	so = pad(p + v,n - p.length,c)
	}
else
	{
	so = v;
	}
return (so);	
}


var NS4 = (document.layers);
var IE4 = (document.all);
var win = this;
var n   = 0;

function findInPage(str) {
	var txt, i, found;
	if (str == "")
		return false;
	if (NS4) {
		if (!win.find(str))
			while(win.find(str, false, true))
		n++;
		else
		n++;
		if (n == 0) alert(str + " was not found on this page.");
	}
	
	if (IE4) {
		txt = win.document.body.createTextRange();
		for (i = 0; i <= n && (found = txt.findText(str)) != false; i++) {
			txt.moveStart("character", 1);
			txt.moveEnd("textedit");
		}
		if (found) {
			txt.moveStart("character", -1);
			txt.findText(str);
			txt.select();
			txt.scrollIntoView();
			n++;
		}
		else 
			{
			if (n > 0) {
			n = 0;
			findInPage(str);
			}
		else
			alert(str + " was not found on this page.");
		}
	}
	return false;
}
	
function validatePersonForm(o,strPassWordErrorMsg,strErrMsg,strPasswordNeeded)
{
var strPass
if (o.PASSWORD)
	{
	if (o.PASSWORD.value != "")
		{
		if (o.PASSWORD.value != o.PASSWORD2.value)
			{
			alert('Jscript Error 0001' + strPassWordErrorMsg);
			o.PASSWORD.focus();
			o.PASSWORD.select();
			return(false);
			}
		}
	else
		{
		if (o.isNew.value == '1')
			{
			alert('Jscript Error 0002 ' + strPassWordErrorMsg);
			o.PASSWORD.focus();
			o.PASSWORD.select();
			return(false);			
			}
		}
	}
if (o.LOGIN)
	{
	if (o.LOGIN.value != o.LOGIN_default.value)
		{
		if (o.isBoss)
		if (o.isBoss.value != "true")
			{
			if (o.oldPASSWORD)
				{
				if (o.oldPASSWORD.value == "")
					{
					alert('Jscript Error 0003' + strPasswordNeeded);
					o.oldPASSWORD.focus();
					o.oldPASSWORD.select();
					return(false);
					}
				}
			}
		}	
	
	}
return(true);
}

function clearGroupRequestee()
{
clearSelectBox(document.editForm.GROUP_REQUESTEE_ID)
}
function clearRequestee()
{
clearSelectBox(document.editForm.REQUESTEE_ID)
}

function popUpNavTo(strPage,objBox,strID,strNothingExists)
{
	var strMyID,strMyPage,strVal
	strMyID = new String(strID)
	strMyPage = new String(strPage)
	strVal = new String(getSelectedValue(objBox))
	if (strMyID == 'NOTHING'){alert(strNothingExists);return}
	if (strMyPage == 'NOTHING'){alert(strNothingExists);return}
	if ((strVal == 'NOTHING') || (strVal =='')){alert(strNothingExists);return}
	//alert("document.location = " + strMyPage + "&" + strMyID + "=" + strVal);
	var popWin
	popWin = window.open(strPage + "&" + strID + "=" + getSelectedValue(objBox),
	'popUpViewer','width=800,height=500,location=no,toolbar=yes,resizable=yes,scrollbars=yes');
	//document.location = strPage + "&" + strID + "=" + getSelectedValue(objBox);
}

function disablePopUp(strFormName,strForumName,strPopUpName,strID)
{
var objP,oEl,objF
objF = eval("document." + strFormName + "." + strForumName);
objP = eval("document." + strFormName + "." + strPopUpName);
if (objF.options[objF.selectedIndex].value == "PUBLIC")
	{
	oEl = document.getElementById(strFormName + "_" + strPopUpName + "_Add")
	oEl.style.visibility = "hidden";
	oEl = document.getElementById(strFormName + "_" + strPopUpName + "_Remove")
	oEl.style.visibility = "hidden";
	selectAllOptions(objP)
	delOptions(objP)
	}
else
	{
	oEl = document.getElementById(strFormName + "_" + strPopUpName + "_Add")
	oEl.style.visibility = "visible";
	oEl = document.getElementById(strFormName + "_" + strPopUpName + "_Remove")
	oEl.style.visibility = "visible";	}
	
}



var isIE = false,isNav = false
if (document.layers) { // browser sniffer
	isNav = true
} else if (document.all) {
	isIE = true
}

//Below is the favorites form functions for the action plans.
//first we need to monitor the visibility of the form
var favFormVisible = false


var mouseX, mouseY;

function getMousePos(e)
{
if (!e)
var e = window.event||window.Event;

if('undefined'!=typeof e.pageX)
{
mouseX = e.pageX;
mouseY = e.pageY;
}
else
{
mouseX = e.clientX + document.body.scrollLeft;
mouseY = e.clientY + document.body.scrollTop;
}
}

// You need to tell Mozilla to start listening:

if(window.Event && document.captureEvents)
document.captureEvents(Event.MOUSEMOVE);

// Then assign the mouse handler

document.onmousemove = getMousePos;

// Then your mouseover function can just read mouseX and mouseY directly.



//function addToFavorites(strID)
//This function will move the addToFavorites box from its hidden location 
//to the same location as the cursor and make it visible until the person moves 
//the cursor more than 5 pixwls off of it.
//The user will be able to submit the form that is in this hidden layer.
function addToFavorites(strID,strType)
{
document.actionPlanFavoriteForm.favType.value = strType
document.actionPlanFavoriteForm.ID.value = strID
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = favForm.style;		
	} else 
		if (isNav) {	// Netscape
		o =document.favForm;
		}
		else
			return
			
//find out where the cursor is right now
var myX,myY
  myX = mouseY;
  myY = mouseX;
  
//put the favForm under the cursor
if (myY < 200)
	{o.left = myY}
else
	{o.left = myY-200}
	
o.top = myX;
o.visibility = "visible";

}

//close the favorites window
function closeFavWindow()
{
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = favForm.style;		
	} else 
		if (isNav) {	// Netscape
		o =document.favForm;
		}
		else
			return
			
o.top = 0;
o.left = 0;
o.visibility = "hidden";
}

//function getNewGroup
//This will get a new favorite group and add it to the list
//It will also select the new group
function getNewGroup(o)
{
if (o.value != "NEW_FAV_GROUP")
	{
	return true
	}
var newGroupName
newGroupName = prompt("Enter New Favorite Group Name","");
if (!(newGroupName)){return(false);}
if (newGroupName != "")
	{
	AddToOptBox(o,"NEW_FAV_GROUP",newGroupName);
	selectOnlyMatchingOptions(o,newGroupName)
	document.actionPlanFavoriteForm.newGroupName.value = newGroupName;
	return true;
	}
else
	{
	return false
	}
}


//function getNewGroup
//This will get a new favorite group and add it to the list
//It will also select the new group
function textListAddToMyList(o)
{
if (o.value != "Add_New")
	{
	return true
	}
var newGroupName
newGroupName = prompt("Enter new item","");
if (!(newGroupName)){	dropDownChangeTo(o,'');return(false);}
if (newGroupName != "")
	{
	clearSelectBox(o);
	AddToOptBox(o,newGroupName,newGroupName);
	selectOnlyMatchingOptions(o,newGroupName)
	return true;
	}
else
	{
	return false
	}
}




/*
function showProperUnits(oForm)
input: the form we are editing
actions: if multiple choice is selected then disable all the items in the form except 
description and hide target.
anything else selected then enable everything
*/
function showProperUnits(oForm)
{
var j; 
if (oForm.UNITS.value == "KPI_MULTIPLE_CHOICE")
	{
	var arrItems = new Array('LOW_FAILURE','LOW_THRESHOLD','TARGET','UP_THRESHOLD','UP_ESCALATION');
	for (j = 0; j < arrItems.length; j++) 
		{
		eval("oForm." + arrItems[j] + ".value = ''");
		eval("oForm." + arrItems[j] + ".disabled = true");
		}
	oForm.SHOULD_BE.value = "KPI_EQUAL"
	oForm.SHOULD_BE.disabled = true;
	oForm.OPINION.value = "0"
	oForm.OPINION.disabled = true;
	oForm.HIDE_TARGET.value = "1"
	oForm.AddChoices.disabled = false;
	}
else
	{
	var arrItems = new Array('LOW_FAILURE','LOW_THRESHOLD','TARGET','UP_THRESHOLD','UP_ESCALATION');
	for (j = 0; j < arrItems.length; j++) 
		{
		eval("oForm." + arrItems[j] + ".disabled = false");
		}
	oForm.SHOULD_BE.disabled = false;
	oForm.OPINION.disabled = false;
	oForm.AddChoices.disabled = true;
	}
}

function checkKPIMultiple()
{
var o = document.KPIMultAddForm
var i;
var boolGotAns = false;
var limit = o.elements.length;
for (i=0;i<limit;i++) 
	{
	if (o.elements[i].name.indexOf("IS_ANSWER___") == 0)
		{
		if (o.elements[i].name.indexOf("___NEW") == -1)
			if (o.elements[i].value == 1) 
				boolGotAns=!(boolGotAns);
		}
	}
if (boolGotAns)
	{
	o.doneButton.disabled = false;
	}
}

function setKPIShouldBe(o)
{
switch(o.SHOULD_BE.value)
	{
	case "KPI_EQUAL":
		o.LOW_FAILURE.disabled = false;
		o.LOW_THRESHOLD.disabled = false;
		o.UP_ESCALATION.disabled = false;
		o.UP_THRESHOLD.disabled = false;
		o.TARGET.disabled = false;
		break;
	case "KPI_BELOW":
		o.LOW_FAILURE.value = '';
		o.LOW_THRESHOLD.value = '';
		o.LOW_FAILURE.disabled = true;
		o.LOW_THRESHOLD.disabled = true;
		o.UP_ESCALATION.disabled = false;
		o.UP_THRESHOLD.disabled = false;
		o.TARGET.disabled = false;
		break;
	case "KPI_ABOVE":
		o.LOW_FAILURE.disabled = false;
		o.LOW_THRESHOLD.disabled = false;
		o.UP_ESCALATION.disabled = true;
		o.UP_THRESHOLD.disabled = true;
		o.UP_ESCALATION.value = '';
		o.UP_THRESHOLD.value = '';
		o.TARGET.disabled = false;
		break;
	case "KPI_BETWEEN":
		o.LOW_FAILURE.disabled = false;
		o.LOW_THRESHOLD.disabled = false;
		o.UP_ESCALATION.disabled = false;
		o.UP_THRESHOLD.disabled = false;
		o.TARGET.disabled = false;
		break;
	
	}
}




//function openPopForm(strFormID,xOffset,yOffset)
//This function will move the div with the strFormID to the current mouse location
//and make it visible until the person chooses something or clicks hide
function openPopForm(strFormID,xOffset,yOffset)
{
var meForm
//oEl = document.getElementById('document_' + o.name + "_" + arrFields[i])
meForm = document.getElementById(strFormID)
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = meForm.style;		
	} else 
		if (isNav) {	// Netscape
		o =document.meForm;
		}
		else
			return
			
//find out where the cursor is right now
var myX,myY
  myX = mouseY;
  myY = mouseX;
  
//put the favForm under the cursor
o.top = myX + xOffset;
o.left = myY + yOffset;
o.width = 0;
o.visibility = "visible";
}

//close the favorites window
function closePopForm(strFormID)
{
var meForm
//oEl = document.getElementById('document_' + o.name + "_" + arrFields[i])
meForm = document.getElementById(strFormID)
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = meForm.style;		
	} else 
		if (isNav) {	// Netscape
		o = document.meForm;
		}
		else
			return
			
o.top = 0;
o.left = 0;
o.visibility = "hidden";
}


//function MakeDivVisible(strFormID)
//This function will move the div with the strFormID to the current mouse location
//and make it visible until the person chooses something or clicks hide
function makeDivVisible(strFormID)
{
var meForm
meForm = document.getElementById(strFormID)
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = meForm.style;		
	} else 
		if (isNav) {	// Netscape
		o =document.meForm;
		}
		else
			return
o.visibility = "visible";
}

//close the favorites window
function makeDivHidden(strFormID)
{
var meForm
meForm = document.getElementById(strFormID)
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = meForm.style;		
	} else 
		if (isNav) {	// Netscape
		o = document.meForm;
		}
		else
			return
o.visibility = "hidden";
}


var strOpenForms = new String();
var strCloseForms = new String();

function addToShowList(strFormID)
{
if (strOpenForms.length == 0) {strOpenForms = strOpenForms.concat(strFormID + ',');}
else
	{
	if (strOpenForms.match(strFormID + ',')){}
	else {strOpenForms = strOpenForms.concat(strFormID + ',');}
	}
removeFromCloseList(strFormID);
setTimeout('hideAndShowDivs()',1);
}

function removeFromShowList(strFormID)
{
strOpenForms = strOpenForms.replace(strFormID + ',','');
}

function addToCloseList(strFormID)
{
if (strCloseForms.length == 0) {strCloseForms = strCloseForms.concat(strFormID + ',');}
else
	{
	if ((strCloseForms.match(strFormID + ','))){}
	else {strCloseForms = strCloseForms.concat(strFormID + ',');}
	}
removeFromShowList(strFormID);
setTimeout('hideAndShowDivs()',1000);
}

function removeFromCloseList(strFormID)
{
strCloseForms = strCloseForms.replace(strFormID + ',','');
}

function hideAndShowDivs()
{
var i
for (i in strOpenForms.split(','))
	{
	if (strOpenForms.split(',')[i].length > 0){showDiv(strOpenForms.split(',')[i]);}
	}
for (i in strCloseForms.split(','))
	{
	if (strCloseForms.split(',')[i].length > 0){hideDiv(strCloseForms.split(',')[i]);}
	}

}


function showDiv(s)
{
	toggleDivVisibility(s)
}

function delayedHideDiv(strVar,strEl)
{
setTimeout('dHide2(\'' + strVar + '\',\'' + strEl + '\')',5000)

}

function dHide2(strVar,strEl)
{
if (eval(strVar))
	{
	hideDiv(strEl)
	}
}



function hideDiv(s)
{
var meForm
meForm = document.getElementById(s)
if (meForm){
//Point to the right style object
var o
	if (isIE) {	// Internet Explorer
		o = meForm.style;		
	} else 
		if (isNav) {	// Netscape
		o =document.meForm;
		}
		else
			return
o.display = "none";
}
}


//function toggleDivVisibility(strFormID)
function toggleDivVisibility(strFormID)
{
var meForm
meForm = document.getElementById(strFormID)
if (meForm.style.display == 'none')
	{meForm.style.display='block';}
else
	{meForm.style.display='none';}
}

function checkOpenForms(strFormID)
{
if (strOpenForms.length == 0) {strOpenForms = strOpenForms.concat(strFormID + ',');return(false)}
if (strOpenForms.match(strFormID)){strOpenForms = strOpenForms.replace(strFormID,'');return(true);}
else{strOpenForms = strOpenForms.concat(strFormID + ',');return(false);}
}



function resetSelectClose()
{var o
	if (document.all && (typeof selectClose != 'undefined')) {	// Internet Explorer
		o = selectClose.style;	
	} else 
		if (document.layers && (typeof document.selectClose != 'undefined')) {	// Netscape
		o =document.selectClose;
		}
		else
			return
	if (lasty + '' == 'undefined')
		{
		lasty = origy;}
var divider = 10

//	alert("lasty = " + lasty);
//	alert("top = " + document.body.scrollTop);

var newTop = document.body.scrollTop + 20
	if (Math.round(lasty/divider) == Math.round(newTop/divider))
	{setTimeout('resetSelectClose()',50);	return;}
	if (Math.round(lasty/divider) > Math.round(newTop/divider))
		{lasty = lasty - divider;}
	else
		{lasty = lasty + divider;}
	if ((origy > lasty))
		{lasty = origy;}
	o.top = lasty
	setTimeout('resetSelectClose()',5);
}

function verifyHours2(cnt,t)
{
if (t.value == '') {t.value='0'}
var f = document.editForm
var n = eval('document.editForm.NORMAL_HOURS_' + String(cnt))
var int1,int2
if (String(Number(n.value))=='NaN')
	{n.value = 0;alert(document.editForm.NAN_msg.value);t.value = '0';}
int1 = Number(n.value)
if ((int1) > 24)
	{alert(document.editForm.hours24ErrMsg.value);t.value = '0';}
else
	{n.value = String(int1);}
}

function verifyHours(cnt,t)
{
if (t.value == '') {t.value='0'}
var f = document.editForm
var n = eval('document.editForm.NORMAL_HOURS_' + String(cnt))
var o = eval('document.editForm.OVER_HOURS_' + String(cnt))
var int1,int2
if (String(Number(o.value))=='NaN')
	{o.value = 0;alert(document.editForm.NAN_msg.value);t.value = '0';}
if (String(Number(n.value))=='NaN')
	{n.value = 0;alert(document.editForm.NAN_msg.value);t.value = '0';}
int1 = Number(n.value)
int2 = Number(o.value)
int1 = int1 * 100
int2 = int2 * 100
int1 = Math.round(int1)
int2 = Math.round(int2)
while ((int1 % 25) > 0)
	{
	int1--;
	}
while ((int2 % 25) > 0)
	{
	int2--;
	}
int1 = int1/100
int2 = int2/100

if ((int1 + int2) > 24)
	{alert(document.editForm.hours24ErrMsg.value);t.value = '0';}
else
	{n.value = String(int1);
	o.value = String(int2)}
}


function massHideDivs(strName,intC)
{
var x
for (x=1;x<=intC;x++)
	{
	hideTogDiv(strName + '_' + String(x));
	}

}

function massShowDivs(strName,intC)
{
var x
for (x=1;x<=intC;x++)
	{
	showTogDiv(strName + '_' + String(x));
	}
}

function setDayOfWeekBox(strForm,strDateBox)
{
var o,d,od;
o = eval('document.' + strForm + '.' + strDateBox)
d = eval('document.' + strForm + '.' + strDateBox + '_dayOfWeek')
od = new Date(o.value)
if (od.getDay() == 0) {d.value = 'Sunday'}
if (od.getDay() == 1) {d.value = 'Monday'}
if (od.getDay() == 2) {d.value = 'Tuesday'}
if (od.getDay() == 3) {d.value = 'Wednesday'}
if (od.getDay() == 4) {d.value = 'Thursday'}
if (od.getDay() == 5) {d.value = 'Friday'}
if (od.getDay() == 6) {d.value = 'Saturday'}
}


function saveSearch(strSearchName,strPageID,strQstring,strPathToTop,strNTLogin)
{
var sql
sql = 'INSERT INTO A_SEARCHES_SAVED (ID,SEARCH_NAME,PAGE_ID,QS,DRCM,PERSON_ID) VALUES (newID(),'
sql += '\'' + strSearchName + '\','
sql += '\'' + strPageID + '\','
sql += '\'' + strQstring + '\','
sql += 'getDate(),'
sql += '\'' + strNTLogin + '\')'
ajaxSave(strPathToTop,sql);
document.saveSearches.searchName.value=''
toggleDivVisibility('saveSearchBox');
}

function deleteSavedSearch(strID,strPathToTop)
{
var sql
sql = 'DELETE FROM A_SEARCHES_SAVED WHERE ID = \'' + strID + '\''
ajaxSave(strPathToTop,sql);
}

function divHide(strID)
{
var o
o = document.getElementById(strID)
if (!document.getElementById(strID)){alert(strID + ' not found!');}
o.style.display="none";
}
				
function divShow(strID)
{
var o
o = document.getElementById(strID)
if (!document.getElementById(strID)){alert(strID + ' not found!');}
o.style.display="block";
}

function getElementPosition(elemID){
var offsetTrail = document.getElementById(elemID);
var offsetLeft = 0;
var offsetTop = 0;
while (offsetTrail){
offsetLeft += offsetTrail.offsetLeft;
offsetTop += offsetTrail.offsetTop;
offsetTrail = offsetTrail.offsetParent;
}
if (navigator.userAgent.indexOf('Mac') != -1 && typeof document.body.leftMargin != 'undefined'){
offsetLeft += document.body.leftMargin;
offsetTop += document.body.topMargin;
}
return {left:offsetLeft,top:offsetTop};
}				

function coverDIV(bottomDiv,topDiv)
{
var o = document.getElementById(bottomDiv);
}

function refreshWorkerScreenProgress()
{
refreshProcFrame()
waitCurStepFrame()
}
function waitCurStepFrame()
{
var sURL = '/ANSWERALPHA/asp/workerscreen/CurStepData.asp?wait=true'
if (parent.curStep)
	{parent.curStep.location = sURL;}
if (parent.parent.curStep)
	{parent.parent.curStep.location = sURL;}
}

function refreshProcFrame()
{
if (parent.document.taskForm)
	{parent.document.taskForm.REFRESH_BUTTONS.click();}
else
	if (parent.parent.document.taskForm)
		{parent.parent.document.taskForm.REFRESH_BUTTONS.click();}
		else
			if (parent.parent.parent.document.taskForm)
				{parent.parent.parent.document.taskForm.REFRESH_BUTTONS.click();}
	
}

function refreshCurStepFrame(strFillID)
{
var sURL = '/ANSWERALPHA/asp/workerscreen/CurStepData.asp?fillID=' + escape(strFillID)
if (parent.curStep)
	{parent.curStep.location = sURL;}
if (parent.parent.curStep)
	{parent.parent.curStep.location = sURL;}
	
}

function noenter() {
  return !(window.event && window.event.keyCode == 13); }

//################################################################################
//## function printReport
//################################################################################
function printReport(oForm,jID,purchFillID,serial)
{
aStat('Printing Report for item = ' + purchFillID);
r = eval('oForm.printType___' + jID + '.value');
l = eval('oForm.labelType___' + jID + '.value');
col = eval('oForm.LABEL_COLUMN___' + jID + '.value');
row = eval('oForm.LABEL_ROW___' + jID + '.value');
aStat('Report Type =' + r);
strPath = '../actualTasks/viewTSR.asp?PRINTABLE=YES&TSR_TYPE=' + r + '&FILL_ID=' + purchFillID + '&SERIAL=' + serial + '&labelType=' + l + '&ROW=' + row + '&COL=' + col
PopWindow(strPath,800,600,'_blank')
}

//################################################################################
//## function showWFInfo(objID)
//################################################################################
function showWFInfo(objID)
{
divShow(objID + '___WF_DATA');
var vars
vars = 'objID=' + escape(objID) + '&'
sURL = '../utilities/ajax/xmlObjectGetWFData.asp'
var myXHC = new XHC()
myXHC.connect(sURL, 'POST', vars)
}

function handleReturnFromObjectGetWFData(xml,xmlText)
{
var objID 
objID = xml.getElementsByTagName('objID')[0].firstChild.data;
var aEls = xml.getElementsByTagName('O');
var i,aStatOut;
aStatOut = ''
for (i=1;i<=aEls.length;i++)
	{
	aStatOut += aEls[i-1].firstChild.data + '<br />'
	}
aStat(aStatOut);
document.getElementById(objID + '___WF_DATA').innerHTML = '<div>Workflow Information:<br />' + aStatOut + '</div>'
}



//'################################################################################
//'## function 
//'################################################################################
function setPerPageVal(intNum)
{
aStat('setting per page value to ' + intNum);
var vars;
vars = 'recordCount=' + escape(intNum)
//sURL = '/' + strRootName + '/answeralpha/asp/utilities/xmlSetSessionVariables.asp'
sURL = strRootName + '/answeralpha/asp/utilities/xmlSetSessionVariables.asp'
var myXHC = new XHC()
myXHC.connect(sURL, 'POST', vars)
}

//'################################################################################
//'## function handleReturnFromSetSessionVariable(xml,xmlText)
//'################################################################################
function handleReturnFromSetSessionVariable(xml,xmlText)
{
aEls = xml.getElementsByTagName('aStat');
var i,aStatOut;
aStatOut = ''
for (i=1;i<=aEls.length;i++)
	{
	aStatOut += aEls[i-1].firstChild.data + '<br />'
	}
aStatOut += 'Set the session variables.'
aStat(aStatOut);
window.location.reload(true);
}

//'################################################################################
//'## function attachFileToTask(strTask,strFileID);
//'################################################################################
function attachFileToTask(strTask,strFileID,strName)
{
divPutRedText(strName + '_STATUS_DIV','File Uploaded. Attaching to the task now...');
var vars;
vars = 'taskID=' + escape(strTask)
vars = vars + '&fileID=' + escape(strFileID)
vars = vars + '&rootName=' + escape(strName)
//sURL = '/' + strRootName + '/answeralpha/asp/actualTasks/ajax/attachFileToTask.asp'
sURL = strRootName + '/answeralpha/asp/actualTasks/ajax/attachFileToTask.asp'
var myXHC = new XHC()
myXHC.connect(sURL, 'POST', vars)
}

//'################################################################################
//'## function handleReturnFromAttachFileToTask(xml,xmlText)
//'################################################################################
function handleReturnFromAttachFileToTask(xml,xmlText)
{
aEls = xml.getElementsByTagName('aStat');
rootName = xml.getElementsByTagName('rootName')[0].firstChild.data;
divPutRedText(rootName + '_STATUS_DIV','File Attached, You may upload another.');
eval('document.' + rootName + '_FORM.' + rootName + '_SUBMIT_BUTTON.disabled=false')
eval('document.' + rootName + '_FORM.' + rootName + '_description.disabled=false')
eval('document.' + rootName + '_FORM.' + rootName + '_FILE_UPLOAD.disabled=false')
eval('document.' + rootName + '_FORM.' + rootName + '_description.value=""')
eval('document.' + rootName + '_FORM.' + rootName + '_FILE_UPLOAD.value=""')

var i,aStatOut;
aStatOut = ''
for (i=1;i<=aEls.length;i++)
	{
	aStatOut += aEls[i-1].firstChild.data + '<br />'
	}
//aStat(aStatOut);
}



//'################################################################################
//'## function attachFileToActualPart(strActPart,strFileID);
//'################################################################################
function attachFileToActualPart(strActPart,strFileID,strName)
{
divPutRedText(strName + '_STATUS_DIV','File Uploaded. Attaching to the actual part now...');
var vars;
vars = 'actPartID=' + escape(strActPart)
vars = vars + '&fileID=' + escape(strFileID)
vars = vars + '&rootName=' + escape(strName)
//sURL = '/' + strRootName + '/answeralpha/asp/actualParts/ajax/attachFileToActualPart.asp'
sURL = strRootName + '/answeralpha/asp/actualParts/ajax/attachFileToActualPart.asp'
var myXHC = new XHC()
myXHC.connect(sURL, 'POST', vars)
}

//'################################################################################
//'## function handleReturnFromSetSessionVariable(xml,xmlText)
//'################################################################################
function handleReturnFromAttachFileToActualPart(xml,xmlText)
{
aEls = xml.getElementsByTagName('aStat');
rootName = xml.getElementsByTagName('rootName')[0].firstChild.data;
divPutRedText(rootName + '_STATUS_DIV','File Attached, You may upload another.');
eval('document.' + rootName + '_FORM.' + rootName + '_SUBMIT_BUTTON.disabled=false')
eval('document.' + rootName + '_FORM.' + rootName + '_description.disabled=false')
eval('document.' + rootName + '_FORM.' + rootName + '_FILE_UPLOAD.disabled=false')
eval('document.' + rootName + '_FORM.' + rootName + '_description.value=""')
eval('document.' + rootName + '_FORM.' + rootName + '_FILE_UPLOAD.value=""')

var i,aStatOut;
aStatOut = ''
for (i=1;i<=aEls.length;i++)
	{
	aStatOut += aEls[i-1].firstChild.data + '<br />'
	}
//aStat(aStatOut);
}


//'################################################################################
//'## function attachFileToActualPart(strActPart,strFileID);
//'################################################################################
function createNewCustomerContact(strName,oForm,customerField)
{
var customerID
var formName = oForm.name
customerID = eval('document.' + formName + '.'  + customerField + '.value' )
if (customerID == '')
	{
	alert('You must choose a customer first');
	return
	}

aStat('Creating a new customer contact' + strName + ' form name = ' + formName);
var vars;
vars = 'name=' + escape(strName)
vars = vars + '&formName=' + escape(formName)
vars = vars + '&firstName=' + escape(eval('document.' + formName + '.'  + strName + '__FIRST_NAME.value' ))
vars = vars + '&lastName=' + escape(eval('document.' + formName + '.'  + strName + '__LAST_NAME.value' ))
vars = vars + '&phone=' + escape(eval('document.' + formName + '.'  + strName + '__PHONE.value' ))
vars = vars + '&customerID=' + escape(customerID)
aStat(vars)
sURL = '/' + strRootName + '/answeralpha/asp/customerContacts/ajax/XMLCreateCustomerContact.asp'
aStat(sURL)
var myXHC = new XHC()
myXHC.connect(sURL, 'POST', vars)
}

//'################################################################################
//'## function handleReturnFromSetSessionVariable(xml,xmlText)
//'################################################################################
function handleReturnFromCreateCustomerContact(xml,xmlText)
{
aEls = xml.getElementsByTagName('aStat');
var i,aStatOut;
aStatOut = ''
for (i=1;i<=aEls.length;i++)
	{
	aStatOut += aEls[i-1].firstChild.data + '<br />'
	}
aStat(aStatOut);
var oBox
oBox = eval('document.' + xml.getElementsByTagName('formName')[0].firstChild.data + '.' + xml.getElementsByTagName('name')[0].firstChild.data)
AddToOptBox(oBox,xml.getElementsByTagName('newID')[0].firstChild.data,xml.getElementsByTagName('displayName')[0].firstChild.data)
}


//'################################################################################
//'## function 
//'################################################################################
function saveFileDescription(o,o2)
{
var vars;
o.disabled = true;
o2.disabled = true;
vars = 'fileDescription=' + escape(o.value)
vars = vars + '&fileID=' + escape(o2.value)
aStat(vars)
sURL = '/' + strRootName + '/answeralpha/asp/actualParts/ajax/updateActualPartFileDescription.asp'
aStat(sURL)
var myXHC = new XHC()
myXHC.connect(sURL, 'POST', vars)
}

//'################################################################################
//'## function 
//'################################################################################
function handleReturnFromUpdateFileDescription(xml,xmlText) 
{
aEls = xml.getElementsByTagName('aStat');
var i,aStatOut;
aStatOut = ''
for (i=1;i<=aEls.length;i++)
	{
	aStatOut += aEls[i-1].firstChild.data + '<br />'
	}
aStat(aStatOut);
}


