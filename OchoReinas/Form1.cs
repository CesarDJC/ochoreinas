using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OchoReinas
{
    public partial class Form1 : Form
    {

        private const int N = 8;
        private int[] reinas = new int[N]; // fila -> columna
        private List<int[]> soluciones = new List<int[]>();
        private int indiceSolucion = 0;


        private Panel panelTablero;
        private Button btnResolver;
        private Button btnSiguiente;
        private Label lblInfo;

        public Form1()
        {
            InitializeComponent();
            InicializarFormulario();
        }
        private void InicializarFormulario()
        {
            Text = "Problema de las 8 Reinas";
            Width = 450;
            Height = 520;


            panelTablero = new Panel
            {
                Width = 320,
                Height = 320,
                Top = 20,
                Left = 50
            };
            panelTablero.Paint += PanelTablero_Paint;


            btnResolver = new Button
            {
                Text = "Resolver",
                Top = 360,
                Left = 50,
                Width = 120
            };
            btnResolver.Click += BtnResolver_Click;


            btnSiguiente = new Button
            {
                Text = "Siguiente solución",
                Top = 360,
                Left = 200,
                Width = 150,
                Enabled = false
            };
            btnSiguiente.Click += BtnSiguiente_Click;


            lblInfo = new Label
            {
                Top = 420,
                Left = 50,
                Width = 300
            };


            Controls.Add(panelTablero);
            Controls.Add(btnResolver);
            Controls.Add(btnSiguiente);
            Controls.Add(lblInfo);
        }

        private void BtnResolver_Click(object sender, EventArgs e)
        {
            soluciones.Clear();
            indiceSolucion = 0;
            Resolver(0);


            if (soluciones.Count == 0)
            {
                MessageBox.Show("No se encontraron soluciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            reinas = soluciones[0];
            btnSiguiente.Enabled = true;
            lblInfo.Text = $"Solución 1 de {soluciones.Count}";
            panelTablero.Invalidate();
        }


        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            indiceSolucion++;
            if (indiceSolucion >= soluciones.Count)
                indiceSolucion = 0;


            reinas = soluciones[indiceSolucion];
            lblInfo.Text = $"Solución {indiceSolucion + 1} de {soluciones.Count}";
            panelTablero.Invalidate();
        }


        private void Resolver(int fila)
        {
            if (fila == N)
            {
                int[] copia = new int[N];
                reinas.CopyTo(copia, 0);
                soluciones.Add(copia);
                return;
            }


            for (int col = 0; col < N; col++)
            {
                if (EsSeguro(fila, col))
                {
                    reinas[fila] = col;
                    Resolver(fila + 1);
                }
            }
        }


        private bool EsSeguro(int fila, int col)
        {
            for (int i = 0; i < fila; i++)
            {
                if (reinas[i] == col || Math.Abs(reinas[i] - col) == Math.Abs(i - fila))
                    return false;
            }
            return true;
        }

        private void PanelTablero_Paint(object sender, PaintEventArgs e)
        {
            int size = panelTablero.Width / N;
            Graphics g = e.Graphics;


            // Dibujar el tablero
            for (int fila = 0; fila < N; fila++)
            {
                for (int col = 0; col < N; col++)
                {
                    Brush brush = ((fila + col) % 2 == 0) ? Brushes.Beige : Brushes.SaddleBrown;
                    g.FillRectangle(brush, col * size, fila * size, size, size);
                }
            }


            // Dibujar las reinas como círculos negros centrados
            int margen = size / 6;
            int diametro = size - margen * 2;


            for (int fila = 0; fila < N; fila++)
            {
                int col = reinas[fila];
                int x = col * size + margen;
                int y = fila * size + margen;


                g.FillEllipse(Brushes.Black, x, y, diametro, diametro);
            }
        


            for (int fila = 0; fila<N; fila++)
            {
            int col = reinas[fila];
                    int x = col * size + margen;
                    int y = fila * size + margen;


                    g.FillEllipse(Brushes.Black, x, y, diametro, diametro);

            }

}





        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
