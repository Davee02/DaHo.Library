using System;
using System.ComponentModel;
using System.Windows;

namespace DaHo.Library.Wpf
{
    public static class ViewModelLocator
    {
        public static bool GetAutoHookedUpViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoHookedUpViewModelProperty);
        }

        public static void SetAutoHookedUpViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoHookedUpViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoHookedUpViewModelProperty =
            DependencyProperty.RegisterAttached("AutoHookedUpViewModel",
                typeof(bool), typeof(ViewModelLocator), new
                    PropertyMetadata(false, AutoHookedUpViewModelChanged));

        private static void AutoHookedUpViewModelChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
                return;
            var viewType = d.GetType();

            string str = viewType.FullName;
            str = str.Replace(".Views.", ".ViewModels.");

            var viewTypeName = str;
            var viewModelTypeName = $"{viewTypeName}Model, {viewType.Assembly.FullName}";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);

            if (viewModel is ViewModelBase viewModelBase)
            {
                var window = (Window)d;
                viewModelBase.ShowAction = window.Show;
                viewModelBase.CloseAction = window.Close;
                viewModelBase.HideAction = window.Hide;
            }

            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
