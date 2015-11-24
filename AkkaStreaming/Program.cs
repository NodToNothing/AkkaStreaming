using System;
using Akka.Actor;
using AkkaStreaming.Actors;  //acces to PlaybackActor object
using AkkaStreaming.Messages; //access to Message objects


namespace AkkaStreaming
{
	class Program
	{
		private static ActorSystem AkkaStreamingActorSystem;

		static void Main(string[] args)
		{

			//making a change to trigger GitHub
			//desktop app.

			AkkaStreamingActorSystem = ActorSystem.Create("AkkaStreamingActorSystem");
			Console.WriteLine("Actor System was Created named AkkaStreamingActorSystem.");

			//need props
			//use the factory generic
			//props control env and other details.
			//Props playbackActorProps = Props.Create<PlaybackActor>();

			//common ref
			//the second param is optional (you'll get a name if you don't give it one).
			//so far what we have is a glorified new - the details are under the covers, lot of arch hiding there
			//IActorRef playbackActorRef = AkkaStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

			//update to program
			Props userActorProps = Props.Create<UserActor>();
			IActorRef userActorRef = AkkaStreamingActorSystem.ActorOf(userActorProps, "UserActor");

			//SEND SOME MESSAGES
			//playbackActorRef.Tell("Scott the Movie");
			//playbackActorRef.Tell(1234);
			//playbackActorRef.Tell('C'); //unhandled

			Console.ReadKey();
			Console.WriteLine("Sending a Playmoviemessage (Scooter's Big Big Movie)");
			userActorRef.Tell(new PlayMovieMessage("Scooter's Big Big Movie", 1234));

			Console.ReadKey();
			Console.WriteLine("Sending a Playmoviemessage (Scooter's Big Big Movie II)");
			userActorRef.Tell(new PlayMovieMessage("Scooter's Big Big Movie II", 25));

			Console.ReadKey();
			Console.WriteLine("Sending a StopMovieMessage");
			userActorRef.Tell(new StopMovieMessage());

			Console.ReadKey();
			Console.WriteLine("Sending another StopMovieMessage");
			userActorRef.Tell(new StopMovieMessage());



			/* playbackActorRef.Tell(new PlayMovieMessage("Scooter's Big Big Movie", 1234));
			playbackActorRef.Tell(new PlayMovieMessage("Scooter's Big Big Movie II", 25)); //shouldn't catch this one because of the predicate
			playbackActorRef.Tell(new PlayMovieMessage("Scooter's Big Big Movie III", 68));
			playbackActorRef.Tell(new PlayMovieMessage("Scooter's Not So Big Movie", 15)); */

			//poison pill
			//after, so it will finish the four messages
			//triggers playbackactors's poststop
			//playbackActorRef.Tell(PoisonPill.Instance);

			Console.ReadKey();

			AkkaStreamingActorSystem.Shutdown();
			AkkaStreamingActorSystem.AwaitTermination();  //pauses main thread until actor system shut down complete
			Console.WriteLine("Actor System Shutdown");

			Console.ReadKey();
		}
	}
}
