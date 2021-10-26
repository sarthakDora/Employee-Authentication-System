import React,{Component} from 'react'
import { Modal, Button, Row, Col, Form } from 'react-bootstrap'

export class AddEmpModal extends Component
{
    constructor(props){
        super(props)
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'employee',
        {method:'POST',
        headers:{
            'Accept':'application/json',
            'Content-Type':'application/json'},
        body:JSON.stringify({
            FirstName:event.target.EmployeeFirstName.value
            // LastName:event.target.EmployeeLastName.value,
            // DateOfBirth:event.target.EmployeeDOB.value,
            // PhotoFIleName:event.target.EmployeePhoto.value
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
                    Add Employee</Modal.Title>
            </Modal.Header>

            <Modal.Body>
                <Row>
                    <Col sm={6}>
                    <Form onSubmit={this.handleSubmit}>
                        <Form.Group controlId="EmployeeName">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control type = "text" name="EmployeeFirstName" required placeholder="Please enter first name"/>
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control type = "text" name="EmployeeLastName" required placeholder="Please enter last name"/>
                            <Form.Label>DOB</Form.Label>
                            <Form.Control type = "date" name="EmployeeDOB" required placeholder="Please enter date of birth"/>
                        </Form.Group>
                        <Form.Group>
                            <Button variant="primary" type="submit">
                                Add Employee
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