using System.Collections.Generic;
using FarmervsZombies.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FarmervsZombies.MenuButtons
{
    internal sealed class VolumeSlider
    {
        private readonly List<Texture2D> mBtnImages;

        private Point mBtnPos;
        private readonly int mBtnHeight;
        private readonly int mBtnWidth;
        private readonly VolumeMode mMode;

        internal enum VolumeMode
        {
            MasterVolume,
            EffectVolume,
            MusicVolume
        }

        public void SetPosition(Point pos)
        {
            mBtnPos = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (mMode)
            {
                case VolumeMode.MasterVolume:
                    spriteBatch.Draw(mBtnImages[(int)(SoundManager.MasterVolume * 10)], new Rectangle(mBtnPos, new Point(mBtnWidth, mBtnHeight)),
                        Color.White);
                    break;
                case VolumeMode.EffectVolume:
                    spriteBatch.Draw(mBtnImages[(int)(SoundManager.EffectVolume * 10)], new Rectangle(mBtnPos, new Point(mBtnWidth, mBtnHeight)),
                        Color.White);
                    break;
                case VolumeMode.MusicVolume:
                    spriteBatch.Draw(mBtnImages[(int)(SoundManager.MusicVolume * 10)], new Rectangle(mBtnPos, new Point(mBtnWidth, mBtnHeight)),
                        Color.White);
                    break;
            }
        }

        public VolumeSlider(Point btnPos, int btnHeight, int btnWidth, Texture2D btnImage0, Texture2D btnImage1, Texture2D btnImage2,
                                    Texture2D btnImage3, Texture2D btnImage4, Texture2D btnImage5, Texture2D btnImage6, Texture2D btnImage7,
                                    Texture2D btnImage8, Texture2D btnImage9, Texture2D btnImage10,
                                    VolumeMode mode = VolumeMode.MasterVolume)
        {
            mBtnImages = new List<Texture2D>
            {
                btnImage0,
                btnImage1,
                btnImage2,
                btnImage3,
                btnImage4,
                btnImage5,
                btnImage6,
                btnImage7,
                btnImage8,
                btnImage9,
                btnImage10
            };
            mBtnPos = btnPos;
            mBtnHeight = btnHeight;
            mBtnWidth = btnWidth;
            mMode = mode;
        }



        public void Update(InputState inputState)
        {
            var mousePosition = inputState.mMouseWindowPosition;
            if (mousePosition.X > mBtnPos.X && mousePosition.X < mBtnPos.X + mBtnWidth && SoundManager.EffectsOn &&
                mousePosition.Y > mBtnPos.Y && mousePosition.Y < mBtnPos.Y + mBtnHeight && inputState.IsButtonPressed(MouseButton.LeftButton))
            {
                var pos = mousePosition.X - mBtnPos.X;

                switch (mMode)
                {
                    case VolumeMode.MasterVolume:
                        SoundManager.MasterVolume = 1.0f;
                        break;
                    case VolumeMode.EffectVolume:
                        SoundManager.EffectVolume = 1.0f;
                        break;
                    case VolumeMode.MusicVolume:
                        SoundManager.MusicVolume = 1.0f;
                        break;
                }

                for (var vol = 0.1f; vol <= 1f; vol += 0.1f)
                {
                    if (pos <= mBtnWidth * vol)
                    {
                        switch (mMode)
                        {
                            case VolumeMode.MasterVolume:
                                SoundManager.MasterVolume = vol;
                                break;
                            case VolumeMode.EffectVolume:
                                SoundManager.EffectVolume = vol;
                                break;
                            case VolumeMode.MusicVolume:
                                SoundManager.MusicVolume = vol;
                                break;
                        }
                        break;
                    }
                }

            }
            else
            {
                switch (mMode)
                {
                    case VolumeMode.MasterVolume:
                        if (!SoundManager.SoundOn) SoundManager.MasterVolume = 0.0f;
                        break;
                    case VolumeMode.EffectVolume:
                        if (!SoundManager.EffectsOn) SoundManager.EffectVolume = 0.0f;
                        break;
                    case VolumeMode.MusicVolume:
                        if (!SoundManager.MusicOn) SoundManager.MusicVolume = 0.0f;
                        break;
                }
            }
        }
    }
}

