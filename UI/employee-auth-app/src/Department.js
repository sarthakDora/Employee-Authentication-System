import React,{Component} from 'react';
import {Table} from 'react-bootstrap'
import {Button, ButtonToolbar} from 'react-bootstrap'
import {AddDeptModal} from './AddDeptModal'
import {EditDeptModal} from './EditDeptModal'

export class Department extends Component {

    constructor(props){
        super(props);
        this.state={deps:[], addModalShow:false, editModalShow:false}
    }

    refreshList()
    {
        fetch(process.env.REACT_APP_API+'department')
        .then(response=>response.json())
        .then(data =>{this.setState({deps:data})});
    }

    componentDidMount(){
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }


    deleteDep(depid, depName){
        if(window.confirm("Are you sure you want to delete "+depName+ "?"))
        {
            fetch(process.env.REACT_APP_API+'department/'+depid,{
                method:'DELETE',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json'}
             
            })
  
        }
    }
    render(){
        const { deps, depid, depname} = this.state;
        let addModalClose=()=>this.setState({addModalShow:false});
        let editModalClose=()=>this.setState({editModalShow:false});
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>DepartmentId</th>
                            <th>DepartmentName</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {deps.map(dep =>
                        <tr key={dep.DepartmentId}>
                            <td>{dep.DepartmentId}</td>
                            <td>{dep.DepartmentName}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button 
                                    className="mr-2" variant="info"
                                     onClick={()=>this.setState({editModalShow:true, depid:dep.DepartmentId, depname:dep.DepartmentName})}>Edit</Button>
                                     <Button 
                                    className="mr-2" variant="danger"
                                     onClick={()=>this.deleteDep(dep.DepartmentId, dep.DepartmentName)}>Delete</Button>
                                    <EditDeptModal 
                                    show={this.state.editModalShow} 
                                    onHide={editModalClose} 
                                    depid={depid}
                                    depname={depname}
                                    ></EditDeptModal>
                  
                                   
                                </ButtonToolbar>
                            </td>
                        </tr>
                        )}
                    </tbody>
                </Table>

                <ButtonToolbar>
                    <Button variant="primary" 
                    onClick={()=>this.setState({addModalShow:true})}>Add Department</Button>
                    <AddDeptModal show={this.state.addModalShow} onHide={addModalClose}></AddDeptModal>
                </ButtonToolbar>

            </div>)
        }
}