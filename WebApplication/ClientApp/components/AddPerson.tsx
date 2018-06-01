import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import { Person } from './PersonData';


interface AddPersonDataState {
    title: string;
    loading: boolean;
    perData: Person;
    showMessage: boolean;
    message: string;
}

export class AddPerson extends React.Component<RouteComponentProps<{}>, AddPersonDataState> {
    constructor(props) { //props
        super(props); //props

        this.state = { title: "", loading: true, perData: new Person, showMessage: false, message:""};
      
        var personid = this.props.match.params["id"];

        //  console.warn(this.props.match.params["id"]);
        // This will set state for Edit person
        if (personid > 0) {
            fetch('api/Person/Get/' + personid)
                .then(response => response.json() as Promise<Person>)
                .then(data => {
                    this.setState({ title: "Manage Person | Edit Person", loading: false, perData: data, showMessage: false, message: ""});
                });
        }
     
        // This will set state for Add person
        else {
            this.state = { title: "Manage Person | Create Person", loading: false, perData: new Person, showMessage: false, message: ""};
        }

        // This binding is necessary to make "this" work in the callback
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();

        return <div>
            <h1>{this.state.title}</h1>

            <hr />
            {contents}
        </div>;
    }

    // This will handle the submit form event.
    private handleSave(event) {
        event.preventDefault();
    
        const data = new FormData(event.target);

        // PUT request for Edit person.
        if (this.state.perData.id) {
            fetch('api/Person/Edit', {
                method: 'PUT',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.setState({
                        showMessage: true,
                        message:"Person details has been updated successfully."
                    });

               //     this.props.history.push("/PersonData");
                })
        }

        // POST request for Add person.
        else {
            fetch('api/Person/Create', {
                method: 'POST',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.setState({
                        showMessage: true,
                        message: "Person details has been added successfully."
                    });


                    //   this.props.history.push("/PersonData");
                });event.target.reset();
        }
            
    }


    // This will handle Cancel button click event.
    private handleCancel(e: { preventDefault: () => void; }) {
        e.preventDefault();
        this.props.history.push("/PersonData");
    }

    // Returns the HTML Form to the render() method.
    private renderCreateForm() {
        var shown = {
            display: this.state.showMessage ? "block" : "none"
        };

        var hidden = {
            display: this.state.showMessage ? "none" : "block"
        }
        return (
            <form classID="formPerson" onSubmit={this.handleSave} >
                <div className="form-group" >
                    <input type="hidden" name="id" value={this.state.perData.id} />
                    <div className="alert alert-success fade in alert-dismissible" style={shown}>
                        <a href="#" className="close" data-dismiss="alert" aria-label="close" title="close">x</a>
                        <strong>Success!</strong> {this.state.message}
</div>
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="first_name"><sup>*</sup>First Name</label>
                    <div className="col-md-4">
                        <input className="form-control" maxLength={20} type="text" name="first_name" defaultValue={this.state.perData.first_name} required />
                    </div>
                </div >

                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="last_name" ><sup>*</sup>Last Name</label>
                    <div className="col-md-4">
                        <input className="form-control" maxLength={20} type="text" name="last_name" defaultValue={this.state.perData.last_name} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="phone" ><sup>*</sup>Phone(format: xxxx-xxx-xxxx)</label>
                    <div className="col-md-4">
                        <input className="form-control" maxLength={13} pattern="^[0-9]{4}-[0-9]{3}-[0-9]{4}$"  type="tel" name="phone" defaultValue={this.state.perData.phone} required />
                    </div>
                </div>

                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>&nbsp;
                    <button className="btn" onClick={this.handleCancel} >Cancel</button>
                </div >
            </form >
        )
    }
}

//to add and edit person detail in the form.
