using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace Raycasting
{
    class Menu
    {
        Vector position = new Vector(0, 0);
        Vector anchor = new Vector(0, 0);

        public Vector Anchor { get => anchor; set => UpdatePages(value, position); }

        public Vector Position { get => position; set => UpdatePages(anchor, value); }

        public MenuPages CurrentPage { get; private set; } = MenuPages.Main;

        public Dictionary<MenuPages, MenuPage> Pages { get; set; } = new Dictionary<MenuPages, MenuPage>();

        public Menu(RenderWindow win)
        {
            Pages[MenuPages.Main] = new MainPage(win);
            Pages[MenuPages.Settings] = new SettingsPage(win);
            Pages[MenuPages.Editor] = new EditorPage(win);
        }

        public MenuEvent ProcessMouseButtonClick(MouseButtonEventArgs e)
        {
            MenuEvent menuEvent = Pages[CurrentPage].OnMouseClick(e);
            switch (menuEvent)
            {
                case MenuEvent.ShowSettings:
                    CurrentPage = MenuPages.Settings;
                    return MenuEvent.ShowSettings;

                case MenuEvent.ShowMain:
                    CurrentPage = MenuPages.Main;
                    return MenuEvent.ShowMain;

                case MenuEvent.ShowEditor:
                    CurrentPage = MenuPages.Editor;
                    return MenuEvent.ShowEditor;                    

                default:
                    return menuEvent;
            }
        }

        public float GetCurrentPageHeight()
        {
            return Pages[CurrentPage].GetHeight();
        }

        public void ReturnToMainPage()
        {
            CurrentPage = MenuPages.Main;
        }

        public void UpdateWindow(RenderWindow win)
        {
            foreach (MenuPage p in Pages.Values)
            {
                p.UpdateWindow(win);
            }
        }

        public void Draw()
        {
            Pages[CurrentPage].Draw();
        }

        void UpdatePages(Vector anchor, Vector position)
        {
            this.anchor = anchor;
            this.position = position;

            foreach (MenuPage p in Pages.Values)
            {
                p.Anchor = anchor;
                p.Position = position;
            }
        }
    }
}