﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AsControls.Parser {

    public class Lexer_test {

        public string Value { get; set; }
        public AsControls.Parser.Attribute Attr { get; set; }

        //public Tuple<int, int> OffsetLen;

        public Tuple<int, int, AsControls.Parser.Attribute> OffsetLenAttr;

        private TokenType tok;
        public LexerReader reader;

        private Rule resultRule;

        public bool isNextLine = false;

        public bool scisNextLine = false;

        private Dictionary<String, Rule> ruleDic = new Dictionary<String, Rule>();
        private Dictionary<String, MultiLineRule> multiruleEndDic = new Dictionary<String, MultiLineRule>();
        private Dictionary<String, Rule> keyWordRuleDic = new Dictionary<String, Rule>();

        private string ruleFirstKeys = string.Empty;
        private string ruleEndKeys = string.Empty;


        private Dictionary<String, ScanRule> scanRuleDic = new Dictionary<String, ScanRule>();
        private Dictionary<String, ScanRule> scanEndRuleDic = new Dictionary<String, ScanRule>();
        private string paruleStartKeys = string.Empty;
        private string paruleEndKeys = string.Empty;

        public void ClearRule() {
            ruleDic.Clear();
            multiruleEndDic.Clear();
            keyWordRuleDic.Clear();
            ruleFirstKeys = string.Empty;
            ruleEndKeys = string.Empty;
        }

        public void AddRule(List<Rule> rules) {
            foreach (var item in rules) {
                this.AddRule(item);
            }
        }
        public void AddRule(Rule rule) {
            if (rule is KeywordRule) {
                keyWordRuleDic.Add(rule.start, rule);
            }
            else {
                //if((rule as MultiLineRule)!=null){
                if (rule is MultiLineRule && !(rule is ScanRule)) {
                    multiruleEndDic.Add(((MultiLineRule)rule).end, (MultiLineRule)rule);
                }

                if ((rule as KeywordRules) != null) {
                    KeywordRules ks = rule as KeywordRules;
                    var rules = ks.Rules();
                    foreach (var item in rules) {
                        ruleDic.Add(item.start, item);
                    }
                }
                else {
                    ruleDic.Add(rule.start, rule);
                }


                foreach (var item in ruleDic.Values) {
                    if (!ruleFirstKeys.Contains(item.start[0])) {
                        ruleFirstKeys += item.start[0];
                    }
                }
                //foreach (var item in ruleDic.Values) {
                foreach (var item in multiruleEndDic.Values) {
                    MultiLineRule mrule = item as MultiLineRule;
                    if (mrule != null) {
                        if (!ruleEndKeys.Contains(mrule.end[0])) {
                            ruleEndKeys += mrule.end[0];
                        }
                    }
                }
            }
        }

        public void AddScanRule(ScanRule rule) {
            scanRuleDic.Add(rule.start, rule);
            scanEndRuleDic.Add(rule.end, rule);

            foreach (var item in scanRuleDic.Values) {
                if (!paruleStartKeys.Contains(item.start[0])) {
                    paruleStartKeys += item.start[0];
                }
                if (!paruleEndKeys.Contains(item.end[0])) {
                    paruleEndKeys += item.end[0];
                }
            }
        }

        public Rule getRule() {
            return resultRule;
        }

        public IText Src {
            get { return reader.Src; }
            set {
                if (reader == null) {
                    reader = new LexerReader(value);
                }
                else {
                    reader.Src = value;
                    tok = TokenType.TXT;
                    value = null;
                    isNextLine = false;
                }
            }
        }

        public TokenType token {
            get { return tok; }
        }

        public Lexer_test(IText src) {
            reader = new LexerReader(src);
        }

        public Lexer_test(LexerReader reader) {
            this.reader = reader;
        }

        public Lexer_test() {
            reader = new LexerReader();
            ruleFirstKeys = string.Empty;
            ruleEndKeys = string.Empty;
        }

        public int Offset {
            get {
                return reader.offset();
            }
        }

        public bool advance(Block preblock, Block curblock) {
            tok = TokenType.TXT;

            if (Src.Length == 0 && curblock.isLineHeadCmt == 1) {
                curblock.elem = preblock.elem;
                isNextLine = true;
            }

            if (Src.Length == 0 && curblock.scisLineHeadCmt == 1) {
                curblock.id = preblock.id;
                scisNextLine = true;
            }

            int c = reader.read();
            if (c == -1) {
                tok = TokenType.EOS;
                return false;
            }

            switch (c) {

                case ' ':
                case 0x3000:
                case '\t':

                default:
                    //TODO test
                    if (curblock.isLineHeadCmt == 0) {// && preblock.elem == curblock.elem) {
                        if (curblock.scisLineHeadCmt == 0) {
                            if (paruleStartKeys.Contains((char)c)) {
                                char fc = (char)c;
                                foreach (var item in scanRuleDic) {
                                    if (item.Key[0] == fc) {
                                        var len = item.Value.start.Length;
                                        if (Offset - 1 + len <= Src.Length) {
                                            var text = Src.Substring(Offset - 1, len);
                                            if (text.ToString() == item.Key) {
                                                //reader.unread();
                                                //lexSymbol(curblock);
                                                tok = TokenType.PartitionStart;
                                                if (curblock != null) {
                                                    curblock.id = item.Value.id;
                                                    scisNextLine = true;
                                                }
                                                reader.setoffset(Offset + text.Length);
                                                return true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else {
                            if (Offset - 1 == 0) {
                                while (c != -1) {
                                    if (paruleStartKeys.Contains((char)c)) {
                                        StringBuilder buf = new StringBuilder();
                                        while (c != -1) {
                                            buf.Append((char)c);

                                            if (scanEndRuleDic.ContainsKey(buf.ToString())) {
                                                if (preblock.id == scanEndRuleDic[buf.ToString()].id) {
                                                    var Eenelem = scanEndRuleDic[buf.ToString()];
                                                    tok = TokenType.PartitionEnd;

                                                    curblock.id = Eenelem.id;
                                                    scisNextLine = false;

                                                    reader.setoffset(Offset + buf.ToString().Length);

                                                    return true;
                                                    goto Finish;
                                                }
                                            }
                                            c = reader.read();
                                        }
                                    }
                                    c = reader.read();
                                }

                            Finish:

                                if (c == -1) {
                                    tok = TokenType.Partition;
                                    curblock.id = preblock.id;
                                    scisNextLine = true;
                                }
                                reader.setoffset(0);
                                c = reader.read();
                            }
                            else if (paruleStartKeys.Contains((char)c)) {
                                char fc = (char)c;
                                foreach (var item in scanRuleDic) {
                                    if (item.Key[0] == fc) {
                                        var len = item.Value.start.Length;
                                        if (Offset - 1 + len <= Src.Length) {
                                            var text = Src.Substring(Offset - 1, len);
                                            if (text.ToString() == item.Key) {
                                                tok = TokenType.PartitionStart;
                                                if (curblock != null) {
                                                    curblock.id = item.Key;
                                                    scisNextLine = true;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (curblock.isLineHeadCmt == 0) { //0: 行頭がブロックコメントの内部ではない
                        if (Char.IsDigit((char)c)) {
                            reader.unread();
                            lexDigit();
                        }
                        else if (Util.isIdentifierPart((char)c)) {
                            reader.unread();
                            lexKeyWord();
                        }
                        else {
                            reader.unread();
                            lexSymbol(curblock);
                        }
                    }
                    else { //1: 行頭がブロックコメントの内部

                        if (Offset - 1 == 0) {
                            {
                                StringBuilder buf = new StringBuilder();
                                while (true) {
                                    c = reader.read();
                                    if (c == -1) {
                                        //return;
                                        break;
                                    }
                                    buf.Append((char)c);

                                    string s = buf.ToString();
                                    if (preblock.elem.end == s) {
                                        if (ruleDic.ContainsKey(s)) {
                                            Rule rule = ruleDic[s];
                                            if (rule.Detected(s, reader)) {
                                                curblock.elem = rule as MultiLineRule;
                                                isNextLine = false;

                                                tok = rule.token;
                                                OffsetLenAttr = new Tuple<int, int, Attribute>(0, rule.getLen(s, reader), rule.attr);
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (c == -1 && preblock.elem != null && ruleDic.ContainsKey(preblock.elem.start)) {
                                    var enelem = ruleDic[preblock.elem.start];

                                    reader.setoffset(Src.Length);
                                    //enelem.startIndex = 0;
                                    //enelem.len = Src.Length;
                                    //tok = enelem.token;
                                    //resultRule = enelem;

                                    curblock.elem = preblock.elem;
                                    isNextLine = true;

                                    tok = TokenType.MultiLineStart;
                                    //Attr = enelem.attr;
                                    //OffsetLen = new Tuple<int, int>(0, Src.Length);
                                    OffsetLenAttr = new Tuple<int, int, Attribute>(0, Src.Length, enelem.attr);
                                }
                                break;
                            }

                        //    while (c != -1) {
                        //        if (ruleEndKeys.Contains((char)c)) {
                        //            StringBuilder buf = new StringBuilder();
                        //            while (c != -1) {
                        //                buf.Append((char)c);
                        //                if (preblock.elem.end == buf.ToString()) {
                        //                    if (multiruleEndDic.ContainsKey(buf.ToString())) {
                        //                        var Eenelem = multiruleEndDic[buf.ToString()];
                        //                        Eenelem.startIndex = 0;
                        //                        Eenelem.len = (Offset - Eenelem.startIndex);
                        //                        tok = Eenelem.token;
                        //                        resultRule = Eenelem;

                        //                        curblock.elem = Eenelem;
                        //                        isNextLine = false;

                        //                        tok = TokenType.MultiLineEnd;
                        //                        Attr = Eenelem.attr;
                        //                        OffsetLen = new Tuple<int, int>(0, Offset);
                        //                        OffsetLenAttr = new Tuple<int, int, Attribute>(0, Offset, Eenelem.attr);
                        //                        goto Finish;
                        //                    }
                        //                }
                        //                c = reader.read();
                        //            }
                        //        }
                        //        c = reader.read();
                        //    }

                        //Finish:

                        //    if (c == -1 && preblock.elem != null && ruleDic.ContainsKey(preblock.elem.start)) {
                        //        var enelem = ruleDic[preblock.elem.start];

                        //        reader.setoffset(Src.Length);
                        //        enelem.startIndex = 0;
                        //        enelem.len = Src.Length;
                        //        tok = enelem.token;
                        //        resultRule = enelem;

                        //        curblock.elem = preblock.elem;
                        //        isNextLine = true;

                        //        tok = TokenType.MultiLineStart;
                        //        Attr = enelem.attr;
                        //        OffsetLen = new Tuple<int, int>(0, Src.Length);
                        //        OffsetLenAttr = new Tuple<int, int, Attribute>(0, Src.Length, enelem.attr);
                        //    }
                        //    break;
                        }
                        else {
                            if (Char.IsDigit((char)c)) {
                                reader.unread();
                                lexDigit();
                            }
                            else if (Util.isIdentifierPart((char)c)) {
                                reader.unread();
                                lexKeyWord();
                            }
                            else {
                                reader.unread();
                                lexSymbol(curblock);
                            }
                        }
                    }
                    break;
            }
            return true;
        }

        private void skipWhiteSpace() {
            int c = reader.read();
            char ch = (char)c;

            while ((c != -1)
                    && (Char.IsWhiteSpace(ch) || (ch == '\t') || (ch == '\n') || (ch == '\r'))) {
                c = reader.read();
                ch = (char)c;
            }
            reader.unread();
        }

        private void lexDigit() {
            int num = 0;
            while (true) {
                int c = reader.read();
                if (c < 0) {
                    break;
                }
                if (!Char.IsDigit((char)c)) {
                    reader.unread();
                    break;
                }
                num = (num * 10) + (c - '0');
            }
            tok = TokenType.Number;
        }

        private void lexKeyWord() {
            int offset = reader.offset() - 1;

            StringBuilder buf = new StringBuilder();
            while (true) {
                int c = reader.read();
                if (c < 0) {
                    break;
                }
                if (!Util.isIdentifierPart((char)c)) {
                    reader.unread();
                    break;
                }
                buf.Append((char)c);
            }
            String s = buf.ToString();
            var value = s;

            if (keyWordRuleDic.ContainsKey(s)) {
                tok = TokenType.Keyword;
                var elem = keyWordRuleDic[s];
                elem.startIndex = offset;
                elem.len = value.Length;
                resultRule = elem;

                OffsetLenAttr = new Tuple<int, int, Attribute>(offset, value.Length, elem.attr);
            }
        }

        private void lexSymbol(Block curblock) {
            StringBuilder buf = new StringBuilder();
            int offset = reader.offset() - 1;

            while (true) {
                int c = reader.read();
                if (c == -1) {
                    return;
                }
                buf.Append((char)c);

                string s = buf.ToString();
                if (ruleDic.ContainsKey(s)) {
                    Rule rule = ruleDic[s];
                    if (rule.Detected(s, reader)) {
                        tok = rule.token;
                        Attr = rule.attr;
                        //Value = buf.ToString();
                        OffsetLen = new Tuple<int,int>(offset, rule.getLen(s, reader));
                        OffsetLenAttr = new Tuple<int, int, Attribute>(offset, rule.getLen(s, reader), rule.attr);
                        break;
                    }                   
                }
            }
        }
    }
}
