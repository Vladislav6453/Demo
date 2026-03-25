using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prototip.VM;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? PropertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
}