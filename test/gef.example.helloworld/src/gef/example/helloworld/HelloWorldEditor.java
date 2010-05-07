package gef.example.helloworld;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.EventObject;

import gef.example.helloworld.editparts.MyEditPartFactory;
import gef.example.helloworld.model.ContentsModel;
import gef.example.helloworld.model.ElementModel;
import gef.example.helloworld.model.GridModel;
import gef.example.helloworld.model.HBoxModel;
import gef.example.helloworld.model.LabelModel;
import gef.example.helloworld.model.RootModel;
import gef.example.helloworld.model.VBoxModel;
import gef.example.helloworld.model.WindowModel;
import gef.example.helloworld.parser.XULLoader;

import org.eclipse.core.resources.IFile;
import org.eclipse.core.resources.IMarker;
import org.eclipse.core.runtime.CoreException;
import org.eclipse.core.runtime.IProgressMonitor;
import org.eclipse.draw2d.geometry.Rectangle;
import org.eclipse.gef.DefaultEditDomain;
import org.eclipse.gef.GraphicalViewer;
import org.eclipse.gef.palette.CreationToolEntry;
import org.eclipse.gef.palette.MarqueeToolEntry;
import org.eclipse.gef.palette.PaletteDrawer;
import org.eclipse.gef.palette.PaletteGroup;
import org.eclipse.gef.palette.PaletteRoot;
import org.eclipse.gef.palette.SelectionToolEntry;
import org.eclipse.gef.palette.ToolEntry;
import org.eclipse.gef.requests.SimpleFactory;
import org.eclipse.gef.ui.parts.GraphicalEditorWithPalette;
import org.eclipse.jface.resource.ImageDescriptor;
import org.eclipse.jface.util.IPropertyChangeListener;
import org.eclipse.jface.util.PropertyChangeEvent;
import org.eclipse.jface.viewers.ISelection;
import org.eclipse.ui.IEditorInput;
import org.eclipse.ui.IEditorPart;
import org.eclipse.ui.IFileEditorInput;
import org.eclipse.ui.IWorkbenchPart;

public class HelloWorldEditor extends GraphicalEditorWithPalette {
	
	private RootModel root;
	
	public HelloWorldEditor() {

		setEditDomain(new DefaultEditDomain(this));
	}

