using System;
using Microsoft.Xna.Framework;

namespace CompGraphics5
{
    class RotationMatrix
    {
        public Matrix X { get; set; }
        public Matrix Y { get; set; }
        public Matrix Z { get; set; }

        public RotationMatrix(double alpha)
        {
            X = rotationMatrixX(alpha);
            Y = rotationMatrixY(alpha);
            Z = rotationMatrixZ(alpha);
        }

        private Matrix rotationMatrixX(double alpha)
        {
           var row1 = new Vector4(1, 0, 0, 0);
           var row2 = new Vector4(0, (float)Math.Cos(alpha *(Math.PI/180)), (float)(-1 * Math.Sin(alpha * (Math.PI / 180))), 0);
           var row3 = new Vector4(0, (float)(Math.Sin(alpha * (Math.PI / 180))), (float)Math.Cos(alpha * (Math.PI / 180)), 0);
           var row4 = new Vector4(0, 0, 0, 1);

            return new Matrix(row1, row2, row3, row4);
        }

        private Matrix rotationMatrixY(double alpha)
        {
            var row1 = new Vector4((float)Math.Cos(alpha * (Math.PI / 180)), 0, (float)(Math.Sin(alpha * (Math.PI / 180))), 0);
            var row2 = new Vector4(0, 1, 0, 0);
            var row3 = new Vector4((float)(-1 * Math.Sin(alpha * (Math.PI / 180))), 0, (float)Math.Cos(alpha * (Math.PI / 180)), 0);
            var row4 = new Vector4(0, 0, 0, 1);

            return new Matrix(row1, row2, row3, row4);
        }

        private Matrix rotationMatrixZ(double alpha)
        {
            var row1 = new Vector4((float)Math.Cos(alpha * (Math.PI / 180)), (float)(-1 * Math.Sin(alpha * (Math.PI / 180))), 0, 0);
            var row2 = new Vector4((float)(Math.Sin(alpha * (Math.PI / 180))), (float)Math.Cos(alpha * (Math.PI / 180)), 0, 0);
            var row3 = new Vector4(0, 0, 1, 0);
            var row4 = new Vector4(0, 0, 0, 1);

            return new Matrix(row1, row2, row3, row4);
        }

    }
}
