using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresOnlineMapEditor
{
    public class Camera
    {
        public Vector2 cameraPosition;
        public float X;
        public float Y;
        public Camera(float moveAmount, string moveDirection)
        {
            if (moveDirection == "up")
            {
                cameraPosition = new Vector2(cameraPosition.X, cameraPosition.Y + moveAmount);
            }
            if (moveDirection == "right")
            {
                cameraPosition = new Vector2(cameraPosition.X - moveAmount, cameraPosition.Y);
            }
            if (moveDirection == "down")
            {
                cameraPosition = new Vector2(cameraPosition.X, cameraPosition.Y - moveAmount);
            }
            if (moveDirection == "left")
            {
                cameraPosition = new Vector2(cameraPosition.X + moveAmount, cameraPosition.Y);
            }
            this.X = cameraPosition.X;
            this.Y = cameraPosition.Y;
        }
        public Camera(float x, float y)
        {
            cameraPosition.X = x;
            cameraPosition.Y = y;
        }
    }
}
