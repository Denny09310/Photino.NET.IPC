export interface IChannel {
  readonly name: string;
}

export class Channel<T> implements IChannel {
  readonly name!: string;

  constructor(name: string) {
    this.name = name;
  }

  sendMessage(data: T) {
    const message = { key: this.name, data };
    window.external.sendMessage(JSON.stringify(message));
  }

  receiveMessage(callback: (data: T) => void) {
    window.external.receiveMessage((message) => {
      var received = JSON.parse(message) as { key: string; data: T };
      if (received.key === this.name) {
        callback(received.data);
      }
    });
  }
}

declare global {
  interface Window {
    readonly external: External;
  }

  interface External {
    sendMessage: (message: string) => void;
    receiveMessage: (callback: (message: string) => void) => void;
  }
}
