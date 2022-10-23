using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SpookyFartGame.entities
{
    public abstract class Entity : IEntity<Entity>
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float LayerDepth { get; private set; }
        public float Scale { get; set; }
        public float Speed { get; private set; }
        public Rectangle hitbox { get; private set; }
        public bool IsDead { get; private set; } = false;

        public int Health { get; private set; }

        public Entity(Texture2D texture, Vector2 position, float rotation, float layerDepth, float scale, float speed, int initialHealth = 100)
        {
            Texture = texture;
            Position = position;
            Rotation = rotation;
            LayerDepth = layerDepth;
            Scale = scale;
            Speed = speed;
            Health = initialHealth;
            hitbox = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

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

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Kill();
        }

        public abstract void UpdatePosition(GameTime time);

        public bool CollidesWith(Entity entity)
            => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height)
            .Intersects(new Rectangle(
                (int)entity.Position.X - (int)(entity.Texture.Width * entity.Scale / 2),
                (int)entity.Position.Y - (int)(entity.Texture.Height * entity.Scale / 2),
                (int)(entity.Texture.Width * entity.Scale), 
                (int)(entity.Texture.Height * entity.Scale))
            );

        public abstract Entity GetSelf();

        public void Kill(ref List<IEntity<Entity>> list)
        {
            list.Remove(this);
            Kill();
        }

        public void Kill() => IsDead = true;

    }
}
