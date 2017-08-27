using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyIntoNyx
{
    class Player
    {

        public AnimationPlayer animationPlayer;
        public Vector2 position = new Vector2(64,1024-64);
        public Vector2 velocity;
        public Rectangle playerRect;
        int lives;
        public bool hasJumped = false;
        public bool hasDied = false;
        public SoundEffect oohmp;
        Animation walkAnimation;
        Animation idleAnimation;
        
        /*
        public int Heigth { return PlayerAnimation.FrameWidth; }
        public int Heigth { return PlayerAnimation.FrameHeigth; }
        */

        public Vector2 Position
        {
            get { return position; }
        }

        
        public void Load(ContentManager Content)
        {
            
            walkAnimation = new Animation(Content.Load<Texture2D>(@"spriteRight"), 64, 0.1f, true);
            idleAnimation = new Animation(Content.Load<Texture2D>(@"spriteStraight"), 64, 0.1f, true);
            oohmp = Content.Load<SoundEffect>(@"oomph");
            //animationPlayer.PlayAnimation(walkAnimation);
            //animationPlayer.PlayAnimation(idleAnimation);
            //Add new animations here
        }

        public void Update(GameTime gameTime, Map map)
        {
            position += velocity;
            playerRect = new Rectangle((int)position.X, (int)position.Y, walkAnimation.FrameWidth, walkAnimation.FrameHeight - 2);

            Input(gameTime, map);

            if (velocity.Y < 10)
                velocity.Y += 0.4f;
        }

        private void Input(GameTime gameTime, Map map)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else velocity.X = 0f;

            if(Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                if (map.CanJump(playerRect))
                {
                    position.Y -= 5f;
                    playerRect.Y = (int)position.Y - 1;
                    velocity.Y = -9f;
                    hasJumped = true;
                }
               
            }

            if (velocity.X != 0)
                animationPlayer.PlayAnimation(walkAnimation);
            else if (velocity.X == 0)
                animationPlayer.PlayAnimation(idleAnimation);
                
        }

        public void Collision(CollisionTiles tile, int xOffset, int yOffset)
        {
            Rectangle newRectangle = tile.Rectangle;

            if (playerRect.TouchTopOf(newRectangle)){
                position.Y = newRectangle.Y - playerRect.Height;
                velocity.Y = 0f;
                hasJumped = false;
                if (tile.tileType == 4)
                {
                    playerDead();
                }


            }

            if (playerRect.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - playerRect.Width - 2;
                if (tile.tileType == 4)
                {
                    playerDead();
                }
            }

            if (playerRect.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
                if (tile.tileType == 4)
                {
                    playerDead();
                }
            }

            if (playerRect.TouchBottomOf(newRectangle))
            {
                velocity.Y = 5f;
                if (tile.tileType == 4)
                {
                    playerDead();
                }
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - playerRect.Width) position.X = xOffset - playerRect.Width;
            if (position.Y < 0) { velocity.Y = 1f; };
            if (position.Y > yOffset - playerRect.Height)
            {
                position.Y = yOffset - playerRect.Height;
                hasDied = true;
            } 
        }

        private void playerDead()
        {
            oohmp.Play();
            position.X = 0;
            position.Y = 1024 - 64;
   
              
            
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (velocity.X >= 0)
                flip = SpriteEffects.None;
            else if (velocity.X <= 0)
                flip = SpriteEffects.FlipHorizontally;
            
            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }
    }
}
