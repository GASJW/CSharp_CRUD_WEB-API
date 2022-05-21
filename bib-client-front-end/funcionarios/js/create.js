window.onload = () => {
    //console.log("Abriu o create de funcionarios");
    document.querySelector("#btnVoltar").onclick = goToIndex;
    document.querySelector("#btnSalvar").onclick = salvar;
}

function goToIndex(){
    document.location = "index.html";
}

function salvar(){
    console.log("Salvar")
    const funcionario = getFuncionario();
    const msg = validar(funcionario);
    if(msg === ""){ //validado
        //console.log("Invocar API")
        post(funcionario);
    }else{
        exibirMensagemValidacao(msg);
    }
}

function validar(funcionario){
    let msg = "";
    if (funcionario.Nome  === ""){
        msg += "Nome\n";
    }
    if (funcionario.Sobrenome === ""){
        msg += "Sobrenome\n"
    }
    if (funcionario.Cargo === ""){
        msg += "Cargo\n"
    }
    if (funcionario.Idade === ""){
        msg += "Idade\n"
    }
    if (msg != ""){
        msg = "Os dados abaixo são obrigatórios:\n" + msg;
        
    }
    return msg;
}

function getFuncionario(){
    console.log("getFuncionario entrou")
    const funcionario = {
        "Nome": getInputNome().value.trim(),
        "Sobrenome": getInputSobrenome().value.trim(),
        "Cargo":  getInputCargo().value.trim(),
        "Idade":  getInputIdade().value.trim()
    }
    return funcionario;
}

function getInputNome(){
    return document.querySelector("#txtNome");
}

function getInputSobrenome(){
    return document.querySelector("#txtSobrenome");
}

function getInputCargo(){
    return document.querySelector("#txtCargo");
}

function getInputIdade(){
    return document.querySelector("#txtIdade");
}

function exibirMensagemValidacao(mensagem){
    alert(mensagem);
}

function post(funcionario){
    console.log(funcionario);
    const endpoint = "https://localhost:44393/api/funcionarios";
    fetch(endpoint, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(funcionario)
    })
    .then(response => gerenciarResposta(response))
    .catch(error => gerenciarErro(error));
}

function gerenciarResposta(response){
    if (response.status === 200){
        exibirMensagemOK();
        goToIndex();
     }else if (response.status === 404){
        throw (1040);
     }else if (response.status === 500){
        throw (5000);
     }else if (response.status === 400){
        throw (4000);
     }else{
        throw (-1);
    }
}

function exibirMensagemOK(){
    alert("Funcionário adicionado com sucesso!");
}

function gerenciarErro(erro){
    exibirDadosErro(erro);
}

function exibirDadosErro(erro){
    let descricaoErro = getDescricaoErro(erro);
    alert(descricaoErro);
}