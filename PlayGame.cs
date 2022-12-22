using Jyunrcaea;
using JyunrcaeaFramework;

namespace GameScene
{
    class GameScene : Scene
    {
        public static int hp = 3;

        public static Grid grid = null!;

        public static Circle circle = null!;

        public static HealthText healthText = null!;

        public static ScoreTime ScoreTime = null!;

        public static StartText StartText = null!;

        public static int[] lengthlist = new int[4];

        public static TextureFromSharedTexture enemytexture = null!;

        public GameScene() {
            enemytexture = TextureSharing.Add("resource/circle.png");
            AddSprites(
                grid = new(),
                circle = new(),
     //           new TestCircle(),
                healthText=  new HealthText(),
                ScoreTime = new ScoreTime(),
                new FPS(),
                StartText = new()
            );
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public float showtime = 0;

        public override void Update(float ms)
        {
            base.Update(ms);
            if (hp == 0 || showtime == 0) return;
            if (Framework.RunningTime >= showtime)
            {
                this.AddSprite(new EnemyCircle());
                this.AddSprite(new EnemyCircle());
                //this.AddSprite(new EnemyCircle());
                showtime += 800f;
            }
        }

        public override void Resize()
        {
            grid.Resize();
            int ll = (int)(grid.Width * 0.245f),i;
            float n = -1.5f;
            for(i = 0; i < 4; i++)
            {
                lengthlist[i] = (int)(ll * n);
                n+=1.0f;
            }
            base.Resize();
        }
    }

    class FPS : TextBox, UpdateEventInterface
    {
        public FPS() : base(Program.fontdir, 0)
        {
            this.DrawX = HorizontalPositionType.Right;
            this.DrawY = VerticalPositionType.Bottom;
            this.OriginX = HorizontalPositionType.Left;
            this.OriginY = VerticalPositionType.Top;
        }

        public override void Start()
        {
            base.Start();
            Resize();
            endtime = Framework.RunningTime + 1000f;
        }

        public override void Resize()
        {
            this.Size = (int)(Window.UHeight * 0.03f);
            this.Y = this.Size;
            base.Resize();
        }

        int fps = 0;

        float endtime = 0;

        public void Update(float ms)
        {
            if (Framework.RunningTime > endtime)
            {
                this.Text = $"FPS: {fps}/{Display.MonitorRefreshRate}";
                fps = 0;
                endtime += 1000f;
            }
            fps++;
        }
    }

    class TestCircle : Sprite , MouseMoveInterface
    {
        //public TestCircle() : base("resource/circle.png") {
        public TestCircle() : base(new TextureFromFile("resource/circle.png")) { 
            this.OriginX = HorizontalPositionType.Left;
            this.OriginY = VerticalPositionType.Top;
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public override void Resize()
        {
            this.Size = Window.Height * 0.0005f;
            base.Resize();
        }

        public void MouseMove()
        {
            this.X = Mouse.X;
            this.Y = Mouse.Y;
            if (Convenience.Distance(this, GameScene.circle) <= this.Width) Console.WriteLine("Over");
        }
    }

    class StartText : TextBox
    {
        public StartText() : base("resource/font.ttf", 0,"시작할려면 스페이스 바를 눌러주세요!") {
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public override void Resize()
        {
            this.Size = (int)(Window.UHeight * 0.05f);
            base.Resize();
        }
    }

    class Grid : SpriteForAnimation
    {

        public Grid() : base(new TextureFromFile("resource/grid.png"))
        {
            this.CompleteFunction = cpt;
        }

        public override void Resize()
        {
            this.Size = Window.UHeight * 0.0009f;
            base.Resize();
        }

        void cpt()
        {
            if (this.X != 0 || this.Y != 0)
            {
                Move(0, 0, 50);
            }
        }
    }

    class ScoreTime : TextBox, UpdateEventInterface
    {
        public ScoreTime() : base("resource/font.ttf", 0, "점수: 0")
        {
            this.DrawX = HorizontalPositionType.Right;
            this.DrawY = VerticalPositionType.Bottom;
            this.OriginX = HorizontalPositionType.Left;
            this.OriginY = VerticalPositionType.Top;
            this.Hide = true;
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public float score = 0;

        public void Update(float ms)
        {
            if (GameScene.hp == 0) return;
            score += ms;
            this.Text = "점수: " + score .ToString();
        }

        public override void Resize()
        {
            this.Size = (int)(Window.UHeight * 0.03f);
            base.Resize();
        }
    }

    class HealthText : TextBox
    {
    
        public HealthText() : base("resource/font.ttf",0,"남은 체력: 3")
        {
            this.OriginY = VerticalPositionType.Bottom;
            this.DrawY = VerticalPositionType.Top;
            this.Y = -5;
            this.FontColor = new();
            //this.BackgroundColor = new(0,0,0);  
        }

