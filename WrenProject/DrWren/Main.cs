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
            ChangeTitleName("Unititled");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                FileName = OpenFile.FileName;
                ChangeTitleName(FileName);
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
            if (FileName == null)
            {
                SaveFile();
            }
            else
            {
                ChangeTitleName(FileName);
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
                ChangeTitleName(FileName);
                TextBox.SaveFile(FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextBox.Text != "")
            {
                var result = MessageBox.Show("Would you like to save your changes?", "Save changes",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.No)
                {
                    TextBox.ResetText();
                    TextBox.Focus();
                }
            }
            else
            {
                TextBox.ResetText();
                TextBox.Focus();
                FileName = null;
                ChangeTitleName("Untitled");
            }
        }

        private void ChangeTitleName(string name)
        {
            this.Text = name + " - DrWren";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.SelectAll();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(TextBox.Text, TextBox.Font, Brushes.Black, 80, 10);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = PrintDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument.Print();
            }
        }
    }
}