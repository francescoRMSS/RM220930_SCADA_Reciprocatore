using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

public class CustomRadioButton : Control
{
    private bool isChecked;
    private const int RadioButtonDiameter = 26; // Dimensione del pallino

    public event EventHandler CheckedChanged;

    // Proprietà per i colori personalizzabili con attributi per il designer
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color UncheckedColor { get; set; } = Color.Black;

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color CheckedColor { get; set; } = Color.Black;

    public bool Checked
    {
        get { return isChecked; }
        set
        {
            if (isChecked != value)
            {
                isChecked = value;
                Invalidate(); // Richiede il ridisegno del controllo
                OnCheckedChanged(EventArgs.Empty); // Solleva l'evento CheckedChanged
                if (isChecked && Parent != null)
                {
                    foreach (Control control in Parent.Controls)
                    {
                        if (control is CustomRadioButton && control != this)
                        {
                            ((CustomRadioButton)control).Checked = false;
                        }
                    }
                }
            }
        }
    }

    public CustomRadioButton()
    {
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        this.Size = new Size(200, RadioButtonDiameter);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Disegna il pallino del RadioButton
        Rectangle radioButtonRect = new Rectangle(0, 0, RadioButtonDiameter, RadioButtonDiameter);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.DrawEllipse(new Pen(UncheckedColor), radioButtonRect);

        if (isChecked)
        {
            int innerDiameter = RadioButtonDiameter - 6;
            int innerRadius = innerDiameter / 2;
            Rectangle innerCircleRect = new Rectangle(3, 3, innerDiameter, innerDiameter);
            e.Graphics.FillEllipse(new SolidBrush(CheckedColor), innerCircleRect);
        }

        // Disegna il testo accanto al pallino
        e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), RadioButtonDiameter + 5, (this.Height - this.Font.Height) / 2);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            Checked = true;
        }
    }

    protected virtual void OnCheckedChanged(EventArgs e)
    {
        CheckedChanged?.Invoke(this, e);
    }
}
