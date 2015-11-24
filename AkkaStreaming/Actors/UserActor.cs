using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaStreaming.Messages;


namespace AkkaStreaming.Actors

{
	public class UserActor: ReceiveActor	
	{

		private string _currentlyWatching;

		public UserActor()
		{
			Console.WriteLine("Creating a UserActor");
			Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
			Receive<StopMovieMessage>(message => HandleStopMovieMessage());
		}

		private void HandlePlayMovieMessage(PlayMovieMessage message)
		{
			if (_currentlyWatching != null)
			{
				ColorConsole.WriteLineRed("Error: Cannot start playing another movie before stopping the existing one");
			}
			else
			{
				StartPlayingMovie(message.MovieTitle);
			}
		}

		private void StartPlayingMovie(string title)
		{
			_currentlyWatching = title;

			ColorConsole.WriteLineYellow(string.Format("User is currently watching '{0}'", _currentlyWatching));
		}

		private void HandleStopMovieMessage()
		{
			if (_currentlyWatching == null)
			{
				ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
			}
			else
			{
				StopPlayingCurrentMovie();
			}
		}

		private void StopPlayingCurrentMovie()
		{
			ColorConsole.WriteLineYellow(string.Format("User has stopped watching '{0}'", _currentlyWatching));

			_currentlyWatching = null;
		}

		protected override void PreStart()
		{
			//base.PreStart();
			ColorConsole.WriteLineGreen("Playback Actor PreStart");
		}

		protected override void PostStop()
		{
			//base.PostStop();
			ColorConsole.WriteLineGreen("User Actor PostStop");
		}

		protected override void PreRestart(Exception reason, object message)
		{
			ColorConsole.WriteLineGreen("User actor PreRestart because: " + reason);
			//has implementation, so keep
			base.PreRestart(reason, message);
		}

		protected override void PostRestart(Exception reason)
		{
			ColorConsole.WriteLineGreen("User actor PostRestart because: " + reason);

			base.PostRestart(reason);
		}
	}
}
