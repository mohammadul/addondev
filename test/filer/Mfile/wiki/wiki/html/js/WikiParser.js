﻿/// <reference path="jquery-1.5.min.js"/>

/************************************************************
Copyright (C) 2004-2006 Masahiko SAWAI All Rights Reserved. 
************************************************************/


function WikiParser(document)
{
	this.document = document;
	this.stack = new Array();
	this.stack.top = function () {
	    return this.length > 0 ? this[this.length - 1] : null;
	};
	this.id = 0;
	//this.cf = {};
}

WikiParser.prototype.linkString = null;
WikiParser.prototype.cursorPosition = 0;
WikiParser.prototype.nodeAtCursorPosition = null;

WikiParser.prototype.parse = function (inputString, id) {
    this.id = id;
    this.cf = [];

    function createQElem(stack) {
        //var _requrl = requrl;
        var qelem = this.document.createElement('div');
        $(qelem).addClass('quote');

        var aelem = document.createElement("a");
        //$(aelem).attr('align','right');
        var script = "";
        aelem.href = "javascript:void(0)";
        aelem.appendChild(document.createTextNode("execute"));
        $(aelem).click(function (event) {
            //alert(requrl + "/script");
            //alert($(qelem).text());
            $.ajax({
                type: "POST",
                cache: false,
                async: false,
                dataType: "text",
                data: script,
                url: requrl + "/script",
                success: function (data) {
                    //alert("data = " + data);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("error textStatus = " + errorThrown);
                }
            });
            return false;
        });
        $(qelem).append(aelem);

        var preelem = this.document.createElement('pre');
        qelem.appendChild(preelem);
        var qtext = "";
        for (var k in stack) {
            script += stack[k] + "\n";
            qtext = stack[k];
            preelem.appendChild(this.document.createTextNode(qtext));
            preelem.appendChild(this.document.createElement('br'));
        }

        //preelem.appendChild(this.document.createTextNode(qtext));

        return qelem;
    }



    var re = RegExp("\r\n");
    while (re.test(inputString)) inputString = inputString.replace(re, "\n");

    // clear stack
    var i;
    for (i = this.stack.length - 1; i >= 0; i--) this.stack.pop()

    var pageElement = this.document.createElement('div');
    this.stack.push(pageElement);
    this.nodeAtCursorPosition = null;

    var characterCount = 0;
    var lines = inputString.split("\n")
    for (i = 0; i < lines.length; i++) {

        if (/^>>$/.test(lines[i])) {
            var qstack = [];
            var fineend = false;
            i++;
            while (i < lines.length) {
                if (/^<</.test(lines[i])) {
                    this.stack.top().appendChild(createQElem(qstack));
                    qstack = [];
                    fineend = true;
                    i++;
                    break;
                }
                qstack.push(lines[i]);
                i++;
            }
            //alert("ines.length=" + lines.length + " i=" + i);
            if (i == lines.length || i == lines.length - 1) {
                if (!fineend) {
                    //alert("lines.length");
                    this.stack.top().appendChild(createQElem(qstack));
                    qstack = [];
                }
                //i++;
                if (i == lines.length) break;
            }
            //            else if (i == lines.length - 1) {
            //                if (!fineend) {
            //                    alert("lines.length-1");
            //                    this.stack.top().appendChild(createQElem(qstack));
            //                    qstack = [];
            //                }
            //            } 
            //            else {
            //                i++;
            //            }
        }



        if (/^(!{1,5})(.*)$/.test(lines[i])) {
            this.jumpToTopLevel();
            var elementName = 'h' + RegExp.$1.length;
            var element = this.document.createElement(elementName);
            this.stack.top().appendChild(element);
            this.stack.push(element)
            this.inline(RegExp.$2);
            this.stack.pop()
        }
        else if (/^----/.test(lines[i])) {
            var element = this.document.createElement('hr')
            this.stack.top().appendChild(element);
        }
        else if (/^""(.*)$/.test(lines[i])) {
            if (this.stack.top().tagName.toLowerCase() != 'blockquote') {
                this.jumpToTopLevel();
                var element;
                element = this.document.createElement('blockquote')
                this.stack.top().appendChild(element);
                this.stack.push(element)
            }
            var textNode = this.document.createTextNode(RegExp.$1 + "\r\n");
            this.stack.top().appendChild(textNode);
        }
        else if (/^ (.*)$/.test(lines[i])) {
            if (this.stack.top().tagName.toLowerCase() != 'pre') {
                this.jumpToTopLevel();
                var element;
                element = this.document.createElement('pre')
                this.stack.top().appendChild(element);
                this.stack.push(element)
            }
            var textNode = this.document.createTextNode(RegExp.$1 + "\r\n");
            this.stack.top().appendChild(textNode);
        }
        else if (/^([*#]{1,3})(.*)/.test(lines[i])) {
            var newTagName = (RegExp.$1.substring(0, 1) == '#') ? 'ol' : 'ul'
            var level = RegExp.$1.length
            var j, listCount = 0;
            for (j = this.stack.length - 1; j >= 0; j--) {
                if ((this.stack[j].tagName.toLowerCase() == 'ul') ||
					(this.stack[j].tagName.toLowerCase() == 'ol'))
                    listCount++;
            }

            if (listCount == 0) this.jumpToTopLevel();

            if (level > listCount) {
                var className = newTagName + '_' + String(level);
                var j;
                for (j = level; j > listCount; j--) {
                    if ((this.stack.top().tagName.toLowerCase() == 'ul') ||
						(this.stack.top().tagName.toLowerCase() == 'ol')) {
                        var listItem = this.document.createElement('li')
                        listItem.setAttribute('class', className);
                        this.stack.top().appendChild(listItem);
                        this.stack.push(listItem)
                    }

                    var element = this.document.createElement(newTagName)
                    element.setAttribute('class', className);
                    this.stack.top().appendChild(element);
                    this.stack.push(element)
                }
            }
            else if (level <= listCount) {
                this.stack.pop();
                for (j = listCount; j > level; j--) {
                    this.stack.pop();
                    this.stack.pop();
                }
            }

            if (this.stack.top().tagName.toLowerCase() != newTagName) {
                this.stack.pop();
                var element = this.document.createElement(newTagName)
                element.setAttribute('class', className);
                this.stack.top().appendChild(element);
                this.stack.push(element)
            }

            var listItem = this.document.createElement('li')
            listItem.setAttribute('class', newTagName + '_' + String(level));
            this.stack.top().appendChild(listItem);
            this.stack.push(listItem)
            this.inline(RegExp.$2);
        }
        else if (/^:(.+)/.test(lines[i])) {
            if (this.stack.top().tagName.toLowerCase() != 'dl') {
                this.jumpToTopLevel();
                var element = this.document.createElement('dl')
                this.stack.top().appendChild(element);
                this.stack.push(element)
            }
            var strings = RegExp.$1.split(':', 2);
            var dtString = strings[0];
            var ddString = strings[1];

            var dtElement = this.document.createElement('dt')
            this.stack.top().appendChild(dtElement);
            dtElement.appendChild(this.document.createTextNode(dtString));

            var ddElement = this.document.createElement('dd')
            var pElement = this.document.createElement('p')
            this.stack.top().appendChild(ddElement);
            this.stack.push(ddElement);
            this.stack.top().appendChild(pElement);
            this.stack.push(pElement);
            this.inline(ddString)
            this.stack.pop();
            this.stack.pop();
        }
        else if (/^,(.+)$/.test(lines[i])) {
            if (this.stack.top().tagName.toLowerCase() != 'tbody') {
                this.jumpToTopLevel();
                var table = this.document.createElement('table')
                table.setAttribute('border', '1');
                this.stack.top().appendChild(table);
                this.stack.push(table)
                var tbody = this.document.createElement('tbody')
                this.stack.top().appendChild(tbody);
                this.stack.push(tbody)
            }
            this.tableLine(RegExp.$1);
        }
        else if (/^\s*$/.test(lines[i])) {
            this.jumpToTopLevel();
        }
        else if (/^>>(\d+)$/.test(lines[i])) {
            var mm;
            $.ajax({
                type: "GET",
                async: false,
                cache: false,
                dataType: "text",
                url: requrl + "/" + RegExp.$1,
                success: function (data) {
                    var json = Util.toJson(data);
                    if (json.length > 0) {
                        mm = jsview.quote(json[0], document);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("error textStatus = " + errorThrown);
                }
            });

            //alert($(mm).text());
            //qelem.appendChild($('#page' + RegExp.$1));
            $(mm).addClass('quote');
            this.stack.top().appendChild(mm);
        }
        /*
        else if(/^>/.test(lines[i]) )
        {
        var element = document.createElement('font');
        element.setAttribute("color", "#FF0000");
        element.innerHTML = lines[i];
        this.stack.top().appendChild(element);
        var pElement= this.document.createElement('p')
        this.stack.top().appendChild(pElement);
        }
        */
        else {
            if (this.stack.top().tagName.toLowerCase() != 'p') {
                this.jumpToTopLevel();
                var element = this.document.createElement('p')
                this.stack.top().appendChild(element);
                this.stack.push(element)
            }
            this.inline(lines[i]);
        }

        //	set this.nodeAtCursorPosition
        if (i < lines.length) {
            characterCount += lines[i].length + 1;
            if (this.nodeAtCursorPosition == null && characterCount >= this.cursorPosition) {
                var lastNode = pageElement;
                while (lastNode.lastChild && lastNode.lastChild.nodeName != '#text')
                    lastNode = lastNode.lastChild;
                this.nodeAtCursorPosition = lastNode;
            }
        }
    }

    //	pageElement.normalize();
    return pageElement;
}

WikiParser.prototype.inline = function (inlineString) {
    var textStack = "";

    var IN_NORMAL = 0;
    var IN_LINK = 1;
    var IN_FRAME = 2;
    var startLevel = this.stack.length;
    var target = inlineString + ' ';
    var status = IN_NORMAL
    while (target && target.length > 0) {
        //		alert('WikiParser#inline target : "' + target + '" len : ' + target.length );
        if (status == IN_NORMAL) {
            if (/^\'\'\'/.test(target)) {
                if (textStack != "") {
                    this.stack.top().appendChild(this.document.createTextNode(textStack));
                    textStack = "";
                }

                target = RegExp.rightContext
                if (this.stack.top().tagName.toLowerCase() != 'strong') {
                    var element = this.document.createElement('strong')
                    this.stack.top().appendChild(element);
                    this.stack.push(element)
                }
                else {
                    this.stack.pop();
                }
            }
            else if (/^\'\'/.test(target)) {

                if (textStack != "") {
                    this.stack.top().appendChild(this.document.createTextNode(textStack));
                    textStack = "";
                }

                target = RegExp.rightContext
                if (this.stack.top().tagName.toLowerCase() != 'em') {
                    var element = this.document.createElement('em')
                    this.stack.top().appendChild(element);
                    this.stack.push(element)
                }
                else {
                    this.stack.pop();
                }
            }
            else if (/^==/.test(target)) {
                if (textStack != "") {
                    this.stack.top().appendChild(this.document.createTextNode(textStack));
                    textStack = "";
                }

                target = RegExp.rightContext
                if (this.stack.top().tagName.toLowerCase() != 's') {
                    var element = this.document.createElement('s')
                    this.stack.top().appendChild(element);
                    this.stack.push(element)
                }
                else {
                    this.stack.pop();
                }
            }
            else if (/^((http|https|ftp|mailto):\/\/[\/\%\+\-_.\!~*\'()a-zA-Z\d;?:@&=$,#]+)/.test(target) ||
				/^mailto:[\/\%\+\-_.\!~*\'()a-zA-Z\d;?:@&=$,]+/.test(target)) {
                target = RegExp.rightContext
                var uri = RegExp.lastMatch;
                var linkElement = this.createURILink(uri, uri);
                this.stack.top().appendChild(linkElement);
            }
            /*
            else if (/^([A-Z][a-z0-9]+){2,}/.test(target))
            { // WikiName
            target = RegExp.rightContext;
            var wikiName = RegExp.lastMatch;
            var linkElement = this.createPageNameLink(wikiName);
            this.stack.top().appendChild(linkElement);
            }
            */
            else if (/^\[\[/.test(target)) {
                target = RegExp.rightContext;
                if (/\]\]/.test(RegExp.rightContext)) {
                    this.linkString = ''
                    status = IN_LINK;
                }
                else {
                    var text = this.document.createTextNode(RegExp.lastMatch);
                    this.stack.top().appendChild(text);
                }
            }
            else if (/^./.test(target)) {
                target = RegExp.rightContext
                //var text = this.document.createTextNode(RegExp.lastMatch);
                //this.stack.top().appendChild(text);
                textStack += RegExp.lastMatch;
            }
            //            else if (/^(\{\{)(.*)$/.test(lines[i])) {
            //            }
            else {
                alert('not match!! : ' + target);
            }
        }
        else if (status == IN_LINK) {
            if (/^\]\]/.test(target)) {
                this.stack.top().appendChild(this.document.createTextNode(textStack));
                textStack = "";

                var linkElement;
                /*
                if (/^(>>)(\d+)|(\d+|,)/.test(this.linkString))
                {
                var tmp = /^\]\]/.test(target);
                target = RegExp.rightContext
                status = IN_NORMAL;
                var label = this.linkString;//.substring(2, this.linkString.length-2);
                var uri = this.linkString;
                linkElement = this.createURILink(uri, label);
                //alert(target);
                }
                else
                {
                */
                target = RegExp.rightContext
                status = IN_NORMAL;
                var strings = this.linkString.split('|', 2);
                if (strings.length >= 2) { // URILink
                    var label = strings[0];
                    var uri = strings[1];
                    linkElement = this.createURILink(uri, label);
                }
                else { // PageNameLink
                    linkElement = this.createPageNameLink(this.linkString);
                }
                //}
                this.stack.top().appendChild(linkElement);
            }
            else if (/^./.test(target)) {
                target = RegExp.rightContext
                this.linkString += RegExp.lastMatch;
            }
        }
    }
    if (textStack != "") {
        this.stack.top().appendChild(this.document.createTextNode(textStack));
    }

    var i;
    for (i = this.stack.length; i > startLevel; i--) this.stack.pop()
    //	this.stack.top().normalize();

    //	alert('inline end');
    return;
}

WikiParser.prototype.tableLine = function(tableLineString)
{
	var IN_CSV_NORMAL = 0;
	var IN_CSV_QUOTED_VALUE = 1;
	var startLevel = this.stack.length;
	var target = tableLineString;
	var status = IN_CSV_NORMAL

	var tableRow = this.document.createElement('tr');
	this.stack.top().appendChild(tableRow);
	this.stack.push(tableRow);
	var columnString = ''

	while (target)
	{
		if (status == IN_CSV_NORMAL)
		{
			if ( /^\"\"/.test(target))
			{
				target = RegExp.rightContext
				columnString += '"'
			}
			else if ( /^\"/.test(target))
			{
				target = RegExp.rightContext
				status = IN_CSV_QUOTED_VALUE;
			}
			else if ( /^,/.test(target) )
			{
				target = RegExp.rightContext
				var tableColumn = this.document.createElement('td');
				this.stack.top().appendChild(tableColumn);
				this.stack.push(tableColumn);
				this.inline(columnString)
				this.stack.pop(); 
				columnString = ''
			}
			else if ( /^./.test(target))
			{
				columnString += RegExp.lastMatch;
				target = RegExp.rightContext
			}
			else
			{
				alert('not match!! : ' + target);
			}
		}
		else if (status == IN_CSV_QUOTED_VALUE)
		{
			if ( /^\"\"/.test(target))
			{
				target = RegExp.rightContext
				columnString += '"'
			}
			else if ( /^\"/.test(target))
			{
				status = IN_CSV_NORMAL;
				target = RegExp.rightContext
			}
			else if ( /^./.test(target))
			{
				columnString += RegExp.lastMatch;
				target = RegExp.rightContext
			}
			else
			{
				alert('not match!! : ' + target);
			}
		}
	}

	if (columnString.length > 0)
	{
		var tableColumn = this.document.createElement('td');
		this.stack.top().appendChild(tableColumn);
		this.stack.push(tableColumn);
		this.inline(columnString)
		this.stack.pop(); 
	}

	// jump to startLevel
	var i;
	for (i = this.stack.length;i > startLevel;i--) this.stack.pop()

	return ;
}

WikiParser.prototype.createURILink = function(uri, label)
{
	var element;
	if (!label) label = uri;
	if (/\.(jpg|jpeg|png|gif)$/.test(uri))
	{
		element = this.document.createElement('img')
		element.setAttribute('src', uri);
		element.setAttribute('alt', label);
		element.setAttribute('title', label);
		
		//if(/^!/.test(label))
		//{
			//element.setAttribute('class', 'URILink');
			element.className = 'URILink';
			var p = this.document.createElement('a');
			p.setAttribute('title', "uri");

			p.href = "javascript:void(0)//" + uri;
			//p.href = uri;
			p.className = "thickbox";
			
			//p.setAttribute('class','thickbox');
			$(p).click(function(event){
				//"var t = this.title || this.name || null;"
				var t = label;
				//+"var a = this.href || this.alt;"
				var a = uri;
				//+"var g = this.rel || false;"
				var g =  false;
				tb_show(t,a,g);
				//this.blur();
				return false;
			})

			p.appendChild(element);
			return p;
		//}
	}
//	else if (/(\.bmp)$/.test(uri))
//	{
//			var p = this.document.createElement('a');
//			p.setAttribute('title', "uri");
//			//p.setAttribute('href', "sub.html?TB_iframe=true&height=300&width=600");
//			//p.href = 'sub.html?TB_iframe=true&height=100&width=200';
//			p.href = "javascript:void(0)";
//			p.className = 'thickbox';
//			//p.title="表示する"
//			var text = this.document.createTextNode("bmp");
//			p.appendChild(text);
//			$(p).click(function(event){
//				//"var t = this.title || this.name || null;"
//				var t = label;
//				//+"var a = this.href || this.alt;"
//				var a = 'sub.html?id=100&TB_iframe=true&height=100&width=200';
//				//+"var g = this.rel || false;"
//				var g =  false;
//				tb_show(t,a,g);
//				//this.blur();
//				return false;
//			})
//			return p;
//	}
	else
	{
		var text = this.document.createTextNode(label);
		element = this.document.createElement('a')
		element.appendChild(text);
		element.setAttribute('href', uri);
	}
	element.setAttribute('class', 'URILink');
	return element;
}

WikiParser.prototype.createPageNameLink = function (pageName) {
    var element;
    var text = this.document.createTextNode(pageName);
    element = this.document.createElement('a')
    element.appendChild(text);

    if (/^>>>(.+)/.test(pageName)) {
        var b = RegExp.$1;

        element.setAttribute('href', 'javascript:void(0) //goto ' + b);
        $(element).click(function (event) {
            $.ajax({
                type: 'POST',
                cache:false,
                url: requrl + "/goto",
                data: b
            });
            return false;
        });
    } else if (/^(>{2})([\d+|,]+)/.test(pageName)) {
        var ids = RegExp.$2;
        //alert(ids);
        element.setAttribute('href', 'javascript:void(0)');
        $(element).click(function (event) {
            var t = text;
            var a = 'sub.html?id=' + ids + '&requrl=' + requrl + '&TB_iframe=true&height=400&width=400';
            var g = false;
            tb_show(t, a, g);
            return false;
        });
    }
    else if (/^<<<(.*)/.test(pageName)) { //come-from
        var b = RegExp.$1;

        if ($.inArray(b, this.cf) == -1) {
            this.cf.push(b);
        }

        element.setAttribute('href', 'javascript:void(0)');
        $(element).click(function (event) {
            $.ajax({
                type: 'POST',
                cache:false,
                url: requrl + "/" + this.id + "/comefrom",
                data: b
            });
            return false;
        });
    }
    else if (/^(<{2})(.*)/.test(pageName)) { //come-from
        var b = RegExp.$2;

        $(text).remove();
        var utext = this.document.createTextNode(b);
        element.appendChild(utext);

        element.setAttribute('href', 'javascript:void(0)');
        $(element).click(function (event) {
            $.ajax({
                type: 'POST',
                cache:false,
                url: requrl + "/" + this.id + "/comefrom",
                data: b
            });
            return false;
        });
    }
    else if (/^!(.+)/.test(pageName)) {
        //var b = RegExp.$1;
				try{
					var j = Util.parseParams(RegExp.$1);
				}catch(e){
						var res ="";
						for(var n in e){
							res += n + " : " +e[n] + "\n";
						}
						alert("Util.parseParams " + res);
				}
        element.setAttribute('href', 'javascript:void(0)');
        $(element).click(function (event) {
            $.ajax({
                type: 'POST',
                cache:false,
                url: requrl + "/exe",
                data: $.toJSON(j)
								//data: b
            });
            return false;
        });
    }
    else if (/^->(\d+)/.test(pageName)) {
        var b = RegExp.$1;
        
        if (this.id == b) return element;

//$(text).remove();
        element = this.document.createElement('div');
        //element.setAttribute('class', 'quote');
        element.className = 'quote';
        //$(element).setclass('quote');
    //element.appendChild(text);

        $.ajax({
            type: 'GET',
            //async: false,
            dataType: "text",
            cache:false,
            url: requrl + '/' + b,
            success: function (value) {
              var page = jsview.quote(Util.toJson(value)[0]);
							element.appendChild(page);
            }
        });
    }
    else {
        element.setAttribute('href', this.pageName2URI(pageName));
        element.setAttribute('class', 'PageNameLink');
    }
    return element;
}

WikiParser.prototype.jumpToTopLevel = function()
{
	for (var i = this.stack.length-1;i > 0;i--) this.stack.pop()
}

// override plz...
WikiParser.prototype.pageName2URI = function(pageName)
{
	return '#' + pageName;
}

