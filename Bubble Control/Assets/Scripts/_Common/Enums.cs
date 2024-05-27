
namespace GDC.Enums
{
    //public enum SceneType
    //{
    //    HOME = 0,


    //    SUNNY_VALLEY = 100,



    //    WHISPERING_FOREST = 200,



    //    FOGGY_HOUSE = 300,



    //    ENLIGHTED_FROST_MOUNTAIN = 400,



    //    RAINBOW_VOLCANO = 500,



    //    SHADOW_DESERT = 600,



    //}

    public enum PlayerState
    {
        NORMAL,
        ATTACK,
        DIE,
        WIN,
    }
    public enum TransitionType
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        IN,
        FADE,
    }
    public enum TransitionLoadSceneType
    {
        NEW_SCENE, //Load sang scene moi
        RELOAD_WITH_TRANSITION, //Load lai scene cu nhung van co transition
        RELOAD_WITHOUT_TRANSITION //Load lai scene cu va khong co transition
    }
    
    public enum SceneType
    {
        UNKNOWN = -999,
        MAIN = 0,
        GAMEPLAY = 1,
        EDITOR = 2,
    }
}
