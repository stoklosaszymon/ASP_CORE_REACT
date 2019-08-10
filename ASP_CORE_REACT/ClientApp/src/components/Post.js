import React, { Component } from 'react';
import './Comments.js';
import { Comments } from './Comments.js';


export class Post extends Component {
    constructor(props) {
        super(props);

        this.id = this.props.match.params.id;

        this.state = {
            post: []
        }
    }

    componentDidMount() {
        fetch(`api/Posts/GetPost/${this.id}`)
            .then(response => response.json())
            .then(data => this.setState(
                () => ({
                    post: data
                })
            ))
            .catch(err => console.log(err));
    }

    render() {
        return (
            <div>
                <h1>{this.state.post.title}</h1>
                <p>{this.state.post.content}</p>
                <div>
                    <Comments postId={this.id} />
                </div>
            </div> 
        );
    }
}