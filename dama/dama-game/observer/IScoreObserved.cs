
interface IScoreObserved
{
   
    void RegisterObserver(IScoreObserver scoreobs);
    void UnregisterObserver(IScoreObserver scoreobs);

}
