using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpookyFartGame.entities
{
    public abstract class Entity : IEntity
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float LayerDepth { get; private set; }
        public float Scale { get; set; }
        public float Speed { get; private set; }
        
        

        public void Draw(ref SpriteBatch spriteBatch)
            => spriteBatch.Draw(
                Texture, 
                Position, 
                null, 
                Color.White, 
                Rotation, 
                new Vector2(Texture.Width / 2, Texture.Height / 2), 
                Scale, 
                SpriteEffects.None, 
                LayerDepth
            );

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
