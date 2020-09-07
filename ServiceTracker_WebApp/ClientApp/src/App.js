import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
//import { Home } from './components/Home';
import { SvcContainer } from './components/Svc/SvcContainer';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faStroopwafel, faSearch, faPlus, faCopy, faMinus, faAngleDoubleUp, faLock, faLockOpen} from '@fortawesome/free-solid-svg-icons'

library.add(faStroopwafel, faSearch, faPlus, faCopy, faMinus, faAngleDoubleUp, faLock, faLockOpen)

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={SvcContainer} />
            </Layout>
        );
    }
}
