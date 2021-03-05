using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrWren
{
    public partial class Main : Form
    {
        public string FileName { get; set; }

        public Main()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog()== DialogResult.OK)
            {
                FileName = OpenFile.FileName;
                TextBox.LoadFile(FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            TextBox.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            OpenFile.DefaultExt = "wren";
            OpenFile.Filter = "Wren file (*.wren)|*.wren| Text files (*.txt)|*.txt";

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FileName == null)
            {
                SaveFile();
            }
            else
            {
                TextBox.SaveFile(FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            SaveFileDialog saveFile = new SaveFileDialog();

           
            saveFile.DefaultExt = "wren";
            saveFile.Filter = "Wren file (*.wren)|*.wren| Text files (*.txt)|*.txt";

            if (saveFile.ShowDialog() == DialogResult.OK &&
               saveFile.FileName.Length > 0)
            {
                FileName = saveFile.FileName;
                TextBox.SaveFile(FileName, RichTextBoxStreamType.PlainText);
            }
        }
    }
}