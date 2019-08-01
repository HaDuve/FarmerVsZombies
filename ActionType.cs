namespace FarmervsZombies
{
    /// <summary>
    /// List of possible actions.
    /// If more actions are needed just add a new action here
    /// </summary>
    internal enum ActionType
    {
        EmptyAction,
        ToggleFullscreen,
        MoveFarmer,
        MoveToTarget,
        SpawnNecromancer,
        GenCow,
        GenChicken,
        GenPig,
        GenACow,
        GenAChicken,
        GenAPig,
        MouseClick,
        ClearBox,
        ToggleQuadTreeDraw,
        ToggleFollowFarmer,
        ToggleFogOfWar,
        SetTarget,
        ToggleFps,
        StopFenceBuilding,
        ExitEndScreen,
        StopMovement,
        BuyStack
    }
}
