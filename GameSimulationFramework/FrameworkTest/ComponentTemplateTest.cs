using Framework.Game.BaseTypes;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Components;

namespace FrameworkTest;

public class ComponentTemplateTest
{
    private ComponentTemplate<Creature> _creatureStatisticsTemplate;

    [SetUp]
    public void Setup()
    {
        _creatureStatisticsTemplate = new ComponentTemplate<Creature>();
    }

    [Test]
    public void Test_clear()
    {
        _creatureStatisticsTemplate.Clear();

        if(_creatureStatisticsTemplate.ComponentCount() == 0) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition()
    {
        _creatureStatisticsTemplate.Clear();

        _creatureStatisticsTemplate.AddComponent(
            new Component<CloneableValue<int>>(ComponentType.Health, new CloneableValue<int>(0))
        );
        if(_creatureStatisticsTemplate.ComponentCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_addition_duplicate_type()
    {
        _creatureStatisticsTemplate.Clear();

        _creatureStatisticsTemplate.AddComponents(
            new Component<CloneableValue<int>>(ComponentType.Health, new CloneableValue<int>(0)),
            new Component<CloneableValue<int>>(ComponentType.Health, new CloneableValue<int>(52))
        );
        if(_creatureStatisticsTemplate.ComponentCount() == 1) Assert.Pass();
        Assert.Fail();
    }

    [Test]
    public void Test_copying()
    {
        _creatureStatisticsTemplate.Clear();
        Creature creature = new();
        _creatureStatisticsTemplate.AddComponents(
            new Component<CloneableValue<int>>(ComponentType.Health, new CloneableValue<int>(525)),
            new Component<CloneableValue<int>>(ComponentType.Shield, new CloneableValue<int>(322)),
            new Component<CloneableValue<int>>(ComponentType.Stamina, new CloneableValue<int>(115)),
            new Component<CloneableValue<int>>(ComponentType.Damage, new CloneableValue<int>(237))
        );
        _creatureStatisticsTemplate.CopyComponents(creature);
        if(creature.ComponentCount() == 4) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_copy()
    {
        _creatureStatisticsTemplate.Clear();
        Creature creature = new();
        _creatureStatisticsTemplate.AddComponents(
            new Component<CloneableValue<int>>(ComponentType.Health, new CloneableValue<int>(525))
        );
        _creatureStatisticsTemplate.CopyComponent
        (_creatureStatisticsTemplate.Query<CloneableValue<int>>(ComponentType.Health)!, creature);
        if(creature.ComponentCount() == 1) Assert.Pass();
        Assert.Fail();
    }
    [Test]
    public void Test_remove()
    {
        _creatureStatisticsTemplate.Clear();

        _creatureStatisticsTemplate.AddComponent(
            new Component<CloneableValue<int>>(ComponentType.Health, new CloneableValue<int>(0))
        );

        _creatureStatisticsTemplate.RemoveComponent(ComponentType.Health);

        if(_creatureStatisticsTemplate.ComponentCount() == 0) Assert.Pass();
        Assert.Fail();
    }
}
