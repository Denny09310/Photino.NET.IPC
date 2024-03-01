
const payload = { key: "test-channel", data: "Hello, World!" }

window.external.sendMessage(JSON.stringify(payload));
window.external.receiveMessage((message) => alert(JSON.parse(message).data))