using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DaHo.Library.Wpf
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public IDialogService DialogService { get; set; }

        /// <summary>
        /// This action closes the view
        /// The current state cannot be recovered by calling the show-action
        /// </summary>
        public Action CloseAction { get; set; }

        /// <summary>
        /// This action hides the view
        /// The current state can be recovered by calling the show-action
        /// </summary>
        public Action HideAction { get; set; }

        /// <summary>
        /// This action shows the view
        /// Recovers the current state after calling the hide-action
        /// </summary>
        public Action ShowAction { get; set; }

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Displays a warn dialog with all the data errors from the validation.
        /// </summary>
        /// <param name="errors">The IEnumerable that contains all the error-objects</param>
        /// <param name="dialogService">A class implementing the <see cref="IDialogService"/> interface. Through this the warn-dialog is showed to the user.</param>
        /// <param name="dialogTitle">The title of the warn-dialog which is showed to the user. The default value is "Fehler"</param>
        protected virtual void DisplayDataErrors(IEnumerable errors, IDialogService dialogService, string dialogTitle = "Fehler")
        {
            var sb = new StringBuilder();
            foreach (var error in errors)
            {
                sb.AppendLine($"- {error}");
            }

            dialogService.ShowWarnDialog(sb.ToString(), dialogTitle);
        }
    }
}