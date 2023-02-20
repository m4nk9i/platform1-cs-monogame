namespace platform1_cs_monogame;     
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
