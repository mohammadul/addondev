package org.addondev.parser.javascript;

import java.util.List;

public interface IFunction {
	public JsNode Run(ScopeManager scopemanager, String name, List<JsNode> args);
}