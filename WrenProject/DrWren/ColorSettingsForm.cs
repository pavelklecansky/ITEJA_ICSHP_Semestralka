using System;
using System.Windows.Forms;

namespace DrWren
{
    public partial class ColorSettingsForm : Form
    {
        private readonly ColorSettings _colorSettings;


        public ColorSettingsForm(ColorSettings colorSettings)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            _colorSettings = colorSettings;
            InitializeComponent();
            var colors = colorSettings.Load();
            colorDialogLiterals.Color = colors.Literals;
            colorDialogKeywords.Color = colors.Keywords;
            colorDialogOperators.Color = colors.Operators;
            buttonLiterals.BackColor = colors.Literals;
            buttonKeywords.BackColor = colors.Keywords;
            buttonOperators.BackColor = colors.Operators;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _colorSettings.Save(colorDialogLiterals.Color, colorDialogOperators.Color, colorDialogKeywords.Color);
            Close();
        }

        private void buttonLiterals_Click(object sender, EventArgs e)
        {
            if (colorDialogLiterals.ShowDialog() == DialogResult.OK)
            {
                buttonLiterals.BackColor = colorDialogLiterals.Color;
            }
        }

        private void buttonOperators_Click(object sender, EventArgs e)
        {
            if (colorDialogOperators.ShowDialog() == DialogResult.OK)
            {
                buttonOperators.BackColor = colorDialogOperators.Color;
            }
        }

        private void buttonKeywords_Click(object sender, EventArgs e)
        {
            if (colorDialogKeywords.ShowDialog() == DialogResult.OK)
            {
                buttonKeywords.BackColor = colorDialogKeywords.Color;
            }
        }
    }
}