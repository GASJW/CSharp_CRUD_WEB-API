window.onload = () => {
  //console.log("Abriu o edit de livros");
  document.querySelector("#btnVoltar").onclick = goToIndex;
  document.querySelector("#btnAlterar").onclick = alterar;
  const id = getQueryStringId();
  //console.log(id);
  if (validarQueryStringId(id)) {
    pesquisarPorId(id);
  } else {
    gerenciarParametrosInvalidos();
  }
};

function validarQueryStringId(id) {
  return !(id === null || id === "");
}

function goToIndex() {
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

function pesquisarPorId(id) {
  //console.log("Inicio da pesquisa por Id");
  const endpoint = `https://localhost:44393/api/livros/${id}`;
  fetch(endpoint)
    .then((response) => gerenciarRespostaPesquisa(response))
    .then((data) => exibirDadosLivro(data))
    .catch((error) => error);
  //console.log("Fim da pesquisa por Id");
}

function gerenciarRespostaPesquisa(response) {
  //console.log("gerenciarRespostaPesquisa=", response);
  if (response.status === 200) {
    return response.json();
  } else if (response.status === 404) {
    throw 1040;
  } else if (response.status === 500) {
    throw 5000;
  } else if (response.status === 400) {
    throw 4000;
  } else {
    throw -1;
  }
}

function exibirDadosLivro(livro) {
  //console.log(livro);
  getInputId().value = livro.Id;
  getInputTitulo().value = livro.Titulo;
  getInputAutor().value = livro.Autor;
  getInputPaginas().value = livro.NumeroDePaginas;
}

function gerenciarErro(erro) {
  exibirDadosErro(erro);
  goToIndex();
}

function exibirDadosErro(erro) {
  let descricaoErro = getDescricaoErro(erro);
  alert(descricaoErro);
}

function getInputId() {
  return document.querySelector("#hddId");
}

function getInputTitulo() {
  return document.querySelector("#txtTitulo");
}

function getInputAutor() {
  return document.querySelector("#txtAutor");
}

function getInputPaginas() {
  return document.querySelector("#txtNumerosPaginas");
}

function alterar() {
  console.log("alterar");
  const livro = getLivro();
  const msg = validar(livro);
  if (msg === "") {
    //Validado.
    //console.log("invocar a API");
    put(livro);
  } else {
    //Precisa de correção.
    exibirMensagemValidacao(msg);
  }
}

function getLivro() {
  const livro = {
    Id: getInputId().value,
    Titulo: getInputTitulo().value.trim(),
    Autor: getInputAutor().value.trim(),
    NumeroDePaginas: getInputPaginas().value.trim(),
  };
  return livro;
}

function validar(livro) {
  let msg = "";
  if (livro.Titulo === "") {
    msg += "Título\n";
  }
  if (livro.NumeroDePaginas === "") {
    msg += "Número de páginas\n";
  }
  if (msg != "") {
    msg = "Os dados abaixo são obrigatórios:\n" + msg;
  }
  return msg;
}

function exibirMensagemValidacao(mensagem) {
  alert(mensagem);
}

function put(livro) {
  //console.log(livro);
  const endpoint = `https://localhost:44393/api/livros/${livro.Id}`;
  fetch(endpoint, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(livro),
  })
    .then((response) => gerenciarResposta(response))
    .catch((error) => gerenciarErro(error));
}

function gerenciarResposta(response) {
  //console.log("gerenciarResposta=", response);
  if (response.status === 200) {
    exibirMensagemOK();
    goToIndex();
  } else if (response.status === 404) {
    throw 1040;
  } else if (response.status === 500) {
    throw 5000;
  } else if (response.status === 400) {
    throw 4000;
  } else {
    throw -1;
  }
}

function exibirMensagemOK() {
  alert("O livro foi alterado com sucesso!");
}

function gerenciarErro(erro) {
  //console.log("gerenciarErro=", erro);
  exibirDadosErro(erro);
}

function exibirDadosErro(erro) {
  let descricaoErro = getDescricaoErro(erro);
  alert(descricaoErro);
}
