// (C) 2010 Ars�ne von Wyss / bsn
using System;
using System.Text.RegularExpressions;

namespace bsn.GoldParser.Grammar {
	/// <summary>
	/// Represents a terminal or nonterminal symbol used by the Deterministic
	/// Finite Automata (DFA) and LR Parser. 
	/// </summary>
	/// <remarks>
	/// Symbols can be either terminals (which represent a class of 
	/// tokens - such as identifiers) or nonterminals (which represent 
	/// the rules and structures of the grammar).  Terminal symbols fall 
	/// into several categories for use by the GOLD Parser Engine 
	/// which are enumerated in <c>SymbolKind</c> enumeration.
	/// </remarks>
	public class Symbol: GrammarObject<Symbol> {
		private static readonly Regex rxEscape = new Regex(@"[^\w']+|'", RegexOptions.CultureInvariant|RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture);
		private static readonly Regex rxXml = new Regex(@"(^[^:_a-z]|(?<!^)[^:_a-z0-9\-\.])+", RegexOptions.CultureInvariant|RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture);

		public static string FormatTerminalSymbol(string source) {
			return rxEscape.Replace(source, match => match.Value == "'" ? "''" : '\''+match.Value+'\'');
		}

		private readonly SymbolKind kind; // type of the symbol
		private readonly string name; // name of the symbol
		private string text; // printable representation of symbol
		private string xmlName;

		/// <summary>
		/// Creates a new instance of <c>Symbol</c> class.
		/// </summary>
		/// <param name="index">Symbol index in symbol table.</param>
		/// <param name="name">Name of the symbol.</param>
		/// <param name="kind">Type of the symbol.</param>
		internal Symbol(CompiledGrammar owner, int index, string name, SymbolKind kind): base(owner, index) {
			this.name = string.Intern(name);
			this.kind = kind;
		}

		/// <summary>
		/// Returns an enumerated data type that denotes
		/// the class of symbols that the object belongs to.
		/// </summary>
		public SymbolKind Kind {
			get {
				return kind;
			}
		}

		/// <summary>
		/// Returns the name of the symbol.
		/// </summary>
		public string Name {
			get {
				return name;
			}
		}

		///<summary>
		/// Gat the symbol name usable as XML name
		///</summary>
		public string XmlName {
			get {
				if (xmlName == null) {
					xmlName = rxXml.Replace(name, "");
					if (xmlName.Length == 0) {
						xmlName = "_";
					}
				}
				return xmlName;
			}
		}

		/// <summary>
		/// Returns the text representation of the symbol.
		/// In the case of nonterminals, the name is delimited by angle brackets,
		/// special terminals are delimited by parenthesis
		/// and terminals are delimited by single quotes 
		/// (if special characters are present).
		/// </summary>
		/// <returns>String representation of symbol.</returns>
		public override string ToString() {
			if (text == null) {
				switch (Kind) {
				case SymbolKind.Nonterminal:
					text = '<'+Name+'>';
					break;
				case SymbolKind.Terminal:
					text = FormatTerminalSymbol(Name);
					break;
				default:
					text = '('+Name+')';
					break;
				}
			}
			return text;
		}
	}
}
