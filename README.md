# cs-autofac-akka
Sample Akka.NET + Autofac implementation in C#

This is a working example of doing Dependency Injection in an application that uses the Akka.NET framework, using the Autofac DI framework.
 
We're using [Akka.DI.AutoFac](https://github.com/akkadotnet/Akka.DI.AutoFac) for this example, but building a custom DependencyResolver could be a better idea.
 
## Running the app
This sample works like a basic RPG battle.
When you run the app, you'll see two messages on the screen:
>Press ESC to exit.
>Press any other key to attack the enemy!

Go on ahead and press the space key (any key will do but I like the spacebar :) and then the following message will appear:
>Attacking the enemy!  
>Caused 4 points of damage.  
>The enemy has 26/30 HP left.

Keep "attacking" repeatedly until the enemy perishes and you gain those yearned experience points.

>Attacking the enemy!  
>You have the defeated the enemy!  
>Gained 11 EXP

Congratulations! You have defeated the enemy ;)

## Why Autofac?
See [The Top 7 Mistakes Newbies Make with Akka.NET](https://petabridge.com/blog/top-7-akkadotnet-stumbling-blocks/) by Aaron Stannard.

To sum it up, it says that 
>The only DI framework that works correctly by default with Akka.NET actors is Autofac

Of course it doesn't necessarily mean other frameworks won't work, but deadlines :)
