import React, { Component } from 'react';
//import { Container } from 'reactstrap';
//import { NavMenu } from './NavMenu/NavMenu';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className="ml-5 mr-5">
                {this.props.children}
            </div>
        );
    }
}