        public override void Start()
        {
            base.Start();
            Resize();
        }

        public override void Resize()
        {
            this.Size = (int)(Window.UHeight * 0.03f);
            base.Resize();
        }

        public int HP
        {
            get => GameScene.hp;
            set
            {
                if (value < 0) return;
                GameScene.hp = value;
                this.Text = "남은 체력: " + GameScene.hp.ToString();
                if (value <= 0)
                {
                    Program.overScene.Hide = false;
                }
            }
        }
    }

    class EnemyCircle : SpriteForAnimation
    {
        bool tr;

        public EnemyCircle()
            //: base(new TextureFromFile("resource/circle.png"))
            : base(GameScene.enemytexture)
        {
            this.CompleteFunction = End;
            Random random = new Random();
            if(random.Next(2) == 0 )
            {
                this.Move((int)(Window.UWidth * ((tr = random.Next(2) == 0)? -0.6f : 0.6f)), GameScene.lengthlist[random.Next(4)],0);
                this.Move((int)(Window.UWidth * (tr ? 0.6f : -0.6f)), this.Y, 2000, 100);
            } else
            {
                this.Move(GameScene.lengthlist[ random.Next(4)],(int)(Window.UHeight * ((tr = random.Next(2) == 0) ? -0.6f : 0.6f)),  0);
                this.Move(this.X,(int)(Window.UHeight * (tr ? 0.6f : -0.6f)),  2000, 100);
            }
        }

        bool success_attack = false;

        public override void Start()
        {
            base.Start();
            Resize();

            //if (tr)
            //{
            //    this.Move()
            //}
            //this.Move(0, (int)(Window.Height * 0.5f), 1000, 100);
        }

        void End()
        {
            Program.mainScene.RemoveSprite(this);
        }

        public override void Resize()
        {
            this.Size = Window.UHeight * 0.0005f;
            base.Resize();
        }

        public override void Update(float ms)
        {
            base.Update(ms);
            if (success_attack) return;
            //if (Convenience.Overlap(this,GameScene.circle))
            if(Convenience.Distance(this,GameScene.circle) <= Width)
            {
                GameScene.healthText.HP--;
                this.success_attack = true;
            }
        }
    }

    class Circle : SpriteForAnimation, KeyDownInterface
    {
        int x =0, y = 0;

        public Circle() : base(new TextureFromFile("resource/maincircle.png")) { 
            
        }

        public override void Resize()
        {
            this.Size = Window.UHeight * 0.0005f;
            Moving();
            base.Resize();
        }

        void Moving()
        {
            this.Move(GameScene.lengthlist[x], GameScene.lengthlist[y], 80);
        }

        public void KeyDown(Keycode e)
        {
            switch (e)
            {
                case Keycode.ESCAPE:
                    Framework.Stop();
                    break;
#if DEBUG
                case Keycode.F1:
                    Framework.ObjectDrawDebuging = !Framework.ObjectDrawDebuging;
                    break;
#endif
                case Keycode.SPACE:
                    if (GameScene.healthText.HP == 0)
                    {
                        GameScene.ScoreTime.score = 0;
                        GameScene.healthText.HP = 3;
                        Program.overScene.Hide = true;
                        Program.mainScene.showtime = Framework.RunningTime;
                        //Program.mainScene.AddSprite(new StartText());
                        return;
                    }
                    if (Program.mainScene.showtime != 0) return;
                    Program.mainScene.RemoveSprite(GameScene.StartText);
                    Program.mainScene.showtime = Framework.RunningTime;
                    GameScene.ScoreTime.Hide = false;
                    break;
                //case Keycode.F3:
                //    Window.FullScreen = !Window.FullScreen;
                //    break;
                case Keycode.NUM_8:
                case Keycode.w:
                case Keycode.UP:
                    if (y == 0) return; y--;
                    GameScene.grid.Move(0, -20, 50);
                    break;
                case Keycode.NUM_4:
                case Keycode.a:
                case Keycode.LEFT:
                    if (x == 0) return; x--;
                    GameScene.grid.Move(20,0, 50);
                    break;
                case Keycode.NUM_6:
                case Keycode.d:
                case Keycode.RIGHT:
                    if (x == 3) return; x++;
                    GameScene.grid.Move(-20,0, 50);
                    break;
                case Keycode.NUM_5:
                case Keycode.NUM_2:
                case Keycode.s:
                case Keycode.DOWN:
                    if (y == 3) return; y++;
                    GameScene.grid.Move(0, 20, 50);
                    break;
                default:
                    return;
            }
            Moving();
        }
    }
}