import React from 'react';
import './App.css';
import {Home}  from './Home';
import {Department}  from './Department';
import {Employee}  from './Employee';
import {Navigation}  from './Navigation';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  BrowserRouter
} from "react-router-dom";


function App() {
  return (
    <BrowserRouter >
    <div className="App">
     <h1 className="m-3 d-flex justify-content-center">Employee Authentication App</h1>
    <Navigation/>
    <Switch>
      <Route path="/" component={Home} exact/>
      <Route path="/employee" component={Employee} exact/>
      <Route path="/department" component={Department} exact/>
      

    </Switch>


    </div>
    </BrowserRouter>
  );
}

export default App;
