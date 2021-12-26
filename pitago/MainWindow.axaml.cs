using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Pitago
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            InitializeComponent();
        }

        private void InitializeComponent()
        {
        }

    }
}