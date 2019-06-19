using SFML.Graphics;

namespace Raycasting
{
    class Button
    {
        public Text Text { get; }

        public string Name { get; }

        public bool Enabled { get; set; }

        public Button(Text text, string name, bool enabled)
        {
            this.Text = text;
            this.Name = name;
            this.Enabled = enabled;
        }
    }
}