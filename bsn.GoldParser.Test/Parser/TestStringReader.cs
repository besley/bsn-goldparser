// bsn GoldParser .NET Engine
// --------------------------
// 
// Copyright 2009, 2010 by Ars�ne von Wyss - avw@gmx.ch
// 
// Development has been supported by Sirius Technologies AG, Basel
// 
// Source:
// 
// https://bsn-goldparser.googlecode.com/hg/
// 
// License:
// 
// The library is distributed under the GNU Lesser General Public License:
// http://www.gnu.org/licenses/lgpl.html
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using System.Text;

namespace bsn.GoldParser.Parser {
	internal class TestStringReader: StringReader {
		private static string CreateTestString(int charCount) {
			StringBuilder builder = new StringBuilder(charCount);
			for (int i = 0; i < charCount; i++) {
				int ix = i%82;
				switch (ix) {
				case 0:
					builder.Append('\r');
					break;
				case 1:
					builder.Append('\n');
					break;
				default:
					builder.Append((char)(ix + 30));
					break;
				}
			}
			return builder.ToString();
		}

		private readonly string data;

		public TestStringReader(int charCount): this(CreateTestString(charCount)) {}

		public TestStringReader(string data): base(data) {
			this.data = data;
		}

		public char this[int index] {
			get {
				return data[index];
			}
		}

		public int Length {
			get {
				return data.Length;
			}
		}

		public override string ToString() {
	 		return data;
		}	
	}
}
