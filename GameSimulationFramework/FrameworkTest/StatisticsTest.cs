using Framework.Game.BaseTypes;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Statistics;

namespace FrameworkTest;

public class StatisticsTest
{
    private Creature _creatureStatistics;
    [SetUp]
    public void Setup()
    {
        _creatureStatistics = new Creature();
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
    [Test]
    public void Test_addition_duplicate_keys()
    {
        _creatureStatistics.Clear();
        // add health statistic
        _creatureStatistics.AddStatistic(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        // attempt to add a duplicate key
        _creatureStatistics.AddStatistic(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        // the statistic count should be 1
        if(_creatureStatistics.StatisticCount() == 1) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_multiple_addition_int_duplicate_keys()
    {
        _creatureStatistics.Clear();
        // add health statistic
        _creatureStatistics.AddStatistics(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10)),
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        // the statistic count should be 1
        if(_creatureStatistics.StatisticCount() == 1) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_multiple_addition_int()
    {
        _creatureStatistics.Clear();
        // add health statistic
        _creatureStatistics.AddStatistics(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10)),
            new Statistic<CloneableInt>(StatisticType.Damage, new CloneableInt(10))
        );
        // the statistic count should be 1
        if(_creatureStatistics.StatisticCount() == 2) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_remove_by_statistic()
    {
        _creatureStatistics.Clear();

        _creatureStatistics.AddStatistic(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        _creatureStatistics.RemoveStatistic(_creatureStatistics.Query<CloneableInt>(StatisticType.Health)!);
        if(_creatureStatistics.StatisticCount() == 0) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_remove_statistic_type()
    {
        _creatureStatistics.Clear();

        _creatureStatistics.AddStatistic(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        _creatureStatistics.RemoveStatistic(StatisticType.Health);
        if(_creatureStatistics.StatisticCount() == 0) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_remove_from_empty_statistic_type()
    {
        _creatureStatistics.Clear();
        _creatureStatistics.RemoveStatistic(StatisticType.Health);
        if(_creatureStatistics.StatisticCount() == 0) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_query()
    {
        _creatureStatistics.Clear();
        _creatureStatistics.AddStatistic(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(10))
        );
        Statistic<CloneableInt> statistic = _creatureStatistics.Query<CloneableInt>(StatisticType.Health)!;
        if(statistic.GetTypedValue()!.GetValue() == 10) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_query_empty()
    {
        _creatureStatistics.Clear();
        Statistic<CloneableInt>? statistic = _creatureStatistics.Query<CloneableInt>(StatisticType.Health);
        if(statistic == null) Assert.Pass();
        Assert.Fail();
    }
}
