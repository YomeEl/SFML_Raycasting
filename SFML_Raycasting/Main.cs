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
        static RenderWindow app;

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
                    requestMenuModeChange = true;
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

        static void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (menu.MouseClick(e))
            {
                case MenuEvent.NewGame:
                    requestMenuModeChange = true;
                    break;
            }
        }

        static void CreateWindow()
        {
            if (app != null)
            {
                app.Closed -= OnClose;
                app.KeyPressed -= OnKeyDown;
                app.KeyReleased -= OnKeyUp;
                app.MouseButtonPressed -= onMouseButtonPressed;

                app.Dispose();
                app = null;
            }

            VideoMode vm = new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
            app = new RenderWindow(fullscreen ? vm : new VideoMode(800, 600), "Raycating game", Styles.None);

            Settings.Drawing.WallHeight = app.Size.Y;

            app.Closed += OnClose;
            app.KeyPressed += OnKeyDown;
            app.KeyReleased += OnKeyUp;
            app.MouseButtonPressed += onMouseButtonPressed;

            app.SetFramerateLimit(60);
            app.SetMouseCursorVisible(showMenu);
        }

        static void Main(string[] args)
        {
            Textures.Load();

            CreateWindow();

            game = new Game(app);
            menu = new Menu(app);
            menu.Anchor = new Vector(0, menu.GetHeight());
            menu.Position = new Vector(app.Size.X * 0.07f, app.Size.Y * 0.9f);

            game.Draw();
            background = new Texture(app.Size.X, app.Size.Y);
            background.Update(app);
            RectangleShape bgRect = new RectangleShape(new Vector2f(app.Size.X, app.Size.Y));
            bgRect.Position = new Vector2f(0, 0);
            bgRect.TextureRect = new IntRect(0, 0, (int)background.Size.X, (int)background.Size.Y);
            bgRect.Texture = background;

            var clock = new Clock();

            int mouseX = Mouse.GetPosition().X;
            while (app.IsOpen)
            {
                app.DispatchEvents();

                if (app.HasFocus())
                {
                    if (!showMenu)
                    {
                        game.RotatePlayer((Mouse.GetPosition().X - mouseX) * (float)(Math.PI / 360));

                        var center = app.GetView().Center;
                        Mouse.SetPosition(new Vector2i((int)center.X, (int)center.Y), app);
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
                        app.Draw(bgRect);
                        menu.Draw();
                    }

                    app.Display();

                    if (requestScreenModeChange)
                    {
                        fullscreen = !fullscreen;
                        CreateWindow();
                        game.UpdateApp(app);
                        menu.UpdateApp(app);
                        menu.Anchor = new Vector(0, menu.GetHeight());
                        menu.Position = new Vector(app.Size.X * 0.1f, app.Size.Y * 0.9f);
                        bgRect.Size = new Vector2f(app.Size.X, app.Size.Y);
                        requestScreenModeChange = false;
                    }
                    
                    if (requestMenuModeChange)
                    {
                        showMenu = !showMenu;
                        app.SetMouseCursorVisible(showMenu);
                        if (showMenu)
                        {
                            background.Dispose();
                            background = new Texture(app.Size.X, app.Size.Y);
                            background.Update(app);
                            bgRect.Texture = background;
                            bgRect.TextureRect = new IntRect(0, 0, (int)background.Size.X, (int)background.Size.Y);
                            Vector2i center = new Vector2i((int)app.GetView().Center.X, (int)app.GetView().Center.Y);
                            Mouse.SetPosition(center, app);
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