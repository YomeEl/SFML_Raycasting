using SFML.Window;
using SFML.Graphics;

namespace Raycasting
{
    class EditorPage : MenuPage
    {
        public EditorPage(RenderWindow win) : base(win) { }

        public override MenuEvent OnMouseClick(MouseButtonEventArgs e)
        {
            Button selected = GetSelectedButton();

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

            Button b_new = new Button(t_new, "New", true);
            Button b_load = new Button(t_load, "Load", true);
            Button b_back = new Button(t_back, "Back", true);

            buttons = new Button[4];

            buttons[0] = b_new;
            buttons[1] = b_load;
            buttons[2] = new Button(new Text(), "blank", false);
            buttons[3] = b_back;
            
            foreach (Button b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }
    }
}