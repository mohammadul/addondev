package org.addondev.ui.editor.javascript;

//import jp.addondev.parser.javascript.SimpleNode;


import org.addondev.parser.javascript.Node;
import org.eclipse.jface.viewers.LabelProvider;
import org.eclipse.swt.graphics.Image;

public class JavaScriptLabelProvider extends LabelProvider {

	@Override
	public String getText(Object element) {
		// TODO Auto-generated method stub
		//JavaScriptNode node = (JavaScriptNode)element;
		//SimpleNode node = SimpleNodeHelper.getIdentifierOfElement(n);
		//if(node != null)
		//	return node.getImage();
//		if(n.getNodeName().equals("Identifier"))
//		{
//			//return ((SimpleNode)n.jjtGetChild(0)).getImage();
//			return n.getImage();
//		}
		Node node = (Node)element;
		return node.getSymbol();
		
		//return super.getText(element);
	}

	@Override
	public Image getImage(Object element) {
		// TODO Auto-generated method stub
		return super.getImage(element);
	}

}
