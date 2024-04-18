using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MultiThreadLab2
{
    public partial class Form1 : Form
    {
        private List<Point> vertices = new List<Point>(); // список вершин
        private List<Color> regionColors = new List<Color>(); // список кольорів
        private Bitmap voronoiBitmap; // бітмап для відображення діаграми Вороного
        private bool parallelMode = false; // режим паралельного обчислення
        private Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            voronoiBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = voronoiBitmap;
            this.MaximumSize = new Size(this.Width, this.Height);
            this.MinimumSize = new Size(this.Width, this.Height);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(voronoiBitmap))
            {
                if (e.Button == MouseButtons.Left)
                {
                    int index = vertices.FindIndex(p => Math.Abs(p.X - e.X) < 5 && Math.Abs(p.Y - e.Y) < 5);
                    if (index == -1)
                    {
                        vertices.Add(e.Location);
                        regionColors.Add(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
                        g.Clear(Color.White);
                    }
                    else
                    {
                        if (index != -1)
                        {
                            vertices.RemoveAt(index);
                            regionColors.RemoveAt(index);
                            g.Clear(Color.White);
                        }
                    }
                }
                DrawVertices();
            }
        }

        private void DrawVertices()
        {
            using (Graphics g = Graphics.FromImage(voronoiBitmap))
            {
                foreach (var vertex in vertices)
                {
                    g.FillEllipse(Brushes.Black, vertex.X - 3, vertex.Y - 3, 8, 8);
                }
            }
            pictureBox1.Image = voronoiBitmap;
        }

        private void CalculateVoronoiDiagram()
        {
            using (Graphics g = Graphics.FromImage(voronoiBitmap))
            {
                if (parallelMode)
                {
                    int segmentWidth = voronoiBitmap.Width / Environment.ProcessorCount;
                    int segmentHeight = voronoiBitmap.Height;

                    List<Task> tasks = new List<Task>();

                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        int startX = i * segmentWidth;
                        int endX = (i + 1) * segmentWidth;
                        tasks.Add(Task.Run(() =>
                        {
                            for (int x = startX; x < endX; x++)
                            {
                                for (int y = 0; y < segmentHeight; y++)
                                {
                                    Point pixel = new Point(x, y);
                                    Point nearestVertex = FindNearestVertex(pixel, vertices);
                                    int index = vertices.IndexOf(nearestVertex);
                                    lock (g)
                                    {
                                        g.FillRectangle(new SolidBrush(regionColors[index]), pixel.X, pixel.Y, 1, 1);
                                    }
                                }
                            }
                            
                        }));
                    }
                    Task.WaitAll(tasks.ToArray());
                    DrawVertices();
                }
                else
                {
                    foreach (Point pixel in GetPixels(voronoiBitmap.Width, voronoiBitmap.Height))
                    {
                        Point nearestVertex = FindNearestVertex(pixel, vertices);
                        int index = vertices.IndexOf(nearestVertex);
                        if (index != -1)
                        {
                            g.FillRectangle(new SolidBrush(regionColors[index]), pixel.X, pixel.Y, 1, 1);
                        }
                    }
                    DrawVertices();
                }
            }
        }

        private Point FindNearestVertex(Point pixel, List<Point> vertices)
        {
            double minDistance = double.MaxValue;
            Point nearestVertex = Point.Empty;
            foreach (Point vertex in vertices)
            {
                double distance = Distance(pixel, vertex);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestVertex = vertex;
                }
            }
            return nearestVertex;
        }

        // Відстань за теоремою піфагора
        private double Distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private IEnumerable<Point> GetPixels(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Повертає кожну точку одна за одною як частину послідовності
                    yield return new Point(x, y);
                }
            }
        }

        private void btnGenerateRandom_Click(object sender, EventArgs e)
        {
            vertices.Clear();
            regionColors.Clear();

            for (int i = 0; i < 500; i++)
            {
                int x = rand.Next(pictureBox1.Width);
                int y = rand.Next(pictureBox1.Height);
                vertices.Add(new Point(x, y));
                regionColors.Add(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
            }

            DrawVertices();
            CalculateVoronoiDiagram();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            parallelMode = checkBox1.Checked;
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            CalculateVoronoiDiagram();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(voronoiBitmap))
            {
                vertices.Clear();
                regionColors.Clear();
                g.Clear(Color.White);
                DrawVertices();
            }                
        }
    }
}
