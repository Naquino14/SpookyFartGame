using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpookyFartGame.entities
{
    public interface IEntity<T>
    {
        public void Draw(ref SpriteBatch spriteBatch);
        public void TakeDamage(int damage);
        public void Kill(ref List<IEntity<Entity>> list);
        public void Kill();
        public void UpdatePosition(GameTime time);

        public bool CollidesWith(Entity entity);
        public Entity GetSelf();
    }
}
