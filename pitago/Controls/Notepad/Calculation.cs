using Avalonia;
using Avalonia.Media;
using AvaloniaEdit.Document;
using AvaloniaEdit.Editing;
using AvaloniaEdit.Rendering;
using System.Linq;

namespace Pitago.Controls
{
    public partial class Notepad
    {
        private string CalculateLineResult(DocumentLine line)
        {
            var text = _textEditor.Document.GetText(line.Offset, line.Length);
            if (string.IsNullOrEmpty(text?.Trim())) return String.Empty;
            return _calProcessor.Process(text);
        }

        private void CalculateLine(DocumentLine line)
        {
            var result = CalculateLineResult(line);
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
            //UpdatePreviewResultPosition();

            var resultText = CalculateLineResult(line);
            var rect = BackgroundGeometryBuilder.GetRectsForSegment(_textEditor.TextArea.TextView, line).FirstOrDefault();
            if (rect == null) return;

            var label = new Avalonia.Controls.TextBlock()
            {
                Text = resultText,
                Foreground = new SolidColorBrush(Colors.Gray),
                FontFamily = _textEditor.FontFamily,
                FontSize = _textEditor.FontSize,
                Margin = new Thickness(_textEditor.Padding.Left + rect.X + rect.Width, rect.Y, 0, 0)
            };
            _resultBox.Children.Add(label);
        }

        public void CalculateDocument()
        {
            _textEditor.Text = _calProcessor.ProcessDocument(_textEditor.Text);
        }

        private void ClearPreviewResult()
        {
            _resultBox.Children.Clear();
        }
    }
}