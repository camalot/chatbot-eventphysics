using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MapMaker {
	public partial class MainForm : Form {
		public MainForm ( ) {
			InitializeComponent ( );
			var appDir = Path.GetDirectoryName ( Assembly.GetCallingAssembly().Location );
			Settings = EventPhysicsSettings.FromFile(Path.Combine(appDir, "../../settings.json"));
			mapTree.Nodes.Add ( MapRoot );
			InitMapTree ( null );
		}

		public EventPhysicsSettings Settings { get; set; }
		public MapRootTreeItem MapRoot { get; set; } = new MapRootTreeItem ( );

		private void InitMapTree(string file) {
			MapRoot.Nodes.Clear ( );
		}

		private void AddBox_Click ( object sender, EventArgs e ) {
			var mi = new MapBox ( ) {
				Name = $"box{MapRoot.Nodes.Count + 1}",
				Size = new Size ( 30, 30 ),
				Location = new Point ( ( this.designer.Width / 2 ) - 30, ( this.designer.Height / 2 ) - 30 )
			};
			mi.OnSelect += delegate(object s, EventArgs ea) {
				this.mapBoxProperties.SelectedObject = mi.MapItem;
			};
			MapRoot.Nodes.Add ( new MapItemTreeItem ( mi ) );
			MapRoot.Expand ( );
			this.designer.Controls.Add ( mi );
		}

	

		private void Save_Click ( object sender, EventArgs e ) {
			var map = new Map ( );

			float diffX = Settings.ScreenWidth / designer.Width;
			float diffY = Settings.ScreenHeight / designer.Height;
			Console.WriteLine ( $"designerW: {designer.Width}; designerH: {designer.Height}" );

			Console.WriteLine ( $"diffX: {diffX}; diffY: {diffY}" );

			foreach ( var ctrl in designer.Controls ) {
				if ( ctrl is MapBox ) {
					var mb = ctrl as MapBox;
					var w = (int)Math.Ceiling ( mb.Size.Width * diffX );
					var h = (int)Math.Ceiling ( mb.Size.Height * diffY );
					var x = (int)Math.Ceiling ( mb.Location.X * diffX );
					var y = (int)Math.Ceiling ( mb.Location.Y * diffY );
					map.Items.Add ( new MapItem {
						Location = new MapPoint {
							X = x + (w / 2),
							Y = y + (h / 2)
						},
						Size = new MapSize {
							Width = w,
							Height = h
						}
					} );
				}
			}

			var ser = new JsonSerializer ( );
			var sfd = new SaveFileDialog ( ) {
				AddExtension = true,
				DefaultExt = "json",
				Filter = "JSON|*.json",
				OverwritePrompt = true,
				Title = "Save Map"
			};
			if ( sfd.ShowDialog ( ) == DialogResult.OK ) {
				using ( var file = File.CreateText ( sfd.FileName ) ) {
					JsonSerializer serializer = new JsonSerializer ( );
					serializer.Serialize ( file, map );
				}
			}
		}

		private void LoadImage_Click ( object sender, EventArgs e ) {
			var ofd = new OpenFileDialog ( ) {
				Filter = "Images | *.jpg;*.png"
			};
			if ( ofd.ShowDialog ( ) == DialogResult.OK ) {
				var bmp = Bitmap.FromFile ( ofd.FileName );
				this.designer.BackgroundImage = bmp;

			}
		}
	}
}
