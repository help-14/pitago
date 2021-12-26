using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using AvaloniaEdit;
using AvaloniaEdit.CodeCompletion;
using AvaloniaEdit.Document;
using AvaloniaEdit.Editing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Input;
using Avalonia.Interactivity;
using AvaloniaEdit.Highlighting;
using Avalonia.Media;

namespace Pitago.Controls
{
    public class Notepad : UserControl
    {
        private TextEditor _textEditor;
        private Canvas _resultBox;
        private CompletionWindow _completionWindow;
        private OverloadInsightWindow _insightWindow;

        public Notepad()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _textEditor = this.FindControl<TextEditor>("Editor");
            _textEditor.TextArea.TextEntered += textEditor_TextEntered;
            _textEditor.TextArea.TextEntering += textEditor_TextEntering;
            //_textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
            _textEditor.Document.LineCountChanged += _textEditor_LineCountChanged;
            
            _resultBox = this.FindControl<Canvas>("ResultBox");

            var line = _textEditor?.TextArea.LeftMargins.FirstOrDefault(margin => margin is Avalonia.Controls.Shapes.Line) as Avalonia.Controls.Shapes.Line;
            if(line != null) line.Margin = new Thickness(10, 0, 10, 0);

            this.AddHandler(PointerWheelChangedEvent, (o, i) =>
            {
                if (i.KeyModifiers != KeyModifiers.Control) return;
                if (i.Delta.Y > 0) _textEditor.FontSize++;
                else _textEditor.FontSize = _textEditor.FontSize > 1 ? _textEditor.FontSize - 1 : 1;
            }, RoutingStrategies.Bubble, true);
        }

        private void _textEditor_LineCountChanged(object? sender, EventArgs e)
        {
            _resultBox.Children.Clear();

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


            for (var i=0; i < _textEditor.Document.Lines.Count; i++)
            {
                if (_textEditor.Document.Lines[i].ToString().Trim().Length == 0) continue;
                var rectBox = new Avalonia.Controls.Border();
                rectBox.BorderBrush = new SolidColorBrush(Colors.Red);
                rectBox.BorderThickness = new Thickness(1);

                var currentLine = _textEditor.Document.GetLineByOffset(_textEditor.Document.Lines[i].Offset);
                var rect = AvaloniaEdit.Rendering.BackgroundGeometryBuilder.GetRectsForSegment(_textEditor.TextArea.TextView, currentLine).FirstOrDefault();
                rectBox.Margin = new Thickness(rect.X, rect.Y, 0,0);
                rectBox.Width = rect.Width;
                rectBox.Height = rect.Height;
                _resultBox.Children.Add(rectBox);
            }
        }

        void textEditor_TextEntering(object sender, TextInputEventArgs e)
        {
            if (e.Text == null || _completionWindow == null) return;
            if (e.Text.Length > 0)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    //_completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            _insightWindow?.Hide();
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

        private class MyOverloadProvider : IOverloadProvider
        {
            private readonly IList<(string header, string content)> _items;
            private int _selectedIndex;

            public MyOverloadProvider(IList<(string header, string content)> items)
            {
                _items = items;
                SelectedIndex = 0;
            }

            public int SelectedIndex
            {
                get => _selectedIndex;
                set
                {
                    _selectedIndex = value;
                    OnPropertyChanged();
                    // ReSharper disable ExplicitCallerInfoArgument
                    OnPropertyChanged(nameof(CurrentHeader));
                    OnPropertyChanged(nameof(CurrentContent));
                    // ReSharper restore ExplicitCallerInfoArgument
                }
            }

            public int Count => _items.Count;
            public string CurrentIndexText => null;
            public object CurrentHeader => _items[SelectedIndex].header;
            public object CurrentContent => _items[SelectedIndex].content;

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class MyCompletionData : ICompletionData
        {
            public MyCompletionData(string text)
            {
                Text = text;
            }

            public IBitmap Image => null;

            public string Text { get; }

            // Use this property if you want to show a fancy UIElement in the list.
            public object Content => Text;

            public object Description => "Description for " + Text;

            public double Priority { get; } = 0;

            IBitmap ICompletionData.Image => throw new NotImplementedException();

            public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
            {
                textArea.Document.Replace(completionSegment, Text);
            }
        }

    }
}