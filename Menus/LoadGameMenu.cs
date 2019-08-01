﻿using System.Collections.Generic;
using System.IO;
using FarmervsZombies.Managers;
using FarmervsZombies.MenuButtons;
using FarmervsZombies.SaveAndLoad;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FarmervsZombies.Menus
{
    internal sealed class LoadGameMenu : Menu
    {
        private SpriteFont mSpriteFont;
        private Texture2D mBackground;
        private List<MenuButton> mListButtons;

        private MenuButton mRefresh;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            spriteBatch.Draw(mBackground, new Rectangle(0, 0, mScreenWidth, mScreenHeight), Color.Azure);
            spriteBatch.DrawString(mSpriteFont, "Bitte Spielstand auswählen", new Vector2(mScreenWidth / 2f -150,30), Color.Blue);

            foreach (var btn in mListButtons)
            {
                btn.Draw(spriteBatch);
            }

            mRefresh.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Initialize(GraphicsDevice gd)
        {
            mScreenHeight = gd.Viewport.Height;
            mScreenWidth = gd.Viewport.Width;
            InputManager.EscPressed += EscPressedLoadGameMenu;
            mListButtons = new List<MenuButton>();
        }

        public void LoadContent(ContentManager content)
        {
            mListButtons.Clear();

            var btnRowCounter = 0;
            var btnColumnCounter = 0;

            mSpriteFont = content.Load<SpriteFont>("FileHeading");
            mBackground = content.Load<Texture2D>("Menus/MainMenu");
            mRefresh = new MenuButton(new Point(mScreenWidth - 170, 30), 50, 150, "Aktualisieren");

            var di = new DirectoryInfo(".\\");
            var files = di.GetFiles("*.spielstand");

            foreach (var btn in files)
            {
                var pufferElement = new MenuButton(new Point(mScreenWidth / 8 - 100 + btnColumnCounter * mScreenWidth / 4, btnRowCounter * 100 + 100), 50,200, btn.Name.Remove(btn.Name.Length - 11));
                btnRowCounter++;
                if (btnRowCounter > 4)
                {
                    btnColumnCounter++;
                }
                btnRowCounter %= 5;
                pufferElement.LoadContent(content);
                mListButtons.Add(pufferElement);
            }

            mRefresh.LoadContent(content);
        }

        public void Update(ContentManager content)
        {
            var inputState = InputManager.GetCurrentInputState();
            if (mRefresh.Update(inputState))
            {
                LoadContent(content);
            }
            else
            {
                foreach (var btn in mListButtons)
                {
                    if (btn.Update(inputState))
                    {
                        var saveLoad = new SaveLoad();
                        saveLoad.Load(btn.GetInput() + ".spielstand");
                        Game1.FarmerDied = false;
                        Game1.GraveyardBuilt = true;
                        Game1.AiDied = false;
                        GameStateManager.State = GameState.PlayGameMenu;
                    }
                }
            }
        }

        public void UpdateButtons()
        {
            mScreenWidth = Game1.Resolution.Item1;
            mScreenHeight = Game1.Resolution.Item2;

            mRefresh.SetPosition(new Point(mScreenWidth - 170, 30));

            var btnRowCounter = 0;
            var btnColumnCounter = 0;

            foreach (var btn in mListButtons)
            {
                btn.SetPosition(new Point(mScreenWidth / 8 - 100 + btnColumnCounter * mScreenWidth / 4,
                    btnRowCounter * 100 + 100));
                btnRowCounter++;
                if (btnRowCounter > 4)
                {
                    btnColumnCounter++;
                }
                btnRowCounter %= 5;
            }
        }

        private void EscPressedLoadGameMenu(object sender, InputState inputState)
        {
            if (GameStateManager.State != GameState.LoadGame) return;
            GameStateManager.State = GameState.MainMenu;
        }
    }
}
