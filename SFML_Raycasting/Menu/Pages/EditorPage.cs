using SFML.Window;
using SFML.Graphics;

namespace Raycasting
{
    class EditorPage : MenuPage
    {
        public EditorPage(RenderWindow win) : base(win) { }

        public override MenuEvent OnMouseClick(MouseButtonEventArgs e)
        {
            TextButton selected = GetSelectedButton();

            if (selected != null && selected.Enabled)
            {
                switch (selected.Name)
                {
                    case "New":
                        return MenuEvent.Editor_NewMap;

                    case "Load":
                        return MenuEvent.Editor_LoadMap;

                    case "Back":
                        return MenuEvent.ShowMain;
                }
            }
            return MenuEvent.Idle;
        }

        protected override void CreateButtons()
        {
            Text t_new = new Text("Create new map", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_load = new Text("Load map", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_back = new Text("Back", Settings.Menu.Font, Settings.Menu.FontSize);

            TextButton b_new = new TextButton(t_new, "New", true);
            TextButton b_load = new TextButton(t_load, "Load", true);
            TextButton b_back = new TextButton(t_back, "Back", true);

            buttons = new TextButton[4];

            buttons[0] = b_new;
            buttons[1] = b_load;
            buttons[2] = new TextButton(new Text(), "blank", false);
            buttons[3] = b_back;
            
            foreach (TextButton b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }
    }
}