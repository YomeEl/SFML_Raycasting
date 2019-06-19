using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Raycasting
{
    class Program
    {
        static Game game;
        static Menu menu;
        static Direction[] dir = { Direction.None, Direction.None, Direction.None, Direction.None };
        static RenderWindow win;

        static Texture background;

        static bool fullscreen = false;
        static bool requestScreenModeChange = false;
        static bool showMenu = true;
        static bool requestMenuModeChange = false;

        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    dir[0] = Direction.Forward;
                    break;

                case Keyboard.Key.D:
                    dir[1] = Direction.Right;
                    break;

                case Keyboard.Key.S:
                    dir[2] = Direction.Backward;
                    break;

                case Keyboard.Key.A:
                    dir[3] = Direction.Left;
                    break;

                case Keyboard.Key.F:
                    requestScreenModeChange = true;
                    break;

                case Keyboard.Key.Escape:
                    if (menu.CurrentPage == MenuPages.Main)
                    {
                        requestMenuModeChange = true;
                    }
                    else
                    {
                        menu.ReturnToMainPage();
                        RecalculateMenuAnchor();
                    }
                    break;
            }
        }

        static void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    dir[0] = Direction.None;
                    break;

                case Keyboard.Key.D:
                    dir[1] = Direction.None;
                    break;

                case Keyboard.Key.S:
                    dir[2] = Direction.None;
                    break;

                case Keyboard.Key.A:
                    dir[3] = Direction.None;
                    break;
            }
        }

        static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (showMenu)
            {
                switch (menu.OnMouseClick(e))
                {
                    case MenuEvent.NewGame:
                        requestMenuModeChange = true;
                        break;

                    case MenuEvent.ShowMain:
                        RecalculateMenuAnchor();
                        break;

                    case MenuEvent.ShowSettings:
                        RecalculateMenuAnchor();
                        break;

                    case MenuEvent.Quit:
                        win.Close();
                        break;
                }
            }
            else
            {
                game.Shoot();
            }
        }

        static void CreateWindow()
        {
            if (win != null)
            {
                win.Closed -= OnClose;
                win.KeyPressed -= OnKeyDown;
                win.KeyReleased -= OnKeyUp;
                win.MouseButtonPressed -= OnMouseButtonPressed;

                win.Dispose();
                win = null;
            }

            VideoMode vm = new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
            win = new RenderWindow(fullscreen ? vm : new VideoMode(800, 600), "Raycating game", Styles.None);

            Settings.Drawing.WallHeight = win.Size.Y;

            win.Closed += OnClose;
            win.KeyPressed += OnKeyDown;
            win.KeyReleased += OnKeyUp;
            win.MouseButtonPressed += OnMouseButtonPressed;

            win.SetFramerateLimit(60);
            win.SetMouseCursorVisible(showMenu);
        }

        static void RecalculateMenuAnchor()
        {
            menu.Anchor = new Vector(0, menu.GetCurrentPageHeight());
        }

        static void Main(string[] args)
        {
            Textures.Load();

            CreateWindow();

            game = new Game(win);
            menu = new Menu(win);
            RecalculateMenuAnchor();
            menu.Position = new Vector(win.Size.X * 0.07f, win.Size.Y * 0.9f);

            game.Draw();
            background = new Texture(win.Size.X, win.Size.Y);
            background.Update(win);
            RectangleShape bgRect = new RectangleShape(new Vector2f(win.Size.X, win.Size.Y))
            {
                Position = new Vector2f(0, 0),
                TextureRect = new IntRect(0, 0, (int)background.Size.X, (int)background.Size.Y),
                Texture = background
            };

            var clock = new Clock();

            int mouseX = Mouse.GetPosition().X;
            while (win.IsOpen)
            {
                win.DispatchEvents();

                if (win.HasFocus())
                {
                    if (!showMenu)
                    {
                        game.RotatePlayer((Mouse.GetPosition().X - mouseX) * (float)(Math.PI / 360));

                        var center = win.GetView().Center;
                        Mouse.SetPosition(new Vector2i((int)center.X, (int)center.Y), win);
                        mouseX = Mouse.GetPosition().X;

                        foreach (Direction d in dir)
                        {
                            if (d != Direction.None)
                            {
                                game.MovePlayer(d, clock.ElapsedTime.AsMilliseconds());
                            }
                        }
                        clock.Restart();

                        game.Draw();
                    }
                    else
                    {
                        win.Draw(bgRect);
                        menu.Draw();
                    }

                    win.Display();

                    if (requestScreenModeChange)
                    {
                        fullscreen = !fullscreen;
                        CreateWindow();
                        game.UpdateWindow(win);
                        menu.UpdateWindow(win);
                        menu.Position = new Vector(win.Size.X * 0.1f, win.Size.Y * 0.9f);
                        bgRect.Size = new Vector2f(win.Size.X, win.Size.Y);
                        requestScreenModeChange = false;
                    }
                    
                    if (requestMenuModeChange)
                    {
                        showMenu = !showMenu;
                        win.SetMouseCursorVisible(showMenu);
                        if (showMenu)
                        {
                            background.Dispose();
                            background = new Texture(win.Size.X, win.Size.Y);
                            background.Update(win);
                            bgRect.Texture = background;
                            bgRect.TextureRect = new IntRect(0, 0, (int)background.Size.X, (int)background.Size.Y);
                            Vector2i center = new Vector2i((int)win.GetView().Center.X, (int)win.GetView().Center.Y);
                            Mouse.SetPosition(center, win);
                        }
                        else
                        {
                            mouseX = Mouse.GetPosition().X;
                            clock.Restart();
                        }
                        requestMenuModeChange = false;
                    }
                }
            }
        }
    }
}