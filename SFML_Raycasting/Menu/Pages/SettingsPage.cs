using SFML.Window;
using SFML.Graphics;

namespace Raycasting
{
    class SettingsPage : MenuPage
    {
        public SettingsPage(RenderWindow win) : base(win) { }

        public override MenuEvent OnMouseClick(MouseButtonEventArgs e)
        {
            Button selected = GetSelectedButton();

            if (selected != null && selected.Enabled)
            {
                switch (selected.Name)
                {
                    case "Back":
                        return MenuEvent.ShowMain;
                }
            }
            return MenuEvent.Idle;
        }

        protected override void CreateButtons()
        {
            Text t_message = new Text("Comming soon!", Settings.Menu.MenuFont, Settings.Menu.FontSize);
            Text t_back = new Text("Back", Settings.Menu.MenuFont, Settings.Menu.FontSize);

            Button b_message = new Button(t_message, "Comming soon!", false);
            Button b_back = new Button(t_back, "Back", true);

            buttons = new Button[3];

            buttons[0] = b_message;
            buttons[1] = new Button(new Text(), "blank", false);
            buttons[2] = b_back;

            foreach (Button b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }
    }
}