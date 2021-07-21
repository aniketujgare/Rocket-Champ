using System.Collections.Generic;

public class SaveState
{
    public int diamond = 0;
    public int completedLevel = 0;
    public int shipOwned = 0;
    public int activeShip = 0;
    public bool isvipMember = false;
    //public int activeShipPreview = 0;
    public List<int> sceneNoIndex = new List<int>();
    public List<bool> IsDiamondCollected = new List<bool>();
}

