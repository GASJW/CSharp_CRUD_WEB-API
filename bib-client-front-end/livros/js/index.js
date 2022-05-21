window.onload = () => {
    //console.log("Abriu o index de livros");
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
    const endpoint = "https://localhost:44393/api/livros";

    fetch(endpoint)
        .then(response => gerenciarRespostaPesquisa(response))
        .then(data => gerenciarLivros(data))
        .catch(error => gerenciarErro(error));
}

//Solução Wesley//
function pesquisarId(id){
    const endpoint=`https://localhost:44393/api/livros/${id}`;
    fetch(endpoint)
        .then (response => gerenciarRespostaPesquisa(response))
        .then (data => gerenciarLivro(data))
        .catch(error => gerenciarErro(error));
}

function gerenciarLivros(livros) {
    //console.log(livros);
    const tabela = getTabelaLivros();
    limparTabela(tabela);
    inserirCabecalhoLivro(tabela);
    //console.log(tabela);
    if (livros.length != 0) {
        livros.forEach(livro => inserirDadosLivro(tabela, livro));
    } else {
        exibirDadosErro(1004);
    }
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
    limparTabela(getTabelaLivros());
    exibirDadosErro(erro);
}

function exibirDadosErro(erro){
    let descricaoErro = getDescricaoErro(erro);
    const tabela = getTabelaLivros();
    limparTabela(tabela);
    inserirCabecalhoLivro(tabela);
    inserirDadosErro(tabela, descricaoErro);
}

function gerenciarLivro(livro){
    const tabela = getTabelaLivros();
    limparTabela(tabela);
    inserirCabecalhoLivro(tabela);
    inserirDadosLivro(tabela, livro);
}

function getTabelaLivros(){
    return document.querySelector("#tbLivros");
}

function limparTabela(tabela) {
    tabela.innerHTML = "";
}

function inserirCabecalhoLivro(tabela) {
    let tr, td;
    tr = tabela.insertRow();
    tr.className = "w3-blue";
    td = tr.insertCell(0);
    td.innerHTML = "Id";
    td.className = "w3-center";
    td = tr.insertCell(1);
    td.innerHTML = "Título";
    td.className = "w3-center";
    td = tr.insertCell(2);
    td.innerHTML = "Autor";
    td.className = "w3-center";
    td = tr.insertCell(3);
    td.innerHTML = "Páginas";
    td.className = "w3-center";
    td = tr.insertCell(4);
    td.innerHTML = "";
}

function inserirDadosLivro(tabela, livro) {
    let tr, td;
    tr = tabela.insertRow();
    td = tr.insertCell(0);
    td.innerHTML = `<a href='edit.html?id=${livro.Id}'>${livro.Id}</a>`;
    td = tr.insertCell(1);
    td.innerHTML = livro.Titulo;
    td = tr.insertCell(2);
    td.innerHTML = (livro.Autor != null) ? livro.Autor : '-';
    td = tr.insertCell(3);
    td.innerHTML = livro.NumeroDePaginas;
    td = tr.insertCell(4);
    td.innerHTML = `<a href="JavaScript:confirmarExclusao(${livro.Id}, '${livro.Titulo}')">Excluir</a>`;
}

function inserirDadosErro(tabela, descricaoErro) {
    const tr = tabela.insertRow();

    let td;
    td = tr.insertCell(0);
    td.innerHTML = descricaoErro;
    td.colSpan = 5;
}

function confirmarExclusao(id, titulo){
    //console.log(`Confirmar exclusão:${id}`)
    if(confirm(`Deseja excluir o livro ${id} - ${titulo}?`)){
        excluir(id);
    }
}

function excluir(id){
    //console.log(`Excluir: ${id}`);
    const endpoint=`https://localhost:44393/api/livros/${id}`;
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
    alert("Livro removido com sucesso!");
}