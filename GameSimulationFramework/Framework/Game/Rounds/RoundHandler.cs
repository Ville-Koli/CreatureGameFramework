namespace Framework.Game.Rounds
{
    public class RoundHandler
    {
        private RoundHistory _history = new();
        private RoundOrder? _unitOrder = null;
        private Round? _currentRound = null;
    }
}