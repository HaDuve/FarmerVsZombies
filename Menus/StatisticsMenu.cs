using System.Globalization;
using FarmervsZombies.Managers;
using FarmervsZombies.MenuButtons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FarmervsZombies.Menus
{
    internal sealed class StatisticsMenu: Menu
    {
        private Texture2D mBackground;
        private HudElement mAnimalsBought;
        private HudElement mAnimalsMax;
        private HudElement mGametime;
        private HudElement mMaxGold;
        private HudElement mZombiesKilled;
        private HudElement mWon;
        private HudElement mLost;
        private HudElement mPoints;


        private Texture2D mHighScoreTexture;

        private SpriteFont mFont;
        public void Initialize(GraphicsDevice gd)
        {
            mScreenHeight = gd.Viewport.Height;
            mScreenWidth = gd.Viewport.Width;
            InputManager.EscPressed += EscPressedStatisticsMenu;
        }

        public void LoadContent(ContentManager content)
        {
            // Loading background picture
            mBackground = content.Load<Texture2D>("Menus/OptionsAndStatisticsMenu");
            mAnimalsBought = new HudElement(new Point(470, mScreenHeight / 12), new Point(150, 30));
            mAnimalsMax = new HudElement(new Point(470, 2 * mScreenHeight / 12), new Point(150, 30));
            mGametime = new HudElement(new Point(470, 3 * mScreenHeight / 12), new Point(150, 30));
            mMaxGold = new HudElement(new Point(470, 4 * mScreenHeight / 12), new Point(150, 30));
            mZombiesKilled = new HudElement(new Point(470, 5 * mScreenHeight / 12), new Point(150, 30));
            mWon = new HudElement(new Point(470, 6 * mScreenHeight / 12), new Point(150, 30));
            mLost = new HudElement(new Point(470, 7 * mScreenHeight / 12), new Point(150, 30));
            mPoints = new HudElement(new Point(470, 8 * mScreenHeight / 12), new Point(150, 30));
            

            mFont = content.Load<SpriteFont>("File");

            mHighScoreTexture = content.Load<Texture2D>("Textures\\highscore");

            mAnimalsBought.LoadContent(content);
            mAnimalsMax.LoadContent(content);
            mGametime.LoadContent(content);
            mMaxGold.LoadContent(content);
            mZombiesKilled.LoadContent(content);
            mWon.LoadContent(content);
            mLost.LoadContent(content);
            mPoints.LoadContent(content);
        }


        public void Update()
        {
            mAnimalsBought.ChangeText(Game1.sStatistics.Animals.ToString(CultureInfo.InvariantCulture));
            mAnimalsMax.ChangeText(Game1.sStatistics.OldAnimalsAlive.ToString(CultureInfo.InvariantCulture));
            mGametime.ChangeText(Game1.sStatistics.OldGameTime.ToString(CultureInfo.InvariantCulture));
            mMaxGold.ChangeText(Game1.sStatistics.OldGold.ToString(CultureInfo.InvariantCulture));
            mZombiesKilled.ChangeText(Game1.sStatistics.ZombiesKilled.ToString(CultureInfo.InvariantCulture));
            mWon.ChangeText(Game1.sStatistics.GameWon.ToString());
            mLost.ChangeText(Game1.sStatistics.GameLost.ToString());
            mPoints.ChangeText(Game1.sStatistics.OldPoints.ToString());
            Game1.sStatistics.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mBackground, new Rectangle(0, 0, mScreenWidth, mScreenHeight),Color.Azure);
            spriteBatch.DrawString(mFont, "Insgesamt gekaufte Tiere : ", new Vector2(50, mAnimalsBought.mBackRect.Y), Color.Black);
            mAnimalsBought.Draw(spriteBatch);
            spriteBatch.DrawString(mFont, "Höste Anzahl gleichzeitig lebender Tiere : ", new Vector2(50, mAnimalsMax.mBackRect.Y), Color.Black);
            mAnimalsMax.Draw(spriteBatch);

            spriteBatch.DrawString(mFont, "Insgesamte Ingame time in Sekunden : ", new Vector2(50, mGametime.mBackRect.Y), Color.Black);
            mGametime.Draw(spriteBatch);

            spriteBatch.DrawString(mFont, "Maximale Anzahl Gold : ", new Vector2(50, mMaxGold.mBackRect.Y), Color.Black);
            mMaxGold.Draw(spriteBatch);

            spriteBatch.DrawString(mFont, "Insgesamt getötete Zombies : ", new Vector2(50, mZombiesKilled.mBackRect.Y), Color.Black);
            mZombiesKilled.Draw(spriteBatch);

            spriteBatch.DrawString(mFont, "Gewonenne Spiele : ", new Vector2(50, mWon.mBackRect.Y), Color.Black);
            mWon.Draw(spriteBatch);

            spriteBatch.DrawString(mFont, "Verlorene Spiele : ", new Vector2(50, mLost.mBackRect.Y), Color.Black);
            mLost.Draw(spriteBatch);

            spriteBatch.DrawString(mFont, "Maximale Punkte : ", new Vector2(50, mPoints.mBackRect.Y), Color.Black);
            mPoints.Draw(spriteBatch);

            spriteBatch.Draw(mHighScoreTexture,new Rectangle(50,9 * mScreenHeight / 12, mScreenHeight / 4, mScreenHeight / 4), Color.AliceBlue);
            spriteBatch.DrawString(mFont, Game1.sStatistics.mHighScore1.ToString(), new Vector2(50 + mScreenHeight / 24, 9 * mScreenHeight / 12 + 2 * mScreenHeight / 20), Color.Black);
            spriteBatch.DrawString(mFont, Game1.sStatistics.mHighScore2.ToString(), new Vector2(50 + mScreenHeight / 24, 9 * mScreenHeight / 12 + 3 * mScreenHeight / 20), Color.Black);
            spriteBatch.DrawString(mFont, Game1.sStatistics.mHighScore3.ToString(), new Vector2(50 + mScreenHeight / 24, 9 * mScreenHeight / 12 + 4 * mScreenHeight / 20), Color.Black);

            spriteBatch.End();
        }

        public void UpdateButtons()
        {
            mScreenWidth = Game1.Resolution.Item1;
            mScreenHeight = Game1.Resolution.Item2;

            mAnimalsBought.SetPosition(new Point(470, mScreenHeight / 12));
            mAnimalsMax.SetPosition(new Point(470, 2 * mScreenHeight / 12));
            mGametime.SetPosition(new Point(470, 3 * mScreenHeight / 12));
            mMaxGold.SetPosition(new Point(470, 4 * mScreenHeight / 12));
            mZombiesKilled.SetPosition(new Point(470, 5 * mScreenHeight / 12));
            mWon.SetPosition(new Point(470, 6 * mScreenHeight / 12));
            mLost.SetPosition(new Point(470, 7 * mScreenHeight / 12));
            mPoints.SetPosition(new Point(470, 8 * mScreenHeight / 12));
        }

        private void EscPressedStatisticsMenu(object sender, InputState inputState)
        {
            if (GameStateManager.State != GameState.StatisticsMenu) return;
            GameStateManager.State = GameState.MainMenu;
        }
    }
}
