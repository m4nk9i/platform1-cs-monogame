namespace platform1_cs_monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.IO;

public class Tile
{
    public enum t_type
    {
        t_Empty=0,
        t_Solid=1,
        t_Water=2

    }
    public enum t_graphics
    {
        g_Empty=0,
        g_Ground,
        g_Cave,
        g_GroundGrassTop,
        g_EmptyLadder,
        g_CaveLadder,
        g_Water
    }
    public t_type type;
    public t_graphics graphics;

    public Tile():this(t_type.t_Empty,t_graphics.g_Empty)
    {

    }

    public Tile(t_type ptype,t_graphics pgraphics)
    {
        this.graphics=pgraphics;
        this.type=ptype;
    }

    public void set(t_type ptype,t_graphics pgraphics)
    {
        this.graphics=pgraphics;
        this.type=ptype;
    //    System.Console.WriteLine(ptype+" "+pgraphics);
    }

    public string ToStr(string pindent)
    {
        return this.type.ToString();
    }


}

public class Level
{
    public int height,width;
    Tile [,] grid;
    public Level()
    {
        //grid=new Tile[10,10];
    }

    public Level (string pfname)
    {
      string tfstr=File.ReadAllText(pfname);
        JsonDocument jsdoc=JsonDocument.Parse(tfstr);
        JsonElement jselement=jsdoc.RootElement;
        this.width=jselement.GetProperty("width").GetInt32();
        this.height=jselement.GetProperty("height").GetInt32();

    //    System.Console.WriteLine(width+";"+height);
        this.grid=new Tile[this.width,this.height];
        
        for (int i=0;i<this.width;i++)
        {
            for (int j=0;j<this.height;j++)
            {
                grid[i,j]=new Tile();
 
            }
        }

        JsonElement.ArrayEnumerator jsenum=jselement.GetProperty("grid").EnumerateArray();
        int k=0;
        while (jsenum.MoveNext())
        {
            string tstr=jsenum.Current.GetString();
            System.Console.WriteLine("["+jsenum.Current.GetString());
            for(int j=0;j<width;j++)
            {

                if ((tstr[j])=='#')
                {
                    grid[j,k].set(Tile.t_type.t_Solid,Tile.t_graphics.g_Ground);
                }
            }
            k++;
        }


    }

    public void InitDummy()
    {
        grid=new Tile[5,10];
        for (int i=0;i<this.grid.GetLength(0);i++)
        {
            for (int j=0;j<this.grid.GetLength(1);j++)
            {
                grid[i,j]=new Tile();
 
            }
        }
        for (int i=0;i<this.grid.GetLength(0);i++)
        {
            
            grid[0,i].set(Tile.t_type.t_Solid,Tile.t_graphics.g_Ground);
            grid[i,0].set(Tile.t_type.t_Solid,Tile.t_graphics.g_Ground);


            grid[this.grid.GetLength(0)-1,i].set(Tile.t_type.t_Solid,Tile.t_graphics.g_Ground);
            grid[i,this.grid.GetLength(1)-1].set(Tile.t_type.t_Solid,Tile.t_graphics.g_Ground);


        }
       // System.Console.WriteLine("888"+this.toStr(""));
    }

    public void LoadLeagues(string ppath)
    {
  
    }

    public string toStr(string pindent)
    {
        string tstr="";
        for (int i=0;i<this.grid.GetLength(0);i++)
        {
            for (int j=0;j<this.grid.GetLength(1);j++)
            {
                tstr+="["+i+","+j+"]"+grid[i,j].ToStr("");
            }
            tstr+="\r\n";
        }
        return tstr;

    }

    public VertexPositionColorTexture[] getVertexbuffer()
    {
        VertexPositionColorTexture[] tbuffer=new VertexPositionColorTexture[6*width*height];
        
        for (int i=0;i<width;i++)
        {
            for (int j=0;j<height;j++)
            {
            //    System.Console.WriteLine(i+" "+j);
                switch (grid[i,j].type)
                {
                    case Tile.t_type.t_Solid:
                    {
                        tbuffer[(i*height+j)*6  ]=new VertexPositionColorTexture(new Vector3(    i*5,    j*5,0),new Color(i*20,j*20,255),new Vector2(1,1));
                        tbuffer[(i*height+j)*6+1]=new VertexPositionColorTexture(new Vector3((i+1)*5,(j+1)*5,0),new Color(i*20,j*20,255),new Vector2(0,0));
                        tbuffer[(i*height+j)*6+2]=new VertexPositionColorTexture(new Vector3(    i*5,(j+1)*5,0),new Color(i*20,j*20,255),new Vector2(1,0));
 
                        tbuffer[(i*height+j)*6+3]=new VertexPositionColorTexture(new Vector3(    i*5,    j*5,0),new Color(i*20,j*20,255),new Vector2(1,1));
                        tbuffer[(i*height+j)*6+4]=new VertexPositionColorTexture(new Vector3((i+1)*5,(j+1)*5,0),new Color(i*20,j*20,255),new Vector2(0,0));
                        tbuffer[(i*height+j)*6+5]=new VertexPositionColorTexture(new Vector3((i+1)*5,    j*5,0),new Color(i*20,j*20,255),new Vector2(0,1));
                    }break;
                    case Tile.t_type.t_Empty:
                    {
                        tbuffer[(i*height+j)*6  ]=new VertexPositionColorTexture(new Vector3(    i*5,    j*5,0),new Color(i*20,j*20,255),new Vector2(1,1));
                        tbuffer[(i*height+j)*6+1]=new VertexPositionColorTexture(new Vector3((i+0.1f)*5,(j+0.1f)*5,0),new Color(i*20,j*20,255),new Vector2(0,0));
                        tbuffer[(i*height+j)*6+2]=new VertexPositionColorTexture(new Vector3(    i*5,(j+0.1f)*5,0),new Color(i*20,j*20,255),new Vector2(1,0));
 
                        tbuffer[(i*height+j)*6+3]=new VertexPositionColorTexture(new Vector3(    i*5,    j*5,0),new Color(i*20,j*20,255),new Vector2(1,1));
                        tbuffer[(i*height+j)*6+4]=new VertexPositionColorTexture(new Vector3((i+0.1f)*5,(j+0.1f)*5,0),new Color(i*20,j*20,255),new Vector2(0,0));
                        tbuffer[(i*height+j)*6+5]=new VertexPositionColorTexture(new Vector3((i+0.1f)*5,    j*5,0),new Color(i*20,j*20,255),new Vector2(0,1));
                    }break;      
                    default:break;              
                }
            
                
            }
        }

        return tbuffer;
    }
}