using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseJiggler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int JumpSpace = 5;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            txtJumpSpace.Text = JumpSpace.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
            trackBar1.Enabled = false;
            txtJumpSpace.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            trackBar1.Enabled = true;
            txtJumpSpace.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);

            var points = new Point[8];
            points[0] = new Point(Cursor.Position.X - JumpSpace, Cursor.Position.Y - JumpSpace);
            points[1] = new Point(Cursor.Position.X + JumpSpace, Cursor.Position.Y - JumpSpace);
            points[2] = new Point(Cursor.Position.X + JumpSpace, Cursor.Position.Y + JumpSpace);
            points[3] = new Point(Cursor.Position.X - JumpSpace, Cursor.Position.Y + JumpSpace);
            points[4] = new Point(Cursor.Position.X - JumpSpace, Cursor.Position.Y);
            points[5] = new Point(Cursor.Position.X + JumpSpace, Cursor.Position.Y);
            points[6] = new Point(Cursor.Position.X, Cursor.Position.Y - JumpSpace);
            points[7] = new Point(Cursor.Position.X, Cursor.Position.Y + JumpSpace);

            var rnd = new Random();
            int random = rnd.Next(0, points.Length);
            Cursor.Position = points[random];
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblTime.Text = trackBar1.Value.ToString();
            timer1.Interval = trackBar1.Value * 1000;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtJumpSpace_TextChanged(object sender, EventArgs e)
        {
            JumpSpace = Convert.ToInt32(txtJumpSpace.Text);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}
