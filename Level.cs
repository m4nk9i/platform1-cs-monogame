namespace platform1_cs_monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;




public class Level
{
    
    public  class tfrag
    {
        public float x1,x2;
        public float y1,y2;

        public tfrag(float px1, float py1, float px2, float py2)
        {
            x1=px1;
            y1=py1;
            x2=px2;
            y2=py2;
        }
    }; 
    public tfrag[] tatlas;
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

        tatlas=new tfrag[3];
        tatlas[0]=new tfrag(0,0,0.330f,1);
        tatlas[1]=new tfrag(0.340f,0,0.660f,1);
        tatlas[2]=new tfrag(0.670f,0,1,1);
        

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
    public void addQuad(List<VertexPositionColorTexture> pbuffer, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4,int tindex )
    {
        pbuffer.Add(new VertexPositionColorTexture(v1,new Color(255,255,255),new Vector2(tatlas[tindex].x1,tatlas[tindex].y1)));
        pbuffer.Add(new VertexPositionColorTexture(v2,new Color(255,255,255),new Vector2(tatlas[tindex].x2,tatlas[tindex].y1)));
        pbuffer.Add(new VertexPositionColorTexture(v4,new Color(255,255,255),new Vector2(tatlas[tindex].x1,tatlas[tindex].y2)));
      
        pbuffer.Add(new VertexPositionColorTexture(v4,new Color(255,255,255),new Vector2(tatlas[tindex].x1,tatlas[tindex].y2)));
        pbuffer.Add(new VertexPositionColorTexture(v2,new Color(255,255,255),new Vector2(tatlas[tindex].x2,tatlas[tindex].y1)));
        pbuffer.Add(new VertexPositionColorTexture(v3,new Color(255,255,255),new Vector2(tatlas[tindex].x2,tatlas[tindex].y2)));
        
    }

    public VertexPositionColorTexture[] getVertexbuffer()
    {
        List<VertexPositionColorTexture> lbuffer=new List<VertexPositionColorTexture>();
        
        for (int i=0;i<width;i++)
        {
            for (int j=0;j<height;j++)
            {
            //    System.Console.WriteLine(i+" "+j);
                switch (grid[i,j].type)
                {
                    case Tile.t_type.t_Solid:
                    {
                        addQuad(lbuffer,
                            new Vector3(    i*5,    j*5,0),
                            new Vector3((1+i)*5,    j*5,0),
                            new Vector3((1+i)*5,(1+j)*5,0),
                            new Vector3(    i*5,(1+j)*5,0),
                            1
                            );
                       addQuad(lbuffer,
                            new Vector3(    i*5,    j*5,0),
                            new Vector3((1+i)*5,    j*5,0),
                            new Vector3((1+i)*5,    j*5,5),
                            new Vector3(    i*5,    j*5,5),
                            2
                            );
                        addQuad(lbuffer,
                            new Vector3(    i*5,    j*5,0),
                            new Vector3(    i*5,    j*5,5),
                            new Vector3(    i*5,(1+j)*5,5),
                            new Vector3(    i*5,(1+j)*5,0),
                            1
                            );
                            
                        addQuad(lbuffer,
                            new Vector3((1+i)*5,    j*5,0),
                            new Vector3((1+i)*5,    j*5,5),
                            new Vector3((1+i)*5,(1+j)*5,5),
                            new Vector3((1+i)*5,(1+j)*5,0),
                            1
                            );

                    }break;
                    case Tile.t_type.t_Empty:
                    {

                    }break;      
                    default:break;              
                }
            
                
            }
        }

        VertexPositionColorTexture[] tbuffer=new VertexPositionColorTexture[lbuffer.Count];
        for(int i=0;i<lbuffer.Count;i++)
        {
            tbuffer[i]=lbuffer[i];
        }
        
        return tbuffer;
    }
}