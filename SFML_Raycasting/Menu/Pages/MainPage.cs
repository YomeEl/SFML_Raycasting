using SFML.Graphics;
using SFML.Window;

namespace Raycasting
{
    class MainPage : MenuPage
    {
        public MainPage(RenderWindow win) : base(win) { }

        public override MenuEvent OnMouseClick(MouseButtonEventArgs e)
        {
            TextButton selected = GetSelectedButton();

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

            TextButton b_newGame = new TextButton(t_newGame, "NewGame", true);
            TextButton b_loadGame = new TextButton(t_loadGame, "LoadGame", false);
            TextButton b_editor = new TextButton(t_editor, "Editor", true);
            TextButton b_settings = new TextButton(t_settings, "Settings", true);
            TextButton b_quit = new TextButton(t_quit, "Quit", true);

            buttons = new TextButton[5];

            buttons[0] = b_newGame;
            buttons[1] = b_loadGame;
            buttons[2] = b_editor;
            buttons[3] = b_settings;
            buttons[4] = b_quit;

            foreach (TextButton b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }
    }
}