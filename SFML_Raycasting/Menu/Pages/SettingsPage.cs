using SFML.Window;
using SFML.Graphics;

namespace Raycasting
{
    class SettingsPage : MenuPage
    {
        public SettingsPage(RenderWindow win) : base(win) { }

        public override MenuEvent OnMouseClick(MouseButtonEventArgs e)
        {
            TextButton selected = GetSelectedButton();

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
            Text t_message = new Text("Comming soon!", Settings.Menu.Font, Settings.Menu.FontSize);
            Text t_back = new Text("Back", Settings.Menu.Font, Settings.Menu.FontSize);

            TextButton b_message = new TextButton(t_message, "Comming soon!", false);
            TextButton b_back = new TextButton(t_back, "Back", true);

            buttons = new TextButton[3];

            buttons[0] = b_message;
            buttons[1] = new TextButton(new Text(), "blank", false);
            buttons[2] = b_back;

            foreach (TextButton b in buttons)
            {
                b.Text.OutlineThickness = 1;
                b.Text.OutlineColor = Color.Black;
            }
        }
    }
}