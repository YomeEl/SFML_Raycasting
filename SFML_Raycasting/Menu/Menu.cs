using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Raycasting
{
    class Menu
    {
        RenderWindow app;

        uint left;
        uint top;
        uint cellSize;
        uint fontHeight;

        List<Button> buttons = new List<Button>();

        public Menu(RenderWindow app)
        {
            UpdateApp(app);
        }

        public void UpdateApp(RenderWindow app)
        {
            this.app = app;

            UpdateConstants();
            UpdateButtons();
        }

        public MenuEvent MouseClick(MouseButtonEventArgs e)
        {
            Button selected = null;
            foreach (Button b in buttons)
            {
                if (isMouseHovering(b))
                {
                    selected = b;
                }
            }

            if (selected != null && selected.Enabled)
            {
                switch (selected.Name)
                {
                    case "NewGame":
                        return MenuEvent.NewGame;

                    case "LoadGame":
                        break;

                    case "Settings":
                        break;

                    case "Quit":
                        app.Close();
                        break;
                }
            }
            return MenuEvent.Idle;
        }

        bool isMouseHovering(Button b)
        {
            Vector2i mousePos = Mouse.GetPosition(app);
            FloatRect bounds = b.Text.GetLocalBounds();
            Vector2f position = b.Text.Position;
            bounds.Height += fontHeight / 3;
            position += new Vector2f(0, fontHeight / 8);
            bool xCondition = mousePos.X >= position.X && mousePos.X < position.X + bounds.Width;
            bool yCondition = mousePos.Y >= position.Y && mousePos.Y < position.Y + bounds.Height;
            return xCondition && yCondition;
        }

        void setTextColor(Button b)
        {
            if (b.Enabled)
            {
                b.Text.FillColor = isMouseHovering(b) ? Settings.Menu.SelectedColor : Settings.Menu.NotSelectedColor;
            }
            else
            {
                b.Text.FillColor = new Color(180, 128, 128);
            }
        }

        void UpdateConstants()
        {
            uint height = app.Size.Y;

            top = 0;
            uint bottom = height;

            top += height / 10;
            bottom -= height / 10;

            cellSize = (bottom - top) / 4;
            fontHeight = cellSize / 2;

            left = top;
        }

        void UpdateButtons()
        {
            buttons.Clear();

            Text t_newGame = new Text("Play", Settings.Menu.MenuFont, fontHeight);
            Text t_loadGame = new Text("Load game", Settings.Menu.MenuFont, fontHeight);
            Text t_settings = new Text("Settings", Settings.Menu.MenuFont, fontHeight);
            Text t_quit = new Text("Quit", Settings.Menu.MenuFont, fontHeight);

            t_newGame.Position = new Vector2f(left, top);
            t_loadGame.Position = new Vector2f(left, top + cellSize);
            t_settings.Position = new Vector2f(left, top + 2 * cellSize);
            t_quit.Position = new Vector2f(left, top + 3 * cellSize);

            Button b_newGame = new Button(t_newGame, "NewGame", true);
            Button b_loadGame = new Button(t_loadGame, "LoadGame", false);
            Button b_settings = new Button(t_settings, "Settings", false);
            Button b_quit = new Button(t_quit, "Quit", true);

            buttons.Add(b_newGame);
            buttons.Add(b_loadGame);
            buttons.Add(b_settings);
            buttons.Add(b_quit);

            foreach (Button b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }

        public void Draw()
        {
            foreach (Button b in buttons)
            {
                setTextColor(b);
                app.Draw(b.Text);
            }
        }
    }
}