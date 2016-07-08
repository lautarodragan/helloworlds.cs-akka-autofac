using System;
using Akka.Actor;

namespace LautaroDragan.Samples.AutofacAkka.Actors
{
    class MonsterActor : ReceiveActor
    {
        private const int MaxHealthPoints = 30;
        private int _healthPoints = MaxHealthPoints;

        public MonsterActor()
        {
            Receive<DamageMessage>(message => ReceiveDamageMessage(message));
        }

        private void ReceiveDamageMessage(DamageMessage message)
        {
            _healthPoints -= message.Damage;

            if (!IsDead())
            {
                Console.WriteLine($"Caused {message.Damage} points of damage");
                Console.WriteLine($"The enemy has {_healthPoints}/{MaxHealthPoints} HP left.");
                Console.WriteLine();
            }
            else
            {
                Sender.Tell(new MonsterDiedMessage());
            }
        }

        private bool IsDead()
        {
            return _healthPoints < 1;
        }
    }
}
