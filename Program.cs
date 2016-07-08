using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using LautaroDragan.Samples.AutofacAkka.Actors;

namespace LautaroDragan.Samples.AutofacAkka
{
    class Program
    {
        private static void Main(string[] args)
        {
            // This is a working example of doing Dependency Injection in an application
            // that uses the Akka.NET framework, using the Autofac DI framework.
            // 
            // We're using Akka.DI.AutoFac for this example (see https://github.com/akkadotnet/Akka.DI.AutoFac), 
            // but building a custom DependencyResolver could be a better idea.
            // 
            // Why Autofac?
            // See this post (https://petabridge.com/blog/top-7-akkadotnet-stumbling-blocks/) by Aaron Stannard
            // To sum it up, it says that "The only DI framework that works correctly by default with Akka.NET actors is Autofac.".
            // Of course it doesn't necessarily mean other frameworks won't work, but deadlines :)

            using (var actorSystem = ActorSystem.Create("HeroActorSystem"))
            {
                var resolver = InjectDependencies(actorSystem);

                // Get an instance of our HeroActor, having Akka.DI.Autofac take care of the Props
                var heroActor = actorSystem.ActorOf(resolver.Create<HeroActor>(), "HeroActor");

                Console.WriteLine("Press ESC to exit.");
                Console.WriteLine("Press any other key to attack the enemy!");
                Console.WriteLine();
                do
                {
                   heroActor.Tell(new AttackMessage());
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            }
        }

        private static IDependencyResolver InjectDependencies(ActorSystem actorSystem)
        {
            var builder = new ContainerBuilder();

            // Register either a Sword or a Bow with Autofac. Try using a Sword - it hits harder!

            builder.RegisterType<Weapons.Bow>().As<Weapons.IWeapon>(); // Comment this line to stop using the Bow!
            // builder.RegisterType<Weapons.Sword>().As<Weapons.IWeapon>(); // Uncomment this line to attack with Sword instead!

            // We need to register the Actor with the special constructor, too, 
            // so autofac takes care of passing the right dependencies
            builder.RegisterType<HeroActor>();

            builder.RegisterType<MonsterActor>();

            IContainer container = builder.Build();

            return new AutoFacDependencyResolver(container, actorSystem);

        }
    }
}
