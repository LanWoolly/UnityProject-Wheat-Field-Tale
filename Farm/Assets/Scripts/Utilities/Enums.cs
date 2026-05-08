public enum ItemType
{
    Seed, Commodity, Furniture,
    HoeTool, ChopTool, BreakTool, ReapTool, WaterTool, CollectTool,
    ReapableScenery
}

public enum SlotType
{
    Bag, Box, Shop
}

public enum InventoryLocation
{
    Player, Box,
}

public enum PartType
{
    None, Carry, Hoe, Break, Water, Chop, Collect, Reap
}

public enum PartName
{
    Body, Hair, Arm, Tool
}

public enum Season
{
    春天, 夏天, 秋天, 冬天
}

public enum GirdType
{
    Diggable, DropItem, PlaceFurniture, NPCObstacle
}

public enum ParticleEffectType
{
    None, LeavesFalling01, LeavesFalling02, Rock, ReapableScenery, Rain,
}

public enum GameState
{
    GamePlay, Pause
}

public enum LightShift
{
    Morning, Night
}

public enum SoundName
{
    None, FootStepSoft, FootStepHard,
    Axe, Pickaxe, Hoe, Reap, Water, Basket, Chop,
    Pickup, Plant, TreeFalling, Rustle,
    AmbientCountryside1, AmbientCountryside2,
    MusicCalm1, MusicCalm2, MusicCalm3,
    AmbientIndoor1
}
