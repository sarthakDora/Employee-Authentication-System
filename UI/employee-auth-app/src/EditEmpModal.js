import React, {Component, useState} from 'react'
import { Modal, Button, Row, Col, Form } from 'react-bootstrap'

export class EditEmpModal extends Component{

    constructor(props){
        super(props)
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'employee',
        {method:'PUT',
        headers:{
            'Accept':'application/json',
            'Content-Type':'application/json'},
        body:JSON.stringify({
            EmployeeID:event.target.EmployeeID.value,
            FirstName:event.target.EmployeeFirstName.value,
            LastName:event.target.EmployeeLastName.value,
            DateOfBirth: event.target.EmployeeDOB.value
        })
        })
        .then(res =>res.json())
        .then((result)=>{
            alert(result);
        },
        (error)=>{
            alert('Failed');
        })

    }

    render()
    {
        return(
            <div className = "container">
            <Modal 
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter">
                
             
 
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Edit Employee</Modal.Title>
            </Modal.Header>

            <Modal.Body>
                <Row>
                    <Col sm={6}>
                    <Form onSubmit={this.handleSubmit}>
                        <Form.Group controlId="EmployeeID">
                            <Form.Label>Employee ID</Form.Label>
                            <Form.Control type = "text" name="EmployeeID" 
                            required 
                            disabled
                            defaultValue={this.props.empid}
                            placeholder="EmployeeID"/>
                        </Form.Group>
                        <Form.Group controlId="EmployeeFirstName">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control type = "text" name="EmployeeFirstName" 
                            required 
                            defaultValue = {this.props.empname}
                            placeholder="EmployeeFirstName"/>
                        </Form.Group>
                        <Form.Group controlId="EmployeeLastName">
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control type = "text" name="EmployeeLastName" 
                            required 
                            defaultValue = {this.props.empLastName}
                            placeholder="EmployeeLastName"/>
                        </Form.Group>
                        <Form.Group controlId="EmployeeDOB">
                            <Form.Label>Date of Birth</Form.Label>
                            <Form.Control type = "date" name="EmployeeDOB" 
                            required 
                            value = {this.props.empDOB}                          
                            placeholder="date of birth"/>
                        </Form.Group>
                        <Form.Group>
                            <Button variant="primary" type="submit">
                                Update Employee
                            </Button>
                        </Form.Group>
                    </Form>
                    </Col>
                </Row>
            </Modal.Body>

            <Modal.Footer>
                <Button variant="danger" onClick={this.props.onHide}>Close</Button>
            </Modal.Footer>
            </Modal>
            </div>


        )
    }
}

