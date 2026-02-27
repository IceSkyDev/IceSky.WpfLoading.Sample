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
    /// TextLoading.xaml 的交互逻辑
    /// </summary>
    public partial class TextLoading : UserControl
    {
        private dynamic defaultValue = new
        {
            Text = "Loading...",
            FontFamily = "Microsoft YaHei",
            FontSize = 16,
            TextColor = "#FF000000",
            RotateFrom = 0,
            RotateTo = 0,
            ScaleXFrom = 1.0,
            ScaleXTo = 1.0,
            ScaleYFrom = 1.0,
            ScaleYTo = 1.0,
            TranslateXFrom = 0.0,
            TranslateXTo = 0.0,
            TranslateYFrom = 0.0,
            TranslateYTo = 0.0,
            SkewXFrom = 0.0,
            SkewXTo = 0.0,
            SkewYFrom = 0.0,
            SkewYTo = 0.0,
            OpacityFrom = 0.3,
            OpacityTo = 1.0,
            AnimationDuration = 1000,
            AutoReverse = true,
            RepeatCount = -1,
            CharacterDelay = 0
        };

        public TextLoading()
        {
            InitializeComponent();
            cbDark.Checked += (o, e) => { bdBg.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")); cbDark.Foreground = Brushes.WhiteSmoke; };
            cbDark.Unchecked += (o, e) => { bdBg.Background = Brushes.White; cbDark.Foreground = Brushes.Black; };
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            var ff = cbFontFamily.ItemsSource as IEnumerable<FontFamily>;
            txtContent.Text = defaultValue.Text;
            tbTAColor.Text = defaultValue.TextColor;
            cbFontFamily.SelectedValue = ff.FirstOrDefault(f => f.FamilyNames.First().Value.Equals("Microsoft YaHei")) ?? ff.FirstOrDefault();
            sldFontSize.Value = defaultValue.FontSize;
            sldStartOpacity.Value = defaultValue.OpacityFrom;
            sldEndOpacity.Value = defaultValue.OpacityTo;
            sldStartAngle.Value = defaultValue.RotateFrom;
            sldEndAngle.Value = defaultValue.RotateTo;
            sldStartScaleX.Value = defaultValue.ScaleXFrom;
            sldEndScaleX.Value = defaultValue.ScaleXTo;
            sldStartScaleY.Value = defaultValue.ScaleYFrom;
            sldEndScaleY.Value = defaultValue.ScaleYTo;
            sldStartTransX.Value = defaultValue.TranslateXFrom;
            sldStartTransY.Value = defaultValue.TranslateYFrom;
            sldEndTransX.Value = defaultValue.TranslateXTo;
            sldEndTransY.Value = defaultValue.TranslateYTo;
            sldStartSkewX.Value = defaultValue.SkewXFrom;
            sldEndSkewX.Value = defaultValue.SkewXTo;
            sldStartSkewY.Value = defaultValue.SkewYFrom;
            sldEndSkewY.Value = defaultValue.SkewYTo;
            sldDuration.Value = defaultValue.AnimationDuration;
            sldRepeatCount.Value = defaultValue.RepeatCount;
            sldDelayTime.Value = defaultValue.CharacterDelay;
            chkAutoReverse.IsChecked = defaultValue.AutoReverse;
        }

        private void BtnCopyCode_Click(object sender, RoutedEventArgs e)
        {
            var color = bdTAColor.Background != null ? ColorToHex((bdTAColor.Background as SolidColorBrush).Color) : "#FF000000";
            var ff = (cbFontFamily.SelectedValue as FontFamily).FamilyNames.First().Value;
            var df = defaultValue.FontFamily;
            try
            {
                var xamlBuilder = new StringBuilder();
                xamlBuilder.AppendLine("<anim:TextLoadingControl");
                AppendPropIfNotDefault(xamlBuilder, "Text", txtAnimation.Text, defaultValue.Text);
                AppendPropIfNotDefault(xamlBuilder, "TextColor", color, defaultValue.TextColor);
                AppendPropIfNotDefault(xamlBuilder, "FontFamily", ff, df);
                AppendPropIfNotDefault(xamlBuilder, "FontSize", txtAnimation.FontSize, defaultValue.FontSize);
                AppendPropIfNotDefault(xamlBuilder, "OpacityFrom", Math.Round(txtAnimation.OpacityFrom, 1), defaultValue.OpacityFrom);
                AppendPropIfNotDefault(xamlBuilder, "OpacityTo", Math.Round(txtAnimation.OpacityTo, 1), defaultValue.OpacityTo);
                AppendPropIfNotDefault(xamlBuilder, "RotateFrom", Math.Round(txtAnimation.RotateFrom, 1), defaultValue.RotateFrom);
                AppendPropIfNotDefault(xamlBuilder, "RotateTo", Math.Round(txtAnimation.RotateTo, 1), defaultValue.RotateTo);
                AppendPropIfNotDefault(xamlBuilder, "ScaleXFrom", Math.Round(txtAnimation.ScaleXFrom, 1), defaultValue.ScaleXFrom);
                AppendPropIfNotDefault(xamlBuilder, "ScaleXTo", Math.Round(txtAnimation.ScaleXTo, 1), defaultValue.ScaleXTo);
                AppendPropIfNotDefault(xamlBuilder, "ScaleYFrom", Math.Round(txtAnimation.ScaleYFrom, 1), defaultValue.ScaleYFrom);
                AppendPropIfNotDefault(xamlBuilder, "ScaleYTo", Math.Round(txtAnimation.ScaleYTo, 1), defaultValue.ScaleYTo);
                AppendPropIfNotDefault(xamlBuilder, "TranslateXFrom", Math.Round(txtAnimation.TranslateXFrom, 1), defaultValue.TranslateXFrom);
                AppendPropIfNotDefault(xamlBuilder, "TranslateXTo", Math.Round(txtAnimation.TranslateXTo, 1), defaultValue.TranslateXTo);
                AppendPropIfNotDefault(xamlBuilder, "TranslateYFrom", Math.Round(txtAnimation.TranslateYFrom, 1), defaultValue.TranslateYFrom);
                AppendPropIfNotDefault(xamlBuilder, "TranslateYTo", Math.Round(txtAnimation.TranslateYTo, 1), defaultValue.TranslateYTo);
                AppendPropIfNotDefault(xamlBuilder, "SkewXFrom", Math.Round(txtAnimation.SkewXFrom, 1), defaultValue.SkewXFrom);
                AppendPropIfNotDefault(xamlBuilder, "SkewXTo", Math.Round(txtAnimation.SkewXTo, 1), defaultValue.SkewXTo);
                AppendPropIfNotDefault(xamlBuilder, "SkewYFrom", Math.Round(txtAnimation.SkewYFrom, 1), defaultValue.SkewYFrom);
                AppendPropIfNotDefault(xamlBuilder, "SkewYTo", Math.Round(txtAnimation.SkewYTo, 1), defaultValue.SkewYTo);
                AppendPropIfNotDefault(xamlBuilder, "AnimationDuration", (double)txtAnimation.AnimationDuration, defaultValue.AnimationDuration);
                AppendPropIfNotDefault(xamlBuilder, "CharacterDelay", (double)txtAnimation.CharacterDelay, defaultValue.CharacterDelay);
                AppendPropIfNotDefault(xamlBuilder, "RepeatCount", (double)txtAnimation.RepeatCount, defaultValue.RepeatCount);
                AppendPropIfNotDefault(xamlBuilder, "IsAnimationAutoReverse", txtAnimation.IsAnimationAutoReverse, defaultValue.AutoReverse);

                var code = xamlBuilder.ToString().TrimEnd() + "/>";

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
        private string ColorToHex(Color? color, string nullValue = "")
        {
            return color == null ? nullValue : $"#{color.Value.A:X2}{color.Value.R:X2}{color.Value.G:X2}{color.Value.B:X2}";
        }
    }
}
