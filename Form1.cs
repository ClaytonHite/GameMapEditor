using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AdventuresOnlineMapEditor
{
    public partial class Form1 : Form
    {
        string[,] MainMap = new string[1000,1000];
        public string mapFilePath = "C:/Users/Clayt/Desktop/TestMap.txt";
        public string monsterMapFilePath = "C:/Users/Clayt/Desktop/MonsterMap.txt";
        public int Layer = 0;
        public bool RemoveLayer = false;
        public Vector2 Camera;
        public Vector2 MapEditorPos;
        Graphics g;
        PictureBox sprite;
        public bool cameraUpdated = true;
        double x;
        double y;
        double LocationX = 0;
        double LocationY = 0;
        public List<PictureBox> pictureBoxList = new List<PictureBox>();
        public static List<Sprite2D> RemoveList = new List<Sprite2D>();
        public static Vector2 imagePos;
        public Form1()
        {

            Camera = new Vector2(0,0);
            MapEditorPos = new Vector2(0, 0);
            InitializeComponent();
            MapEditorPanel.Paint += Renderer;
            g = MapEditorPanel.CreateGraphics();
            foreach (Control c in MapTilesSelectionPanel.Controls)
            {
                if (c.Name.Contains("pictureBox"))
                {
                    pictureBoxList.Add((PictureBox)c);
                }
            }
            MapEditorPanel.GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(MapEditorPanel, new object[]{ControlStyles.UserPaint |ControlStyles.AllPaintingInWmPaint |ControlStyles.DoubleBuffer, true});
            MapTilesSelectionPanel.GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(MapTilesSelectionPanel, new object[] { ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true });
        }

        public void pictureBox66_Click(object sender, EventArgs e)
        {
           sprite = (PictureBox)sender;
        }

        public void MapEditorPanel_MouseClick(object sender, MouseEventArgs e)
        {
            g.Clear(Color.Black);
            LocationX = (int)((e.X + MapEditorPos.X)/96);
            LocationY = (int)((e.Y + MapEditorPos.Y)/64);
            imagePos = new Vector2((float)LocationX, (float)LocationY);
            RemoveList.Clear();
            if (RemoveLayer)
            {
                if (Layer == 0)
                {
                    foreach (Sprite2D removeSprite in Sprite2D.GetSprites())
                    {
                        if (removeSprite.Position.X == (int)imagePos.X && removeSprite.Position.Y == (int)imagePos.Y && removeSprite.Tag == "Layer1")
                        {
                            RemoveList.Add(removeSprite);
                        }
                    }
                }
                if (Layer == 1)
                {
                    foreach (Sprite2D removeSprite2 in Sprite2D.GetSprites())
                    {
                        if (removeSprite2.Position.X == (int)imagePos.X && removeSprite2.Position.Y == (int)imagePos.Y && removeSprite2.Tag == "Layer2")
                        {
                            RemoveList.Add(removeSprite2);
                        }
                    }
                }
                foreach (Sprite2D removeSprite in RemoveList)
                {
                    removeSprite.DestroySelf();
                }
            }
            if (sprite != null)
            {
                if (!RemoveLayer)
                {
                    if (Layer == 0)
                    {
                        foreach (Sprite2D sprites in Sprite2D.GetSprites())
                        {
                            if (sprites.Position.X == (int)imagePos.X && sprites.Position.Y == (int)imagePos.Y && sprites.Tag == "Layer1")
                            {
                                RemoveList.Add(sprites);
                            }
                        }
                        foreach (Sprite2D removeSprite in RemoveList)
                        {
                            removeSprite.DestroySelf();
                        }
                        new Sprite2D(new Vector2(imagePos.X, imagePos.Y), new Vector2(96, 64), sprite.Image, "Layer1", sprite.Tag.ToString());
                    }
                    if (Layer == 1)
                    {
                        foreach (Sprite2D sprites in Sprite2D.GetSprites())
                        {
                            if (sprites.Position.X == (int)imagePos.X && sprites.Position.Y == (int)imagePos.Y && sprites.Tag == "Layer2")
                            {
                                RemoveList.Add(sprites);
                            }
                        }
                        foreach (Sprite2D removeSprite in RemoveList)
                        {
                            removeSprite.DestroySelf();
                        }
                        new Sprite2D(new Vector2(imagePos.X, imagePos.Y), new Vector2(96, 64), sprite.Image, "Layer2", sprite.Tag.ToString());
                    }
                }
            }
            Refresh();
        }
        public void Renderer(object sender, PaintEventArgs e)
        {
            g.Clear(Color.Black);
            if (Sprite2D.GetSprites() != null)
            {
                foreach (Sprite2D sprite in Sprite2D.GetSprites())
                {
                    g.DrawImage(sprite.Sprite, sprite.Position.X * 96, sprite.Position.Y * 64, sprite.Scale.X, sprite.Scale.Y);
                }
            }
        }
        private void buttonRIGHT_Click(object sender, EventArgs e)
        {
            if (cameraUpdated)
            {
                Camera.X = 0;
                Camera.Y = 0;
                if (sender == buttonUP)
                {
                    Camera.Y = Camera.Y - 64;
                    MapEditorPos.Y = MapEditorPos.Y - 64;                    
                    g.TranslateTransform((-1 * Camera.X), (-1 * Camera.Y));
                }
                if (sender == buttonRIGHT)
                {
                    Camera.X = Camera.X + 96;
                    MapEditorPos.X = MapEditorPos.X + 96;
                    g.TranslateTransform((-1 * Camera.X), (-1 * Camera.Y));
                }
                if (sender == buttonDOWN)
                {
                    Camera.Y = Camera.Y + 64;
                    MapEditorPos.Y = MapEditorPos.Y + 64;
                    g.TranslateTransform((-1 * Camera.X), (-1 * Camera.Y));
                }
                if (sender == buttonLEFT)
                {
                    Camera.X = Camera.X - 96;
                    MapEditorPos.X = MapEditorPos.X - 96;
                    g.TranslateTransform((-1 * Camera.X), (-1 * Camera.Y));
                }
            }
        }
        private void buttonSAVE_Click(object sender, EventArgs e)
        {
            List<string> saveList = new List<string>();
            for (int i = 0; i < 1000000; i++)
            {
                saveList.Add(".");
            }
            foreach (Sprite2D sprites in Sprite2D.GetSprites())
            {
                if (sprites.Tag == "Layer1")
                {
                    saveList[(int)((sprites.Position.Y * 1000) + sprites.Position.X)] = sprites.imageNumber;
                }
            }
            string saveText = string.Join(":", saveList);
            File.WriteAllText(mapFilePath, saveText);
            List<string> monsterSaveList = new List<string>();
            for (int i = 0; i < 1000000; i++)
            {
                monsterSaveList.Add(".");
            }
            foreach (Sprite2D sprites in Sprite2D.GetSprites())
            {
                if (sprites.Tag == "Layer2")
                {
                    monsterSaveList[(int)((sprites.Position.Y * 1000) + sprites.Position.X)] = sprites.imageNumber;
                }
            }
            string saveMonsterText = string.Join(":", monsterSaveList);
            File.WriteAllText(monsterMapFilePath, saveMonsterText);
            //File.WriteAllLines(mapFilePath, saveList);
        }

        private void buttonLOAD_Click(object sender, EventArgs e)
        {
            string ReadFile = File.ReadAllText(mapFilePath);
            string[] LoadFile = ReadFile.Split(':');
            int mapSize = 1000; //1000 by 1000 tiles
            for (int i = 0; i < LoadFile.Length; i++)
            {
                int posY = i / mapSize;
                int posX = i % mapSize;
                MainMap[posY, posX] = LoadFile[i];
                if (LoadFile[i] != ".")
                {
                    Vector2 imagePos = new Vector2(posX, posY);
                    new Sprite2D(new Vector2(posX, posY), new Vector2(96, 64), (Image)Properties.Resources.ResourceManager.GetObject("_" + LoadFile[i]), "Layer1", $"{LoadFile[i]}");
                    //ImageDictionary.Add(imagePos, new ImageListMap((Image)Properties.Resources.ResourceManager.GetObject("_"+ LoadFile[i]), new Vector2(96, 64), LoadFile[i]));
                }
            }
            string ReadMonsterFile = File.ReadAllText(monsterMapFilePath);
            string[] LoadMonsterFile = ReadMonsterFile.Split(':');
            for (int i = 0; i < LoadMonsterFile.Length; i++)
            {
                int posY = i / mapSize;
                int posX = i % mapSize;
                MainMap[posY, posX] = LoadMonsterFile[i];
                if (LoadMonsterFile[i] != ".")
                {
                    Vector2 imagePos = new Vector2(posX, posY);
                    new Sprite2D(new Vector2(posX, posY), new Vector2(96, 64), (Image)Properties.Resources.ResourceManager.GetObject("_" + LoadMonsterFile[i]), "Layer2", $"{LoadMonsterFile[i]}");
                    //ImageDictionary2.Add(imagePos, new ImageListMap((Image)Properties.Resources.ResourceManager.GetObject("_" + LoadMonsterFile[i]), new Vector2(96, 64), LoadMonsterFile[i]));
                }
            }
            Refresh();
        }

        private void layerButton1_MouseClick(object sender, MouseEventArgs e)
        {
            Layer = 0;
        }

        private void layerButton2_Click(object sender, EventArgs e)
        {
            Layer = 1;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                for (int i = 0; i < pictureBoxList.Count; i++)
                {
                    Layer = 0;
                    string imageName = $"_{i}";
                    pictureBoxList[i].Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
                    pictureBoxList[i].Tag = i;
                    MapEditorPanel.Focus();
                }
            }
            if(comboBox1.SelectedIndex == 3)
            {
                Layer = 1;
                for (int i = 0; i < pictureBoxList.Count; i++)
                {
                    int num = i + 500;
                    string imageName = $"_{num}";
                    pictureBoxList[i].Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
                    pictureBoxList[i].Tag = num;
                    MapEditorPanel.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(RemoveLayer)
            {
                RemoveLayer = false;
            }
            else if(!RemoveLayer)
            {
                RemoveLayer = true;
            }
        }
    }
}
