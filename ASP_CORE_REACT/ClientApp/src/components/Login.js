import React, { Component } from 'react'
import { connect } from 'react-redux';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';


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
        })
        .then(response => response.json())
            .then(data => data !== 0 ? onLogIn(data) : false)
    }

    onChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }


    render() {
        const { logged, onLogOut} = this.props;
        const logForm =
            <div>
                <form onSubmit={this.onSubmit}>
                    <input type="text" name="login" placeholder="Email" onChange={this.onChange} />
                    <input type="password" name="password" placeholder="Password" onChange={this.onChange} />
                    <input type="submit" value='Login' />
                </form>
                <NavLink tag={Link} className="text-dark" to="/register">Create new account</NavLink>
             </div>;

        let isLogged = logged ? <button onClick={onLogOut}>wyloguj</button> : logForm;
        return (
            <div>
                {isLogged}
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return { logged: state.logged, loggedUserId: state.loggedUserId };
};
const mapDispatchToProps = (dispatch) => {
    return {
        onLogIn: (userId) => dispatch({ type: 'LOG_IN', loggedUserId: userId }),
        onLogOut: () => dispatch({ type: 'LOG_OUT' }),
    }
};

export default Login = connect(mapStateToProps, mapDispatchToProps)(Login);