﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyIntoNyx
{
    class Animation
    {
        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }

        }

        public int FrameWidth;
        public int FrameHeight
        {
            get { return texture.Height; }
        }

        float frameTime;
        public float FrameTime
        {
            get { return frameTime; }
        }
        public int FrameCount;

        bool isLooping;
        public bool IsLooping
        {
            get { return isLooping; }
        }

        public Animation(Texture2D newTexture, int newFrameWidth, float newFrameTime, bool newIsLooping)
        {
            texture = newTexture;
            FrameWidth = newFrameWidth;
            frameTime = newFrameTime;
            isLooping = newIsLooping;
            FrameCount = texture.Width / FrameWidth;
            
        }
        
    }
}
