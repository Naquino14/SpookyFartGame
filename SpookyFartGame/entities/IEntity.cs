using Microsoft.Xna.Framework.Graphics;

namespace SpookyFartGame.entities
{
    public interface IEntity
    {
        public void Draw(ref SpriteBatch spriteBatch);
        public void TakeDamage(int damage);
        public void Kill();
    }
}
