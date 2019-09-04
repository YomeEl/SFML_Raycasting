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
        static Editor editor;
        static Direction[] dir = { Direction.None, Direction.None, Direction.None, Direction.None };
        static RenderWindow win;

        static Texture background;

        static bool fullscreen = false;
        static bool requestScreenModeChange = false;
        static bool requestMenuModeChange = false;
        static bool requestEditor = false;
        static bool showEditor = false;
        static bool played = false;

        static Vector previousMousePos = new Vector();

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
                    dir[2] = Direction.Back;
                    break;

                case Keyboard.Key.A:
                    dir[3] = Direction.Left;
                    break;

                case Keyboard.Key.F:
                    requestScreenModeChange = true;
                    break;

                case Keyboard.Key.Escape:
                    if (showEditor)
                    {
                        showEditor = false;
                    }
                    else
                    {
                        if (menu.CurrentPage == MenuPages.Main)
                        {
                            if (played)
                            {
                                requestMenuModeChange = true;
                            }
                            else
                            {
                                win.Close();
                            }
                        }
                        else
                        {
                            menu.ReturnToMainPage();
                            RecalculateMenuAnchor();
                        }
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
            if (e.Button == Mouse.Button.Left)
            {
                previousMousePos = new Vector(e.X, e.Y);

                if (menu.Visible && !showEditor)
                {
                    switch (menu.ProcessMouseButtonClick(e))
                    {
                        case MenuEvent.NewGame:
                            requestMenuModeChange = true;
                            played = true;
                            break;

                        case MenuEvent.Editor_NewMap:
                            Console.WriteLine("New map event");
                            Map m = new Map();
                            Serializer.Deserialize(out m, "Maps/map1.dat");
                            editor.Open(m);
                            requestEditor = true;
                            break;

                        case MenuEvent.Idle:
                            break;

                        case MenuEvent.Quit:
                            win.Close();
                            break;

                        default:
                            RecalculateMenuAnchor();
                            break;
                    }
                }
                else
                {
                    game.Shoot();
                }
            }
        }

        static void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            if (showEditor)
            {
                editor.Scale *= (float)Math.Pow(1.5, e.Delta);
            }
        }

        static void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            var currentMousePos = new Vector(e.X, e.Y);
            if (showEditor)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    var mouseDelta = currentMousePos - previousMousePos;
                    editor.CameraPos = editor.CameraPos - mouseDelta * (1.9f / editor.Scale / 2);
                }
            }
            previousMousePos = currentMousePos;
        }

        static void CreateWindow()
        {
            if (win != null)
            {
                win.Closed -= OnClose;
                win.KeyPressed -= OnKeyDown;
                win.KeyReleased -= OnKeyUp;
                win.MouseButtonPressed -= OnMouseButtonPressed;
                win.MouseWheelScrolled -= OnMouseWheelScrolled;
                win.MouseMoved -= OnMouseMoved;

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
            win.MouseWheelScrolled += OnMouseWheelScrolled;
            win.MouseMoved += OnMouseMoved;

            win.SetFramerateLimit(60);
            if (menu != null)
            {
                win.SetMouseCursorVisible(menu.Visible);
            }
            else
            {
                win.SetMouseCursorVisible(true);
            }
        }

        static void RecalculateMenuAnchor()
        {
            menu.Anchor = new Vector(0, menu.GetCurrentPageHeight());
        }

        static void Main()
        {
            Textures.Load();

            CreateWindow();
            game = new Game(win);
            menu = new Menu(win);
            editor = new Editor(win);
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
                    if (!showEditor)
                    {
                        if (!menu.Visible)
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
                    }
                    else
                    {
                        foreach (Direction d in dir)
                        {
                            float moveSpeed = 1.9f / editor.Scale;
                            switch (d)
                            {
                                case Direction.Forward:
                                    editor.CameraPos -= new Vector(0, clock.ElapsedTime.AsMilliseconds()) * moveSpeed;
                                    break;

                                case Direction.Right:
                                    editor.CameraPos += new Vector(clock.ElapsedTime.AsMilliseconds(), 0) * moveSpeed;
                                    break;

                                case Direction.Back:
                                    editor.CameraPos += new Vector(0, clock.ElapsedTime.AsMilliseconds()) * moveSpeed;
                                    break;

                                case Direction.Left:
                                    editor.CameraPos -= new Vector(clock.ElapsedTime.AsMilliseconds(), 0) * moveSpeed;
                                    break;
                            }
                        }

                        clock.Restart();
                        editor.Draw();
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
                        menu.ToggleVisibility();
                        win.SetMouseCursorVisible(menu.Visible);
                        if (menu.Visible)
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

                    if (requestEditor)
                    {
                        showEditor = true;
                        requestEditor = false;
                    }
                }
            }
        }
    }
}