window.onload = () => {
    //console.log("Abriu o create de livros");
    document.querySelector("#btnVoltar").onclick = goToIndex;
    document.querySelector("#btnSalvar").onclick = salvar;
}

function goToIndex(){
    document.location = "index.html";
}

function salvar(){
    //console.log("Salvar")
    const livro = getLivro();
    const msg = validar(livro);
    if(msg === ""){ //validado
        //console.log("Invocar API")
        post(livro);
    }else{
        exibirMensagemValidacao(msg);
    }
}

function validar(livro){
    let msg = "";
    if (livro.Titulo  === ""){
        msg += "Titulo\n";
    }
    if (livro.NumeroDePaginas === ""){
        msg += "Número de páginas\n"
    }
    if (msg != ""){
        msg = "Os dados abaixo são obrigatórios:\n" + msg;
        
    }
    return msg;
}

function getLivro(){
    const livro = {
        "Titulo": getInputTitulo().value.trim(),
        "Autor": getInputAutor().value.trim(),
        "NumeroDePaginas":  getInputNumeroPaginas().value.trim(),
    }
    return livro;
}

function getInputTitulo(){
    return document.querySelector("#txtTitulo");
}

function getInputAutor(){
    return document.querySelector("#txtAutor");
}

function getInputNumeroPaginas(){
    return document.querySelector("#txtNumerosPaginas");
}

function exibirMensagemValidacao(mensagem){
    alert(mensagem);
}

function post(livro){
    console.log(livro);
    const endpoint = "https://localhost:44393/api/livros";
    fetch(endpoint, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(livro)
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
    alert("Livro adicionado com sucesso!");
}

function gerenciarErro(erro){
    exibirDadosErro(erro);
}

function exibirDadosErro(erro){
    let descricaoErro = getDescricaoErro(erro);
    alert(descricaoErro);
}