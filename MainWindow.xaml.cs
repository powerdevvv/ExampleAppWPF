using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebView();
            OpacityAnimationLooped(TitleAnimated);
        }

        private void OpacityAnimationLooped(UIElement element)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } 
            };
            fadeInAnimation.RepeatBehavior = RepeatBehavior.Forever;
            fadeInAnimation.AutoReverse = true;
            element.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }

        private async void InitializeWebView()
        {
            try
            {
                if (TestEditor?.CoreWebView2 == null)
                {
                    await TestEditor.EnsureCoreWebView2Async(null);
                }
                string localPath = System.IO.Path.Combine(Environment.CurrentDirectory, "editor-xd", "editor.html");
                string fileUri = "file:///" + localPath.Replace(@"\", @"/");
#if DEBUG
                MessageBox.Show($"Path (copied to clipboard): {fileUri}", "Debug");
                Clipboard.SetText(fileUri);
#endif
                TestEditor.CoreWebView2.Navigate(fileUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WebView2 init failed: {ex.Message}");
            }
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }
    }
}