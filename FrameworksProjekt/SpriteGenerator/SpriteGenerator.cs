using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WindowsFormsApplication2
{
    static class SpriteGenerator
    {
        // folder names
        private static string mainPath;
        private static string spriteName;
        private static string mainDirectory = "IMG/";
        private static string destinationFolder = mainDirectory + "Pirates/";
        // main body template - might add a random option for this too
        private static string bodyTemplate = "BodyTemplate.png";

        // All sprites to draw in order of z-index. 
        private static List<Image> sprites;
        private static SpriteTemplate[] spriteParts;
        private static Image sprite;
        private static Image spritesheet;
        private static Random r = new Random();
        private static List<string> filesNames;
        private static string[] spritePartNames;

        private static List<SpriteFrame> frames;

        public static string GenerateSprite(List<SpriteFrame> Frames)
        {
            frames = Frames;
            sprites = new List<Image>();
            mainPath = Path.GetDirectoryName(mainDirectory);

            CreateSpriteTemplate();

            for (int i = 0; i < frames.Count; i++)
            {
                Init(frames[i]);

                if (i == 0)
                ChooseRandomSprites(frames[i]);

                GetChosenSprites(frames[i]);

                CombineSpriteParts();

            }

            CreateSpriteSheet();

            return spriteName;
        }

        /// <summary>
        /// Create image from sprite template 
        /// </summary>
        private static void CreateSpriteTemplate()
        {
            float x = 0;
            float y = 0;
            int height = 0;
            int width = 0;

            for(int i = 0; i < frames.Count; i++)
            {
                sprite = Image.FromFile(mainDirectory + frames[i].FolderName + "/" + bodyTemplate);

                // find outermost positions on spritesheet
                if (frames[i].Position.X > x || (frames[i].Position.X + sprite.Width > x+ width))
                {
                    x = frames[i].Position.X;
                    width = sprite.Width;
                }
                if(frames[i].Position.Y > y || (frames[i].Position.Y + sprite.Height > y + height))
                {
                    y = frames[i].Position.Y;
                    height = sprite.Height;                  
                }
            }

            spritesheet = new Bitmap((int)x + width, (int)y + height, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Set initial values
        /// </summary>
        private static void Init(SpriteFrame frame)
        {
            filesNames = new List<string>();
            spriteParts = new SpriteTemplate[frame.Components.Length];
            spriteName = Path.GetRandomFileName() + ".png";
        }

        /// <summary>
        /// Go through the folders defined in components and choose a random image. Add said image to sprites
        /// </summary>
        private static void GetChosenSprites(SpriteFrame frame)
        {
            sprite = Image.FromFile(mainPath + "/" + frame.FolderName + "/" + bodyTemplate);

            for (int i = 0; i < frame.Components.Length; i++)
            {
                string folderPath = mainPath + "/" + frame.FolderName + "/" + frame.Components[i];

                if(File.Exists(folderPath + spritePartNames[i]))
                {
                    spriteParts[i] = new SpriteTemplate(folderPath + spritePartNames[i], frame.Offsets[i]);
                }
            }
        }

        private static void ChooseRandomSprites(SpriteFrame frame)
        {
            spritePartNames = new string[frame.Components.Length];

            for (int i = 0; i < frame.Components.Length; i++)
            {
                string folderPath = mainPath + "/" + frame.FolderName + "/" + frame.Components[i];

                if(Directory.Exists(folderPath))
                { 
                    filesNames = Directory.GetFiles(folderPath).ToList();
                    int t = r.Next(0, filesNames.Count);
                    spritePartNames[i] = filesNames[t].Substring(filesNames[t].IndexOf("\\"));
                }
            }
        }

        /// <summary>
        /// Combine chosen sprites to final sprite
        /// </summary>
        private static void CombineSpriteParts()
        {
            // Draw and store sprite
            using (var canvas = Graphics.FromImage(sprite))
            {
                for (int i = 0; i < spriteParts.Count(); i++)
                {
                    if(spriteParts[i] != null)
                    canvas.DrawImage(spriteParts[i].Sprite, spriteParts[i].Offset);
                }

                sprites.Add(sprite);
            }
        }

        /// <summary>
        /// Create spritesheet
        /// </summary>
        private static void CreateSpriteSheet()
        {
            string fileName = destinationFolder + spriteName;

            using (var canvas = Graphics.FromImage(spritesheet))
            {
                for(int i = 0; i < frames.Count; i++)
                {
                    SpriteFrame f = frames[i];

                    if(f.Reverse)
                    {
                        sprites[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                        canvas.DrawImage(sprites[i], f.Position);
                    }
                    else
                    {
                        canvas.DrawImage(sprites[i], f.Position);
                    }
                }

                spritesheet.Save(fileName);
            }
        }
    }
}
