using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpookyFartGame.entities;
using SpookyFartGame.player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SpookyFartGame
{
    public class SpookyFartGame : Game
    {
        #region required variables

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #endregion

        #region static members

        public static Direction Direction { get; private set; }
        public static Random random;

        #endregion

        #region instance members

        #region player

        Texture2D spookySprite;
        Vector2 playerPos;
        float playerSpeed;
        int score = 0;
        float lastFireTime;
        float fireRate;

        #endregion

        #region sfx

        Song yoquierotacobell;

        #endregion

        #region pewpews

        Texture2D pewpew1;
        public List<IEntity<Entity>> pewpews;

        #endregion

        #region enemies

        Texture2D ghost1;
        Entity currentGhost;
        //public List<IEntity<Entity>> enemies;

        #endregion

        #endregion

        public SpookyFartGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // set window w and h
            _graphics.PreferredBackBufferHeight = 1920;
            _graphics.PreferredBackBufferWidth = 823;
            _graphics.ApplyChanges();
            _graphics.ToggleFullScreen();

            pewpews = new();
            random = new();

            playerPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            playerSpeed = 600f;
            fireRate = .08f;

            base.Initialize();
            MediaPlayer.Play(yoquierotacobell);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            spookySprite = Content.Load<Texture2D>("assets/yoquerotacobell");
            pewpew1 = Content.Load<Texture2D>("assets/pewpew1");
            yoquierotacobell = Content.Load<Song>("assets/yoquerotacobellsfx");
            ghost1 = Content.Load<Texture2D>("assets/ghost1");

            currentGhost = new Ghost(ghost1, new(400, 400), 0, 0, .3f, 10, 100);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Direction = Inputs.GetDirectionState();
            playerPos.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * Direction switch { 
                Direction.up or Direction.upLeft or Direction.upRight => -1,
                Direction.down or Direction.downLeft or Direction.downRight => 1,
                _ => 0
            };

            playerPos.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * Direction switch {
                Direction.right or Direction.upRight or Direction.downRight => 1,
                Direction.left or Direction.upLeft or Direction.downLeft => -1,
                _ => 0
            };

            bool isFiring = Inputs.isFiring();
            
            if (isFiring && gameTime.TotalGameTime.TotalSeconds - lastFireTime > fireRate)
            {
                pewpews.Add(new PewPew(pewpew1,
                    new(playerPos.X, playerPos.Y - spookySprite.Height / 8),
                    rotation: (float)(Math.PI / 180) * (float)random.NextDouble() * 10 * (random.NextDouble() > .5f ? 1 : -1), // random angle between -3 and 3 degrees
                    0f,
                    scale: (float)Math.Max(.75, (float)(random.NextDouble() * 2) % 2),
                    speed: 400,
                    initialHealth: 1,
                    damage: 50
                ));
                lastFireTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            for (int i = 0; i < pewpews.Count; i++)
            {
                IEntity<Entity> pewpew;
                try {
                    pewpew = pewpews[i];
                } catch
                { continue; }
                pewpew.UpdatePosition(gameTime);
                if (currentGhost is not null && pewpew.CollidesWith(currentGhost))
                {
                           currentGhost.TakeDamage((pewpew.GetSelf() as PewPew).Damage);
                    pewpew.Kill(ref pewpews);
                }
                if (pewpew.GetSelf().Position.X > _graphics.PreferredBackBufferWidth
                    || pewpew.GetSelf().Position.X < 0
                    || pewpew.GetSelf().Position.Y > _graphics.PreferredBackBufferHeight
                    || pewpew.GetSelf().Position.Y < 0)
                    pewpew.Kill(ref pewpews);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(
                spookySprite, 
                playerPos, 
                null, 
                Color.White, 
                0f, 
                new(spookySprite.Width / 2, spookySprite.Height / 2), 
                new Vector2(.25f, .25f), 
                SpriteEffects.None, 
                0f
            );

            foreach (IEntity<Entity> pewpew in pewpews)
                pewpew.Draw(ref _spriteBatch);

            //foreach (var ghost in enemies)
            //    ghost.Draw(ref _spriteBatch);
            if (currentGhost is not null && !currentGhost.IsDead)
                currentGhost.Draw(ref _spriteBatch);
            else
                currentGhost = null;

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}