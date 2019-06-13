using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Raycasting
{
    class Menu
    {
        RenderWindow app;

        List<Button> buttons = new List<Button>();

        Vector position = new Vector(0, 0);
        Vector anchor = new Vector(0, 0);

        public Vector Anchor
        {
            get
            {
                return anchor;
            }
            set
            {
                anchor = value;
                UpdateButtons();
            }
        }

        public Vector Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateButtons();
            }
        }

        public Menu(RenderWindow app)
        {
            UpdateApp(app);
            UpdateButtons();
        }

        public void UpdateApp(RenderWindow app)
        {
            this.app = app;
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
            FloatRect bounds = new FloatRect(b.Text.Position.X, b.Text.Position.Y, b.Text.GetLocalBounds().Width, Settings.Menu.FontSize - 6);
            bounds.Height += Settings.Menu.FontSize / 4;

            return bounds.Contains(mousePos.X, mousePos.Y);
        }

        void setTextColor(Button b)
        {
            b.Text.FillColor = b.Enabled ? isMouseHovering(b) ? Settings.Menu.SelectedColor : Settings.Menu.NotSelectedColor : Settings.Menu.DisabledColor;
        }
        
        void UpdateButtons()
        {
            buttons.Clear();
            Vector offset = position - anchor;
            const uint fontHeight = Settings.Menu.FontSize;
            const uint gap = Settings.Menu.MenuGap;

            Text t_newGame = new Text("Play", Settings.Menu.MenuFont, fontHeight);
            Text t_loadGame = new Text("Load game", Settings.Menu.MenuFont, fontHeight);
            Text t_settings = new Text("Settings", Settings.Menu.MenuFont, fontHeight);
            Text t_quit = new Text("Quit", Settings.Menu.MenuFont, fontHeight);

            t_newGame.Position = new Vector2f(offset.X, offset.Y);
            t_loadGame.Position = new Vector2f(offset.X, offset.Y + fontHeight + gap);
            t_settings.Position = new Vector2f(offset.X, offset.Y + 2 * (fontHeight + gap));
            t_quit.Position = new Vector2f(offset.X, offset.Y + 3 * (fontHeight + gap));

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

        public uint GetHeight()
        {
            return (Settings.Menu.FontSize + Settings.Menu.MenuGap) * (uint)buttons.Count - Settings.Menu.MenuGap;
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