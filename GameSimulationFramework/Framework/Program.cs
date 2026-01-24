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

        Console.WriteLine($"Comparison test: {new Component<int>(ComponentType.Health, 100) < new Component<int>(ComponentType.Health, 23)}");
    }
}