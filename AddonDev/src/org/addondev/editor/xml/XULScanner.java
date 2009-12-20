package org.addondev.editor.xml;

import org.eclipse.jface.text.rules.*;
import org.eclipse.jface.text.*;

public class XULScanner extends RuleBasedScanner {

	public XULScanner(ColorManager manager) {
		IToken procInstr =
			new Token(
				new TextAttribute(
					manager.getColor(IXULColorConstants.PROC_INSTR)));

		IRule[] rules = new IRule[2];
		//Add rule for processing instructions
		rules[0] = new SingleLineRule("<?", "?>", procInstr);
		// Add generic whitespace rule.
		rules[1] = new WhitespaceRule(new XULWhitespaceDetector());

		setRules(rules);
	}
}
