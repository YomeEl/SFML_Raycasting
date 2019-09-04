namespace Raycasting
{
    enum QualitySettings
    {
        Low = 8, Medium = 4, High = 1
    }

    enum Direction
    {
        Forward, Right, Back, Left, None
    }

    enum MenuEvent
    {
        Idle, NewGame, Continue, ShowSettings, ShowMain, ShowEditor, Editor_NewMap, Editor_LoadMap, Quit
    }

    enum MenuPages
    {
        Main, Settings, Editor
    }

    enum EditorAction
    {
        Move, PlaceEnemy, SetPlayerStartingPositon, PlaceWall
    }
}
