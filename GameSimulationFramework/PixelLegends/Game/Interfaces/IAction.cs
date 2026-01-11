namespace PixelLegends.Game.Interfaces
{
    public interface IAction
    {
        public ActionType GetActionType();
        public void Action(Source from, Source to);
    }
}