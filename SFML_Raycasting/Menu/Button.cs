using SFML.Graphics;

class Button
{
    Text text;
    string name;
    bool enabled;

    public Text Text
    {
        get
        {
            return text;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public bool Enabled
    {
        get
        {
            return enabled;
        }

        set
        {
            enabled = value;
        }
    }

    public Button(Text text, string name, bool enabled)
    {
        this.text = text;
        this.name = name;
        this.enabled = enabled;
    }
}