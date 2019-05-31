using System.Windows;

namespace DaHo.Library.Wpf
{
    public class MessageboxDialogService : IDialogService
    {
        public bool AskQuestion(string message, string header)
        {
            var dialogResult = MessageBox.Show(message, header, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return dialogResult == MessageBoxResult.Yes;
        }

        public void ShowInfoDialog(string message, string header)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowErrorDialog(string message, string header)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowWarnDialog(string message, string header)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
