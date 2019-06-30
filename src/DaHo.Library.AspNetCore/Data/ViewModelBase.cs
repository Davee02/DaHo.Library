namespace DaHo.Library.AspNetCore.Data
{
    public class ViewModelBase
    {
        public ViewModelBase(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
