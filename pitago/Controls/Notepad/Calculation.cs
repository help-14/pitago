using Avalonia;
using Avalonia.Media;
using AvaloniaEdit.Document;
using AvaloniaEdit.Editing;
using System.Linq;

namespace Pitago.Controls
{
    public partial class Notepad
    {
        private void CalculateLine(DocumentLine line)
        {
            var text = _textEditor.Document.GetText(line.Offset, line.Length);

            if (string.IsNullOrEmpty(text?.Trim())) return;
            var result = _calProcessor.Process(text);
            if (string.IsNullOrEmpty(result)) return;

            _textEditor.Document.Insert(line.EndOffset, result);
        }

        private void CalculateLines(DocumentLine[] lines)
        {
            foreach(var line in lines)
            {
                CalculateLine(line);
            }
        }

        private void PreviewCalculation(DocumentLine line)
        {
            ClearPreviewResult();
            UpdatePreviewResultPosition();

            for (var i = 0; i < _textEditor.Document.Lines.Count; i++)
            {
                if (_textEditor.Document.Lines[i].ToString().Trim().Length == 0) continue;
                var rectBox = new Avalonia.Controls.Border();
                rectBox.BorderBrush = new SolidColorBrush(Colors.Red);
                rectBox.BorderThickness = new Thickness(1);

                var currentLine = _textEditor.Document.GetLineByOffset(_textEditor.Document.Lines[i].Offset);
                var rect = AvaloniaEdit.Rendering.BackgroundGeometryBuilder.GetRectsForSegment(_textEditor.TextArea.TextView, currentLine).FirstOrDefault();
                rectBox.Margin = new Thickness(rect.X, rect.Y, 0, 0);
                rectBox.Width = rect.Width;
                rectBox.Height = rect.Height;
                _resultBox.Children.Add(rectBox);
            }
        }

        private void ClearPreviewResult()
        {
            _resultBox.Children.Clear();
        }
    }
}