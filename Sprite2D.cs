using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresOnlineMapEditor
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public Image Image;
        public string Tag = "";
        public string imageNumber = "";
        public Bitmap Sprite = null;
        public static List<Sprite2D> Sprites = new List<Sprite2D>();
        public Sprite2D(Vector2 Position, Vector2 Scale, Image CharImage, string Tag, string ImageNumber)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Image = CharImage;
            this.Tag = Tag;
            this.imageNumber = ImageNumber;

            Image tmp = CharImage;
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;

            RegisterSprite(this);
        }
        public static void onLoad()
        {
            Sprites = new List<Sprite2D>();
        }
        public void DestroySelf()
        {
            UnRegisterSprite(this);
        }
        public static void RegisterSprite(Sprite2D sprite)
        {
            Sprites.Add(sprite);
        }
        public static void UnRegisterSprite(Sprite2D sprite)
        {
            Sprites.Remove(sprite);
        }
        public static List<Sprite2D> GetSprites()
        {
            return Sprites;
        }
    }
}

