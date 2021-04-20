using System.Drawing;
using System.Windows.Forms;

namespace Language.Interpreter.NativeLibrary
{
    internal partial class DrawingPanel : Form
    {

        public Graphics Graphics { get; }
        
        public DrawingPanel()
        {
            Text = @"Turtle Graphics";
            InitializeComponent();
            Graphics = Canvas.CreateGraphics();
            ResizeRedraw = true;
        }

        private void DrawingPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Turtle.Clear();
        }
    }
}