using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Raycasting
{
    abstract class MenuPage
    {
        RenderWindow win;

        protected TextButton[] buttons;

        Vector position = new Vector(0, 0);
        Vector anchor = new Vector(0, 0);

        public bool Visible { get; set; } = true;

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

        bool IsMouseHovering(TextButton b)
        {
            Vector2i mousePos = Mouse.GetPosition(win);
            FloatRect bounds = new FloatRect(b.Text.Position.X, b.Text.Position.Y, b.Text.GetLocalBounds().Width, Settings.Menu.FontSize - 6);
            bounds.Height += Settings.Menu.FontSize / 4;

            return Visible && bounds.Contains(mousePos.X, mousePos.Y);
        }

        void SetTextColor(TextButton b)
        {
            b.Text.FillColor = b.Enabled ? IsMouseHovering(b) ? Settings.Menu.SelectedColor : Settings.Menu.NotSelectedColor : Settings.Menu.DisabledColor;
        }

        protected abstract void CreateButtons();

        protected TextButton GetSelectedButton()
        {
            TextButton selected = null;

            foreach (TextButton b in buttons)
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
            const uint gap = Settings.Menu.Gap;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text.Position = new Vector2f(offset.X, offset.Y + i * (fontHeight + gap));
            }
        }

        public uint GetHeight()
        {
            return (Settings.Menu.FontSize + Settings.Menu.Gap) * (uint)buttons.Length - Settings.Menu.Gap;
        }

        public void Draw()
        {
            foreach (TextButton b in buttons)
            {
                SetTextColor(b);
                win.Draw(b.Text);
            }
        }
    }
}