using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyIntoNyx
{
    struct AnimationPlayer
    {
        
        Animation animation;
        public Animation Animation
        {
            get { return animation ;}
            set { animation = value; }
        }
        
        int frameIndex;
        public int FrameIndex
        {
            get { return FrameIndex; }
            set { frameIndex = value; }
        }

        private float timer;
        public Vector2 Origin
        {
            get { return new Vector2(0,0); }
        }

        public void PlayAnimation(Animation newAnimation)
        {
            if (animation == newAnimation)
                return;
            animation = newAnimation;
            frameIndex = 0;
            timer = 0;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            while (timer >= animation.FrameTime)
            {
                timer -= animation.FrameTime;
                if (animation.IsLooping)
                    frameIndex = (frameIndex + 1) % animation.FrameCount;
                else frameIndex = Math.Min(frameIndex + 1, animation.FrameCount - 1);
            }

            Rectangle sourceRect = new Rectangle(frameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);

            spriteBatch.Draw(Animation.Texture, position, sourceRect, Color.White, 0f, Origin, 1f, spriteEffects, 0f);
        }
    }
}
