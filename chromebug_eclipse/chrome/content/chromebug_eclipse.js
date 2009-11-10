
FBL.ns(function chromebug() { with (FBL) {


Firebug.chromebug_eclipseModle =extend(Firebug.Module,
{
	initialize: function() 
	{
		//alert("initialize");
		//Application.console.log("chromebug_eclipseModle initialize");
		/*
		var cbe_resetBreakpoints = FBL.fbs.resetBreakpoints;	
		FBL.fbs.resetBreakpoints = function(sourceFile, lastLineNumber)
		{		
			chromebug_eclipseModle.resetBreakpoints(sourceFile, lastLineNumber);
			cbe_resetBreakpoints(sourceFile, lastLineNumber);
		}
		Firebug.Debugger.addListener(this); 
		*/

		
		var firebug_resetBreakpoints =  Firebug.Debugger.fbs.resetBreakpoints;
		Firebug.Debugger.fbs.resetBreakpoints = function(sourceFile, lastLineNumber)
		{
			//
			Firebug.chromebug_eclipseModle.resetBreakpoints(sourceFile, lastLineNumber);
			firebug_resetBreakpoints(sourceFile, lastLineNumber);
		}
		//Firebug.ActivableModule.initialize.apply(this, arguments);
		//Firebug.Debugger.fbs.countContext(true); 
		Firebug.Debugger.addListener(this); 
		//Firebug.Chromebug.Debugger.addListener(this); 
		
		if(Firebug.Chromebug)
		{
		var sss = Firebug.Chromebug.onStop;
		
		Firebug.Chromebug.onStop = function(context, frame, type, rv)
		{
			//alert("wakiyama");
			
			sss(context, frame, type, rv);
		}
		}
		this.net = {};
		this.util = {};
		Components.utils.import("resource://ec_modules/net.js", this.net);
		Components.utils.import("resource://ec_modules/xmlutil.js", this.util);
		
		//var h = new nsHttpServer();
		//h.start(8083);
		//for(key in FBL)
		//{
		//	Application.console.log("FBL key = " + key + " : " + FBL[key]);
		//}
		
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
		//this.hh.stop();
    },
 
    resetBreakpoints: function(sourceFile, lastLineNumber)
    {
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
    		Application.console.log("resetBreakpoints sourceFile.href in breakpointMap = " + sourceFile.href);
    		for(line in Firebug.chromebug_eclipse.util.breakpointMap[sourceFile.href])
    		{
    			Application.console.log("resetBreakpoints sourceFile.href = " + sourceFile.href);
    			var linenum = Firebug.chromebug_eclipse.util.breakpointMap[sourceFile.href][line];
    			Firebug.Debugger.fbs.setBreakpoint(sourceFile, linenum, null, Firebug.Debugger);
    		}
    	}
    },
    terminate : function()
    {
		var os = Cc["@mozilla.org/observer-service;1"].getService(Ci.nsIObserverService);
		var cancelQuit = Cc["@mozilla.org/supports-PRBool;1"].createInstance(Ci.nsISupportsPRBool);
		os.notifyObservers(cancelQuit, "quit-application-requested", null);
      
		if (cancelQuit.data) return;
      
		//alert("quit");
		var appStartup = Cc['@mozilla.org/toolkit/app-startup;1'].getService(Ci.nsIAppStartup);
		appStartup.quit(Ci.nsIAppStartup.eAttemptQuit);
    },
    
	startServer : function()
	{
		
		
		//var ss = new HttpServer(3644);
		//HttpServer.start(8083);
		
		//this.hh = new nsHttpServer();
		//this.hh.start(8083);
		var eclipseport = Application.storage.get('ce_eport', -1);
		var chromeport = Application.storage.get('ce_cport', -1);	
		//var eclipseport = 8084;
		//var chromeport = 8083;
		
		Application.console.log("eclipseport = " + eclipseport);
		Application.console.log("chromeport = " + chromeport);
		
		if((eclipseport && eclipseport > 0) && (chromeport && chromeport > 0))
		{
			Application.console.log("startServer0");
			//this.server = new this.net.server(chromeport); 
			if(this.net.server.isWorking) 
				return;
			
			this.net.server.isWorking= true;
			
			Firebug.Debugger.fbs.filterSystemURLs = false;
			Firebug.filterSystemURLs = false;
			
			//alert("startServer");
			Application.console.log("startServer");
			//ecclient.port = eclipseport;
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
				ecclient.send(eclipseport, "ready");
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
    
    onStop: function(context, frame, type, rv)
    {
    	//Application.console.log("ce onStop");
    	//Firebug.Debugger.resume(FirebugContext);
    	//var line = 28;
    	//FBL.fbs.clearBreakpoint(href, line);
    	//FBL.fbs.disableBreakpoint(href, line);
    	//Application.console.log("frame.script.functionName = " + frame.script.functionName);
    	Application.console.log("frame.args = " +frame.args);
    	Firebug.chromebug_eclipse.util.currnetFrame = frame;
    	Firebug.chromebug_eclipse.util.currentStackTrace = FBL.getStackTrace(frame, context);
        var postdata = Firebug.chromebug_eclipse.util.getStackFramesXML();
        
        Application.console.log("ce onStop postdata = " + postdata);
        
        var eclipseport = Application.storage.get('ce_eport', -1);
        ecclient.send(eclipseport, "suspend", postdata);
    	//this.net.client.send("suspend", postdata);
    },
    
	pathHandler:function(queryString, postdata) 
	{

    	Application.console.log("queryString = " + queryString);
	  var params={};
	  if (queryString) {
		  var qs = queryString;
		  var qsa=qs.split('&');
		  for(var i=0; i<qsa.length; i++) {
			  var pair=qsa[i].split('=');
			  if (pair[0]) {
				  params[pair[0]]=decodeURIComponent(pair[1]);
			  }
		  }
	  }
	  else
	  {
	  	//return "accept";
	  }

	  var body = "accept";
	  var cmd = params.cmd;
	  var result;
	  Application.console.log("cmd = " + cmd);
	  switch(cmd) 
	  {
	  	case "setbreakpoint":
	  		//alert("setbreakpoint postdata = " + postdata);
	  		let file;
	  		let line;
	  		result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
	  		
	  		for(i=0;i<result.length; i++)
	  		{
	  			file = decodeURIComponent(result[i]["filename"]);
	  			line = parseInt(result[i]["line"]);
	  			Application.console.log("setbreakpoint file=" + file + " : " + "line = "+ line);
		  		if(file in Firebug.chromebug_eclipse.util.sourceFileMap)
		  		{
		  			var s = Firebug.chromebug_eclipse.util.sourceFileMap[file];
		  			FBL.fbs.setBreakpoint(s, line, null, Firebug.Debugger);
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
			  			
			  			//LOG("setbreakpoint file=" + file + "\n" + "line = "+ line);
			  		}
		  		}
	  		}
	    	break;
	    case "removebreakpoint":
	  		let file;
	  		let line;
	  		result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
	  		
	  		for(i=0;i<result.length; i++)
	  		{
	  			file = decodeURIComponent(result[i]["filename"]);
	  			line = parseInt(result[i]["line"]);
		  		FBL.fbs.clearBreakpoint(file, line);
	  		}	    	
	    	break;
	  	case "load":
	  		loadURI(params.file);
	  		break;
	  	case "open":
	  		window.open().home();
	  		//loadURI(params.file);
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
	  		Application.console.log("getvalues postdata = " + postdata);
	  		//alert("getvalues postdata = " + postdata);
	  		result = Firebug.chromebug_eclipseModle.util.XML2Obj.parseFromString(postdata);
	  		Application.console.log("getvalues result.length = " + result.length);
	  		if(result.length ==1)
	  		{
	  			var depth = parseInt(result[0]["depth"]);
	  			var valuename = result[0]["valuename"];
	  			var frame = Firebug.chromebug_eclipse.util.currnetFrame;
	  			
	  			Application.console.log("getvalues depth = " + depth);
	  			Application.console.log("getvalues valuename = " + valuename);
	  			Application.console.log("getvalues frame = " + frame);
	  			//if(valuename == 'this') depth++;
	  			
		  		for (let i = 0; i<depth; i++)
		  		{
		  			frame = frame.callingFrame;
		  		}
		  		//alert("getvalues params.name = " + params.name);
		  		//alert("getvalues id = " + id);
		  		//alert("getvalues frame = " + frame);
		  		body = Firebug.chromebug_eclipse.util.getValuesXML(frame, valuename);	
		  		//body = 
		  		//alert("getvalues = " + body);
		  		Application.console.log("getvalues body = " + body);
	  		}
	  		else
	  		{
	  			Application.console.log("getvalues result.length = 0");
	  		}
	  		break;
	  	case "terminate":
	  		Firebug.chromebug_eclipseModle.terminate();
	  		break;
	  	default:
	  		Application.console.log("default");
	  		break;
	  }  
	  return body;	
	}
});

var ecclient = {
	send:function(port, command, data)
	{
		try
		{
			var url = "http://localhost:" + port+ "/?cmd=" + command;
			
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
			
	        if(!data) data = "debuggeraccept";
			request.open('POST', url, false);
			//request.setRequestHeader("content-type","text/xml");
			request.send(data);
		    if(request.status == 200) {
		    	//LOG('request.responseText = ' + request.responseText);
		    }
		}
		catch(exc)
		{
			//LOG("send error = " + exc);
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

Firebug.registerModule(Firebug.chromebug_eclipseModle); 
Firebug.registerPanel(ChromeDebuggerPanel); 
}});


