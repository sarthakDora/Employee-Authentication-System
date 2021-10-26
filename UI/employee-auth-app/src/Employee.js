import React,{Component} from 'react';
import {Table} from 'react-bootstrap'
import {Button, ButtonToolbar} from 'react-bootstrap'
import {AddEmpModal} from './AddEmpModal'
import {EditEmpModal} from './EditEmpModal'

export class Employee extends Component {
    constructor(props){
        super(props);
        this.state={emps:[], addModalShow:false, editModalShow:false}
    }

    refreshList()
    {
        fetch(process.env.REACT_APP_API+'employee')
        .then(response=>response.json())
        .then(data =>{this.setState({emps:data})});
    }

    componentDidMount(){
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }


    deleteDep(empid, empname){
        if(window.confirm("Are you sure you want to delete "+empname+ "?"))
        {
            fetch(process.env.REACT_APP_API+'employee/'+empid,{
                method:'DELETE',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json'}
             
            })
  
        }
    }
    render(){
        const { emps, empid, empname} = this.state;
        let addModalClose=()=>this.setState({addModalShow:false});
        let editModalClose=()=>this.setState({editModalShow:false});
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>EmployeeId</th>
                            <th>EmployeeFirstName</th>
                            <th>EmployeeLastName</th>
                            <th>Date of Birth</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {emps.map(emp =>
                        <tr key={emp.EmployeeID}>
                            <td>{emp.EmployeeID}</td>
                            <td>{emp.FirstName}</td>
                            <td>{emp.LastName}</td>
                            <td>{emp.DateOfBirth}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button 
                                    className="mr-2" variant="info"
                                     onClick={()=>this.setState({editModalShow:true, empid:emp.EmployeeID, empname:emp.FirstName})}>Edit</Button>
                                     <Button 
                                    className="mr-2" variant="danger"
                                     onClick={()=>this.deleteDep(emp.EmployeeID, emp.FirstName)}>Delete</Button>
                                    <EditEmpModal 
                                    show={this.state.editModalShow} 
                                    onHide={editModalClose} 
                                    empid={empid}
                                    empname={empname}
                                    ></EditEmpModal>
                  
                                   
                                </ButtonToolbar>
                            </td>
                        </tr>
                        )}
                    </tbody>
                </Table>

                <ButtonToolbar>
                    <Button variant="primary" 
                    onClick={()=>this.setState({addModalShow:true})}>Add Employee</Button>
                    <AddEmpModal show={this.state.addModalShow} onHide={addModalClose}></AddEmpModal>
                </ButtonToolbar>

            </div>)
        }
}