package org.addondev.editor.xul;

import javax.xml.transform.stax.StAXSource;

import org.addondev.editor.xul.formeditor.BrowserFormPage;
import org.addondev.editor.xul.formeditor.XULFormEditor;
import org.eclipse.core.resources.IFile;
import org.eclipse.core.runtime.IProgressMonitor;
import org.eclipse.ui.IEditorInput;
import org.eclipse.ui.IEditorPart;
import org.eclipse.ui.IEditorReference;
import org.eclipse.ui.IFileEditorInput;
import org.eclipse.ui.IWorkbenchPart;
import org.eclipse.ui.IWorkbenchWindow;
import org.eclipse.ui.PlatformUI;
import org.eclipse.ui.editors.text.TextEditor;

public class XULEditor extends TextEditor {

	private ColorManager colorManager;

	public XULEditor() {
		super();
		colorManager = new ColorManager();
		setSourceViewerConfiguration(new XULConfiguration(colorManager));
		setDocumentProvider(new XULDocumentProvider());
	}
	public void dispose() {
		colorManager.dispose();
		super.dispose();
	}
	@Override
	public void doSave(IProgressMonitor progressMonitor) {
		// TODO Auto-generated method stub
		super.doSave(progressMonitor);
		getXUL();
		
		StAXSource source;
		
	}

	private void getXUL()
	{
		IWorkbenchWindow[] windows = PlatformUI.getWorkbench().getWorkbenchWindows();
		for (IWorkbenchWindow iWorkbenchWindow : windows) {
			//iWorkbenchWindow.getActivePage().
			//IEditorReference[] editorref = iWorkbenchWindow.getActivePage().getEditorReferences();
			IEditorPart[] editorref = iWorkbenchWindow.getActivePage().getEditors();
			for (IEditorPart editorpart : editorref) {
			//for (IEditorReference iEditorReference : editorref) {
				//IEditorPart editorpart = iEditorReference.getEditor(false);
				if(editorpart instanceof XULFormEditor)
				{
					XULFormEditor xulformeditor = (XULFormEditor)editorpart;
					String text = getSourceViewer().getDocument().get();
					IEditorInput input = getEditorInput();
//					if(input instanceof IFileEditorInput)
//					{
//						xulformeditor.setFile(((IFileEditorInput)input).getFile());
//					}
					xulformeditor.settest(text);
//					BrowserFormPage formpage = (BrowserFormPage)xulformeditor.setActivePage(BrowserFormPage.ID);
//					
//					String text = getSourceViewer().getDocument().get();
//					formpage.setDocument(text);
//					
//					IEditorInput input = getEditorInput();
//					if(input instanceof IFileEditorInput)
//					{
//						
//						formpage.setFile(((IFileEditorInput)input).getFile());
//					}
					//iWorkbenchWindow.getActivePage().findView(viewId)
				}
			}
		}

	}
}
