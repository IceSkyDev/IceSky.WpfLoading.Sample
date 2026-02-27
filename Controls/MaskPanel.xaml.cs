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
    /// MaskPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MaskPanel : UserControl
    {
        public MaskPanel()
        {
            InitializeComponent();
            maskPanel.Opened += (o, e) => { tbMsg.Text = $"Opend at {DateTime.Now}"; };
        }

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
    }
}
