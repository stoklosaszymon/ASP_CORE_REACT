import React, { Component } from 'react';
import { connect } from 'react-redux';

export class Comments extends Component {
    constructor(props) {
        super(props);

        this.id = props.postId;

        this.state = {
            comments: [], newComment: []
        };

        this.onChangeNewComment = this.onChangeNewComment.bind(this);
        this.onSubmitNewComment = this.onSubmitNewComment.bind(this);
    }

    componentDidMount() {
        this.fetchCommentsForPost(this.id);
    }

    fetchCommentsForPost(id) {
        fetch(`api/Comments/GetCommentsForPost/${id}`)
            .then(response => response.json())
            .then(data => this.setState({ comments: data }))
            .catch( err => console.log(err))
    }   

    onChangeNewComment(event) {
        this.setState({ newComment: event.target.value });
    }

    onSubmitNewComment(event) {
        event.preventDefault();
        fetch('Comments/AddCommentToPost', {
            method: 'POST',
            body: JSON.stringify({
                PostId: this.id, CommentContent: this.state.newComment, UserId: this.props.loggedUserId 
            }),
            headers: {
                'Content-type': 'application/json'
            }
        }).then( () => this.fetchCommentsForPost(this.id) ) 
           .catch(err => console.log(err))
    }

    render() {
        const { logged } = this.props;
        return (
            <div>
                {
                    logged && <AddComment content={this.state.newComment} onChange={this.onChangeNewComment} onSubmit={this.onSubmitNewComment} />
                }             
                <RenderComments comments={this.state.comments} />
            </div>
        );
    }
}

const AddComment = ({ content, onSubmit, onChange }) => 
    <div>
        <form onSubmit={onSubmit}>
            <input type="text" onChange={onChange} value={content} />
         </form>
    </div>


const RenderComments = ({ comments }) =>
        comments.map(com =>
            <div key={com.commentId}>
                <h3>posted by {com.userId}</h3>
                <p>{com.commentContent}</p>
            </div>
    )

const mapStateToProps = (state) => {
    return { logged: state.logged, loggedUserId: state.loggedUserId };
};

export default Comments = connect(mapStateToProps)(Comments);