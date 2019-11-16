using System;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GnomeFun
{
	public sealed partial class MainPage : Page
	{
		private ObservableProperty<string> DeviceModelName { get; } = new ObservableProperty<string>();
		private ObservableProperty<string> DeviceName { get; } = new ObservableProperty<string>();
		private ObservableProperty<string> Storage { get; } = new ObservableProperty<string>();
		private ObservableProperty<string> SystemType { get; } = new ObservableProperty<string>();
		
		public MainPage()
		{
			InitializeComponent();
			ActualThemeChanged += (s, e) =>
			{
				var titleBar = ApplicationView.GetForCurrentView().TitleBar;
				if (ActualTheme == ElementTheme.Dark)
				{
					ThemeButton.Content = "\uE708";
					titleBar.ButtonForegroundColor = Colors.White;
				}
				else
				{
					ThemeButton.Content = "\uE706";
					titleBar.ButtonForegroundColor = Colors.Black;
				}

			};
			ThemeButton.Content = ActualTheme == ElementTheme.Dark ? "\uE708" : "\uE706";
		}



		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (splitView.IsPaneOpen)
			{
				column1.Width = new GridLength(50.0);
				column2.Width = new GridLength(220.0);
				column3.Width = new GridLength(50.0);
				backButton.Visibility = Visibility.Collapsed;
				Grid.SetColumn(ThemeButton, 3);
			}
			else
			{
				column1.Width = new GridLength(0);
				column2.Width = new GridLength(0);
				column3.Width = new GridLength(0);
			}

			SideBarItem[] items = new SideBarItem[]
			{
				new SideBarItem("Wi-FI", "\uE701", 0),
				new SideBarItem("Bluetooth", "\uE702", 0),
				new SideBarItem("Background", "\uE771", 0),
				new SideBarItem("Notifications", "\uE781", 0),
				new SideBarItem("Search", "\uE721", 0),
				new SideBarItem("Region & Language", "\uE775", 0),
				new SideBarItem("Universal Accesss", "\uE776", 0),
				new SideBarItem("Online Accounts", "\uE753", 1),
				new SideBarItem("Privacy", "\uE72E", 1),
				new SideBarItem("Apps", "\uE71D", 1),
				new SideBarItem("Sharing", "\uE72D", 1),
				new SideBarItem("Sound", "\uE767", 2),
				new SideBarItem("Power", "\uEBA8", 2),
				new SideBarItem("Network", "\uEE77", 2),
				new SideBarItem("Devices", "\uE772", 3),
				new SideBarItem("Details", "\uE946", 3),
			};
			GroupedItems.Source = from itemMenu in items
								  group itemMenu by itemMenu.Group;
			//foreach (var sideBarItem in items)
			//SideBarItemsList.Add(sideBarItem);

			DeviceModelName.Property = $"{Info.DeviceManufacturer} {Info.DeviceModel}";
			DeviceName.Property = Environment.MachineName;			
			Storage.Property = await Info.GetStorageCapacityAsync();
			SystemType.Property = Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits";
		}


		private void TitleBarLoaded(object sender, RoutedEventArgs e)
		{
			CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
			var titleBar = ApplicationView.GetForCurrentView().TitleBar;
			titleBar.ButtonBackgroundColor = Colors.Transparent;
			titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
			Window.Current.SetTitleBar((UIElement)sender);
		}

		private void SplitViewPaneOpening(SplitView sender, object args)
		{
			column1.Width = new GridLength(50.0);
			column2.Width = new GridLength(220.0);
			column3.Width = new GridLength(50.0);
			backButton.Visibility = Visibility.Collapsed;
			Grid.SetColumn(ThemeButton, 3);
		}

		private void SplitViewPaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
		{
			column1.Width = new GridLength(0);
			column2.Width = new GridLength(0);
			column3.Width = new GridLength(0);
			backButton.Visibility = Visibility.Visible;
			Grid.SetColumn(ThemeButton, 4);
		}

		private void BackButtonClick(object sender, RoutedEventArgs e) => splitView.IsPaneOpen = true;

		private void ThemeButtonClick(object sender, RoutedEventArgs e)
		{
			RequestedTheme = RequestedTheme == Windows.UI.Xaml.ElementTheme.Dark
				? Windows.UI.Xaml.ElementTheme.Light
				: Windows.UI.Xaml.ElementTheme.Dark;
		}


	}


}
