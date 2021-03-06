FBL.ns(function chromebug() { with (FBL) {

	
	const Cc = Components.classes;
	const Ci = Components.interfaces;

	const IOService = Cc["@mozilla.org/network/io-service;1"].createInstance(Ci.nsIIOService);
	
if (!Firebug.chromebug_eclipse)
    Firebug.chromebug_eclipse = {};

Firebug.chromebug_eclipse.util = {
	breakpointMap:{},	
	currnetFrame:null,	
	currentStackTrace:null,	
	sourceFileMap:{},
	
	ignoreVars :
	{
    "__firebug__": 1,
    "eval": 1,
    "java": 1,
    "sun": 1,
    "Packages": 1,
    "JavaArray": 1,
    "JavaMember": 1,
    "JavaObject": 1,
    "JavaClass": 1,
    "JavaPackage": 1,
    "_firebug": 1,
    "_FirebugConsole": 1,
    "_FirebugCommandLine": 1
	},
	
	getObjList : function(obj){
		var en="";
		for(var name in obj)
		{
			try{
				en += "name = " + name +" : " + obj[name] + "\n"; 
			}catch(exc){
				en += "name = " + name +" ERROR";
			}
		}
		return en;
	},
	
	Stringformat : function() {
		var args = [];
		for(var i = 0; i < arguments.length; i++) args[i] = arguments[i];
		var format = args.shift();
		
		var reg = /\{((\d)|([1-9]\d+))\}/g;
		return format.replace( reg, function() {
			var index = Number( arguments[1] );
			var result = args[ index ];
			if( typeof( result ) == "undefined" )
				throw new Error( "arguments[ " + index + " ] is undefined." );
				
				return result;
				} );
		},
  
  		hasProperties : function(ob){
  			try{
        		for (var name in ob)
            		return true;
    		} catch (exc) {}
    			return false;
  		},
  		
  		hasChildren : function(value){
  			var valueType = typeof(value);
  			return  this.hasProperties(value) && !(value instanceof FBL.ErrorCopy) && 
								(valueType == "function" || (valueType == "object" && value != null) 
							|| (valueType == "string" && value.length > Firebug.stringCropLength));
	},
	
	getLocals : function(frame){
     	//var localsxml = this.Stringformat("<file name=\"{0}\" function=\"{1}\" line=\"{2}\"/>", encodeURIComponent(frame.script.fileName), frame.script.functionName, frame.line);
    	//var localsxml = "";
    	var name = "";
    	var value =null;
        var valueType = null;
        var hasCh = null;    	
    	
        let json = {};
		json.cmd = "getvalues";
		json.propertylist = [];
        
    	if (frame && frame.isValid)
        {
        	name = "this";
            value = frame.thisValue.getWrappedValue();
            if(value == null) value = "null";
            valueType = typeof(value);
            hasCh = this.hasChildren(value); 	
//        	localsxml += this.Stringformat("<value name=\"{0}\" type=\"{1}\" value=\"{2}\" hasChildren=\"{3}\" />", 
//        			name, valueType, encodeURIComponent(value), hasCh);
        	
		 	let property = {};
		 	property["name"] = name;
		 	property["type"] = valueType;
		 	property["value"] = value;
		 	property["hasChildren"] = hasCh + "";
		 	json.propertylist.push(property);
 
        	//name = "scopeChain";
            //value = this.generateScopeChain(frame.scope);
            //if(value == null) value = "null";
            //valueType = typeof(value);
            //hasCh = this.hasChildren(value);
            
            //alert("getLocalsXML value = " + value);
            //var Application = Components.classes["@mozilla.org/fuel/application;1"].getService(Components.interfaces.fuelIApplication);
            //Application.console.log("getLocalsXML value = " + value);
            //Application.console.log("getLocalsXML valueType = " + valueType);
            //Application.console.log("getLocalsXML hasCh = " + hasCh);
            
         	//localsxml += this.Stringformat("<value name=\"{0}\" type=\"{1}\" value=\"{2}\" hasChildren=\"{3}\"/>", name, valueType, encodeURIComponent(value), hasCh);
         	//localsxml += "<value valuename=\"" + name + "\" type=\""+ valueType + "\" value=\"" + value + "\" hasChildren=\"" +hasCh+ "\"/>";
        }
    	
        var listValue = {value: null}, lengthValue = {value: 0};
        frame.scope.getProperties(listValue, lengthValue);
        //LOG("getLocalsXML lengthValue.value = " + lengthValue.value);
        //var props = [], funcs = [];
        for (var i = 0; i < lengthValue.value; ++i)
        {
            var prop = listValue.value[i];
            name = prop.name.getWrappedValue();
            //if (Firebug.ignoreVars[name] == 1)
            //    continue;
            if (this.ignoreVars[name] == 1)
            	continue;

            value = prop.value.getWrappedValue();
            if(value == null)
            {
            	value = "null";
            }
            
            valueType = typeof(value);
            hasCh = this.hasChildren(value);
//            localsxml += this.Stringformat("<value name=\"{0}\" type=\"{1}\" value=\"{2}\" hasChildren=\"{3}\" />", 
//            		name, valueType, encodeURIComponent(value), hasCh);
            
		 	let property = {};
		 	property["name"] = name;
		 	property["type"] = valueType;
		 	property["value"] = value;
		 	property["hasChildren"] = hasCh + "";
		 	json.propertylist.push(property);
            
        }
        //return "<xml> " + localsxml + " </xml>";
        return json;
	},
	
	getvalues : function (parent, names, level)
	{
		if(level >= names.length) return parent;
	
		var listValue = {value: null}, lengthValue = {value: 0};
		if(level == 0)
		{
			Application.console.log("getvalues parent = " + parent);
			Application.console.log("getvalues level = " + level);
			parent.getProperties(listValue, lengthValue);

			var props = [], funcs = [];
			Application.console.log("getvalues lengthValue.value = " + lengthValue.value +" level = " +  level);
			//alert("names[level] = " + names[level]);
			for (var i = 0; i < lengthValue.value; ++i)
			{
				var prop = listValue.value[i];
				var name = prop.name.getWrappedValue();
				
				Application.console.log("getvalues prop = " + prop);
				Application.console.log("getvalues name = " + name);
				
				if (this.ignoreVars[name] == 1)
				    continue;
				    
				if(names[level].indexOf(name) == 0 && names[level].length == name.length)
				{
					Application.console.log("names[level] == name = " + name);
					//alert("1names[level] == name : " + names[level] + " == " + name);
					return this.getvalues(prop.value.getWrappedValue(), names, level+1);
					//return this.getvalues(prop.value, names, level+1);
				}
			}      
		}
		else
		{
//			if (parent.wrappedJSObject)
//            	var insecureObject = parent.wrappedJSObject;
//        	else
//            	var insecureObject = parent;
            
			//var insecureObject = parent;
			Application.console.log(" else parent = " + parent + " : level = " +  level);
			for (var name in parent)
			{		
				if(names[level] == name)
				{
					//var n = 
					//alert("2names[level] == name : " + names[level] + " == " + name);
					return this.getvalues(parent[name], names,  level+1);
				}
			}
		}
	},
	
	getValues : function(frame, namepath){  
		//var valuesxml = "";
		var names = [];
		
		if(namepath=="")
		{	
			return this.getLocals(frame);
		}
		else
		{
	  		if(namepath.indexOf(".") >=0 )
	  			names = namepath.split(".");
	  		else
	  		{
	  			names.push(namepath);
	  		}
	  		
	  		var values;
	  		
	  		if(names[0] == "this")
	  		{
  				//namepath = "Hello";
  				Application.console.log("getValuesXML names[0] == this namepath = " + namepath);
  				var thisVar = frame.thisValue.getWrappedValue();
  				//var test = ["this","_panel"];
  				//values = getMembers(thisVar, names, 1);
  				values = this.getvalues(thisVar, names, 1); 
//  				Application.console.log("###members values = " + values.toString());
//  				for(key in values)
//  				{
//  					Application.console.log("###values key = " + key + " : " + values[key]);
//  				}  			
	  		}
	  		else
	  		{
	        	values = this.getvalues(frame.scope, names, 0);  	
	        	Application.console.log("getvaluesXML names[0] != this namepath = " + namepath + " : values = " + values );

//				for(key in value)
//				{
//					Application.console.log("###value key = " + key + " : " + value[key]);
//				} 
	  		}
	        //alert("getValuesXML values = " + values);
	       
			if (values.wrappedJSObject)
            	var insecureObject = values.wrappedJSObject;
        	else
            	var insecureObject = values;
            	
			
	        let json = {};
			json.cmd = "getvalues";
			json.propertylist = [];   
            for (var name in insecureObject)
            {
            	if (this.ignoreVars[name] == 1)
            		continue;
            	
            	var value;
            	try
            	{
            		value = insecureObject[name];
            		//Application.console.log("getvaluesXML name = " + name + " : value = " + value );
            		
            	}
            	catch (exc)
            	{
            		Application.console.log("getvaluesXML exp name = " + name + " : " + exc);
            	}
            	
            	try
            	{
            		if(value != undefined)
            		{
            			var valueType = typeof(value);
            			var hasCh = this.hasChildren(value);
//            			valuesxml += this.Stringformat("<value name=\"{0}\" type=\"{1}\" value=\"{2}\" hasChildren=\"{3}\" />", 
//            					name, valueType, encodeURIComponent(value), hasCh); 
            			
            		 	let property = {};
            		 	property["name"] = name;
            		 	property["type"] = valueType;
            		 	property["value"] = value;
            		 	property["hasChildren"] = hasCh + "";
            		 	json.propertylist.push(property);
            		}
            	}
            	catch (exc2)
            	{
            		Application.console.log("getvaluesXML exp2 name = " + name + " : " + exc2);
            	}
            	
            	
            }
            //Application.console.log("getvaluesXML namepath = " + namepath + " : valuesxml = " + valuesxml);
	        //return "<xml> " + valuesxml + " </xml>"; 	
            return json;
		}
	},
	
	getValue : function(frame, namepath)
	{
		var names = [];
		
		if(namepath=="")
		{	
			return this.getLocals(frame);
		}
		else
		{
	  		if(namepath.indexOf(".") >=0 )
	  			names = namepath.split(".");
	  		else
	  		{
	  			names.push(namepath);
	  		}
	  		
	  		var value;
	  		
	  		if(names[0] == "this")
	  		{
  				Application.console.log("getValuesXML names[0] == this namepath = " + namepath);
  				var thisVar = frame.thisValue.getWrappedValue();
  				//value = getMembers(thisVar, names, 1);
  				value = this.getvalues(thisVar, names, 1); 
  				Application.console.log("###members value = " + value.toString());
  			
	  		}
	  		else
	  		{
	  			value = this.getvalues(frame.scope, names, 0);  	
	        	Application.console.log("getvalueXML names[0] != this namepath = " + namepath + " : value = " + value );

	  		}
	        //alert("getValuesXML values = " + values);
	       
//			if (value.wrappedJSObject)
//            	var insecureObject = value.wrappedJSObject;
//        	else
//            	var insecureObject = value;
//			
//			value = insecureObject;
			var valueType = typeof(value);
			var hasCh = this.hasChildren(value);
			
//			var valuesxml= this.Stringformat("<value name=\"{0}\" type=\"{1}\" value=\"{2}\" hasChildren=\"{3}\" />", 
//					namepath, valueType, encodeURIComponent(value), hasCh);			
//            //Application.console.log("getvalueXML namepath = " + namepath + " : valuesxml = " + valuesxml);
//	        return "<xml> " + valuesxml + " </xml>"; 	
			
	        let json = {};
			json.cmd = "getvalue";
			json.propertylist = [];        
		 	let property = {};
		 	property["name"] = namepath;
		 	property["type"] = valueType;
		 	property["value"] = value;
		 	property["hasChildren"] = hasCh + "";
		 	json.propertylist.push(property);
		 	
		 	return json;
		}		
	},
	
	getStackFrames : function(){
		//var stackframesxml = "";
		//Application.console.log("getStackFramesXML this.currentStackTrace = " + this.currentStackTrace);
		let json = {};
		json.cmd = "getstackframes";
		json.propertylist = [];
		
		for(var i=0; i<this.currentStackTrace.frames.length; i++)
		{	
		 	var stackframe = this.currentStackTrace.frames[i];
//		 	for(var key in stackframe)
//		 	{
//		 		Application.console.log("getStackFramesXML key = " + key + " : " + stackframe[key]);
////		 		if(key == 'script')
////		 		{
////		 			var con = stackframe[key];
////		 			for(var key2 in con)
////		 			{
////		 				Application.console.log("getStackFramesXML key2 = " + key2 + " : " + con[key2]);
////		 			}
////		 		}
//		 	}
		 	//Application.console.log("getStackFramesXML stackframe.href = " + stackframe.href);
		 	//Application.console.log("getStackFramesXML stackframe.lineNo = " + stackframe.lineNo);
		 	//Application.console.log("getStackFramesXML stackframe");
		 	//Application.console.log("getStackFramesXML !path Firebug.SourceCache = " + Firebug.SourceCache);
		 	var path = this.getFilePathFromURL(stackframe.href);
		 	if(path == null) 
		 	{
		 		continue;
		 	}
		 	var fn;
		 	if(path.indexOf('jar:file://') == 0)
		 	{
		 		fn = stackframe.fn;
		 	}
		 	//Application.console.log("getStackFramesXML stackframe.fn = " +  stackframe.fn);
		 	if(fn == undefined)
		 	{
		 		fn = "";
		 	}

		 	//Application.console.log("stackframe.script.functionName = " + stackframe.script.functionName);
		 	//var functionname = FBL.getFunctionName(stackframe.script, stackframe.context, stackframe);
		 	var functionname = stackframe.script.functionName;
		 	
//		 	stackframesxml += this.Stringformat("<stackframe depth=\"{0}\" url=\"{1}\" filename=\"{2}\" functionname=\"{3}\" line=\"{4}\" fn=\"{5}\" />", 
//		 			i, 
//		 			encodeURIComponent(stackframe.href), 
//		 			encodeURIComponent(path), 
//		 			functionname, 
//		 			//stackframe.lineNo,
//		 			stackframe.line,
//		 			encodeURIComponent(fn));
		 	let property = {};
		 	property["depth"] = i + "";
		 	property["url"] = stackframe.href;
		 	property["filename"] = path;
		 	property["functionname"] = functionname;
		 	property["line"] = stackframe.line + "";
		 	property["fn"] = fn;
		 	json.propertylist.push(property);

		 }
		 //return "<xml> " + stackframesxml + " </xml>";	
		return json;
	},
	
	generateScopeChain: function (scope)
	{
        var ret = [];
        while (scope) {
            var scopeVars;
            // getWrappedValue will not contain any variables for closure
            // scopes, so we want to special case this to get all variables
            // in all cases.
            if (scope.jsClassName == "Call") {
                scopeVars = {};
                var listValue = {value: null}, lengthValue = {value: 0};
                scope.getProperties(listValue, lengthValue);

                for (var i = 0; i < lengthValue.value; ++i)
                {
                    var prop = listValue.value[i];
                    var name = prop.name.getWrappedValue();
                    if (this.ignoreVars[name] == 1)
                        continue;

                    scopeVars[name] = prop.value.getWrappedValue();
                }
            } else {
                scopeVars = scope.getWrappedValue();
            }
            
            if (!scopeVars.hasOwnProperty("toString")) {
                (function() {
                    var className = scope.jsClassName;
                    scopeVars.toString = function() {
                        return (className + " Scope");
                    };
                })();
            }            
            
            ret.push(scopeVars);
            scope = scope.jsParent;
        }
        
        
        ret.toString = function() {
            return ("Scope Chain");
        };        
        
        return ret;
    },
    
    getFilePathFromURL : function(aURI) 
    {
    	
   		if (aURI.indexOf('file://') == 0) 
		{
   			Application.console.log("file:// getFilePathFromURL aURI = " + aURI);
			return aURI;
		}
    	
    	try {	
    		var chromeurl;
    		
    		if (!aURI) aURI = '';
    	
    		if (aURI.indexOf('chrome://') == 0) 
    		{
    			//chrome://chromebug/content/chromebug.xul -> chrome://chromebug_eclipse/content/chromebug_eclipse.js
    			if(aURI.indexOf(' -> ') == 0)
    			{
    				var index = aURI.indexOf('chrome://', 1);
    				if(index > 0)
    				{
    					aURI = aURI.slice(index);
    					//Application.console.log("convertChromeURL chromeurl -> url = " + aURI);
    				}
    			}
//    			var uri =
//    		        Components.classes["@mozilla.org/network/standard-url;1"]
//    		            .createInstance(Components.interfaces.nsIURI);
//    		    uri.spec = aURI;
				var uri = Components.classes["@mozilla.org/network/io-service;1"]
					.getService(Components.interfaces.nsIIOService)
					.newURI(aURI, null, null);
    			var ChromeRegistry = Cc["@mozilla.org/chrome/chrome-registry;1"].getService(Ci.nsIChromeRegistry);
    			//aURI = ChromeRegistry.convertChromeURL(IOService.newURI(aURI, null, null));
    			chromeurl = ChromeRegistry.convertChromeURL(uri).spec;
    			Application.console.log("convertChromeURL chromeurl = " + chromeurl);
    		}
    	
    		//jar:file:///D:/program/firefox36/chrome/browser.jar!/content/browser/browser.xul
    		if (chromeurl.indexOf('jar:file://') == 0) 
    		{
    			return chromeurl;
    		}   		
    		
    		//var file = Components.classes["@mozilla.org/file/local;1"]
    		//                              .createInstance(Components.interfaces.nsILocalFile);
    		//         file.initWithPath(aURI.spec);

    		
    		var fileHandler = IOService.getProtocolHandler('file')
    							.QueryInterface(Ci.nsIFileProtocolHandler);
    				
    		return fileHandler.getFileFromURLSpec(chromeurl).path; //error
    	
    	}catch(e){
    		Application.console.log("getFilePathFromURL aURI = " + aURI + " error = " + e);
    	}
    	return null;
    }
};

function getMembers(object, names, level)
{
	var members = {};
	//Application.console.log("getMembers");
    if (object.wrappedJSObject)
        var insecureObject = object.wrappedJSObject;
    else
        var insecureObject = object;	

    for (var name in insecureObject)  // enumeration is safe
    {
    	if(level > names.length)
    	{
    		break;
    	}
    	
        var val;
        try
        {
            val = insecureObject[name];  // getter is safe
            members[name] = val;
            //var hasChildren();
            //Application.console.log("getMembers name = " + name +" : " + val);
            
            if(names[level] == name)
            	return getMembers(val, names, level+1);
        }
        catch (exc)
        {
            // Sometimes we get exceptions trying to access certain members
            //if (FBTrace.DBG_ERRORS && FBTrace.DBG_DOM)
            //    FBTrace.sysout("dom.getMembers cannot access "+name, exc);
        }    	
    }
    
    return members;
}

}});


