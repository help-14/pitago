using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Pitago.Controls
{
    public class TabHeader : UserControl
    {
        private TextBlock HeaderTb;
        private ExperimentalAcrylicBorder AcrylicBorder;
        public TabHeader()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            HeaderTb = this.FindControl<TextBlock>("HeaderTb");
            AcrylicBorder = this.FindControl<ExperimentalAcrylicBorder>("AcrylicBorder");
        }

        public string Text
        {
            get => HeaderTb.Text;
            set => HeaderTb.Text = value;
        }

        public bool IsSelected
        {
            get => AcrylicBorder.IsVisible;
            set => AcrylicBorder.IsVisible = value;
        }
        
    }
}