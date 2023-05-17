namespace weather_widget.Command
{
    /// <summary>
    /// Exit entire application -> Will call cancellationtokens in the future
    /// </summary>

    internal class ExitApplicationCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
