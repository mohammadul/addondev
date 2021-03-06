package gef.example.helloworld.editparts;

import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.text.html.parser.ContentModel;

import gef.example.helloworld.figure.TabBoxFigure;
import gef.example.helloworld.figure.TabPanelFigure;
import gef.example.helloworld.model.BoxModel;
import gef.example.helloworld.model.AbstractElementModel;
import gef.example.helloworld.model.ContentsModel;
import gef.example.helloworld.model.PrefpaneModel;
import gef.example.helloworld.model.PrefwindowModel;

import org.eclipse.draw2d.ColorConstants;
import org.eclipse.draw2d.IFigure;
import org.eclipse.draw2d.Label;
import org.eclipse.draw2d.MouseEvent;
import org.eclipse.draw2d.MouseListener;
import org.eclipse.draw2d.geometry.Dimension;
import org.eclipse.draw2d.geometry.Rectangle;
import org.eclipse.gef.EditPart;

public class PrefwindowEditPart extends AbstractTabEditPart {

	@Override
	protected IFigure createFigure() {
		// TODO Auto-generated method stub
		//h=50
		TabBoxFigure tabbox = (TabBoxFigure)super.createFigure();
		tabbox.getTabs().setPreferredSize(new Dimension(-1, 50));
		//tabbox.getCanvas().setBackgroundColor(c)		
		
		//BoxModel model =  (BoxModel)getModel();
		//model.addChild(new PrefpaneModel());
		//model.addChild(new PrefpaneModel());
		List panels = new ArrayList();
		PrefwindowModel model = (PrefwindowModel)getModel();
		for (Object obj : model.getChildren()) {
			if(obj instanceof PrefpaneModel){
				//panels.add(obj);
				if(model.getPanels().indexOf(obj) == -1){
					model.getPanels().add(obj);
				}
			}
		}
		//model.setPanels(panels);
//		refreshChildren();
//		if(model.getPrefPanesModel().getChildren().size()>0){
//			model.firePropertyChange("prefnanes", null, model.getPrefPanesModel().getChildren());
//		}
//		
		return tabbox;
	}
	
	private boolean lock= false;
	
	@Override
	public void propertyChange(PropertyChangeEvent evt) {
		// TODO Auto-generated method stub
		super.propertyChange(evt);
		
		if(evt.getPropertyName().equals(ContentsModel.P_ADD_CHILD) && !lock){
			PrefwindowModel model =  (PrefwindowModel)getModel();
			model.getPanels().add(evt.getNewValue());
		}else if(evt.getPropertyName().equals(ContentsModel.P_REMOVE_CHILD) && !lock){
			PrefwindowModel model =  (PrefwindowModel)getModel();
			model.getPanels().remove(evt.getNewValue());
		}else if(evt.getPropertyName().equals("prefpanes")){
			PrefwindowModel model =  (PrefwindowModel)getModel();
			//AbstractElementModel tabs = (AbstractElementModel)model.getPropertyValue("prefnanes");
			//AbstractElementModel tabs = model.getPrefPanesModel();
//			AbstractElementModel tabs = model.getPrefPanesModel();
			List list = (List)evt.getNewValue();
//			while(tabs.getChildren().size() > 0){
//				tabs.getChildren().remove(0);
//			}
//			
//			for (Object object : list) {
//				tabs.getChildren().add(object);
//			}		
			
//			model.removeAllChild();
//			for (Object object : list) {
//				model.addChild((AbstractElementModel) object);
//			}
			lock = true;
			List panels = model.getPanels();
			for (Object panel : panels) {
				model.removeChild((AbstractElementModel) panel);		
			}
			
			for (Object object : list) {
				model.addChild((AbstractElementModel) object);
			}
			lock = false;
			//refreshTabs();
		}else if(evt.getPropertyName().equals("resize")){
			Rectangle rect = (Rectangle)evt.getNewValue();
			IFigure figure = getFigure();
			//Rectangle rewrect = figure.getBounds()
			//figure.setBounds(rect);
			//figure.setSize(figure.getBounds().width+rect.width, figure.getBounds().height+rect.height);

			//figure.setPreferredSize(new Dimension(figure.getBounds().width, figure.getBounds().height));
			int w = figure.getPreferredSize().width +rect.width;
			int h = figure.getPreferredSize().height+rect.height;
			figure.setPreferredSize(new Dimension(w, h));
			refreshVisuals(); 
			//refreshChildren();
		}
	}

	@Override
	protected Label getNewTab(TabPanelFigure child, EditPart childEditPart) {
		// TODO Auto-generated method stub
		return new TabButton(child, childEditPart);
	}
	public class TabButton extends Label implements MouseListener {
        private TabPanelFigure pane;
        //private IFigure pane;
        private EditPart childEditPart;
        //private Label tabLabel;
        
        public TabButton(TabPanelFigure pane, EditPart childEditPart) {
            this.pane = pane;
            this.childEditPart = childEditPart;
            //TabModel paneModel = pane.getModel();
            //setLabelAlignment(PositionConstants.BOTTOM);
            setOpaque(true);         
            //setBorder(new TabPanelLineBorder());
            
            ((PrefpaneModel)childEditPart.getModel()).addPropertyChangeListener(new PropertyChangeListener() {
				
				@Override
				public void propertyChange(PropertyChangeEvent arg0) {
					// TODO Auto-generated method stub
					refreshTabs();
				}
			});
            
            String message = ((PrefpaneModel)childEditPart.getModel()).getTabLabel();
            
            setText(message);
            addMouseListener(this);

            setBackgroundColor(ColorConstants.lightGray);
            setForegroundColor(ColorConstants.black);
            enableOtherButtons();

        }

        @Override
		public void setText(String s) {
			// TODO Auto-generated method stub
			super.setText(s);
			//((TabPanelModel)childEditPart.getModel()).a
		}

		@Override
		public String getText() {
			// TODO Auto-generated method stub
        	String message = ((PrefpaneModel)childEditPart.getModel()).getTabLabel();
			return message;
		}

		protected void showEp() {
            activateChilds(this.childEditPart);
        }
        
        public void update() {
            String message = getText();
            //this.tabLabel.setText(message);
            this.setText(message);
        }

		@Override
		public void mouseDoubleClicked(MouseEvent me) {
			// TODO Auto-generated method stub
			
		}

		@Override
		public void mousePressed(MouseEvent me) {
			// TODO Auto-generated method stub
            hideChilds();
            showEp();
            this.pane.setVisible(true);
            enableOtherButtons();
            setBackgroundColor(ColorConstants.white);
            setForegroundColor(ColorConstants.black);
            //setBorder(selectedBorder);
            getLayout().repaint();			
		}

		@Override
		public void mouseReleased(MouseEvent me) {
			// TODO Auto-generated method stub
			
		}

    }
}
