namespace platform1_cs_monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player
{
    public Vector2 position;
    public Vector2 vel;
    public Vector2 accel;
    public Player()
    {
        position.X=0;
        position.Y=0;
        vel.X=0;
        vel.Y=0;
        accel.X=0;
        accel.Y=0;
    }

    public void update(GameTime pgt)
    {
        vel+=accel;
        position+=vel;
    }

    public VertexPositionColorTexture[] getVertexbuffer()
    {
        VertexPositionColorTexture[] tbuffer=new VertexPositionColorTexture[6];
        tbuffer[0]=new VertexPositionColorTexture(new Vector3( 0, 0, 0),new Color(255,255,255),new Vector2(1,1));
        tbuffer[1]=new VertexPositionColorTexture(new Vector3( 5, 5, 0),new Color(255,255,255),new Vector2(0,0));
        tbuffer[2]=new VertexPositionColorTexture(new Vector3( 0, 5, 0),new Color(255,255,255),new Vector2(1,0));
        tbuffer[3]=new VertexPositionColorTexture(new Vector3( 0, 0, 0),new Color(255,255,255),new Vector2(1,1));
        tbuffer[4]=new VertexPositionColorTexture(new Vector3( 5, 5, 0),new Color(255,255,255),new Vector2(0,0));
        tbuffer[5]=new VertexPositionColorTexture(new Vector3( 5, 0, 0),new Color(255,255,255),new Vector2(0,1));
 
        return tbuffer;
    }
}