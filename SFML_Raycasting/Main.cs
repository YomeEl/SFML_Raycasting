using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Raycasting
{
    class Program
    {
        static Game game;
        static Direction[] dir = { Direction.None, Direction.None, Direction.None, Direction.None };
        static RenderWindow app;
        static bool fullscreen = false;
        static bool requestScreenModeChange = false;

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

                case Keyboard.Key.Space:
                    if (game != null)
                    {
                        game.toggleTextures();
                    }
                    break;

                case Keyboard.Key.Escape:
                    app.Close();
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

        static void CreateWindow()
        {
            fullscreen = !fullscreen;

            if (app != null)
            {
                app.Closed -= OnClose;
                app.KeyPressed -= OnKeyDown;
                app.KeyReleased -= OnKeyUp;

                app.Dispose();
                app = null;
            }

            VideoMode vm = new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
            app = new RenderWindow(fullscreen ? vm : new VideoMode(800, 600), "Raycating game", Styles.None);

            Settings.Drawing.WallHeight = app.Size.Y;

            app.Closed += OnClose;
            app.KeyPressed += OnKeyDown;
            app.KeyReleased += OnKeyUp;

            app.SetMouseCursorVisible(false);
            app.SetFramerateLimit(60);
            app.RequestFocus();
        }

        static void Main(string[] args)
        {
            Textures.Load();

            CreateWindow();

            game = new Game(app);
            game.toggleTextures();

            var clock = new Clock();

            int mouseX = Mouse.GetPosition().X;
            while (app.IsOpen)
            {
                if (app.HasFocus())
                {
                    app.DispatchEvents();

                    game.RotatePlayer((Mouse.GetPosition().X - mouseX) * (float)(Math.PI / 360));

                    var center = app.GetView().Center;
                    Mouse.SetPosition(new Vector2i((int)center.X + app.Position.Y, (int)center.Y + app.Position.X));
                    mouseX = Mouse.GetPosition().X;

                    foreach (Direction d in dir)
                    {
                        if (d != Direction.None)
                        {
                            game.MovePlayer(d, clock.ElapsedTime.AsMilliseconds());
                        }
                    }
                    clock.Restart();

                    app.Clear(new Color(135, 206, 235));

                    game.Draw();

                    app.Display();

                    if (requestScreenModeChange)
                    {
                        CreateWindow();
                        game.UpdateApp(app);
                        requestScreenModeChange = false;
                    }
                }
            }
        }
    }
}