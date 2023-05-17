using System;
using System.Windows.Input;

namespace weather_widget.Command
{
    /// <summary>
    /// Base command clase -> all commands inherit from
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}