using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IceSky.WpfLoading.Sample.Controls
{
    /// <summary>
    /// GridAnimation.xaml 的交互逻辑
    /// </summary>
    public partial class GridAnimation : UserControl
    {
        private dynamic defaultValue = new
        {
            LayoutType = LayoutType.Triangle,
            LayoutMode = LayoutMode.Edge,
            ArcAngle = 360.0,
            LineCount = 5,
            Curvature = 1.0,
            StartScale = 0.5,
            EndScale = 1.0,
            StartOpacity = 0.1,
            EndOpacity = 1.0,
            XOffset = 0.0,
            YOffset = 0.0,
            AnimationDuration = 1000,
            AnimationDelay = 100,
            AutoReverse = true,
            ItemMargin = 3,
        };
        public GridAnimation()
        {
            InitializeComponent();
            aicGrid.ItemsSource = Enumerable.Range(1, 30).Select(i => i.ToString());
            cbDark.Checked += (o, e) => { bdBg.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")); cbDark.Foreground = Brushes.WhiteSmoke; };
            cbDark.Unchecked += (o, e) => { bdBg.Background = Brushes.White; cbDark.Foreground = Brushes.Black; };
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            cbGridType.SelectedIndex = 0;
            slGridCurvature.Value = defaultValue.Curvature;
            slGridLineCount.Value = defaultValue.LineCount;
            slGridStartScale.Value = defaultValue.StartScale;
            slGridEndScale.Value = defaultValue.EndScale;
            slGridStartOpacity.Value = defaultValue.StartOpacity;
            slGridEndOpacity.Value = defaultValue.EndOpacity;
            slGridXOffset.Value = defaultValue.XOffset;
            slGridYOffset.Value = defaultValue.YOffset;
            slGridAnimationDuration.Value = defaultValue.AnimationDuration;
            slGridAnimationDelay.Value = defaultValue.AnimationDelay;
            chkAutoReverse.IsChecked = defaultValue.AutoReverse;
            slGridItemMargin.Value = defaultValue.ItemMargin;
        }

        private void BtnCopyCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var xamlBuilder = new StringBuilder();
                xamlBuilder.AppendLine("<ctrl:AnimateItemsControl");
                AppendPropIfNotDefault(xamlBuilder, "LayoutType", aicGrid.LayoutType, defaultValue.LayoutType);
                AppendPropIfNotDefault(xamlBuilder, "LayoutMode", aicGrid.LayoutMode, defaultValue.LayoutMode);
                AppendPropIfNotDefault(xamlBuilder, "GridLineCount", aicGrid.GridLineCount, defaultValue.LineCount);
                AppendPropIfNotDefault(xamlBuilder, "Curvature", Math.Round(aicGrid.Curvature, 1), defaultValue.Curvature);
                AppendPropIfNotDefault(xamlBuilder, "StartScale", Math.Round(aicGrid.StartScale, 1), defaultValue.StartScale);
                AppendPropIfNotDefault(xamlBuilder, "EndScale", Math.Round(aicGrid.EndScale, 1), defaultValue.EndScale);
                AppendPropIfNotDefault(xamlBuilder, "StartOpacity", Math.Round(aicGrid.StartOpacity, 1), defaultValue.StartOpacity);
                AppendPropIfNotDefault(xamlBuilder, "EndOpacity", Math.Round(aicGrid.EndOpacity, 1), defaultValue.EndOpacity);
                AppendPropIfNotDefault(xamlBuilder, "XOffset", Math.Round(aicGrid.XOffset, 1), defaultValue.XOffset);
                AppendPropIfNotDefault(xamlBuilder, "YOffset", Math.Round(aicGrid.YOffset, 1), defaultValue.YOffset);
                AppendPropIfNotDefault(xamlBuilder, "AnimationDuration", (double)aicGrid.AnimationDuration, defaultValue.AnimationDuration);
                AppendPropIfNotDefault(xamlBuilder, "AnimationDelay", (double)aicGrid.AnimationDelay, defaultValue.AnimationDelay);
                AppendPropIfNotDefault(xamlBuilder, "IsAnimationAutoReverse", aicGrid.IsAnimationAutoReverse, defaultValue.AutoReverse);
                var c = Environment.NewLine.Length;
                xamlBuilder.Remove(xamlBuilder.Length - c, c).AppendLine(">");
                if (slGridItemMargin.Value > 0)
                {
                    xamlBuilder.AppendLine("    <ctrl:AnimateItemsControl.ItemContainerStyle>");
                    xamlBuilder.AppendLine("        <Style TargetType=\"ContentPresenter\">");
                    xamlBuilder.AppendLine("            <Setter Property=\"Margin\" Value=\"" + (int)slGridItemMargin.Value + "\"/>");
                    xamlBuilder.AppendLine("        </Style>");
                    xamlBuilder.AppendLine("    </ctrl:AnimateItemsControl.ItemContainerStyle>");
                }
                xamlBuilder.AppendLine("    <ctrl:AnimateItemsControl.ItemTemplate>");
                xamlBuilder.AppendLine("        <DataTemplate>");
                xamlBuilder.AppendLine("            <Grid>");
                xamlBuilder.AppendLine("            </Grid>");
                xamlBuilder.AppendLine("        </DataTemplate>");
                xamlBuilder.AppendLine("    </ctrl:AnimateItemsControl.ItemTemplate>");
                xamlBuilder.AppendLine("</ctrl:AnimateItemsControl>");

                var code = xamlBuilder.ToString();

                Clipboard.SetText(code);
                MessageBox.Show("Code copied！", "复制成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Copy failed：{ex.Message}", "复制失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            void AppendPropIfNotDefault(StringBuilder builder, string propName, dynamic value, dynamic defaultValue)
            {
                if (!value.Equals(defaultValue)) builder.AppendLine($"\t{propName}=\"{value}\"");
            }
        }
    }
}
