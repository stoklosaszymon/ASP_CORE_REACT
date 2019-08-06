import React, { Component } from 'react'

export class Login extends Component {

    constructor(props) {
        super(props);

        this.state = { login: '', password: '', logged: false };

        this.onSubmit = this.onSubmit.bind(this);
        this.onChange = this.onChange.bind(this);
    }


    onSubmit(event) {
        event.preventDefault();
        fetch('Login/SignIn', {
            method: 'POST',
            body: JSON.stringify({
                Email: this.state.login, Password: this.state.password
            }),
            headers: {
                'Content-type': 'application/json'
            }
        }).then(response => response.json())
            .then(response => this.setState({ logged: response }))
    }

    onChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    render() {
        let isLogged = this.state.logged ? <p>zalogowano</p> : <p>nie zalogowano</p>;
        return (
            <div>
                {isLogged}
                <form onSubmit={this.onSubmit}>
                    <input type="text" name="login" placeholder="Email" onChange={this.onChange}/>
                    <input type="password" name="password" placeholder="Password" onChange={this.onChange} />
                    <input type="submit" />
                </form>
            </div>
        );
    }
}