using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Imaging; 

namespace Pasta 
{ 
    public class AppMain
 {
  
  public static void Main (string[] args)
  {
   Director.Initialize();
   
   Scene scene = new Scene();
   scene.Camera.SetViewFromViewport();
   
   var width = Director.Instance.GL.Context.GetViewport().Width;
   var height = Director.Instance.GL.Context.GetViewport().Height;
   
   Image img = new Image(ImageMode.Rgba,new ImageSize(width,height),new ImageColor(255,0,0,0));
   img.DrawText("Pasta...", 
                new ImageColor(255,0,0,255),
                new Font(FontAlias.System,170,FontStyle.Bold),
                new ImagePosition(150,150));
  
   Texture2D texture = new Texture2D(width,height,false,PixelFormat.Rgba);
   texture.SetPixels(0,img.ToBuffer());
   img.Dispose();                                  
   
   TextureInfo ti = new TextureInfo();
   ti.Texture = texture;
   
   SpriteUV sprite = new SpriteUV();
   sprite.TextureInfo = ti;
   
   sprite.Quad.S = ti.TextureSizef;
   sprite.CenterSprite();
   sprite.Position = scene.Camera.CalcBounds().Center;
   
   scene.AddChild(sprite);
   
   Director.Instance.RunWithScene(scene,true);
   
   bool gameOver = false;
   
   while(!gameOver)
   {
    Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Update();
    if(Input2.GamePad.GetData(0).Left.Release)
    {
     sprite.Rotate(Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Deg2Rad(90));
    }
    if(Input2.GamePad0.Right.Release)
    {
     sprite.Rotate(Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Deg2Rad(-90));
    }
    if((Sce.PlayStation.Core.Input.GamePad.GetData(0).Buttons & GamePadButtons.Up) == GamePadButtons.Up)
    {
     sprite.Quad.S = new Vector2(sprite.Quad.S.X += 10.0f,sprite.Quad.S.Y += 10.0f);
     sprite.CenterSprite();
    }
    if((Sce.PlayStation.Core.Input.GamePad.GetData(0).Buttons & GamePadButtons.Down) == GamePadButtons.Down)
    {
     sprite.Quad.S = new Vector2(sprite.Quad.S.X -= 10.0f,sprite.Quad.S.Y -= 10.0f);
     sprite.CenterSprite();
    }
    if(Input2.GamePad0.Circle.Press == true)
     gameOver = true;
    
    Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Render();
    Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.Context.SwapBuffers();
    Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.PostSwap();
   }
   
   Director.Terminate();
  }
 }
}