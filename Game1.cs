using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace platform1_cs_monogame;

public class Game1 : Game
{
    private Level level;
    private Player player1;
    private GraphicsDeviceManager _graphics;
    //private SpriteBatch _spriteBatch;

    private Texture2D texture1;

            //Camera
        Vector3 camTarget;
        Vector3 camPosition;
        Matrix projectionMatrix;
        Matrix viewMatrix;
        Matrix worldMatrix;

        //BasicEffect for rendering
        BasicEffect basicEffect;

        //Geometric info
        VertexPositionColorTexture[] triangleVertices;
        VertexBuffer vertexBuffer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        level=new Level("Content/level0.json");
        player1=new Player();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
    
        base.Initialize();

    //            _spriteBatch = new SpriteBatch(GraphicsDevice);

        FileStream fileStream = new FileStream("Content/texture_a2.png", FileMode.Open);
        texture1 = Texture2D.FromStream(GraphicsDevice, fileStream);
        fileStream.Dispose();

            camTarget = new Vector3(0f, 0f, 0f);
            camPosition = new Vector3(0f, 0f, -100f);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                               MathHelper.ToRadians(45f), 
                               GraphicsDevice.DisplayMode.AspectRatio,1f, 1000f);

            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, new Vector3(0f, 1f, 0f));// Y up
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);

            //BasicEffect
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;

            // Want to see the colors of the vertices, this needs to be on
            basicEffect.VertexColorEnabled = true;
            basicEffect.TextureEnabled=true;
            basicEffect.Texture=texture1;

            //Lighting requires normal information which VertexPositionColor does not have
            //If you want to use lighting and VPC you need to create a custom def
            basicEffect.LightingEnabled = false;

            //Geometry  - a simple triangle about the origin


            triangleVertices = level.getVertexbuffer();
            
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorTexture), triangleVertices.GetLength(0), BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColorTexture>(triangleVertices);
                    
    }

    protected override void LoadContent()
    {

        
        //level.InitDummy();
        // TODO: use this.Content to load your game content here
    }

    protected override void UnloadContent()
    {
        texture1.Dispose(); //<-- Only directly loaded
        //    Content.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
            player1.accel.X=0;
            player1.accel.Y=0;
        if (Keyboard.GetState().IsKeyDown(Keys.Left)) player1.accel.X=-0.05f;
        if (Keyboard.GetState().IsKeyDown(Keys.Right)) player1.accel.X=0.05f;
        if (Keyboard.GetState().IsKeyDown(Keys.Up)) player1.accel.Y=-0.05f;

        // TODO: Add your update logic here

        player1.update(gameTime);

//        viewMatrix = Matrix.CreateLookAt(new Vector3(player1.position.X, player1.position.Y, -100f), new Vector3( player1.position.X,player1.position.Y, 0f), new Vector3(0f, 1f, 0f));

        viewMatrix = Matrix.CreateLookAt(new Vector3(-20f, -15f, -50f), new Vector3( player1.position.X,player1.position.Y+25f, 0f), new Vector3(0f, -1f, 0f));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            

            //Turn off culling so we see both sides of our rendered triangle
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 900);
            }
            /*
           _spriteBatch.Begin();
            _spriteBatch.Draw(texture1, destinationRectangle: new Rectangle(50, 50, 150, 300),new Color(255,255,255));
            _spriteBatch.End();
*/
        base.Draw(gameTime);
    }
}
