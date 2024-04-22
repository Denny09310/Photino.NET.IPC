import { Channel, IChannel } from "./channel";

class ChannelManager {
  private channels: Map<string, IChannel> = new Map();

  createChannel<T>(name: string, handler: (data: T) => void) {
    if (!this.channels.has(name)) {
      var channel = new Channel<T>(name);
      channel.receiveMessage(handler);
      this.channels.set(name, channel);
    }
  }

  getChannel<T>(name: string) {
    var channel = this.channels.get(name);
    if (channel) return channel as Channel<T>;

    throw new Error("No channel registered with that name");
  }
}

export default new ChannelManager();
