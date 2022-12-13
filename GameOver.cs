using JyunrcaeaFramework;
using System.Diagnostics;

namespace GameOver
{
    class OverScene : Scene
    {
        public static OverText ot = null!;

        public OverScene()
        {
            this.Hide = true;
            //this.AddSprite(new BackgroundRectangle());
            this.AddSprite(ot = new OverText());
            this.AddSprite(new RetryText());
        }
    }

    class BackgroundRectangle : Rectangle {
        public BackgroundRectangle()
        {
            this.Color = new(255, 255, 255, 50);
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public override void Resize()
        {
            this.UWidth = Window.UWidth;
            this.UHeight = Window.UHeight;
            base.Resize();
        }
    }

    class RetryText : TextBox
    {
        public RetryText() : base("resource\\font.ttf", 0, "다시 하실려면 스페이스 바를 눌러주세요!")
        {
            
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public override void Resize()
        {
            this.Y = OverScene.ot.Size;
            this.Size = (int)(Window.UHeight * 0.03f);
            base.Resize();
        }
    }

    class OverText : TextBox
    {
        public OverText() :base("resource\\font.ttf",0,"Game Over")
        {

        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public override void Resize()
        {
            this.Size = (int)(Window.UHeight * 0.1f);
            base.Resize();
        }
    }
}