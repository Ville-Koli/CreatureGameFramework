namespace Framework.Game.Interfaces
{
    public interface IAction<T>
    {
        public ActionType GetActionType();
        public void Action(Source<T> from, Source<T> to);
    }
}