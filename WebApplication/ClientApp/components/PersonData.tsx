import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchPersonState {
    title: string;
    persons: Person[];
    loading: boolean;
    showMessage: boolean;
    message: string;
}

export class PersonData extends React.Component<RouteComponentProps<{}>, FetchPersonState> {
    constructor() {
        super();
        this.state = { title: "Manage Persons", persons: [], loading: true, showMessage: false, message: "" };

        fetch('api/Person/List')//gets the data from the database
            .then(response => response.json() as Promise<Person[]>)
            .then(data => {
                this.setState({ persons: data, loading: false, showMessage: false, message: "" });
            });
        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);  
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderPersonTable(this.state.persons);

        return <div>
            <h1>{this.state.title}</h1>
           
            <div className="addbutton" > <a href="/Person/Add" className="btn btn-info btn-sm">
                <span className="glyphicon glyphicon-plus"></span> Create New
        </a></div> 
          
                {contents}
          
        </div>;
    }
    // Handle Delete request for an employee  
    private handleDelete(id: number) {
        if (!confirm("Do you want to delete ? "))
            return;
        else {
            fetch('api/Person/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        persons: this.state.persons.filter((rec) => {
                            return (rec.id != id);
                        }), showMessage: true,
                       message: "Person has been deleted successfully."
                    });
            });
        }
    }
    private  handleEdit(id: number) {
        this.props.history.push("/Person/Edit/" + id);
    }  
    private renderPersonTable(persons: Person[]) {
        var shown = {
            display: this.state.showMessage ? "block" : "none"
        };

        var hidden = {
            display: this.state.showMessage ? "none" : "block"
        }
        return<div><div className="alert alert-success fade in alert-dismissible" style={shown}>
            <a href="#" className="close" data-dismiss="alert" aria-label="close" title="close">x</a>
            <strong>Success!</strong> {this.state.message}
        </div>

        <table className='table'>
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Phone</th>
                    <th>Edit</th>
                    <th>Delete</th>
                </tr>
            </thead>
            <tbody>
                {persons.map(person =>
                    <tr key={person.id}>
                        <td>{person.first_name}</td>
                        <td>{person.last_name}</td>
                        <td>{person.phone}</td>
                        <td>
 
                            <a className="btn btn-default btn-sm" onClick={(id) => this.handleEdit(person.id)}>
                            <span className="glyphicon glyphicon-edit"></span> Edit
        </a></td>
                        <td><a onClick={(id) => this.handleDelete(person.id)}  className="btn btn-default btn-sm">
                            <span className="glyphicon glyphicon-remove-circle"></span> Delete
        </a></td>
                    </tr>
                )}
                </tbody>
            </table></div>;
    }
}

export class Person {
    id: number = 0;  
    first_name: string = "";
    last_name: string = "";
    phone: string = "";
}
