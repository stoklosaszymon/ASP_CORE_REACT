import React, { Component } from 'react';


export class Post extends Component {
    constructor(props) {
        super(props);

        this.state = {
            post: []
        }
    }

    componentDidMount() {
        const { id } = this.props.match.params
        fetch('api/Posts/GetPost',
            {
                method: 'POST',
                body: JSON.stringify({
                    PostId: id
                }),
                headers: {
                    'Content-type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(data => {
                this.setState({ post: data });
            })
            .catch(err => console.log(err));
    }

    render() {
        return (
            <div>
                <h1>{this.state.post.title}</h1>
                <p>{this.state.post.content}</p>
            </div>
        );
    }
}