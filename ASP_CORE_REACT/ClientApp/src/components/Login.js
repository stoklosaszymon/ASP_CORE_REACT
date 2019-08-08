import React, { Component } from 'react'
import { connect } from 'react-redux';

class Login extends Component {

    constructor(props) {
        super(props);

        this.state = { login: '', password: ''};

        this.onSubmit = this.onSubmit.bind(this);
        this.onChange = this.onChange.bind(this);
    }


    onSubmit(event) {
        const { onLogIn } = this.props;
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
            .then(response => response ? onLogIn() : false)
    }

    onChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    render() {
        const { logged, onLogOut } = this.props;
        let isLogged = logged ? <button onClick={onLogOut}>wyloguj</button> : <p>zaloguj sie</p>;
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

const mapStateToProps = (state) => {
    return { logged: state.logged };
};
const mapDispatchToProps = (dispatch) => {
    return {
        onLogIn: () => dispatch({ type: 'LOG_IN' }),
        onLogOut: () => dispatch({ type: 'LOG_OUT' }),
    }
};

export default Login = connect(mapStateToProps, mapDispatchToProps)(Login);