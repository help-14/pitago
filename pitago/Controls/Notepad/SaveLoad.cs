using Avalonia.Controls;
using System;
using System.IO;

namespace Pitago.Controls
{
    public partial class Notepad
    {
        public void New()
        {
            IsChangesSaved = true;
            FilePath = String.Empty;

            _textEditor.Text = "";
            ClearPreviewResult();
            _calProcessor = new Calculation.CalculationProcessor();
        }

        public bool IsChangesSaved { get; set; }
        public string FilePath { get; set; }

        public async void Save(Window window)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                var dialog = new SaveFileDialog()
                {
                    InitialFileName = DateTime.Now.ToString(),
                    DefaultExtension = ".pd"
                };
                dialog.Filters.Add(new FileDialogFilter()
                {
                    Name = "Pitago Document",
                    Extensions = { "pd" }
                });

                var result = await dialog.ShowAsync(window);
                if (!string.IsNullOrEmpty(result))
                    Save(result);
            }
            else
                Save(FilePath);
        }

        public void Save(string path)
        {
            FilePath = path;
            if (!string.IsNullOrEmpty(FilePath))
            {
                File.WriteAllText(FilePath, _textEditor.Text);
                IsChangesSaved = true;
            }
        }

        public void Load(string path)
        {
            var document = File.ReadAllText(path);
            document = _calProcessor.ProcessDocument(document, true);
            _textEditor.Text = document;

            FilePath = path;
            IsChangesSaved = true;
        }

    }
}
