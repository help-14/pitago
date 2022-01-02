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
            _textEditor.KeyUp += textEditor_KeyUp;
            _textEditor.TextArea.TextEntered += textEditor_TextEntered;
            _textEditor.TextArea.TextEntering += textEditor_TextEntering;
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

        private void textEditor_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CalculateLine(GetCurrentLine().PreviousLine);
            }
        }

        private void _textEditor_LineCountChanged(object? sender, EventArgs e)
        {
            UpdatePreviewResultPosition();
        }

        void textEditor_TextEntering(object sender, TextInputEventArgs e)
        {
            //if (e.Text == null || _completionWindow == null) return;
            //if (e.Text.Length > 0)
            //{
            //    if (!char.IsLetterOrDigit(e.Text[0]))
            //    {
            //        //_completionWindow.CompletionList.RequestInsertion(e);
            //    }
            //}
            //_insightWindow?.Hide();
        }

        void textEditor_TextEntered(object sender, TextInputEventArgs e)
        {

            //if (e.Text == ".")
            //{
            //    _completionWindow = new CompletionWindow(_textEditor.TextArea);
            //    _completionWindow.Closed += (o, args) => _completionWindow = null;

            //    var data = _completionWindow.CompletionList.CompletionData;
            //    data.Add(new MyCompletionData("Item1"));
            //    data.Add(new MyCompletionData("Item2"));
            //    data.Add(new MyCompletionData("Item3"));
            //    data.Add(new MyCompletionData("Item4"));
            //    data.Add(new MyCompletionData("Item5"));
            //    data.Add(new MyCompletionData("Item6"));
            //    data.Add(new MyCompletionData("Item7"));
            //    data.Add(new MyCompletionData("Item8"));
            //    data.Add(new MyCompletionData("Item9"));
            //    data.Add(new MyCompletionData("Item10"));
            //    data.Add(new MyCompletionData("Item11"));
            //    data.Add(new MyCompletionData("Item12"));
            //    data.Add(new MyCompletionData("Item13"));


            //    _completionWindow.Show();
            //}
            //else if (e.Text == "(")
            //{
            //    _insightWindow = new OverloadInsightWindow(_textEditor.TextArea);
            //    _insightWindow.Closed += (o, args) => _insightWindow = null;

            //    _insightWindow.Provider = new MyOverloadProvider(new[]
            //    {
            //        ("Method1(int, string)", "Method1 description"),
            //        ("Method2(int)", "Method2 description"),
            //        ("Method3(string)", "Method3 description"),
            //    });

            //    _insightWindow.Show();
            //}
        }

    }
}