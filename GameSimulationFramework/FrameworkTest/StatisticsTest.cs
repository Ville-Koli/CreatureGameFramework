using Framework.Game.BaseTypes;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Components;

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
        if(_creatureStatistics.ComponentCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition()
    {
        _creatureStatistics.Clear();

        _creatureStatistics.AddComponent(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        if(_creatureStatistics.ComponentCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition_duplicate_keys()
    {
        _creatureStatistics.Clear();
        // add health statistic
        _creatureStatistics.AddComponent(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        // attempt to add a duplicate key
        _creatureStatistics.AddComponent(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        // the statistic count should be 1
        if(_creatureStatistics.ComponentCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_multiple_addition_int_duplicate_keys()
    {
        _creatureStatistics.Clear();
        // add health statistic
        _creatureStatistics.AddComponents(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10)),
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        // the statistic count should be 1
        if(_creatureStatistics.ComponentCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_multiple_addition_int()
    {
        _creatureStatistics.Clear();
        // add health statistic
        _creatureStatistics.AddComponents(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10)),
            new Component<CloneableInt>(ComponentType.Damage, new CloneableInt(10))
        );
        // the statistic count should be 1
        if(_creatureStatistics.ComponentCount() == 2) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_remove_by_statistic()
    {
        _creatureStatistics.Clear();

        _creatureStatistics.AddComponent(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        _creatureStatistics.RemoveComponent(_creatureStatistics.Query<CloneableInt>(ComponentType.Health)!);
        if(_creatureStatistics.ComponentCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_remove_statistic_type()
    {
        _creatureStatistics.Clear();

        _creatureStatistics.AddComponent(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        _creatureStatistics.RemoveComponent(ComponentType.Health);
        if(_creatureStatistics.ComponentCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_remove_from_empty_statistic_type()
    {
        _creatureStatistics.Clear();
        _creatureStatistics.RemoveComponent(ComponentType.Health);
        if(_creatureStatistics.ComponentCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_query()
    {
        _creatureStatistics.Clear();
        _creatureStatistics.AddComponent(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10))
        );
        Component<CloneableInt> statistic = _creatureStatistics.Query<CloneableInt>(ComponentType.Health)!;
        if(statistic.GetTypedValue()!.GetValue() == 10) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_query_empty()
    {
        _creatureStatistics.Clear();
        Component<CloneableInt>? statistic = _creatureStatistics.Query<CloneableInt>(ComponentType.Health);
        if(statistic == null) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    [TestCase(20)]
    [TestCase(-50)]
    [TestCase(0)]
    [TestCase(100)]
    public void Test_update_statistic(int health)
    {
        _creatureStatistics.Clear();
        _creatureStatistics.AddComponent(
            new Component<int>(ComponentType.Health, 10)
        );
        
        _creatureStatistics.UpdateStatistic(
            new Component<int>(ComponentType.Health, health)
        );
        // its not null as it is defined above.
        if(_creatureStatistics.Query<int>(ComponentType.Health)!.GetTypedValue() == health) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_equals()
    {
        Creature creature = new();
        _creatureStatistics.Clear();
        if(_creatureStatistics != creature) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_to_string()
    {
        _creatureStatistics.Clear();
        string stringValue = _creatureStatistics.ToString()!;
        // if didnt crash on making string of statistics then pass
        Assert.Pass();
    }
}
