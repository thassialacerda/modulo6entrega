import React, { useState, useEffect } from 'react';
import './App.css';

import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';


function App() {

  const baseUrl = "https://localhost:44365/api/Clientes";

  const [data, setData] = useState([]);
  
  const [modalIncluir, setModalIncluir] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [modalExcluir, setModalExcluir] = useState(false);

  const [clienteSelecionado, SetClienteSelecionado] = useState({

    id: '',
    nome: '',
    email: '',
    endereco: '',
    cep: '',
    destino: '',
    quantidade: '',
    formapag: '',

  })

  const selecionarCliente = (cliente, opcao) => {
    SetClienteSelecionado(cliente);
    (opcao==="Editar") ?
      abrirFecharModalEditar() : abrirFecharModalExcluir();
  }

  const abrirFecharModalIncluir = () => {
    setModalIncluir(!modalIncluir);
  }

  const abrirFecharModalEditar = () => {
    setModalEditar(!modalEditar);
  }

  const abrirFecharModalExcluir = () => {
    setModalExcluir(!modalExcluir);
  }

  const handleChange = e => {
    const { name, value } = e.target;
    SetClienteSelecionado({
      ...clienteSelecionado,
      [name]: value
    });
    console.log(clienteSelecionado);

  }

  const pedidoGet = async () => {
    await axios.get(baseUrl)
      .then(Response => {
        setData(Response.data);
      }).catch(erro => {
        console.log(erro);
      })
  }

  const pedidoPost = async () => {
    delete clienteSelecionado.id;
    await axios.post(baseUrl, clienteSelecionado)
      .then(Response => {
        setData(data.concat(Response.data));
        abrirFecharModalIncluir();
      }).catch(erro => {
        console.log(erro);
      })
  }

  const pedidoPut = async () => {
    clienteSelecionado.nome = parseInt(clienteSelecionado.nome);
    await axios.put(baseUrl + "/" + clienteSelecionado.nome, clienteSelecionado)
      .then(response => {
        var resposta = response.data;
        var dadosAuxiliar = data;
        dadosAuxiliar.forEach(cliente => {
          if (cliente.id === clienteSelecionado.id) {
            cliente.nome = resposta.nome;
            cliente.email = resposta.email;
            cliente.endereco = resposta.endereco;
            cliente.cep = resposta.cep;
            cliente.destino = resposta.destino;
            cliente.quantidade = resposta.quantidade;
            cliente.formapag = resposta.formapag;
          }
        });
        abrirFecharModalEditar();
      }).catch(error => {
        console.log(error);
      });
  }

  const pedidoDelete = async () => {
    await axios.delete(baseUrl + "/" + clienteSelecionado.id)
      .then(Response => {
        setData(data.filter(cliente => cliente.id !== Response.data));
        abrirFecharModalExcluir();
      })
      .catch(error => {
        console.log(error);
      })
  }


  useEffect(()=>{
    pedidoGet();
  })
  

  return (
    <div className="App">
      
        <br />
        <h3>Cadastro de Cotação</h3>
        <header>
          <br></br>
          <button className="btn btn-success" onClick={() => abrirFecharModalIncluir()}>Incluir Nova Cotação</button>
        </header>
        <br></br>
        <table className="table table-bordered" >
          <thead>
            <tr>
              <th>Id</th>
              <th>Nome</th>
              <th>Email</th>
              <th>Endereço</th>
              <th>Cep</th>
              <th>Destino</th>
              <th>Quantidade</th>
              <th>Forma de Pagamento</th>
            </tr>

          </thead>
          <tbody>
            {data.map(cliente => (
              <tr key={cliente.id}>
                <td>{cliente.id}</td>
                <td>{cliente.nome}</td>
                <td>{cliente.email}</td>
                <td>{cliente.endereco}</td>
                <td>{cliente.cep}</td>
                <td>{cliente.destino}</td>
                <td>{cliente.quantidade}</td>
                <td>{cliente.formapag}</td>
                <td>
                  <button className="btn btn-primary" onClick={() => selecionarCliente(cliente, "Editar")}>Editar</button> {" "}
                  <button className="btn btn-danger" onClick={() => selecionarCliente(cliente, "Excluir")}>Excluir</button>
                </td>
              </tr>
            ))}

          </tbody>
        </table>

        <Modal isOpen={modalIncluir}>
          <ModalHeader>Incluir Cotação</ModalHeader>
          <ModalBody>
            <div className="form-group">
              <label>Nome:</label>
              <br />
              <input type="text" className="form-control" name="nome" onChange={handleChange} />
              <br />
              <label>Email:</label>
              <br />
              <input type="text" className="form-control" name="email" onChange={handleChange} />
              <br />
              <label>Endereço:</label>
              <br />
              <input type="text" className="form-control" name="endereco" onChange={handleChange} />
              <br />
              <label>Cep:</label>
              <br />
              <input type="text" className="form-control" name="cep" onChange={handleChange} />
              <br />
              <label>Destino:</label>
              <br />
              <input type="text" className="form-control" name="destino" onChange={handleChange} />
              <br />
              <label>Quantidade:</label>
              <br />
              <input type="text" className="form-control" name="quantidade" onChange={handleChange} />
              <br />
              <label>Forma de Pagamento:</label>
              <br />
              <input type="text" className="form-control" name="formapag" onChange={handleChange} />
              <br />
            </div>
          </ModalBody>
          <ModalFooter>
            <button className="btn btn-primary" onClick={() => pedidoPost()}>Incluir</button>{"  "}
            <button className="btn btn-danger" onClick={() => abrirFecharModalIncluir()}>Cancelar</button>
          </ModalFooter>
        </Modal>

        <Modal isOpen={modalEditar}>
          <ModalHeader>Editar Cotação</ModalHeader>
          <ModalBody>
            <div className="form-group">
              <label>ID: </label>
              <input type="text" className="form-control" readOnly value={clienteSelecionado && clienteSelecionado.id} />
              <label>Nome:</label>
              <br />
              <input type="text" className="form-control" name="nome" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.nome} />
              <br />
              <label>Email:</label>
              <br />
              <input type="text" className="form-control" name="email" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.email} />
              <br />
              <label>Endereço:</label>
              <br />
              <input type="text" className="form-control" name="endereco" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.endereco} />
              <br />
              <label>Cep:</label>
              <br />
              <input type="text" className="form-control" name="cep" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.cep} />
              <br />
              <label>Destino:</label>
              <br />
              <input type="text" className="form-control" name="destino" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.destino} />
              <br />
              <label>Quantidade:</label>
              <br />
              <input type="text" className="form-control" name="quantidade" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.quantidade} />
              <br />
              <label>Forma de Pagamento:</label>
              <br />
              <input type="text" className="form-control" name="formapag" onChange={handleChange}
                value={clienteSelecionado && clienteSelecionado.formapag} />
              <br />
            </div>
          </ModalBody>
          <ModalFooter>
            <button className="btn btn-primary" onClick={() => pedidoPut()}>Editar</button>{"   "}
            <button className="btn btn-danger" onClick={() => abrirFecharModalEditar()}>Cancelar</button>
          </ModalFooter>
        </Modal>

        <Modal isOpen={modalExcluir}>

          <ModalBody>
            Confirma a exclusão desta cotação : {clienteSelecionado && clienteSelecionado.nome} ?
          </ModalBody>

          <ModalFooter>
            <button className="btn btn-danger" onClick={() => pedidoDelete()}> Sim </button>
            <button className="btn btn-secondary" onClick={() => abrirFecharModalExcluir()}> Não </button>
          </ModalFooter>
        </Modal>
      
    </div>
  );
}

export default App;
