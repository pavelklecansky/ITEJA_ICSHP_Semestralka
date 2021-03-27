using System.Drawing;
using System.Windows.Forms;

namespace Language
{
    internal partial class DrawingPanel : Form
    {

        public Graphics Graphics { get; }
        
        public DrawingPanel()
        {
           
            InitializeComponent();
            Graphics = Canvas.CreateGraphics();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        private void DrawingPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Turtle.Clear();
        }
    }
}