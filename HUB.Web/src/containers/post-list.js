import React, { Component } from 'react';
import { connect } from 'react-redux';
//import  {selectBook}  from '../actions/index';
import { bindActionCreators } from 'redux';
import { getPosts, readPost } from '../actions/index'

class PostList extends Component {
    constructor(props) {
        super(props);
        this.state = { posts: [] };
    }
    componentDidMount() {
        this.props.getPosts();
    }

    renderList() {
        return this.props.posts.map((post) => {
            return (
                <div className="" key={post.postId}>
                    <div className="row border border-dark rounded post-panel">
                        <div className="co2-sm-3 col-md-3 col-lg-1">
                            <img src="/images/profilePhoto.png" className='post-image rounded-circle' />
                        </div>
                        <div className="col-sm-7 col-md-7 col-lg-10">{post.content}</div>
                        <div className="col-sm-2 col-md-2 col-lg-1 read-button">
                            <button type="button" className="btn btn-primary"
                                onClick={() => this.props.readPost(post.postId)}
                            >Read</button>
                        </div>
                    </div>
                    <div className="post-spliter"></div>
                </div>
            )
        });
    }
    render() {
        return (
            <div>
                <div className="row">
                <div className="col-sm-7 col-md-6 col-lg-9"></div>
                <div className='col-sm-5 col-md-6 col-lg-3 posts-lenght'>
                    Number of un-read Post is ({this.props.posts.length})
                </div>
                </div>
                {this.renderList()}
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        posts: state.posts,
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getPosts, readPost }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PostList);