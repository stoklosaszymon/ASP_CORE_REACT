import React, { Component } from 'react';

export class Users extends Component {
    static displayName = Users.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [], loading: true,
        };

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
                    <PrintList list={this.state.users} onDelete={ this.onDelete } />
                </tbody>
            </table>
        );
    }

    onChangeText(event) {
        this.setState({ [event.target.name]: event.target.value });
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
        }).then( () => this.fetchUsersData() )
    }

    render() {
        let contents = this.state.loading
            ? <p>Loading...</p>
            : this.renderUsersTable(this.state.users);

        return (
            <div>
                <h1>Users list: </h1>
                {contents}
            </div>
        );
    }
}

const PrintList = ({ list, onDelete }) => 
    list.map(user =>
        <tr key={user.userId}>
            <td>{user.userId}</td>
            <td>{user.userName}</td>
            <td>{user.userSurname}</td>
            <td>
                <button onClick={() => onDelete(user.userId)}>Remove</button>
            </td>
        </tr>
    )