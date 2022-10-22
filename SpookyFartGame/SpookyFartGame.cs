using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpookyFartGame.entities;
using SpookyFartGame.player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

        #endregion

        #region instance members

        #region player

        Texture2D spookySprite;
        Vector2 playerPos;
        float playerSpeed;
        int score = 0;

        #endregion

        #region pewpews

        Texture2D pewpew1;
        public List<IEntity<Entity>> pewpews;

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

            playerPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            playerSpeed = 600f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            spookySprite = Content.Load<Texture2D>("assets/spookyahh");
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

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

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}