using System.Drawing;
using System.Windows.Forms;

namespace Language
{
    public partial class DrawingPanel : Form
    {

        public Graphics Graphics { get; private set; }
        
        public DrawingPanel()
        {
            
            InitializeComponent();
            Graphics = Canvas.CreateGraphics();
        }
        
    }
}