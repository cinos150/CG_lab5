using System.Windows.Media.Media3D;
using Microsoft.Xna.Framework;

namespace CompGraphics5
{
    class Cillinder
    {
        
        public Matrix ModelObject { get; set; }
        public Matrix ProjMatrix { get; set; }
        

        public Cillinder( int angle)
        {
            RotationMatrix rotationMatrix = new RotationMatrix(angle);
            Matrix scaleMatrix = Utils.ScalingMatrix(new Point3D(1, 1, 1));


            Matrix transMatrixObject = Utils.TranslationMatrix(new Point3D(1,1,1));
            ModelObject = Utils.ModelMatrixFun(rotationMatrix, transMatrixObject, scaleMatrix);
            ProjMatrix = projectionMatrix();
        }

        private Matrix projectionMatrix()
        {
            float FOV = 90;
            float Near = 0.1f;
            float far = 1000;
  
            return Utils.SetProjectionMatrix(FOV,Near,far);
        }
        
        


    }
}
