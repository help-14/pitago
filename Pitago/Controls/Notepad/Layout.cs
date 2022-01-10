using Avalonia;
using Avalonia.Layout;
using AvaloniaEdit.Editing;

namespace Pitago.Controls
{
    public partial class Notepad
    {

        private void UpdatePreviewResultPosition()
        {
            var controls = _textEditor.TextArea.LeftMargins;
            double leftmargin = _textEditor.Padding.Left;
            foreach (var control in controls)
            {
                if (control is LineNumberMargin)
                    leftmargin += ((LineNumberMargin)control).DesiredSize.Width;
                else if (control is Avalonia.Controls.Shapes.Line)
                    leftmargin += ((Avalonia.Controls.Shapes.Line)control).DesiredSize.Width;
            }
            _resultBox.Margin = new Thickness(leftmargin, _textEditor.Padding.Top, _textEditor.Padding.Right, _textEditor.Padding.Bottom);
        }

        private void OnEffectiveViewportChanged(object? sender, EffectiveViewportChangedEventArgs e)
        {
            UpdatePreviewResultPosition();
        }

    }
}
