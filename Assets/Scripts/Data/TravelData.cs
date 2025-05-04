using System.Collections.Generic;

[System.Serializable]
public class TravelData
{
    public int nodesVisited;
    public List<int> nodeVisitSequence = new List<int>();
}

[System.Serializable]
public class TravelDataLog
{
    public List<TravelData> runs = new List<TravelData>();
}