using System.Diagnostics;
using System.Numerics;
using Newtonsoft.Json;
using Raylib_cs;
using REngine;
using System.Security.Cryptography;

namespace REngine.REngineTests {


public abstract class REngineTestsBase : IDisposable
{
    protected REngineTestsBase()
    {
        // Windows must be inited so OpenGL context is created 
        Renderer.Init(500,500);
    }

    public void Dispose()
    {
        Raylib.CloseWindow();
    }
}

public class REngineTests : REngineTestsBase
{
    



    [Fact]
    public void CreateSpriteFromJSONTest()
    {
        // Windows must be inited so OpenGL context is created
        Raylib.InitWindow(0,0, "-"); 
        var spriteMeta = DataModel.LoadSpriteDataFromJSON(Path.Combine("res", "TestSprite"));
        Assert.True( spriteMeta.frameRow==0
        && spriteMeta.textureFilename == "spritesheet.png"
        && spriteMeta.dimensions.w==32
        && spriteMeta.dimensions.h==48
        && spriteMeta.animations[0].name=="walk_down", "Object does not match JSON");

        Sprite sprite = new Sprite(spriteMeta);
        Assert.True( sprite.Animation.Name =="walk_down");
        Assert.True(sprite.Animation.Frames[0]==new Vector2(0,0));
    }

    [Fact]
    public void CreateSpriteFromPathTest()
    {
        // Windows must be inited so OpenGL context is created
        Raylib.InitWindow(0, 0, "-");
        var spriteMeta = DataModel.LoadSpriteDataFromPath(Path.Combine("res","TestSprite"));
        Assert.True(spriteMeta.frameRow == 0
        && spriteMeta.textureFilename == "spritesheet.png"
        && spriteMeta.dimensions.w == 32
        && spriteMeta.dimensions.h == 48
        && spriteMeta.animations[0].name == "walk_down", "Object does not match JSON");

        Sprite sprite = new Sprite(spriteMeta);
        Assert.True(sprite.Animation.Name == "walk_down");
        Assert.True(sprite.Animation.Frames[0] == new Vector2(0, 0));
    }
    [Fact]
    public void CreateSpriteTest()
    {
        // Windows must be inited so OpenGL context is created
        Raylib.InitWindow(0,0, "-");                    
        Sprite sprite = new Sprite("TestSprite/spritesheet.png");
        //Assert.True(sprite.spriteSheet.Width==192
        //&& sprite.spriteSheet.Height==96);
        Assert.True( sprite.Animation.Name =="walk_down");
        Assert.True(sprite.Animation.Frames[0]==new Vector2(0,0));
    }
    protected void DrawTextFonts(string text, Vector2 pos, int fontSz ){
        Renderer.DrawToBuffer(text, pos, Color.White, fontSz);
    }
        
    [Fact]
    public void DrawTextFontsTest()
    {
        
        // Windows must be inited so OpenGL context is created
        Raylib.InitWindow(500,500, "-");   
        Renderer.Init(500,500);
        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose())
        {
            String text =  "This is an Example text";
        
             int y = 0;
             for (var i = 4; i < 32; i+=2)
             {
                Vector2 TextDim = Raylib.MeasureTextEx(Raylib.GetFontDefault(),text,i,1.0f);
                y += (int)(TextDim.Y+5);
                DrawTextFonts(text+" ("+i+")",new Vector2(0, y),i);
             }
             //DrawTextFonts(string text, Vector2 pos, fontSz )

            //Renderer.DrawToBuffer("(This should Fit Below Example Text)" , new Vector2(0,TextDim.Y+5), Color.White, fontSz);
            //Renderer.DrawToBuffer(new Rectangle(0, TextDim.Y, TextDim.X, 2), Color.White);
           //Renderer.DrawToBuffer(" | (This should Fit Beside Example Text)" , new Vector2(TextDim.X,0), Color.White, fontSz);
            Renderer.Draw();
            //Debug.WriteLine(sprite.Frame);    
        }

        Raylib.CloseWindow();
    }

    [Fact]
    public void DrawTextFonts2()
    {
        
        // Windows must be inited so OpenGL context is created 
        Renderer.Init(500,500);
        int i = 0;
        
        Text text = new("This is an Example text", new Vector2(75,75),Color.Orange);
        Renderer.RenderQueue.Enqueue(text);

        while (!Raylib.WindowShouldClose())
        {
            if(++i == 5) {
                text.Position = new Vector2(RandomNumberGenerator.GetInt32(0,500),RandomNumberGenerator.GetInt32(0,500));
                text.Color = new Color(RandomNumberGenerator.GetInt32(0,255), RandomNumberGenerator.GetInt32(0,255), RandomNumberGenerator.GetInt32(0,255),255);
                text.Size = RandomNumberGenerator.GetInt32(8,26);
                i=0;
                }
            
            Renderer.Draw();
            Raylib.WaitTime(1); 
        }

        Raylib.CloseWindow();
    }

    [Fact]
    public void DrawTestSprites(){
        var sprite = new Sprite(DataModel.LoadSpriteDataFromJSON("res/TestSprite/SpriteData.json"));
        
        Renderer.RenderQueue.Enqueue(sprite);

        while (!Raylib.WindowShouldClose())
        {
            
            sprite.Update();
            Renderer.Draw();
        }

    }

    [Fact]
    public void ControllerTest()
    {
            KeyboardControllable kb = new();
            Text text = new("", new Vector2(Raylib.GetScreenWidth()/2, Raylib.GetScreenWidth() / 2), Color.Orange,20);
            Renderer.RenderQueue.Enqueue(text);

            while (!Raylib.WindowShouldClose())
            {
                InputAction act =  kb.GetNextInput();
                text.text = act.ToString();
                Renderer.Draw();
            }
                
    }    
}
}