import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';
import { connect } from 'react-redux';

export class Chat extends Component {
    constructor(props) {
        super(props);

        this.state = {
            nick: '',
            message: '',
            messages: [],
            hubConnection: null
        };

        this.onChange = this.onChange.bind(this);
    }

    componentDidMount = () => {
        const nick = this.props.logged && window.prompt('Your name:', this.props.loggedUserId.toString());

        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/chat")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this.setState({ hubConnection, nick }, () => {
            this.state.hubConnection
                .start()
                .then(() => console.log('Connection started!'))
                .catch(err => console.log('Error while establishing connection :('));

            this.state.hubConnection.on('ReceiveMessage', (nick, receivedMessage) => {
                const text = `${nick}: ${receivedMessage}`;
                const messages = this.state.messages.concat([text]);
                this.setState({ messages });
            });
        });
    }

    sendMessage = () => {
        this.state.hubConnection
            .invoke('SendMessage', this.state.nick, this.state.message)
            .catch(err => console.error(err));

        this.setState({ message: '' });
    }

    onChange(event) {
        this.setState({ message: event.target.value })
    }

    render() {
        return (
        <div>
            {
                this.props.logged && <ChatInput sendMessage={this.sendMessage} onChange={this.onChange} message={this.state.message} />
            }
                <ChatPanel messages={this.state.messages}/>
         </div>       
        );
    }
}

const ChatInput = ({ sendMessage, onChange, message }) => 
    <div>
        <br />
        <input
            type="text"
            value={message}
            onChange={onChange}
        />

        <button onClick={sendMessage}>Send</button>
    </div>

const ChatPanel = ({ messages }) => 
    <div>
        {messages.map((message, index) => (
            <span style={{ display: 'block' }} key={index}> {message} </span>
        ))}
</div>

const mapStateToProps = (state) => {
    return { logged: state.logged, loggedUserId: state.loggedUserId };
};

export default Chat = connect(mapStateToProps)(Chat);