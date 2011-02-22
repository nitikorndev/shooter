using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace shooter
{
    class show
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public int step { get; set; }
        public SpriteEffects eff{ get; set; }
        public bool Display { get; set; }
        public bool move { get; set; }
        public Vector2 target { get; set; }
        public show(ContentManager content,String asset)
        {
            Texture = content.Load<Texture2D>(asset);
            Position = Vector2.Zero;
            Origin = new Vector2(Texture.Width/2,Texture.Height/2);
            Velocity = Vector2.Zero;
            Rotation = 0.0f;
            step = 0;
            eff = SpriteEffects.None;
            Display = false;
            move = false;
            target = new Vector2(0, 0);
        }
    }
}
