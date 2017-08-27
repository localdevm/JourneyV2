using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JourneyIntoNyx
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Player player;
        Camera camera;
        Texture2D background;
        Rectangle mainFrame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

      
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            player = new Player();
            camera = new Camera(GraphicsDevice.Viewport);
            
            base.Initialize();
        }


     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>(@"BackdropDawnFull");
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Tiles.Content = Content;
            map.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0, },
                {0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,0, },
                {1,3,0,0,4,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,4,0,0,0,0,0,1,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,0, },
            }, 64);
            player.Load(Content);

            // TODO: use this.Content to load your game content here
        }

       
        public void playerDead()
        {
            
            if (player.hasDied == true)
            {
                player = new Player();
            }
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, map);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile, map.Width, map.Heigth);
                camera.Update(player.Position, map.Width, map.Heigth);
                playerDead();
            }
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.Transform);
            map.Draw(spriteBatch);
            player.Draw(gameTime,spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
