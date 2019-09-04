import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';

export class Chat extends Component {
    constructor(props) {
        super(props);
        console.log(signalR.VERSION);
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/chat", { transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling })
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        this.hubConnection.on('ReceiveMessage', (nick, receivedMessage) => {
            const text = `${nick}: ${receivedMessage}`;
            const messages = this.state.messages.concat([text]);
            this.setState({ messages });
        });

        this.hubConnection
            .start({ transport: signalR.HttpTransportType.WebSockets })
            .then(() => console.log('Connection started!'))
            .catch(err => console.log(err));

        this.state = {
            nick: '',
            message: '',
            messages: [],
            hubConnection: null
        };
    }

    sendMessage = () => {
        this.hubConnection
            .invoke('SendMessage', this.state.nick, this.state.message)
            .catch(err => console.error(err));

        this.setState({ message: '' });
    }


    render() {
        return (
            <div>
                <br />
                <input
                    type="text"
                    value={this.state.message}
                    onChange={e => this.setState({ message: e.target.value })}
                />

                <button onClick={this.sendMessage}>Send</button>

                <div>
                    {this.state.messages.map((message, index) => (
                        <span style={{ display: 'block' }} key={index}> {message} </span>
                    ))}
                </div>
            </div>
        );
    }
}