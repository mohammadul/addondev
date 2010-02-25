package org.addondev.ui.editor.xml;

import org.eclipse.ui.editors.text.TextEditor;
import org.eclipse.ui.views.contentoutline.IContentOutlinePage;

public class XMLEditor extends TextEditor {

	//public static final String XML_EDIT_CONTEXT = "#AddonDevJavascriptEditContext";
	public static final String ID = "org.addondev.ui.editor.xml";
	
	private XMLOutlinePage outline;
	private ColorManager colorManager;

	public XMLEditor() {
		super();
		colorManager = new ColorManager();
		setSourceViewerConfiguration(new XMLConfiguration(colorManager));
		setDocumentProvider(new XMLDocumentProvider());
	}
	public void dispose() {
		colorManager.dispose();
		super.dispose();
	}
	
	@Override
	public Object getAdapter(Class adapter) {
		// TODO Auto-generated method stub
		if (IContentOutlinePage.class.equals(adapter)) 
		{
			if (outline == null) 
			{
				outline = new XMLOutlinePage(this);
			}
			return outline;
		}
		return super.getAdapter(adapter);
	}
}
