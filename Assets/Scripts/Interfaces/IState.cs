public interface IState
{
    int Highscore { get; set; }
    int Score { get; set; }
    int CollectableCollected { get; set; }
    bool IsHit { get; set; }
    void HandleSave();
    void IncreaseScore();
}