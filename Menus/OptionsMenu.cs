using FarmervsZombies.Managers;
using FarmervsZombies.MenuButtons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FarmervsZombies.Menus
{
    internal sealed class OptionsMenu : Menu
    {
        private Texture2D mBackground;

        private SpriteFont mSpriteFont;

        private Switch mVolume;
        private Switch mEffects;
        private Switch mMusic;
        private Switch mCheatMode;

        private MenuButton mResolution;

        public static bool mFromGame;
        private bool mFirstClick = true;

        // The Textures for the Animation of slider
        private VolumeSlider mVolumeSlider;
        private VolumeSlider mEffectSlider;
        private VolumeSlider mMusicSlider;

        public void Initialize(GraphicsDevice gd)
        {
            mScreenHeight = gd.Viewport.Height;
            mScreenWidth = gd.Viewport.Width;
            InputManager.EscPressed += EscPressedOptionsMenu;
        }

        public void LoadContent(ContentManager content)
        {
            // Loading background picture
            mBackground = content.Load<Texture2D>("Buttons/pause");
            var volumeOffImage = TextureManager.GetTexture("button_off");
            var volumeOnImage = TextureManager.GetTexture("button_on");

            mSpriteFont = content.Load<SpriteFont>("FileHeading");

            mVolume = new Switch(volumeOnImage, volumeOffImage, new Point(200, 100),
                                64, 128, "An", "Aus");

            mEffects = new Switch(volumeOnImage, volumeOffImage, new Point(200, 200),
                64, 128, "An", "Aus");

            mMusic = new Switch(volumeOnImage, volumeOffImage, new Point(200, 300),
                64, 128, "An", "Aus");

            mCheatMode = new Switch(volumeOnImage, volumeOffImage, new Point(200, 400),
                64, 128, "An", "Aus");

            mResolution = new MenuButton(new Point(200, 500), 75, 240, "Auflösung anpassen");

            mVolume.LoadContent(content);
            mEffects.LoadContent(content);
            mMusic.LoadContent(content);
            mCheatMode.LoadContent(content);
            mResolution.LoadContent(content);

            var slider00 = content.Load<Texture2D>("Buttons/slider/Volumebar_00");
            var slider01 = content.Load<Texture2D>("Buttons/slider/Volumebar_01");
            var slider02 = content.Load<Texture2D>("Buttons/slider/Volumebar_02");
            var slider03 = content.Load<Texture2D>("Buttons/slider/Volumebar_03");
            var slider04 = content.Load<Texture2D>("Buttons/slider/Volumebar_04");
            var slider05 = content.Load<Texture2D>("Buttons/slider/Volumebar_05");
            var slider06 = content.Load<Texture2D>("Buttons/slider/Volumebar_06");
            var slider07 = content.Load<Texture2D>("Buttons/slider/Volumebar_07");
            var slider08 = content.Load<Texture2D>("Buttons/slider/Volumebar_08");
            var slider09 = content.Load<Texture2D>("Buttons/slider/Volumebar_09");
            var slider10 = content.Load<Texture2D>("Buttons/slider/Volumebar_10");

            mVolumeSlider = new VolumeSlider(new Point(400, 100), 70, 300, slider00, slider01,
                slider02, slider03, slider04, slider05, slider06,
                slider07, slider08, slider09, slider10);

            mEffectSlider = new VolumeSlider(new Point(400, 200), 70, 300, slider00, slider01,
                slider02, slider03, slider04, slider05, slider06,
                slider07, slider08, slider09, slider10, VolumeSlider.VolumeMode.EffectVolume);

            mMusicSlider = new VolumeSlider(new Point(400, 300), 70, 300, slider00, slider01,
                slider02, slider03, slider04, slider05, slider06,
                slider07, slider08, slider09, slider10, VolumeSlider.VolumeMode.MusicVolume);

            mBackground = content.Load<Texture2D>("Menus/OptionsAndStatisticsMenu");
        }

        public void Update()
        {
            var inputState = InputManager.GetCurrentInputState();
            if (!inputState.IsButtonPressed(MouseButton.LeftButton)) mFirstClick = false;
            if (!mFirstClick) mVolumeSlider.Update(inputState);
            if (mVolume.Update(inputState))
            {
                mVolume.ChangeSwitch();
                ChangeVolume();
            }
            else
            {
                mVolume.SetSwitch(SoundManager.SoundOn);
            }

            if (!mFirstClick) mEffectSlider.Update(inputState);
            if (mEffects.Update(inputState))
            {
                mEffects.ChangeSwitch();
                ChangeEffects();
            }
            else
            {
                mEffects.SetSwitch(SoundManager.EffectsOn);
            }

            if (!mFirstClick) mMusicSlider.Update(inputState);
            if (mMusic.Update(inputState))
            {
                mMusic.ChangeSwitch();
                ChangeMusic();
            }
            else
            {
                mMusic.SetSwitch(SoundManager.MusicOn);
            }

            if (mCheatMode.Update(inputState))
            {
                mCheatMode.ChangeSwitch();
                Game1.CheatMode = !Game1.CheatMode;
            }
            else
            {
                mCheatMode.SetSwitch(Game1.CheatMode);
            }

            if (mResolution.Update(inputState))
            {
                GameStateManager.State = GameState.ResolutionMenu;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mBackground, new Rectangle(0,0,mScreenWidth,mScreenHeight), Color.Azure);
            spriteBatch.DrawString(mSpriteFont, "Ton : ", new Vector2(50, 120), Color.Black);
            spriteBatch.DrawString(mSpriteFont, "Effekte : ", new Vector2(50, 220), Color.Black);
            spriteBatch.DrawString(mSpriteFont, "Musik : ", new Vector2(50, 320), Color.Black);
            spriteBatch.DrawString(mSpriteFont, "Cheats : ", new Vector2(50, 420), Color.Black);

            mVolume.Draw(spriteBatch);
            mVolumeSlider.Draw(spriteBatch);
            mEffects.Draw(spriteBatch);
            mEffectSlider.Draw(spriteBatch);
            mMusic.Draw(spriteBatch);
            mMusicSlider.Draw(spriteBatch);
            mResolution.Draw(spriteBatch);
            mCheatMode.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void EscPressedOptionsMenu(object sender, InputState inputState)
        {
            if (GameStateManager.State != GameState.OptionsMenu) return;
            if (mFromGame)
            {
                GameStateManager.State = GameState.PlayGameMenu;
                mFromGame = false;
                mFirstClick = true;
            }
            else
            {
                GameStateManager.State = GameState.MainMenu;
                mFirstClick = true;
            }
        }

        private static void ChangeVolume()
        {
            SoundManager.SoundOn = !SoundManager.SoundOn;
        }


        private static void ChangeEffects()
        {
            SoundManager.EffectsOn = !SoundManager.EffectsOn;
            SoundManager.EffectVolume = SoundManager.EffectsOn ? 1.0f : 0.0f;
        }

        private static void ChangeMusic()
        {
            SoundManager.MusicOn = !SoundManager.MusicOn;
            SoundManager.MusicVolume = SoundManager.MusicOn ? 1.0f : 0.0f;
        }
    }
}
