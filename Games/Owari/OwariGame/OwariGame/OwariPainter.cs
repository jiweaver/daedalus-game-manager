/* $Id$
 * 
 * Description: The OwariPainter class takes a PictureBox as a parameter to its
 * constructor and latches onto it; then it draws the display within that box
 * whenever a game state is passed to the Draw method.
 *
 * Copyright (c) 2010, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and 
 * Justin Weaver).
 * 
 * Distributed under the BSD-new license. For details see the BSD_LICENSE file 
 * that should have been included with this distribution. If the source you
 * acquired this distribution from incorrectly removed this file, the license 
 * may be viewed at http://www.opensource.org/licenses/bsd-license.php. 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using OwariGame;
using DaedalusGameProtocol;

namespace OwariGame
{
    public class OwariPainter : IGamePainter
    {
        protected PictureBox pictureBox1;

        // UNSCALED CONSTANTS.
        protected abstract class BackgroundImageSizeInfo
        {
            public static Size BucketSize = new Size(133, 173);
            public static Point[] BucketOffsets = new Point[]{
                // Bottom Positions.
                new Point(185,754), //Pos 0
                new Point(337,754), //Pos 1
                new Point(489,754), //Pos 2
                new Point(641,754), //Pos 3
                new Point(793,754), //Pos 4
                new Point(946,754), //Pos 5

                new Point(994,545), //Pos 6 [Right Tray]

                // Top Positions.
                new Point(946,341), //Pos 7
                new Point(793,341), //Pos 8
                new Point(641,341), //Pos 9
                new Point(489,341), //Pos 10
                new Point(337,341), //Pos 11
                new Point(185,341), //Pos 12

                new Point(132,545), //Pos 13 [Left Tray]
            };

            public static Point BucketTextOffset = new Point(25, 33);
        }

        public class _CurrentImageInfo
        {
            // Offset of the background image within the PictureBox.
            public Point ImageOffset;

            public Size BucketSize;
            public Point[] BucketOffsets = new Point[14];
            public Point BucketTextOffset;

            public Size PieceSize;
        }

        public _CurrentImageInfo CurrentImageInfo;

        public OwariPainter(PictureBox aPictureBox)
        {
            this.pictureBox1 = aPictureBox;
            CurrentImageInfo = new _CurrentImageInfo();
            this.pictureBox1.Resize += pictureBox1_Resize;
            this.pictureBox1.Image = Properties.Resources.GreenOwariBoardVersion1_RFC_;
            UpdateScalingInfo();
        }

        // Takes a screen coordinate and returns a board position.
        public int PictureBoxCoordsToBoardPosition(PointF p)
        {
            int pos = -1;

            for (int i = 0; i < BackgroundImageSizeInfo.BucketOffsets.Length; i++)
            {
                PointF m = CurrentImageInfo.BucketOffsets[i];
                int maxXError = (CurrentImageInfo.BucketSize.Width / 2) + 5;
                int maxYError = (CurrentImageInfo.BucketSize.Height / 2) + 5;
                if ((p.X >= m.X - (CurrentImageInfo.PieceSize.Width / 2) - maxXError) && (p.X <= m.X + (CurrentImageInfo.PieceSize.Width / 2) + maxXError) && (p.Y >= m.Y - (CurrentImageInfo.PieceSize.Height / 2) - maxYError) && (p.Y <= m.Y + (CurrentImageInfo.PieceSize.Height / 2) + maxYError))
                {
                    // Found it!
                    pos = i;
                    break;
                }
            }

            if (pos < 0)
                throw new Exception("Specified coordinate is not on a piece.\n");

            return pos;
        }

        // Takes a board position and returns the coordinate of the center of
        // that position on the screen.
        public PointF BoardPositionToPictureBoxCoords(int pos)
        {
            if (pos < 0 || pos >= BackgroundImageSizeInfo.BucketOffsets.Length)
                throw new Exception("Position out of range");

            return new PointF(CurrentImageInfo.BucketOffsets[pos].X, CurrentImageInfo.BucketOffsets[pos].Y);
        }

        // Draw the words "Game Over" in big red letters on the PictureBox.
        private void DrawGameOver(Graphics g)
        {
            using (Font f = new Font("Arial", 34))
            using (SolidBrush b = new SolidBrush(Color.Red))
            using (StringFormat stringFormat = new StringFormat())
            {
                PointF center = new PointF(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);

                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                g.DrawString("Game Over", f, b, center, stringFormat);
            }
        }

        public void Draw(Graphics g, GameState gState)
        {
            OwariGameState state = (OwariGameState)gState;

            for (int pos = 0; pos < 14; pos++)
                DrawBucket(g, state, pos);

            if (state.GameIsOver)
                DrawGameOver(g);
        }

        protected PointF ApplyBucketTextOffset(PointF p)
        {
            return new PointF(p.X + CurrentImageInfo.BucketTextOffset.X, p.Y + CurrentImageInfo.BucketTextOffset.Y);
        }

        protected PointF ApplyRectangleCornerOffset(PointF p)
        {
            int pieceRadius = CurrentImageInfo.PieceSize.Width / 2;
            return new PointF(p.X - pieceRadius, p.Y - pieceRadius);
        }

        private void DrawBucket(Graphics g, OwariGameState state, int pos)
        {
            int pieceCount = state.Board.Buckets[pos];

            if (pieceCount == 0)
                return;

            PointF pieceLocation = BoardPositionToPictureBoxCoords(pos);

            PointF piecePos = ApplyRectangleCornerOffset(pieceLocation);

            g.DrawImage(Properties.Resources.OwariPiece, new RectangleF(piecePos.X, piecePos.Y, CurrentImageInfo.PieceSize.Width, CurrentImageInfo.PieceSize.Height));

            // Draw the pieceCount on the piece.
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                PointF textPos = ApplyBucketTextOffset(pieceLocation);

                GraphicsPath gPath = new GraphicsPath();

                gPath.AddString(pieceCount.ToString(), new FontFamily("Arial"), (int)FontStyle.Bold, 18.0f, new PointF(textPos.X, textPos.Y), stringFormat);

                // Fill in the outline.
                g.FillPath(Brushes.Orange, gPath);
                // Draw the outline.
                g.DrawPath(new Pen(Color.DarkRed, 1.5f), gPath);
            }
        }

        // Calculate the current scale and pieceWidth (from the window size) for
        // the board display.
        private void UpdateScalingInfo()
        {
            float scale;

            // Calculate the graphics scaling ratio, depending on the current
            // window size.
            if (this.pictureBox1.Width > this.pictureBox1.Height)
            {
                scale = (float)this.pictureBox1.Height / Properties.Resources.GreenOwariBoardVersion1_RFC_.Height;
                CurrentImageInfo.ImageOffset.X = (this.pictureBox1.Width - this.pictureBox1.Height) / 2;
                CurrentImageInfo.ImageOffset.Y = 0;
            }
            else
            {
                scale = (float)this.pictureBox1.Width / Properties.Resources.GreenOwariBoardVersion1_RFC_.Width;
                CurrentImageInfo.ImageOffset.X = 0;
                CurrentImageInfo.ImageOffset.Y = (this.pictureBox1.Height - this.pictureBox1.Width) / 2;
            }

            CurrentImageInfo.BucketSize = new Size(Convert.ToInt32(scale * BackgroundImageSizeInfo.BucketSize.Width), Convert.ToInt32(scale * BackgroundImageSizeInfo.BucketSize.Height));

            CurrentImageInfo.PieceSize = new Size(Convert.ToInt32(scale * Properties.Resources.OwariPiece.Width), Convert.ToInt32(scale * Properties.Resources.OwariPiece.Height));

            CurrentImageInfo.BucketTextOffset = new Point(Convert.ToInt32(scale * BackgroundImageSizeInfo.BucketTextOffset.X), Convert.ToInt32(scale * BackgroundImageSizeInfo.BucketTextOffset.Y));

            for (int pos = 0; pos < CurrentImageInfo.BucketOffsets.Length; pos++)
                CurrentImageInfo.BucketOffsets[pos] = new Point(CurrentImageInfo.ImageOffset.X + Convert.ToInt32(BackgroundImageSizeInfo.BucketOffsets[pos].X * scale), CurrentImageInfo.ImageOffset.Y + Convert.ToInt32(BackgroundImageSizeInfo.BucketOffsets[pos].Y * scale));
        }

        // Called when the window gets resized.
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            UpdateScalingInfo();

            pictureBox1.Invalidate();
        }
    }
}