using System.Drawing;
using System.Windows.Forms;

namespace Language.Interpreter.NativeLibrary
{
    /// <summary>
    /// Form app for showing turtle graphics.
    /// </summary>
    internal partial class TurtleGraphicsPanel : Form
    {

        public Graphics Graphics { get; }
        
        public TurtleGraphicsPanel()
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