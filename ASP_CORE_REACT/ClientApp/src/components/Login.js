import React, { Component } from 'react'

export class Login extends Component {

    constructor(props) {
        super(props);

        this.state = { login: '', passwordHash: '', looged: false };

        this.onSubmit = this.onSubmit.bind(this);
        this.onChange = this.onChange.bind(this);
    }


    onSubmit() {
        fetch('Login/SignIn', {
            method: 'POST',
            body: JSON.strigify({ Email: this.state.login, PasswordHash: this.state.passwordHash }),
            headers: {
                'Content-type': 'application/json'
            }
        }).then(response => this.setState({ logged: true }));
    }

    onChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    render() {
        return (
            <div>
                <p> {this.state.logged} </p>
                <form onSubmit={this.onSubmit}>
                    <input type="text" name="login" placeholder="Email" onChange={this.onChange}/>
                    <input type="text" name="password" placeholder="Password" onChange={this.onChange}/>
                </form>
            </div>
        );
    }
}