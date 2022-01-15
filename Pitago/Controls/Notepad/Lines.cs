using AvaloniaEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitago.Controls
{
    public partial class Notepad
    {
        private DocumentLine GetLine(int offset)
        {
            return _textEditor.Document.GetLineByOffset(offset);
        }

        private string GetLineText(DocumentLine line)
        {
            return _textEditor.Document.GetText(line.Offset, line.Length);
        }

        private DocumentLine GetCurrentLine()
        {
            return GetLine(_textEditor.SelectionStart + _textEditor.SelectionLength);
        }

        private List<DocumentLine> GetSelectionLines()
        {
            var startLine = GetLine(_textEditor.SelectionStart);
            var endLine = GetLine(_textEditor.SelectionStart + _textEditor.SelectionLength);

            var result = new List<DocumentLine>();
            while (startLine.Offset <= endLine.Offset)
            {
                result.Add(startLine);
                startLine = startLine.NextLine;
            }

            return result;
        }
    }
}
