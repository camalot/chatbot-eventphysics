using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMaker {
	public class MapItemTreeItem : System.Windows.Forms.TreeNode {

		public MapItemTreeItem (MapBox mapItem ) : base( mapItem.Name) {
			MapItem = mapItem;
		}

		public MapBox MapItem { get; set; }
	}
}
