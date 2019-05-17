﻿using System;
using System.Windows.Input;

namespace DaHo.Library.Wpf
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Func<object, bool> m_CanExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            m_Execute(parameter);
        }
    }
}
