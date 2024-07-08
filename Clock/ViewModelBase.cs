
using System.ComponentModel;

using System.Runtime.CompilerServices;


namespace Clock;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyname = null!)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
    
}
