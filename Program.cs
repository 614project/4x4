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
            async Task SaveFile(string path, byte[] file)
            {
                if (!File.Exists(path))
                {
                    await File.WriteAllBytesAsync(path, file);
                }
            }
            async void LoadSource(string path)
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
                await SaveFile("dll\\SDL2.dll", _4x4.Properties.Resources.SDL2);
                await SaveFile("dll\\SDL2_image.dll", _4x4.Properties.Resources.SDL2_image);
                await SaveFile("dll\\SDL2_ttf.dll", _4x4.Properties.Resources.SDL2_ttf);
                await SaveFile("libfreetype-6.dll", _4x4.Properties.Resources.libfreetype_6);
                await SaveFile("libjpeg-9.dll", _4x4.Properties.Resources.libjpeg_9);
                await SaveFile("libpng16-16.dll", _4x4.Properties.Resources.libpng16_16);
                await SaveFile("libtiff-5.dll", _4x4.Properties.Resources.libtiff_5);
                await SaveFile("libwebp-7.dll", _4x4.Properties.Resources.libwebp_7);
                await SaveFile("zlib1.dll", _4x4.Properties.Resources.zlib1);
                await SaveFile(path+"grid.png", _4x4.Properties.Resources.grid);
                await SaveFile(path + "font.ttf", _4x4.Properties.Resources.SUIT);
                await SaveFile(path + "circle.png", _4x4.Properties.Resources.circle);
                await SaveFile(path + "maincircle.png", _4x4.Properties.Resources.maincircle);
            }
            LoadSource("resource");
            Framework.Init("4x4", 1200, 720, null, null, new(true, false, false, false, true), new(true, false, false, true));
            Window.Icon("resource/maincircle.png");
            Display.AddScene(mainScene = new GameScene.GameScene());
            //Display.AddScene(menuScene = new MainMenu.MainScene());
            Display.AddScene(overScene = new GameOver.OverScene());
            Framework.function = new FrameworkOption();
            Framework.BackgroundColor = new(50, 50, 50,255);
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
        }
    }
}

