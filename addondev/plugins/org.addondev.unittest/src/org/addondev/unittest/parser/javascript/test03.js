
//var ary = [];
////var num = 10;
//var st = "t";
//
//var MyObject = 
//{
////    message3: null,  
//    hello3: function (arg0, arg1){
//		//arg0=100;
//		this.h= 10;
//	
//	}
//}

//////
//////MyObject.hello3("b");
////MyObject.message3=st;
////MyObject.add = {};
//
////var st_l = st.length;
//
//

//Components2 = function()
//{
//	
//	//this.tesy = 10;
//	var tth=10;
//}
//Components2.prototype =
//{
//	test:function()
//	{
//		this.gg = 0;
//	}
//}
//
////var c2 = new Components2();
//
//var b.c = 100;

//var gb = new Components2();
//gb.tesy;
////var Components = 
////{
////	/**
////	 * @type    createInstanceComponents
////	 * @memberOf   Components
////	*/ 	
////	classes:function(){}
////}
//
var nsIIOService = 
{
  message3: null,
  hello3: function (arg0)
  {
	//this.ll=0;
  }
}

function Components(){};
/**
 * @memberOf   Components
*/ 	
Components.classes = function(){};

/**
 * @type    interfaces
 * @memberOf   Components
*/ 	
Components.classes.createInstance = function(){};

/**
 * @type    service
 * @memberOf   Components
*/
Components.classes.getService = function(){};

//const Components.classes;
//const Ci = Components.interfaces;
var ioService = Components.classes["@mozilla.org/network/io-service;1"].createInstance(Components.interfaces.nsIIOService);



////
//function func0(arg0)
//{
//	this.h0=0;
//	this.h0=10;
//	h0=3;
//	
//	this.h1=0;
//	arg0=0;
//	arg=p;
//	arg=100;
//	var ttt=0;
//	ttt = 100;
//}
//
//func0("e");

//
//MyObject3.test={
//		nn:null
//}
//MyObject3.prototype = 
//{
//    message3: null,
//    hello3: function (arg0){
//    }
//}
//
//MyObject3.test.add = {};

//function Accelimation(obj, prop, to, time, zip, unit) {
//  this.obj = obj;
//  this.prop = prop;
//}
//
//Accelimation = function(o) {
//	this.test0 = o;
//	this.test1 = 1;
//	//var hh=0;
//};
//
//Accelimation._add = function(o) {
//	this.test = 1;
//};
//
//Accelimation.prototype.start = function() {
//		var tt=0;
//  this.t0 = Time;
//  this.t1 = Time;
//  Accelimation._add(this);
//  //var hh2=0;
//};


//
//function ArrayEnumerator(array)
//{
//	var tt=0;
//	this.index = 0;
//	this.index2 = 0;
//	//this.array = array;
//	this.hasMoreElements = function()
//	{
//		//return (this.index < array.length);
//	}
//	this.getNext = function()
//	{
//		//return this.array[++this.index];
//	}
//}

//var test0, tes1, test2;



//Firebug.Debugger = extend(Firebug.ActivableModule,
//{
//	fbs: fbs,
//	
//    initialize: function()
//    {
//        //this.nsICryptoHash = Components.interfaces["nsICryptoHash"];
//        //this.debuggerName =  window.location.href+"--"+FBL.getUniqueId(); /*@explore*/
//        //this.toString = function() { return this.debuggerName; } /*@explore*/ //bug
//        //this.hash_service = CCSV("@mozilla.org/security/hash;1", "nsICryptoHash");
//
//        //this.wrappedJSObject = this;  // how we communicate with fbs
//        this.panelName = "script";
//
////        Firebug.broadcast = function encapsulateFBSBroadcast(message, args)
////        {
////            fbs.broadcast(message, args);
////        }
////        
////        this.onFunctionCall = bind(this.onFunctionCall, this);  
////        Firebug.ActivableModule.initialize.apply(this, arguments);
//    },
//    
//    getCurrentFrameKeys: function()
//    {
//		this.hhhh=200;
//		var tt = 10;
//    },
//    
////    focusWatch: function(context)
////    {
////        if (context.detached)
////            context.chrome.focus();
////        else
////            Firebug.toggleBar(true);
////
////        context.chrome.selectPanel("script");
////
////        var watchPanel = context.getPanel("watches", true);
////        if (watchPanel)
////        {
////        	Firebug.CommandLine.isReadyElsePreparing({title:"reaady", tf:function(){var p = tr;}});
////            watchPanel.editNewWatch();
////        }
////    }
//});
//
//var name = Firebug.Debugger.panelName;


myExt.ns(function() { 
	with (myExt.LIB) 
{

	var m = "A";
	dump("myExtension.Module2 initialization " + getCurrentURI() + "\n");

}});



//var stacklink_106ec9de_7db3_40c6_93c2_39563e25a8d6 = {};
//(function(){
//	
//const Cc = Components.classes;
//const Ci = Components.interfaces;
//
//const SAVEFILE = '';
//const version = '0.1.0';
//
//var viewtypeAry = ['start','center','end','wrap'];
//
//var $ = stacklink_106ec9de_7db3_40c6_93c2_39563e25a8d6;
//var $_ = 'stacklink_106ec9de_7db3_40c6_93c2_39563e25a8d6';
//
//$.stackpanel = {
//	isItemAppended:false,
//	
//	load: function()
//	{
//
//		if(this.save_restore)
//		{
//			try
//			{
//				var file = Cc["@mozilla.org/file/directory_service;1"].getService(Ci.nsIProperties).get("ProfD", Ci.nsILocalFile);
//				file.append('stacklink.js');
//				if(file.exists())
//				{
//					var stritems = $.util.read(file);
//					if (typeof(JSON) == "undefined") {
//					  Components.utils.import("resource://gre/modules/JSON.jsm");
//					  JSON.parse = JSON.fromString;
//					  JSON.stringify = JSON.toString;
//					}
//
//					this.tmpitems = JSON.parse(stritems);	
//				}
//			}
//			catch(ex)
//			{
//				Components.utils.reportError("load restor error : " + ex);
//				this.tmpitems = null;
//			}
//		}
//	}
//}
//window.addEventListener("load", function(e) { $.stackpanel.load(e); }, false);
//})();

//CmdUtils.CreateCommand({
//	   name:'macro',
//	   author: { name: "Kurt Cagle", email: "kurt@oreilly.com"}
//	});