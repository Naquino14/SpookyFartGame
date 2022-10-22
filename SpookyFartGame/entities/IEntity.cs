using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpookyFartGame.entities
{
    public interface IEntity<T>
    {
        public void Draw(ref SpriteBatch spriteBatch);
        public void TakeDamage(int damage);
        public void Kill(ref List<T> list);
    }
}
