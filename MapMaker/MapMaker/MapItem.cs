using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapMaker {
	public class MapItem {
		[JsonProperty ( "location" )]
		[Category ( "Settings" )]
		public MapPoint Location { get; set; }
		[Category ( "Settings" )]
		[JsonProperty ( "size" )]
		public MapSize Size { get; set; }
		[Category("Settings")]
		[DisplayName("(Name)")]
		[ReadOnly(true)]
		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class MapPoint {
		[JsonProperty("x")]
		public int X { get; set; }
		[JsonProperty ( "y" )]
		public int Y { get; set; }
		
	}

	public class MapSize {
		[JsonProperty ( "height" )]
		public int Height { get; set; }
		[JsonProperty ( "width" )]
		public int Width { get; set; }

		
	}
}
