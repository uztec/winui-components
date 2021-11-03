using System;
using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls
{
    public class EnhancedFlowLayoutPanel : Panel
    {
        private HorizontalFlowDirection horizontalFlow;
        private VerticalFlowDirection verticalFlow;
        private bool reverseOrder;
        private Padding internalPadding;

        public event EventHandler HorizontalFlowDirectionChanged;
        public event EventHandler VerticalFlowDirectionChanged;
        public event EventHandler ReverseOrderChanged;
        public event EventHandler InternalPaddingChanged;

        public HorizontalFlowDirection HorizontalFlowDirection
        {
            get => this.horizontalFlow;
            set { this.horizontalFlow = value; this.RearrangeControls(); this.HorizontalFlowDirectionChanged?.Invoke(this, EventArgs.Empty); }
        }

        public VerticalFlowDirection VerticalFlowDirection
        {
            get => this.verticalFlow;
            set { this.verticalFlow = value; this.RearrangeControls(); this.VerticalFlowDirectionChanged?.Invoke(this, EventArgs.Empty); }
        }

        public bool ReverseOrder
        {
            get => this.reverseOrder;
            set { this.reverseOrder = value; this.RearrangeControls(); this.ReverseOrderChanged?.Invoke(this, EventArgs.Empty); }
        }

        public Padding InternalPadding
        {
            get => this.internalPadding;
            set { this.internalPadding = value; this.RearrangeControls(); this.InternalPaddingChanged?.Invoke(this, EventArgs.Empty); }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            this.RearrangeControls();
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            this.RearrangeControls();
        }

        private void RearrangeControls()
        {
            Rectangle lastControlRect = new Rectangle
            {
                X = (this.horizontalFlow == HorizontalFlowDirection.LeftToRight) ? this.ClientRectangle.Left + this.internalPadding.Left: this.ClientRectangle.Right - this.internalPadding.Right,
                Y = (this.verticalFlow == VerticalFlowDirection.TopDown) ? this.ClientRectangle.Top + this.internalPadding.Top : this.ClientRectangle.Bottom - this.internalPadding.Bottom,
                Height = 0,
                Width = 0,
            };

            if (this.reverseOrder)
            {
                for (int i = this.Controls.Count; i > 0; i--)
                {
                    this.SetControlLocation(this.Controls[i - 1], ref lastControlRect);
                }
            }
            else
            {
                foreach (Control child in this.Controls)
                {
                    this.SetControlLocation(child, ref lastControlRect);
                }
            }
        }

        private void SetControlLocation(Control control, ref Rectangle lastControlRect)
        {
            if (this.horizontalFlow == HorizontalFlowDirection.LeftToRight)
            {
                control.Left = lastControlRect.Right + this.internalPadding.Left;
                if (control.Right > this.ClientRectangle.Right)
                {
                    // Todo Next Line
                }
            }
            else
            {
                control.Left = lastControlRect.Left - control.Width - this.internalPadding.Right - this.internalPadding.Left;

                if (control.Left < this.ClientRectangle.Left)
                {
                    // Todo Next Line
                }

            }
            lastControlRect.X = control.Left;
            lastControlRect.Width = control.Width + this.internalPadding.Right;

            if (this.verticalFlow == VerticalFlowDirection.TopDown)
            {
                control.Top = lastControlRect.Top;
            }
            else
            {
                control.Top = lastControlRect.Bottom - control.Height;
            }
        }
    }

    public enum HorizontalFlowDirection { LeftToRight, RightToLeft }
    public enum VerticalFlowDirection { TopDown, BottomUp }

}
