using SFML.Graphics;

namespace Raycasting
{
    class TextButton
    {
        public Text Text { get; }

        public string Name { get; }

        public bool Enabled { get; set; }

        public TextButton(Text text, string name, bool enabled)
        {
            Text = text;
            Name = name;
            Enabled = enabled;
        }
    }
}