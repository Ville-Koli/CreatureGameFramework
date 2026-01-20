using Framework.Game.BaseTypes;
using Framework.Game.Extensions;
using Framework.Game.Teams.Bag;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Components;
class Program
{
    static void Main()
    {
        Creature creature = new Creature().AddComponents(
                    new Component<int>(ComponentType.Health, 100),
                    new Component<int>(ComponentType.Stamina, 30),
                    new Component<int>(ComponentType.Shield, 50),
                    new Component<string>(ComponentType.Name, "Creature 1")
        );
        Creature creature2 = new Creature().AddComponents(
                    new Component<int>(ComponentType.Health, 3),
                    new Component<int>(ComponentType.Stamina, 55),
                    new Component<int>(ComponentType.Shield, 65),
                    new Component<string>(ComponentType.Name, "Creature 2")
        );
        InventorySlot<Creature> inventorySlot = new InventorySlot<Creature>();
        inventorySlot.OnItemChanged += (o, e) => {Console.WriteLine($"Item changed to {e.GetToItem()?.Query<string>(ComponentType.Name)?.GetTypedValue()}");};

        inventorySlot.Item = creature;  // Item changed to Creature 1
        inventorySlot.Item = creature2; // Item changed to Creature 2
        inventorySlot.Item = null;      // Item changed to

        Component<int> reference = creature2.Query<int>(ComponentType.Health)!;
        Component<string> referenceName = creature2.Query<string>(ComponentType.Name)!;

        reference.OnValueChanged += (o, e) => {Console.WriteLine($"statistic health changed to {e.GetChangedTo()}");};

        Component statistic = new Component<int>(ComponentType.Health, 50);

        creature2.UpdateStatistic(statistic);                 // statistic health changed to 50
        reference.SetTypedValue(20);                          // statistic health changed to 20
        referenceName.SetTypedValue("Creature NAME CHANGED"); // statistic name changed to Creature NAME CHANGED

        /**
        OUTPUTS: 
            Item changed to Creature 1
            Item changed to Creature 2
            Item changed to
            statistic health changed to 50
            statistic health changed to 20
            statistic name changed to Creature NAME CHANGED
        **/

        // Testing cloning statistics

        var statisticTemplate = new StatisticTemplate<Creature>();
        string name = "Clone Creature";
        CloneableString cloneableString = new CloneableString(name);

        statisticTemplate.AddComponents(
            new Component<CloneableInt>(ComponentType.Health, new CloneableInt(30)),
            new Component<CloneableInt>(ComponentType.Stamina, new CloneableInt(50)),
            new Component<CloneableInt>(ComponentType.Shield, new CloneableInt(100)),
            new Component<CloneableString>(ComponentType.Name, cloneableString)
        );

        Creature newCreature = new Creature();

        statisticTemplate.CopyComponents(newCreature);

        Component<int> statisticCloneableInt = newCreature.Query<int>(ComponentType.Health)!;
        Console.WriteLine("new creature health is: " + statisticCloneableInt.GetTypedValue() + " amount of statistics: " + newCreature.GetComponents().ToArray().Length);
        statisticCloneableInt.SetValue(50);

        Console.WriteLine("creature name: " + newCreature.Query<string>(ComponentType.Name)!.GetTypedValue());
        name += "!";
        Console.WriteLine("creature name: " + newCreature.Query<string>(ComponentType.Name)!.GetTypedValue());

        Console.WriteLine("creature name: " + statisticTemplate.Query<CloneableString>(ComponentType.Name)!.GetTypedValue()!.GetValue());
        statisticTemplate.Query<CloneableString>(ComponentType.Name)!.GetTypedValue()!.SetValue("new creature name");
        Console.WriteLine("creature name: " + newCreature.Query<string>(ComponentType.Name)!.GetTypedValue());

        Console.WriteLine("creature name: " + statisticTemplate.Query<CloneableString>(ComponentType.Name)!.GetTypedValue()!.GetValue());

        
        RandomTemplate<Creature> randomTemplate = new();
        randomTemplate.AddComponents(

        new Component<ComponentRange<CloneableValue<float>>>
                    (ComponentType.Health,
                        new ComponentRange<CloneableValue<float>>(
                            new CloneableLazyValueRange(0, 100, 1f)
                        )),

        new Component<ComponentRange<CloneableValue<float>>>
                    (ComponentType.Stamina,
                    new ComponentRange<CloneableValue<float>>(
                            new CloneableLazyValueRange(50, 100, 1f)
                        ))
        
        );

        Console.WriteLine("random template statistic count: " + randomTemplate.ComponentCount());
        Creature creature1 = new();
        randomTemplate.CopyComponents(creature1);

        Console.WriteLine("Realized value from random template: " + creature1.Query<float>(ComponentType.Health)!.GetTypedValue());
        Console.WriteLine("Realized value from random template: " + creature1.Query<float>(ComponentType.Stamina)!.GetTypedValue());
        foreach(var element in new CloneableLazyValueRange(0, 100, 25f))
        {
            Console.WriteLine("Realized value of elem: " + element);
        }
        
        // testing whether statistic can have a statistic inside it
        Component<Component<int>> statistic1 = new(ComponentType.Damage, new Component<int>(ComponentType.Health, 5));
    }
}