namespace A2_Graphs_PieChart
{
    public partial class Form1 : Form
    {
        private TextBox txtValues;
        private Button btnDraw;
        private List<float> dataValues;
        private Color[] colors; 
    

        public Form1()
        {
            InitializeComponent();
            dataValues = new List<float>();
            colors = new Color[] { Color.Magenta, Color.DarkOrchid, Color.OliveDrab, Color.Teal, Color.Orange, Color.Purple };

            this.Width = 500;
            this.Height = 400;

            // TextBox and Button
            txtValues = new TextBox 
            { 
                Location = new Point(20, 10), 
                Width = 200 
            };


            btnDraw = new Button 
            { Text = "Draw", 
              Location = new Point(250, 10),
              BackColor = Color.LightBlue,
              Width = 100,
              Height = 40
            };

            btnDraw.Click += (sender, e) => DrawChart(txtValues.Text);

            // Add controls to form
            this.Controls.Add(txtValues);
            this.Controls.Add(btnDraw);

            // Set up the paint event
            this.Paint += Form1_Paint;
        }


        private void DrawChart(string input)
        {
            dataValues.Clear();

            // Parse values from input string
            string[] values = input.Split(',');
            foreach (var value in values)
            {
                if (float.TryParse(value.Trim(), out float number))
                {
                    dataValues.Add(number);
                }
            }

            // Trigger form repaint
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (dataValues.Count == 0) return; // No data to draw

            Graphics g = e.Graphics;
            Rectangle pieRectangle = new Rectangle(50, 50, 200, 200); // Define pie chart area
            float total = dataValues.Sum();
            float startAngle = 0;

            // Draw each slice of the pie chart
            for (int i = 0; i < dataValues.Count; i++)
            {
                float sweepAngle = (dataValues[i] / total) * 360;
                g.FillPie(new SolidBrush(colors[i % colors.Length]), pieRectangle, startAngle, sweepAngle);
                startAngle += sweepAngle;
            }

            // Draw the legend
            for (int i = 0; i < dataValues.Count; i++)
            {
                g.FillRectangle(new SolidBrush(colors[i % colors.Length]), 300, 60 + (i * 20), 10, 10);
                g.DrawString($"Value {dataValues[i]} - {(dataValues[i] / total * 100):F1}%",
                             new Font("Arial", 10), Brushes.Black, 320, 60 + (i * 20));
            }
        }
    }
}
