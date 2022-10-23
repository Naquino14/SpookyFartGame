using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpookyFartGame.entities
{
    public class PewPew : Entity
    {
        public int Damage { get; private set; }
        public PewPew(Texture2D texture, Vector2 position, float rotation, float layerDepth, float scale, float speed, int initialHealth = 100,
            int damage = 0) 
            : base(texture, position, rotation, layerDepth, scale, speed, initialHealth)
        {
            Damage = damage;
        }

        public override void UpdatePosition(GameTime time)
        {
            // move the projectile forward
            Position += new Vector2(
                Speed * (float)Math.Sin(Rotation) * (float)time.ElapsedGameTime.TotalSeconds, 
                -1 * Speed * (float)Math.Cos(Rotation) * (float)time.ElapsedGameTime.TotalSeconds
            );
        }

        public override Entity GetSelf() => this;
    }
}
