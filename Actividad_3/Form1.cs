using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Actividad_3
{
    public partial class Form1 : Form
    {
        bool band;
        Pen pen1 = new Pen(Color.Red, 1);
        Pen pen2 = new Pen(Color.Black, 1);
        Pen pen3 = new Pen(Color.Blue, 1);
        Bitmap bmp = new Bitmap(800, 500);
        int x1, y1,x2,y2, r;

        public Form1()
        {
            InitializeComponent();
            band = true;
            x1 = x2 = y1 = y2 = r = 0 ;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
           
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (band)
            {
                y1 = e.Y;
                x1 = e.X;
                panel1.CreateGraphics().DrawEllipse(pen1, x1, y1, 1, 1);
                bmp.SetPixel(x1, y1, Color.Red);
                band = false;
            }
            else
            {
                y2 = e.Y;
                x2 = e.X;
                //panel1.CreateGraphics().DrawEllipse(pen1, x1, y1, 1, 1);
                //DrawElipseDDA(x1, y1, SacarRadio(x1, y1, x2, y2));
                DrawElipseBresenham(x1, y1, SacarRadio(x1, y1, x2, y2));
                pictureBox1.Image = bmp;
                band = true;
            }
        }

        private void DrawElipseDDA(int xc, int yc, double r)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double xi, yi;
            xi = yi = 0;
            for(xi = 0; xi<=yi; xi++)
            {
                yi = Math.Sqrt(Math.Abs(Math.Pow(r, 2) - Math.Pow(xi, 2)));
                drawOctante(xi, yi, xc, yc,Color.Blue);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            tiempodda.Text = String.Format("{0}", ts.TotalMilliseconds);

        }

        private void DrawElipseBresenham(int xc, int yc, double r)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double xi, yi;
            double pk;

            xi = 0;

            yi = r ;

            pk = (5 / 4) - r; ///Calculo primera iteracion de PK para iniciar

            drawOctante(xi, yi, xc, yc,Color.Black);

            while(yi>=xi)
            {
                if (pk < 0)
                {
                   pk += 2 * xi + 5;   ///Calcula pk en caso de que Y se mantenga en la misma posicion
                    xi++;
                }
                else
                {
                    pk += 2 * (xi - yi) + 5;   ///Calcula Pk en caso de que Y deba bajar de posicion
                    xi++;
                    yi--;
                }
                drawOctante(xi, yi, xc, yc, Color.Black);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            tiempobresenham.Text = String.Format("{0}", ts.TotalMilliseconds);
        }

        void drawOctante(double x, double y, int xc, int yc, Color col)
        {

            if (x + xc > 0 && x + xc < 600 && y + yc > 0 && y + yc < 450)  /// Cuadrante 2
                bmp.SetPixel((int)x + xc, (int)y + yc, col);
            if (-x + xc > 0 && -x + xc < 600 && -y + yc > 0 && -y + yc < 450)  ///CUADRANTE 6
                bmp.SetPixel((int)-x + xc, (int)-y + yc, col);
            if (-x + xc > 0 && -x + xc < 600 && y + yc > 0 && y + yc < 450)  ///Cuadrante 3
                bmp.SetPixel((int)-x + xc, (int)y + yc, col);
            if (x + xc > 0 && x + xc < 600 && -y + yc > 0 && -y + yc < 450)  ///Cuadrante 7
                bmp.SetPixel((int)x + xc, (int)-y + yc, col);
            if (y + xc > 0 && y + xc < 600 && x + yc > 0 && x + yc < 450)    ///Cuadrante 1
                bmp.SetPixel((int)y + xc, (int)x + yc,col);
            if (-y + xc > 0 && -y + xc < 600 && -x + yc > 0 && -x + yc < 450) ///
                bmp.SetPixel((int)-y + xc, (int)-x + yc, col);
            if (-y + xc > 0 && -y + xc < 800 && x + yc > 0 && x + yc < 450)  ///Cuadrante 4
                bmp.SetPixel((int)-y + xc, (int)x + yc, col);
            if (y + xc > 0 && y + xc < 600 && -x + yc > 0 && -x + yc < 450)  ///Cuadrante 8
                bmp.SetPixel((int)y + xc, (int)-x + yc, col);
          
            


            /*panel1.CreateGraphics().DrawEllipse(p1, (int)x + xc, (int)y + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)-x + xc, (int)-y + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)-x + xc, (int)y + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)x + xc, (int)-y + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)y + xc, (int)x + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)-y + xc, (int)-x + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)-y + xc, (int)x + yc, 1, 1);
            panel1.CreateGraphics().DrawEllipse(p1, (int)y + xc, (int)-x + yc, 1, 1);*/
          
        }

        private double SacarRadio(int x1, int y1, int x2, int y2)
        {
            double resultado;
            resultado = 0;
            int Dx2 = Math.Abs(x2 - x1);
            int Dy2 = Math.Abs(y2 - y1);
            resultado = (Math.Sqrt(Math.Pow(Dx2,2) + Math.Pow(Dy2,2)));
            return resultado;
        }
    }
}
