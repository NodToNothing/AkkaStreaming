using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaStreaming.Messages;

namespace AkkaStreaming.Actors
{
	public class PlaybackActor : ReceiveActor //UntypedActor
	{

		//Constructor
		public PlaybackActor()
		{
			Console.WriteLine("Scooter's Playback Actor");

			//ReceiveActor - use the type I defined
			//have to give it a method
			//Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserID == 1234);
			Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
		}

		private void HandlePlayMovieMessage(PlayMovieMessage message)
		{
			ColorConsole.WriteLineYellow(string.Format("PlayMovieMessage '{0}' for user {1}", message.MovieTitle, message.UserID));
			//Console.WriteLine("Movie Title Receive Base Class: " + message.MovieTitle);
			//Console.WriteLine("User ID Receive Base Class: " + message.UserID);

		}

		protected override void PreStart()
		{
			//base.PreStart();
			ColorConsole.WriteLineGreen("Playback Actor PreStart");
		}

		protected override void PostStop()
		{
			//base.PostStop();
			ColorConsole.WriteLineGreen("Playback Actor PostStop");
		}

		protected override void PreRestart(Exception reason, object message)
		{
			ColorConsole.WriteLineGreen("Playback actor PreRestart because: " + reason);
			//has implementation, so keep
			base.PreRestart(reason, message);
		}

		protected override void PostRestart(Exception reason)
		{
			ColorConsole.WriteLineGreen("Playback actor PostRestart because: " + reason);

			base.PostRestart(reason);
		}

		//this is mandatory for Untyped
		//he was a lot faster using ReSharper
		/* protected override void OnReceive(object message)
		{
			if(message is string)
			{
				Console.WriteLine("Received movie title:" + message);
			}
			else if (message is int)
			{
				Console.WriteLine("Movie ID: " + message);
			}
			else if (message is PlayMovieMessage)
			{
				//intermediary variable to strongly type
				var m = message as PlayMovieMessage;
				Console.WriteLine("Movie Title: " + m.MovieTitle);
				Console.WriteLine("User ID: " + m.UserID);
			}
			else
			{
				//doesn't do anything in the Console
				Unhandled(message);
			}

			//throw new NotImplementedException();
		} */
	}
}
