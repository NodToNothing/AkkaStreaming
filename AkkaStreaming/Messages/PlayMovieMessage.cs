namespace AkkaStreaming.Messages
{
	class PlayMovieMessage
	{
		//keep setters private so they stay immutable
		//use constructor parameters
		//Movie Message message
		public string MovieTitle { get; private set; }
		public int UserID { get; private set; }

		public PlayMovieMessage(string movieTitle, int userID)
		{
			MovieTitle = movieTitle;
			UserID = userID;

		}
	}
}
