using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMaker {
	public class MapRootTreeItem : System.Windows.Forms.TreeNode {
		public MapRootTreeItem ( ) : base("Map") {
			this.ExpandAll ( );
		}
	}
}
