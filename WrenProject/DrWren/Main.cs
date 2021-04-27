using System;
using System.Drawing;
using System.Drawing.Printing;
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
            TextBox.KeyUp += TextBoxOnTextChanged;
            colorToolStripMenuItem.Click += colorToolStripMenuItem_Click;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = ColorSettings.Instance;
            settings.Open();
        }

        private void TextBoxOnTextChanged(object sender, EventArgs e)
        {
            HighlightText();
        }

        private void HighlightText()
        {
            if (TextBox.Modified)
            {
                errorslabel.Focus();
                SyntaxHighlighter.Highlight(TextBox);
                TextBox.Focus();
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

            HighlightText();

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
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFile();
                        break;
                    case DialogResult.No:
                        TextBox.ResetText();
                        TextBox.Focus();
                        break;
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

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
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
            RunWren();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunWren();
        }

        private void OnRanCode(object source, CodeEventArgs e)
        {
            CodeRunningTime.Text = e.CodeRunningTime + " ms";
        }

        private void RunWren()
        {
            OutputTextBox.Clear();
            var runner = new CodeRunner(TextBox.Text, new OutputWriter(OutputTextBox));
            runner.CodeRan += OnRanCode;
            runner.Run();
        }
    }
}