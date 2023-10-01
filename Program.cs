using JyunrcaeaFramework;

namespace Jyunrcaea
{
    class Program
    {
        public const string fontdir = "resource/font.ttf";

        public static GameScene.GameScene mainScene = null!;

        public static GameOver.OverScene overScene = null!;

        //public static MainMenu.MainScene menuScene = null!;

        static void Main()
        {
            void SaveFile(string path, byte[] file)
            {
                if (File.Exists(path)) return;
                File.WriteAllBytes(path, file);
            }
            void LoadSource(string path)
            {
                path += "\\";
                if (!Directory.Exists("resource"))
                {
                    Directory.CreateDirectory("resource");
                }
                if (!Directory.Exists("dll"))
                {
                    Directory.CreateDirectory("dll");
                }
                SaveFile("dll\\SDL2.dll", _4x4.Properties.Resources.SDL2);
                SaveFile("dll\\SDL2_image.dll", _4x4.Properties.Resources.SDL2_image);
                SaveFile("dll\\SDL2_ttf.dll", _4x4.Properties.Resources.SDL2_ttf);
                SaveFile("dll\\SDL2_mixer.dll", _4x4.Properties.Resources.SDL2_mixer);
                SaveFile("libfreetype-6.dll", _4x4.Properties.Resources.libfreetype_6);
                SaveFile("libjpeg-9.dll", _4x4.Properties.Resources.libjpeg_9);
                SaveFile("libpng16-16.dll", _4x4.Properties.Resources.libpng16_16);
                SaveFile("libtiff-5.dll", _4x4.Properties.Resources.libtiff_5);
                SaveFile("libwebp-7.dll", _4x4.Properties.Resources.libwebp_7);
                SaveFile("zlib1.dll", _4x4.Properties.Resources.zlib1);
                SaveFile(path+"grid.png", _4x4.Properties.Resources.grid);
                SaveFile(path + "font.ttf", _4x4.Properties.Resources.SUIT);
                SaveFile(path + "circle.png", _4x4.Properties.Resources.circle);
                SaveFile(path + "maincircle.png", _4x4.Properties.Resources.maincircle);
            }
            LoadSource("resource");
            Framework.Init("4x4", 1200, 720);
            Window.Icon("resource/maincircle.png");
            Display.AddScene(mainScene = new GameScene.GameScene());
            //Display.AddScene(menuScene = new MainMenu.MainScene());
            Display.AddScene(overScene = new GameOver.OverScene());
            Framework.Function = new FrameworkOption();
            Window.BackgroundColor = new(50, 50, 50,255);
            Display.FrameLateLimit = 0; //모니터 주사율
            Display.FrameLateLimit *= 2 ; //그에 2배
            Framework.Run();
        }
    }

    class FrameworkOption : FrameworkFunction
    {
        public override void WindowQuit()
        {
            base.WindowQuit();
            Framework.Stop();
        }

        public override void Start()
        {
            base.Start();
            Window.Show = true;
            GameScene.GameScene.circle.Opacity(255, 300f, 300f);
        }
    }
}

