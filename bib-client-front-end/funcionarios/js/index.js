window.onload = () => {
    //console.log("Abriu o index de funcionários");
    document.querySelector("#btnNovo").onclick = goToNovo;
    document.querySelector("#btnPesquisar").onclick = pesquisar;
}

function goToNovo(){
    document.location = "create.html";
}

function pesquisar(){
    const id = getInputId().value;

    if(id === ""){
        pesquisarTodos();
    }else{
        pesquisarId(id);
    }
}

function getInputId(){
    return document.querySelector("#txtId");
}

function pesquisarTodos(){
    //console.log("Inicio da Pesquisa");
    const endpoint = "https://localhost:44393/api/funcionarios";
    fetch(endpoint)
        .then (response => gerenciarRespostaPesquisa(response))
        .then (data => gerenciarFuncionarios(data))
        .catch(error => gerenciarErro(error));
}

function exibirDadosFuncionarios (funcionarios){
    const tabela=getTabela();
    limparTabela(tabela);
    inserirCabecalho(tabela);
    if (funcionarios.length != 0){
        funcionarios.forEach(funcionario => inserirDadosFunc(tabela, funcionario));
     }else{
        exibirDadosErro(2040)
    }
}

function gerenciarFuncionarios(funcionarios){
    //console.log(funcionarios);
    const tabela = getTabelaFunc();
    limparTabela(tabela);
    inserirCabecalho(tabela);
    //console.log(tabela);
    if(funcionarios.length != 0){
        funcionarios.forEach(funcionario => inserirDadosFunc (tabela, funcionario));
    }else{
        exibirDadosErro(2040);
    }
}

function pesquisarId(id){
    //console.log("Inicio da Pesquisa");
    const endpoint = `https://localhost:44393/api/funcionarios/${id}`;

    fetch(endpoint)
        .then (response => gerenciarRespostaPesquisa(response))
        .then (data => gerenciarFuncionario(data))
        .catch(error => gerenciarErro(error));
}

function gerenciarFuncionario(funcionario){
    //console.log(funcionario)
    const tabela = getTabelaFunc();
    limparTabela(tabela);
    inserirCabecalho(tabela);
    inserirDadosFunc(tabela, funcionario);
}

function gerenciarRespostaPesquisa(response){
    if (response.status === 200){
        return response.json();
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

function gerenciarErro(erro){
    limparTabela(getTabelaFunc());
    exibirDadosErro(erro);
}

function exibirDadosErro(erro){
    let descricaoErro = getDescricaoErro(erro);
    const tabela = getTabelaFunc();
    limparTabela(tabela);
    inserirCabecalho(tabela);
    inserirDadosErro(tabela, descricaoErro);
}

function inserirDadosErro(tabela, descricaoErro){
    const tr = tabela.insertRow();

    let td;
    td = tr.insertCell(0);
    td.innerHTML = descricaoErro;
    td.colSpan = 6;
}

function getTabelaFunc(){
    return document.querySelector("#tbFuncionarios");
}

function limparTabela(tabela){
    tabela.innerHTML = "";
}

function inserirCabecalho(tabela){
    let tr, th;
    tr = tabela.insertRow();
    tr.className = "w3-blue";
    th = tr.insertCell(0);
    th.innerHTML = "Id";
    th.className = "w3-center";
    th = tr.insertCell(1);
    th.innerHTML = "Nome";
    th.className = "w3-center";
    th = tr.insertCell(2);
    th.innerHTML = "Sobrenome";
    th.className = "w3-center";
    th = tr.insertCell(3);
    th.innerHTML = "Cargo";
    th.className = "w3-center";
    th = tr.insertCell(4);
    th.innerHTML = "Idade";
    th.className = "w3-center";
    th = tr.insertCell(5);
    th.innerHTML = "";
}

function inserirDadosFunc(tabela, funcionario){
    let tr, td;
    tr = tabela.insertRow();
    td = tr.insertCell(0);
    td.innerHTML = `<a href='edit.html?id=${funcionario.Id}'>${funcionario.Id}</a>`;
    td = tr.insertCell(1);
    td.innerHTML = funcionario.Nome;
    td = tr.insertCell(2);
    td.innerHTML = funcionario.Sobrenome;
    td = tr.insertCell(3);
    td.innerHTML = funcionario.Cargo;
    td = tr.insertCell(4);
    td.innerHTML = funcionario.Idade;
    td = tr.insertCell(5);
    td.innerHTML = `<a href="JavaScript:confirmarExclusao(${funcionario.Id}, '${funcionario.Nome}')">Excluir</a>`;
}

function confirmarExclusao(id, nome){
    //console.log(`Confirmar exclusão:${id}`)
    if(confirm(`Deseja excluir o livro ${id} - ${nome}?`)){
        excluir(id);
    }
}

function excluir(id){
    //console.log(`Excluir: ${id}`);
    const endpoint=`https://localhost:44393/api/funcionarios/${id}`;
    fetch(endpoint, {
        method: "delete"
    })
        .then(response => gerenciarRespostaExclusao(response))
        .catch(error => gerenciarErro(error));
}

function gerenciarRespostaExclusao(response){
    if (response.status === 200){
        exibirMensagemExclusao();
        pesquisar();
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

function exibirMensagemExclusao(){
    alert("Funcionário removido com sucesso!");
}