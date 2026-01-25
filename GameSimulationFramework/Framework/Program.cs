using Framework.Game.BaseTypes;
using Framework.Game.Teams;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Abilities;
using Framework.Game.Teams.Creatures.Components;
class Program
{
    static void Main()
    {
        // make random component template
        RandomTemplate<Creature> template = (RandomTemplate<Creature>) new RandomTemplate<Creature>()
        .AddComponents(
            new Component<ComponentRange<CloneableValue<string>>>(
                ComponentType.Name, 
                new ComponentRange<CloneableValue<string>>(
                        new List<CloneableValue<string>>()
                        {
                            new CloneableValue<string>("Lizard"),
                            new CloneableValue<string>("Dragon"),
                            new CloneableValue<string>("Arcane Golem"),
                            new CloneableValue<string>("Holy spirit")
                        }
                    )),

            new Component<ComponentRange<CloneableValue<float>>>(
                ComponentType.Health, 
                new ComponentRange<CloneableValue<float>>(
                    new CloneableLazyValueRange(50, 65, 1)
                    )),

            new Component<ComponentRange<CloneableValue<float>>>(
                ComponentType.Stamina, 
                new ComponentRange<CloneableValue<float>>(
                    new CloneableLazyValueRange(45, 55, 1)
                    )),

            new Component<ComponentRange<CloneableValue<Ability>>>(
                ComponentType.Abilities, 
                new ComponentRange<CloneableValue<Ability>>(
                        new List<CloneableValue<Ability>>()
                        {
                            new CloneableValue<Ability>(new BasicAttack(10)),
                            new CloneableValue<Ability>(new BasicAttack(15)),
                            new CloneableValue<Ability>(new BasicAttack(8)),
                            new CloneableValue<Ability>(new BasicAttack(23))
                        }
                    ))
        );

        ComponentTemplate<Creature> componentTemplate = template.RealizeTemplate();
        Creature creature = new();
        componentTemplate.CopyComponents(creature);

        Team team = new(template, "Player", 4, 8, 8);
        team.PrintTeam();

        Console.WriteLine($"Comparison test: {new Component<CloneableInt>(ComponentType.Health, new CloneableInt(10)) < new Component<CloneableInt>(ComponentType.Health, new CloneableInt(555))}");
        List<CloneableLazyValueRange> cloneableFloats = new()
        {
            new CloneableLazyValueRange(0, 10, 1),
            new CloneableLazyValueRange(5, 10, 1),
            new CloneableLazyValueRange(1, 2, 1),
            new CloneableLazyValueRange(1.5f, 1.95f, 1),
            new CloneableLazyValueRange(3, 5, 1),
            new CloneableLazyValueRange(25, 50, 1),
            new CloneableLazyValueRange(-103, -32, 1),
            new CloneableLazyValueRange(-95, -32, 1),
            new CloneableLazyValueRange(-95, -32, 1),
            new CloneableLazyValueRange(5, 23, 1)
        };
        cloneableFloats.Sort();
        cloneableFloats.ForEach(e => Console.WriteLine(e));    
    }
}