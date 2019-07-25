import React, { Component } from 'react';
import './Posts.css';

export class Posts extends Component {
    constructor(props) {
        super(props);

        this.state = {
            posts: [], newTitle: '', newContent: ''
        }

        this.onChangeText = this.onChangeText.bind(this);
        this.onSubmitPost = this.onSubmitPost.bind(this);
        this.onClickPost = this.onClickPost.bind(this);
    }

    componentDidMount() {
        this.fetchPosts();
    }

    onChangeText(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    onSubmitPost(event) {
        fetch('api/Posts/AddPost', {
            method: "POST",
            body: JSON.stringify({
                Title: this.state.newTitle, Content: this.state.newContent
            }),
            headers: {
                'Content-type': 'application/json'
            }
        })
        event.preventDefault();
    }

    fetchPosts() {
        fetch('api/Posts/GetAllPosts')
            .then(response => response.json())
            .then(data => {
                this.setState({ posts: data });
            })
            .catch(err => console.log(err));
    }

    onClickPost = (id) => {
        
    }

    render() {
        return (
            <div>
            <AddNewPost onChange={this.onChangeText} onSubmit={this.onSubmitPost}/>
                 <RenderPosts posts={this.state.posts} />
            </div>

        );
    }
}

const RenderPosts = ({ posts, }) =>
    posts.map(post =>
        <div key={post.postId}>
            <a href={'/post/' + post.postId}>
                <h3>{post.title}</h3>
            </a>
        </div>
    );

const AddNewPost = ({ onChange, onSubmit }) =>
        <form className="container" onSubmit={ onSubmit }>
            <input className="input" type="text" onChange={ onChange } name="newTitle"/>
            <textarea name="newContent" onChange={onChange}/>
            <input type="submit" />
        </form>