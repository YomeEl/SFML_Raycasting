using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Raycasting
{
    abstract class MenuPage
    {
        RenderWindow win;

        protected Button[] buttons;

        Vector position = new Vector(0, 0);
        Vector anchor = new Vector(0, 0);

        public Vector Anchor
        {
            get => anchor;
            set
            {
                anchor = value;
                UpdateButtons();
            }
        }

        public Vector Position
        {
            get => position;
            set
            {
                position = value;
                UpdateButtons();
            }
        }

        public MenuPage(RenderWindow win)
        {
            UpdateWindow(win);
            CreateButtons();
            UpdateButtons();
        }

        public void UpdateWindow(RenderWindow win)
        {
            this.win = win;
        }

        public abstract MenuEvent OnMouseClick(MouseButtonEventArgs e);

        bool IsMouseHovering(Button b)
        {
            Vector2i mousePos = Mouse.GetPosition(win);
            FloatRect bounds = new FloatRect(b.Text.Position.X, b.Text.Position.Y, b.Text.GetLocalBounds().Width, Settings.Menu.FontSize - 6);
            bounds.Height += Settings.Menu.FontSize / 4;

            return bounds.Contains(mousePos.X, mousePos.Y);
        }

        void SetTextColor(Button b)
        {
            b.Text.FillColor = b.Enabled ? IsMouseHovering(b) ? Settings.Menu.SelectedColor : Settings.Menu.NotSelectedColor : Settings.Menu.DisabledColor;
        }

        protected abstract void CreateButtons();

        protected Button GetSelectedButton()
        {
            Button selected = null;

            foreach (Button b in buttons)
            {
                if (IsMouseHovering(b))
                {
                    selected = b;
                }
            }

            return selected;
        }

        void UpdateButtons()
        {
            Vector offset = position - anchor;
            const uint fontHeight = Settings.Menu.FontSize;
            const uint gap = Settings.Menu.MenuGap;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text.Position = new Vector2f(offset.X, offset.Y + i * (fontHeight + gap));
            }
        }

        public uint GetHeight()
        {
            return (Settings.Menu.FontSize + Settings.Menu.MenuGap) * (uint)buttons.Length - Settings.Menu.MenuGap;
        }

        public void Draw()
        {
            foreach (Button b in buttons)
            {
                SetTextColor(b);
                win.Draw(b.Text);
            }
        }
    }
}