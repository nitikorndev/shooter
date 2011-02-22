using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace shooter
{
    public class shooter : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        show bg, ch, logo, l, r, f, j, e, eg, cannon, ball, power, arrow,ef,charr,over,boom,wel,level,die;
        int x, y,xx,yy,lv=1,step=0,e1=0,e2=0,at1=0,at2=0,m=0,cha=3,hp=100,exp=0,pi=0;
        float c = 0.0f, cc = 0.0f;
        bool fire = false,start = true,eff = false,boomm = false,enter=false,levell=false,diee=false;
        int [] at_cop = new int[10];
        int[] at_mis = new int[10];
        SpriteFont font;
        Rectangle[] boxr = new Rectangle[11];
        Rectangle[] copr = new Rectangle[10];
        Rectangle[] misr = new Rectangle[10];
        List<show> shows;
        show[] shows2 = new show[10];
        Model ballx;
        public shooter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333);

        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font1");
            ballx = Content.Load<Model>("ballx");
            bg = new show(Content,"bg");
            eg = new show(Content, "eg");
            ch = new show(Content, "character2");
            ch.step = 4;
            ch.Origin = new Vector2(10, 0);
            ch.Position = new Vector2(400, 362);
            logo = new show(Content, "logo");
            logo.Position = new Vector2(200, 50);
            charr = new show(Content, "char");
            charr.Position = new Vector2(570,20);
            boom = new show(Content, "boom");
            level = new show(Content, "level");
            wel = new show(Content, "welcome");
            die = new show(Content, "die");
            l = new show(Content, "l");
            r = new show(Content, "r");
            j = new show(Content, "p");
            e = new show(Content, "e");
            f = new show(Content, "f");
            ef = new show(Content, "ef");
            over = new show(Content, "over");
            ball = new show(Content, "ball2");
            ball.Position = new Vector2(850,0);
            cannon = new show(Content, "cannon");
            cannon.Rotation = 0.45f;
            power = new show(Content, "power");
            arrow = new show(Content, "arrow");
            shows = new List<show>();
             for (int i = 0; i < 10; i++)
            {
                show sh = new show(Content,"cop2");
                Random rx = new Random();
                int x1 = rx.Next(0, 800);
                Random ry = new Random();
                int y1 = ry.Next(0, 300);
                sh.target = new Vector2(x1, y1);
                sh.Display = true;
                sh.Origin= new Vector2(0,0);
                shows.Add(sh);
            }
            for (int i = 0; i < 10; i++)
            {
                shows2[i] = new show(Content, "miss");
            }
          }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (enter)
            {
                character();

                if (fire)
                    firer();

                copter();

                if (boomm)
                    boomr();


                if (levell)
                    levelr();

                if (diee)
                    dier();
            }
            else
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                foreach (TouchLocation tl in touchCollection)
                {
                    x = (int)tl.Position.X;
                    y = (int)tl.Position.Y;
                    if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved))
                    {
                        if ((x > 218 && x < 460) && (y>302&&y<390))
                        {
                            enter = true;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (!enter)
            {
                spriteBatch.Draw(wel.Texture, wel.Position, null, Color.White, wel.Rotation, wel.Origin, 2.0f, wel.eff, 0.0f);
            }
           // spriteBatch.DrawString(font, x + "--" + y, new Vector2(200, 200), Color.White);

            if (enter)
            if (cha > 0)
            {
                spriteBatch.Draw(bg.Texture, bg.Position, null, Color.White, bg.Rotation, bg.Origin, 2.0f, bg.eff, 0.0f);

                for (int i = 0; i < 28; i++)
                    for (int k = 0; k < 2; k++)
                    {
                        eg.Position = new Vector2(i * eg.Texture.Width, 450 + (k * eg.Texture.Height));
                        spriteBatch.Draw(eg.Texture, eg.Position, null, Color.White, eg.Rotation, eg.Origin, 1.0f, eg.eff, 0.0f);
                    }
                int cc = 0;
                if (step == 1)
                {
                    step = 0;
                }
                else
                {
                    step = 1;
                }
                foreach (show sha in shows)
                {
                    if (sha.Display && at_cop[cc] != 1)
                    {
                        //spriteBatch.DrawString(font, "("+sha.Position.X+","+sha.Position.Y+") to "+"("+sha.target.X+","+sha.target.Y+") missi "+m, sha.Position-new Vector2(10,50), Color.White);
                        spriteBatch.Draw(sha.Texture, sha.Position, new Rectangle(step * 100, 0, 100, sha.Texture.Height), Color.White, sha.Rotation, sha.Origin, 1.0f, sha.eff, 0.0f);
                        copr[cc] = new Rectangle((int)sha.Position.X, (int)sha.Position.Y, 100, (int)sha.Texture.Height);
                        if (shows2[cc].Display)
                        {
                            spriteBatch.Draw(shows2[cc].Texture, shows2[cc].Position, null, Color.White, shows2[cc].Rotation, shows2[cc].Origin, 1.0f, shows2[cc].eff, 0.0f);
                            misr[cc] = new Rectangle((int)(shows2[cc].Position.X), (int)(shows2[cc].Position.Y), 50, (int)shows2[cc].Texture.Height);
                        }
                        if (boxr[3].Intersects(copr[cc]) || copr[cc].Intersects(boxr[3]))
                        {
                            ball.Position = ch.Position;
                            ball.Display = false;
                            at1 = (int)sha.Position.X;
                            at2 = (int)sha.Position.Y;
                            eff = true;
                            fire = false;
                            sha.Position = new Vector2(-300, -300);
                            pi += (lv * 5);
                            exp += 10;
                            if (exp >= 100)
                            {
                                lv++;

                                levell = true;
                                level.Display = true;
                                level.Position = ch.Position;

                                if (lv % 5 == 0)
                                {
                                    cha++;
                                }
                                exp = 0;
                            }
                        }

                        if (!(shows2[cc].Display))
                        {
                            Random ry = new Random();
                            int y1 = ry.Next(0, 10);
                            if (y1 == 5)
                            {
                                m = 0;
                                shows2[cc].Position = sha.Position + new Vector2(53, 40);
                                misr[cc].X = (int)sha.Position.X + (int)new Vector2(53, 40).X;
                                misr[cc].Y = (int)sha.Position.Y + (int)new Vector2(53, 40).Y;
                                shows2[cc].Display = true;
                            }
                        }
                        else
                        {
                            m = 1;
                            shows2[cc].Position += new Vector2(1, 1 * 1 + (int)(((float)lv / 2.0)));
                            misr[cc].Y += 1 * 1 + (int)(((float)lv / 2.0));
                            misr[cc].X += 1;

                            if (misr[cc].Y > 480 || misr[cc].Intersects(boxr[4]))
                            {
                                if (misr[cc].Intersects(boxr[4]))
                                {
                                    hp -= 10;
                                    boomm = true;
                                    boom.Display = true;
                                  
                                    if (hp <= 0)
                                    {
                                        cha--;

                                        diee = true;
                                        die.Display = true;
                                        die.Position = ch.Position;

                                        hp = 100;
                                    }
                                    at1 = (int)ch.Position.X;
                                    at2 = (int)ch.Position.Y;
                                    boom.Position = new Vector2(at1,at2);
                                }
                                else
                                {
                                    at1 = misr[cc].X;
                                    at2 = misr[cc].Y;
                                }

                                eff = true;
                                shows2[cc].Position = new Vector2(-1000, -1000);
                                misr[cc].Y = -1000;
                                misr[cc].X = -1000;
                                shows2[cc].Display = false;
                            }

                        }

                    }
                    cc++;
                }

                effect();

                //spriteBatch.Draw(cannon.Texture, cannon.Position, null, Color.White, cannon.Rotation, cannon.Origin, 1.0f, cannon.eff, 0.0f);
                spriteBatch.Draw(ch.Texture, ch.Position, new Rectangle(ch.step * 60, 0, 60, 77), Color.White, ch.Rotation, ch.Origin, 1.0f, ch.eff, 0.0f);
                cannon.Position = ch.Position - new Vector2(155, -5);
                cannon.Origin = new Vector2(5, 60);
                //spriteBatch.Draw(logo.Texture, logo.Position, null, Color.White, logo.Rotation, logo.Origin, 2.0f, SpriteEffects.None, 0.0f);
                r.Position = new Vector2(750, 380);
                l.Position = new Vector2(50, 380);
                j.Position = new Vector2(50, 50);
                e.Position = new Vector2(50, 150);
                f.Position = new Vector2(750, 50);
                spriteBatch.Draw(r.Texture, r.Position, null, Color.White, r.Rotation, r.Origin, 1.0f, r.eff, 0.0f);
                spriteBatch.Draw(l.Texture, l.Position, null, Color.White, l.Rotation, l.Origin, 1.0f, l.eff, 0.0f);
                spriteBatch.Draw(j.Texture, j.Position, null, Color.White, j.Rotation, j.Origin, 1.0f, j.eff, 0.0f);
                spriteBatch.Draw(e.Texture, e.Position, null, Color.White, e.Rotation, e.Origin, 1.0f, e.eff, 0.0f);
                spriteBatch.Draw(f.Texture, f.Position, null, Color.White, f.Rotation, f.Origin, 1.0f, f.eff, 0.0f);
                spriteBatch.Draw(charr.Texture, charr.Position, null, Color.White, charr.Rotation, charr.Origin, 1.0f, charr.eff, 0.0f);
                spriteBatch.DrawString(font, "Level " + lv + " Exp " + exp + " Hp " + hp + " Character " + cha + "  X  ", new Vector2(300, 5), Color.White);
                spriteBatch.DrawString(font, "Point " + pi, new Vector2(300, 25), Color.White);
                spriteBatch.DrawString(font, " " + (int)((-100 * c) / 1.7361) + " D e g r e e ", new Vector2(100, 5), Color.White);

               box();

                if (ball.Display)
                    spriteBatch.Draw(ball.Texture, ball.Position, null, Color.White, ball.Rotation, ball.Origin, 1.0f, ball.eff, 0.0f);

                power.Position = new Vector2(150, 85);
                spriteBatch.Draw(power.Texture, power.Position, null, Color.White, power.Rotation, power.Origin, 1.0f, power.eff, 0.0f);
                arrow.Origin = new Vector2(0, 8);
                spriteBatch.Draw(arrow.Texture, power.Position, null, Color.White, arrow.Rotation, arrow.Origin, 1.0f, arrow.eff, 0.0f);
            }
            else
            {
                over.Position = new Vector2(450,200);
                spriteBatch.Draw(over.Texture, over.Position, null, Color.White, over.Rotation, over.Origin, 1.0f, over.eff, 0.0f);
            }


            if (boom.Display)
                spriteBatch.Draw(boom.Texture, boom.Position, null, Color.White, boom.Rotation, boom.Origin, 1.0f, boom.eff, 0.0f);

            if (level.Display)
                spriteBatch.Draw(level.Texture, level.Position, null, Color.White, level.Rotation, level.Origin, 1.0f, level.eff, 0.0f);

            if (die.Display)
                spriteBatch.Draw(die.Texture, die.Position, null, Color.White, die.Rotation, die.Origin, 1.0f, die.eff, 0.0f);

                spriteBatch.End();
            base.Draw(gameTime);
        } 
        public void box()
        {
            //พื้นล่าง
            boxr[0] = new Rectangle(0, 0, 800, 480);
            //กล่อง 1
            boxr[1] = new Rectangle(479, 408, 117, 1);
            //กล่อง 2
            boxr[2] = new Rectangle(386, 730, 48, 1);
            //ลูกปืน
            boxr[3] = new Rectangle((int)ball.Position.X,(int)ball.Position.Y, 10, 10);
            //คน
            boxr[4] = new Rectangle((int)ch.Position.X, (int)ch.Position.Y, 60,77);
        }

        public void character()
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                x = (int)tl.Position.X;
                y = (int)tl.Position.Y;
                if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved))
                {
                    if ((x > 724 && x < 774) && (y > 360 && y < 400))
                    {//เดินหน้า
                        ch.eff = SpriteEffects.None;
                        cannon.Rotation = 0.45f;
                        c = (float)((45 * 1.7361) / (-100));
                        if (ch.Position.X % 15 == 0)
                            ch.step += 1;
                        if (ch.step >= 4)
                            ch.step = 0;
                        ch.Position += new Vector2(5, 0);
                    }

                    if ((x > 25 && x < 75) && (y > 360 && y < 400))
                    {//กลับหลัง
                        ch.eff = SpriteEffects.FlipHorizontally;
                         cannon.Rotation = -0.45f;
                         c = (float)((135 * 1.7361) / (-100));
                        if (ch.Position.X % 15 == 0)
                            ch.step += 1;
                        if (ch.step >= 4)
                            ch.step = 0;
                        ch.Position -= new Vector2(5, 0);
                    }
                    //+
                    if ((x > 25 && x < 75) && (y > 30 && y < 70)&&!(ball.Display))
                    {
                        c -= 0.025f;
                    }
                    //-
                    if ((x > 25 && x < 75) && (y > 130 && y < 172)&&!(ball.Display))
                    {
                        c += 0.025f;
                     }
                    //ปรับค่าเกินรอบ
                    if ((int)((-100 * c) / 1.7361) >= 360)
                        c = 0.0f;

                    if ((int)((-100 * c) / 1.7361) < 0)
                        c = (float)((360*1.7361)/(-100));

                    arrow.Rotation = c;
                    cannon.Rotation = MathHelper.Clamp(cannon.Rotation, MathHelper.ToRadians(0), MathHelper.ToRadians(50));
                    
                    if ((x > 724 && x < 774) && (y > 30 && y < 70))
                    {//โจมตี
                        if (!fire)
                        {
                            cc = c;
                            fire = true;
                            ball.Position = ch.Position+new Vector2(10,5);
                        }
                    }
                }
                else
                {   //หยุดเดิน
                    ch.step = 4;
                }
            }
        }
        public void firer()
        {
            ball.Display = true;
          //  (int)((-100 * c) / 1.7361);
            ball.Position += new Vector2(10 * (float)Math.Cos(cc), 10 * (float)Math.Sin(cc));

           if (!(boxr[0].Intersects(boxr[3])))
            {    
              ball.Position = ch.Position;
              ball.Display = false;
              fire = false;
            }

        }


        public void boomr()
        {
            boom.Display = true;
            boom.Position -= new Vector2(0, 1);

            if (boom.Position.Y<300)
            {
                boom.Display = false;
                boomm = false;
            }

        }


        public void levelr()
        {
            level.Display = true;
            level.Position -= new Vector2(0, 1);

            if (level.Position.Y < 300)
            {
                level.Display = false;
                levell = false;
            }

        }

        public void dier()
        {
            die.Display = true;
            die.Position -= new Vector2(0, 1);

            if (die.Position.Y < 300)
            {
                die.Display = false;
                diee = false;
            }

        }
        public void copter()
        {
            int cc = 0;
            foreach (show sha in shows)
            {
                if (sha.Display)
                {
                    if (sha.Position.X == sha.target.X && sha.Position.Y == sha.target.Y)
                    {
                        Random rx = new Random();
                        int x1 = rx.Next(0, 800);
                        Random ry = new Random();
                        int y1 = ry.Next(0, 300);
                        sha.target = new Vector2(x1,y1);
                    }
                    else
                    {
                        if (sha.Position.X > sha.target.X)
                        {
                            sha.Position -= new Vector2(1, 0);
                            sha.eff = SpriteEffects.None;
                            if (!shows2[cc].Display)
                            shows2[cc].eff = SpriteEffects.None;

                        }
                        else if (sha.Position.X < sha.target.X)
                        {
                            sha.Position += new Vector2(1, 0);
                            sha.eff = SpriteEffects.FlipHorizontally;
                            if(!shows2[cc].Display)
                            shows2[cc].eff = SpriteEffects.FlipHorizontally;
                        }
                        if (sha.Position.Y > sha.target.Y)
                        {
                            sha.Position -= new Vector2(0, 1);
                        }
                        else if (sha.Position.Y < sha.target.Y)
                        {
                            sha.Position += new Vector2(0, 1);
                        }
                    }
                }
                cc++;
            }
        }
        public void effect()
        {
            if (eff)
            {
                spriteBatch.Draw(ef.Texture, new Vector2(at1, at2 - 20), new Rectangle((e1 * 106), (e2 * 106) + 2, 106, 100), Color.White);

                if (e1 < 4)
                {
                    e1++;
                }
                else
                {
                    e1 = 0;
                    e2++;
                }
                if (e1 == 4 && e2 == 3)
                {
                    e1 = 0; e2 = 0;
                    eff = false;
                }
            }
        }

    }
  
}
