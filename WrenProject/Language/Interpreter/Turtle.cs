using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Language
{
    internal static class Turtle
    {
        private static Control DrawControl { get; set; }
        private static Graphics DrawGraphics { get; set; }
        private static Pen DrawPen { get; set; }

        private static PictureBox TurtleImage { get; set; }

        private static DrawingPanel forms;

        private static float X { get; set; }
        private static float Y { get; set; }

        private static int PenWidth { get; set; }

        public static double Direction { get; set; }

        static List<Action> functions = new();


        static Turtle()
        {
            Clear();
        }

        static void Init()
        {
            Dispose();
            Direction = 0;
            PenWidth = 4;

            DrawControl = forms;

            DrawPen = new Pen(Color.Black, PenWidth);
            DrawPen.StartCap = LineCap.Round;
            DrawPen.EndCap = LineCap.Round;


            DrawGraphics = forms.Graphics;
            forms.Canvas.Paint += CanvasOnPaint;
            X = forms.ClientSize.Width / 2;
            Y = forms.ClientSize.Height / 2;
            // TurtleImage = new PictureBox();
            // TurtleImage.Left = (int) Y;
            // TurtleImage.Top = (int) X;
            // TurtleImage.Size = new Size(100, 100);
            // TurtleImage.BackColor = Color.Transparent;
            //forms.Controls.Add(TurtleImage);
        }

        private static void CanvasOnPaint(object sender, PaintEventArgs e)
        {
            foreach (Action func in functions)
            {
                Thread.Sleep(200);
                func();
            }
        }

        public static void Forward()
        {
            functions.Add(() => Forward(1));
        }

        public static void Forward(double steps)
        {
            functions.Add(() => DrawLine(steps));
        }

        public static void Right(double degrees)
        {
            functions.Add(() => Direction += degrees);
        }

        public static void Left(double degrees)
        {
            Right(-degrees);
        }


        public static void DrawLine(double steps)
        {
            var radians = Direction * (Math.PI / 180);
            var oldX = X;
            var oldY = Y;
            X += (float) (steps * Math.Cos(radians));
            Y += (float) (steps * Math.Sin(radians));
            DrawGraphics.DrawLine(DrawPen, oldX, oldY, X, Y);
            //DrawGraphics.Draw(TurtleImage.BackgroundImage, X, Y);
            // DrawTurtle();
        }

        private static void DrawTurtle()
        {
            var turtleImg = new Bitmap(@"E:\Programovany\ITEJA_ICSHP_Semestralka\WrenProject\Language\turtle.png");
            var turtleImgSize = Math.Max(turtleImg.Width, turtleImg.Height);
            TurtleImage.BackgroundImage = turtleImg;
            TurtleImage.Width = turtleImg.Width;
            TurtleImage.Height = turtleImg.Height;

            TurtleImage.Left = 100;
            TurtleImage.Top = 100;
        }

        public static void Done()
        {
            forms.ShowDialog();
        }

        public static void Clear()
        {
            forms = new DrawingPanel();
            Init();
        }

        private static void Dispose()
        {
            if (DrawControl != null)
            {
                forms.Canvas.Paint -= CanvasOnPaint;
                DrawPen.Dispose();
                DrawPen = null;
                DrawGraphics.Dispose();
                DrawGraphics = null;
                DrawControl.Invalidate();
                DrawControl = null;
            }
        }
    }
}