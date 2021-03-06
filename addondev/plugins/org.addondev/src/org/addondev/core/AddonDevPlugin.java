package org.addondev.core;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

import org.addondev.parser.dtd.DTDMap;
import org.addondev.util.ChromeURLMap;
import org.eclipse.core.resources.IFile;
import org.eclipse.core.resources.IProject;
import org.eclipse.core.resources.IWorkspace;
import org.eclipse.core.resources.ResourcesPlugin;
import org.eclipse.core.runtime.CoreException;
import org.eclipse.core.runtime.Path;
import org.eclipse.jface.resource.ImageDescriptor;
import org.eclipse.jface.resource.ImageRegistry;
import org.eclipse.jface.text.templates.ContextTypeRegistry;
import org.eclipse.jface.text.templates.persistence.TemplateStore;
import org.eclipse.swt.graphics.Image;
import org.eclipse.ui.editors.text.templates.ContributionContextTypeRegistry;
import org.eclipse.ui.editors.text.templates.ContributionTemplateStore;
import org.eclipse.ui.plugin.AbstractUIPlugin;

public class AddonDevPlugin extends AbstractUIPlugin {
	
	public static final String PLUGIN_ID = "org.addondev"; 
	
	public static final String IMG_BP_ENABLE = "bp_enable";
	public static final String IMG_BP_DISABLE = "bp_disable.png";
	public static final String IMG_ADDON = "addondev";
	
    
		
	private static AddonDevPlugin plugin;
	
	public AddonDevPlugin(){
        super();
        plugin = this;
	}
	
	public static AddonDevPlugin getDefault() {
		// TODO Auto-generated method stub
		return plugin;
	}

	@Override
	protected void initializeImageRegistry(ImageRegistry registry) {
		registerImage(registry, IMG_BP_ENABLE, "icon/bp_enable.png");
		registerImage(registry, IMG_BP_DISABLE, "icon/bp_disable.png");
		registerImage(registry, IMG_ADDON, "icon/addon.png");
	}
	
	private void registerImage(ImageRegistry registry, String key, String path){
		URL url = getBundle().getEntry(path);
		if (url != null) {
			ImageDescriptor desc = ImageDescriptor.createFromURL(url);
			registry.put(key, desc);
		}
//		try {
//			
//			IPath path = new Path("icons/" + fileName);
//			URL url = find(path);
//			if (url != null) {
//				ImageDescriptor desc = ImageDescriptor.createFromURL(url);
//				registry.put(key, desc);
//			}
//		} catch (Exception e) {
//			e.printStackTrace();
//		}
	 }
	
	public Image getImage(String key) {
		return getImageRegistry().get(key);
	}
	
	public static IWorkspace getWorkspace() {
		return ResourcesPlugin.getWorkspace();
	}
    
    private HashMap<String, ChromeURLMap> fChromeURL = new HashMap<String, ChromeURLMap>();
    public ChromeURLMap getChromeURLMap(IProject project, boolean isupdate)
    {
		if(project != null)
		{
			//IFile file = project.getFile(ChromeURLMap.MANIFEST_FILENAME);
			String name = project.getName();
			if(fChromeURL.containsKey(name))
			{
				return fChromeURL.get(name);
			}
			else
			{
				ChromeURLMap map = new ChromeURLMap();
				try {
					//map.readManifest(file.getLocation());
					//map.readManifest(file);
					map.readManifest(project);
    				fChromeURL.put(name, map);
    				
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
					//map = null;
				} catch (CoreException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				return map;
			}
		}
		return null;
    }
    
    private HashMap<String, DTDMap> fDTDHashMap = new HashMap<String, DTDMap>();
    public Map<String, String> getEntityMap(IProject project, String chromeuri, String locale, boolean update)
    {
		if(project != null)
		{
			String name = project.getName();
			if(fDTDHashMap.containsKey(name))
			{
				if(fDTDHashMap.get(name).hasLocate(locale))
				{	
					return fDTDHashMap.get(name).getEntityMap(locale);
				}
				
			}
			else
			{
		    	ChromeURLMap chromeURLMap = getChromeURLMap(project, false);
		    	String lpath = chromeURLMap.convertChrome2Local(chromeuri);
		    	if(lpath != null)
		    	{
			    	File file  = new File(lpath);
			    	if(!file.exists()) return new HashMap<String, String>();;
			    	
			    	DTDMap dtdmap = new DTDMap();
			    	dtdmap.parse(locale, new Path(lpath));
			    	fDTDHashMap.put(name, dtdmap);
			    	
			    	return dtdmap.getEntityMap(locale);
		    	}
			}
		}
		
		return new HashMap<String, String>();

    }
}
