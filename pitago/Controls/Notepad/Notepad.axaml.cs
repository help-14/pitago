using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using AvaloniaEdit.CodeCompletion;
using System;
using System.Linq;
using Avalonia.Input;
using Avalonia.Interactivity;
using Pitago.Calculation;

namespace Pitago.Controls
{
    public partial class Notepad : UserControl
    {
        private TextEditor _textEditor;
        private Canvas _resultBox;
        private CompletionWindow _completionWindow;
        private OverloadInsightWindow _insightWindow;

        private CalculationProcessor _calProcessor;

        public Notepad()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _textEditor = this.FindControl<TextEditor>("Editor");
            _textEditor.TextArea.TextEntered += OnTextEntered;
            _textEditor.Document.LineCountChanged += _textEditor_LineCountChanged;

            this.EffectiveViewportChanged += OnEffectiveViewportChanged;

            //_textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JavaScript");
            _resultBox = this.FindControl<Canvas>("ResultBox");

            var line = _textEditor?.TextArea.LeftMargins.FirstOrDefault(margin => margin is Avalonia.Controls.Shapes.Line) as Avalonia.Controls.Shapes.Line;
            if(line != null) line.Margin = new Thickness(10, 0, 10, 0);

            this.AddHandler(PointerWheelChangedEvent, (o, i) =>
            {
                if (i.KeyModifiers != KeyModifiers.Control) return;
                if (i.Delta.Y > 0) _textEditor.FontSize++;
                else _textEditor.FontSize = _textEditor.FontSize > 1 ? _textEditor.FontSize - 1 : 1;
            }, RoutingStrategies.Bubble, true);

            _calProcessor = new CalculationProcessor();
        }

        private void _textEditor_LineCountChanged(object? sender, EventArgs e)
        {
            UpdatePreviewResultPosition();
        }

        private void OnTextEntered(object sender, TextInputEventArgs e)
        {
            IsChangesSaved = false;

            var currentLine = GetCurrentLine();
            if (e.Text == "\n")
            {
                CalculateLine(currentLine.PreviousLine);
                ClearPreviewResult();
            }
            else
                PreviewCalculation(currentLine);
        }

    }
}