using System.Windows.Media.Media3D;
using Microsoft.Xna.Framework;

namespace CompGraphics5
{
    internal class Camera
    {
        public Matrix viewCamera { get; set; }

        public Camera(int leftRight, int topBottom, int farNear)
        {
            viewCamera = Utils.CreateWorldToCamMatrix(new Point3D(leftRight,topBottom,farNear));
        }

    }
}
