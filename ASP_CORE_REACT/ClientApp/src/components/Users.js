import React, { Component } from 'react';

export class Users extends Component {
    static displayName = Users.name;

    constructor(props) {
        super(props);
        this.state = { users: [], loading: true };

        fetch('api/Users/GetUsers')
            .then(response => response.json())
            .then(data => {
                this.setState({ users: data, loading: false });
            });
    }

    static renderUsersTable(users) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Surname</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        users.map(user =>
                            <tr key={user.userId}>
                                <td>{user.userId}</td>
                                <td>{user.userName}</td>
                                <td>{user.userSurname}</td>
                            </tr>
                        )              
                    }
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Users.renderUsersTable(this.state.users);

        return (
            <div>
                <h1>Users list: </h1>
                {contents}
            </div>
        );
    }
}