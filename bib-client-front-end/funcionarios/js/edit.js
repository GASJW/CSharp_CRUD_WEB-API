window.onload = () => {
    document.querySelector("#btnVoltar").onclick = goToIndex;
    document.querySelector("#btnAlterar").onclick = alterar;
    const id = getQueryStringId();
    if(validarQueryStringId(id)){
        pesquisarPorId(id);
    }else{
        gerenciarParametrosInvalidos();
    }
}

function validarQueryStringId(id){
    return !(id === null || id === "");
}

function pesquisarPorId(id) {
    //console.log("Inicio da pesquisa por Id");
    const endpoint = `https://localhost:44393/api/funcionarios/${id}`;
    fetch(endpoint)
        .then(response => gerenciarRespostaPesquisa(response))
        .then(data => exibirDadosFuncionario(data))
        .catch(error => gerenciarFuncionario(error));
    //console.log("Fim da pesquisa por Id");
}

function gerenciarRespostaPesquisa(response) {
    //console.log("gerenciarRespostaPesquisa=", response);
    if (response.status === 200) {
        return response.json();
    } else if (response.status === 404) {
        throw (1040);
    } else if (response.status === 500) {
        throw (5000);
    } else if (response.status === 400) {
        throw (4000);
    } else {
        throw (-1);
    }
}

function exibirDadosFuncionario(funcionario) {
    //console.log(livro);
    getInputId().value = funcionario.Id;
    getInputNome().value = funcionario.Nome;
    getInputSobrenome().value = funcionario.Sobrenome;
    getInputCargo().value = funcionario.Cargo;
    getInputIdade().value = funcionario.Idade;
}

function getInputId(){
    return document.querySelector("#hddId")
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

function alterar() {
    console.log("alterar");
    const funcionario = getFuncionario();
    const msg = validar(funcionario);
    if (msg === "") { //Validado.
        //console.log("invocar a API");
        put(funcionario);
    } else { //Precisa de correção.
        exibirMensagemValidacao(msg);
    }
}

function getFuncionario(){
    const funcionario = {
        "Id": getInputId().value.trim(),
        "Nome": getInputNome().value.trim(),
        "Sobrenome": getInputSobrenome().value.trim(),
        "Cargo":  getInputCargo().value.trim(),
        "Idade":  getInputIdade().value.trim()
    }
    return funcionario;
}

function validar(funcionario){
    let msg = "";
    if (funcionario.Nome === "") {
        console.log(msg);
        msg += `${funcionario.Nome}\n`;
    }
    if (funcionario.Sobrenome === "") {
        console.log(msg);
        msg += `${funcionario.Sobrenome}\n`;
    }
    if (funcionario.Cargo === "") {
        console.log(msg);
        msg += `${funcionario.Cargo}\n`;
    }
    if (funcionario.Idade === "") {
        console.log(msg);
        msg += `${funcionario.Idade}`
    }
    if (msg.length > 0) {
        console.log(msg);
        msg = "Os dados abaixo são obrigatórios:\n" + msg;
    }
    console.log(msg)
    return msg;
}

function exibirMensagemValidacao(mensagem) {
    alert(mensagem);
}

function put(funcionario) {
    console.log(funcionario);
    const endpoint = `https://localhost:44393/api/funcionarios/${funcionario.Id}`;
    fetch(endpoint, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(funcionario)
    })
        .then(response => gerenciarResposta(response))
        .catch(error => gerenciarErro(error));
}

function gerenciarResposta(response) {
    //console.log("gerenciarResposta=", response);
    if (response.status === 200) {
        exibirMensagemOK();
        goToIndex();
    } else if (response.status === 404) {
        throw (1040);
    } else if (response.status === 500) {
        throw (5000);
    } else if (response.status === 400) {
        throw (4000);
    } else {
        throw (-1);
    }
}

function exibirMensagemOK() {
    alert("O funcionario foi alterado com sucesso!");
}

function gerenciarErro(erro) {
    exibirDadosErro(erro);
    goToIndex();
}

function exibirDadosErro(erro) {
    let descricaoErro = getDescricaoErro(erro);
    alert(descricaoErro);
}

function goToIndex(){
    document.location = "index.html";
}

function getQueryStringId() {
    const params = new URLSearchParams(window.location.search);
    return params.get("id");
}

function gerenciarParametrosInvalidos() {
    exibirMEnsagemParametrosInvalidos();
    goToIndex();
}

function exibirMEnsagemParametrosInvalidos() {
    alert("Parâmetros inválidos.");
}