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
    }

    componentDidMount() {
        this.fetchPosts();
    }

    componentDidUpdate() {
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

    render() {
        let list = this.state.posts.count === 0 ? <h2>Loading...</h2> : <RenderPosts posts={this.state.posts} />
        return (
            <div className="container">
                <div className="addPostContainer">
                <AddNewPost onChange={this.onChangeText} onSubmit={this.onSubmitPost}/>
                </div>
                <div className="postListContainer">
                    {list}
                </div>    
            </div>
        );
    }
}

let stringCutter = (text, count) => {
    return text.length > count ? text.substring(0, text.substring(0, count).lastIndexOf('.') ) + '...' : text;
}

const RenderPosts = ({ posts, }) =>
    posts.map(post =>
        <div key={post.postId} className="post">
            <a href={'/post/' + post.postId}>
                <h3>{post.title}</h3>
                <article> {stringCutter(post.content, 200)} </article>
            </a>
        </div>
    );

const AddNewPost = ({ onChange, onSubmit }) =>
        <form onSubmit={onSubmit}>
            <div className="addPostChild">
                <input type="text" onChange={onChange} name="newTitle" placeholder="Title" />
            </div>
            <div className="addPostChild">
                <textarea name="newContent" onChange={onChange} placeholder="Content..." />
            </div>
            <div className="addPostChild">
                <input type="submit" className="submit" />
            </div>
        </form>