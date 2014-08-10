﻿using C3DE.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C3DE.UI
{
    public class GUISkin
    {
        public Texture2D Box { get; set; }
        public Texture2D Border { get; set; }
        public Texture2D[] Buttons { get; set; }
        public Texture2D[] Checkbox { get; set; }
        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; }
        public float Margin { get; set; }
        private string _fontName;

        public GUISkin(string fontName = "")
        {
            _fontName = fontName;
        }

        public void LoadContent(ContentManager content)
        {
            Box = GraphicsHelper.CreateTexture(new Color(0.08f, 0.12f, 0.16f, 0.7f), 1, 1);

            Border = GraphicsHelper.CreateBorderTexture(Color.White, new Color(0.08f, 0.12f, 0.16f, 0.7f), 128, 128, 1);

            Buttons = new Texture2D[3] 
            {
                GraphicsHelper.CreateBorderTexture(Color.White, new Color(0.08f, 0.12f, 0.16f, 0.7f), 128, 48, 1), // Normal
                GraphicsHelper.CreateBorderTexture(Color.WhiteSmoke, new Color(0.16f, 0.19f, 0.23f, 0.7f), 128, 48, 1),  // Hover
                GraphicsHelper.CreateBorderTexture(Color.LightGray, new Color(0.19f, 0.23f, 0.27f, 0.7f), 128, 48, 1)   // Clicked
            };

            Checkbox = new Texture2D[3] 
            {
                GraphicsHelper.CreateBorderTexture(Color.White, new Color(0.08f, 0.12f, 0.16f, 0.7f), 48, 48, 2),         // Normal
                GraphicsHelper.CreateTexture(Color.DarkGray, 1, 1),                            // Hover
                GraphicsHelper.CreateTexture(Color.White, 1, 1)                             // Clicked
            };

            TextColor = Color.White;

            Margin = 5.0f;

            if (!string.IsNullOrEmpty(_fontName))
                Font = content.Load<SpriteFont>(_fontName);
        }
    }
}
