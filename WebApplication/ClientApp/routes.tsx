import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { PersonData } from './components/PersonData';
import { AddPerson } from './components/AddPerson';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/PersonData' component={PersonData} />
    <Route path='/Person/Add' component={AddPerson} />
    <Route path='/Person/Edit/:id' component={AddPerson} />
</Layout>;

//so this create the bridge between database and view
