using System;

namespace GnomeFun
{
	public class SideBarItem
	{
		public SideBarItem() { }

		public SideBarItem(string name, string glyph)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Glyph = glyph ?? throw new ArgumentNullException(nameof(glyph));
		}
		public SideBarItem(string name, string glyph, int group)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Glyph = glyph ?? throw new ArgumentNullException(nameof(glyph));
			Group = group;
		}

		public string Name { get; set; }
		public string Glyph { get; set; }
		public int Group { get; set; }
	}

}
