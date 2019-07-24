import React, { Component } from 'react';

export class Users extends Component {
    static displayName = Users.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [], loading: true, newName: '', newSurname: ''
        };

        this.onSubmitText = this.onSubmitText.bind(this);
        this.onChangeText = this.onChangeText.bind(this); 
    }

    componentDidMount() {
        this.fetchUsersData();
    }

    fetchUsersData() {
        fetch('api/Users/GetUsers')
            .then(response => response.json())
            .then(data => {
                this.setState({ users: data, loading: false });
            });
    }

    componentDidUpdate() {
        this.fetchUsersData();
    }

    renderUsersTable(users) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Surname</th>
                        <th>Delete User</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        users.map(user =>
                            <tr key={user.userId}>
                                <td>{user.userId}</td>
                                <td>{user.userName}</td>
                                <td>{user.userSurname}</td>
                                <td>
                                    <button onClick={() => this.onDelete(user.userId) }>Remove</button>
                                </td>
                            </tr>
                        )              
                    }
                </tbody>
            </table>
        );
    }

    onChangeText(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    onSubmitText(event) {
            fetch('api/Users/AddUser', {
                method: "POST",
                body: JSON.stringify({
                    UserName: this.state.newName, UserSurname: this.state.newSurname
                }),
                headers: {
                    'Content-type': 'application/json'
                }
            })
        event.preventDefault();
    }

    onDelete(id) {
        fetch('api/Users/RemoveUser', {
            method: "POST",
            body: JSON.stringify({
                UserName: '-', UserSurname: '-', UserId: parseInt(id, 10)
            }),
            headers: {
                'Content-type': 'application/json'
            }
        })
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderUsersTable(this.state.users);

        return (
            <div>
                <NewUserInput onChange={this.onChangeText} onSubmit={this.onSubmitText} />
                <h1>Users list: </h1>
                {contents}
            </div>
        );
    }
}

const NewUserInput = ({ onChange, onSubmit }) =>
    <form onSubmit={ onSubmit }>
        <input type="text" name="newName" placeholder="Name" onChange={onChange}/>
        <input type="text" name="newSurname" placeholder="Surname" onChange={onChange} />
        <input type="submit" />
    </form>