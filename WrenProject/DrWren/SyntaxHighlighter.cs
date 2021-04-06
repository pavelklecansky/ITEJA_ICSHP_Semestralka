using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Language;
using Language.Lexer;

namespace DrWren
{
    public static class SyntaxHighlighter
    {
        private static List<string> Keywords { get; } = new();
        private static List<string> Operators { get; } = new();

        private static List<string> String { get; } = new();

        static SyntaxHighlighter()
        {
            //Keywords initialization
            Keywords.Add("var");
            Keywords.Add("while");
            Keywords.Add("if");
            Keywords.Add("else");
            Keywords.Add("System");
            Keywords.Add("Turtle");
            // //Óperators inicialization
            Operators.Add("=");
            Operators.Add("!=");
            Operators.Add("==");
            Operators.Add(">=");
            Operators.Add(">");
            Operators.Add("<=");
            Operators.Add("-");
            Operators.Add("/");
            Operators.Add("%");

            String.Add("(\"[^\"\r\n]*\")");
        }

        public static void Highlight(RichTextBox textBox)
        {
            ClearColor(textBox);
            ColorText(textBox, Keywords, Color.Red);
            ColorText(textBox, Operators, Color.Red);
            ColorText(textBox, String, Color.Orange); 
            
        }

        private static void ClearColor(RichTextBox textBox)
        {
            var position = textBox.SelectionStart + textBox.SelectionLength;
            textBox.SelectAll();
            textBox.SelectionColor = Color.Black;
            textBox.Select(position, 0);
        }

        private static void ColorText(RichTextBox textBox, List<string> keywords, Color color)
        {
            var text = textBox.Text;
            var indexAndSize = AllIndexOfAndSize(text, keywords);
            foreach (var tuple in indexAndSize)
            {
                var position = textBox.SelectionStart + textBox.SelectionLength;
                var oldColor = textBox.SelectionColor;
                textBox.Select(tuple.Item1, tuple.Item2);
                textBox.SelectionColor = color;
                textBox.Select(position, 0);
                textBox.SelectionColor = oldColor;
            }
        }

        private static List<Tuple<int, int>> AllIndexOfAndSize(string text, List<string> keywords)
        {
            var indexesAndSize = new List<Tuple<int, int>>();
            foreach (var keyword in keywords)
            {
                foreach (Match match in Regex.Matches(text, keyword))
                {
                    indexesAndSize.Add(new Tuple<int, int>(match.Index, match.Length));
                }
            }

            return indexesAndSize;
        }
    }
}