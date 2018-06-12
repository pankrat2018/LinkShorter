using System;
using System.Threading;
using System.Windows;

namespace LinkShorter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Resources _resources = LinkShorter.Resources.GetResources();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Short()
        {
            string shortLink = Shorter.Short(_resources.Link);

            if (!_resources.Link.Equals(shortLink))
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    textBoxShortLink.Text = shortLink;

                    Clipboard.SetText(shortLink);
                }));

                MessageBox.Show("Ссылка успешно сокращена и скопирована в буффер обмена", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            else
            {
                MessageBox.Show("Не удалось сократить ссылку, попробуйте в другой раз.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSourceLink.Text) || !textBoxSourceLink.Text.Contains("http://") && !textBoxSourceLink.Text.Contains("https://"))
            {
                MessageBox.Show("Введите корректную ссылку", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _resources.Link = textBoxSourceLink.Text;

            new Thread(Short).Start();
        }
    }
}
