using BackgroundJobService.DataProviders.Interfaces;
using System.Threading.Channels;

namespace BackgroundJobService.DataProviders
{
    public class ChannelBasedQueueProvider : IQueueProvider
    {
        private readonly Channel<string> _channel;

        public ChannelBasedQueueProvider()
        {
            this._channel = Channel.CreateUnbounded<string>();
        }

        public string ReadFromQueue()
        {
            var item = string.Empty;
            if (this._channel.Reader.Count > 0)
            {
                var read = this._channel.Reader.TryRead(out item);
                item = read ? item : string.Empty;
            }
            
            return item;
        }

        public void WriteToQueue(string value)
        {
            this._channel.Writer.TryWrite(value);
        }
    }
}
