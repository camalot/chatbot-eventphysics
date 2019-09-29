using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapMaker {
	public partial class MapBox : UserControl {

		public event EventHandler OnSelect;

		public MapBox ( ) {
			InitializeComponent ( );
			ResizeRedraw = true;
			BackColor = Color.Red;
		}
		private const int GRAB = 16;
		private Point InitLocation = Point.Empty;

		public MapItem MapItem {
			get {
				return new MapItem {
					Name = Name,
					Location = new MapPoint {
						X = Location.X,
						Y = Location.Y
					},
					Size = new MapSize {
						Height = Size.Height,
						Width = Size.Width
					}
				};
			}
		}

		protected override void OnPaint ( PaintEventArgs e ) {
			base.OnPaint ( e );

			var rc = new Rectangle ( this.ClientSize.Width - GRAB, this.ClientSize.Height - GRAB, GRAB, GRAB );
			//ControlPaint.DrawSizeGrip ( e.Graphics, this.BackColor, rc );
		}

		protected override void OnMouseDown ( MouseEventArgs e ) {
			base.OnMouseDown ( e );
			InitLocation = e.Location;
			OnSelect?.Invoke ( this, EventArgs.Empty );
		}

		protected override void OnMouseMove ( MouseEventArgs e ) {
			base.OnMouseMove ( e );
			if ( e.Button == MouseButtons.Left ) {
				this.Left += e.X - InitLocation.X;
				this.Top += e.Y - InitLocation.Y;
			}
		}



		protected override void WndProc ( ref Message m ) {
			base.WndProc ( ref m );
			if ( m.Msg == 0x84 ) {  // Trap WM_NCHITTEST
				var pos = this.PointToClient ( new Point ( m.LParam.ToInt32 ( ) ) );
				if ( pos.X >= this.ClientSize.Width - GRAB && pos.Y >= this.ClientSize.Height - GRAB )
					m.Result = new IntPtr ( 17 );  // HT_BOTTOMRIGHT
			}
		}
	}
}
