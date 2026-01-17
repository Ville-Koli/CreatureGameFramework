using Framework.Game.BaseTypes;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Statistics;

namespace FrameworkTest;

public class StatisticTemplate_Creature_Test
{
    private StatisticTemplate<Creature> _creatureStatistics;
    [SetUp]
    public void Setup()
    {
        _creatureStatistics = new();
    }

    [Test]
    public void Test_clear()
    {
        _creatureStatistics.Clear();
        if(_creatureStatistics.StatisticCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition()
    {
        _creatureStatistics.Clear();

        _creatureStatistics.AddStatistic(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        if(_creatureStatistics.StatisticCount() == 1) Assert.Pass();
        Assert.Fail();
    }
}
