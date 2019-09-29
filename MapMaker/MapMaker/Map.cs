using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapMaker {
	public partial class Map {
		[JsonProperty ( "items" )]
		public List<MapItem> Items { get; set; } = new List<MapItem> ( );
	}


	public partial class Map {
		public static Map FromJson ( string json ) => JsonConvert.DeserializeObject<Map> ( json, JsonConverter.Settings );
		public static Map FromFile ( string file ) {
			using ( var f = new StreamReader ( file ) ) {
				using ( var jtr = new JsonTextReader ( f ) ) {
					var ser = JsonSerializer.Create ( JsonConverter.Settings );
					return ser.Deserialize<Map> ( jtr );
				}
			}
		}
	}


	public static class MapSerialize {
		public static string ToJson ( this Map self ) => JsonConvert.SerializeObject ( self, JsonConverter.Settings );
		public static void ToFile ( this Map self, string file ) {
			using ( var f = File.CreateText ( file ) ) {
				var ser = JsonSerializer.Create ( JsonConverter.Settings );
				ser.Serialize ( f, self );
			}
		}

	}

}
