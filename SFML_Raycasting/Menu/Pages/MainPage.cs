using SFML.Graphics;
using SFML.Window;

namespace Raycasting
{
    class MainPage : MenuPage
    {
        public MainPage(RenderWindow win) : base(win) { }

        public override MenuEvent OnMouseClick(MouseButtonEventArgs e)
        {
            Button selected = GetSelectedButton();

            if (selected != null && selected.Enabled)
            {
                switch (selected.Name)
                {
                    case "NewGame":
                        return MenuEvent.NewGame;

                    case "LoadGame":
                        break;

                    case "Editor":
                        return MenuEvent.ShowEditor;
                        break;

                    case "Settings":
                        return MenuEvent.ShowSettings;

                    case "Quit":
                        return MenuEvent.Quit;
                }
            }
            return MenuEvent.Idle;
        }

        protected override void CreateButtons()
        {
            Text t_newGame = new Text("Play", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_loadGame = new Text("Load game", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_editor = new Text("Map editor", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_settings = new Text("Settings", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_quit = new Text("Quit", Settings.Menu.Font, Settings.Menu.FontSize);

            Button b_newGame = new Button(t_newGame, "NewGame", true);
            Button b_loadGame = new Button(t_loadGame, "LoadGame", false);
            Button b_editor = new Button(t_editor, "Editor", true);
            Button b_settings = new Button(t_settings, "Settings", true);
            Button b_quit = new Button(t_quit, "Quit", true);

            buttons = new Button[5];

            buttons[0] = b_newGame;
            buttons[1] = b_loadGame;
            buttons[2] = b_editor;
            buttons[3] = b_settings;
            buttons[4] = b_quit;

            foreach (Button b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }
    }
}