using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IceSky.WpfLoading.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private dynamic defaultValue = new
        {
            ItemWidth = 10.0,
            ItemHeight = 20.0,
            CornerRadius = 0.0,
            Count = 6,
            Color1 = "#FF4A90E2",
            Color2 = "#FF9013FE",
            LoopColor = true,
            IsAverageSpacing = true,
            Spacing = 8.0,
            ArcDegree = 100,
            CurveRange = 360,
            InnerRadius = 30.0,
            IsRingRotation = false,
            RotationSpeed = 90.0,
            IsDiscreteRotation = false,
            DiscreteStepTime = 0.5,
            IsItemRotate = false,
            ItemRotateSpeed = 180.0,
            ItemAnimationDuration = 500,
            StartScaleX = 0.5,
            EndScaleX = 1.0,
            StartScaleY = 0.5,
            EndScaleY = 1.0,
            StartOffsetX = 0.0,
            StartOffsetY = 0.0,
            EndOffsetX = 0.0,
            EndOffsetY = 0.0,
            StartOpacity = 0.3,
            EndOpacity = 1.0,
            AutoReverse = true,
            RepeatCount = -1,
            DelayTime = 0
        };
        public MainWindow()
        {
            InitializeComponent();
            cbDark.Checked += (o, e) => { bdBg.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")); cbDark.Foreground = Brushes.WhiteSmoke; };
            cbDark.Unchecked += (o, e) => { bdBg.Background = Brushes.White; cbDark.Foreground = Brushes.Black; };

            aicEdge.ItemsSource = Enumerable.Range(1, 12).Select(i => (char)(i + 64));
            aicGrid.ItemsSource = Enumerable.Range(1, 30).Select(i => i.ToString());
            maskPanel.Opened += (o, e) => { tbMsg.Text = $"Opend at {DateTime.Now}"; };
        }
        #region MaskTest
        private double _blurRadius = 10;
        private double _maskOpacity = 0.7;
        private Color _maskColor = Color.FromRgb(51, 51, 51); // #333333
        private int _timeoutMs = 8000;
        private int _taskSeconds = 5;

        private void ShowMask_Click(object sender, RoutedEventArgs e)
        {
            txtElapsedTime.Text = "";
            maskPanel.Open();
        }
        private void ExecuteTask_Click(object sender, RoutedEventArgs e)
        {
            var st = DateTime.Now;
            var taskCts = new CancellationTokenSource();
            var d = int.Parse(txtTaskSeconds.Text);
            var task = Task.Run(async () =>
            {
                var time = DateTime.Now.Subtract(st);
                while (time.TotalSeconds < d && !taskCts.IsCancellationRequested)
                {
                    time = DateTime.Now.Subtract(st);
                    Dispatcher.Invoke(() => txtElapsedTime.Text = $"{time:hh\\:mm\\:ss}");
                    await Task.Delay(100);
                }
            }, taskCts.Token);
            maskPanel.OpenWithTask(task, taskCts, uint.Parse(txtTimeoutMs.Text));
        }

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            // 方式1：普通关闭（自动取消任务）
            // maskPanel.Close();

            // 方式2：强制关闭（立即取消+清理资源）
            maskPanel.ForceClose();
        }
        private void MaskPanel_TaskCancelled(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("操作已取消，任务终止！");
        }
        private void MaskPanel_TaskCompleted(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("任务完成，遮罩关闭！");
        }

        private void MaskPanel_Timeout(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("任务超时，遮罩关闭！");
        }

        #endregion

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            sldRectWidth.Value = defaultValue.ItemWidth;
            sldRectHeight.Value = defaultValue.ItemHeight;
            sldCornerRadius.Value = defaultValue.CornerRadius;
            sldCount.Value = defaultValue.Count;
            tbColor1.Text = defaultValue.Color1;
            tbColor2.Text = defaultValue.Color2;
            chkLoopColor.IsChecked = defaultValue.LoopColor;
            chkIsAverageSpacing.IsChecked = defaultValue.IsAverageSpacing;
            sldSpacing.Value = defaultValue.Spacing;
            sldArcDegree.Value = defaultValue.ArcDegree;
            sldCurveRange.Value = defaultValue.CurveRange;
            sldInnerRadius.Value = defaultValue.InnerRadius;
            chkIsRingRotation.IsChecked = defaultValue.IsRingRotation;
            sldRotationSpeed.Value = defaultValue.RotationSpeed;
            chkIsDiscreteRingRotation.IsChecked = defaultValue.IsDiscreteRotation;
            sldDiscreteStepTime.Value = defaultValue.DiscreteStepTime;
            chkIsRectRotate.IsChecked = defaultValue.IsItemRotate;
            sldRectRotateSpeed.Value = defaultValue.ItemRotateSpeed;
            sldSingleDuration.Value = defaultValue.ItemAnimationDuration;
            sldStartScaleX.Value = defaultValue.StartScaleX;
            sldEndScaleX.Value = defaultValue.EndScaleX;
            sldStartScaleY.Value = defaultValue.StartScaleY;
            sldEndScaleY.Value = defaultValue.EndScaleY;
            sldStartTransX.Value = defaultValue.StartOffsetX;
            sldStartTransY.Value = defaultValue.StartOffsetY;
            sldEndTransX.Value = defaultValue.EndOffsetX;
            sldEndTransY.Value = defaultValue.EndOffsetY;
            sldStartOpacity.Value = defaultValue.StartOpacity;
            sldEndOpacity.Value = defaultValue.EndOpacity;
            chkAutoReverse.IsChecked = defaultValue.AutoReverse;
            sldRepeatCount.Value = defaultValue.RepeatCount;
            sldDelayTime.Value = defaultValue.DelayTime;
        }
        private void BtnCopyCode_Click(object sender, RoutedEventArgs e)
        {
            var color1 = bdColor1.Background != null ? ColorToHex((bdColor1.Background as SolidColorBrush).Color) : ColorToHex((bdColor2.Background as SolidColorBrush).Color, "#FF4A90E2");
            var color2 = bdColor2.Background != null ? ColorToHex((bdColor2.Background as SolidColorBrush).Color) : ColorToHex((bdColor1.Background as SolidColorBrush).Color, "#FF9013FE");
            try
            {
                var xamlBuilder = new StringBuilder();
                xamlBuilder.AppendLine("<anim:GradientLoadingControl");
                AppendPropIfNotDefault(xamlBuilder, "ItemWidth", Math.Round(loadingControl.ItemWidth, 1), defaultValue.ItemWidth);
                AppendPropIfNotDefault(xamlBuilder, "ItemHeight", Math.Round(loadingControl.ItemHeight, 1), defaultValue.ItemHeight);
                AppendPropIfNotDefault(xamlBuilder, "CornerRadius", Math.Round(loadingControl.CornerRadius, 1), defaultValue.CornerRadius);
                AppendPropIfNotDefault(xamlBuilder, "Count", (double)loadingControl.Count, defaultValue.Count);
                AppendPropIfNotDefault(xamlBuilder, "Spacing", Math.Round(loadingControl.Spacing, 1), defaultValue.Spacing);
                AppendPropIfNotDefault(xamlBuilder, "IsAverageSpacing", loadingControl.IsAverageSpacing, defaultValue.IsAverageSpacing);
                AppendPropIfNotDefault(xamlBuilder, "GradientStartColor", color1, defaultValue.Color1);
                AppendPropIfNotDefault(xamlBuilder, "GradientEndColor", color2, defaultValue.Color2);
                AppendPropIfNotDefault(xamlBuilder, "LoopColor", loadingControl.LoopColor, defaultValue.LoopColor);
                AppendPropIfNotDefault(xamlBuilder, "ArcDegree", (double)loadingControl.ArcDegree, defaultValue.ArcDegree);
                AppendPropIfNotDefault(xamlBuilder, "CurveRange", (double)loadingControl.CurveRange, defaultValue.CurveRange);
                AppendPropIfNotDefault(xamlBuilder, "InnerRadius", Math.Round(loadingControl.InnerRadius, 1), defaultValue.InnerRadius);
                AppendPropIfNotDefault(xamlBuilder, "IsRingRotation", loadingControl.IsRingRotation, defaultValue.IsRingRotation);
                AppendPropIfNotDefault(xamlBuilder, "RingRotationSpeed", Math.Round(loadingControl.RingRotationSpeed, 1), defaultValue.RotationSpeed);
                AppendPropIfNotDefault(xamlBuilder, "IsDiscreteRotation", loadingControl.IsDiscreteRotation, defaultValue.IsDiscreteRotation);
                AppendPropIfNotDefault(xamlBuilder, "DiscreteRotateStepTime", Math.Round(loadingControl.DiscreteRotateStepTime, 1), defaultValue.DiscreteStepTime);
                AppendPropIfNotDefault(xamlBuilder, "IsItemRotateAnimation", loadingControl.IsItemRotateAnimation, defaultValue.IsItemRotate);
                AppendPropIfNotDefault(xamlBuilder, "ItemRotateSpeed", Math.Round(loadingControl.ItemRotateSpeed, 1), defaultValue.ItemRotateSpeed);
                AppendPropIfNotDefault(xamlBuilder, "ItemAnimationDuration", (double)loadingControl.ItemAnimationDuration, defaultValue.ItemAnimationDuration);
                AppendPropIfNotDefault(xamlBuilder, "DelayTime", (double)loadingControl.DelayTime, defaultValue.DelayTime);
                AppendPropIfNotDefault(xamlBuilder, "StartScaleX", Math.Round(loadingControl.StartScaleX, 1), defaultValue.StartScaleX);
                AppendPropIfNotDefault(xamlBuilder, "EndScaleX", Math.Round(loadingControl.EndScaleX, 1), defaultValue.EndScaleX);
                AppendPropIfNotDefault(xamlBuilder, "StartScaleY", Math.Round(loadingControl.StartScaleY, 1), defaultValue.StartScaleY);
                AppendPropIfNotDefault(xamlBuilder, "EndScaleY", Math.Round(loadingControl.EndScaleY, 1), defaultValue.EndScaleY);
                AppendPropIfNotDefault(xamlBuilder, "StartOffsetX", Math.Round(loadingControl.StartOffsetX, 1), defaultValue.StartOffsetX);
                AppendPropIfNotDefault(xamlBuilder, "StartOffsetY", Math.Round(loadingControl.StartOffsetY, 1), defaultValue.StartOffsetY);
                AppendPropIfNotDefault(xamlBuilder, "EndOffsetX", Math.Round(loadingControl.EndOffsetX, 1), defaultValue.EndOffsetX);
                AppendPropIfNotDefault(xamlBuilder, "EndOffsetY", Math.Round(loadingControl.EndOffsetY, 1), defaultValue.EndOffsetY);
                AppendPropIfNotDefault(xamlBuilder, "StartOpacity", Math.Round(loadingControl.StartOpacity, 1), defaultValue.StartOpacity);
                AppendPropIfNotDefault(xamlBuilder, "EndOpacity", Math.Round(loadingControl.EndOpacity, 1), defaultValue.EndOpacity);
                AppendPropIfNotDefault(xamlBuilder, "IsAnimationAutoReverse", loadingControl.IsAnimationAutoReverse, defaultValue.AutoReverse);
                AppendPropIfNotDefault(xamlBuilder, "AnimationRepeatCount", (double)loadingControl.AnimationRepeatCount, defaultValue.RepeatCount);

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
