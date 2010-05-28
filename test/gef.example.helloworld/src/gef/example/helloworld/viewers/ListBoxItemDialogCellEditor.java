package gef.example.helloworld.viewers;

import java.util.List;

import gef.example.helloworld.model.AbstractElementModel;
import gef.example.helloworld.model.ListBoxModel;

import org.eclipse.jface.dialogs.IDialogConstants;
import org.eclipse.jface.viewers.DialogCellEditor;
import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Control;

public class ListBoxItemDialogCellEditor extends DialogCellEditor {

	private Class fClass;
	private AbstractElementModel flistenermodel;
	private String id;
	
	public ListBoxItemDialogCellEditor(Composite parent) {
		// TODO Auto-generated constructor stub
		 super(parent, SWT.NONE);
	}

	public void setClass(Class _class) {
		// TODO Auto-generated method stub
		this.fClass = _class;
	}

	public void setListenerModel(AbstractElementModel listenermodel) {
		// TODO Auto-generated method stub
		this.flistenermodel = listenermodel;
	}

	public void setID(String id) {
		// TODO Auto-generated method stub
		this.id = id;
	}
	
	@Override
	protected Object openDialogBox(Control cellEditorWindow) {
		// TODO Auto-generated method stub
		ListBoxItemDialog dialog = new ListBoxItemDialog(cellEditorWindow.getShell());
		dialog.setClass(fClass);
		
		int columnum = ((ListBoxModel)flistenermodel).getListcols().getChildren().size();
		
		dialog.setNumColum(columnum);
        Object value = getValue();
        if(value != null ){
        	
    		dialog.setValue((List) value);
        }
        
        int ret = dialog.open();
        if (ret == IDialogConstants.OK_ID) {
        	
        }
		return null;
	}
}
