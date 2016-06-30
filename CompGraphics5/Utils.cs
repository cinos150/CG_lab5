using System;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Microsoft.Xna.Framework;
using Point = System.Windows.Point;

namespace CompGraphics5
{
    internal class Utils
    {

        public static Matrix TranslationMatrix(Point3D T)
        {
            Vector4 row1 = new Vector4(1, 0, 0, 0);
            Vector4 row2 = new Vector4(0, 1, 0, 0);
            Vector4 row3 = new Vector4(0, 0, 1, 0);
            Vector4 row4 = new Vector4((float)T.X, (float)T.Y, (float)T.Z, 1);

            return new Matrix(row1, row2, row3, row4);
        }



        public static Matrix ScalingMatrix(Point3D S)
        {
            Vector4 row1 = new Vector4((float) S.X, 0, 0, 0);
            Vector4 row2 = new Vector4(0, (float) S.Y, 0, 0);
            Vector4 row3 = new Vector4(0, 0, (float) S.Z, 0);
            Vector4 row4 = new Vector4(0, 0, 0, 1);

            return new Matrix(row1, row2, row3, row4);
        }

        public static Matrix ViewMatrixFun(Matrix modelMatrix)
        {
            return Matrix.Invert(modelMatrix); //z kamery
        }


        public static Matrix ModelMatrixFun(RotationMatrix rotationMatrix, Matrix transMatrix,Matrix scaleMatrix)
        {
            var modelMatrix = Matrix.Multiply(transMatrix, scaleMatrix);
            modelMatrix = Matrix.Multiply(modelMatrix, rotationMatrix.X);
            modelMatrix = Matrix.Multiply(modelMatrix, rotationMatrix.Y);
            modelMatrix = Matrix.Multiply(modelMatrix, rotationMatrix.Z);
            return modelMatrix;
        }



        public static Matrix SetProjectionMatrix( float angleOfView,  float near,  float far)
        {
            var m = Matrix.Identity;

            float scale = (float) (1/Math.Tan(angleOfView*0.5*Math.PI/180));
            m.M11 = scale; 
            m.M22 = scale; 
            m.M33 = -far/(far - near); 
            m.M43 = -far*near/(far - near); 
            m.M34 = -1; 
            m.M44 = 0;

            return m;
        }

        //public static Vector4 multPointMatrix(Vector4 input, Matrix M)
        //{
        //    Vector4 outPar = new Vector4
        //    {
        //        X = input.X * M.M11 + input.Y * M.M21 + input.Z * M.M31 + /* in.z = 1 */ M.M41,
        //        Y = input.X * M.M12 + input.Y * M.M22 + input.Z * M.M32 + /* in.z = 1 */ M.M42,
        //        Z = input.X * M.M13 + input.Y * M.M23 + input.Z * M.M33 + /* in.z = 1 */ M.M43,
        //        W = input.X * M.M14 + input.Y * M.M24 + input.Z * M.M34 + /* in.z = 1 */ M.M44
        //    };

          

        //    // normalize if w is different than 1 (convert from homogeneous to Cartesian coordinates)
        //    if (outPar.W != 1)
        //    {
        //        outPar.X /= outPar.W;
        //        outPar.Y /= outPar.W;
        //        outPar.Z /= outPar.W;
        //        outPar.W /= outPar.W;
        //    }

        //    return outPar;

        //}

        public static Matrix CreateWorldToCamMatrix(Point3D T)
        {
            return Matrix.Invert(TranslationMatrix(T));
        }

        public static Vector4 Normalize(Vector4 point)
        {
            int x;
            var result = point;


            if (result.W.Equals(0.0f))
                x = 5;

                if (!result.W.Equals(1.0f))
            {
                result.X /= result.W;
                result.Y /= result.W;
                result.Z /= result.W;
                result.W /= result.W;

            }

            return result;
        }
    

        public static System.Drawing.Point Point3Dto2D(Matrix objectModelMatrix,Matrix cameraViewMatrix, Vector4 point,Canvas myCanvas, Matrix cameraProjMatrix)
        {
            var imageWidth = (int) myCanvas.Width;
            var imageHeight = (int) myCanvas.Height;

            var initial = Vector4.Transform(point, objectModelMatrix);
            var a = Vector4.Transform(initial, cameraViewMatrix);
            var b = Vector4.Transform(a, cameraProjMatrix);
            var c = Normalize(b);

            var x = (int) Math.Min(imageWidth - 1, (c.X + 1)*0.5*imageWidth);
            var y = (int) Math.Min(imageHeight - 1, ((c.Y + 1)*0.5)*imageHeight);

            return new System.Drawing.Point(x, y);
        }


        //public static System.Drawing.Point To3D(Canvas myCanvas,Vector4 input)
        //{
        //    var imageWidth = (int)myCanvas.Width;
        //    var imageHeight = (int)myCanvas.Height;

        //    Matrix worldToCamera = Matrix.Identity;
            
        //    worldToCamera.M42 = -10;
        //    worldToCamera.M43 = -20;

        //    float angleOfView = 90;
        //    float near = 0.1f;
        //    float far = 100;

        //   Matrix Mproj = SetProjectionMatrix(angleOfView, near, far);
        //    Vector4 step1 = multPointMatrix(input, worldToCamera);
        //    Vector4 step2 = multPointMatrix(step1, Mproj);

        //    if (step2.X < -1 || step2.X > 1 || step2.Y < -1 || step2.Y > 1) return new System.Drawing.Point(-1, -1); ;

        //    var x = (int)Math.Min(imageWidth - 1, (step2.X + 1) * 0.5 * imageWidth);
        //    var y = (int)Math.Min(imageHeight - 1, (1 - (step2.Y + 1) * 0.5) * imageHeight);


        //    return new System.Drawing.Point(x,y);
        //}

    }


}
