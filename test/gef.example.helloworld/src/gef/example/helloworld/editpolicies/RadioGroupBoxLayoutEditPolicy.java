package gef.example.helloworld.editpolicies;

import gef.example.helloworld.editparts.RadioEditPart;
import gef.example.helloworld.model.ElementModel;
import gef.example.helloworld.model.RadioModel;

import org.eclipse.gef.EditPart;

public class RadioGroupBoxLayoutEditPolicy extends FilterBoxLayoutEditPolicy {

	@Override
	protected boolean isFilter(EditPart editpart) {
		// TODO Auto-generated method stub
		return !(editpart instanceof RadioEditPart);
	}

	@Override
	protected boolean isFilter(ElementModel model) {
		// TODO Auto-generated method stub
		return !(model instanceof RadioModel);
	}

}
