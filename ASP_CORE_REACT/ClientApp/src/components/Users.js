import React, { Component } from 'react';

export class Users extends Component {
    static displayName = Users.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [], loading: true, newName: ''
        };

        this.onSubmitText = this.onSubmitText.bind(this);
        this.onChangeText = this.onChangeText.bind(this);

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
        this.setState({ newName: event.target.value });
    }

    onSubmitText(event) {
            fetch('api/Users/AddUserFetch', {
                method: "POST",
                body: JSON.stringify({
                    UserName: "Zbyszek", UserSurname: "Kromka"
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
                <Test/>
                <h1>Users list: </h1>
                {contents}
            </div>
        );
    }
}

const Test = () =>
    <div>
        <form action="api/Users/AddUser" method="post">
            <input type="text" name="name" placeholder="Name" />
            <input type="text" name="surname" placeholder="Surname" />
                <input type="submit" value="Sign Up"/>
        </form>
    </div>