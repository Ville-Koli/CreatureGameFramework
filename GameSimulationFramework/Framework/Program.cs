using Framework.Game.BaseTypes;
using Framework.Game.Teams.Bag;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Statistics;
class Program
{
    static void Main()
    {
        Creature creature = new Creature().AddStatistics(
                    new Statistic<int>(StatisticType.Health, 100),
                    new Statistic<int>(StatisticType.Stamina, 30),
                    new Statistic<int>(StatisticType.Shield, 50),
                    new Statistic<string>(StatisticType.Name, "Creature 1")
        );
        Creature creature2 = new Creature().AddStatistics(
                    new Statistic<int>(StatisticType.Health, 3),
                    new Statistic<int>(StatisticType.Stamina, 55),
                    new Statistic<int>(StatisticType.Shield, 65),
                    new Statistic<string>(StatisticType.Name, "Creature 2")
        );
        InventorySlot<Creature> inventorySlot = new InventorySlot<Creature>();
        inventorySlot.OnItemChanged += (o, e) => {Console.WriteLine($"Item changed to {e.GetToItem()?.Query<string>(StatisticType.Name)?.GetTypedValue()}");};

        inventorySlot.Item = creature;  // Item changed to Creature 1
        inventorySlot.Item = creature2; // Item changed to Creature 2
        inventorySlot.Item = null;      // Item changed to

        Statistic<int> reference = creature2.Query<int>(StatisticType.Health)!;
        Statistic<string> referenceName = creature2.Query<string>(StatisticType.Name)!;

        reference.OnValueChanged += (o, e) => {Console.WriteLine($"statistic health changed to {e.GetChangedTo()}");};

        Statistic statistic = new Statistic<int>(StatisticType.Health, 50);

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

        statisticTemplate.AddStatistics(
            new Statistic<CloneableInt>(StatisticType.Health, new CloneableInt(30)),
            new Statistic<CloneableInt>(StatisticType.Stamina, new CloneableInt(50)),
            new Statistic<CloneableInt>(StatisticType.Shield, new CloneableInt(100)),
            new Statistic<CloneableString>(StatisticType.Name, cloneableString)
        );

        Creature newCreature = new Creature();

        statisticTemplate.CopyStatistics(newCreature);

        Statistic<int> statisticCloneableInt = newCreature.Query<int>(StatisticType.Health)!;
        Console.Write("new creature health is: " + statisticCloneableInt.GetTypedValue() + " amount of statistics: " + newCreature.GetStatistics().ToArray().Length);
        statisticCloneableInt.SetValue(50);

        Console.WriteLine("creature name: " + newCreature.Query<string>(StatisticType.Name)!.GetTypedValue());
        name += "!";
        Console.WriteLine("creature name: " + newCreature.Query<string>(StatisticType.Name)!.GetTypedValue());

        Console.WriteLine("creature name: " + statisticTemplate.Query<CloneableString>(StatisticType.Name)!.GetTypedValue()!.GetValue());
        statisticTemplate.Query<CloneableString>(StatisticType.Name)!.GetTypedValue()!.SetValue("new creature name");
        Console.WriteLine("creature name: " + newCreature.Query<string>(StatisticType.Name)!.GetTypedValue());

        Console.WriteLine("creature name: " + statisticTemplate.Query<CloneableString>(StatisticType.Name)!.GetTypedValue()!.GetValue());
        }
}