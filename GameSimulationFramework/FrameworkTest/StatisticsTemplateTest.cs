using Framework.Game.BaseTypes;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Statistics;

namespace FrameworkTest;

public class StatisticsTemplateTest
{
    private StatisticTemplate<Creature> _creatureStatisticsTemplate;

    [SetUp]
    public void Setup()
    {
        _creatureStatisticsTemplate = new StatisticTemplate<Creature>();
    }

    [Test]
    public void Test_clear()
    {
        _creatureStatisticsTemplate.Clear();

        if(_creatureStatisticsTemplate.StatisticCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition()
    {
        _creatureStatisticsTemplate.Clear();

        _creatureStatisticsTemplate.AddStatistic(
            new Statistic<CloneableValue<int>>(StatisticType.Health, new CloneableValue<int>(0))
        );
        if(_creatureStatisticsTemplate.StatisticCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition_duplicate_type()
    {
        _creatureStatisticsTemplate.Clear();

        _creatureStatisticsTemplate.AddStatistics(
            new Statistic<CloneableValue<int>>(StatisticType.Health, new CloneableValue<int>(0)),
            new Statistic<CloneableValue<int>>(StatisticType.Health, new CloneableValue<int>(52))
        );
        if(_creatureStatisticsTemplate.StatisticCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_copying()
    {
        _creatureStatisticsTemplate.Clear();
        Creature creature = new();
        _creatureStatisticsTemplate.AddStatistics(
            new Statistic<CloneableValue<int>>(StatisticType.Health, new CloneableValue<int>(525)),
            new Statistic<CloneableValue<int>>(StatisticType.Shield, new CloneableValue<int>(322)),
            new Statistic<CloneableValue<int>>(StatisticType.Stamina, new CloneableValue<int>(115)),
            new Statistic<CloneableValue<int>>(StatisticType.Damage, new CloneableValue<int>(237))
        );
        _creatureStatisticsTemplate.CopyStatistics(creature);
        if(creature.StatisticCount() == 4) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_copy()
    {
        _creatureStatisticsTemplate.Clear();
        Creature creature = new();
        _creatureStatisticsTemplate.AddStatistics(
            new Statistic<CloneableValue<int>>(StatisticType.Health, new CloneableValue<int>(525))
        );
        _creatureStatisticsTemplate.CopyStatistic<int>
        (_creatureStatisticsTemplate.Query<CloneableValue<int>>(StatisticType.Health)!, creature);
        if(creature.StatisticCount() == 1) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_remove()
    {
        _creatureStatisticsTemplate.Clear();

        _creatureStatisticsTemplate.AddStatistic(
            new Statistic<CloneableValue<int>>(StatisticType.Health, new CloneableValue<int>(0))
        );

        _creatureStatisticsTemplate.RemoveStatistic(StatisticType.Health);

        if(_creatureStatisticsTemplate.StatisticCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_unsupported_type()
    {
        _creatureStatisticsTemplate.Clear();
        try
        {
            Creature creature = new Creature();

            // int[] is not cloneable by itself as it is a reference and would need
            // to be extended by an external class so int[] is suitable for testing
            // unsupported type

            // statistic type does not matter for this test (any will suffice)
            _creatureStatisticsTemplate.AddStatistic(
                new Statistic<CloneableValue<int[]>>(StatisticType.Health, new CloneableValue<int[]>([]))
            );

            _creatureStatisticsTemplate.CopyStatistics(creature);
        }
        catch (NotSupportedException)
        {
            // pass the test if the template causes not supported error on types
            // which are not cloneable by ICloneable interace
            Assert.Pass();
        }
        Assert.Fail();
    }
}
