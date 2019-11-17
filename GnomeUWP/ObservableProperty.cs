using System.ComponentModel;

namespace GnomeFun
{
	public class ObservableProperty<T> : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private T property;
		public T Property
		{
			get => property;
			set
			{
				if(!Equals(property, value))
				{
					property = value;
					OnNotify();
				}
			}
		}
		private void OnNotify() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Property"));		
	}


}
