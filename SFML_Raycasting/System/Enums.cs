namespace Raycasting
{
    enum QualitySettings
    {
        Low = 8, Medium = 4, High = 1
    }

    enum Direction
    {
        Forward, Right, Backward, Left, None
    }

    enum MenuEvent
    {
        Idle, NewGame, Continue, ShowSettings, ShowMain, Quit
    }

    enum MenuPages
    {
        Main, Settings
    }
}
