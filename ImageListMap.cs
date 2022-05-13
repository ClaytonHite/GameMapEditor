using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresOnlineMapEditor
{
    public class ImageListMap
    {
        public Image Tile;
        public Vector2 Scale;
        public string Tag;
        public string Position;
        public ImageListMap(Image image, Vector2 scale, string tag)
        {
            this.Tile = image;
            this.Scale = scale;
            this.Tag = tag;
        }
    }
}
