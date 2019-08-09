import React, { Component } from 'react';

export class Register extends Component {
    constructor(props) {
        super(props);

        this.state = { email: '', password: '', name: '', surname: '', status: '' }

        this.onChange = this.onChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    onChange(event) {
        this.setState({ [event.target.name] : event.target.value });
    }

    onSubmit(event) {
        event.preventDefault();
        fetch('Register/AddUser',
                {
                    method: 'POST',
                    body: JSON.stringify({
                        UserName: this.state.name,
                        UserSurname: this.state.surname,
                        Email: this.state.email,
                        PasswordHash: this.state.password
                    }),
                    headers: {
                        'Content-type': 'application/json'
                    }
                })
                .then(response => response.json())
                .then(data => console.log(data));
    }

    render() {
        return (
            <RegisterForm onSubmit={this.onSubmit} onChange={this.onChange} />  
         )
    }
}

const RegisterForm = ({ onSubmit, onChange }) =>
    <form onSubmit={onSubmit}>
        <p>Name: </p><input type="text" name='name' onChange={onChange}/>
        <p>Surname: </p> <input type="text" name='surname' onChange={onChange}/>
        <p>Email: </p><input type="text" name='email' onChange={onChange}/>
        <p>Password: </p> <input type="password" name='password' onChange={onChange}/>
        <input type="submit" value="Create account" />
    </form> 