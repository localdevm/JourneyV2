using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JourneyIntoNyx
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Player player;
        Camera camera;
        Texture2D background;
        Texture2D splashy;
        Texture2D endsplash;
        Rectangle mainFrame;
        SpriteFont scorefont;
        SpriteFont timetot;
        bool splash = true;
        public int enddeaths;
        private const float _delay = 5; // seconds
        private float _remainingDelay = _delay;

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
            splashy = Content.Load<Texture2D>(@"Splash");
            endsplash = Content.Load<Texture2D>(@"BackdropDay");
            timetot = Content.Load<SpriteFont>(@"score");
            scorefont = Content.Load<SpriteFont>(@"score");

            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Tiles.Content = Content;
            Song song = Content.Load<Song>(@"rude");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            map.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,2,0,0,0,0,1,3,0,4,2,0,1,4,3,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,1,2,3,0,4,0,0,0,0,2,0,2, },
                {0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,0,4,0,0,2,0,0,0,0,0,0,0,1,3,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,5,4,0,1,3,0,0,0,1,2,2,2,3,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0, },
                {0,0,0,0,1,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0, },
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,5,2,4,2,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,4, },
                {0,0,0,0,2,0,0,0,0,0,4,0,0,0,0,0,0,0,4,0,2,0,4,0,0,0,0,0,0,2,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,2,0,0,0,0,4,0,0,0,0,0,0,1,2,3,0,0,0,2,0,0,0,0,0,0,0,2,0,0,4,0,0,0,0,0,0,0,0,0,1,2,2,3, },
                {0,4,4,0,0,0,2,0,0,0,4,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,4,0,0,0,0,0,1,3,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,2,5,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,4,0,4,0,0,0,0,0,0,0,0,0,7,0, },
                {0,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,3,0, },
                {0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,4,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,0,0, },
                {2,2,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0, },
            }, 64);
            player.Load(Content);

            // TODO: use this.Content to load your game content here
        }


        public void playerDead()
        {

            if (player.hasDied == true)
            {
                player.oohmp.Play();
                player.position.X = 0;
                player.position.Y = 1024 - 64;
                player.amountdied = player.amountdied + 1;
                player.hasJumped = false;
                player.hasDied = false;

            }
        }

        public void win()
        {
            if (player.playerwon == true)
            {
                enddeaths = player.amountdied;
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

            if (splash == true)
            {
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

                _remainingDelay -= timer;

                if (_remainingDelay <= 0)
                {

                    _remainingDelay = _delay;
                    splash = false;
                }

            }

            player.Update(gameTime, map);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile, map.Width, map.Heigth);
                camera.Update(player.Position, map.Width, map.Heigth);

            }
   
            playerDead();
            win();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {

            //Only draw this for the beginning
            if (splash == true)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                //spriteBatch.Draw();
                spriteBatch.Begin();
                spriteBatch.Draw(splashy, mainFrame, Color.White);
                spriteBatch.End();
            }
            
            else if (player.playerwon == true)
            {
                GraphicsDevice.Clear(Color.White);
                spriteBatch.Begin();
                spriteBatch.Draw(endsplash, mainFrame, Color.White);
                spriteBatch.DrawString(scorefont, "Amount died : " + enddeaths, new Vector2(80, 100), Color.Black);
                int endscore = (200 - enddeaths *5);
                spriteBatch.DrawString(timetot, "Total score : " + endscore, new Vector2(80, 150), Color.Black);
                spriteBatch.End();
            }
            

            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here
                spriteBatch.Begin();
                spriteBatch.Draw(background, mainFrame, Color.White);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                map.Draw(spriteBatch);
                player.Draw(gameTime, spriteBatch);
                spriteBatch.End();


                base.Draw(gameTime);
            }

        }
    }
}
