public class FullMapSearch : Search
{

    public override void FindCells(Cell startingCell)
    {
        foreach (Cell cell in Level.instance.transform.GetChild(0).GetComponentsInChildren<Cell>())
            results.Add(cell);
    }

}
