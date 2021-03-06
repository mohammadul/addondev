
//chromebug-1.5.0a2.xpi
//firebug-1.4.2-fx.xpi

//FBL.ns(function chromebug() { with (FBL) {
FBL.ns(function() { with (FBL) {

Firebug.chromebug_eclipseModle =extend(Firebug.Module,
{
	initialize: function() 
	{
		//var path = Firebug.chromebug_eclipse.util.getFilePathFromURL("chrome://stacklink/content/stacklink.js");
		//Application.console.log("chrome://stacklink/content/stacklink.js -> " + path);
		//Application.console.log("urlFilters = " +  urlFilters);
		//Application.console.log("chromebug_eclipseModle initialize");
		
//		var cbe_resetBreakpoints = FBL.fbs.resetBreakpoints;	
//		FBL.fbs.resetBreakpoints = function(sourceFile, lastLineNumber)
//		{		
//			Firebug.chromebug_eclipseModle.resetBreakpoints.resetBreakpoints(sourceFile, lastLineNumber);
//			firebug_resetBreakpoints(sourceFile, lastLineNumber);
//		}
//		Firebug.Debugger.addListener(this); 
		
		var firebug_resetBreakpoints =  Firebug.Debugger.fbs.resetBreakpoints;
		//Application.console.log("firebug_resetBreakpoints = " + firebug_resetBreakpoints);
		Firebug.Debugger.fbs.resetBreakpoints = function(sourceFile, lastLineNumber)
		{
			Firebug.chromebug_eclipseModle.resetBreakpoints(sourceFile, lastLineNumber);			
			firebug_resetBreakpoints(sourceFile, lastLineNumber);
		};
		
		Firebug.ActivableModule.initialize.apply(this, arguments);
		//Firebug.Debugger.fbs.countContext(true); 
		//Firebug.Debugger.addListener(this); 
		//Firebug.Debugger.addListener(Firebug.chromebug_eclipseModle.DebuggerListener); 
		//Firebug.Debugger.setDefaultState(Firebug.chromebug_eclipseModle.DebuggerListener);
		//Firebug.Chromebug.Debugger.addListener(this); 
		
		Application.console.log("Firebug.Chromebug = " + Firebug.Chromebug);
//		for(var name in Firebug.Chromebug)
//		{
//			try{
//				Application.console.log("name = " + name +" : " + Firebug.Chromebug[name]); 
//			}catch(exc){
//				en += "name = " + name +" ERROR";
//			}
//		}
		if(Firebug.Chromebug)
		{		
			var sss = Firebug.Chromebug.onStop;
			Firebug.Chromebug.onStop = function(context, frame, type, rv)
			{
				sss(context, frame, type, rv);
				Firebug.chromebug_eclipseModle.onStop(context, frame, type, rv);
			};	
		}

//		if(Firebug.Chromebug)
//		{
//			var sss = Firebug.Chromebug.onStop;
//			
//			Firebug.Chromebug.onStop = function(context, frame, type, rv)
//			{
//				sss(context, frame, type, rv);
//				//this.onStop(context, frame, type, rv);
//			};
//		}
		this.net = {};
		this.util = {};

		Components.utils.import("resource://ec_modules/net.js", this.net);
		Components.utils.import("resource://ec_modules/xmlutil.js", this.util);
		
		//Components.utils.import("resource://gre/modules/JSON.jsm", this.util);
		if (typeof(JSON) == "undefined") {
			  Components.utils.import("resource://gre/modules/JSON.jsm");
			  JSON.parse = JSON.fromString;
			  JSON.stringify = JSON.toString;
		}
		//this.util.JSON = JSON;
		
		
		this.startServer();
	},
	initializeUI: function()
	{
		//this.startServer();
	},	
    shutdown: function()
    {
		//alert("shutdown");
		//this.server.stop();
		server.stop();
    },
    resetBreakpoints: function(sourceFile, lastLineNumber)
    {
//    	Application.console.log("resetBreakpoints sourceFile.href = " + sourceFile.href);
//    	if(sourceFile.href.indexOf("browser/content/browser.xul/event/seq/") > 0)
//    	{
//    		var val2 = sourceFile.outerScript;
//    		val2 = sourceFile;
//	 		for(var key in val2)
//			{
//				Application.console.log("sourceFile key = " + key + " : "+ val2[key]);
//			}   		
//    	}
    	
    	if(sourceFile.href in Firebug.chromebug_eclipse.util.sourceFileMap)
    	{
    		
    	}
    	else
    	{
    		Firebug.chromebug_eclipse.util.sourceFileMap[sourceFile.href] = sourceFile;
    	}
    	//chromebug_eclipse.util.sourceFileMap[sourceFile.href] = sourceFile;
    	//Application.console.log("resetBreakpoints sourceFile.href = " + sourceFile.href);
    	//if(sourceFile.href.indexOf("hello.js") > 0)
    	//{
    	//	Firebug.Debugger.fbs.setBreakpoint(sourceFile, 26, null, Firebug.Debugger);
    	//}
    	
    	if(sourceFile.href in Firebug.chromebug_eclipse.util.breakpointMap)
    	{	
    		//Application.console.log("resetBreakpoints sourceFile.href in breakpointMap = " + sourceFile.href);
    		for(line in Firebug.chromebug_eclipse.util.breakpointMap[sourceFile.href])
    		{
    			var linenum = Firebug.chromebug_eclipse.util.breakpointMap[sourceFile.href][line];
    			//if(!(linenum in this.pp))
    			//{
    			Application.console.log("resetBreakpoints sourceFile.href=" + sourceFile.href + " linenum=" + linenum);
    			//Firebug.Debugger.fbs.setBreakpoint(sourceFile, linenum, null, Firebug.Debugger);
    			Firebug.Debugger.setBreakpoint(sourceFile, linenum);
    			//this.pp.push(linenum);
    			//}
    		}
    	}
    },
    
    onStop: function(context, frame, type, rv)
    {
    	//Firebug.Debugger.resume(FirebugContext);
    	//var line = 28;
    	//FBL.fbs.clearBreakpoint(href, line);
    	//FBL.fbs.disableBreakpoint(href, line);
    	Application.console.log("frame.script.functionName = " + frame.script.functionName);
    	Application.console.log("onStop frame.href = " +frame.href);
    	//if(frame.href.indexOf('file:/')==0)
    	//{
//		for(var key in context.sourceCache.cache)
//		{
//			Application.console.log("sourceCache key = " + key);
//		}
		//Application.console.log("context.sourceCache.cache ckey = " + context.sourceCache.cache[ckey]);
    	//Application.console.log("frame.args = " +frame.args);
    	Firebug.chromebug_eclipse.util.currnetFrame = frame;
    	try
    	{
    		//Firebug.chromebug_eclipse.util.currentStackTrace = FBL.getStackTrace(frame, context);
    		Firebug.chromebug_eclipse.util.currentStackTrace = FBL.getCorrectedStackTrace(frame, context);
    	}catch (e) {
			// TODO: handle exception
    		Application.console.log("ce getStackTrace e = " + e);
		}
    	try
    	{
    		//var postdata = Firebug.chromebug_eclipse.util.getStackFramesXML();
    		var postdata = Firebug.chromebug_eclipse.util.getStackFrames();
		}catch (ex) {
			// TODO: handle exception
			Application.console.log("ce getStackFramesXML ex = " + ex);
		}

		Application.console.log("onStop = " + postdata);
        //ecclient.send("suspend", postdata);
		postdata.cmd = "suspend";
		ecclient.send(postdata);
    },
    
    terminate : function()
    {
    	Application.console.log("chrome_eclipse terminate");
    	goQuitApplication();
    },
    
	startServer : function()
	{
		var eclipseport = Application.storage.get('ce_eport', -1);
		var chromeport = Application.storage.get('ce_cport', -1);	
		//var eclipseport = 8084;
		//var chromeport = 8083;
		
		Application.console.log("eclipseport = " + eclipseport);
		Application.console.log("chromeport = " + chromeport);
		
		if((eclipseport && eclipseport > 0) && (chromeport && chromeport > 0))
		{
			//Application.console.log("startServer0");
			//this.server = new this.net.server(chromeport); 
			if(this.net.server.isWorking) 
				return;
			
			this.net.server.isWorking= true;
			
			//extensions.firebug.service.filterSystemURLs;true
			//Firebug.Debugger.fbs.filterSystemURLs = false;
			//Firebug.filterSystemURLs = false;
			var filterSystemURLs = Application.prefs.getValue("extensions.firebug.service.filterSystemURLs", true);
			if(!filterSystemURLs)
				Application.prefs.setValue("extensions.firebug.service.filterSystemURLs", false);
			
			//javascript.options.strict false => OK
			//javascript.options.strict true => NG
			//var strict = Application.prefs.getValue("javascript.options.strict", true);
			//if(!strict)
			//	Application.prefs.setValue("javascript.options.strict", false);			
			
			//extensions.firebug.service.showAllSourceFiles;false
			//Firebug.Debugger.fbs.showAllSourceFiles = true;
			//Firebug.showAllSourceFiles = true;
			//Firebug.filterSystemURLs = true;
			//Application.prefs.setValue("extensions.firebug.service.showAllSourceFiles", true);
			
			//alert("startServer");
			Application.console.log("startServer");
			ecclient.port = eclipseport;
			this.server = this.net.server;
			this.server.init(chromeport);
			//this.server.port = chromeport;//Firebug.getPref(Firebug.prefDomain, "FireJavaScriptDebugger.port");
			this.server.pathHandler = this.pathHandler;
			this.server.start();
			//var res = FireJavaScriptDebugger.server.start();
			//alert("client.port type = " + typeof(FireJavaScriptDebugger.client.port));
			try
			{
				//alert("ready");
				//ecclient.send("ready");
		        let json = {};
				json.cmd = "ready";
				json.propertylist = [];
				ecclient.send(json);
			}catch(ex)
			{
				Application.console.log("client error : " + ex);
			}
		}
		else
		{
		}
		
		//var path = Firebug.chromebug_eclipse.util.getFilePathFromURL("chrome://hello/content/hello.js");
		//Application.console.log("chrome://hello/content/hello.js -> " + path);
	},
    
	pathHandler:function(postdata) 
	{

    	//Application.console.log("queryString = " + queryString);
//	  var params={};
//	  if (queryString) {
//		  var qs = queryString;
//		  var qsa=qs.split('&');
//		  for(var i=0; i<qsa.length; i++) {
//			  var pair=qsa[i].split('=');
//			  if (pair[0]) {
//				  params[pair[0]]=decodeURIComponent(pair[1]);
//			  }
//		  }
//	  }
//	  else
//	  {
//	  	//return "accept";
//	  }

    	 Application.console.log("pathHandler postdata = " + postdata);
	  //var body = "accept";
	  var body = null;
	  //var cmd = params.cmd;
	  //var result;
//	  if(postdata == "accept")
//	  {
//	  }
//	  else
//	  {
		  var result = JSON.parse(postdata);
		  var cmd = result.cmd;
//	  }
	  var file, line;
	 
	  switch(cmd) 
	  {
	  	case "setbreakpoint":
	  		//alert("setbreakpoint postdata = " + postdata);
	  		//result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
	  		for(i=0;i<result.propertylist.length; i++)
	  		{
	  			file = result.propertylist[i]["filename"];
	  			line = parseInt(result.propertylist[i]["line"]);
	  			Application.console.log("setbreakpoint file=" + file + " : " + "line = "+ line);
		  		if(file in Firebug.chromebug_eclipse.util.sourceFileMap)
		  		{
		  			var s = Firebug.chromebug_eclipse.util.sourceFileMap[file];
		  			Firebug.Debugger.fbs.setBreakpoint(s, line, null, Firebug.Debugger);
		  		}
		  		else
		  		{		  		
			  		if(file in Firebug.chromebug_eclipse.util.breakpointMap)
			  		{
			  			Firebug.chromebug_eclipse.util.breakpointMap[file].push(line);
			  		}
			  		else
			  		{
			  			Firebug.chromebug_eclipse.util.breakpointMap[file] = new Array();
			  			Firebug.chromebug_eclipse.util.breakpointMap[file].push(line);
			  		}
		  		}	  			
	  		}
//	  		for(i=0;i<result.length; i++)
//	  		{
//	  			file = decodeURIComponent(result[i]["filename"]);
//	  			line = parseInt(result[i]["line"]);
//	  			Application.console.log("setbreakpoint file=" + file + " : " + "line = "+ line);
//		  		if(file in Firebug.chromebug_eclipse.util.sourceFileMap)
//		  		{
//		  			var s = Firebug.chromebug_eclipse.util.sourceFileMap[file];
//		  			Firebug.Debugger.fbs.setBreakpoint(s, line, null, Firebug.Debugger);
//		  		}
//		  		else
//		  		{		  		
//			  		if(file in Firebug.chromebug_eclipse.util.breakpointMap)
//			  		{
//			  			Firebug.chromebug_eclipse.util.breakpointMap[file].push(line);
//			  		}
//			  		else
//			  		{
//
//			  			Firebug.chromebug_eclipse.util.breakpointMap[file] = new Array();
//			  			Firebug.chromebug_eclipse.util.breakpointMap[file].push(line);
//			  			
//			  			//LOG("setbreakpoint file=" + file + "\n" + "line = "+ line);
//			  		}
//		  		}
//	  		}
	    	break;
	    case "removebreakpoint":
	  		for(i=0;i<result.propertylist.length; i++)
	  		{
	  			file = result.propertylist[i]["filename"];
	  			line = parseInt(result.propertylist[i]["line"]);
	  			
	  			if(file in Firebug.chromebug_eclipse.util.breakpointMap)
	  			{
	  				if(line in Firebug.chromebug_eclipse.util.breakpointMap[file])
	  				{
	  					let index = Firebug.chromebug_eclipse.util.breakpointMap[file].indexOf(line);
	  					if(index != -1)
	  					{
	  						Firebug.chromebug_eclipse.util.breakpointMap[file].splice(index,1);
	  					}
	  				}
	  			}
	  			
	  			Firebug.Debugger.fbs.clearBreakpoint(file, line);
	  		}
	    	
//	  		result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);	
//	  		for(i=0;i<result.length; i++)
//	  		{
//	  			file = decodeURIComponent(result[i]["filename"]);
//	  			line = parseInt(result[i]["line"]);
//	  			
//	  			if(file in Firebug.chromebug_eclipse.util.breakpointMap)
//	  			{
//	  				if(line in Firebug.chromebug_eclipse.util.breakpointMap[file])
//	  				{
//	  					let index = Firebug.chromebug_eclipse.util.breakpointMap[file].indexOf(line);
//	  					if(index != -1)
//	  					{
//	  						Firebug.chromebug_eclipse.util.breakpointMap[file].splice(index,1);
//	  					}
//	  				}
//	  			}
//	  			
//	  			Firebug.Debugger.fbs.clearBreakpoint(file, line);
//	  		}	    	
	    	break;
	    case "setbreakpointcondition":
	  		for(i=0;i<result.propertylist.length; i++)
	  		{
	  			file = result.propertylist[i]["filename"];
	  			line = parseInt(result.propertylist[i]["line"]);
	  			var condition = result.propertylist[i]["condition"];
	  			Application.console.log("setbreakpointcondition file=" + file + " : " + "line = "+ line);
		  		if(file in Firebug.chromebug_eclipse.util.sourceFileMap)
		  		{
		  			var s = Firebug.chromebug_eclipse.util.sourceFileMap[file];
		  			Firebug.Debugger.fbs.setBreakpointCondition(s, line, condition, Firebug.Debugger);
		  		}
	  		}
	    	
//	    	result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
//	  		for(i=0;i<result.length; i++)
//	  		{
//	  			file = decodeURIComponent(result[i]["filename"]);
//	  			line = parseInt(result[i]["line"]);
//	  			var condition = decodeURIComponent(result[i]["condition"]);
//	  			Application.console.log("setbreakpointcondition file=" + file + " : " + "line = "+ line);
//		  		if(file in Firebug.chromebug_eclipse.util.sourceFileMap)
//		  		{
//		  			var s = Firebug.chromebug_eclipse.util.sourceFileMap[file];
//		  			Firebug.Debugger.fbs.setBreakpointCondition(s, line, condition, Firebug.Debugger);
//		  		}
//	  		}
	    	break;
//	  	case "load":
//	  		loadURI(params.file);
//	  		break;
	  	case "open":
	  		window.open().home();
	  		//loadURI(params.file);
	  		break;
	  	case "closebrowser":
	  		//window.close();
	  		//Application.quit();
  			for(file in Firebug.chromebug_eclipse.util.breakpointMap)
  			{
  				Firebug.chromebug_eclipse.util.breakpointMap[file].length = 0;
  			}
	  		Firebug.Debugger.clearAllBreakpoints();
	  		const WindowManager = Components.classes['@mozilla.org/appshell/window-mediator;1'].getService(Components.interfaces.nsIWindowMediator);
	  		var browsers = WindowManager.getEnumerator('navigator:browser');
	  		var browser;
	  		while (browsers.hasMoreElements()) {
	  		    browser = browsers.getNext()
	  		            .QueryInterface(Components.interfaces.nsIDOMWindowInternal);
	  		    browser.BrowserTryToCloseWindow();
	  		}
	  		break;
	  	case "resume":
	  		Firebug.Debugger.resume(FirebugContext);
	  		break;
	  	case "stepover":
	  		Firebug.Debugger.stepOver(FirebugContext);
	  		break;
	  	case "stepinto":
	  		Firebug.Debugger.stepInto(FirebugContext);
	  		break;
	  	case "stepout":
	  		Firebug.Debugger.stepOut(FirebugContext);
	  		break;
	  	case "getvalues": 
	  		//Application.console.log("getvalues postdata = " + postdata);
	  		//alert("getvalues postdata = " + postdata);
	  		if(result.propertylist.length ==1)
	  		{
	  			var depth = parseInt(result.propertylist[0]["depth"]);
	  			var valuename = result.propertylist[0]["valuename"];
	  			var frame = Firebug.chromebug_eclipse.util.currnetFrame;
	  			
	  			//Application.console.log("getvalues depth = " + depth);
	  			//Application.console.log("getvalues valuename = " + valuename);
	  			//Application.console.log("getvalues frame = " + frame);
	  			//if(valuename == 'this') depth++;
	  		
		  		for (let i = 0; i<depth; i++)
		  		{
		  			frame = frame.callingFrame;
		  		}
		  		body = Firebug.chromebug_eclipse.util.getValues(frame, valuename);	
		  		//Application.console.log("getvalues body = " + body);
	  		}
	  		else
	  		{
	  			Application.console.log("getvalues result.length = 0");
	  		}
	  		
//	  		//Application.console.log("getvalues postdata = " + postdata);
//	  		//alert("getvalues postdata = " + postdata);
//	  		result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
//	  		//Application.console.log("getvalues result.length = " + result.length);
//	  		if(result.length ==1)
//	  		{
//	  			var depth = parseInt(result[0]["depth"]);
//	  			var valuename = result[0]["valuename"];
//	  			var frame = Firebug.chromebug_eclipse.util.currnetFrame;
//	  			
//	  			//Application.console.log("getvalues depth = " + depth);
//	  			//Application.console.log("getvalues valuename = " + valuename);
//	  			//Application.console.log("getvalues frame = " + frame);
//	  			//if(valuename == 'this') depth++;
//	  		
//		  		for (let i = 0; i<depth; i++)
//		  		{
//		  			frame = frame.callingFrame;
//		  		}
//		  		body = Firebug.chromebug_eclipse.util.getValuesXML(frame, valuename);	
//		  		//Application.console.log("getvalues body = " + body);
//	  		}
//	  		else
//	  		{
//	  			Application.console.log("getvalues result.length = 0");
//	  		}
	  		break;
	  	case "getvalue":
	  		Application.console.log("getvalue postdata = " + postdata);
	  		//alert("getvalues postdata = " + postdata);
	  		if(result.propertylist.length ==1)
	  		{
	  			var depth = parseInt(result.propertylist[0]["depth"]);
	  			var valuename = result.propertylist[0]["valuename"];
	  			var frame = Firebug.chromebug_eclipse.util.currnetFrame;
	  			
	  			//Application.console.log("getvalues depth = " + depth);
	  			//Application.console.log("getvalues valuename = " + valuename);
	  			//Application.console.log("getvalues frame = " + frame);
	  			//if(valuename == 'this') depth++;
	  			
		  		for (let i = 0; i<depth; i++)
		  		{
		  			frame = frame.callingFrame;
		  		}
		  		body = Firebug.chromebug_eclipse.util.getValue(frame, valuename);	
		  		Application.console.log("getvalue body = " + body);
	  		}
	  		else
	  		{
	  			Application.console.log("getvalue result.length = 0");
	  		}
	  		
//	  		Application.console.log("getvalue postdata = " + postdata);
//	  		//alert("getvalues postdata = " + postdata);
//	  		result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
//	  		//Application.console.log("getvalues result.length = " + result.length);
//	  		if(result.length ==1)
//	  		{
//	  			var depth = parseInt(result[0]["depth"]);
//	  			var valuename = result[0]["valuename"];
//	  			var frame = Firebug.chromebug_eclipse.util.currnetFrame;
//	  			
//	  			//Application.console.log("getvalues depth = " + depth);
//	  			//Application.console.log("getvalues valuename = " + valuename);
//	  			//Application.console.log("getvalues frame = " + frame);
//	  			//if(valuename == 'this') depth++;
//	  			
//		  		for (let i = 0; i<depth; i++)
//		  		{
//		  			frame = frame.callingFrame;
//		  		}
//		  		body = Firebug.chromebug_eclipse.util.getValueXML(frame, valuename);	
//		  		Application.console.log("getvalue body = " + body);
//	  		}
//	  		else
//	  		{
//	  			Application.console.log("getvalue result.length = 0");
//	  		}
	  		break;
	  	case "terminate":
	  		Application.console.log("terminate");
	  		Firebug.chromebug_eclipseModle.terminate();
	  		break;
	  	default:
	  		Application.console.log("default");
	  		break;
	  }  
	  
	  if(body == null)
	  {
        let json = {};
		json.cmd = "accept";
//		json.propertylist = [];        
//	 	let prop = {};
//	 	json.propertylist.push(prop); 
	  }
	  
	  let data = JSON.stringify(body);
	  
	  return data;	
	}
//}
});

var ecclient = {
	port:null,
	
	send:function(data)
	{
		try
		{
			
			//var url = "http://localhost:" + this.port+ "/?cmd=" + command;
			var url = "http://localhost:" + this.port;
			
			//var url = "http://localhost:8084/?cmd=" + command;
	        var request = new XMLHttpRequest();
//			request.onreadystatechange = function() {
//				LOG(request.readyState+":"+request.status);
//				if (request.readyState == 4 && request.status == 200) {
//				//if (request.status == 200) {
//			      //var result = document.getElementById("result_post");
//			      //var text = document.createTextNode(decodeURI(request.responseText));
//			      //result.appendChild(text);
//						LOG('request.responseText = ' + request.responseText);
//				}
//			}        
//	        if(!data) data = "accept";
//			request.open("POST", url, false);
//	        request.setRequestHeader('Content-Type', 'text/xml');
//			request.send(data);
			
	        if(!data || data == undefined) data = "debuggeraccept";
	        
	        //let postdata = Firebug.chromebug_eclipse.util.JSON.stringify(data);
	        let postdata = JSON.stringify(data);
	        
	        Application.console.log("ecclient.send postdata= " + postdata);
	        
			request.open('POST', url, false);
			//request.setRequestHeader("content-type","text/xml");
			request.send(postdata);
		    if(request.status == 200) {
		    	//LOG('request.responseText = ' + request.responseText);
		    }
		}
		catch(exc)
		{
			Application.console.log("send error = " + exc);
		}
	}
};

function ChromeDebuggerPanel() 
{
} 
ChromeDebuggerPanel.prototype = extend(Firebug.Panel, 
{ 
    name: "ChromeDebugger", 
    title: "ChromeDebugger", 

    initialize: function() {
		Firebug.Panel.initialize.apply(this, arguments);
      //Firebug.Debugger.addListener(this);   
      //LOG("ChromeDebuggerPanel initialize");
      //LOG("FBL.TabWatcher.getContextByWindow = " + FBL.TabWatcher.getContextByWindow);
    }
}); 


//window.addEventListener('load', function() { 
//	 
//}, false);

window.addEventListener('unload', function() {
	//alert("unload");
	window.removeEventListener('unload', arguments.callee, false);
	// windowtype="chromebug:ui"
	const WindowManager = Components.classes['@mozilla.org/appshell/window-mediator;1'].getService(Components.interfaces.nsIWindowMediator);
	var targets = WindowManager.getEnumerator('navigator:browser');
	var chromebugui = WindowManager.getMostRecentWindow('chromebug:ui');
	if (targets.hasMoreElements() && !chromebugui) {
		//window.close();
		Application.quit();
		//targets.close();
		//ecclient.send("windowstate", "<xml><window=\"main\" value=\"open\"><window=\"chromebug\" value=\"close\"></xml>");
	}
	else if(!targets.hasMoreElements() && chromebugui)
	{
		//ecclient.send("closebrowser");
        let json = {};
		json.cmd = "closebrowser";
		json.propertylist = [];
		ecclient.send(json);
	}

}, false);


Firebug.registerModule(Firebug.chromebug_eclipseModle); 
//Firebug.registerPanel(ChromeDebuggerPanel); 

}});


