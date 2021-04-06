using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrWren
{
    public partial class Main : Form
    {
        private string FileName { get; set; }

        public Main()
        {
            InitializeComponent();
            ChangeTitleName("Untitled");
            TextBox.TextChanged += TextBoxOnTextChanged;
        }

        private void TextBoxOnTextChanged(object sender, EventArgs e)
        {
            if (TextBox.Modified)
            {
                SyntaxHighlighter.Highlight(TextBox);
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                FileName = OpenFile.FileName;
                ChangeTitleName(FileName);
                TextBox.LoadFile(FileName, RichTextBoxStreamType.PlainText);
            }

            SyntaxHighlighter.Highlight(TextBox);
            TextBox.Select(0, 0);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            TextBox.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            OpenFile.DefaultExt = "wren";
            OpenFile.Filter = @"Wren file (*.wren)|*.wren| Text files (*.txt)|*.txt | All files (*.*)|*.*";
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
            var saveFile = new SaveFileDialog
            {
                DefaultExt = "wren",
                Filter = @"Wren file (*.wren)|*.wren| Text files (*.txt)|*.txt | All files (*.*)|*.*"
            };


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
                var result = MessageBox.Show(@"Would you like to save your changes?", @"Save changes",
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
            Text = name + @" - DrWren";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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
            var printDialog = new PrintDialog {Document = PrintDocument};
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument.Print();
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            Language.Wren.Run(TextBox.Text, new OutputWriter(OutputTextBox));
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language.Wren.Run(TextBox.Text, new OutputWriter(OutputTextBox));
        }
    }
}