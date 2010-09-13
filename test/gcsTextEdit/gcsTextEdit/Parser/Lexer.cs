﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AsControls.Parser {

    public class Attribute {
        public readonly Color color;
        public readonly bool islink;
        public readonly bool isimage;
        public readonly bool isbold;
        public readonly bool isunderline;

        public Attribute(Color color, bool islink, bool isimage, bool isbold, bool isunderline) {
            this.color = color;
            this.islink = islink;
            this.isimage = isimage;
            this.isbold = isbold;
            this.isunderline = isunderline;
        }
        public Attribute(Color color) {
            this.color = color;
        }
    }

    public enum TokenType {
        EOS,
        TXT, // 普通の字
        Enclose,
        EndLine,
        Line,
        Image,
        Keyword,
        Element,
        Number
    }

    public abstract class Element {
        public Attribute attr;
        public TokenType token;
        public string start { get; set; }
        public abstract int exer(Lexer lex);
        public int startIndex;
        public int len;
    }

    public class TextElement : Element {
        public TextElement(Attribute attr) {
            this.attr = attr;
            this.token = TokenType.TXT;
        }

        public override int exer(Lexer lex) {
            //throw new NotImplementedException();
            return 0;
        }
    }

    //[[..]]end
    public class EncloseElement :Element{
        public string end;
        public EncloseElement() {
            token = TokenType.Enclose;
        }
        public EncloseElement(string start, string end, Attribute attr) {
            this.start = start;
            this.end = end;
            this.attr = attr;
            token = TokenType.Enclose;
        }

        public override int exer(Lexer lex) {
            int offset = lex.reader.offset();
            int index = lex.reader.Src.IndexOf(end, offset);
            
            if (index < 0) return index;

            int endindex = lex.reader.Src.IndexOf(end, offset) + this.end.Length;
            return endindex;
        }
    }

    // //. ..\nend
    class EndLineElement :Element{

        public EndLineElement(string start, Attribute attr) {
            this.start = start;
            this.attr = attr;
            token = TokenType.EndLine;
        }
        public override int exer(Lexer lex) {
            //throw new NotImplementedException();
            int offset = lex.reader.offset();
            return lex.reader.Src.Length;
        }
    }

    // >>.. end
    class LineElement : Element {
        private Char[] c = new char[3];

        public LineElement(string start, Attribute attr) {
            this.start = start;
            this.attr = attr;
            token = TokenType.Line;
            c[0] = '\t';
            c[1] = ' ';
            c[2] = '　';
        }
        public override int exer(Lexer lex) {
            //throw new NotImplementedException();
            int offset = lex.reader.offset();
            string src = lex.reader.Src;
            int endindex = src.IndexOfAny(c, offset);
            //int index = lex.reader.src.Length - offset;
            if (endindex < 0) {
                endindex = src.Length;
            }
            else {
                endindex = src.Length - endindex;
            }
            return endindex;
        }
    }

    //##name
    class ImageElement : EncloseElement {

        public Size size;

        public ImageElement(string start, string end, Attribute attr) {
            this.start = start;
            this.end = end;
            this.attr = attr;
            token = TokenType.Image;
        }
    }

    //class BlockElement : Element {

    //}

    class KeywordElement : Element {
        public KeywordElement(string start, Attribute attr) {
            this.start = start;
            this.attr = attr;
            token = TokenType.Keyword;
        }

        public override int exer(Lexer lex) {
            return lex.reader.offset();
        }
    }


    public  class LexerReader {
        private bool unget_p = false;
        private int ch;
        private int cnt;
        private string src;
        public string Src {
            get { return src; }
            set{
                this.src = value;
                ch = 0;
                cnt = 0;
                unget_p = false;
            } 
        }

        public LexerReader(string src) {
            this.src = src;
            cnt = 0;
        }

        public LexerReader() {
            cnt = 0;
        }

        public int read() {
            if (unget_p) {
                unget_p = false;
            }
            else {
                if (cnt > src.Length - 1) {
                    ch = -1;
                }
                else {
                    ch = src[cnt];
                    cnt++;
                }
            }
            return ch;
        }

        public void unread() {
            unget_p = true;
        }

        public int offset() {
            return cnt;
        }

        public void setoffset(int offset) {
            cnt = offset;
        }
    }


    public class Lexer {

        private TokenType tok;
        private String value;
        public LexerReader reader;
        //private int offset;

        private Element resultElement;

        //private int preoffset=0;

        private Element defaultElement;
        private Dictionary<String, Element> elemDic = new Dictionary<String, Element>();
        private Dictionary<String, Element> keyWordElemDic = new Dictionary<String, Element>();


        private Dictionary<String, EncloseElement> encelemDic = new Dictionary<String, EncloseElement>();

        private Dictionary<String, EncloseElement> encendelemDic = new Dictionary<String, EncloseElement>();
        //private Dictionary<String, SingleLineAttribute> SingleLineattrDic = new Dictionary<String, SingleLineAttribute>();
        //private Dictionary<String, KeywordAttribute> keywordattrDic = new Dictionary<String, KeywordAttribute>();

        private string fkeys = string.Empty;

        private string fCncEndkeys = string.Empty;

        //public void AddElement(Element elem) {
        //    if (elem is KeywordElement) {
        //        keyWordElemDic.Add(elem.start, elem);
        //    } else {
        //        elemDic.Add(elem.start, elem);
        //    }

        //    string key=string.Empty;
        //    foreach (var item in elemDic.Values) {
        //        key += item.start[0];
        //    }
        //    fkeys = key;
        //}

        public void AddElement(Element elem) {
            if (elem is KeywordElement) {
                keyWordElemDic.Add(elem.start, elem);
            }
            else if (elem is EncloseElement) {
                encelemDic.Add(elem.start, (EncloseElement)elem);
                encendelemDic.Add(((EncloseElement)elem).end, (EncloseElement)elem);

                string key = string.Empty;
                foreach (var item in encendelemDic.Values) {
                    key += item.end[0];
                }
                fCncEndkeys = key;

                key = string.Empty;
                foreach (var item in encelemDic.Values) {
                    key += item.start[0];
                }
                fkeys = key;

            }else{
                elemDic.Add(elem.start, elem);

                string key = string.Empty;
                foreach (var item in elemDic.Values) {
                    key += item.start[0];
                }
                fkeys = key;
            }
        }

        public void AddDefaultElement(Element elem) {
            defaultElement = elem;
        }

        public Element getElement() {
            return resultElement;
        }

        public string Src {
            get { return reader.Src; }
            set {
                //preoffset = 0;
                if (reader == null) {
                    reader = new LexerReader(value);
                }
                else {
                    reader.Src = value;
                    //reader.setoffset(0);
                    tok = TokenType.TXT;
                    value = null;
                }
            }
        }

        public TokenType token {
            get { return tok; }
        }

        public Lexer(string src) {

            reader = new LexerReader(src);
        }

        public Lexer() {
            reader = new LexerReader();
        }

        public string Value {
            get { return value; }
        }

        public int Offset {
            get {
                return reader.offset();
            }
        }

        public Block Block {
            get;
            set;
        }

        public bool advance() {
            //skipWhiteSpace();
            //resultAttr = null;
            tok = TokenType.TXT;

            int c = reader.read();
            if (c == -1) {
                tok = TokenType.EOS;
                return false;
            }
            switch (c) {
                case ' ':
                    break;

                //case '　':
                case 0x3000:
                    break;

                case '\t':
                    break;

                default:
                    //if( (c>=33 && c<=47)
                    //    || (c >= 58 && c <= 74)
                    //    || (c >= 91 && c <= 96)
                    //    || (c >= 123 && c <= 126)){

                    ////if (Char.IsSymbol((char)c)) {
                    //    reader.unread(c);
                    //    //lexSymbol(((char)c).ToString());
                    //    lexSymbol();
                    //} else if (Char.IsDigit((char)c)) {
                    //    reader.unread(c);
                    //    lexDigit();

                    //} else {
                    //    reader.unread(c);
                    //    lexKeyWord();
                    //}
                    if (Char.IsDigit((char)c)) {
                        reader.unread();
                        lexDigit();
                    } else if(Util.isIdentifierPart((char)c)){
                        reader.unread();
                        lexKeyWord();
                    } else if(fkeys.Contains((char)c)){
                        //if (Char.IsSymbol((char)c)) {
                        reader.unread();
                        //lexSymbol(((char)c).ToString());
                        lexSymbol();
                    }
                    break;

            }
            return true;
        }

        public bool advance(Block block) {
            //skipWhiteSpace();
            //resultAttr = null;
            tok = TokenType.TXT;

            int c = reader.read();
            if (c == -1) {
                tok = TokenType.EOS;
                return false;
            }
            switch (c) {
                case ' ':
                    break;

                //case '　':
                case 0x3000:
                    break;

                case '\t':
                    break;

                default:
                    if (block.state == BlockState.all || block.state == BlockState.start || block.state == BlockState.end) {
                        //if (fCncEndkeys.Contains((char)c)) {
                        if(Src.IndexOf(block.elem.end)>=0){

                            var enelem = encelemDic[block.elem.start];

                            //if (Char.IsSymbol((char)c)) {
                            int index = Src.IndexOf(block.elem.end);
                            //reader.unread();
                            reader.setoffset(index+block.elem.end.Length+1);
                            //lexSymbol(((char)c).ToString());
                            //Block = EncEndlexSymbol(block);
                            enelem.startIndex = 0;
                            enelem.len = index + block.elem.end.Length;
                            tok = enelem.token;
                            resultElement = enelem;

                            //if(){
                            //    Block = new Block { elem = enelem, state= BlockState.all };
                            //}else{
                                Block = new Block { elem = enelem, state= BlockState.end };
                            //}
                        } else {
                             var enelem = encelemDic[block.elem.start];

                            Block = new Block{ elem= block.elem, state= BlockState.all};
                            reader.setoffset(Src.Length);
                            enelem.startIndex = 0;
                            enelem.len = Src.Length;
                            tok = enelem.token;
                            resultElement = enelem;

                            Block = new Block { elem = enelem, state = BlockState.all };
   
                        }
                    //}else if(block.state == BlockState.end){

                    } else {
                        if (Char.IsDigit((char)c)) {
                            reader.unread();
                            lexDigit();
                            Block = new Block();
                        }
                        else if (Util.isIdentifierPart((char)c)) {
                            reader.unread();
                            lexKeyWord();
                            Block = new Block();
                        }
                        else if (fkeys.Contains((char)c)) {
                            //if (Char.IsSymbol((char)c)) {
                            reader.unread();
                            //lexSymbol(((char)c).ToString());
                            lexSymbol();
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
            value = num.ToString();
            tok = TokenType.Number;		
        }	

        private void lexKeyWord() {
            //offset = reader.offset();
            int offset = reader.offset()-1;

            //tok = TokenType.Keyword;
            StringBuilder buf = new StringBuilder();
            while (true) {
                int c = reader.read();
                if (c < 0) {
                    // throw new Exception("ファイルの終わりに到達しました。");
                    //tok = TokenType.EOS;
                    break;
                }
                if (!Util.isIdentifierPart((char)c)) {
                    reader.unread();
                    break;
                }
                buf.Append((char)c);
            }
            String s = buf.ToString();
            value = s;

            if (keyWordElemDic.ContainsKey(s)) {
                //reader.setoffset(offset);
                //int offset = reader.offset() - value.Length;
                tok = TokenType.Keyword;
                var elem = keyWordElemDic[s];
                elem.startIndex = offset;
                elem.len = value.Length;
                resultElement = elem;
            }
        }

        private void lexSymbol() {
            StringBuilder buf = new StringBuilder();
            //if (initstr != null) buf.Append(initstr);
            int offset = reader.offset()-1;
            while (true) {

                if (elemDic.ContainsKey(buf.ToString())) {
                    
                    Element elem = elemDic[buf.ToString()];
                    //int offset = reader.offset()-elem.start.Length;
                    Block = new Block { elem = null, state = BlockState.no };

                    int index = elem.exer(this);
                    if (index < 0) {
                        //int ii = reader.offset();
                        reader.setoffset(reader.offset()+1);
                        reader.unread();
                        return;
                    }
                    //int offset = reader.offset() - value.Length;
                    value = reader.Src.Substring(offset, index - offset);
                    elem.startIndex = offset;
                    elem.len = value.Length;
                    tok = elem.token;
                    resultElement = elem;
                    int offse = reader.offset();
                    reader.setoffset(index);
                    //reader.unread(' ');
                    
                    return;
                }
                else if (encelemDic.ContainsKey(buf.ToString())) {
                    var elem = encelemDic[buf.ToString()];

                    int index = elem.exer(this);
                    if (index < 0) {
                        //int ii = reader.offset();
                        //reader.setoffset(reader.offset() + 1);
                        //reader.unread();

                        Block = new Block { elem = elem, state = BlockState.start };

                        elem.startIndex = offset;
                        elem.len = Src.Length-offset;
                        tok = elem.token;
                        resultElement = elem;

                        reader.setoffset(reader.Src.Length);
                        reader.unread();

                        return;
                    }
                    //int offset = reader.offset() - value.Length;
                    value = reader.Src.Substring(offset, index - offset);
                    elem.startIndex = offset;
                    elem.len = value.Length;
                    tok = elem.token;
                    resultElement = elem;
                    int offse = reader.offset();
                    reader.setoffset(index);
                    //reader.unread(' ');

                    Block = new Block { elem = elem, state = BlockState.start_end };

                    return;
                }

                int c = reader.read();
                if (c == -1) {
                    //tok = TokenType.EOS;
                    break;
                }
                buf.Append((char)c);
            }
        }

        private Block EncEndlexSymbol(Block block) {
            StringBuilder buf = new StringBuilder();
            //if (initstr != null) buf.Append(initstr);
            int offset = reader.offset() - 1;
            while (true) {

                if (encendelemDic.ContainsKey(buf.ToString()) && block.elem.end == buf.ToString()) {

                    EncloseElement elem = encendelemDic[buf.ToString()];
                    //int offset = reader.offset()-elem.start.Length;

                    //int offset = reader.offset() - value.Length;
                    value = reader.Src.Substring(0, reader.offset());
                    elem.startIndex = 0;
                    elem.len = value.Length;
                    tok = elem.token;
                    resultElement = elem;
                    int offse = reader.offset();
                    reader.setoffset(offse+1);
                    //reader.unread(' ');

                    return new Block { elem = elem, state= BlockState.end };
                }

                int c = reader.read();
                if (c == -1) {
                    //tok = TokenType.EOS;
                    break;
                }
                buf.Append((char)c);
            }
            return new Block { elem = block.elem, state = BlockState.all };
        }

    }
}
