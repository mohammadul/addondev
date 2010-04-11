package org.addondev.parser.javascript;

import java.util.HashMap;


public class JsNode {
	
	private JsNode parent;
	private HashMap<String, JsNode> fSymbalTable; 

	private EnumNode fNodeType;
	private boolean isParam;
	
	private String fName;
	private int fOffset;
	private int fEndOffset;

	private JsDocParser fJsDocParser;
	private String fJsDoc;	
	private String fReturnType;	
	
	public boolean isParam() {
		return isParam;
	}

	public void setParam(boolean isParam) {
		this.isParam = isParam;
	}

	public int getEndoffset() {
		return fEndOffset;
	}

	public void setEndoffset(int endoffset) {
		this.fEndOffset = endoffset;
	}
	
	public String getfJsDoc() {
		return fJsDoc;
	}

	public void setfJsDoc(String fJsDoc) {
		this.fJsDoc = fJsDoc;
	}

	public JsNode getClone(JsNode p)
	{
		JsNode node = new JsNode(p, fNodeType, fName, fOffset);
		node.setfJsDoc(fJsDoc);
		return node;
	}

	public JsNode(JsNode parent, EnumNode nodetype, String name, int offset)
	{
		this.parent = parent;
		this.fName = name;
		this.fOffset = offset;	
		this.fNodeType = nodetype;
		this.fReturnType = null;
		this.isParam = false;
		
		fSymbalTable = new HashMap<String, JsNode>();
	}
	
	public JsNode(JsNode parent, EnumNode nodetype, String name, String returntype, int offset)
	{
		this.parent = parent;
		this.fName = name;
		this.fOffset = offset;
		this.fNodeType = nodetype;
		this.fReturnType = returntype;
		this.isParam = false;
		
		fSymbalTable = new HashMap<String, JsNode>();
	}
	
	public String getName()
	{
		return fName;
	}
	
	public EnumNode getNodeType() {
		return fNodeType;
	}

	public void setNodeType(EnumNode nodetype) {
		this.fNodeType = nodetype;
	}
	
	public JsNode getChild(String name) {
		return fSymbalTable.get(name);
	}
	
	public int getOffset()
	{
		return fOffset;
	}
	public void setOffset(int offset)
	{
		this.fOffset = offset;
	}
	
	public void addChildNode(JsNode node)
	{
		if(!fSymbalTable.containsKey(node))
			fSymbalTable.put(node.getName(), node);
	}
	
	public JsNode getParent()
	{
		return parent;
	}
	
	public String toString(String prefix) { return prefix + fNodeType; }
	
	public void dump(String prefix) {
		System.out.println(toString(prefix) + " : " + fName + " returntype=" + getReturnType());//" s:e= " + s + ":" + e);
		for (JsNode node : fSymbalTable.values()) {
			node.dump(prefix + " ");
		}
	}
	
	public boolean hasChildNode()
	{
		return fSymbalTable.size()>0?true:false;
	}
	
	public boolean hasChildNode(String name)
	{
		return fSymbalTable.containsKey(name);
	}
	
	public JsNode[] getChildNodes() {
		return fSymbalTable.values().toArray(new JsNode[fSymbalTable.size()]);
	}
	
	public HashMap<String, JsNode> getSymbalTable()
	{
		return fSymbalTable;
	}
	
	public void setSymbalTable(HashMap<String, JsNode> table)
	{
		fSymbalTable.clear();
		fSymbalTable = table;
	}
	
	public void setReturnType(String type)
	{
		fReturnType = type;
	}
	public String getReturnType()
	{
		if(fReturnType != null) return fReturnType;
		
		if(fJsDocParser == null)
		{
			fJsDocParser = new JsDocParser();
			fJsDocParser.parse(fJsDoc);
			fReturnType = fJsDocParser.getType();
		}
		
		return fReturnType;
	}
	
	public String getParamType(String paramname)
	{
		if(fJsDocParser == null)
		{
			fJsDocParser = new JsDocParser();
			fJsDocParser.parse(fJsDoc);
		}
		
		return fJsDocParser.getParamType(paramname);		
	}
}
