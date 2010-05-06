package gef.example.helloworld.editparts;

import gef.example.helloworld.figure.ElementFigure;
import gef.example.helloworld.model.AbstractModel;
import gef.example.helloworld.model.ElementModel;
import gef.example.helloworld.model.HBoxModel;

import java.beans.PropertyChangeListener;
import java.util.ArrayList;
import java.util.List;

import org.eclipse.draw2d.IFigure;
import org.eclipse.draw2d.geometry.Dimension;
import org.eclipse.gef.editparts.AbstractGraphicalEditPart;

abstract public class EditPartWithListener
	extends AbstractGraphicalEditPart
	implements PropertyChangeListener {

	public void activate() {
		super.activate();
		// 自身をリスナとしてモデルに登録
		 ((AbstractModel) getModel()).addPropertyChangeListener(this);
	}

	public void deactivate() {
		super.deactivate();
		// モデルから削除
		 ((AbstractModel) getModel()).removePropertyChangeListener(this);
	}
	
	protected List getModelChildren() {
		return ((ElementModel) getModel()).getChildren();
	}
	
	public void resizeChildren(){}
	
	public void resizeWidth(){
		//int w = getFigure().getSize().width;
		int w = ((AbstractGraphicalEditPart)getParent()).getFigure().getPreferredSize().width;
		double sumflex=0;
		double sumzerofilexw=0;
		
		List children = getChildren();
		for (Object object : children) {
			ElementModel elem = (ElementModel)((EditPartWithListener)object).getModel();
			//Object jjj = elem.getPropertyValue(ElementModel.ATTR_FLEX);
			int flex = Integer.parseInt(elem.getPropertyValue(ElementModel.ATTR_FLEX).toString());
			sumflex += flex;
			if(flex==0){
				ElementFigure figuer = (ElementFigure)((EditPartWithListener)object).getFigure();
				//figuer.setSize(figuer.getDefaultWidth(), figuer.getDefaultHeight());
				figuer.setPreferredSize(figuer.getDefaultWidth(), figuer.getDefaultHeight());
				sumzerofilexw += figuer.getSize().width;
			}
		}
		w -= sumzerofilexw;
		for (Object object : children) {
			ElementModel elem = (ElementModel)((EditPartWithListener)object).getModel();
			int flex = Integer.parseInt(elem.getPropertyValue(ElementModel.ATTR_FLEX).toString());
			if(flex>0){
				int newwidth = (int) (flex/sumflex*w);
				IFigure figuer = ((EditPartWithListener)object).getFigure();
				figuer.setPreferredSize(new Dimension(newwidth, figuer.getSize().height));
				((EditPartWithListener)object).resizeWidth();
				
			}
		}		
	}
	
	
	
}