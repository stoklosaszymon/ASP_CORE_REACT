import React, { Component } from 'react';

export class Comments extends Component {
    constructor(props) {
        super(props);

        this.id = props.postId;

        this.state = {
            comments: [], newComment: []
        };
    }

    componentDidMount() {
        this.fetchCommentsForPost(this.id);
    }

    fetchCommentsForPost(id) {
        fetch('Comments/GetCommentsForPost', {
            method: 'POST',
            body: JSON.stringify({
                PostId: this.id
            }),
            headers: {
                'Content-type': 'application/json'
            }})
        .then(response => response.json())
        .then(data => this.setState({ comments: data }))
        .catch( err => console.log(err))
    }

    render() {
        return (
            this.state.comments.map(com =>
                <div key={com.commentId}>
                    <p>{com.commentContent}</p>
                </div>
            )
        );
    }
}