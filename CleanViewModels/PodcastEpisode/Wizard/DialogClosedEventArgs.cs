using System;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class DialogClosedEventArgs : EventArgs
    {
        public bool Finished { get; set; }
    }

    public delegate void DialogClosedHandler(object sender, DialogClosedEventArgs args);
}
