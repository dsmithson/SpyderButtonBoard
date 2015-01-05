using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spyder.Client.ButtonBoardUI.Controls
{
    public partial class ButtonBoardControl : UserControl
    {
        private List<ButtonState> buttons = new List<ButtonState>();

        private int horizontalCount = 2;
        public int HorizontalCount
        {
            get { return horizontalCount; }
            set
            {
                if(horizontalCount != value)
                {
                    horizontalCount = value;
                    UpdateButtonArraySize();
                }
            }
        }

        private int verticalCount = 2;
        public int VerticalCount
        {
            get { return verticalCount; }
            set
            {
                if(verticalCount != value)
                {
                    verticalCount = value;
                    UpdateButtonArraySize();
                }
            }
        }

        private int buttonPadding = 4;
        public int ButtonPadding
        {
            get { return buttonPadding; }
            set
            {
                if(buttonPadding != value)
                {
                    buttonPadding = value;
                    UpdateButtonRects();
                }
            }

        }

        private Color defaultButtonColor = Color.WhiteSmoke;
        public Color DefaultButtonColor
        {
            get { return defaultButtonColor; }
            set
            {
                if(defaultButtonColor != value)
                {
                    defaultButtonColor = value;
                    Invalidate();
                }
            }
        }

        public ButtonBoardControl()
        {
            InitializeComponent();
            UpdateButtonArraySize();
        }
        
        public Color? GetColor(int row, int column)
        {
            int buttonID = (row * horizontalCount) + column;
            return GetColor(buttonID);
        }

        public Color? GetColor(int buttonID)
        {
            if (buttonID < 0 || buttonID >= buttons.Count)
                return null;
            else
                return buttons[buttonID].Color;
        }

        public void SetColor(int row, int column, Color? color)
        {
            int buttonID = (row * horizontalCount) + column;
            SetColor(buttonID, color);
        }

        public void SetColor(int buttonID, Color? color)
        {
            var button = buttons[buttonID];
            if (button.Color != color)
            {
                button.Color = color;
                Invalidate(button.Rect);
            }
        }

        private void UpdateButtonArraySize()
        {
            int buttonCount = (horizontalCount * verticalCount);
            if(buttonCount != buttons.Count)
            {
                while(buttonCount > buttons.Count)
                {
                    buttons.Add(new ButtonState());
                }
                while(buttonCount < buttons.Count)
                {
                    buttons.RemoveAt(buttons.Count - 1);
                }

                //Update the button rectangles when done
                UpdateButtonRects();
            }
        }

        private void UpdateButtonRects(bool invalidateWhenDone = true)
        {
            int totalHorizontalPadding = buttonPadding * (horizontalCount + 1);
            int totalVerticalPadding = buttonPadding * (verticalCount + 1);
            int availableWidth = this.ClientRectangle.Width - totalHorizontalPadding;
            int availableHeight = this.ClientRectangle.Height - totalVerticalPadding;

            //Constrain to fit AR of our control
            float targetButtonAR = (float)horizontalCount / (float)verticalCount;
            float clientRectAR = (float)this.ClientRectangle.Width / (float)this.ClientRectangle.Height;
            if(targetButtonAR < clientRectAR)
            {
                availableWidth = (int)Math.Round(availableHeight * targetButtonAR);
            }
            else if(targetButtonAR > clientRectAR)
            {
                availableHeight = (int)Math.Round(availableWidth * targetButtonAR);
            }

            int startX = (this.ClientRectangle.Width - availableWidth) / 2;
            int startY = (this.ClientRectangle.Height - availableHeight) / 2;
            int buttonWidth = availableWidth / horizontalCount;
            int buttonHeight = availableHeight / horizontalCount;

            int buttonIndex = 0;
            for(int row = 0; row < verticalCount; row++)
            {
                int y = startY + (row * buttonHeight) + (buttonPadding * row);
                for(int column = 0; column < horizontalCount; column++)
                {
                    int x = startX + (column * buttonWidth) + (buttonPadding * column);
                    buttons[buttonIndex++].Rect = new Rectangle(x, y, buttonWidth, buttonHeight);
                }
            }

            if (invalidateWhenDone)
                Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using(Bitmap b = new Bitmap(this.Width, this.Height))
            {
                using(Graphics g = Graphics.FromImage(b))
                {
                    //Clear background
                    g.Clear(this.BackColor);

                    var brushes = new Dictionary<Color, Brush>();
                    foreach(var button in buttons)
                    {
                        //Get/cache brush
                        Color color = button.Color == null ? defaultButtonColor : button.Color.Value;
                        Brush brush = (brushes.ContainsKey(color) ? brushes[color] : null);
                        if(brush == null)
                        {
                            brush = new SolidBrush(color);
                            brushes.Add(color, brush);
                        }

                        //Draw button
                        g.FillRectangle(brush, button.Rect);
                        g.DrawRectangle(Pens.Black, button.Rect);
                    }
                }

                //Now blit the buttons to the main graphics instance
                e.Graphics.DrawImage(b, Point.Empty);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void ButtonBoardControl_SizeChanged(object sender, EventArgs e)
        {
            UpdateButtonRects();
        }

        private class ButtonState
        {
            public Color? Color { get; set; }
            public Rectangle Rect { get; set; }
        }
    }
}
