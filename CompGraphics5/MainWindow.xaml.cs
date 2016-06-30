using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Xna.Framework;
using Point = System.Drawing.Point;


namespace CompGraphics5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static int leftRight =0;
        private static int topBottom =0 ;
        private static int farNear =-50 ;
        private static int angle =100 ;
         Camera _cam = new Camera(leftRight, topBottom, farNear);
         Cillinder _cill = new Cillinder(angle);


     

        public MainWindow()
        {
            InitializeComponent();
   
            generateCylinderMesh();

        }



        private void generateCylinderMesh()
        {
            float h = 20.0f;
            float r = 20.0f;
            Vector4 p0 =new Vector4(0,h,0,1);
            Vector4 n0 =new Vector4(0,1,0,0);
            int n = 30;

            List<Triangle> allTriangles = new List<Triangle>();
            //TOP BASE 
            //Triangles 



            Vertex [] allVertexList = new Vertex[(int) (4*n+2)];
            allVertexList[0] = new Vertex(p0,n0);


            for (int i = 1; i <= n; i++)
            {



                Vector4 pi = new Vector4
                {
                    X = (float) (r*Math.Cos((2*Math.PI/n)*(i - 1))),
                    Y = h,
                    Z = (float) (r*Math.Sin((2*Math.PI/n)*(i - 1))),
                    W = 1
                };

                allVertexList[i] = (new Vertex(pi, n0));

            }

            int position = 0;
            for (int i = 0; i < n; i++)
            {
                position = ((i + 2) % (n + 1) == 0) ? 1 : (int)((i + 2) % (n + 1));


                allTriangles.Add(new Triangle(allVertexList[0], allVertexList[position],
                    allVertexList[i + 1]));
            }

            //BOTTOM BASE

            allVertexList[4 * n + 1] = (new Vertex(new Vector4(0, 0, 0, 1), new Vector4(0, -1, 0, 0)));
            for (int i = 3 * n + 1; i < 4 * n + 1; ++i)
            {
                allVertexList[i] = new Vertex(new Vector4(r * (float)Math.Cos(2 * Math.PI / n * (i - 1)), 0, r * (float)Math.Sin(2 * Math.PI / n * (i - 1)), 1), new Vector4(0, 1, 0, 0));
            }
            for (int i = 3 * n; i <= 4 * n - 1; ++i)
            {
                int index = i + 2;
                if (index == 4 * n + 1)
                    index = 3 * n + 1;
                allTriangles.Add(new Triangle(allVertexList[4 * n + 1], allVertexList[i + 1], allVertexList[index]));
            }


            for (int i = n + 1; i <= 3 * n; ++i)
            {
                if (i >= n + 1 && i <= 2 * n)
                {
                    allVertexList[i] = allVertexList[i - n];
                }
                if (i >= 2 * n + 1 && i <= 3 * n)
                {
                    allVertexList[i] = allVertexList[i + n];
                }
            }
            for (int i = n; i <= 3 * n - 1; ++i)
            {
                if (i >= n && i <= 2 * n - 2)
                {
                    allTriangles.Add(new Triangle(allVertexList[i + 1], allVertexList[i + 2], allVertexList[i + 1 + n]));
                }
                if (i == 2 * n - 1)
                {
                    allTriangles.Add(new Triangle(allVertexList[2 * n], allVertexList[n + 1], allVertexList[3 * n]));
                }
                if (i >= 2 * n && i <= 3 * n - 2)
                {
                    allTriangles.Add(new Triangle(allVertexList[i + 1], allVertexList[i + 2 - n], allVertexList[i + 2]));
                }
                if (i == 3 * n - 1)
                {
                    allTriangles.Add(new Triangle(allVertexList[3 * n], allVertexList[n + 1], allVertexList[2 * n + 1]));
                }
            }




            foreach (var triangle in allTriangles)
            {

                DrawTriangle(triangle);
            }

        }

        private System.Drawing.Point PointTransforms(Vector4 initialPoint)
        {
            return Utils.Point3Dto2D(_cill.ModelObject, _cam.viewCamera, initialPoint, MyCanvas, _cill.ProjMatrix);
           // return Utils.To3D(MyCanvas, initialPoint);
        }

        void DrawTriangle(Triangle triangle)
        {
            Point A = PointTransforms(triangle.a.p);
            Point B = PointTransforms(triangle.b.p);
            Point C = PointTransforms(triangle.c.p);

            DrawTriangle(A,B,C);
        }


        private bool shouldDisplay(System.Drawing.Point a, System.Drawing.Point b, System.Drawing.Point c)
        {
            Vector3 v1 =  new Vector3(b.X - a.X, b.Y-a.Y, 0);
            Vector3 v2 =  new Vector3(c.X - a.X, c.Y-a.Y, 0);
            Vector3 cross = Vector3.Cross(v1, v2);
            if (cross.Z > 0)
                return true;
            return false;
        }

        void DrawTriangle(System.Drawing.Point a, System.Drawing.Point b, System.Drawing.Point c)
        {
            if (shouldDisplay(a, b, c))
                return;

            var line1 = new Line
            {
                X1 = a.X,
                X2 = b.X,
                Y1 = a.Y,
                Y2 = b.Y,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 5

            };

            var line2 = new Line
            {
                X1 = b.X,
                X2 = c.X,
                Y1 = b.Y,
                Y2 = c.Y,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 5
            };


            var line3 = new Line
            {
                X1 = c.X,
                X2 = a.X,
                Y1 = c.Y,
                Y2 = a.Y,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 5
            };

            MyCanvas.Children.Add(line1);
            MyCanvas.Children.Add(line2);
            MyCanvas.Children.Add(line3);
        }

      

        private void leftButton(object sender, RoutedEventArgs e)
        {
            _cam = new Camera(leftRight-=10, topBottom, farNear);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            _cam = new Camera(leftRight += 10, topBottom, farNear);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonTop_OnClick(object sender, RoutedEventArgs e)
        {
            _cam = new Camera(leftRight, topBottom-=10, farNear);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonBottom_OnClick(object sender, RoutedEventArgs e)
        {
            _cam = new Camera(leftRight, topBottom +=10, farNear);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonRotateLeft_OnClick(object sender, RoutedEventArgs e)
        {
            --angle ;
            _cill = new Cillinder( angle);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonRotateRight_OnClick(object sender, RoutedEventArgs e)
        {
            ++angle;
            _cill = new Cillinder( angle);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonFar_OnClick(object sender, RoutedEventArgs e)
        {
            _cam = new Camera(leftRight, topBottom, farNear-=10);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }

        private void ButtonNear_OnClick(object sender, RoutedEventArgs e)
        {
            _cam = new Camera(leftRight, topBottom, farNear += 10);
            MyCanvas.Children.Clear();
            generateCylinderMesh();
        }
    }


    public class Triangle
    {
       public Vertex b { get; }
       public Vertex c { get; }
       public  Vertex a { get; }

        public Triangle(Vertex _a, Vertex _b, Vertex _c)
        {
            a = _a;
            b = _b;
            c = _c;
        }


    }

    public class Vertex
    {
       public Vector4 p { get; }
       
       public Vector4 n { get; }

        public Vertex(Vector4 _p, Vector4 _n)
        {
            p = _p;
            n = _n;
          
        }
    }



}