	/* (非 Javadoc)
	 * @see org.eclipse.gef.ui.parts.GraphicalEditor#initializeGraphicalViewer()
	 */
	protected void initializeGraphicalViewer() {
		GraphicalViewer viewer = getGraphicalViewer();
		RootModel root = new RootModel();
		//WindowModel child1 = new WindowModel();
		//root.addChild(child1);
		viewer.setContents(root);
		
		IEditorInput input = getEditorInput();
		if (input instanceof IFileEditorInput) {
			IFile file = ((IFileEditorInput) input).getFile();

			try {
				XULLoader.loadXUL(file.getContents(), root);
				if(root.getChildren().size()>0){
					viewer.setContents(root.getChildren().get(0));
				}
			} catch (CoreException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
	
	/* (非 Javadoc)
	 * @see org.eclipse.ui.part.EditorPart#doSave(org.eclipse.core.runtime.IProgressMonitor)
	 */
	public void doSave(IProgressMonitor monitor) {
		
		RootModel parent = (RootModel) getGraphicalViewer().getContents().getModel();
		ElementModel model =  (ElementModel) parent.getChildren().get(0);
		String mm = model.toXML();
		getCommandStack().markSaveLocation();
	}

	/* (非 Javadoc)
	 * @see org.eclipse.ui.part.EditorPart#doSaveAs()
	 */
	public void doSaveAs() {

	}

	/* (非 Javadoc)
	 * @see org.eclipse.ui.part.EditorPart#gotoMarker(org.eclipse.core.resources.IMarker)
	 */
	public void gotoMarker(IMarker marker) {

	}

	/* (非 Javadoc)
	 * @see org.eclipse.ui.part.EditorPart#isDirty()
	 */
	public boolean isDirty() {
		//return false;
		return getCommandStack().isDirty();
	}

	@Override
	public void commandStackChanged(EventObject event) {
		// TODO Auto-generated method stub
		firePropertyChange(IEditorPart.PROP_DIRTY);
		
		super.commandStackChanged(event);
	}

	/* (非 Javadoc)
	 * @see org.eclipse.ui.part.EditorPart#isSaveAsAllowed()
	 */
	public boolean isSaveAsAllowed() {
		return false;
	}

	/* (非 Javadoc)
	 * @see org.eclipse.gef.ui.parts.GraphicalEditor#configureGraphicalViewer()
	 */
	protected void configureGraphicalViewer() {
		super.configureGraphicalViewer();

		GraphicalViewer viewer = getGraphicalViewer();
		// EditPartFactoryの作成と設定
		viewer.setEditPartFactory(new MyEditPartFactory());

	}

	/* (非 Javadoc)
	 * @see org.eclipse.gef.ui.parts.GraphicalEditorWithPalette#getPaletteRoot()
	 */
	protected PaletteRoot getPaletteRoot() {
		// パレットのルート
		PaletteRoot root = new PaletteRoot();

		// モデル作成以外のツールを格納するグループ
		PaletteGroup toolGroup = new PaletteGroup("ツール");

		// '選択' ツールの作成と追加
		ToolEntry tool = new SelectionToolEntry();
		toolGroup.add(tool);
		root.setDefaultEntry(tool); // デフォルトでアクティブになるツール

		// '囲み枠' ツールの作成と追加
		tool = new MarqueeToolEntry();
		toolGroup.add(tool);

		// モデルの作成を行うツールを格納するグループ
		PaletteDrawer drawer = new PaletteDrawer("作成");

		ImageDescriptor descriptor =
		  ImageDescriptor.createFromFile(
			HelloWorldEditor.class,
			"newModel.gif");

		// 'モデルの作成'ツールの作成と追加
		CreationToolEntry creationEntry =
		  new CreationToolEntry(
			"HelloModelの作成", // パレットに表示される文字列
			"モデル作成", // ツールチップ
			new SimpleFactory(LabelModel.class), // モデルを作成するファクトリ
			descriptor, // パレットに表示する16X16のイメージ
			descriptor);// パレットに表示する24X24のイメージ
		drawer.add(creationEntry);
		
		// 'モデルの作成'ツールの作成と追加
		CreationToolEntry creationVBoxEntry =
		  new CreationToolEntry(
			"VBoxの作成", // パレットに表示される文字列
			"モデル作成", // ツールチップ
			new SimpleFactory(VBoxModel.class), // モデルを作成するファクトリ
			descriptor, // パレットに表示する16X16のイメージ
			descriptor);// パレットに表示する24X24のイメージ
		drawer.add(creationVBoxEntry);
		
		// 'モデルの作成'ツールの作成と追加
		CreationToolEntry creationHBoxEntry =
		  new CreationToolEntry(
			"HBoxの作成", // パレットに表示される文字列
			"モデル作成", // ツールチップ
			new SimpleFactory(HBoxModel.class), // モデルを作成するファクトリ
			descriptor, // パレットに表示する16X16のイメージ
			descriptor);// パレットに表示する24X24のイメージ
		drawer.add(creationHBoxEntry);
		
		CreationToolEntry creationGridEntry =
			  new CreationToolEntry(
				"Gridの作成", // パレットに表示される文字列
				"モデル作成", // ツールチップ
				new SimpleFactory(GridModel.class), // モデルを作成するファクトリ
				descriptor, // パレットに表示する16X16のイメージ
				descriptor);// パレットに表示する24X24のイメージ
			drawer.add(creationGridEntry);

		// 作成した2つのグループをルートに追加
		root.add(toolGroup);
		root.add(drawer);

		return root;
	}

	@Override
	public void selectionChanged(IWorkbenchPart part, ISelection selection) {
		// TODO Auto-generated method stub
		   // getActivePage()がnullの場合は無視する
	    if (part.getSite().getWorkbenchWindow().getActivePage() == null)
	      return;

		super.selectionChanged(part, selection);
	}

}
