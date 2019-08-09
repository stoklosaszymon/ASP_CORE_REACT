import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Users } from './components/Users';
import  Posts  from './components/Posts';
import { Post } from './components/Post';
import { Register } from './components/Register'

export default class App extends Component {
    static displayName = App.name;

    render() {

      return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/users' component={Users} />
        <Route path='/posts' component={Posts} />
        <Route path='/post/:id' component={Post} />
        <Route path='/register' component={Register} />

      </Layout>
    );
  }
}



