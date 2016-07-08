using System;
using Akka.Actor;
using Akka.DI.Core;
using LautaroDragan.Samples.AutofacAkka.Weapons;

namespace LautaroDragan.Samples.AutofacAkka.Actors
{
    class AttackMessage { }

    class DamageMessage
    {
        public readonly int Damage;

        public DamageMessage(int damage)
        {
            Damage = damage;
        }
    }

    class MonsterDiedMessage
    {
        
    }

    class HeroActor : ReceiveActor
    {
        private readonly IWeapon _weapon;
        private readonly IActorRef _monster;

        private int enemyHealthPoints = 30;

        public HeroActor(IWeapon weapon)
        {
            // To get a reference to a dependency-injected actor from inside another actor, use the Context.DI().Props method
            _monster = Context.ActorOf(Context.DI().Props<MonsterActor>(), "monsterActor");
            _weapon = weapon;

            Receive<AttackMessage>(message => Attack());
            Receive<MonsterDiedMessage>(message => MonsterDied(message));
        }

        private void Attack()
        {
            Console.WriteLine("Attacking the enemy! ");
            _monster.Tell(new DamageMessage(_weapon.Damage));
        }

        private void MonsterDied(MonsterDiedMessage message)
        {
            var r = new Random();
            var experiencePoints = r.Next(8, 13);

            Console.WriteLine("You have the defeated the enemy!");
            Console.WriteLine("Gained " + experiencePoints + " EXP");
            Console.WriteLine();
            Console.WriteLine("Press ESC to quit");

            Become(() => { });
        }
    }
}
