using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Pitago.Controls;

namespace Pitago
{
    public partial class MainWindow : Window
    {
        Notepad notePad;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            notePad = this.FindControl<Notepad>("notePad");
        }

#region AppFunction

        /// <summary>
        /// Open files into new tabs
        /// Currently only 1 tab supported, Reserve for future work
        /// </summary>
        private async void OpenFiles()
        {
            var dialog = new OpenFileDialog()
            {
                AllowMultiple = false
            };
            dialog.Filters.Add(new FileDialogFilter()
            {
                Name = "Pitago Document",
                Extensions = { "pd" }
            });

            var result = await dialog.ShowAsync(this);

            if (result != null)
            {
                foreach (var path in result)
                    notePad.Load(path);
            }
        }

        /// <summary>
        /// Save all opened tabs to files
        /// Currently only 1 tab supported, Reserve for future work
        /// </summary>
        private void SaveFiles()
        {
            notePad.Save(this);
        }

        /// <summary>
        /// Save opened tab to file
        /// </summary>
        private void SaveCurrentTab()
        {
            notePad.Save(this);
        }

#endregion

#region Events

        public void NewOnClick(object sender, RoutedEventArgs args)
        {
            notePad.New();
        }

        public void OpenOnClick(object sender, RoutedEventArgs args)
        {
            OpenFiles();
        }

        public void SaveOnClick(object sender, RoutedEventArgs args)
        {
            SaveCurrentTab();
        }

#endregion

    }
}