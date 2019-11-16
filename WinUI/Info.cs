using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

namespace GnomeFun
{
	public static class Info
	{
		public static string SystemFamily { get; }
		public static string SystemVersion { get; }
		public static string SystemArchitecture { get; }
		public static string ApplicationName { get; }
		public static string ApplicationVersion { get; }
		public static string DeviceManufacturer { get; }
		public static string DeviceModel { get; }

		static Info()
		{
			// get the system family name
			AnalyticsVersionInfo ai = AnalyticsInfo.VersionInfo;
			SystemFamily = ai.DeviceFamily;

			// get the system version number
			string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
			ulong v = ulong.Parse(sv);
			ulong v1 = (v & 0xFFFF000000000000L) >> 48;
			ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
			ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
			ulong v4 = (v & 0x000000000000FFFFL);
			SystemVersion = $"{v1}.{v2}.{v3}.{v4}";

			// get the package architecure
			Package package = Package.Current;
			SystemArchitecture = package.Id.Architecture.ToString();

			// get the user friendly app name
			ApplicationName = package.DisplayName;

			// get the app version
			PackageVersion pv = package.Id.Version;
			ApplicationVersion = $"{pv.Major}.{pv.Minor}.{pv.Build}.{pv.Revision}";

			// get the device manufacturer and model name
			EasClientDeviceInformation eas = new EasClientDeviceInformation();
			DeviceManufacturer = eas.SystemManufacturer;
			DeviceModel = eas.SystemProductName;
		}

		public static async Task<string> GetStorageCapacityAsync()
		{
			var local = Windows.Storage.ApplicationData.Current.LocalFolder;
			var retrivedProperties = await local.Properties.RetrievePropertiesAsync(new string[] { "System.FreeSpace", "System.Capacity" });
			var totalFreespace = (ulong)retrivedProperties["System.FreeSpace"];
			var capacity = (ulong)retrivedProperties["System.Capacity"];
			return string.Format("{0} free of {1}", FormatBytes(totalFreespace), FormatBytes(capacity));
		}

		private static string FormatBytes(ulong bytes)
		{
			string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
			int i;
			double dblSByte = bytes;
			for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
			{
				dblSByte = bytes / 1024.0;
			}

			return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
		}



	}


}
