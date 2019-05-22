namespace DaHo.Library.Wpf
{
    public interface IDialogService
    {
        bool AskQuestion(string message, string title);

        void ShowInfoDialog(string message, string title);

        void ShowErrorDialog(string message, string title);

        void ShowWarnDialog(string message, string title);
    }
}
